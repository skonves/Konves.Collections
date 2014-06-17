using System;
using System.Collections.Generic;
using Konves.Collections.Generic;

namespace Konves.Collections.Comparers
{
	public class IntervalComparer<TBound> : IComparer<IInterval<TBound>> where TBound : IComparable<TBound>
	{
		public int Compare(IInterval<TBound> x, IInterval<TBound> y)
		{
			int lowerUpper = x.LowerBound.Value.CompareTo(y.UpperBound.Value);
			if (lowerUpper > 0 || (lowerUpper == 0 && (!x.LowerBound.IsInclusive || !y.UpperBound.IsInclusive)))
				return 1;

			int upperLower = x.UpperBound.Value.CompareTo(y.LowerBound.Value);
			if (upperLower < 0 || (upperLower == 0 && (!x.UpperBound.IsInclusive || !y.LowerBound.IsInclusive)))
				return -1;

			return 0;
		}
	}
}
