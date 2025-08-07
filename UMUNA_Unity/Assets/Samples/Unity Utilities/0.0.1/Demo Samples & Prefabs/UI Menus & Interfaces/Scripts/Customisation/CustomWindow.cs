using UnityEngine;
using UnityEngine.UI;

namespace UUP.UI.Customisation
{
    public class CustomWindow : CustomUiComponent
    {
        public WindowThemeSO WindowThemeSO;
        public GameObject ContainerTopObj;
        public GameObject ContainerCenterObj;
        public GameObject ContainerBottomObj;

        private Image _imageTop;
        private Image _imageCenter;
        private Image _imageBottom;
        private VerticalLayoutGroup _verticalLayoutGroup;

        public override void Setup()
        {
            _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            _imageTop = ContainerTopObj.GetComponent<Image>();
            _imageCenter = ContainerCenterObj.GetComponent<Image>();
            _imageBottom = ContainerBottomObj.GetComponent<Image>();
        }

        //[ProButton]
        public override void Configure()
        {
            _imageTop.color = WindowThemeSO.TopBottomColor;
            _imageCenter.color = WindowThemeSO.CenterColor;
            _imageBottom.color = WindowThemeSO.TopBottomColor;
        }
    }
}