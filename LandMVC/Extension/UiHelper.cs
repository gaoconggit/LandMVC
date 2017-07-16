using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace LandMVC
{
	public static class UiHelper
	{
		// 网站根目录。
		// 注意：如果这段代码没有运行在ASP.NET环境中，会出现异常！
		private static readonly string s_root = HttpRuntime.AppDomainAppPath.TrimEnd('\\');


		/// <summary>
		/// 生成一个引用JS文件的HTML代码，其中URL包含了文件的最后更新时间。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string RefJsFileHtml(string path)
		{
			string filePath = s_root + path.Replace("/", "\\");
			string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
			return string.Format("<script type=\"text/javascript\" src=\"{0}?_t={1}\"></script>", path, version);
		}

		/// <summary>
		/// 生成一个引用CSS文件的HTML代码，其中URL包含了文件的最后更新时间。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string RefCssFileHtml(string path)
		{
			string filePath = s_root + path.Replace("/", "\\");
			string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
			return string.Format("<link type=\"text/css\" rel=\"Stylesheet\" href=\"{0}?_t={1}\" />", path, version);
		}
	}
}
