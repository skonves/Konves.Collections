using System;
using System.Collections.Generic;

namespace Konves.Collections.ObjectModel
{
	public static class BinarySearchTree
	{
		public static IEnumerable<INode<T>> Traverse<T>(this INode<T> root) where T : IComparable, IComparable<T>
		{
			if (ReferenceEquals(root, null))
				yield break;

			foreach (INode<T> child in root.Left.Traverse())
			    yield return child;

			yield return root;

			foreach (INode<T> child in root.Right.Traverse())
			    yield return child;
		}

		public static INode<T> Search<T>(this INode<T> root, object query) where T : IComparable, IComparable<T>
		{
			if (ReferenceEquals(root, null))
				return null;

			int c = -root.Value.CompareTo(query);

			if (c < 0)
				return root.Left.Search(query);

			if (c > 0)
				return root.Right.Search(query);

			return root;
		}

		public static bool Insert<T>(this INode<T> root, INode<T> node) where T : IComparable, IComparable<T>
		{
			if (ReferenceEquals(root, null))
				return false;

			int c = -root.Value.CompareTo(node.Value);

			if (c < 0)
				if (ReferenceEquals(root.Left, null))
					root.Left = node;
				else
					return root.Left.Insert(node);
			else if (c > 0)
				if (ReferenceEquals(root.Right, null))
					root.Right = node;
				else
					return root.Right.Insert(node);
			else
				return false;

			return true;
		}

		// TODO: return new root node
		public static bool Remove<T>(this INode<T> root, object query, bool favorLeft) where T : IComparable, IComparable<T>
		{
			if (ReferenceEquals(root, null))
				return false;

			int c = -root.Value.CompareTo(query);

			if (c < 0)
			{
				if (ReferenceEquals(root.Left, null))
					return false;

				else if (ReferenceEquals(root.Left.Left, null) && ReferenceEquals(root.Left.Right, null)) // Delete leaf
					root.Left = null;

				else if (ReferenceEquals(root.Left.Left, null)) // Delete left and replace with a single grandchild
					root.Left = root.Left.Right;
				else if (ReferenceEquals(root.Left.Right, null))
					root.Left = root.Left.Left;

				else if (root.Left.Value.CompareTo(query) == 0)
					RemoveLeft(root, favorLeft); // Delete with children

				else
					return root.Left.Remove(query, favorLeft); // proceed to left

				return true;
			}
			if (c > 0)
			{
				if (ReferenceEquals(root.Right, null))
					return false;

				else if (ReferenceEquals(root.Right.Right, null) && ReferenceEquals(root.Right.Left, null)) // Delete leaf
					root.Right = null;

				else if (ReferenceEquals(root.Right.Right, null)) // Delete right and replace with a single grandchild
					root.Right = root.Right.Left;
				else if (ReferenceEquals(root.Right.Left, null))
					root.Right = root.Right.Right;

				else if (root.Right.Value.CompareTo(query) == 0)
					RemoveRight(root, favorLeft); // Delete with children

				else
					return root.Right.Remove(query, favorLeft); // proceed to right

				return true;
			}

			// delete root
			throw new NotImplementedException();
		}

		// may break if root has no children
		public static void RemoveLeft<T>(INode<T> root, bool favorLeft) where T : IComparable, IComparable<T>
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
		public static void RemoveRight<T>(INode<T> root, bool favorLeft) where T : IComparable, IComparable<T>
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
