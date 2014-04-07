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
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			b.Left = a;
			f.Left = e;
			f.Right = g;
			d.Left = b;
			d.Right = f;

			Node<int> root = d;
			Node<int> c = new Node<int> { Value = 3 };
			IComparer comparer = (new Comparer<int>());

			Node<int> expected = root;

			// Act
			Node<int> result = root.Insert(c, comparer);

			// Assert
			Assert.AreSame(expected, result);
			Assert.AreSame(b.Right, c);
		}

		[TestMethod()]
		public void InsertTest_Duplicate()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> d = new Node<int> { Value = 4 };
			Node<int> e = new Node<int> { Value = 5 };
			Node<int> f = new Node<int> { Value = 6 };
			Node<int> g = new Node<int> { Value = 7 };

			b.Left = a;
			f.Left = e;
			f.Right = g;
			d.Left = b;
			d.Right = f;

			Node<int> root = d;
			Node<int> c = new Node<int> { Value = 6 };
			IComparer comparer = (new Comparer<int>());

			Node<int> expected = root;

			// Act
			Node<int> result = root.Insert(c, comparer);

			// Assert
			Assert.AreSame(expected, result);
		}

		[TestMethod()]
		public void Remove_Leaf()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };

			Node<int> root = a;
			IComparer comparer = (new Comparer<int>());
			Node<int> expected = null;

			// Act
			Node<int> result = root.Remove(1, comparer, true);

			// Assert
			Assert.AreSame(expected, result);			
		}

		[TestMethod()]
		public void Remove_LeftBranch()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };

			b.Left = a;

			Node<int> root = b;
			IComparer comparer = (new Comparer<int>());
			Node<int> expected = a;

			// Act
			Node<int> result = root.Remove(b.Value, comparer, true);

			// Assert
			Assert.AreSame(expected, result);
		}

		[TestMethod()]
		public void Remove_RightBranch()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };

			a.Right = b;

			Node<int> root = a;
			IComparer comparer = (new Comparer<int>());
			Node<int> expected = b;

			// Act
			Node<int> result = root.Remove(a.Value, comparer, true);

			// Assert
			Assert.AreSame(expected, result);
		}

		[TestMethod()]
		public void Remove_FavorLeft()
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

			d.Left = c;
			d.Right = e;
			j.Left = i;
			j.Right = k;

			f.Left = d;
			h.Right = j;

			b.Left = a;
			b.Right = f;
			l.Left = h;
			l.Right = m;

			g.Left = b;
			g.Right = l;

			Node<int> root = g;
			IComparer comparer = (new Comparer<int>());
			Node<int> expected = f;

			// Act
			Node<int> result = root.Remove(root.Value, comparer, true);

			// Assert
			Assert.AreSame(expected, result);
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
	}
}
