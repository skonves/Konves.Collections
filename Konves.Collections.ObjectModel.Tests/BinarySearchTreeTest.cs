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

			INode<int> expected = root;

			// Act
			INode<int> result = root.Insert(c, comparer);

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

			INode<int> expected = root;

			// Act
			INode<int> result = root.Insert(c, comparer);

			// Assert
			Assert.AreSame(expected, result);
		}

		[TestMethod()]
		public void RemoveLeftTest_FavorLeft_Large()
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

			e.Left = d;
			e.Right = f;
			g.Left = e;
			c.Right = g;
			b.Left = a;
			b.Right = c;
			j.Left = i;
			j.Right = k;
			h.Left = b;
			h.Right = j;
			n.Left = m;
			n.Right = o;
			l.Left = h;
			l.Right = n;

			Node<int> root = l;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveLeft(root, comparer, true);

			// Assert
			Assert.AreSame(l.Left, g);
			Assert.AreSame(g.Left, b);
			Assert.AreSame(g.Right, j);
			Assert.AreSame(c.Right, e);
			Assert.IsNull(h.Left);
			Assert.IsNull(h.Right);
		}

		[TestMethod()]
		public void RemoveLeftTest_FavorLeft_Small()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };

			b.Left = a;
			b.Right = c;
			d.Left = b;

			Node<int> root = d;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveLeft(root, comparer, true);

			// Assert
			Assert.AreSame(d.Left, a);
			Assert.AreSame(a.Right, c);
			Assert.IsNull(b.Left);
			Assert.IsNull(b.Right);
		}

		[TestMethod()]
		public void RemoveLeftTest_FavorRight_Large()
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

			g.Left = d;
			g.Right = h;
			e.Right = g;
			i.Left = e;
			b.Left = a;
			b.Right = c;
			j.Left = i;
			j.Right = k;
			d.Left = b;
			d.Right = j;
			n.Left = m;
			n.Right = o;
			l.Left = d;
			l.Right = n;

			Node<int> root = l;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveLeft(root, comparer, false);

			// Assert
			Assert.AreSame(l.Left, e);
			Assert.AreSame(e.Left, b);
			Assert.AreSame(e.Right, j);
			Assert.AreSame(i.Left, g);
			Assert.IsNull(d.Left);
			Assert.IsNull(d.Right);
		}

		[TestMethod()]
		public void RemoveLeftTest_FavorRight_Small()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };

			b.Left = a;
			b.Right = c;
			d.Left = b;

			Node<int> root = d;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveLeft(root, comparer, false);

			// Assert
			Assert.AreSame(d.Left, c);
			Assert.AreSame(c.Left, a);
			Assert.IsNull(b.Left);
			Assert.IsNull(b.Right);
		}


		[TestMethod()]
		public void RemoveRightTest_FavorLeft_Large()
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

			i.Left = h;
			i.Right = j;
			k.Left = i;
			g.Right = k;
			f.Left = e;
			f.Right = g;
			n.Left = m;
			n.Right = o;
			b.Left = a;
			b.Right = c;
			l.Left = f;
			l.Right = n;
			d.Left = b;
			d.Right = l;

			Node<int> root = d;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveRight(root, comparer, true);

			// Assert
			Assert.AreSame(d.Right, k);
			Assert.AreSame(k.Left, f);
			Assert.AreSame(k.Right, n);
			Assert.AreSame(g.Right, i);
			Assert.IsNull(l.Left);
			Assert.IsNull(l.Right);
		}

		[TestMethod()]
		public void RemoveRightTest_FavorLeft_Small()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };

			a.Right = c;
			c.Left = b;
			c.Right = d;

			Node<int> root = a;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveRight(root, comparer, true);

			// Assert
			Assert.AreSame(a.Right, b);
			Assert.AreSame(b.Right, d);
			Assert.IsNull(c.Left);
			Assert.IsNull(c.Right);
		}

		[TestMethod()]
		public void RemoveRightTest_FavorRight_Large()
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

			k.Left = j;
			k.Right = l;
			i.Right = k;
			m.Left = i;
			f.Left = e;
			f.Right = g;
			n.Left = m;
			n.Right = o;
			b.Left = a;
			b.Right = c;
			h.Left = f;
			h.Right = n;
			d.Left = b;
			d.Right = h;

			Node<int> root = d;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveRight(root, comparer, false);

			// Assert
			Assert.AreSame(d.Right, i);
			Assert.AreSame(i.Left, f);
			Assert.AreSame(i.Right, n);
			Assert.AreSame(m.Left, k);
			Assert.IsNull(h.Left);
			Assert.IsNull(h.Right);
		}

		[TestMethod()]
		public void RemoveRightTest_FavorRight_Small()
		{
			// Arrange
			Node<int> a = new Node<int> { Value = 1 };
			Node<int> b = new Node<int> { Value = 2 };
			Node<int> c = new Node<int> { Value = 3 };
			Node<int> d = new Node<int> { Value = 4 };

			a.Right = c;
			c.Left = b;
			c.Right = d;

			Node<int> root = a;
			IComparer comparer = (new Comparer<int>());

			// Act
			BinarySearchTree.RemoveRight(root, comparer, false);

			// Assert
			Assert.AreSame(a.Right, d);
			Assert.AreSame(d.Left, b);
			Assert.IsNull(c.Left);
			Assert.IsNull(c.Right);
		}
	}
}
