using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Konves.Collections.ObjectModel
{
	public interface IBound<T>
	{
		T Value { get; }
		bool IsInclusive { get; }
	}

	public interface IInterval<TBound>
	{
		IBound<TBound> LowerBound { get; }
		IBound<TBound> UpperBound { get; }
	}

	public class IntervalValuePair<TBound, TValue>
	{
		public IntervalValuePair(IInterval<TBound> interval, TValue value)
		{
			m_interval = interval;
			m_value = value;
		}

		public IInterval<TBound> Interval { get { return m_interval; } }

		public TValue Value
		{
			get {
				return m_value;
			}
			internal set
			{
				m_value = value;
			}
		}

		readonly IInterval<TBound> m_interval;

		TValue m_value;
	}

	public interface IIntervalDictionary<TBound, TValue> : ICollection<IntervalValuePair<TBound, TValue>>, IEnumerable<IntervalValuePair<TBound, TValue>>, IEnumerable
	{
		ICollection<TValue> Values { get; }
		TValue this[TBound key] { get; set; }
		void Add(IInterval<TBound> interval, TValue value);
		bool ContainsKey(TBound key);
		bool Remove(TBound key);
		bool TryGetValue(TBound key, out TValue value);
	}

	/// <summary>
	/// Provides functionality to compare two intervals;
	/// </summary>
	/// <typeparam name="TBound">The type of the interval bound.</typeparam>
	public class IntervalComparer<TBound> : IComparer where TBound : IComparable<TBound>
	{
		public int Compare(object x, object y)
		{
			IInterval<TBound> a = x as IInterval<TBound>;
			IInterval<TBound> b = x as IInterval<TBound>;

			if (!ReferenceEquals(a, null) && !ReferenceEquals(b, null))
			{
			}
		}
	}

	public class ValueComparer<TBound, TValue> : IComparer where TBound : IComparable<TBound>
	{
		public int Compare(object x, object y)
		{
			throw new NotImplementedException();
		}
	}

	public class IntervalDictionary<TBound, TValue> : IIntervalDictionary<TBound, TValue> where TBound : IComparable<TBound>
	{
		Node<IntervalValuePair<TBound, TValue>> m_root = null;
		readonly IntervalValuePairComparer<TBound, TValue> m_intervalComparer = new IntervalValuePairComparer<TBound, TValue>();
		readonly IntervalToValueComparer<TBound, TValue> m_valueComparer = new IntervalToValueComparer<TBound, TValue>();

		public ICollection<TValue> Values
		{
			get
			{
				return m_root.Traverse().Select(n => n.Value.Value).ToArray();
			}
		}

		public TValue this[TBound key]
		{
			get
			{
				return m_root.Search(key, m_valueComparer).Value.Value;
			}
			set
			{
				Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(key, m_valueComparer);

				if (ReferenceEquals(node, null))
					node.Value.Value = value;

				throw new KeyNotFoundException();
			}
		}

		public void Add(IInterval<TBound> interval, TValue value)
		{
			m_root = m_root.Insert(new Node<IntervalValuePair<TBound, TValue>> { Value = new IntervalValuePair<TBound, TValue>(interval, value) }, m_intervalComparer);
		}

		public bool ContainsKey(TBound key)
		{
			return !ReferenceEquals(m_root.Search(key, m_valueComparer), null);
		}

		public bool Remove(TBound key)
		{
			Node<IntervalValuePair<TBound, TValue>> node;
			m_root = m_root.Remove(key, m_valueComparer, out node);
			return !ReferenceEquals(node, null);
		}

		public bool TryGetValue(TBound key, out TValue value)
		{
			Node<IntervalValuePair<TBound, TValue>> node = m_root.Search(key, m_valueComparer);

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

		public void Add(IntervalValuePair<TBound, TValue> item)
		{
			m_root.Insert(new Node<IntervalValuePair<TBound, TValue>> { Value = item }, m_intervalComparer);
		}

		public void Clear()
		{
			m_root = null;
		}

		public bool Contains(IntervalValuePair<TBound, TValue> item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(IntervalValuePair<TBound, TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { return m_root.Traverse().Count(); }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(IntervalValuePair<TBound, TValue> item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<IntervalValuePair<TBound, TValue>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
