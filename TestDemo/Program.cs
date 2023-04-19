using System.Reflection;

namespace TestDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new TestClass();
            var t = typeof(TestClass).GetTypeInfo();
            foreach (var item in t.GetRuntimeMethods())
            {
                var attrs = item.GetCustomAttributes();
                foreach (var attr in attrs)
                {


                Console.WriteLine(attr);
                }
            }
        }
    }
}