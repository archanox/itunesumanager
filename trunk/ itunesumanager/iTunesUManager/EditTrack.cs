using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iTunesUManager
{
	public partial class EditTrack : Form
	{
		public EditTrack()
		{
			InitializeComponent();
		}

		public string TrackName
		{
			get { return NameTb.Text;}
			set { NameTb.Text = value;}
		}
		public string Artist
		{
			get { return ArtistNameTb.Text; }
			set { ArtistNameTb.Text = value; }
		}
		public string Album
		{
			get { return AlbumNameTb.Text; }
			set { AlbumNameTb.Text = value; }
		}
		public string Kind
		{
			get { return KindTb.Text; }
			set { KindTb.Text = value; }
		}

		public string DiscNumber
		{
			get { return DiscNumberTb.Text; }
			set { DiscNumberTb.Text = value; }
		}

		public string TrackNumber
		{
			get { return TrackNumberTb.Text; }
			set { TrackNumberTb.Text = value; }
		}
	}
}
