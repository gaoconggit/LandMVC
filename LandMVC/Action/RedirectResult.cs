using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	/// <summary>
	/// 表示一个重定向的结果
	/// </summary>
	public sealed class RedirectResult : IActionResult
	{
		public string Url { get; private set; }

		public RedirectResult(string url)
		{
			if( string.IsNullOrEmpty(url) )
				throw new ArgumentNullException("url");
			Url = url;
		}

		void IActionResult.Ouput(HttpContext context)
		{
			context.Response.Redirect(Url, true);
		}
	}

}
