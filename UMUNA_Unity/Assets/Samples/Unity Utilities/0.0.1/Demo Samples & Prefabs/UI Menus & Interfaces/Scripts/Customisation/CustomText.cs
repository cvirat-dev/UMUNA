using TMPro;
using UnityEngine;

namespace UUP.UI.Customisation
{
    public class CustomText : CustomUiComponent
    {
        public TextSO TextData;
        public Style style;

        private TextMeshProUGUI textMeshProUGUI;

        public override void Setup()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        [ContextMenu("Configure()")]
        public override void Configure()
        {
            //textMeshProUGUI.color = ...;
            textMeshProUGUI.color = TextData.Theme.GetTextColor(style);
            textMeshProUGUI.font = TextData.Font;
            textMeshProUGUI.fontSize = TextData.size;
        }
    }
}