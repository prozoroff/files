using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TypeCastTest
{
	class Program
	{
		static long[] result = new long[]{0,0,0};
		static Action[] methods = new Action[]{ VirtualCallTest, AsCastTest, ExplicitCastTest };

		static int iterCount = 100000;
		static int measureCount = 1000;

		static void Main(string[] args)
		{
			for (int i = 0; i < measureCount; i++) 
			{
				for (int m = 0; m < 3; m++) 
				{
					result [m] += measureTicks (methods [m]);
				}
			}

			Console.WriteLine ("Virtual Call: " + ((long)(result [0] / measureCount)).ToString ());
			Console.WriteLine ("'As' method: " + ((long)(result [1] / measureCount)).ToString ());
			Console.WriteLine ("'()' method: " + ((long)(result [2] / measureCount)).ToString ());
			Console.ReadKey ();

		}

		static long measureTicks(Action method)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			method.Invoke();
			stopwatch.Stop();
			return stopwatch.ElapsedTicks;
		}
			
		static A a = new B();
		static A b = new B();

		static void VirtualCallTest()
		{
			for (int i = 0; i < iterCount; i++)
				b = a.GetB();
		}

		static void AsCastTest()
		{
			for (int i = 0; i < iterCount; i++)
				b = a as B;
		}

		static void ExplicitCastTest()
		{
			for (int i = 0; i < iterCount; i++)
				b = (B)a;
		}

		class A
		{
			protected int _i;

			public virtual B GetB() { return null; }
		}

		class B : A
		{
			public B()
			{
				_a = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
				_s = "some test string";
				_t = DateTime.Now;
				_i = 0;
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
		}
	}
}
