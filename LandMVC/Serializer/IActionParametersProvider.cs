using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC.Serializer
{
	internal interface IActionParametersProvider
	{
		object[] GetParameters(HttpRequest request, ActionDescription action);

	}
}
