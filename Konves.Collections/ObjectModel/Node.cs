namespace Konves.Collections.ObjectModel
{
	/// <summary>
	/// Represents a node in a binary tree.
	/// </summary>
	/// <typeparam name="T">The type of the node's value.</typeparam>
	public class Node<T>
	{
		/// <summary>
		/// This node's left child node, or null.
		/// </summary>
		public Node<T> Left;
		/// <summary>
		/// This node's right child node, or null.
		/// </summary>
		public Node<T> Right;
		/// <summary>
		/// The hight of this node.  A value of 0 indicates that it is a leaf.
		/// </summary>
		public int Height;
		/// <summary>
		/// This node's value.
		/// </summary>
		public T Value;
	}
}
