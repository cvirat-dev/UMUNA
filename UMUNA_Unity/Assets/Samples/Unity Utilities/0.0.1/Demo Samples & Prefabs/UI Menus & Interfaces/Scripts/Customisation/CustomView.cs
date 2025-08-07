using UnityEngine;
using UnityEngine.UI;

namespace UUP.UI.Customisation
{
    public class CustomView : CustomUiComponent
    {
        public ViewSO ViewData;

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

        [ContextMenu("Configure()")]
        public override void Configure()
        {
            // Configure the LayoutGroup
            _verticalLayoutGroup.padding = ViewData.Padding;
            _verticalLayoutGroup.spacing = ViewData.Spacing;

            _imageTop.color = ViewData.Theme.Primary_bg;
            _imageCenter.color = ViewData.Theme.Secondary_bg;
            _imageBottom.color = ViewData.Theme.Tertiary_bg;
        }
    }
}