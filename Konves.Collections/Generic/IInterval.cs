using System;

namespace Konves.Collections.Generic
{
	/// <summary>
	/// Represents the interval defined by two bounds.
	/// </summary>
	/// <typeparam name="TBound">The type of the value of the interval's bounds.</typeparam>
	public interface IInterval<TBound> where TBound : IComparable<TBound>
	{
		/// <summary>
		/// Gets this interval's lower bound.
		/// </summary>
		IBound<TBound> LowerBound { get; }
		/// <summary>
		/// Gets this interval's upper bound.
		/// </summary>
		IBound<TBound> UpperBound { get; }
	}
}
