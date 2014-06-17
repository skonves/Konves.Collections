using System;
using System.Collections.Generic;

namespace Konves.Collections.Generic
{
	public interface IIntervalDictionary<TBound, TValue> : ICollection<IntervalValuePair<TBound, TValue>> where TBound : IComparable<TBound>
	{
		/// <summary>
		/// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		ICollection<TValue> Values { get; }
		/// <summary>
		/// Gets an <see cref="ICollection{T}"/> containing the intervals of the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		ICollection<IInterval<TBound>> Intervals { get; }
		/// <summary>
		/// Gets or sets the element whose interval intersects the specified key.
		/// </summary>
		/// <param name="key">The key intersecting the interval of the element to get or set.</param>
		/// <returns>The element whose interval intersects the specified key</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		/// <exception cref="KeyNotFoundException">The propery is set or retrieved and an interval intersecting <paramref name="key"/> is not found.</exception>
		/// <exception cref="NotSupportedException">The property is set and the object that implements <see cref="IIntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		TValue this[TBound key] { get; set; }
		/// <summary>
		/// Adds an item with the provided interval and value to the <see cref="IIntervalDictionary{TBound,TValue}"/>. 
		/// </summary>
		/// <param name="interval">The object to use as the added element's interval.</param>
		/// <param name="value">The object to use ans the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="interval"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="interval"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The object implementing <see cref="IIntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		void Add(IInterval<TBound> interval, TValue value);
		/// <summary>
		/// Adds an item with the provied upper and lower bounds and value to the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="lowerBound">The object to use as the inclusive lower bound of the added element's interval.</param>
		/// <param name="upperBound">The object to use as the inclusive upper bound of the added element's interval.</param>
		/// <param name="value">The object to use as the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="lowerBound"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentNullException"><paramref name="upperBound"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">The interval defined by <paramref name="lowerBound"/> and <paramref name="upperBound"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The object implementing <see cref="IIntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		void Add(TBound lowerBound, TBound upperBound, TValue value);
		/// <summary>
		/// Adds an item with the provided interval parameters and value to the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="lowerBound">The object to use as the value of the added element's lower bound.</param>
		/// <param name="isLowerBoundInclusive">A value indicating whether the added element's lower bound is inclusive.</param>
		/// <param name="upperBound">The object to use as the value of the added element's upper bound.</param>
		/// <param name="isUpperBoundInclusive">A value indicating whether the added element's upper bound is inclusive.</param>
		/// <param name="value">The object to sue as the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="lowerBound"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentNullException"><paramref name="upperBound"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">The interval defined by <paramref name="lowerBound"/> and <paramref name="upperBound"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The object implementing <see cref="IIntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		void Add(TBound lowerBound, bool isLowerBoundInclusive, TBound upperBound, bool isUpperBoundInclusive, TValue value);
		/// <summary>
		/// Determines whether the <see cref="IIntervalDictionary{TBound,TValue}"/> contains an element whose interval intersects the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="IIntervalDictionary{TBound,TValue}"/>.</param>
		/// <returns><c>true</c> if the <see cref="IIntervalDictionary{TBound,TValue}"/> contains an interval which intersects the key; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		bool IntersectsKey(TBound key);
		/// <summary>
		/// Removes the element whose iterval intersects the specified key from the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="key">The key intersected by the element to remove.</param>
		/// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.  This method also returns <c>false</c> if and element with an interval intersecting <paramref name="key"/> was not found in the original <see cref="IIntervalDictionary{TBound,TValue}"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		/// <exception cref="NotSupportedException">The object implementing <see cref="IIntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		bool Remove(TBound key);
		/// <summary>
		/// Gets the value associated with the interval that intersects the specified key.
		/// </summary>
		/// <param name="key">The key intersected by the interval of the element whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the interval that intersects the specified key, if such an interval is found; otherwise, the default value for the type of the <paramref name="value"/> parameter.  This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if the object that implements <see cref="IIntervalDictionary{TBound,TValue}"/> contains an element whose interval intersects the specified key; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		bool TryGetValue(TBound key, out TValue value);
	}
}
