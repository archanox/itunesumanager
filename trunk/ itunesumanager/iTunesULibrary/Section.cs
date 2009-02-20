using System;
using System.Collections.Generic;
using System.Text;

namespace edu.dts.iTunesULibrary
{
	public class Section
	{
		private string _name = string.Empty;
		private string _handle = string.Empty;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string Handle
		{
			get { return _handle; }
			set { _handle = value; }
		}
	}
}
