using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using edu.dts.iTunesULibrary;
using iTunesUManager.Properties;

namespace iTunesUManager
{
	public partial class MainForm : Form
	{
		private BackgroundWorker uploadingBackgroundWorker;

		private iTunesU _iTunes = null;

		public MainForm()
		{
			InitializeComponent();

			InitBackgroundWorker();

			iTunesTree.MouseUp += new MouseEventHandler(iTunesTree_MouseUp);
		}


	

		void MainForm_Load(object sender, System.EventArgs e)
		{
			// load values
			SiteDomain.Text = Settings.Default.SiteDomain;
			SharedSecret.Text = Settings.Default.SharedSecret;
		}

		void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			// save values			
			Settings.Default.SiteDomain = SiteDomain.Text;
			Settings.Default.SharedSecret = SharedSecret.Text;
			
			Settings.Default.Save();
		}

		private void InitBackgroundWorker()
		{
			uploadingBackgroundWorker = new BackgroundWorker();
			uploadingBackgroundWorker.DoWork +=
				new DoWorkEventHandler(uploadingBackgroundWorker_DoWork);
			uploadingBackgroundWorker.RunWorkerCompleted +=
				new RunWorkerCompletedEventHandler(uploadingBackgroundWorker_RunWorkerCompleted);
			uploadingBackgroundWorker.ProgressChanged +=
				new ProgressChangedEventHandler(uploadingBackgroundWorker_ProgressChanged);
		}


		void iTunesTree_MouseUp(object sender, MouseEventArgs e)
		{

			if (e.Button == MouseButtons.Right)
			{
				Point clickPoint = new Point(e.X, e.Y);
				TreeNode clickNode = iTunesTree.GetNodeAt(clickPoint);
				if (clickNode == null) return;
				// Convert from Tree coordinates to Screen coordinates
				Point screenPoint = iTunesTree.PointToScreen(clickPoint);
				// Convert from Screen coordinates to Form coordinates
				Point formPoint = this.PointToClient(screenPoint);
				// Show context menu
				contextMenuStrip1.Items.Clear();

				NodeData nodeData = clickNode.Tag as NodeData;

				if (nodeData == null)
					return;

				switch (nodeData.NodeType)
				{
					case "Site":
						ToolStripButton addSectionItem = new ToolStripButton("Add Section");
						addSectionItem.Click += new EventHandler(AddSectionToSite_Click);
						addSectionItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(addSectionItem);

						break;

					case "Section":
						ToolStripButton editSection = new ToolStripButton("Edit Section");
						editSection.Click += new EventHandler(EditSection_Click);
						editSection.Tag = nodeData;
						contextMenuStrip1.Items.Add(editSection);

						
						ToolStripButton addCourseToSection = new ToolStripButton("Add Course to Section");
						addCourseToSection.Click += new EventHandler(AddCourseToSection_Click);
						addCourseToSection.Tag = nodeData;
						contextMenuStrip1.Items.Add(addCourseToSection);
						

						ToolStripButton deleteSection = new ToolStripButton("Delete Section");
						deleteSection.Click += new EventHandler(DeleteSection_Click);
						deleteSection.Tag = nodeData;
						contextMenuStrip1.Items.Add(deleteSection);

						break;


					case "Course":
						ToolStripButton editCourse = new ToolStripButton("Edit Course");
						editCourse.Click += new EventHandler(EditCourse_Click);
						editCourse.Tag = nodeData;
						contextMenuStrip1.Items.Add(editCourse);

						ToolStripButton addGroupItem = new ToolStripButton("Add Group to Course");
						addGroupItem.Click += new EventHandler(AddGroupToCourse_Click);
						addGroupItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(addGroupItem);

						ToolStripButton deleteCourse = new ToolStripButton("Delete Course");
						deleteCourse.Click += new EventHandler(DeleteCourse_Click);
						deleteCourse.Tag = nodeData;
						contextMenuStrip1.Items.Add(deleteCourse);

						break;

					case "Group":
						ToolStripButton editGroupItem = new ToolStripButton("Edit Group");
						editGroupItem.Click += new EventHandler(EditGroup_Click);
						editGroupItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(editGroupItem);


						ToolStripButton uploadToGroupItem = new ToolStripButton("Upload Files to Group");
						uploadToGroupItem.Click += new EventHandler(UploadToGroup_Click);
						uploadToGroupItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(uploadToGroupItem);

						ToolStripButton deleteGroupItem = new ToolStripButton("Delete Group");
						deleteGroupItem.Click += new EventHandler(DeleteGroup_Click);
						deleteGroupItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(deleteGroupItem);

						break;

					case "Track":
						ToolStripButton editTrackItem = new ToolStripButton("Edit Track");
						editTrackItem.Click += new EventHandler(EditTrack_Click);
						editTrackItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(editTrackItem);

						ToolStripButton deleteTrackItem = new ToolStripButton("Delete Track");
						deleteTrackItem.Click += new EventHandler(DeleteTrack_Click);
						deleteTrackItem.Tag = nodeData;
						contextMenuStrip1.Items.Add(deleteTrackItem);

						break;

				}



				contextMenuStrip1.Show(this, formPoint);
			}
		}

		#region Section

		void AddSectionToSite_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			EditSection editSection = new EditSection();
			editSection.SectionName = "";

			if (editSection.ShowDialog(this) == DialogResult.OK)
			{
				Section section = new Section();
				section.Name = editSection.SectionName;

				string newHandle = _iTunes.AddSection(nodeData.Handle, section);

				// create new tree node
				TreeNode sectionNode = new TreeNode();
				sectionNode.Text = section.Name;
				sectionNode.Tag = new NodeData("Section", newHandle, sectionNode);

				nodeData.TreeNode.Nodes.Add(sectionNode);
			}

			editSection.Dispose();
		}

		void EditSection_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			Section section = _iTunes.GetSection(nodeData.Handle);

			EditSection editSection = new EditSection();
			editSection.SectionName= section.Name;


			if (editSection.ShowDialog(this) == DialogResult.OK)
			{
				section.Name = editSection.SectionName;
			
				_iTunes.MergeSection(nodeData.Handle, section);

				nodeData.TreeNode.Text = section.Name;
			}

			editSection.Dispose();
		}

		void DeleteSection_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			_iTunes.DeleteSection(nodeData.Handle);

			nodeData.TreeNode.Remove();
		}
		#endregion


		#region Course

		void AddCourseToSection_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			EditCourse editCourse = new EditCourse();
			editCourse.CourseName = "";
			editCourse.ShortName = "";
			editCourse.Identifier = "";
			editCourse.Instructor = "";
			editCourse.Description = "";

			if (editCourse.ShowDialog(this) == DialogResult.OK)
			{
				Course course = new Course();
				course.Name = editCourse.CourseName;
				course.ShortName = editCourse.ShortName;
				course.Identifier = editCourse.Identifier;
				course.Instructor = editCourse.Instructor;
				course.Description = editCourse.Description;

				string newHandle = _iTunes.AddCourse(nodeData.Handle, course);

				// create new tree node
				TreeNode courseNode = new TreeNode();
				courseNode.Text = course.Name;
				courseNode.Tag = new NodeData("Course", newHandle, courseNode);

				nodeData.TreeNode.Nodes.Add(courseNode);
				nodeData.TreeNode.Expand();
			}

			editCourse.Dispose();
		}

		void EditCourse_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			Course course = _iTunes.GetCourse(nodeData.Handle);

			EditCourse editCourse = new EditCourse();
			editCourse.CourseName = course.Name;
			editCourse.ShortName = course.ShortName;
			editCourse.Identifier = course.Identifier;
			editCourse.Instructor = course.Instructor;
			editCourse.Description = course.Description;


			if (editCourse.ShowDialog(this) == DialogResult.OK)
			{
				course.Name = editCourse.CourseName;
				course.ShortName = editCourse.ShortName;
				course.Identifier = editCourse.Identifier;
				course.Instructor = editCourse.Instructor;
				course.Description = editCourse.Description;

				_iTunes.MergeCourse(nodeData.Handle, course);

				nodeData.TreeNode.Text = course.Name;
				nodeData.TreeNode.Expand();
			}

			editCourse.Dispose();
		}

		void DeleteCourse_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			_iTunes.DeleteCourse(nodeData.Handle);

			nodeData.TreeNode.Remove();
		}
		#endregion

		#region Group Events
		void AddGroupToCourse_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			EditGroup editGroup = new EditGroup();
			editGroup.GroupType = "Simple";
			editGroup.GroupName = "";
			
			if (editGroup.ShowDialog(this) == DialogResult.OK)
			{
				Group group = new Group();
				group.Name = editGroup.GroupName;
				group.GroupType = editGroup.GroupType;

				string newHandle = _iTunes.AddGroup(nodeData.Handle, group);

				// create new tree node
				TreeNode groupNode = new TreeNode();
				groupNode.Text = group.Name;
				groupNode.Tag = new NodeData("Group", newHandle, groupNode);

				nodeData.TreeNode.Nodes.Add(groupNode);
				nodeData.TreeNode.Expand();
			}

			editGroup.Dispose();
		}

		void EditGroup_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			Group group = _iTunes.GetGroup(nodeData.Handle);

			EditGroup editGroup = new EditGroup();
			editGroup.GroupName = group.Name;
			editGroup.GroupType = group.GroupType;
		

			if (editGroup.ShowDialog(this) == DialogResult.OK)
			{
				group.Name = editGroup.GroupName;
				group.GroupType = editGroup.GroupType;
				
				_iTunes.MergeGroup(nodeData.Handle, group);

				nodeData.TreeNode.Text = group.Name;
			}

			editGroup.Dispose();
		}

		void DeleteGroup_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			_iTunes.DeleteGroup(nodeData.Handle);

			nodeData.TreeNode.Remove();
		}

		void UploadToGroup_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;
			

			// reset values
			UploadedProgressBar.Minimum = 0;
			UploadedProgressBar.Value = 0;
			NumberOfFiles.Text = "";
			BytesUploaded.Text = "";

			SelectFilesToUploadDialog.Filter = "iTunes U Files (mp3, mp4, pdf) |*.mp3;*.mp4;*.pdf";
			SelectFilesToUploadDialog.Multiselect = true;

			if (SelectFilesToUploadDialog.ShowDialog() == DialogResult.OK)
			{
				if (uploadingBackgroundWorker.IsBusy)
				{
					MessageBox.Show("Worker is still uploading.");
				}
				else
				{
					UploadRequest uRequest = new UploadRequest();
					uRequest.SiteDomain = SiteDomain.Text;
					uRequest.SharedSecret = SharedSecret.Text;
					uRequest.Handle = nodeData.Handle;
					uRequest.Files = SelectFilesToUploadDialog.FileNames;

					TreeNode groupNode = nodeData.TreeNode; // iTunesTree.SelectedNode;
					TreeNode courseNode = groupNode.Parent;

					uRequest.ParentTreeNode = groupNode;

					uRequest.AttemptRename = (courseNode.Text.Trim() == "DTS Chapel" || courseNode.Text.Trim() == "DTS Devotional");
					uploadingBackgroundWorker.RunWorkerAsync(uRequest);
				}
			}
		}
		#endregion

		#region Track Events
		void EditTrack_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;

			Track track = _iTunes.GetTrack(nodeData.Handle);

			EditTrack editTrack = new EditTrack();
			editTrack.TrackName = track.Name;
			editTrack.Artist = track.ArtistName;
			editTrack.Kind = track.Kind;
			editTrack.Album = track.AlbumName;
			editTrack.DiscNumber = track.DiscNumber;
			editTrack.TrackNumber = track.TrackNumber;

			if (editTrack.ShowDialog(this) == DialogResult.OK)
			{
				track.Name = editTrack.TrackName;
				track.ArtistName = editTrack.Artist;
				track.Kind = editTrack.Kind;
				track.AlbumName = editTrack.Album;
				track.DiscNumber = editTrack.DiscNumber;
				track.TrackNumber = editTrack.TrackNumber;

				_iTunes.MergeTrack(nodeData.Handle, track);

				nodeData.TreeNode.Text = track.Name;
			}

			editTrack.Dispose();
		}

		void DeleteTrack_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			NodeData nodeData = item.Tag as NodeData;			

			_iTunes.DeleteTrack(nodeData.Handle);

			nodeData.TreeNode.Remove();
		}

#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			if (_iTunes == null) {
				_iTunes = new iTunesU();
				_iTunes.SiteDomain = SiteDomain.Text;
				_iTunes.SharedSecret = SharedSecret.Text;
			}

			XmlDocument siteXml = _iTunes.GetSiteXmlDocument(true);

			if (siteXml != null)
				CreateTreeFromXml(siteXml);
			else
				MessageBox.Show("There was an error getting your document tree. Check your domain name and shared secret and try again.");
		}


		private void CreateTreeFromXml(XmlDocument siteXml)
		{
			iTunesTree.Nodes.Clear();

			// create site root
			XmlNode siteXmlNode = siteXml.SelectSingleNode("/ITunesUResponse/Site");
			TreeNode siteTreeNode = new TreeNode();
			siteTreeNode.Text = siteXmlNode.SelectSingleNode("Name").InnerText;
			siteTreeNode.Tag = new NodeData("Site", siteXmlNode.SelectSingleNode("Handle").InnerText, siteTreeNode);

			iTunesTree.Nodes.Add(siteTreeNode);			

			// get sections
			XmlNodeList sectionXmlNodes = siteXml.SelectNodes("/ITunesUResponse/Site/Section");


			foreach (XmlNode sectionXmlNode in sectionXmlNodes)
			{
				TreeNode sectionTreeNode = new TreeNode();
				sectionTreeNode.Text = sectionXmlNode.SelectSingleNode("Name").InnerText;
				sectionTreeNode.Tag = new NodeData("Section", sectionXmlNode.SelectSingleNode("Handle").InnerText, sectionTreeNode);

				siteTreeNode.Nodes.Add(sectionTreeNode);

				// get courses of section
				XmlNodeList courseXmlNodes = sectionXmlNode.SelectNodes("Course");

				foreach (XmlNode courseXmlNode in courseXmlNodes)
				{
					TreeNode courseTreeNode = new TreeNode();

					// for empty group/courses
					if (courseXmlNode.SelectSingleNode("Name") == null)
						continue;

					courseTreeNode.Text = courseXmlNode.SelectSingleNode("Name").InnerText;
					courseTreeNode.Tag = new NodeData("Course", courseXmlNode.SelectSingleNode("Handle").InnerText, courseTreeNode);

					sectionTreeNode.Nodes.Add(courseTreeNode);

					// get groups in couress
					XmlNodeList groupXmlNodes = courseXmlNode.SelectNodes("Group");

					foreach (XmlNode groupXmlNode in groupXmlNodes)
					{
						TreeNode groupTreeNode = new TreeNode();
						groupTreeNode.Text = groupXmlNode.SelectSingleNode("Name").InnerText;
						groupTreeNode.Tag = new NodeData("Group", groupXmlNode.SelectSingleNode("Handle").InnerText, groupTreeNode);

						courseTreeNode.Nodes.Add(groupTreeNode);


						string groupType = groupXmlNode.SelectSingleNode("GroupType").InnerText;

						if (groupType == "Simple")
						{
							// get groups in couress
							XmlNodeList trackXmlNodes = groupXmlNode.SelectNodes("Track");


							// get files in groups
							foreach (XmlNode trackXmlNode in trackXmlNodes)
							{
								if (trackXmlNode.HasChildNodes)
								{
									TreeNode trackTreeNode = new TreeNode();
									trackTreeNode.Tag = new NodeData("Track", trackXmlNode.SelectSingleNode("Handle").InnerText, trackTreeNode);
									trackTreeNode.Text = trackXmlNode.SelectSingleNode("Name").InnerText;

									groupTreeNode.Nodes.Add(trackTreeNode);
								}

							}
						}
						else
						{
							groupTreeNode.Text += " (" + groupType + ")";
						}
					}


				}

			}

			siteTreeNode.Expand();
		}


		private void uploadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			UploadRequest uRequest = e.Argument as UploadRequest;
			
			iTunesU iTunesU = new iTunesU();
			iTunesU.SiteDomain = uRequest.SiteDomain;
			iTunesU.SharedSecret = uRequest.SharedSecret;

			string handle = uRequest.Handle;
			string[] filesToUpload = uRequest.Files;

			BackgroundWorker worker = sender as BackgroundWorker;
			worker.WorkerReportsProgress = true;

			List<String> newHandleList = new List<string>();


			// setup event for the entire process

			ProgressData pData = new ProgressData();
			iTunesU.OnReceiveData += new iTunesU.OnReceiveDataHandler(delegate(object o, iTunesU.OnReceiveDataEventArgs itunesE)
			{

				int percentUploaded = 0;
				if (itunesE.TotalBytes > 0)
					percentUploaded = Convert.ToInt32(100 * Convert.ToDouble(itunesE.CurrentByteCount) / Convert.ToDouble(itunesE.TotalBytes));

				pData.TotalBytes = itunesE.TotalBytes;
				pData.BytesUploaded = itunesE.CurrentByteCount;
				pData.Error = itunesE.Error;
				pData.FileCompleted = false;

				worker.ReportProgress(percentUploaded, pData);
			});


			for (int i=0; i<filesToUpload.Length; i++) 
			{
				try
				{
					string fileToUpload = filesToUpload[i];


					pData.TotalFiles = filesToUpload.Length;
					pData.FileNumber = i + 1;
					pData.Filename = System.IO.Path.GetFileName(fileToUpload);

					// go through each file and upload
					string fullHandle = iTunesU.UploadFile(handle, fileToUpload);

					// parse off the end handle
					string shortHandle = fullHandle.Split(new char[] { '.' })[4];
					string filename = System.IO.Path.GetFileNameWithoutExtension(fileToUpload);

					// get the new track's info
					Track track = iTunesU.GetTrack(shortHandle, true);

					// DTS Specific code for adding on the date (chapel)
					if (filename.Length >= 6 && uRequest.AttemptRename)
					{
						string datePart = filename.Substring(0, 4) + "-" + filename.Substring(4, 2) + "-" + filename.Substring(6, 2);

						if (track.Name != "")
						{
							track.Name = datePart + " " + track.Name;
							iTunesU.MergeTrack(shortHandle, track);
						}
					}

					// DTS Specific code for adding on the date (devotional)
					if (filename.Length == 5 && uRequest.AttemptRename)
					{
						string datePart = filename;

						if (track.Name != "")
						{
							track.Name = datePart + " - " + track.Name;
							iTunesU.MergeTrack(shortHandle, track);
						}
					}

					// send finished 
					pData.FileCompleted = true;
					pData.ParentTreeNode = uRequest.ParentTreeNode;
					pData.NewTrackName = track.Name;
					pData.NewFileHandle = fullHandle;
					worker.ReportProgress(100, pData);

					newHandleList.Add(shortHandle);
					

				}
				catch {
					ProgressData errorData = new ProgressData();
					errorData.Error = true;
					errorData.TotalFiles = filesToUpload.Length;
					errorData.FileNumber = i + 1;

					worker.ReportProgress(0, errorData);					
				}
				
			}

			e.Result = String.Join("; ", newHandleList.ToArray());

		}
		private void uploadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
			else if (e.Cancelled)
			{

			}
			else
			{
				//MessageBox.Show("new handle: " + e.Result);
			}

		}
		private void uploadingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressData pData = e.UserState as ProgressData;

			if (pData.Error)
			{
				NumberOfFiles.Text = "There was an error";
			}
			else if (pData.FileCompleted)
			{
				NumberOfFiles.Text = "File completed";
				BytesUploaded.Text = "";
				UploadedProgressBar.Value = 0;

				WriteLog(pData.Filename + "\t" + pData.NewFileHandle);

				// create a new one
				TreeNode trackNode = new TreeNode();
				trackNode.Text = pData.NewTrackName + ": " + pData.NewFileHandle;
				trackNode.Tag = new NodeData("Track", pData.NewFileHandle, trackNode);

				pData.ParentTreeNode.Nodes.Add(trackNode);
				pData.ParentTreeNode.Expand();
			}
			else
			{

				UploadedProgressBar.Maximum = Convert.ToInt32(pData.TotalBytes / 1024);
				UploadedProgressBar.Value = Convert.ToInt32(pData.BytesUploaded / 1024);

				NumberOfFiles.Text = "Uploading File: " + pData.FileNumber.ToString() + " of " + pData.TotalFiles.ToString() + " : " + pData.Filename;
				BytesUploaded.Text = "Uploaded: " + pData.BytesUploaded.ToString() + "/" + pData.TotalBytes.ToString() + " KB";
			} 
		}


		void iTunesU_OnReceiveData(object sender, iTunesU.OnReceiveDataEventArgs e)
		{
			uploadingBackgroundWorker.CancelAsync();
		}

		private void GetTrackButton_Click(object sender, EventArgs e)
		{
			
			
			/*
			iTunesU iTunesU = new iTunesU();
			iTunesU.SiteDomain = SiteDomain.Text;
			iTunesU.SharedSecret = SharedSecret.Text;

			Track track = iTunesU.GetTrack(SelectedHandle.Text);

			MessageBox.Show(track.Name);
			*/
		}

		private void WriteLog(string text)
		{
			string logPath = System.IO.Path.Combine(Application.StartupPath,"log.txt");
			System.IO.File.AppendAllText(logPath, text + Environment.NewLine);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// check if busy and confirm close
			if (uploadingBackgroundWorker != null && uploadingBackgroundWorker.IsBusy)
			{
				if (MessageBox.Show("Are you sure you want to exit","Confirm exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
					Application.Exit();		
			}
			else
			{
				Application.Exit();
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			if (about.ShowDialog(this) == DialogResult.Cancel)
				about.Close();
		}
	}

	public class NodeData
	{
		public NodeData(string nodeType, string handle, TreeNode treeNode)
		{
			Handle = handle;
			NodeType = nodeType;
			TreeNode = treeNode;
		}

		public string Handle = "";
		public string NodeType = "";
		public TreeNode TreeNode = null;
	}

	public class UploadRequest
	{
		public string SharedSecret = "";
		public string SiteDomain = "";
		public string Handle = "";
		public TreeNode ParentTreeNode = null;
		public string[] Files;
		public bool AttemptRename = false;
	}

	public class ProgressData
	{
		public int TotalFiles = 0;
		public int FileNumber = 0;
		public string Filename = "";
		public bool Error = false;
		public long TotalBytes = 0;
		public long BytesUploaded = 0;
		public bool FileCompleted = false;
		public string NewTrackName = "";
		public string NewFileHandle = "";
		public TreeNode ParentTreeNode = null;
	}
}
