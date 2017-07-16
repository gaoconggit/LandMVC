using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace LandMVC
{
	/// <summary>
	/// 响应AJAX请求的HttpHandlerFactory。它要求将所有Action放在一个以Ajax开头的类型中。
	/// </summary>
	public sealed class AjaxHandlerFactory : BaseActionHandlerFactory
	{
		private static readonly Regex s_urlRegex
			= new Regex(@"/(?<name>(\w[\./\w]*)?(?=)\w+)[/\.](?<method>\w+)", RegexOptions.Compiled);

		

		public override ControllerActionPair ParseUrl(HttpContext context, string path)
		{
			if( string.IsNullOrEmpty(path) )
				throw new ArgumentNullException("path");

			Match match = s_urlRegex.Match(path);
			if( match.Success == false )
				return null;

			return new ControllerActionPair {
				Controller = match.Groups["name"].Value.Replace("/", "."),
				Action = match.Groups["method"].Value
			};
		}


		public override bool TypeIsService(Type type)
		{
			return type.Name.StartsWith("Ajax");
		}
	}

}
