using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	/// <summary>
	/// 用于UI输出方面的常用字符串扩展
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// 将字符串转换为 HTML 编码的字符串。
		/// </summary>
		/// <param name="str">要编码的字符串。</param>
		/// <returns>一个已编码的字符串。</returns>
		public static string HtmlEncode(this string str)
		{
			if( string.IsNullOrEmpty(str) )
				return string.Empty;

			return HttpUtility.HtmlEncode(str);
		}

		/// <summary>
		/// 将字符串最小限度地转换为 HTML 编码的字符串。
		/// </summary>
		/// <param name="str">要编码的字符串。</param>
		/// <returns>一个已编码的字符串。</returns>
		public static string HtmlAttributeEncode(this string str)
		{
			if( string.IsNullOrEmpty(str) )
				return string.Empty;

			return HttpUtility.HtmlAttributeEncode(str);
		}

		/// <summary>
		/// 判断二个字符串是否相等，忽略大小写的比较方式。
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool IsSame(this string a, string b)
		{
			return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
		}


		/// <summary>
		/// 等效于 string.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries)
		/// 且为每个拆分后的结果又做了Trim()操作。
		/// </summary>
		/// <param name="value">要拆分的字符串</param>
		/// <param name="separator">分隔符</param>
		/// <returns></returns>
		public static string[] SplitTrim(this string str, params char[] separator)
		{
			if( string.IsNullOrEmpty(str) )
				return null;
			else
				return (from s in str.Split(separator)
						let u = s.Trim()
						where u.Length > 0
						select u).ToArray();
		}


		internal static readonly char[] CommaSeparatorArray = new char[] { ',' };


		/// <summary>
		/// 将字符串的首个英文字母大写
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string ToTitleCase(this string text)
		{
			// 重新实现：CultureInfo.CurrentCulture.TextInfo.ToTitleCase
			// 那个方法太复杂了，重新实现一个简单的版本。

			if( text == null || text.Length < 2 )
				return text;

			char c = text[0];
			if( (c >= 'a') && (c <= 'z') )
				return ((char)(c - 32)).ToString() + text.Substring(1);
			else
				return text;
		}

	}


	
}
