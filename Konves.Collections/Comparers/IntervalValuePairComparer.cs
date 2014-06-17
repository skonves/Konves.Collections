using System;
using System.Collections;
using System.Collections.Generic;
using Konves.Collections.Generic;

namespace Konves.Collections.Comparers
{
	public class IntervalValuePairComparer<TBound, TValue> : IComparer<IntervalValuePair<TBound,TValue>>, IComparer where TBound : IComparable<TBound>
	{
		public int Compare(IntervalValuePair<TBound, TValue> x, IntervalValuePair<TBound, TValue> y)
		{
			return s_comparer.Compare(x.Interval, y.Interval);
		}

		static readonly IntervalComparer<TBound> s_comparer = new IntervalComparer<TBound>();

		public int Compare(object x, object y)
		{
			throw new NotImplementedException();
		}
	}
}
