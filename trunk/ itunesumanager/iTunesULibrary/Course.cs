using System;
using System.Collections.Generic;
using System.Text;

namespace edu.dts.iTunesULibrary
{
	public class Course
	{
		private string _name = string.Empty;
		private string _handle = string.Empty;
		private string _shortName = string.Empty;
		private string _instructor = string.Empty;
		private string _identifier = string.Empty;
		private string _description = string.Empty;

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

		public string ShortName
		{
			get { return _shortName; }
			set { _shortName = value; }
		}

		public string Instructor
		{
			get { return _instructor; }
			set { _instructor = value; }
		}

		public string Identifier
		{
			get { return _identifier; }
			set { _identifier = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
	}
}
