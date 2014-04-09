using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konves.Collections.ObjectModel
{
	public class Node<T>
	{
		public Node<T> Left;
		public Node<T> Right;
		public int Height;
		public T Value;
	}
}
