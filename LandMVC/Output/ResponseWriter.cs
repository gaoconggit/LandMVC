using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	public static class ResponseWriter
	{
		

		private static void SetResponseContentType(HttpResponse response)
		{
			if( string.IsNullOrEmpty(response.ContentType) )
				response.ContentType = "text/html";
		}

		/// <summary>
		/// 将指定的HTML代码写入HttpContext.Current.Response
		/// </summary>
		/// <param name="html">要写入的HTML文本</param>
		/// <param name="flush">是否需要在输出html后调用Response.Flush()</param>
		public static void WriteHtml(string html, bool flush)
		{
			HttpContext context = HttpContext.Current;
			if( context == null )
				return;

			if( string.IsNullOrEmpty(html) )
				return;

			SetResponseContentType(context.Response);
			context.Response.Write(html);
			if( flush )
				context.Response.Flush();
		}

	}
}
