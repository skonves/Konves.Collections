using System;
using System.Collections;
using Konves.Collections.Generic;

namespace Konves.Collections.Comparers
{
	/// <summary>
	/// Provides functionality to compare an IntervalValuePair with a Key of the type of the IntervalValuePair's interval bounds.
	/// </summary>
	/// <typeparam name="TBound"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class IntervalValuePairKeyComparer<TBound,TValue> : IComparer where TBound : IComparable<TBound>
	{
		public int Compare(object x, object y)
		{
			IntervalValuePair<TBound, TValue> pair = x as IntervalValuePair<TBound, TValue>;
			TBound value;

			if (!ReferenceEquals(pair, null) && y is TBound)
			{
				value = (TBound) y;
				return s_comparer.Compare(pair.Interval, value);
			}

			pair = y as IntervalValuePair<TBound, TValue>;

			if (!ReferenceEquals(pair, null) && x is TBound)
			{
				value = (TBound) x;
				return s_comparer.Compare(value, pair.Interval);
			}

			throw new InvalidOperationException("'x' cannot be compared to 'y'");
		}

		static readonly IntervalValueComparer<TBound> s_comparer = new IntervalValueComparer<TBound>();
	}
}
