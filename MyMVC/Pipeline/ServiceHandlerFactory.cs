using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;


namespace LandMVC
{
	/// <summary>
	/// 响应服务请求的HttpHandlerFactory。它要求将所有Action放在一个以Service结尾的类型中。
	/// </summary>
	public class ServiceHandlerFactory : BaseActionHandlerFactory
	{
		private static readonly Regex s_urlRegex
			= new Regex(@"/(?<type>\w{3,})/((?<namespace>[\.\w]+)\.)?(?<name>\w+)/(?<method>\w+)\.(?<extname>[a-zA-Z]+)", RegexOptions.Compiled);

		/*
			可以解析以下格式的URL：（前一个表示包含命名空间的格式）

			/service/Fish.AA.Demo/GetMd5.aspx
		    /service/Demo/GetMd5.aspx
		*/

		public override ControllerActionPair ParseUrl(HttpContext context, string path)
		{
			if( string.IsNullOrEmpty(path) )
				throw new ArgumentNullException("path");

			Match match = s_urlRegex.Match(path);
			if( match.Success == false )
				return null;

			
			string serivceType = match.Groups["type"].Value;
			string nspace = match.Groups["namespace"].Value;
			string className = match.Groups["name"].Value;
			string extname = match.Groups["extname"].Value;

			return new ControllerActionPair {
				Controller = GetControllerName(serivceType, nspace, className, extname),
				Action = match.Groups["method"].Value
			};
		}


		public virtual string GetControllerName(string serviceType, string nspace, string className, string extname)
		{
			return nspace
				+ (nspace.Length > 0 ? "." : string.Empty)
				+ className + "Service";
		}
		
		public override bool TypeIsService(Type type)
		{
			return type.Name.EndsWith("Service");
		}
	}
}
