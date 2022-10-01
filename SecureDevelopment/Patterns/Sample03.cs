using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Sample03
    {
        public void Main(string[] args)
        {
            var obj1 = ProductFactory.Create<SampleProduct1>();
            var obj2 = ProductFactory.Create<SampleProduct2>();

            ProductFactory.Create(typeof(SampleProduct2));

            Console.ReadKey();
        }

    }

    public abstract class Product
    {
        public abstract void PostConstruction();
    }

    public class SampleProduct1 : Product
    {
        public SampleProduct1() { }

        public SampleProduct1(int a, int b)
        {

        }

        public override void PostConstruction()
        {
            Console.WriteLine("Product1 created.");
        }
    }

    public class SampleProduct2 : Product
    {
        public SampleProduct2() { }

        public SampleProduct2(string a)
        {

        }

        public override void PostConstruction()
        {
            Console.WriteLine("Product2 created.");
        }
    }

    public static class ProductFactory
    {
        public static T Create<T>() where T : Product, new()
        {
            var t = new T();
            t.PostConstruction();
            return t;
        }

        //public static Product Create(Product t)
        //{
        //    return null;
        //}

        public static Product Create(Type t)
        {

            // CreateInstance
            var assembly = Assembly.GetAssembly(typeof(Sample03));
            return (Product)assembly.CreateInstance(t.FullName);
        }
    }
}
