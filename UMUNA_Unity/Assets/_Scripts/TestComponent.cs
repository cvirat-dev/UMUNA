using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;

namespace UMUNA
{
    public class TestComponent : MonoBehaviourT
    {
        [InspectorButton]
        public void TestManager()
        {
            FakeManager.Instance.DebugMe();
        }
    }
}
