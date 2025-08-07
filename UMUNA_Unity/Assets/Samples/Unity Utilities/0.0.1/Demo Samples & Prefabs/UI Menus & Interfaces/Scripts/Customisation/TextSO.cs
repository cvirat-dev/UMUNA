using TMPro;
using UnityEngine;

namespace UUP.UI.Customisation
{
    [CreateAssetMenu(menuName = "UUP/CustomUI/TextSO", fileName = "Text")]
    public class TextSO : ScriptableObject
    {
        public ThemeSO Theme;

        public TMP_FontAsset Font;
        public float size;
    }
}