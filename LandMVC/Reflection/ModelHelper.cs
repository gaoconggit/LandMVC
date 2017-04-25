using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Configuration;
using System.Web.Configuration;
using OptimizeReflection;

namespace LandMVC
{
	internal static class ModelHelper
	{
		public static readonly bool IsDebugMode;

		static ModelHelper()
		{
			CompilationSection configSection = 
						ConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
			if( configSection != null )
				IsDebugMode = configSection.Debug;
		}

		/// <summary>
		/// 根据HttpRequest填充一个数据实体。
		/// 这里不支持嵌套类型的数据实体，且要求各数据成员都是简单的数据类型。
		/// </summary>
		/// <param name="request"></param>
		/// <param name="model"></param>
		public static void FillModel(HttpRequest request, object model, string paramName)
		{
			ModelDescripton descripton = ReflectionHelper.GetModelDescripton(model.GetType());

			object val = null;
			foreach( DataMember field in descripton.Fields ) {
				if( field.Ignore )
					continue;

				// 这里的实现方式不支持嵌套类型的数据实体。
				// 如果有这方面的需求，可以将这里改成递归的嵌套调用。

				val = GetValueByNameAndTypeFrommRequest(
									request, field.Name, field.Type.GetRealType(), paramName);
				if( val != null )
					field.SetValue(model, val);
			}
		}


		/// <summary>
		/// 读取一个HTTP参数值。这里只读取QueryString以及Form
		/// </summary>
		/// <param name="request"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		private static string[] GetHttpValues(HttpRequest request, string name)
		{
			string[] val = request.QueryString.GetValues(name);
			if( val == null )
				val = request.Form.GetValues(name);

			return val;
		}
	

		private static string[] GetValueFromHttpRequest(HttpRequest request, string name, string parentName)
		{
			string[] val = GetHttpValues(request, name);
			if( val == null ) {
				// 再试一次。有可能是多个自定义类型，Form表单元素采用变量名做为前缀。
				if( string.IsNullOrEmpty(parentName) == false ) {
					val = GetHttpValues(request, parentName + "." + name);
				}
			}
			return val;
		}
		
		public static object GetValueByNameAndTypeFrommRequest(
							HttpRequest request, string name, Type type, string parentName)
		{
			MethodInfo stringImplicit = null;

			// 检查是否为不支持的参数类型
			if( type.IsSupportableType() == false ) {

				// 检查是否可以做隐式类型转换
				stringImplicit = GetStringImplicit(type);

				if( stringImplicit == null )
					return null;
			}

			string[] val = GetValueFromHttpRequest(request, name, parentName);

			if( type == typeof(string[]) )
				return val;

			if( val == null || val.Length == 0 ) 
				return null;

			// 还原ASP.NET的默认数据格式
			string str = val.Length == 1 ? val[0] : string.Join(",", val);

			// 可以做隐式类型转换
			if( stringImplicit != null )
				return stringImplicit.FastInvoke(null, str.Trim());


			return SafeChangeType(str.Trim(), type);
		}



		public static object SafeChangeType(string value, Type conversionType)
		{
			if( conversionType == typeof(string)  )
				return value;

			if( value == null || value.Length == 0 ) {
				// 空字符串根本不能做任何转换，所以直接返回null
				return null;
			}

			try {
				if( conversionType == typeof(Guid) )
					return new Guid(value);

				if( conversionType.IsEnum )
					return Enum.Parse(conversionType, value);


				// 为了简单，直接调用 .net framework中的方法。
				// 如果转换失败，则会抛出异常。
				return Convert.ChangeType(value, conversionType);
			}
			catch {
				if( IsDebugMode )
					throw;			// Debug 模式下抛异常
				else {
					// Release模式下忽略异常（防止恶意用户错误输入）
					return null;
				}
			}
		}


		/// <summary>
		/// 判断指定的类型是否能从String类型做隐式类型转换，如果可以，则返回相应的方法
		/// </summary>
		/// <param name="conversionType"></param>
		/// <returns></returns>
		private static MethodInfo GetStringImplicit(Type conversionType)
		{
			MethodInfo m = conversionType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public);

			if( m != null && m.IsStatic && m.IsSpecialName && m.ReturnType == conversionType ) {
				ParameterInfo[] paras = m.GetParameters();
				if( paras.Length == 1 && paras[0].ParameterType == typeof(string) )
					return m;
			}

			return null;
		}


	}


	
}
