using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	internal static class UrlHelper
	{
		/// <summary>
		/// 获取实际的虚拟路径，如果网站部署在虚拟目录中，将去除虚拟目录的顶层目录名。
		/// </summary>
		/// <param name="context">HttpContext实例的引用</param>
		/// <param name="virtualPath">可能包含虚拟目录的虚拟路径</param>
		/// <returns>去除虚拟目录后的实际虚拟路径。</returns>
		public static string GetRealVirtualPath(HttpContext context, string virtualPath)
		{
			// 解决虚拟目录问题
			if( context.Request.ApplicationPath != "/" )
				if( virtualPath.StartsWith(context.Request.ApplicationPath + "/") )
					return virtualPath.Substring(context.Request.ApplicationPath.Length);


			return virtualPath;
		}

	}
}
