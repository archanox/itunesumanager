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
	    public string Name { get; set; } = string.Empty;

	    public string Handle { get; set; } = string.Empty;

	    public string Kind { get; set; } = string.Empty;

	    public string DiscNumber { get; set; } = string.Empty;

	    public string TrackNumber { get; set; } = string.Empty;

	    public string DurationMilliseconds { get; set; } = string.Empty;

	    public string AlbumName { get; set; } = string.Empty;

	    public string ArtistName { get; set; } = string.Empty;

	    public string DownloadURL { get; set; } = string.Empty;
	}
}
