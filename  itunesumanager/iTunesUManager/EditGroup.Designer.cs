namespace iTunesUManager
{
	partial class EditGroup
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
			this.SaveChangesButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.GroupTypeTb = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.NameTb = new System.Windows.Forms.TextBox();
			this.CancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SaveChangesButton
			// 
			this.SaveChangesButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.SaveChangesButton.Location = new System.Drawing.Point(76, 77);
			this.SaveChangesButton.Name = "SaveChangesButton";
			this.SaveChangesButton.Size = new System.Drawing.Size(118, 24);
			this.SaveChangesButton.TabIndex = 21;
			this.SaveChangesButton.Text = "Save Changes";
			this.SaveChangesButton.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Group Type";
			// 
			// GroupTypeTb
			// 
			this.GroupTypeTb.Location = new System.Drawing.Point(115, 38);
			this.GroupTypeTb.Name = "GroupTypeTb";
			this.GroupTypeTb.Size = new System.Drawing.Size(273, 20);
			this.GroupTypeTb.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Name";
			// 
			// NameTb
			// 
			this.NameTb.Location = new System.Drawing.Point(115, 12);
			this.NameTb.Name = "NameTb";
			this.NameTb.Size = new System.Drawing.Size(273, 20);
			this.NameTb.TabIndex = 13;
			// 
			// CancelButton
			// 
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(200, 77);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(116, 24);
			this.CancelButton.TabIndex = 12;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// EditGroup
			// 
			this.AcceptButton = this.SaveChangesButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelButton;
			this.ClientSize = new System.Drawing.Size(400, 118);
			this.Controls.Add(this.SaveChangesButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.GroupTypeTb);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.NameTb);
			this.Controls.Add(this.CancelButton);
			this.Name = "EditGroup";
			this.Text = "Edit Group";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SaveChangesButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox GroupTypeTb;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox NameTb;
		private System.Windows.Forms.Button CancelButton;
	}
}