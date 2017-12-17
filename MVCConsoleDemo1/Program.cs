using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCConsoleDemo1
{
    class Program
    {

        delegate int AddDel(int a, int b);
        AddDel fun = delegate(int a, int b) { return a + b; };
        //Console.WriteLine(fun(1, 3));

        AddDel funLambda = (a, b) => a + b;

        static bool GetEvenNum(int num)
        {
            if (num % 2 == 0) return true;
            return false;
        }

        static void Main(string[] args)
        {
            List<string> strs = new List<string>() { "1", "2", "3" };
            var temp = strs.FindAll(s => int.Parse(s) > 1);
            foreach (var item in temp)
                Console.WriteLine(item);

            List<int> nums = new List<int>() { 1, 2, 3, 4, 6, 9, 12 };
            List<int> evenNums = nums.FindAll(GetEvenNum);
            foreach (var item in evenNums)
                Console.WriteLine(item);

            List<int> evenNumLamdas = nums.FindAll(N => N % 2 == 0);
            foreach (var item in evenNumLamdas)
                Console.WriteLine(item);
            //  string str1 = "a"; string str2 = "b";
            //  Swap<string>(ref str1, ref str2);
            //  Console.WriteLine(str1 + "," + str2);
            ////
            ////  ConsoleWrite DELcw1 = new ConsoleWrite(string WriteMsg);
            //   //DELcw1("天下第一");
            //  ConsoleWrite delCW2 = delegate(string strMsg)
            //  {
            //      Console.WriteLine(strMsg);
            //  };
            //  delCW2("aaabbbcc");

        }
        //    //泛形委託
        //    public delegate void Del<T>(T item);
        //    public static void Notify(int i) { }

        //    Del<int> m1 = new Del<int>(Notify);
        //    //泛形方法
        //    static void Swap<T>(ref T t1, ref T t2) { T temp = t1; t1 = t2; t2 = temp; }
        //    //匿名方法
        //    public delegate void ConsoleWrite(string strMsg);

        //    //系統內置委託
        //    Action<Object> test = delegate(object o) { Console.WriteLine(o); };
        //  //  Action<string> test2 = test;
        //    Func<string> fest = delegate(){ return Console.ReadLine();};
        //   // fest2 =fest;
        //    public delegate void Action();
        //    public delegate bool Predicate<in T>(T obj);
        //    public delegate int Comparison<in T>(T x, T y);

        //    public delegate TResult Func<out TResult>();
        //    public delegate TResult Func<in T,out TResult>(T arg);

        //    //
        //    public delegate void Action<in T>(T obj);
        //    public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);

        //   // public delegate bool Predicate<in T>(T obj);

        //   // public delegate int Comparison<in T>(T x, T y);

        //}
        ////泛形接口
        //interface MyInterface<T1, T2, T3> { T1 Method1(T2 param1, T3 param2);}
        //public class MyClass<T1, T2, T3> : MyInterface<T1, T2, T3> { public T1 Method(T2 param1, T3 param2) { throw new NotImplementedException(); } };

    }
}

