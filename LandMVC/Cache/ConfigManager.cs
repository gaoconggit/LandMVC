using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace LandMVC
{
	internal static class ConfigManager
	{
		private static readonly string CacheKey = Guid.NewGuid().ToString();

		private static Exception s_loadConfigException;
		private static Dictionary<string, OutputCacheSetting> s_settings;

		public static Dictionary<string, OutputCacheSetting> Settings
		{
			get
			{
				Exception exceptin = s_loadConfigException;
				if( exceptin != null )
					throw exceptin;

				return s_settings;
			}
		}

		public static void LoadConfig(string xmlFilePath)
		{
			Dictionary<string, OutputCacheSetting> dict = null;

			try {
				OutputCacheConfig config = XmlHelper.XmlDeserializeFromFile<OutputCacheConfig>(xmlFilePath, Encoding.UTF8);
				dict = config.Settings.ToDictionary(x => x.FilePath, StringComparer.OrdinalIgnoreCase);
			}
			catch( Exception ex ) {
				s_loadConfigException = new System.Configuration.ConfigurationErrorsException(
					"初始化SetOutputCacheModule时发生异常，请检查" + xmlFilePath + "文件是否配置正确。", ex);
			}


			if( dict != null ) {
				// 注册缓存移除通知，以便在用户修改了配置文件后自动重新加载。

				// 参考：细说 ASP.NET Cache 及其高级用法
				//	      http://www.cnblogs.com/fish-li/archive/2011/12/27/2304063.html
				CacheDependency dep = new CacheDependency(xmlFilePath);
				HttpRuntime.Cache.Insert(CacheKey, xmlFilePath, dep,
					Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, CacheRemovedCallback);
			}

			s_settings = dict;
		}


		private static void CacheRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			string xmlFilePath = (string)value;

			// 由于事件发生时，文件可能还没有完全关闭，所以只好让程序稍等。
			System.Threading.Thread.Sleep(3000);

			// 重新加载配置文件
			LoadConfig(xmlFilePath);
		}
	}
}
