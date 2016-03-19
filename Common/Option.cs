using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class Option<T> : IEnumerable<T>
    {
        private readonly T[] _data;

        private Option(T[] data)
        {
            _data = data;
        }

        public static Option<T> Some(T value)
        {
            return new Option<T>(new T[] { value });
        }

        public static Option<T> None()
        {
            return new Option<T>(new T[0]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
