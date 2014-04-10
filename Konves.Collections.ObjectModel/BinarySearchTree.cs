using System;
using System.Collections.Generic;
using System.Collections;

namespace Konves.Collections.ObjectModel
{
	public static class BinarySearchTree
	{
		public static IEnumerable<Node<T>> Traverse<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				yield break;

			foreach (Node<T> child in root.Left.Traverse())
			    yield return child;

			yield return root;

			foreach (Node<T> child in root.Right.Traverse())
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
		public static Node<T> Search<T>(this Node<T> root, object value, IComparer comparer)
		{
			if (ReferenceEquals(root, null))
				return null;

			int c = comparer.Compare(value, root.Value);

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
		public static Node<T> Insert<T>(this Node<T> root, Node<T> node, IComparer comparer)
		{
			if (ReferenceEquals(root, null))
				return node;

			int c = comparer.Compare(node.Value, root.Value);

			if (c < 0)
			{
				if (ReferenceEquals(root.Left, null))
				{
					root.Left = node;
					root.Left.Height = 0;
				}
				else
				{
					root.Left = root.Left.Insert(node, comparer);
				}
			}
			else if (c > 0)
			{
				if (ReferenceEquals(root.Right, null))
				{
					root.Right = node;
					root.Right.Height = 0;
				}
				else
				{
					root.Right = root.Right.Insert(node, comparer);
				}
			}

			root.Height = root.GetHeight();

			return root.Balance();
		}

		public static Node<T> Remove<T>(this Node<T> root, object value, IComparer comparer)
		{
			if (ReferenceEquals(root, null))
				return null;

			int c = comparer.Compare(value, root.Value);

			if (c < 0)
			{
				// Remove to the left
				root.Left = root.Left.Remove(value, comparer);
				root.Height = root.GetHeight();
				return root;
			}
			else if (c > 0)
			{
				// Remove to the right
				root.Right = root.Right.Remove(value, comparer);
				root.Height = root.GetHeight();
				return root;
			}
			else
			{
				// Remove root and deal with children
				if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Right, null)) 
				{
					// Leaf
					return null;
				}
				else if (ReferenceEquals(root.Left, null))
				{
					// one subtree to the right
					Node<T> node = root.Right;					
					root.Right = null;
					return node;
				}
				else if (ReferenceEquals(root.Right, null))
				{
					// one subtree to the left
					Node<T> node = root.Left;
					root.Left = null;
					return node;
				}
				else if (root.GetBalanceFactor() > 0)
				{
					// two subtrees, left is taller
					Node<T> inOrderPredecessor;
					Node<T> newLeft = root.Left.RemoveLast(out inOrderPredecessor);

					inOrderPredecessor.Left = newLeft;
					inOrderPredecessor.Right = root.Right;

					root.Clear();

					return inOrderPredecessor;
				}
				else
				{
					// two subtrees, same height or right is taller
					Node<T> inOrderSucessor;
					Node<T> newRight = root.Right.RemoveFirst(out inOrderSucessor);

					inOrderSucessor.Left = root.Left;
					inOrderSucessor.Right = newRight;

					root.Clear();

					return inOrderSucessor;
				}
			}
		}

		/// <summary>
		/// Removes the first node and returns the new root.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root.</param>
		/// <param name="removed">The removed node.</param>
		/// <returns>Returns the new root node.</returns>
		public static Node<T> RemoveFirst<T>(this Node<T> root, out Node<T> removed)
		{
			if (!ReferenceEquals(root.Left, null))
			{
				root.Left = root.Left.RemoveFirst(out removed).UpdateHeight().Balance();
				return root.UpdateHeight().Balance();
			}

			Node<T> newRoot = root.Right;
			removed = root.Clear();
			return newRoot;
		}

		public static Node<T> RemoveLast<T>(this Node<T> root, out Node<T> removed)
		{
			if (!ReferenceEquals(root.Left, null))
			{
				root.Right = root.Right.RemoveLast(out removed).UpdateHeight().Balance();
				return root.UpdateHeight().Balance();
			}

			Node<T> newRoot = root.Left;
			removed = root.Clear();
			return newRoot;
		}

		/// <summary>
		/// Rotates the node to the right and returns the pivot node (original the root's left node) as the new root node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The original root node.</param>
		/// <returns>Returns the new root node</returns>
		public static Node<T> RotateRight<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return null;

			Node<T> pivot = root.Left;

			root.Left = pivot.Right;
			pivot.Right = root;

			root.Height = root.GetHeight();
			pivot.Height = pivot.GetHeight();

			return pivot;
		}

		/// <summary>
		/// Rotates the node to the left and returns the pivot node (original the root's right node) as the new root node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The original root node.</param>
		/// <returns>Returns the new root node</returns>
		public static Node<T> RotateLeft<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return null;

			Node<T> pivot = root.Right;

			root.Right = pivot.Left;
			pivot.Left = root;

			root.Height = root.GetHeight();
			pivot.Height = pivot.GetHeight();

			return pivot;
		}

		public static Node<T> Balance<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return null;

			int rootBalanceFactor = root.GetBalanceFactor();

			if (rootBalanceFactor < -1)
			{
				if (root.Right.GetBalanceFactor() > 0)
					root.Right = root.Right.RotateRight(); // Right-Left case
				return root.RotateLeft(); // Right-Right case
			}
			else if (rootBalanceFactor > 1)
			{
				if (root.Left.GetBalanceFactor() < 0)
					root.Left = root.Left.RotateLeft(); // Left-Right case
				return root.RotateRight(); // Left-Left case
			}
			else
			{
				return root;
			}
		}

		public static Node<T> UpdateHeight<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return null;

			root.Height = root.GetHeight();
			return root;
		}

		/// <summary>
		/// Gets the height of the specified root node based on the height of its children.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root node.</param>
		/// <returns>The hight of the specified node.</returns>
		public static int GetHeight<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return -1;
			else if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Right, null))
				return 0;
			else if (ReferenceEquals(root.Left, null))
				return root.Right.Height + 1;
			else if (ReferenceEquals(root.Right, null))
				return root.Left.Height + 1;
			else
				return Math.Max(root.Left.Height, root.Right.Height) + 1;			
		}

		public static int GetBalanceFactor<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return 0;
			else if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Right, null))
				return 0;
			else if (ReferenceEquals(root.Left, null))
				return -1 - root.Right.Height;
			else if (ReferenceEquals(root.Right, null))
				return root.Left.Height + 1;
			else
				return root.Left.Height - root.Right.Height;
		}

		public static Node<T> Clear<T>(this Node<T> root)
		{
			root.Right = null;
			root.Left = null;
			root.Height = 0;
			return root;
		}
	}
}
