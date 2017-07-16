using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.SessionState;

namespace LandMVC
{
	internal class RequiresSessionActionHandler : ActionHandler, IRequiresSessionState
	{
	}

	internal class ReadOnlySessionActionHandler : ActionHandler, IRequiresSessionState, IReadOnlySessionState
	{
	}

	internal class ActionHandler : IHttpHandler
	{
		internal InvokeInfo InvokeInfo;

		public void ProcessRequest(HttpContext context)
		{
			// 调用核心的工具类，执行Action
			ActionExecutor.ExecuteAction(context, this.InvokeInfo);
		}

		public bool IsReusable
		{
			get { return false; }
		}


		public static ActionHandler CreateHandler(InvokeInfo vkInfo)
		{
			SessionMode mode = vkInfo.GetSessionMode();

			if( mode == SessionMode.NotSupport )
				return new ActionHandler { InvokeInfo = vkInfo };

			else if( mode == SessionMode.ReadOnly )
				return new ReadOnlySessionActionHandler { InvokeInfo = vkInfo };

			else
				return new RequiresSessionActionHandler { InvokeInfo = vkInfo };
		}
	}
}
