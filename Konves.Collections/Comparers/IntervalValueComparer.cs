using System;
using System.Collections;
using Konves.Collections.Generic;

namespace Konves.Collections.Comparers
{
	public class IntervalValueComparer<TBound> : IComparer where TBound : IComparable<TBound>
	{
		public int Compare(object x, object y)
		{
			IInterval<TBound> interval = x as IInterval<TBound>;
			TBound value;
			int sign;

			if (!ReferenceEquals(interval, null) && y is TBound)
			{
				value = (TBound) y;
				sign = -1;
			}
			else
			{
				interval = y as IInterval<TBound>;

				if (!ReferenceEquals(interval, null) && x is TBound)
				{
					value = (TBound) x;
					sign = 1;
				}
				else
				{
					throw new InvalidOperationException("'x' cannot be compared to 'y'");
				}
			}

			int valueLower = value.CompareTo(interval.LowerBound.Value);
			if (valueLower < 0 || (valueLower == 0 && !interval.LowerBound.IsInclusive))
				return -1 * sign;

			int valueUpper = value.CompareTo(interval.UpperBound.Value);
			if (valueUpper > 0 || (valueUpper == 0 && !interval.UpperBound.IsInclusive))
				return 1 * sign;

			return 0;
		}
	}
}
