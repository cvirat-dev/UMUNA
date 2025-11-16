using System;
using UMUNA.SavingSystem;
using UMUNA.Singletons;
using UMUNA.Utils;
using UnityEngine;

namespace UMUNA
{
    public class BindSystem : IBindSystem
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public void Bind<TBinder, TData>(TData data, out TBinder binder, Action onBindingCompleted = null) where TBinder : MonoBehaviour, IBind<TData>
        {
            binder = BindingLocator.Instance.GetComponentInChildren<TBinder>();
            if (binder != null)
            {
                Debug.Log($"Binding {typeof(TBinder)} with {typeof(TData)}");
                binder.Bind(data);
                return;
            }

            // If the binder is not found in children of BindingLocator, try to find it in the scene
            binder = SceneHelper.Instance.FindMonoBehaviour<TBinder>();
            if (binder == null)
                throw new System.Exception($"No {typeof(TBinder)} found in children of {nameof(BindingLocator)} or in scene");

            Debug.LogWarning($"No {typeof(TBinder)} found in children of {nameof(BindingLocator)} but found in scene");
            binder.Bind(data);

            onBindingCompleted?.Invoke();
        }
        #endregion
    }
}
