using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandMVCDemo
{
    public class HomeService
    {
        //Get /Home/helloWorld
        public string helloWorld()
        {
            return "你好,世界！";
        }
    }
}