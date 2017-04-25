using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
	public interface IActionResult
	{
		void Ouput(HttpContext context);
	}
}
