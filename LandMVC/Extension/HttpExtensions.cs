using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace LandMVC
{
	internal static class HttpExtensions
	{
		public static string ReadInputStream(this HttpRequest request)
		{
			//if( request == null )
			//    throw new ArgumentNullException("request");

			request.InputStream.Position = 0;
			StreamReader sr = new StreamReader(request.InputStream, request.ContentEncoding);
			return  sr.ReadToEnd();
		}

	}
}
