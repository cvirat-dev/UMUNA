using UnityEngine;

namespace UUP.UI.Customisation
{
    [CreateAssetMenu(menuName = "UUP/CustomUI/ViewSO", fileName = "ViewSO")]
    public class ViewSO : ScriptableObject
    {
        public ThemeSO Theme;
        public RectOffset Padding;
        public float Spacing;
    }
}