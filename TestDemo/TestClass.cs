using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    [MyAttirubute]
    internal class TestClass
    {
    [MyAttirubute("my-url")]
        public void Do()
        {
            Console.WriteLine("Done");
        }
    }
}
