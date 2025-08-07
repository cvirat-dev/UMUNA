using UnityEngine;

namespace UUP.UI.Customisation
{
    [CreateAssetMenu(menuName = "UUP/CustomUI/ThemeSO", fileName = "Theme")]
    public class ThemeSO : ScriptableObject
    {
        [Header("Primary")]
        public Color Primary_bg;
        public Color Primary_txt;

        [Header("Secondary")]
        public Color Secondary_bg;
        public Color Secondary_txt;

        [Header("Tertiary")]
        public Color Tertiary_bg;
        public Color Tertiary_txt;

        [Header("Other")]
        public Color Disable;

        public Color GetBackgroundColor(Style style)
        {
            return style switch
            {
                Style.Primary => Primary_bg,
                Style.Secondary => Secondary_bg,
                Style.Tertiary => Tertiary_bg,
                _ => Disable
            };
        }

        public Color GetTextColor(Style style)
        {
            return style switch
            {
                Style.Primary => Primary_txt,
                Style.Secondary => Secondary_txt,
                Style.Tertiary => Tertiary_txt,
                _ => Disable
            };
        }

    }
}