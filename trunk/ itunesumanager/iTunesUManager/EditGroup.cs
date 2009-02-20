using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iTunesUManager
{
	public partial class EditGroup : Form
	{
		public EditGroup()
		{
			InitializeComponent();
		}


		public string GroupName
		{
			get { return NameTb.Text; }
			set { NameTb.Text = value; }
		}
		public string GroupType
		{
			get { return GroupTypeTb.Text; }
			set { GroupTypeTb.Text = value; }
		}
	}
}
