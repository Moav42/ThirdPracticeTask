using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public delegate void BinaryTreHandler(string message);
        public event BinaryTreHandler AddingTreNode;
        public event BinaryTreHandler RemovingTreNode;

        private BinaryTreeNode<T> _head;
        public BinaryTreeNode<T> Head { get { return _head; } }
        private int _count;
        public int Count { get { return _count; } }

        public void Add(T value)
        {           
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
                AddingTreNode?.Invoke("Root  added to Binary Tree");
            }        
            else
            {
                AddTo(_head, value);
                AddingTreNode?.Invoke("Child node added to Binary Tree");
            }
            _count++;
        }

        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {  
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {            
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }
        public void AddRange(params T[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                this.Add(elements[i]); 
            }
        }
        public bool Contains(T value)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        { 
            BinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            _count--;

            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {         
                        parent.Right = current.Left;

                    }
                }
            }

            else if (current.Right.Left == null) 
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }

            else
            {
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)

                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
  
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftmost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }
            RemovingTreNode?.Invoke($"Node with value {value.ToString()} removed from Binary Tree");
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
            //return PreOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> InOrderTraversal()
        {

            if (_head != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = _head; 

                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    { 
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }                
            }           
        }

        public IEnumerator<T> PreOrderTraversal()
        {
            if (_head != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = _head;
                bool goLeftNext = true;

                stack.Push(current);
                yield return current.Value;

                while (stack.Count > 0)
                {                  
                    if (goLeftNext)
                    {                       
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                            yield return current.Value;
                        }
                    }

                    if (current.Right != null)
                    {
                        current = current.Right;
                        yield return current.Value;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

    }
}
