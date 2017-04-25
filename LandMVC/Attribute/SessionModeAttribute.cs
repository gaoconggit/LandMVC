using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandMVC
{
	/// <summary>
	/// Action所支持的Session模式
	/// </summary>
	public enum SessionMode
	{
		/// <summary>
		/// 不支持
		/// </summary>
		NotSupport,
		/// <summary>
		/// 全支持
		/// </summary>
		Support,
		/// <summary>
		/// 仅支持读取
		/// </summary>
		ReadOnly
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class SessionModeAttribute : Attribute
	{
		/// <summary>
		/// 要支持的Session模式
		/// </summary>
		public SessionMode SessionMode { get; private set; }

		public SessionModeAttribute(SessionMode mode)
		{
			this.SessionMode = mode;
		}
	}
}
