namespace iTunesUManager
{
	partial class EditSection
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
			this.label1 = new System.Windows.Forms.Label();
			this.NameTb = new System.Windows.Forms.TextBox();
			this.CancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SaveChangesButton
			// 
			this.SaveChangesButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.SaveChangesButton.Location = new System.Drawing.Point(71, 47);
			this.SaveChangesButton.Name = "SaveChangesButton";
			this.SaveChangesButton.Size = new System.Drawing.Size(118, 24);
			this.SaveChangesButton.TabIndex = 27;
			this.SaveChangesButton.Text = "Save Changes";
			this.SaveChangesButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 24;
			this.label1.Text = "Name";
			// 
			// NameTb
			// 
			this.NameTb.Location = new System.Drawing.Point(110, 12);
			this.NameTb.Name = "NameTb";
			this.NameTb.Size = new System.Drawing.Size(273, 20);
			this.NameTb.TabIndex = 23;
			// 
			// CancelButton
			// 
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(195, 47);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(116, 24);
			this.CancelButton.TabIndex = 22;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// EditSection
			// 
			this.AcceptButton = this.SaveChangesButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelButton;
			this.ClientSize = new System.Drawing.Size(401, 87);
			this.Controls.Add(this.SaveChangesButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.NameTb);
			this.Controls.Add(this.CancelButton);
			this.Name = "EditSection";
			this.Text = "Edit Section";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SaveChangesButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox NameTb;
		private System.Windows.Forms.Button CancelButton;
	}
}