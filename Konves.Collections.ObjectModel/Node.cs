using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konves.Collections.ObjectModel
{
	public class Node<T> : INode<T>
	{
		public INode<T> Left { get; set; }

		public INode<T> Right { get; set; }

		public T Value { get; set; }
	}
}
