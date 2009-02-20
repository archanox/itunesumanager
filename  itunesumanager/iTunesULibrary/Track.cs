using System;
using System.Collections.Generic;
using System.Text;

namespace edu.dts.iTunesULibrary
{

	/// <summary>
	/// iTunes U Track object
	/// </summary>
	public class Track
	{
		private string _name = string.Empty;
		private string _handle = string.Empty;
		private string _kind = string.Empty;
		private string _discNumber = string.Empty;
		private string _trackNumber = string.Empty;
		private string _durationMilliseconds = string.Empty;
		private string _albumName = string.Empty;
		private string _artistName = string.Empty;
		private string _downloadURL = string.Empty;
	

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string Handle {
			get { return _handle; }
			set { _handle = value; }
		}

		public string Kind {
			get { return _kind; }
			set { _kind = value; }
		}

		public string DiscNumber {
			get { return _discNumber; }
			set { _discNumber = value; }
		}

		public string TrackNumber
		{
			get { return _trackNumber; }
			set { _trackNumber = value; }
		}

		public string DurationMilliseconds {
			get { return _durationMilliseconds; }
			set { _durationMilliseconds = value; }
		}

		public string AlbumName {
			get { return _albumName; }
			set { _albumName = value; }
		}

		public string ArtistName {
			get { return _artistName; }
			set { _artistName = value; }
		}

		public string DownloadURL {
			get { return _downloadURL; }
			set { _downloadURL = value; }
		}
	}
}
