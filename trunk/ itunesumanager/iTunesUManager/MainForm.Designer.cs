
namespace iTunesUManager
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.iTunesTree = new System.Windows.Forms.TreeView();
			this.SharedSecret = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.LoadSiteButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SiteDomain = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CancelUploadButton = new System.Windows.Forms.Button();
			this.BytesUploaded = new System.Windows.Forms.Label();
			this.NumberOfFiles = new System.Windows.Forms.Label();
			this.UploadedProgressBar = new System.Windows.Forms.ProgressBar();
			this.SelectFilesToUploadDialog = new System.Windows.Forms.OpenFileDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ehlpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// iTunesTree
			// 
			this.iTunesTree.Location = new System.Drawing.Point(10, 19);
			this.iTunesTree.Name = "iTunesTree";
			this.iTunesTree.Size = new System.Drawing.Size(524, 293);
			this.iTunesTree.TabIndex = 0;
			// 
			// SharedSecret
			// 
			this.SharedSecret.Location = new System.Drawing.Point(87, 19);
			this.SharedSecret.Name = "SharedSecret";
			this.SharedSecret.Size = new System.Drawing.Size(266, 20);
			this.SharedSecret.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.LoadSiteButton);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.SiteDomain);
			this.groupBox1.Controls.Add(this.SharedSecret);
			this.groupBox1.Location = new System.Drawing.Point(12, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(540, 80);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setup";
			// 
			// LoadSiteButton
			// 
			this.LoadSiteButton.Location = new System.Drawing.Point(367, 20);
			this.LoadSiteButton.Name = "LoadSiteButton";
			this.LoadSiteButton.Size = new System.Drawing.Size(156, 23);
			this.LoadSiteButton.TabIndex = 5;
			this.LoadSiteButton.Text = "Load Site Tree";
			this.LoadSiteButton.UseVisualStyleBackColor = true;
			this.LoadSiteButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Domain";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Shared Secret";
			// 
			// SiteDomain
			// 
			this.SiteDomain.Location = new System.Drawing.Point(87, 45);
			this.SiteDomain.Name = "SiteDomain";
			this.SiteDomain.Size = new System.Drawing.Size(266, 20);
			this.SiteDomain.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CancelUploadButton);
			this.groupBox2.Controls.Add(this.BytesUploaded);
			this.groupBox2.Controls.Add(this.NumberOfFiles);
			this.groupBox2.Controls.Add(this.UploadedProgressBar);
			this.groupBox2.Location = new System.Drawing.Point(12, 456);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(540, 107);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Uploading Status";
			// 
			// CancelUploadButton
			// 
			this.CancelUploadButton.Location = new System.Drawing.Point(10, 69);
			this.CancelUploadButton.Name = "CancelUploadButton";
			this.CancelUploadButton.Size = new System.Drawing.Size(114, 23);
			this.CancelUploadButton.TabIndex = 8;
			this.CancelUploadButton.Text = "Cancel Upload";
			this.CancelUploadButton.UseVisualStyleBackColor = true;
			this.CancelUploadButton.Click += new System.EventHandler(this.GetTrackButton_Click);
			// 
			// BytesUploaded
			// 
			this.BytesUploaded.AutoSize = true;
			this.BytesUploaded.Location = new System.Drawing.Point(352, 69);
			this.BytesUploaded.Name = "BytesUploaded";
			this.BytesUploaded.Size = new System.Drawing.Size(85, 13);
			this.BytesUploaded.TabIndex = 9;
			this.BytesUploaded.Text = "Bytes Uploaded:";
			// 
			// NumberOfFiles
			// 
			this.NumberOfFiles.AutoSize = true;
			this.NumberOfFiles.Location = new System.Drawing.Point(7, 24);
			this.NumberOfFiles.Name = "NumberOfFiles";
			this.NumberOfFiles.Size = new System.Drawing.Size(54, 13);
			this.NumberOfFiles.TabIndex = 8;
			this.NumberOfFiles.Text = "Files : 0/0";
			// 
			// UploadedProgressBar
			// 
			this.UploadedProgressBar.Location = new System.Drawing.Point(10, 40);
			this.UploadedProgressBar.Name = "UploadedProgressBar";
			this.UploadedProgressBar.Size = new System.Drawing.Size(524, 23);
			this.UploadedProgressBar.TabIndex = 1;
			// 
			// SelectFilesToUploadDialog
			// 
			this.SelectFilesToUploadDialog.FileName = "openFileDialog1";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.iTunesTree);
			this.groupBox3.Location = new System.Drawing.Point(12, 132);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(540, 318);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Site Tree";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.ehlpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(564, 24);
			this.menuStrip1.TabIndex = 9;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// ehlpToolStripMenuItem
			// 
			this.ehlpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.ehlpToolStripMenuItem.Name = "ehlpToolStripMenuItem";
			this.ehlpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.ehlpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 577);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "iTunesU Manager";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		#endregion

		private System.Windows.Forms.TreeView iTunesTree;
		private System.Windows.Forms.TextBox SharedSecret;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox SiteDomain;
		private System.Windows.Forms.Button LoadSiteButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label BytesUploaded;
		private System.Windows.Forms.Label NumberOfFiles;
		private System.Windows.Forms.ProgressBar UploadedProgressBar;
		private System.Windows.Forms.OpenFileDialog SelectFilesToUploadDialog;
		private System.Windows.Forms.Button CancelUploadButton;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ehlpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
	}
}

