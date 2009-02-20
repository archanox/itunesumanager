using System;
using System.Collections.Generic;
using System.Text;

namespace edu.dts.iTunesULibrary
{
	public class Group
	{
		private string _name = string.Empty;
		private string _handle = string.Empty;
		private string _shortName = string.Empty;
		private string _groupType = "Simple";

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string Handle {
			get { return _handle; }
			set { _handle = value; }
		}

		public string ShortName
		{
			get { return _shortName; }
			set { _shortName = value; }
		}

		public string GroupType
		{
			get { return _groupType; }
			set { _groupType = value; }
		}
	}
}
