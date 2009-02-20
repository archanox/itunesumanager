using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iTunesUManager
{
	public partial class EditSection : Form
	{
		public EditSection()
		{
			InitializeComponent();
		}

		public string SectionName
		{
			get { return NameTb.Text; }
			set { NameTb.Text = value; }
		}
	}
}
