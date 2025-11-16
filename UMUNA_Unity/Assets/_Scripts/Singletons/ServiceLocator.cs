//using UMUNA.AppManagement;
//using UnityEngine;
//using UUP.Persistence;

//namespace UMUNA
//{
//    [DefaultExecutionOrder(-20)]
//    public class ServiceLocator : PersistentSingletonT<ServiceLocator>
//    {
//        private AppManager GlobalAppManager;

//        protected override void Awake()
//        {
//            base.Awake();
//            GlobalAppManager = new AppManager();
//            Debug.Log($"{nameof(ServiceLocator)}: {nameof(GlobalAppManager)} created.");
//        }

//        private void Start()
//        {
//            GlobalAppManager.Initialize();
//        }

//        public AppManager AppManager => GlobalAppManager ??= new AppManager();
//    }
//}
