using System;
namespace HyperCasualMatchGame
{
    public class WatchedVariable<T>
    {
        private T _value;
        public event Action<T> ValueChanged;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
    }
}