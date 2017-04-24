using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

namespace LandMVC
{
	public static class TestEnvironment
	{
		/// <summary>
		/// 当前运行环境是否为测试环境（非ASP.NET环境）
		/// </summary>
		internal static readonly bool IsTestEnvironment = (HttpRuntime.AppDomainAppId == null);


		[ThreadStatic]
		private static Hashtable s_ContextInfo;

		private static Hashtable ContextInfo
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment == false )
					throw new InvalidOperationException("只有测试代码才允许执行这个调用。");

				if( s_ContextInfo == null )	// 测试环境不考虑线程安全问题。
					s_ContextInfo = new Hashtable(100, StringComparer.Ordinal);
				return s_ContextInfo;
			}
		}

		public static object GetValue(string key)
		{
			object val = ContextInfo[key];
			if( val == null )
				throw new InvalidOperationException("您忘记了给测试环境赋值了。参数名：" + key);
			return val;
		}

		public static void SetValue(string key, object val)
		{
			ContextInfo[key] = val;
		}

		public static void ClearTestEnvironmentInfo()
		{
			ContextInfo.Clear();
			CookieHelper.ClearCookie();
		}
	}
}
