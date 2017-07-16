using System;
using System.Collections.Generic;
using System.Linq;


namespace LandMVCDemo
{
    public class HomeService
    {
        // /Home/helloWorld
        public string helloWorld()
        {
            return "你好,世界！";
        }
        // /Home/Add?a=10&b=10
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}