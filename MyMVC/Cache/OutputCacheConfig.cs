using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LandMVC
{
	[XmlRoot("OutputCache")]
	public class OutputCacheConfig
	{
		[XmlArrayItem("Setting")]
		public List<OutputCacheSetting> Settings = new List<OutputCacheSetting>();
	}


	public class OutputCacheSetting : OutputCacheAttribute
	{
		[XmlAttribute]
		public string FilePath { get; set; }
	}

}
