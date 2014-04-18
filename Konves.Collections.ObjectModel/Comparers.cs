using System;
using System.Collections;
using System.Collections.Generic;

namespace Konves.Collections.ObjectModel
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

	public class IntervalValueComparer<TBound> : IComparer where TBound : IComparable<TBound>
	{
		public int Compare(object x, object y)
		{
			IInterval<TBound> interval = x as IInterval<TBound>;
			TBound value;

			if (!ReferenceEquals(interval, null) && y is TBound)
			{
				value = (TBound) y;
			}
			else
			{
				interval = y as IInterval<TBound>;

				if (!ReferenceEquals(interval, null) && x is TBound)
				{
					value = (TBound) x;
				}
				else
				{
					throw new ArgumentException("'x' cannot be compared to 'y'");
				}
			}

			int valueLower = value.CompareTo(interval.LowerBound.Value);
			if (valueLower < 0 || (valueLower == 0 && !interval.LowerBound.IsInclusive))
				return -1;

			int valueUpper = value.CompareTo(interval.UpperBound.Value);
			if (valueUpper > 0 || (valueUpper == 0 && !interval.UpperBound.IsInclusive))
				return 1;

			return 0;
		}
	}
}
