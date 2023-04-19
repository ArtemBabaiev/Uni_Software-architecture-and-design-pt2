using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    internal class MyAttirubute: Attribute
    {
        string url;

        public MyAttirubute()
        {
        }

        public MyAttirubute(string url)
        {
            this.url = url;
        }
    }
}
