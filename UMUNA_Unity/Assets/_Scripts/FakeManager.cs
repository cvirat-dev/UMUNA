using UnityEngine;
using UUP.Persistence;

namespace UMUNA
{
    public class FakeManager : PersistentSingleton<FakeManager>
    {
        public void DebugMe()
        {
            Debug.Log("GameManager is here!");
        }
    }
}
