using UUP.CustomDataTypes;
using UUP.Extensions;
using UUP.ScriptableObjects.Data.Entries;
using UUP.ScriptableObjects.Data.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UMUNA._Scripts.CameraPositionNotifRegister
{
    public class EntryContentController : MonoBehaviour
    {
        [SerializeField] private SpatialOrientationEntrySO spatialOrientationEntrySO;
        [SerializeField] private ColorVariableSO emptyEntryColor;
        [SerializeField] private ColorVariableSO nonEmptyEntryColor;
        [SerializeField] private ColorVariableSO onSelectedColor;
        [SerializeField] private IntVariableSO cameraPositionIndex;
        [SerializeField] private GameObject checkIconWhenActive;

        private Text _uiText;
        private Image _uiImage;
        private Animator _animator;

        private const string _line1base = "Entry ID: ";
        private const string _line2base = "Position: ";
        private const string _line3base = "Rotation: ";
        private int _entryID;
        private bool _isListening = false;
        private const string _entrySelectedAnimation = "TextScaleUpDown";

        private void Awake()
        {
            if(spatialOrientationEntrySO == null)
            {
                Debug.LogError("Spatial Orientation Entry SO not found in EntryContentController script", this);
                return;
            }

            if(cameraPositionIndex == null)
            {
                Debug.LogError("Camera Position Index SO not found in EntryContentController script", this);
                return;
            }

            _uiText = GetComponentInChildren<Text>();
            _uiImage = GetComponent<Image>();
            _animator = GetComponentInChildren<Animator>();

            if(_uiText == null || _uiImage == null || _animator == null)
            {
                Debug.LogError("UI Text, UI Image or Animator component not found in EntryContentController script", this);
                return;
            }

            _uiImage.color = emptyEntryColor.Value;
            checkIconWhenActive.SetActive(false);
            _entryID = spatialOrientationEntrySO.ID;

            cameraPositionIndex.OnValueSet += UpdateImageColor;
            spatialOrientationEntrySO.OnValueSet += UpdateTextUI;
            spatialOrientationEntrySO.OnIDChanged += OnIdChange;
            spatialOrientationEntrySO.OnStateChanged += OnRegisterStatusChange;
            _isListening = true;

            // First update
            UpdateTextUI(spatialOrientationEntrySO.Value);
        }

        private void OnEnable()
        {
            if(spatialOrientationEntrySO == null)
            {
                Debug.LogError("Spatial Orientation Entry SO not found in EntryContentController script", this);
                return;
            }

            if(!_isListening)
            {
                spatialOrientationEntrySO.OnValueSet += UpdateTextUI;
                spatialOrientationEntrySO.OnIDChanged += OnIdChange;
                spatialOrientationEntrySO.OnStateChanged += OnRegisterStatusChange;
            }
        }

        private void OnDisable()
        {
            if(spatialOrientationEntrySO == null)
            {
                Debug.LogError("Spatial Orientation Entry SO not found in EntryContentController script", this);
                return;
            }

            spatialOrientationEntrySO.OnValueSet -= UpdateTextUI;
            spatialOrientationEntrySO.OnIDChanged -= OnIdChange;
            spatialOrientationEntrySO.OnStateChanged -= OnRegisterStatusChange;
            _isListening = false;
        }

        private void OnIdChange(int id)
        {
            _entryID = id;
            var spatialOrientation = spatialOrientationEntrySO.Value;
            UpdateTextUI(spatialOrientation);
        }

        private void UpdateTextUI(SpatialOrientation spatialOrientation)
        {
            _animator.RestartAnimation(_entrySelectedAnimation);

            if(spatialOrientationEntrySO.Value == null)
            {
                _uiText.text = $"{_line1base}{_entryID}\n{_line2base}N/A\n{_line3base}N/A";
                return;
            }

            _uiText.text = $"{_line1base}{_entryID}\n{_line2base}{spatialOrientation.PositionMessage}\n{_line3base}{spatialOrientation.RotationMessage}";
        }

        private void UpdateImageColor(int cameraIndex)
        {
            if(_entryID == cameraIndex)
            {
                _uiImage.color = onSelectedColor.Value;
                checkIconWhenActive.SetActive(true);
            }
            else
            {
                checkIconWhenActive.SetActive(false);
                bool entryIsDefined = spatialOrientationEntrySO.IsDefined;

                if (entryIsDefined)
                {
                    _uiImage.color = nonEmptyEntryColor.Value;
                    return;
                }

                _uiImage.color = emptyEntryColor.Value;
            }
        }

        private void OnRegisterStatusChange(bool entryIsDefined)
        {
            if(entryIsDefined) {
                _uiImage.color = nonEmptyEntryColor.Value;
            }
            else
            {
                _uiImage.color = emptyEntryColor.Value;
            }
        }
    }
}