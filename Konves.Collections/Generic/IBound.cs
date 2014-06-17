using System;

namespace Konves.Collections.Generic
{
	/// <summary>
	/// Represents an inclusive or exclusive bound.
	/// </summary>
	/// <typeparam name="T">The type of the bound's value.</typeparam>
	public interface IBound<T> where T : IComparable<T>
	{
		/// <summary>
		/// Gets the value of this bound.
		/// </summary>
		T Value { get; }
		/// <summary>
		/// Gets a value indicating whether or no this bound is inclusive.
		/// </summary>
		bool IsInclusive { get; }
	}
}
