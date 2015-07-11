using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Xml;

namespace edu.dts.iTunesULibrary
{
    /// <summary>
    /// Library for connected to Apple's iTunesU Web Service
    /// </summary>
    public class iTunesU
    {
        // EVENTS
        public event OnReceiveDataHandler OnReceiveData;

        // PROPERTIES

        public string SiteDomain { get; set; } = "";

        public string SharedSecret { get; set; } = "";

        private static string _browseUrl = "https://deimos.apple.com/WebObjects/Core.woa/Browse/";
        private static string _uploadUrl = "https://deimos.apple.com/WebObjects/Core.woa/API/GetUploadURL/";
        private static string _webServicesDocUrl = "https://deimos.apple.com/WebObjects/Core.woa/API/ProcessWebServicesDocument/";

        public string GetUploadUrl(string classToken)
        {
            string authUrl = _uploadUrl + SiteDomain + ((classToken != "") ? "." + classToken : "");
            return SendAuthenticationToITunes(authUrl, GetAuthorization());
        }

        public string GetWebServicesUrl()
        {
            return _webServicesDocUrl + SiteDomain + "?" + GetAuthorization(); // +"&type=XMLControlFile";
        }

        public string GetAuthorization()
        {

            string[] credentialArray = new string[] { "Administrator@urn:mace:itunesu.com:sites:" + SiteDomain };

            string displayName = "Admin User";
            string emailAddress = "admin@dts.edu";
            string userName = "Admin";
            string userIdentifier = "12345";


            string identity = "";
            string currentTime = "";
            string credentials = "";

            // create identity
            if (displayName.Length > 0)
                identity += "\"" + displayName + "\"";
            if (emailAddress.Length > 0)
                identity += "<" + emailAddress + ">";
            if (userName.Length > 0)
                identity += "(" + userName + ")";
            if (userIdentifier.Length > 0)
                identity += "[" + userIdentifier + "]";

            // create credentials				
            credentials = string.Join(";", credentialArray);

            // get UNIX time
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = DateTime.UtcNow - epoch;
            currentTime = ((int)span.TotalSeconds).ToString();

            // encode and concatenate values
            string token = "credentials=" + EncodeString(credentials) + "&identity=" + EncodeString(identity) + "&time=" + currentTime;

            // Generate the signature string
            string signature = "&signature=" + GenerateSignature(token, SharedSecret);

            return token + signature;
        }

        private string EncodeString(string input)
        {
            return FixURLEncoding(System.Web.HttpUtility.UrlEncode(input));
        }

        private string FixURLEncoding(string urlEncodedString)
        {
            StringBuilder s = new StringBuilder(urlEncodedString);
            for (int i = 0; i < s.Length; i++)
            {
                // Find an encoded character and make the letter
                // part upper case because the iTunes U algorithm
                // is very sensitive.
                if (s[i] == '%')
                {
                    if (Char.IsDigit(s[i + 1]) && Char.IsLetter(s[i + 2]))
                    {
                        s[i + 2] = Char.ToUpper(s[i + 2]);
                    }
                }
            }

            // Encode ‘(’ and ‘)’ b/c UrlEncode doesn’t do it and iTunes U
            // is sensitive to this
            s = s.Replace("(", "%28");
            s = s.Replace(")", "%29");

            return s.ToString();

        }


        private string GenerateSignature(string token, string sharedSecret)
        {
            //get the bytes of shared key
            byte[] key = Encoding.ASCII.GetBytes(sharedSecret);

            // encode the token with the key
            UTF8Encoding ue = new UTF8Encoding();
            HMACSHA256 hmac = new HMACSHA256(key);
            byte[] hash = hmac.ComputeHash(ue.GetBytes(token.ToCharArray()));

            // Convert the bytes to hex
            StringBuilder sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString().ToLower();

        }


        //This method makes the HTTP request. It is based on code originally posted by "Ed at NAIT"
        private string SendAuthenticationToITunes(string siteUrl, string signature)
        {

            string response = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(siteUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (signature != "")
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream());

                //The token is the query string (ie. credentials=foo&identity=bar&time=111&signature=key)
                writer.Write(signature);
                writer.Close();
            }
            try
            {
                WebResponse webResponse = request.GetResponse();
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                response = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                response = "ERROR: " + signature + "\r\n" + ex + ": ";
            }

            return response;

        }


        private string UploadFilesToRemoteUrl(string url, string filePath, string contentType)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return UploadFilesToRemoteUrl(url, Path.GetFileName(filePath), fileStream, contentType);
        }

        private string UploadFilesToRemoteUrl(string url, string filename, Stream fileStream, string contentType)
        {
            OnReceiveDataEventArgs args = new OnReceiveDataEventArgs();

            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            long length = 0;
            string response = "";

            try
            {

                // create header
                string name = "file";

                byte[] header = Encoding.UTF8.GetBytes(
                                "--" + boundary + "\r\n"
                                + "Content-Disposition: form-data; name=\"" + name
                                + "\"; filename=\"" + filename + "\"\r\n"
                                + "Content-Type: " + contentType + "\r\n"
                                + "\r\n");
                length += header.Length;

                // create footer
                byte[] footer = Encoding.UTF8.GetBytes(
                                "\r\n--" + boundary + "--\r\n");
                length += footer.Length;



                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "multipart/form-data; boundary=\"" + boundary + "\"";
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                httpWebRequest.AllowWriteStreamBuffering = true;
                httpWebRequest.UserAgent = "iTunes Upload Utility";
                httpWebRequest.Timeout = 30 * 60 * 1000;
                //httpWebRequest.ContentLength = memStream.Length;



                length += fileStream.Length;
                httpWebRequest.ContentLength = length;
                args.TotalBytes = fileStream.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                // write header
                requestStream.Write(header, 0, header.Length);

                // write file

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                int totalBytesSoFar = 0;
                int numberOfReads = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                    totalBytesSoFar += bytesRead;
                    numberOfReads++;

                    // *** Raise an event if hooked up
                    if (OnReceiveData == null) continue;
                    // *** Update the event handler
                    args.CurrentByteCount = totalBytesSoFar;
                    args.NumberOfReads = numberOfReads;
                    //args.CurrentChunk = bytesRead;
                    OnReceiveData(this, args);
                }

                // write footer
                requestStream.Write(footer, 0, footer.Length);
                fileStream.Close();
                requestStream.Flush();
                requestStream.Close();

                args.Done = true;
                OnReceiveData?.Invoke(this, args);

                try
                {

                    WebResponse webResponse = httpWebRequest.GetResponse();
                    Stream stream = webResponse.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream);
                        response = reader.ReadToEnd();
                        reader.Close();
                    }
                    webResponse.Close();
                }
                catch (Exception e)
                {
                    response = e.ToString();
                }
            }
            catch
            {
                args.Error = true;
                OnReceiveData?.Invoke(this, args);
            }
            return response;
        }

        private string UploadXmlFile(string url, string filePath)
        {
            return UploadXmlText(url, File.ReadAllText(filePath));
        }

        public string UploadXmlText(string url, string xmlText)
        {

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;

            Stream requestStream = httpWebRequest.GetRequestStream();
            StreamWriter sw = new StreamWriter(requestStream);
            sw.Write(xmlText);
            sw.Close();
            sw.Dispose();

            // get response
            string response = "";
            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);
                    response = reader.ReadToEnd();
                    reader.Close();
                }
                webResponse.Close();
            }
            catch (Exception e)
            {
                response = e.ToString();
            }

            return response;
        }


        public string UploadFile(string classToken, string filepath)
        {
            string url = GetUploadUrl(classToken);

            return UploadFilesToRemoteUrl(url, filepath, "application/octet-stream");
        }

        public delegate void OnReceiveDataHandler(object sender, OnReceiveDataEventArgs e);
        public class OnReceiveDataEventArgs
        {
            public long CurrentByteCount = 0;
            public long TotalBytes = 0;
            public int NumberOfReads = 0;
            public char[] CurrentChunk;
            public bool Done = false;
            public bool Cancel = false;
            public bool Error = false;
        }

        private XmlDocument _lastDocument = null;

        public XmlDocument GetSiteXmlDocument()
        {
            return GetSiteXmlDocument(false);
        }

        public XmlDocument GetSiteXmlDocument(bool forceRefresh)
        {
            if (_lastDocument == null || forceRefresh)
            {

                string uploadXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ITunesUDocument><Version>1.1.1</Version><ShowTree><KeyGroup>maximal</KeyGroup></ShowTree></ITunesUDocument>";

                // get upload URL
                string returnXML = UploadXmlText(GetWebServicesUrl(), uploadXML);

                _lastDocument = new XmlDocument();
                try
                {
                    _lastDocument.LoadXml(returnXML);
                }
                catch
                {
                    _lastDocument = null;
                }
            }

            return _lastDocument;
        }

        public string AttemptLogin()
        {
            return SendAuthenticationToITunes(_browseUrl + SiteDomain, GetAuthorization());
        }

        #region Track Methods

        public Track GetTrack(string handle)
        {
            return GetTrack(handle, false);
        }

        public Track GetTrack(string handle, bool refresh)
        {
            XmlDocument doc = GetSiteXmlDocument(refresh);

            XmlNode node = doc.SelectSingleNode("//Track[Handle=" + handle + "]");

            return XmlUtility.ParseTrackXml(node);
        }

        public string AddTrack(string parentHandle, Track track)
        {
            string xml = XmlUtility.ConvertTrackToAdd(parentHandle, track);

            UploadXmlText(GetWebServicesUrl(), xml);

            string returnXml = UploadXmlText(GetWebServicesUrl(), xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(returnXml);

            // get new handle
            var selectSingleNode = doc.SelectSingleNode("//AddedObjectHandle");
            if (selectSingleNode == null)
                return null;
            var newHandle = selectSingleNode.InnerText;

            GetSiteXmlDocument(true);

            return newHandle;
        }

        public void MergeTrack(string handle, Track track)
        {
            string xml = XmlUtility.ConvertTrackToMerge(handle, track);

            UploadXmlText(GetWebServicesUrl(), xml);
        }

        public void DeleteTrack(string handle)
        {
            string xml = XmlUtility.ConvertTrackToDelete(handle);

            UploadXmlText(GetWebServicesUrl(), xml);
        }

        #endregion

        #region Group Methods

        public Group GetGroup(string handle)
        {
            XmlDocument doc = GetSiteXmlDocument();

            XmlNode node = doc.SelectSingleNode("//Group[Handle=" + handle + "]");

            return XmlUtility.ParseGroupXml(node);
        }

        public void MergeGroup(string handle, Group group)
        {
            string groupPath = GetGroupPath(handle);

            string xml = XmlUtility.ConvertGroupToMerge(handle, groupPath, group);

            UploadXmlText(GetWebServicesUrl(), xml);
        }

        public string AddGroup(string parentHandle, Group group)
        {
            string parentPath = GetCoursePath(parentHandle);

            string xml = XmlUtility.ConvertGroupToAdd(parentHandle, parentPath, group);

            string returnXml = UploadXmlText(GetWebServicesUrl(), xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(returnXml);

            // get new handle
            string newHandle = doc.SelectSingleNode("//AddedObjectHandle")?.InnerText;

            GetSiteXmlDocument(true);

            return newHandle;
        }


        public void DeleteGroup(string handle)
        {
            string groupPath = GetGroupPath(handle);

            string xml = XmlUtility.ConvertGroupToDelete(handle, groupPath);

            UploadXmlText(GetWebServicesUrl(), xml);
        }
        #endregion

        #region Course Methods

        public Course GetCourse(string handle)
        {
            XmlDocument doc = GetSiteXmlDocument();

            XmlNode node = doc.SelectSingleNode("//Course[Handle=" + handle + "]");

            return XmlUtility.ParseCourseXml(node);
        }

        public void MergeCourse(string handle, Course course)
        {
            string coursePath = GetCoursePath(handle);

            string xml = XmlUtility.ConvertCourseToMerge(handle, coursePath, course);

            UploadXmlText(GetWebServicesUrl(), xml);
        }

        public string AddCourse(string parentHandle, Course course)
        {
            string parentPath = GetSectionPath(parentHandle);

            string xml = XmlUtility.ConvertCourseToAdd("", parentHandle, parentPath, course);

            string returnXml = UploadXmlText(GetWebServicesUrl(), xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(returnXml);

            // get new handle
            string newHandle = doc.SelectSingleNode("//AddedObjectHandle")?.InnerText;

            GetSiteXmlDocument(true);

            return newHandle;
        }


        public void DeleteCourse(string handle)
        {
            string coursePath = GetCoursePath(handle);

            string xml = XmlUtility.ConvertCourseToDelete(handle, coursePath);

            UploadXmlText(GetWebServicesUrl(), xml);
        }
        #endregion

        #region Section Methods

        public Section GetSection(string handle)
        {
            XmlDocument doc = GetSiteXmlDocument();

            XmlNode node = doc.SelectSingleNode("//Section[Handle=" + handle + "]");

            return XmlUtility.ParseSectionXml(node);
        }

        public void MergeSection(string handle, Section section)
        {
            string sectionPath = GetSectionPath(handle);

            string xml = XmlUtility.ConvertSectionToMerge(handle, sectionPath, section);

            UploadXmlText(GetWebServicesUrl(), xml);
        }

        public string AddSection(string parentHandle, Section section)
        {
            string parentPath = GetSitePath(parentHandle);

            string xml = XmlUtility.ConvertSectionToAdd(parentHandle, parentPath, section);

            string returnXml = UploadXmlText(GetWebServicesUrl(), xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(returnXml);

            // get new handle
            string newHandle = doc.SelectSingleNode("//AddedObjectHandle")?.InnerText;

            GetSiteXmlDocument(true);

            return newHandle;
        }


        public void DeleteSection(string handle)
        {
            string sectionPath = GetSectionPath(handle);

            string xml = XmlUtility.ConvertSectionToDelete(handle, sectionPath);

            UploadXmlText(GetWebServicesUrl(), xml);
        }
        #endregion

        #region Utility Stuff

        private string GetSitePath(string handle)
        {
            // first we have to get the path, which seems pointless
            XmlDocument doc = GetSiteXmlDocument();
            XmlNode siteNode = doc.SelectSingleNode("//Site");

            string sitePath = siteNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//");

            return sitePath;
        }

        private string GetSectionPath(string handle)
        {
            // first we have to get the path, which seems pointless
            XmlDocument doc = GetSiteXmlDocument();
            XmlNode sectionNode = doc.SelectSingleNode("//Section[Handle=" + handle + "]");
            XmlNode siteNode = sectionNode?.ParentNode;

            string sectionPath =
                $"{siteNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//")}/{sectionNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//")}";

            return sectionPath;
        }


        private string GetCoursePath(string handle)
        {
            // first we have to get the path, which seems pointless
            XmlDocument doc = GetSiteXmlDocument();
            XmlNode courseNode = doc.SelectSingleNode("//Course[Handle=" + handle + "]");
            XmlNode sectionNode = courseNode?.ParentNode;
            XmlNode siteNode = sectionNode?.ParentNode;

            string coursePath = siteNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//") + "/" +
                sectionNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//") + "/" +
                courseNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//");

            return coursePath;
        }


        private string GetGroupPath(string handle)
        {
            // first we have to get the path, which seems pointless
            XmlDocument doc = GetSiteXmlDocument();
            XmlNode groupNode = doc.SelectSingleNode("//Group[Handle=" + handle + "]");
            XmlNode courseNode = groupNode?.ParentNode;
            XmlNode sectionNode = courseNode?.ParentNode;
            XmlNode siteNode = sectionNode?.ParentNode;

            string groupPath = siteNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//") + "/" +
                sectionNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//") + "/" +
                courseNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//") + "/" +
                groupNode?.SelectSingleNode("Name")?.InnerText.Replace("/", "//");

            return groupPath;
        }

        #endregion

    }
}
