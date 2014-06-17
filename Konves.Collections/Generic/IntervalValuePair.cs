using System;

namespace Konves.Collections.Generic
{
	public class IntervalValuePair<TBound, TValue> where TBound : IComparable<TBound>
	{
		public IntervalValuePair(IInterval<TBound> interval, TValue value)
		{
			m_interval = interval;
			Value = value;
		}

		public IInterval<TBound> Interval { get { return m_interval; } }

		public TValue Value { get; internal set; }

		readonly IInterval<TBound> m_interval;
	}
}
