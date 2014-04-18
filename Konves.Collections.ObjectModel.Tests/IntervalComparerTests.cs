using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.Collections.ObjectModel.Tests
{
	[TestClass()]
	public class IntervalComparerTests
	{
		[TestMethod()]
		public void CompareTest_1()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 3 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 4 }
			};

			int expected = -1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_2()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 3 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 4 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int expected = 1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_3()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			int expected = 0;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_4()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int expected = 0;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_5()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = false, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			int expected = -1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_6()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = false, Value = 2 }
			};

			int expected = 1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_7()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = false, Value = 2 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			int expected = -1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void CompareTest_8()
		{
			// Arrange
			IntervalComparer<int> comparer = new IntervalComparer<int>();

			IInterval<int> x = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = false, Value = 2 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 3 }
			};

			IInterval<int> y = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int expected = 1;

			//Act
			int result = comparer.Compare(x, y);

			// Assert
			Assert.AreEqual(expected, result);
		}

		public class Interval<TBound> : IInterval<TBound> where TBound : IComparable<TBound>
		{
			public IBound<TBound> LowerBound { get; set; }
			public IBound<TBound> UpperBound { get; set; }
		}

		public class Bound<T> : IBound<T> where T : IComparable<T>
		{
			public T Value { get; set; }
			public bool IsInclusive { get; set; }
		}
	}
}
