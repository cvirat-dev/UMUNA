//using System.Collections.Generic;
//using System.Linq;
//using Umuna.Core.SharedData;
//using UMUNA.EventManagement;
//using UnityEngine;
//using UnityEngine.UIElements;
//using UUP.CustomDataTypes;

//public class CameraDataUiController : MonoBehaviour
//{
//    #region Fields
//    private ListView savedPositionsListView;
//    [SerializeField] private VisualTreeAsset _listItemUxml;
//    private TextField positionField;
//    private TextField rotationField;
//    private Button addButton;
//    private Button updateButton;
//    private Button removeButton;
//    private Button clearButton;
//    private int _currentIndex = -1;
//    #endregion

//    #region Properties
//    public CameraDataWrapper CameraData
//    {
//        get => _cameraData;
//        set
//        {
//            _cameraData = value;
//            if (_cameraData != null)
//            {
//                RefreshListView();
//            }
//        }
//    }
//    #endregion

//    private void OnEnable()
//    {
//        var root = GetComponent<UIDocument>().rootVisualElement;

//        // Bind UI elements
//        savedPositionsListView = root.Q<ListView>("SavedPositionsListView");
//        positionField = root.Q<TextField>("PositionField");
//        rotationField = root.Q<TextField>("RotationField");
//        addButton = root.Q<Button>("AddButton");
//        updateButton = root.Q<Button>("UpdateButton");
//        removeButton = root.Q<Button>("RemoveButton");
//        clearButton = root.Q<Button>("ClearButton");

//        // Add button listeners
//        addButton.clicked += AddPosition;
//        updateButton.clicked += UpdatePosition;
//        removeButton.clicked += RemovePosition;
//        clearButton.clicked += ClearPositions;

//        savedPositionsListView.selectionChanged += OnPositionChange;
//        EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.AddListener(Bind);
//        EventManager.CameraSystem.OnNotifyCameraMovement.AddListener(DeselectAll);
//    }

//    private void Bind(UmunaData data)
//    {
//        _cameraData = new(data.CameraData);
//        CameraData.OnValueChanged += () => RefreshListView();
//        RefreshListView();
//    }

//    private void OnDisable()
//    {
//        // Unbind UI elements
//        savedPositionsListView.selectionChanged -= OnPositionChange;
//        EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.RemoveListener(Bind);
//        EventManager.CameraSystem.OnNotifyCameraMovement.RemoveListener(DeselectAll);
//        // Remove button listeners
//        addButton.clicked -= AddPosition;
//        updateButton.clicked -= UpdatePosition;
//        removeButton.clicked -= RemovePosition;
//        clearButton.clicked -= ClearPositions;
//        // Clear the list view
//        savedPositionsListView.itemsSource = null;
//    }

//    private void OnPositionChange(IEnumerable<object> enumerable)
//    {
//        // find the index of the selected item
//        int index = savedPositionsListView.selectedIndex;

//        if (index < 0 || index >= CameraData.SavedPositions.Count)
//        {
//            _currentIndex = -1; // Reset current index if selection is invalid
//            return;
//        }

//        // Raise the event with the selected index
//        EventManager.Ui.OnPositionSelected.Invoke(index);
//    }

//    private void RefreshListView()
//    {
//        savedPositionsListView.itemsSource = CameraData.SavedPositions.ToList();
//        if (_listItemUxml == null)
//        {
//            Debug.LogError("List item UXML template not found in Resources.");
//            return;
//        }

//        savedPositionsListView.makeItem = () =>
//        {
//            var ve = _listItemUxml.Instantiate();
//            // Cache child controls in userData for faster bind
//            var toggle = ve.Q<Toggle>("SelectToggle");
//            var nameField = ve.Q<TextField>("NameField");
//            var goButton = ve.Q<Button>("GoButton");

//            // Store references for bindItem
//            ve.userData = new ItemRefs
//            {
//                Toggle = toggle,
//                NameField = nameField,
//                GoButton = goButton
//            };

//            // Register persistent handlers once; they will use element.userData for index
//            toggle?.RegisterValueChangedCallback(evt =>
//            {
//                if (ve.userData is ItemRefs r && r.Index >= 0)
//                {
//                    // Keep ListView selection in sync with toggle
//                    if (evt.newValue)
//                    {
//                        savedPositionsListView.SetSelection(r.Index);
//                    }
//                    else if (savedPositionsListView.selectedIndex == r.Index)
//                    {
//                        savedPositionsListView.ClearSelection();
//                    }
//                }
//            });

//            if (goButton != null)
//            {
//                goButton.clicked += () =>
//                {
//                    if (ve.userData is ItemRefs r && r.Index >= 0)
//                    {
//                        // Raise same event as selecting the item
//                        EventManager.Ui.OnPositionSelected.Invoke(r.Index);
//                        savedPositionsListView.SetSelection(r.Index);
//                    }
//                };
//            }

//            return ve;
//        };
//        savedPositionsListView.bindItem = (element, i) =>
//        {
//            // Unpack helpers
//            var refs = element.userData as ItemRefs;
//            if (refs == null)
//            {
//                // In case element was created before code update, rebuild refs
//                refs = new ItemRefs
//                {
//                    Toggle = element.Q<Toggle>("SelectToggle"),
//                    NameField = element.Q<TextField>("NameField"),
//                    GoButton = element.Q<Button>("GoButton")
//                };
//                element.userData = refs;
//            }

//            refs.Index = i;
//            var item = CameraData.SavedPositions[i];

//            // Display: use ToString or custom name when available
//            if (refs.NameField != null)
//            {
//                refs.NameField.SetValueWithoutNotify(item.ToString());
//                refs.NameField.isReadOnly = true; // prevent editing for now
//            }

//            //// Style based on current camera index
//            //bool isCurrent = i == CameraData.CurrentCameraIndex;
//            //element.EnableInClassList("selected-item", isCurrent);
//            //element.EnableInClassList("default-item", !isCurrent);

//            refs.Toggle?.SetValueWithoutNotify(savedPositionsListView.selectedIndex == i);
//        };
//        savedPositionsListView.fixedItemHeight = 34f;
//        savedPositionsListView.Rebuild();
//    }

//    private class ItemRefs
//    {
//        public Toggle Toggle;
//        public TextField NameField;
//        public Button GoButton;
//        public int Index = -1;
//    }

//    /// <summary>
//    /// Reset default style to last selected item.
//    /// </summary>
//    private void DeselectAll()
//    {
//        if (_currentIndex < 0)
//        {
//            return; // No valid index to deselect
//        }

//        // Clear the ListView selection
//        savedPositionsListView.ClearSelection();
        
//        // Rebuild the ListView to ensure styling is updated
//        savedPositionsListView.Rebuild();

//        _currentIndex = -1;
//    }

//    private void AddPosition()
//    {
//        var position = ParseVector3(positionField.value);
//        var rotation = ParseVector3(rotationField.value);
//        if (position != null && rotation != null)
//        {
//            CameraData.AddPosition(new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
//        }
//    }

//    private void UpdatePosition()
//    {
//        var position = ParseVector3(positionField.value);
//        var rotation = ParseVector3(rotationField.value);
//        if (position != null && rotation != null)
//        {
//            CameraData.UpdatePosition(_currentIndex, new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
//        }
//    }

//    private void RemovePosition()
//    {
//        // To be implemented
//        // Withot an index, we cannot remove a specific position
//    }

//    private void ClearPositions()
//    {
//        CameraData.ClearPositions();
//    }

//    private Vector3? ParseVector3(string input)
//    {
//        var parts = input.Split(',');
//        if (parts.Length == 3 &&
//            float.TryParse(parts[0], out var x) &&
//            float.TryParse(parts[1], out var y) &&
//            float.TryParse(parts[2], out var z))
//        {
//            return new Vector3(x, y, z);
//        }
//        return null;
//    }
//}
