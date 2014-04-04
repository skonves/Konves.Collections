using System;
using System.Collections.Generic;
using System.Collections;

namespace Konves.Collections.ObjectModel
{
	public static class BinarySearchTree
	{
		public static IEnumerable<INode<T>> Traverse<T>(this INode<T> root)
		{
			if (ReferenceEquals(root, null))
				yield break;

			foreach (INode<T> child in root.Left.Traverse())
			    yield return child;

			yield return root;

			foreach (INode<T> child in root.Right.Traverse())
			    yield return child;
		}

		/// <summary>
		/// Searches for a value within the specified node and its subtree.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root node of the subtree to search.</param>
		/// <param name="value">The value used to specify the node for which to search.</param>
		/// <param name="comparer">An instance of a object to compare the value of the nodes in the subtree.</param>
		/// <returns>The node whose key matches the query value</returns>
		public static INode<T> Search<T>(this INode<T> root, object value, IComparer comparer)
		{
			if (ReferenceEquals(root, null))
				return null;

			int c = -comparer.Compare(root.Value, value);

			if (c < 0)
				return root.Left.Search(value, comparer);

			if (c > 0)
				return root.Right.Search(value, comparer);

			return root;
		}

		/// <summary>
		/// Inserts a node to into the subtree with the specified root and returns the new root node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node">The node to insert.</param>
		/// <param name="root">The original root node.</param>
		/// <param name="comparer">An instance of a object to compare the value of the nodes in the subtree.</param>
		/// <returns>Returns the subtree's new root node.</returns>
		public static INode<T> Insert<T>(this INode<T> root, INode<T> node, IComparer comparer)
		{
			if (ReferenceEquals(root, null))
				return node;

			int c = -comparer.Compare(root.Value, node.Value);

			if (c < 0)
				if (ReferenceEquals(root.Left, null))
					root.Left = node;
				else
					root.Left = root.Left.Insert(node, comparer);
			else if (c > 0)
				if (ReferenceEquals(root.Right, null))
					root.Right = node;
				else
					root.Right = root.Right.Insert(node, comparer);

			// TODO: balance and return new root instead.
			return root;
		}

		public static INode<T> Remove<T>(this INode<T> root, object value, IComparer comparer, bool favorLeft)
		{
			if (ReferenceEquals(root, null))
				return root; // node is not found. don't remove, but still return existing root

			int c = -comparer.Compare(root.Value, value);

			if (c < 0) // to the left, to the left
			{
				root.Left = root.Left.Remove(value, comparer, favorLeft);
				return root;
			}
			else if (c > 0)
			{
				root.Right = root.Right.Remove(value, comparer, favorLeft);
				return root;
			}
			else if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Right, null)) // Leaf
			{
				return null;
			}
			else if (ReferenceEquals(root.Left, null)) // one child: Right
			{
				INode<T> node = root.Right;
				root.Right = null;
				return node;
			}
			else if (ReferenceEquals(root.Right, null)) // one child: Left
			{
				INode<T> node = root.Left;
				root.Left = null;
				return node;
			}
			else if (favorLeft)
			{
				INode<T> iop, iopp = null;
				for (iop = root.Left; !ReferenceEquals(iop.Right, null); iopp = iop, iop = iop.Right) { }

				iop.Right = root.Right;
				if (!ReferenceEquals(iopp, null))
					iopp.Right = iop.Left;
				iop.Left = root.Left;
				root.Left = null;
				root.Right = null;
				 return iop;
			}
			else
			{
				INode<T> ios, iosp = null;
				for (ios = root.Right; !ReferenceEquals(ios.Left, null); iosp = ios, ios = ios.Left) { }

				ios.Left = root.Left;
				if (!ReferenceEquals(iosp, null))
					iosp.Left = ios.Right;
				ios.Right = root.Right;
				root.Left = null;
				root.Right = null;
				return ios;
			}
		}

		/// <summary>
		/// Rotates the node to the right and returns the pivot node (original the root's left node) as the new root node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The original root node.</param>
		/// <returns>Returns the new root node</returns>
		public static INode<T> RotateRight<T>(this INode<T> root)
		{
			INode<T> pivot = root.Left;

			root.Left = pivot.Right;
			pivot.Right = root;

			return pivot;
		}

		/// <summary>
		/// Rotates the node to the left and returns the pivot node (original the root's right node) as the new root node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The original root node.</param>
		/// <returns>Returns the new root node</returns>
		public static INode<T> RotateLeft<T>(this INode<T> root)
		{
			INode<T> pivot = root.Right;

			root.Right = pivot.Left;
			pivot.Left = root;

			return pivot;
		}
	}
}
