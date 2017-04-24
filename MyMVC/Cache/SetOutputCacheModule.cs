using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;

namespace LandMVC
{
	public class SetOutputCacheModule : IHttpModule
	{
		public void Init(HttpApplication app)
		{
			app.PreRequestHandlerExecute += new EventHandler(app_PreRequestHandlerExecute);
		}

		void app_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;

			Dictionary<string, OutputCacheSetting> settings = ConfigManager.Settings;
			if( settings == null )
				throw new ConfigurationErrorsException("SetOutputCacheModule加载配置文件失败。");

			// 实现方法：
			// 查找配置参数，如果找到匹配的请求，就设置OutputCache
			OutputCacheSetting setting = null;
			if( settings.TryGetValue(app.Request.FilePath, out setting) ) {
				setting.SetResponseCache(app.Context);
			}
		}
		static SetOutputCacheModule()
		{
			// 加载配置文件
			string xmlFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "OutputCache.config");
			ConfigManager.LoadConfig(xmlFilePath);
		}
		public void Dispose()
		{
		}
	}
}
