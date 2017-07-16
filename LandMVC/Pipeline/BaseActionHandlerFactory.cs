using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	public abstract class BaseActionHandlerFactory : IHttpHandlerFactory
	{
		public abstract ControllerActionPair ParseUrl(HttpContext context, string path);

		public abstract bool TypeIsService(Type type);

		public IHttpHandler GetHandler(HttpContext context,
							string requestType, string virtualPath, string physicalPath)
		{
			// 说明：这里不使用virtualPath变量，因为不同的配置，这个变量的值会不一样。
			// 例如：/Ajax/*/*.aspx 和 /Ajax/*
			// 为了映射HTTP处理器，下面直接使用context.Request.Path

			string vPath = UrlHelper.GetRealVirtualPath(context, context.Request.Path);

			// 根据请求路径，定位到要执行的Action
			ControllerActionPair pair = ParseUrl(context, vPath);
			if( pair == null )
				ExceptionHelper.Throw404Exception(context);

			// 获取内部表示的调用信息
			InvokeInfo vkInfo = ReflectionHelper.GetActionInvokeInfo(pair, context.Request);
			if( vkInfo == null )
				ExceptionHelper.Throw404Exception(context);

			// 创建能够调用Action的HttpHandler
			return ActionHandler.CreateHandler(vkInfo);
		}

		public void ReleaseHandler(IHttpHandler handler)
		{
		}

	}
}
