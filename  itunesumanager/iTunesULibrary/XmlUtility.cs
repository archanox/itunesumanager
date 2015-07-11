using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace edu.dts.iTunesULibrary
{
	/// <summary>
	/// Helper utility for creating iTunesU Web Service XML documents
	/// </summary>
	public class XmlUtility
	{

		#region Track

		public static string ConvertTrackToDelete(string handle)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<DeleteTrack>
<TrackHandle>" + handle + @"</TrackHandle>
</DeleteTrack>
</ITunesUDocument>";
		}

		public static string ConvertTrackToAdd(string parentHandle, Track track)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<AddTrack>
<ParentHandle>" + parentHandle + @"</ParentHandle>
<Track>
	<Name>" + System.Web.HttpUtility.HtmlEncode(track.Name) + @"</Name>
	<Handle>" + track.Handle + @"</Handle>
	<Kind>" + track.Handle + @"</Kind>
	<DiscNumber>" + track.DiscNumber + @"</DiscNumber>
	<DurationMilliseconds>" + track.DurationMilliseconds + @"</DurationMilliseconds>
	<AlbumName>" + System.Web.HttpUtility.HtmlEncode(track.AlbumName) + @"</AlbumName>
	<ArtistName>" + System.Web.HttpUtility.HtmlEncode(track.ArtistName) + @"</ArtistName>
	<DownloadURL>" + track.DownloadURL + @"</DownloadURL>
</Track>
</AddTrack>
</ITunesUDocument>";
		}

		public static string ConvertTrackToMerge(string handle, Track track)
		{
			return string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<MergeTrack>
<TrackHandle>{0}</TrackHandle>
<Track>
	<Name>{1}</Name>
	<Handle>{2}</Handle>
	<Kind>{2}</Kind>
	<DiscNumber>{3}</DiscNumber>
	<TrackNumber>{4}</TrackNumber>
	<DurationMilliseconds>{5}</DurationMilliseconds>
	<AlbumName>{6}</AlbumName>
	<ArtistName>{7}</ArtistName>
	<DownloadURL>{8}</DownloadURL>
</Track>
</MergeTrack>
</ITunesUDocument>", handle, track.Name, track.Handle, track.DiscNumber, track.TrackNumber, track.DurationMilliseconds, track.AlbumName, track.ArtistName, track.DownloadURL);
		}

		public static Track ParseTrackXml(XmlNode node)
		{
			Track track = new Track();

			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{

						case "AlbumName":
							track.AlbumName = childNode.InnerText;
							break;
						case "ArtistName":
							track.ArtistName = childNode.InnerText;
							break;
						case "DownloadURL":
							track.DownloadURL = childNode.InnerText;
							break;
						case "DurationMilliseconds":
							track.DurationMilliseconds = childNode.InnerText;
							break;
						case "Handle":
							track.Handle = childNode.InnerText;
							break;
						case "Kind":
							track.Kind = childNode.InnerText;
							break;
						case "Name":
							track.Name = childNode.InnerText;
							break;
						case "DiscNumber":
							track.DiscNumber = childNode.InnerText;
							break;
						case "TrackNumber":
							track.TrackNumber = childNode.InnerText;
							break;
					}
				}
			}

			return track;
		}

		#endregion

		#region Group

		public static string ConvertGroupToDelete(string handle, string groupPath)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<DeleteGroup>
<GroupHandle>" + handle + @"</GroupHandle>
<GroupPath>" + groupPath + @"</GroupPath>
</DeleteGroup>
</ITunesUDocument>";
		}

		public static string ConvertGroupToAdd(string parentHandle, string parentPath, Group group)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<AddGroup>
<ParentHandle>" + parentHandle + @"</ParentHandle>
<ParentPath>" + parentPath + @"</ParentPath>
<Group>
	<Name>" + group.Name + @"</Name>
	<GroupType>" + group.GroupType + @"</GroupType>
</Group>
</AddGroup>
</ITunesUDocument>";
		}


		public static string ConvertGroupToMerge(string handle, string groupPath, Group group)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<MergeGroup>
<GroupHandle>" + handle + @"</GroupHandle>
<GroupPath>" + groupPath + @"</GroupPath>
<MergeByHandle>true</MergeByHandle>
<Destructive>false</Destructive>
<Group>
	<Name>" + group.Name + @"</Name>
	<GroupType>" + group.GroupType + @"</GroupType>
</Group>
</MergeGroup>
</ITunesUDocument>";
		}

		public static Group ParseGroupXml(XmlNode node)
		{
			Group group = new Group();

			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{

						case "Name":
							group.Name = childNode.InnerText;
							break;
						case "GroupType":
							group.GroupType = childNode.InnerText;
							break;
						case "Handle":
							group.Handle = childNode.InnerText;
							break;
						case "ShortName":
							group.ShortName = childNode.InnerText;
							break;
						
					}
				}
			}

			return group;

		}

		#endregion

		#region Course

		public static string ConvertCourseToDelete(string handle, string coursePath)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<DeleteCourse>
<CourseHandle>" + handle + @"</CourseHandle>
<CoursePath>" + coursePath + @"</CoursePath>
</DeleteCourse>
</ITunesUDocument>";
		}

		public static string ConvertCourseToAdd(string templateHandle, string parentHandle, string parentPath, Course course)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<AddCourse>
<ParentHandle>" + parentHandle + @"</ParentHandle>
<ParentPath>" + parentPath + @"</ParentPath>
<TemplateHandle>" + templateHandle + @"</TemplateHandle>
<Course>
	<Name>" + course.Name + @"</Name>
    <ShortName>" + course.ShortName + @"</ShortName>
    <Identifier>" + course.Identifier + @"</Identifier>
    <Instructor>" + course.Instructor + @"</Instructor>
    <Description>" + course.Description + @"</Description>
</Course>
</AddCourse>
</ITunesUDocument>";
		}


		public static string ConvertCourseToMerge(string handle, string coursePath, Course course)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<MergeCourse>
<CourseHandle>" + handle + @"</CourseHandle>
<CoursePath>" + coursePath + @"</CoursePath>
<MergeByHandle>true</MergeByHandle>
<Destructive>false</Destructive>
<Course>
	<Name>" + course.Name + @"</Name>
    <ShortName>" + course.ShortName + @"</ShortName>
    <Identifier>" + course.Identifier + @"</Identifier>
    <Instructor>" + course.Instructor + @"</Instructor>
    <Description>" + course.Description + @"</Description>
</Course>
</MergeCourse>
</ITunesUDocument>";
		}

		public static Course ParseCourseXml(XmlNode node)
		{
			Course course = new Course();

			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{

						case "Name":
							course.Name = childNode.InnerText;
							break;
						case "ShortName":
							course.ShortName = childNode.InnerText;
							break;
						case "Handle":
							course.Handle = childNode.InnerText;
							break;
						case "Identifier":
							course.Identifier = childNode.InnerText;
							break;
						case "Instructor":
							course.Instructor = childNode.InnerText;
							break;
						case "Description":
							course.Description = childNode.InnerText;
							break;	

					}
				}
			}

			return course;

		}

		#endregion

		#region Section

		public static string ConvertSectionToDelete(string handle, string sectionPath)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<DeleteSection>
<SectionHandle>" + handle + @"</SectionHandle>
<SectionPath>" + sectionPath + @"</SectionPath>
</DeleteSection>
</ITunesUDocument>";
		}

		public static string ConvertSectionToAdd(string parentHandle, string parentPath, Section section)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<AddSection>
<ParentHandle>" + parentHandle + @"</ParentHandle>
<ParentPath>" + parentPath + @"</ParentPath>
<Section>
	<Name>" + section.Name + @"</Name>
</Section>
</AddSection>
</ITunesUDocument>";
		}


		public static string ConvertSectionToMerge(string handle, string sectionPath, Section section)
		{
			return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ITunesUDocument>
<Version>1.1.1</Version>
<MergeSection>
<SectionHandle>" + handle + @"</SectionHandle>
<SectionPath>" + sectionPath + @"</SectionPath>
<MergeByHandle>true</MergeByHandle>
<Destructive>false</Destructive>
<Section>
	<Name>" + section.Name + @"</Name>
</Section>
</MergeSection>
</ITunesUDocument>";
		}

		public static Section ParseSectionXml(XmlNode node)
		{
			Section section = new Section();

			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{

						case "Name":
							section.Name = childNode.InnerText;
							break;					
						case "Handle":
							section.Handle = childNode.InnerText;
							break;
					}
				}
			}

			return section;

		}

		#endregion

	}
}
