using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenOneDArray
{
    public class GenOneDArray<T> where T : IComparable
    {
        private T[] _elements;
        public T[] Elements { get { return _elements; } }
        public int StarterIndex { get; }

        public GenOneDArray(int startIndex, int numberOfEl)
        {
            _elements = new T[numberOfEl];
            StarterIndex = startIndex;
        }
        public GenOneDArray(int startIndex, T[] valuesAr)
        {       
            _elements = valuesAr;
            StarterIndex = startIndex;
        }
        public T this[int index]
        {
            get
            {
                if ((index - StarterIndex) < 0)
                {
                    throw new IndexOutOfRangeException($"Index Out Of Range, first index of this Array is {StarterIndex}.");
                }
                return _elements[index - StarterIndex];
            }
            set
            {
                if ((index - StarterIndex) < 0)
                {
                    throw new IndexOutOfRangeException($"Index Out Of Range, first index of this Array is {StarterIndex}.");
                }
                _elements[index - StarterIndex] = value;
            }
        }
        public int IndexOf(T el)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i].CompareTo(el) == 0)
                {
                    return i + StarterIndex;
                }
            }
            return -1;
        }
    
        //Implementation of IEnumerable and IEnumerator
        int position = -1;

        public T Current
        {
            get { return _elements[position]; }
        }

        public GenOneDArray<T> GetEnumerator()
        {
            return this;
        }

        void Dispose()
        {
            this.Reset();
        }

        public bool MoveNext()
        {
            if (position < _elements.Length - 1)
            {
                position++;
                return true;
            }
            Reset();
            return false;
        }

        void Reset()
        {
            position = -1;
        }
    }
   
}
