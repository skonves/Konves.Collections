using System;
using System.Collections.Generic;
using System.Collections;

namespace Konves.Collections.ObjectModel
{
	/// <summary>
	/// Provides functionality for manipulating binary tree nodes.  All operations are self-balancing based on an AVL tree.
	/// </summary>
	public static class BinarySearchTree
	{
		/// <summary>
		/// Performs a lazy, depth-first traversal of the subtree defined by the current root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
		/// <param name="root">The root node.</param>
		/// <returns>Returns an enumeration of all of the subtree's nodes, including the root node.</returns>
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
		/// Searches for a value within the current node and its subtree.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
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
		/// Inserts a node to into the subtree with the current root and returns the new root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
		/// <param name="root">The original root node.</param>
		/// <param name="node">The node to insert.</param>
		/// <param name="comparer">An instance of an object to compare the value of the nodes in the subtree.</param>
		/// <param name="success">An output value indicating whether or not the new node was successfully inserted.</param>
		/// <returns>Returns the subtree's new root node.</returns>
		public static Node<T> Insert<T>(this Node<T> root, Node<T> node, IComparer<T> comparer, out bool success)
		{
			if (ReferenceEquals(root, null))
			{
				success = true;
				return node;
			}

			int c = comparer.Compare(node.Value, root.Value);

			if (c < 0)
			{
				if (ReferenceEquals(root.Left, null))
				{
					success = true;
					root.Left = node;
					root.Left.Height = 0;
				}
				else
				{
					root.Left = root.Left.Insert(node, comparer, out success);
				}
			}
			else if (c > 0)
			{
				if (ReferenceEquals(root.Right, null))
				{
					success = true;
					root.Right = node;
					root.Right.Height = 0;
				}
				else
				{
					root.Right = root.Right.Insert(node, comparer, out success);
				}
			}
			else
			{
				success = false;
			}

			return root.UpdateHeight().Balance();
		}

		public static Node<T> Remove<T>(this Node<T> root, object value, IComparer comparer, out Node<T> removed)
		{
			if (ReferenceEquals(root, null))
			{
				removed = null;
				return null;
			}

			int c = comparer.Compare(value, root.Value);

			if (c < 0)
			{
				// Remove to the left
				root.Left = root.Left.Remove(value, comparer, out removed);
				return root.UpdateHeight().Balance();
			}
			else if (c > 0)
			{
				// Remove to the right
				root.Right = root.Right.Remove(value, comparer, out removed);
				return root.UpdateHeight().Balance();
			}
			else
			{
				// Remove root and deal with children
				if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Right, null)) 
				{
					// Leaf
					removed = root;
					return null;
				}
				else if (ReferenceEquals(root.Left, null))
				{
					// one subtree to the right
					Node<T> node = root.Right;
					removed = root.Clear();
					return node;
				}
				else if (ReferenceEquals(root.Right, null))
				{
					// one subtree to the left
					Node<T> node = root.Left;
					removed = root.Clear();
					return node;
				}
				else if (root.GetBalanceFactor() > 0)
				{
					// two subtrees, left is taller
					Node<T> inOrderPredecessor;
					Node<T> newLeft = root.Left.RemoveLast(out inOrderPredecessor);

					inOrderPredecessor.Left = newLeft;
					inOrderPredecessor.Right = root.Right;

					removed = root.Clear();

					return inOrderPredecessor;
				}
				else
				{
					// two subtrees, same height or right is taller
					Node<T> inOrderSucessor;
					Node<T> newRight = root.Right.RemoveFirst(out inOrderSucessor);

					inOrderSucessor.Left = root.Left;
					inOrderSucessor.Right = newRight;

					removed = root.Clear();

					return inOrderSucessor;
				}
			}
		}

		/// <summary>
		/// Removes the first node of the current root and returns the new root.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
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
		/// Rotates the current node to the right and returns the pivot node (original the root's left node) as the new root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
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
		/// Rotates the current node to the left and returns the pivot node (original the root's right node) as the new root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
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

		/// <summary>
		/// Balances the current node and returns the new root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
		/// <param name="root">The original root node.</param>
		/// <returns>Returns the new root node.</returns>
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

		/// <summary>
		/// Updates the height property of the current root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
		/// <param name="root">The root node.</param>
		/// <returns>Returns the updated root node.</returns>
		public static Node<T> UpdateHeight<T>(this Node<T> root)
		{
			if (ReferenceEquals(root, null))
				return null;

			root.Height = root.GetHeight();
			return root;
		}

		/// <summary>
		/// Gets the height of the current root node based on the height of its children.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
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

		/// <summary>
		/// Clears all child and height properties on the current root node.
		/// </summary>
		/// <typeparam name="T">The type of the current tree's node values.</typeparam>
		/// <param name="root">The root node.</param>
		/// <returns>Returns the updated root node.</returns>
		public static Node<T> Clear<T>(this Node<T> root)
		{
			root.Right = null;
			root.Left = null;
			root.Height = 0;
			return root;
		}
	}
}
