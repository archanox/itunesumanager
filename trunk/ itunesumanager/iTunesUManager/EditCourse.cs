using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iTunesUManager
{
	public partial class EditCourse : Form
	{
		public EditCourse()
		{
			InitializeComponent();
		}

		public string CourseName
		{
			get { return NameTb.Text; }
			set { NameTb.Text = value; }
		}

		public string ShortName
		{
			get { return ShortNameTb.Text; }
			set { ShortNameTb.Text = value; }
		}

		public string Identifier
		{
			get { return IdentifierTb.Text; }
			set { IdentifierTb.Text = value; }
		}

		public string Instructor
		{
			get { return InstructorTb.Text; }
			set { InstructorTb.Text = value; }
		}

		public string Description
		{
			get { return DescriptionTb.Text; }
			set { DescriptionTb.Text = value; }
		}
	}
}
