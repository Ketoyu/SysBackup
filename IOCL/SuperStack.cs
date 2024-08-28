using System.Collections.Generic;

namespace IOCL {
    public sealed class SuperStack<T>
	{
		private Stack<Stack<T>> _parent = new();
		private Stack<T> _child = new();

		public SuperStack() {}
		public void Push(T obj)
		{
			if (_child.Count < int.MaxValue)
				_child.Push(obj);
			else {
				_parent.Push(_child);
				_child = new Stack<T>();
				_child.Push(obj);
			}
		}
		public T Pop()
		{
				if (!_child.Any()) {
					if (!_parent.Any())
						return default;
					else
						_child = _parent.Pop();
				}

				return _child.Pop();
		}
		public bool IsEmpty()
		{
			return !_child.Any() && !_parent.Any();
		}
	}
}
