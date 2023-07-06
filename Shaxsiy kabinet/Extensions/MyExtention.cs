using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Extensions
{
    internal static class MyExtention
    {
        public static void AddText(this string str, char ch, out string result)
        {
            result = str + ch;
        }
    }
}
