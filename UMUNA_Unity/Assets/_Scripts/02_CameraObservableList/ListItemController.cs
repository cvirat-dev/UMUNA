using UUP.CustomDataTypes;
using UUP.ScriptableObjects.Data.Variables;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UMUNA._Scripts.CameraObservableList
{
    public class ListItemController : MonoBehaviour
    {
        [Serializable]
        public struct ItemData
        {
            public int Index;
            public string Position;
            public string Rotation;
            public bool IsSelected;
        }

        public ItemData ItemDataF;

        [SerializeField]
        private ColorVariableSO nonSelectedColor;

        [SerializeField]
        private ColorVariableSO selectedColor;

        private Button _button;
        private TextMeshProUGUI _text;
        private Image _image;

        public event Action<ListItemController> OnSelected;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _image = this.transform.GetChild(0).GetComponent<Image>();

            if (_button == null)
            {
                throw new NullReferenceException("Button component not found in " + name);
            }

            if (_text == null)
            {
                throw new NullReferenceException("Text component not found in " + name);
            }

            if (_image == null)
            {
                throw new NullReferenceException("Image component not found in " + name);
            }

            _button.onClick.AddListener(SelectItem);

            Debug.Log("ListItemController Awake");
        }

        private void Start()
        {
            SetColor();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SelectItem);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SelectItem);
        }

        public void OnInit(SpatialOrientation spatialOrientation, int index)
        {
            ItemDataF.Index = index;
            ItemDataF.Position = spatialOrientation.PositionMessage;
            ItemDataF.Rotation = spatialOrientation.RotationMessage;
            ItemDataF.IsSelected = false;
            SetColor();
        }

        public void SelectItem()
        {
            Debug.Log("SelectItem");
            ItemDataF.IsSelected = true;
            SetColor();
            OnSelected?.Invoke(this);
        }

        public void DeSelectItem()
        {
            ItemDataF.IsSelected = false;
            SetColor();
        }

        public void SetText(string text)
        {
            try 
            { 
                _text.text = text;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("TextMeshProUGUI not set in " + name + " " + e.Message);
                Destroy(this);
            }
        }

        private void SetColor()
        {
            try
            {
                _image.color = ItemDataF.IsSelected ? selectedColor.Value : nonSelectedColor.Value;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("ColorVariableSO not set in " + name + " " + e.Message);
                Destroy(this);
            }
        }

    }
}