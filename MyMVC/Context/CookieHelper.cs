using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	/// <summary>
	/// 读写Cookie的辅助工具类。这个类对测试环境仍然有效。
	/// </summary>
	public static class CookieHelper
	{
		[ThreadStatic]
		private static HttpCookieCollection s_cookies;

		private static HttpCookieCollection TestCookies
		{
			get
			{
				if( TestEnvironment.IsTestEnvironment == false )
					throw new InvalidOperationException();

				if( s_cookies == null )	// 测试环境不考虑线程安全问题。
					s_cookies = new HttpCookieCollection();
				return s_cookies;
			}
		}
		
		/// <summary>
		/// 从Request中获取一个Cookie对象
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static HttpCookie GetCookie(string key)
		{
			if( TestEnvironment.IsTestEnvironment ) 
				return TestCookies[key];
			
			return HttpContext.Current.Request.Cookies[key];
		}

		/// <summary>
		/// 从Request中获取一个Cookie对象的值，如果不存在，则返回null
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetCookieValue(string key)
		{
			HttpCookie cookie = GetCookie(key);
			if( cookie == null )
				return null;
			else
				return cookie.Value;
		}

		/// <summary>
		/// 将一个Cookie对应写入到Response中
		/// </summary>
		/// <param name="cookie"></param>
		public static void SetCookie(HttpCookie cookie)
		{
			if( TestEnvironment.IsTestEnvironment ) 
				TestCookies.Set(cookie);			
			else
				HttpContext.Current.Response.Cookies.Set(cookie);
		}

		/// <summary>
		/// 将一个Cookie对应写入到Response中
		/// </summary>
		/// <param name="cookie"></param>
		public static void AddCookie(HttpCookie cookie)
		{
			if( TestEnvironment.IsTestEnvironment ) 
				TestCookies.Add(cookie);
			else
				HttpContext.Current.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// 清除所有写入的Cookie 
		/// </summary>
		public static void ClearCookie()
		{
			if( TestEnvironment.IsTestEnvironment )
				TestCookies.Clear();
			else
				HttpContext.Current.Response.Cookies.Clear();
		}

		/// <summary>
		/// 删除指定名称的Cookie
		/// </summary>
		/// <param name="key"></param>
		public static void RemoveCookie(string key)
		{
			if( TestEnvironment.IsTestEnvironment ) 
				TestCookies.Remove(key);
			else
				HttpContext.Current.Response.Cookies.Remove(key);
		}

	}
}
