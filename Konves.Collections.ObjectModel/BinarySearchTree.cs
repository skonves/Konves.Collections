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
					return root.Left.Insert(node, comparer);
			else if (c > 0)
				if (ReferenceEquals(root.Right, null))
					root.Right = node;
				else
					return root.Right.Insert(node, comparer);
			else
				return root; // node already exists. don't insert, but still return existing root

			// TODO: balance and return new root instead.
			return root;
		}
		
		/// <summary>
		/// Removes a node matching the specified query and returns the resulting tree's root node.
		/// </summary>
		public static INode<T> Remove<T>(this INode<T> root, object value, IComparer comparer, bool favorLeft)
		{
			if (ReferenceEquals(root, null))
				return root; // node is not found. don't remove, but still return existing root

			int c = -comparer.Compare(root.Value, value);

			if (c < 0)
			{
				if (ReferenceEquals(root.Left, null))
					return root; // node is not found. don't remove, but still return existing root

				else if (ReferenceEquals(root.Left.Left, null) && ReferenceEquals(root.Left.Right, null)) // Delete leaf
					root.Left = null;

				else if (ReferenceEquals(root.Left.Left, null)) // Delete left and replace with a single grandchild
					root.Left = root.Left.Right;
				else if (ReferenceEquals(root.Left.Right, null))
					root.Left = root.Left.Left;

				else if (comparer.Compare(root.Left.Value, value) == 0)
					RemoveLeft(root, comparer, favorLeft); // Delete with children

				else
					return root.Left.Remove(value, comparer, favorLeft); // proceed to left

				// TODO: balance and return new root instead.
				return root;
			}
			if (c > 0)
			{
				if (ReferenceEquals(root.Right, null))
					return root;

				else if (ReferenceEquals(root.Right.Right, null) && ReferenceEquals(root.Right.Left, null)) // Delete leaf
					root.Right = null;

				else if (ReferenceEquals(root.Right.Right, null)) // Delete right and replace with a single grandchild
					root.Right = root.Right.Left;
				else if (ReferenceEquals(root.Right.Left, null))
					root.Right = root.Right.Right;

				else if (comparer.Compare(root.Right.Value, value) == 0)
					RemoveRight(root, comparer, favorLeft); // Delete with children

				else
					return root.Right.Remove(value, comparer, favorLeft); // proceed to right

				return root;
			}

			// delete root
			throw new NotImplementedException();
		}

		// may break if root has no children
		public static void RemoveLeft<T>(INode<T> root, IComparer comparer, bool favorLeft)
		{
			INode<T> delete = root.Left;

			if(favorLeft)
			{
				INode<T> deleteIOPParent = null;
				INode<T> deleteIOP = null;

				for (INode<T> n = delete.Left; ; n = n.Right)
				{
					if (ReferenceEquals(n.Right, null))
					{
						deleteIOP = n;
						break;
					}

					if (ReferenceEquals(n.Right.Right, null))
					{
						deleteIOPParent = n;
						deleteIOP = n.Right;
						break;
					}
				}

				root.Left = deleteIOP;
				deleteIOP.Right = delete.Right;
				if(!ReferenceEquals(deleteIOPParent,null))
					deleteIOPParent.Right = deleteIOP.Left;
				deleteIOP.Left = delete.Left;
				delete.Right = null;
				delete.Left = null;
			}
			else
			{
				INode<T> deleteIOSParent = null;
				INode<T> deleteIOS = null;

				for (INode<T> n = delete.Right; ; n = n.Left)
				{
					if (ReferenceEquals(n.Left, null))
					{
						deleteIOS = n;
						break;
					}

					if (ReferenceEquals(n.Left.Left, null))
					{
						deleteIOSParent = n;
						deleteIOS = n.Left;
						break;
					}
				}

				root.Left = deleteIOS;
				deleteIOS.Left = delete.Left;
				if (!ReferenceEquals(deleteIOSParent, null))
					deleteIOSParent.Left = deleteIOS.Right;
				deleteIOS.Right = delete.Right;
				delete.Right = null;
				delete.Left = null;
			}
		}

		// may break if root has no children
		public static void RemoveRight<T>(INode<T> root, IComparer comparer, bool favorLeft)
		{
			INode<T> delete = root.Right;

			if (favorLeft)
			{
				INode<T> deleteIOPParent = null;
				INode<T> deleteIOP = null;

				for (INode<T> n = delete.Left; ; n = n.Right)
				{
					if (ReferenceEquals(n.Right, null))
					{
						deleteIOP = n;
						break;
					}

					if (ReferenceEquals(n.Right.Right, null))
					{
						deleteIOPParent = n;
						deleteIOP = n.Right;
						break;
					}
				}

				root.Right = deleteIOP;
				deleteIOP.Right = delete.Right;
				if (!ReferenceEquals(deleteIOPParent, null))
					deleteIOPParent.Right = deleteIOP.Left;
				deleteIOP.Left = delete.Left;
				delete.Right = null;
				delete.Left = null;
			}
			else
			{
				INode<T> deleteIOSParent = null;
				INode<T> deleteIOS = null;

				for (INode<T> n = delete.Right; ; n = n.Left)
				{
					if (ReferenceEquals(n.Left, null))
					{
						deleteIOS = n;
						break;
					}

					if (ReferenceEquals(n.Left.Left, null))
					{
						deleteIOSParent = n;
						deleteIOS = n.Left;
						break;
					}
				}

				root.Right = deleteIOS;
				deleteIOS.Left = delete.Left;
				if (!ReferenceEquals(deleteIOSParent, null))
					deleteIOSParent.Left = deleteIOS.Right;
				deleteIOS.Right = delete.Right;
				delete.Right = null;
				delete.Left = null;
			}
		}
	}
}
