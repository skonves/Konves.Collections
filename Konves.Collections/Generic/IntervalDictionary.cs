using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Konves.Collections.Comparers;
using Konves.Collections.ObjectModel;

namespace Konves.Collections.Generic
{
	/// <summary>
	/// Represents a collection of intervals and values.
	/// </summary>
	/// <typeparam name="TBound">The type of the interval bounds in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	public class IntervalDictionary<TBound, TValue> : IIntervalDictionary<TBound, TValue> where TBound : IComparable<TBound>
	{
		/// <summary>
		/// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		public ICollection<TValue> Values
		{
			get { return m_root.Traverse().Select(n => n.Value.Value).ToList(); }
		}

		/// <summary>
		/// Gets an <see cref="ICollection{T}"/> containing the intervals of the <see cref="IIntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		public ICollection<IInterval<TBound>> Intervals
		{
			get { return m_root.Traverse().Select(n => n.Value.Interval).ToList(); }
		}

		/// <summary>
		/// Gets or sets the element whose interval intersects the specified key.
		/// </summary>
		/// <param name="key">The key intersecting the interval of the element to get or set.</param>
		/// <returns>The element whose interval intersects the specified key</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		/// <exception cref="KeyNotFoundException">The propery is set or retrieved and an interval intersecting <paramref name="key"/> is not found.</exception>
		/// <exception cref="NotSupportedException">The property is set and the <see cref="IntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		public TValue this[TBound key]
		{
			get
			{
				if(ReferenceEquals(key,null))
					throw new ArgumentNullException("key", "key is null.");

				Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(key, s_pairKeyComparer);

				if (ReferenceEquals(node, null))
					throw new KeyNotFoundException();

				return node.Value.Value;
			}
			set
			{
				if (ReferenceEquals(key, null))
					throw new ArgumentNullException("key", "key is null.");

				if (m_isReadOnly)
					throw new NotSupportedException("the IntervalDictionary<TBound, TValue> is read-only.");

				Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(key, s_pairKeyComparer);

				if (ReferenceEquals(node, null))
					throw new KeyNotFoundException();

				node.Value.Value = value;
			}
		}

		/// <summary>
		/// Adds an item with the provided interval and value to the <see cref="IntervalDictionary{TBound,TValue}"/>. 
		/// </summary>
		/// <param name="interval">The object to use as the added element's interval.</param>
		/// <param name="value">The object to use ans the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="interval"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="interval"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The <see cref="IntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		void IIntervalDictionary<TBound, TValue>.Add(IInterval<TBound> interval, TValue value)
		{
			if (ReferenceEquals(interval, null))
				throw new ArgumentNullException("interval", "interval is null.");

			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			bool success;

			m_root = m_root.Insert(new Node<IntervalValuePair<TBound, TValue>> { Value = new IntervalValuePair<TBound, TValue>(interval, value) }, s_pairComparer, out success);

			if (!success)
				throw new ArgumentException("interval exists or intersects with an existing interval.", "interval");
		}

		/// <summary>
		/// Adds an item with the provied upper and lower bounds and value to the <see cref="IntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="lowerBound">The object to use as the inclusive lower bound of the added element's interval.</param>
		/// <param name="upperBound">The object to use as the inclusive upper bound of the added element's interval.</param>
		/// <param name="value">The object to use as the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="lowerBound"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentNullException"><paramref name="upperBound"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">The interval defined by <paramref name="lowerBound"/> and <paramref name="upperBound"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The <see cref="IntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		public void Add(TBound lowerBound, TBound upperBound, TValue value)
		{
			if (ReferenceEquals(lowerBound, null))
				throw new ArgumentNullException("lowerBound", "lowerBound is null.");

			if (ReferenceEquals(lowerBound, null))
				throw new ArgumentNullException("upperBound", "upperBound is null.");

			if(m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			bool success;

			m_root = m_root.Insert(new Node<IntervalValuePair<TBound, TValue>>
			{
				Value = new IntervalValuePair<TBound, TValue>(
					new Interval<TBound>
					{
						LowerBound = new Bound<TBound> { Value = lowerBound, IsInclusive = true },
						UpperBound = new Bound<TBound> { Value = upperBound, IsInclusive = true }
					}, value)
			}, s_pairComparer, out success);

			if (!success)
				throw new ArgumentException("The interval defined by lowerBound and upperBound exists or intersects with an existing interval.");
		}

		/// <summary>
		/// Adds an item with the provided interval parameters and value to the <see cref="IntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="lowerBound">The object to use as the value of the added element's lower bound.</param>
		/// <param name="isLowerBoundInclusive">A value indicating whether the added element's lower bound is inclusive.</param>
		/// <param name="upperBound">The object to use as the value of the added element's upper bound.</param>
		/// <param name="isUpperBoundInclusive">A value indicating whether the added element's upper bound is inclusive.</param>
		/// <param name="value">The object to sue as the added element's value.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="lowerBound"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentNullException"><paramref name="upperBound"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">The interval defined by <paramref name="lowerBound"/> and <paramref name="upperBound"/> exists or intersects with an existing interval.</exception>
		/// <exception cref="NotSupportedException">The <see cref="IntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		public void Add(TBound lowerBound, bool isLowerBoundInclusive, TBound upperBound, bool isUpperBoundInclusive, TValue value)
		{
			if (ReferenceEquals(lowerBound, null))
				throw new ArgumentNullException("lowerBound", "lowerBound is null.");

			if (ReferenceEquals(lowerBound, null))
				throw new ArgumentNullException("upperBound", "upperBound is null.");

			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			bool success;

			m_root = m_root.Insert(new Node<IntervalValuePair<TBound, TValue>>
			{
				Value = new IntervalValuePair<TBound, TValue>(
					new Interval<TBound>
					{
						LowerBound = new Bound<TBound> { Value = lowerBound, IsInclusive = isLowerBoundInclusive },
						UpperBound = new Bound<TBound> { Value = upperBound, IsInclusive = isUpperBoundInclusive }
					}, value)
			}, s_pairComparer, out success);

			if (!success)
				throw new ArgumentException("The interval defined by lowerBound and upperBound exists or intersects with an existing interval.");
		}

		/// <summary>
		/// Determines whether the <see cref="IntervalDictionary{TBound,TValue}"/> contains an element whose interval intersects the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="IIntervalDictionary{TBound,TValue}"/>.</param>
		/// <returns><c>true</c> if the <see cref="IIntervalDictionary{TBound,TValue}"/> contains an interval which intersects the key; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		public bool IntersectsKey(TBound key)
		{
			if (ReferenceEquals(key, null))
				throw new ArgumentNullException("key", "key is null.");

			return !ReferenceEquals(m_root.Search(key, s_pairKeyComparer), null);
		}

		/// <summary>
		/// Removes the element whose iterval intersects the specified key from the <see cref="IntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <param name="key">The key intersected by the element to remove.</param>
		/// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.  This method also returns <c>false</c> if and element with an interval intersecting <paramref name="key"/> was not found in the original <see cref="IIntervalDictionary{TBound,TValue}"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		/// <exception cref="NotSupportedException">The <see cref="IntervalDictionary{TBound,TValue}"/> is read-only.</exception>
		public bool Remove(TBound key)
		{
			if (ReferenceEquals(key, null))
				throw new ArgumentNullException("key", "key is null.");

			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			Node<IntervalValuePair<TBound, TValue>> node;
			m_root = m_root.Remove(key, s_pairKeyComparer, out node);
			return !ReferenceEquals(node, null);
		}

		/// <summary>
		/// Gets the value associated with the interval that intersects the specified key.
		/// </summary>
		/// <param name="key">The key intersected by the interval of the element whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the interval that intersects the specified key, if such an interval is found; otherwise, the default value for the type of the <paramref name="value"/> parameter.  This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if the <see cref="IntervalDictionary{TBound,TValue}"/> contains an element whose interval intersects the specified key; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		public bool TryGetValue(TBound key, out TValue value)
		{
			if (ReferenceEquals(key, null))
				throw new ArgumentNullException("key", "key is null.");

			Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(key, s_pairKeyComparer);

			if (ReferenceEquals(node, null))
			{
				value = default(TValue);
				return false;
			}
			else
			{
				value = node.Value.Value;
				return true;
			}
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="IntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		public int Count
		{
			get { return m_root.Traverse().Count(); }
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IntervalDictionary{TBound,TValue}"/>.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<IntervalValuePair<TBound, TValue>> GetEnumerator()
		{
			return m_root.Traverse().Select(n => n.Value).GetEnumerator();
		}

		void ICollection<IntervalValuePair<TBound, TValue>>.Add(IntervalValuePair<TBound, TValue> item)
		{
			if (ReferenceEquals(item, null))
				throw new ArgumentNullException("item", "item is null.");

			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			bool success;
			m_root = m_root.Insert(new Node<IntervalValuePair<TBound, TValue>> { Value = item }, s_pairComparer, out success);
			
			if (!success)
				throw new ArgumentException("The item's interval exists or intersects with an existing interval.");
		}

		void ICollection<IntervalValuePair<TBound, TValue>>.Clear()
		{
			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			m_root = null;
		}

		bool ICollection<IntervalValuePair<TBound, TValue>>.Contains(IntervalValuePair<TBound, TValue> item)
		{
			if (ReferenceEquals(item, null))
				throw new ArgumentNullException("item", "item is null");

			Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(item, s_pairComparer);

			if (ReferenceEquals(node, null))
				return false;

			return item.Value.Equals(node.Value.Value);
		}

		void ICollection<IntervalValuePair<TBound, TValue>>.CopyTo(IntervalValuePair<TBound, TValue>[] array, int arrayIndex)
		{
			if (ReferenceEquals(array, null))
				throw new ArgumentNullException("array", "array is null");

			if(arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");

			int i = arrayIndex;
			foreach (var node in m_root.Traverse())
			{
				if (i < array.Length)
					throw new ArgumentException("The number of elements in the dictionary is greater than the available space from arrayIndex to the end of the destination array.");

				array[i] = node.Value;

				i++;
			}
		}

		bool ICollection<IntervalValuePair<TBound, TValue>>.IsReadOnly
		{
			get { return m_isReadOnly; }
		}

		bool ICollection<IntervalValuePair<TBound, TValue>>.Remove(IntervalValuePair<TBound, TValue> item)
		{
			if (ReferenceEquals(item, null))
				throw new ArgumentNullException("item", "item is null");

			if (m_isReadOnly)
				throw new NotSupportedException("The IntervalDictionary<TBound, TValue> is read-only.");

			Node<IntervalValuePair<TBound, TValue>> removedNode;

			Node<IntervalValuePair<TBound, TValue>> node = m_root.Remove(item, s_pairComparer, out removedNode);

			return !ReferenceEquals(removedNode, null);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_root.Traverse().Select(n => n.Value).GetEnumerator();
		}

		class Interval<TBound> : IInterval<TBound> where TBound : IComparable<TBound>
		{
			public IBound<TBound> LowerBound { get; set; }
			public IBound<TBound> UpperBound { get; set; }
		}

		class Bound<TBound> : IBound<TBound> where TBound : IComparable<TBound>
		{
			public TBound Value { get; set; }
			public bool IsInclusive { get; set; }
		}

		Node<IntervalValuePair<TBound, TValue>> m_root;
		bool m_isReadOnly = false;

		static readonly IntervalValuePairComparer<TBound, TValue> s_pairComparer = new IntervalValuePairComparer<TBound, TValue>();
		static readonly IntervalValuePairKeyComparer<TBound, TValue> s_pairKeyComparer = new IntervalValuePairKeyComparer<TBound, TValue>();
	}
}
