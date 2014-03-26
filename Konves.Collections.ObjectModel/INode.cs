using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konves.Collections.ObjectModel
{
	public interface INode<T> where T : IComparable, IComparable<T>
	{
		INode<T> Left { get; set; }
		INode<T> Right { get; set; }
		T Value { get; set; }
	}
}
