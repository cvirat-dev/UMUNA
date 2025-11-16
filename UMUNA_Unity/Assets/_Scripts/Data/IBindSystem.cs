using System;
using UMUNA.SavingSystem;
using UnityEngine;

namespace UMUNA
{
    public interface IBindSystem
    {
        void Bind<TBinder, TData>(TData data, out TBinder binder, Action onBindingCompleted = null) where TBinder : MonoBehaviour, IBind<TData>;
    }
}