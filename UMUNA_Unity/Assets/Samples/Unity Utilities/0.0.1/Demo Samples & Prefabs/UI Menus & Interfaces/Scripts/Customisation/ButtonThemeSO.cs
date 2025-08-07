using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UUP.UI.Customisation
{
    [CreateAssetMenu(menuName = "UUP/CustomUI/ButtonThemeSO", fileName = "ButtonThemeSO")]
    public class ButtonThemeSO : ScriptableObject
    {
        [Header("Button-Color 1")]
        public Color ButtonColor1;

        [Header("Button-Color 2")]
        public Color ButtonColor2;

        [Header("Font-style of button")]
        public TMP_FontAsset ButtonFont;

        [Header("Color of button-text")]
        public Color TextColor;

        [Header("Fontsize")]
        public int FontSize;
        
        [Header("Fontsize")]
        public FontStyles FontStyle;

        public Color GetButtonColor(ButtonType ButtonType)
        {
            return ButtonType switch
            {
                ButtonType.Type1 => ButtonColor1,
                ButtonType.Type2 => ButtonColor2,
                _ => ButtonColor1
            };
        }

    }
}