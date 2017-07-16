using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

namespace LandMVC
{
	/// <summary>
	/// 用于访问当前请求上下文的工具类。这个类对测试环境仍然有效。
	/// </summary>
	public static class HttpContextHelper
	{
		// 为了能让代码支持测试环境
		// 会判断 TestEnvironment.IsTestEnvironment
		// 如果在ASP.NET环境中运行，则直接返回HttpRuntime或者HttpContext.Current中的参数值
		// 如果是在测试环境中运行，则使用另一个静态变量来维护所有需要访问的状态。
		
		// 在测试前，需要先给相应的参数赋值。
		// 测试完成后可调用TestEnvironment.ClearTestEnvironmentInfo()清除临时环境。


		/// <summary>
		/// return HttpRuntime.AppDomainAppPath;
		/// </summary>
		public static string AppRootPath
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment )
					return TestEnvironment.GetValue("AppDomainAppPath") as string;
				return HttpRuntime.AppDomainAppPath;
			}
			set
			{
				TestEnvironment.SetValue("AppDomainAppPath", value);
			}
		}


		/// <summary>
		/// return HttpContext.Current.Request.FilePath;
		/// </summary>
		public static string RequestFilePath
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment )
					return TestEnvironment.GetValue("RequestFilePath") as string;
				return HttpContext.Current.Request.FilePath;
			}
			set
			{
				TestEnvironment.SetValue("RequestFilePath", value);
			}
		}


		/// <summary>
		/// return HttpContext.Current.Request.RawUrl;
		/// </summary>
		public static string RequestRawUrl
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment )
					return TestEnvironment.GetValue("RequestRawUrl") as string;
				return HttpContext.Current.Request.RawUrl;
			}
			set
			{
				TestEnvironment.SetValue("RequestRawUrl", value);
			}
		}


		/// <summary>
		/// return HttpContext.Current.User.Identity.Name;
		/// </summary>
		public static string UserIdentityName
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment )
					return TestEnvironment.GetValue("UserIdentityName") as string;

				if( HttpContext.Current.Request.IsAuthenticated == false )
					return null;

				return HttpContext.Current.User.Identity.Name;
			}
			set
			{
				TestEnvironment.SetValue("UserIdentityName", value);
			}
		}



		// 如果还需要访问更多的HttpContext信息，也可以采用下面的方法。请自行完成。

		//public static HttpContextBase Current
		//{
		//    get { }
		//    set { }
		//}
	}
}
