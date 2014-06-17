using System;
using Konves.Collections.Comparers;
using Konves.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.Collections.ObjectModel.Tests
{
	[TestClass()]
	public class IntervalValueComparerTests
	{
		[TestMethod]
		public void CompareTest_ValueFirst_1()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 3;

			int expected = 1;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_ValueFirst_2()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 1;

			int expected = 0;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_ValueFirst_3()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 2;

			int expected = 0;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_ValueFirst_4()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 0;

			int expected = -1;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_ValueFirst_5()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = false, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 1;

			int expected = -1;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_ValueFirst_6()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = false, Value = 2 }
			};

			int value = 2;

			int expected = 1;

			// Act
			int result = comparer.Compare(value, interval);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_1()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 3;

			int expected = -1;

			// Act
			int result = comparer.Compare(interval, value);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_2()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 1;

			int expected = 0;

			// Act
			int result = comparer.Compare(interval, value);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_3()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 2;

			int expected = 0;

			// Act
			int result = comparer.Compare(interval, value);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_4()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 0;

			int expected = 1;

			// Act
			int result = comparer.Compare(interval, value);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_5()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = false, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = true, Value = 2 }
			};

			int value = 1;

			int expected = 1;

			// Act
			int result = comparer.Compare(interval, value);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CompareTest_IntervalFirst_6()
		{
			// Arrange
			IntervalValueComparer<int> comparer = new IntervalValueComparer<int>();

			IInterval<int> interval = new Interval<int>
			{
				LowerBound = new Bound<int> { IsInclusive = true, Value = 1 },
				UpperBound = new Bound<int> { IsInclusive = false, Value = 2 }
			};

			int value = 2;

			int expected = -1;

			// Act
			int result = comparer.Compare(interval, value);

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
