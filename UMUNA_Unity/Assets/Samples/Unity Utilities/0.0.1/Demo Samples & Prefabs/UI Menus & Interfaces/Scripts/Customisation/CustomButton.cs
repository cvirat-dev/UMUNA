using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UUP.ScriptableObjects.GameEvents.NonSerialized;
using UUP.ScriptableObjects.GameEvents.Serialized;

namespace UUP.UI.Customisation
{
    public class CustomButton : CustomUiComponent
    {
        [Header("OnButtonHover-Message")]
        [SerializeField] private string buttonHoverMessage = " ";

        [Header("Events")]
        public UnityEvent OnClick;
        public DataGameEvent OnClickGE;
        public StringGameEvent OnButtonHoverEnterSGE;

        private Button _button;
        private TextMeshProUGUI _buttonText;
        public enum ConfigurationEnum
        {
            Yes,
            No
        }

        [Header("Yes: Theme-Customization / No: Manual-Customization (Optional)")]
        public ConfigurationEnum configurationState = ConfigurationEnum.Yes;

        // Customizable Inspector Field
        [SerializeField, HideInInspector]
        ButtonThemeSO ButtonTheme;
        // Customizable Inspector Field
        [SerializeField, HideInInspector]
        ButtonType ButtonType;

        public override void Setup()
        {
            _button = GetComponentInChildren<Button>();
            Debug.Assert(_button != null, "Button-Component not found!");

            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        //[ProButton]
        public override void Configure()
        {
            if (configurationState == ConfigurationEnum.Yes && ButtonTheme != null) // Step 2: Check the bool condition
            {
                ColorBlock cb = _button.colors;
                cb.normalColor = ButtonTheme.GetButtonColor(ButtonType);
                cb.selectedColor = ButtonTheme.GetButtonColor(ButtonType);

                _button.colors = cb;

                // not every button must have a text-component
                if (_buttonText != null)
                {
                    _buttonText.color = ButtonTheme.TextColor;
                    _buttonText.font = ButtonTheme.ButtonFont;
                    _buttonText.fontSize = ButtonTheme.FontSize;
                    _buttonText.fontStyle = ButtonTheme.FontStyle;
                }
            }
        }

        public void OnClickEvent()
        {
            OnClick.Invoke();
            OnClickGE.Raise(this);
        }

        public void LogButtonMessage()
        {
            Debug.Log(buttonHoverMessage);
        }

        public void OnButtonHoverEnter()
        {
            OnButtonHoverEnterSGE.Raise(buttonHoverMessage);
        }

        public void OnButtonHoverExit()
        {
            OnButtonHoverEnterSGE.Raise(string.Empty);
        }
    }

}

