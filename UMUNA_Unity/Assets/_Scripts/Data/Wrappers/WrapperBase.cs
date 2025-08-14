
using System;

namespace UMUNA.Data.Wrappers
{
    public class WrapperBase<T> where T : class
    {
        public event Action OnValueChanged;
        private T _model;

        public T Model
        {
            get => _model;
            set
            {
                if (!Equals(_model, value))
                {
                    _model = value;
                    OnValueChanged?.Invoke();
                }
            }
        }

        public WrapperBase(T obj)
        {
            _model = obj;
        }

        public void NotifyChange() => OnValueChanged?.Invoke();
    }
}
