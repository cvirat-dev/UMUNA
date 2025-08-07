using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;
using UnityEngine;

namespace UMUNA.ScriptableObjects.InspectorHelpers
{
    [CreateAssetMenu(fileName = "UrlHelperSO", menuName = "UMUNA/Inspector Helpers/Url Helper SO")]
    public class UrlHelperSO : ScriptableObjectT
    {
        public string ussPropertiesReferences = "https://docs.unity3d.com/Manual/UIE-USS-Properties-Reference.html";

        [InspectorButton]
        public void OpenUSSPropertiesReferences()
        {
            Application.OpenURL(ussPropertiesReferences);
        }
    }
}
