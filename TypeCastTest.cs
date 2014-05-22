using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TypeCastTest
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var a in new Action[]{VirtualCallTest, AsCastTest, ExplicitCastTest})
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                a.Invoke();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedTicks.ToString());
            }

            Console.ReadKey();

        }

        static int IterCount = 10000;
        static A a = new B();
        static A b = new B();

        static void VirtualCallTest()
        {
            for (int i = 0; i < IterCount; i++)
                b = a.GetB();
        }

        static void AsCastTest()
        {
            for (int i = 0; i < IterCount; i++)
                b = a as B;
        }

        static void ExplicitCastTest()
        {
            for (int i = 0; i < IterCount; i++)
                b = (B)a;
        }

        class A
        {
            protected int _i;

            public virtual B GetB() { return null; }
        }

        class B : A, ICloneable
        {
            public B()
            {
                _a = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                _s = "some test string";
                _t = DateTime.Now;
            }

            public B(B b)
            {
                _a = b._a;
                _s = b._s;
                _i = b._i;
                _t = b._t;
            }

            public int[] _a;
            string _s;
            DateTime _t;

            public override B GetB() { return this; }

            #region ICloneable Members
            public object Clone()
            {
                return MemberwiseClone();
            }
            #endregion
        }


    }
}
