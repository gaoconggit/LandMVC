using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	public sealed class XmlResult : IActionResult
	{
		public object Model { get; private set; }

		public XmlResult(object model)
		{
			if( model == null )
				throw new ArgumentNullException("model");

			this.Model = model;
		}

		void IActionResult.Ouput(HttpContext context)
		{
			context.Response.ContentType = "application/xml";
			string xml = XmlHelper.XmlSerialize(Model, Encoding.UTF8);
			context.Response.Write(xml);
		}
	}
}
