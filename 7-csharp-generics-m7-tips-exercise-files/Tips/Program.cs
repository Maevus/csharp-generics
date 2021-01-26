using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tips
{

    public enum Steps
    {
        Step1,
        Step2,
        Step3
    }

    public static class StringExtensions
    {
        public static TEnum ParseEnum<TEnum>(this string value) where TEnum : struct // note cannot use special types like enums as constraints!
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var input = "Step1";
    //        Steps value = input.ParseEnum<Steps>(); //(Steps)Enum.Parse(typeof(Steps), input);
    //        Console.WriteLine(value);
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var numbers = new double[] { 1, 2, 3, 4, 5, 6 };
    //        var result = SampledAverage(numbers);
    //        Console.WriteLine(result);
    //    }

    //    private static object SampledAverage(double[] numbers)
    //    {
    //        var count = 0;
    //        var sum = 0.0;
    //        for (int i = 0; i < numbers.Length; i +=2)
    //        {
    //            sum += numbers[i];
    //            count += 1;
    //        }
    //        return sum / count;
    //    }
    //}

    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var a = new Item();
    //        var b = new Item();
    //        var c = new Item();

    //        Console.WriteLine("InstanceCount: " +  Item.InstanceCount);
    //    }
    //}

    //public class Item
    //{
    //    public Item()
    //    {
    //        InstanceCount += 1;

    //    }

    //    public static int InstanceCount;
    //}

    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var a = new Item<int>();
    //        var b = new Item<int>();
    //        var c = new Item<string>();

    //        Console.WriteLine("InstanceCount: " + Item<int>.InstanceCount);
    //        Console.WriteLine("InstanceCount: " + Item<string>.InstanceCount);
    //    }
    //}

    //public class Item<T>
    //{
    //    public Item()
    //    {
    //        InstanceCount += 1;

    //    }

    //    public static int InstanceCount;
    //}


    /* I fyou want instances of all types of a certain item, not just generic types, create a base class and count that.  */
    public class Program
    {
        static void Main(string[] args)
        {
            var a = new Item<int>();
            var b = new Item<int>();
            var c = new Item<string>();

            Console.WriteLine("InstanceCount: " + Item.InstanceCount);
        }
    }

    public class Item<T> : Item
    {

    }

    public class Item
    {
        public Item()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount;
    }
}
