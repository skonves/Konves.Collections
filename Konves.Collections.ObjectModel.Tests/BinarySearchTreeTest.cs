using Konves.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Konves.Collections.ObjectModel.Tests
{
	[TestClass()]
	public class BinarySearchTreeTest
	{
		public class Comparer<T> : IComparer where T : IComparable
		{
			public int Compare(object x, object y)
			{
				return (x as IComparable).CompareTo(y);
			}
		}

		[TestMethod()]
		public void GetHeight_Normative()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 2 };

			root.Left = new Node<int> { Value = 1, Height = 3 };
			root.Right = new Node<int> { Value = 3, Height = 4 };

			int expected = 5;

			// Act
			int result = root.GetHeight();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetHeight_LeftNode()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 2 };

			root.Left = new Node<int> { Value = 1, Height = 3 };

			int expected = 4;

			// Act
			int result = root.GetHeight();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetHeight_RightNode()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 1 };

			root.Right = new Node<int> { Value = 2, Height = 3 };

			int expected = 4;

			// Act
			int result = root.GetHeight();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetHeight_Leaf()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 1 };

			int expected = 0;

			// Act
			int result = root.GetHeight();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetHeight_Null()
		{
			// Arrange
			Node<int> root = null;

			int expected = -1;

			// Act
			int result = root.GetHeight();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetBalanceFactor_Normative()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 2 };

			root.Left = new Node<int> { Value = 1, Height = 3 };
			root.Right = new Node<int> { Value = 3, Height = 4 };

			int expected = -1;

			// Act
			int result = root.GetBalanceFactor();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetBalanceFactor_LeftNode()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 2 };

			root.Left = new Node<int> { Value = 1, Height = 3 };

			int expected = 4;

			// Act
			int result = root.GetBalanceFactor();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetBalanceFactor_RightNode()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 1 };

			root.Right = new Node<int> { Value = 2, Height = 3 };

			int expected = -4;

			// Act
			int result = root.GetBalanceFactor();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetBalanceFactor_Leaf()
		{
			// Arrange
			Node<int> root = new Node<int> { Value = 1 };

			int expected = 0;

			// Act
			int result = root.GetBalanceFactor();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void GetBalanceFactor_Null()
		{
			// Arrange
			Node<int> root = null;

			int expected = 0;

			// Act
			int result = root.GetBalanceFactor();

			// Assert
			Assert.AreEqual(expected, result);
		}
		
		[TestMethod()]
		public void RotateRightTest_Normative()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };

			b.Left = a;
			b.Right = c;
			d.Left = b;
			d.Right = e;

			Node<int> root = d;
			Node<int> expected = b;
			IComparer comparer = (new Comparer<int>());

			// Act
			Node<int> result = root.RotateRight();

			// Assert
			Assert.AreSame(expected, result);
			Assert.AreSame(d.Left, c);
			Assert.AreSame(d.Right, e);
			Assert.AreSame(b.Left, a);
			Assert.AreSame(b.Right, d);
		}

		[TestMethod()]
		public void RotateLeftTest_Normative()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };

			d.Left = c;
			d.Right = e;
			b.Left = a;
			b.Right = d;

			Node<int> root = b;
			Node<int> expected = d;
			IComparer comparer = (new Comparer<int>());

			// Act
			Node<int> result = root.RotateLeft();

			// Assert
			Assert.AreSame(expected, result);
			Assert.AreSame(b.Left, a);
			Assert.AreSame(b.Right, c);
			Assert.AreSame(d.Left, b);
			Assert.AreSame(d.Right, e);
		}
		
		[TestMethod()]
		public void Balance_LeftRightCase()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			d.Left = c;
			d.Right = e;
			b.Left = a;
			b.Right = d;
			f.Left = b;
			f.Right = g;

			a.Height = 0;
			c.Height = 0;
			e.Height = 0;
			g.Height = 0;
			d.Height = 1;
			b.Height = 2;
			f.Height = 3;

			Node<int> root = f;

			// Act
			Node<int> result = root.Balance();

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}

		[TestMethod()]
		public void Balance_LeftLeftCase()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			b.Left = a;
			b.Right = c;
			d.Left = b;
			d.Right = e;
			f.Left = d;
			f.Right = g;

			a.Height = 0;
			c.Height = 0;
			e.Height = 0;
			g.Height = 0;
			b.Height = 1;
			d.Height = 2;
			f.Height = 3;

			Node<int> root = f;

			// Act
			Node<int> result = root.Balance();

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}

		[TestMethod()]
		public void Balance_RightLeftCase()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			d.Left = c;
			d.Right = e;
			f.Left = d;
			f.Right = g;
			b.Left = a;
			b.Right = f;

			a.Height = 0;
			c.Height = 0;
			e.Height = 0;
			g.Height = 0;
			d.Height = 1;
			f.Height = 2;
			b.Height = 3;

			Node<int> root = b;

			// Act
			Node<int> result = root.Balance();

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}

		[TestMethod()]
		public void Balance_RightRightCase()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			f.Left = e;
			f.Right = g;
			d.Left = c;
			d.Right = f;
			b.Left = a;
			b.Right = d;

			a.Height = 0;
			c.Height = 0;
			e.Height = 0;
			g.Height = 0;
			f.Height = 1;
			d.Height = 2;
			b.Height = 3;

			Node<int> root = b;

			// Act
			Node<int> result = root.Balance();

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}
		
		[TestMethod()]
		public void TraverseTest()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			b.Left = a;
			b.Right = c;
			f.Left = e;
			f.Right = g;
			d.Left = b;
			d.Right = f;

			Node<int> root = d;

			Node<int>[] expected = new Node<int>[] { a, b, c, d, e, f, g };

			// Act
			Node<int>[] result = root.Traverse().Cast<Node<int>>().ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod()]
		public void SearchTest()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			b.Left = a;
			b.Right = c;
			f.Left = e;
			f.Right = g;
			d.Left = b;
			d.Right = f;

			Node<int> root = d;

			IComparer comparer = (new Comparer<int>());

			Node<int> expected = c;

			// Act
			Node<int> result = (Node<int>)root.Search(c.Value, comparer);

			// Assert
			Assert.AreSame(expected, result);
		}

		[TestMethod()]
		public void InsertTest()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			IComparer comparer = (new Comparer<int>());
			
			// Act
			Node<int> result =
				a
				.Insert(b, comparer)
				.Insert(c, comparer)
				.Insert(d, comparer)
				.Insert(e, comparer)
				.Insert(f, comparer)
				.Insert(g, comparer);

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}

		[TestMethod()]
		public void InsertTest_Duplicate()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			IComparer comparer = (new Comparer<int>());

			// Act
			Node<int> result =
				a
				.Insert(b, comparer)
				.Insert(c, comparer)
				.Insert(d, comparer)
				.Insert(e, comparer)
				.Insert(f, comparer)
				.Insert(g, comparer)

				.Insert(b, comparer)
				.Insert(c, comparer)
				.Insert(d, comparer)
				.Insert(e, comparer)
				.Insert(f, comparer)
				.Insert(g, comparer);

			// Assert
			Assert.AreSame(d, result);
			Assert.AreSame(b, d.Left);
			Assert.AreSame(f, d.Right);
			Assert.AreSame(a, b.Left);
			Assert.AreSame(c, b.Right);
			Assert.AreSame(e, f.Left);
			Assert.AreSame(g, f.Right);

			Assert.AreEqual(2, result.Height);
			Assert.AreEqual(1, b.Height);
			Assert.AreEqual(1, f.Height);
			Assert.AreEqual(0, a.Height);
			Assert.AreEqual(0, c.Height);
			Assert.AreEqual(0, e.Height);
			Assert.AreEqual(0, g.Height);
		}

		[TestMethod()]
		public void RemoveTest()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };
			Node<int> h = new Node<int> { Value = 8 };
			Node<int> i = new Node<int> { Value = 9 };
			Node<int> j = new Node<int> { Value = 10 };
			Node<int> k = new Node<int> { Value = 11 };
			Node<int> l = new Node<int> { Value = 12 };
			Node<int> m = new Node<int> { Value = 13 };
			Node<int> n = new Node<int> { Value = 14 };
			Node<int> o = new Node<int> { Value = 15 };

			b.Left = a;
			b.Right = c;
			f.Left = e;
			f.Right = g;
			j.Left = i;
			j.Right = k;
			n.Left = m;
			n.Right = o;
			d.Left = b;
			d.Right = f;			
			l.Left = j;
			l.Right =n;
			h.Left = d;
			h.Right = l;

			a.Height = 0;
			c.Height = 0;
			e.Height = 0;
			g.Height = 0;
			i.Height = 0;
			k.Height = 0;
			m.Height = 0;
			o.Height = 0;			
			b.Height = 1;
			f.Height = 1;
			j.Height = 1;
			n.Height = 1;			
			d.Height = 2;
			l.Height = 2;			
			h.Height = 3;

			IComparer comparer = (new Comparer<int>());

			int expectedCount = 12;

			// Act
			Node<int> result =
				h
				.Remove(8, comparer)
				.Remove(9, comparer)
				.Remove(10, comparer);

			int actualCount = result.Traverse().Count();

			// Assert
			Assert.AreSame(k, result);
			Assert.AreEqual(expectedCount, actualCount);
		}
	}
}
