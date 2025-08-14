using System.Collections.Generic;
using System.Linq;
using Umuna.Core.Data;
using UMUNA.Data.Wrappers.Umuna;
using UMUNA.EventManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UUP.CustomDataTypes;

public class CameraDataUiController : MonoBehaviour
{
    #region Fields
    private CameraDataWrapper _cameraData;
    private ListView savedPositionsListView;
    private TextField positionField;
    private TextField rotationField;
    private Button addButton;
    private Button updateButton;
    private Button removeButton;
    private Button clearButton;
    private int _currentIndex = -1;
    #endregion

    #region Properties
    public CameraDataWrapper CameraData
    {
        get => _cameraData;
        set
        {
            _cameraData = value;
            if (_cameraData != null)
            {
                RefreshListView();
            }
        }
    }
    #endregion

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Bind UI elements
        savedPositionsListView = root.Q<ListView>("SavedPositionsListView");
        positionField = root.Q<TextField>("PositionField");
        rotationField = root.Q<TextField>("RotationField");
        addButton = root.Q<Button>("AddButton");
        updateButton = root.Q<Button>("UpdateButton");
        removeButton = root.Q<Button>("RemoveButton");
        clearButton = root.Q<Button>("ClearButton");

        // Add button listeners
        addButton.clicked += AddPosition;
        updateButton.clicked += UpdatePosition;
        removeButton.clicked += RemovePosition;
        clearButton.clicked += ClearPositions;

        savedPositionsListView.selectionChanged += OnPositionChange;
        EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.AddListener(Bind);
        EventManager.CameraSystem.OnNotifyCameraMovement.AddListener(DeselectAll);
    }

    private void Bind(UmunaData data)
    {
        _cameraData = new(data.CameraData);
        RefreshListView();
    }

    private void OnDisable()
    {
        // Unbind UI elements
        savedPositionsListView.selectionChanged -= OnPositionChange;
        EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.RemoveListener(Bind);
        EventManager.CameraSystem.OnNotifyCameraMovement.RemoveListener(DeselectAll);
        // Remove button listeners
        addButton.clicked -= AddPosition;
        updateButton.clicked -= UpdatePosition;
        removeButton.clicked -= RemovePosition;
        clearButton.clicked -= ClearPositions;
        // Clear the list view
        savedPositionsListView.itemsSource = null;
    }

    private void OnPositionChange(IEnumerable<object> enumerable)
    {
        // find the index of the selected item
        int index = savedPositionsListView.selectedIndex;

        if (index < 0 || index >= CameraData.SavedPositions.Count)
        {
            _currentIndex = -1; // Reset current index if selection is invalid
            return;
        }

        // Raise the event with the selected index
        EventManager.Ui.OnPositionSelected.Invoke(index);
    }

    private void RefreshListView()
    {
        savedPositionsListView.itemsSource = CameraData.SavedPositions.ToList();
        savedPositionsListView.makeItem = () => new Label();
        savedPositionsListView.bindItem = (element, i) =>
        {
            var label = (Label)element;
            label.text = CameraData.SavedPositions[i].ToString();

            // Apply style class based on selection
            if (i == CameraData.CurrentCameraIndex)
            {
                _currentIndex = i;
                label.AddToClassList("selected-item");
                label.RemoveFromClassList("default-item");
            }
            else
            {
                label.AddToClassList("default-item");
                label.RemoveFromClassList("selected-item");
            }
        };
        savedPositionsListView.Rebuild();
    }

    /// <summary>
    /// Reset default style to last selected item.
    /// </summary>
    private void DeselectAll()
    {
        if (_currentIndex < 0)
        {
            return; // No valid index to deselect
        }

        // Clear the ListView selection
        savedPositionsListView.ClearSelection();
        
        // Rebuild the ListView to ensure styling is updated
        savedPositionsListView.Rebuild();

        _currentIndex = -1;
    }

    private void AddPosition()
    {
        var position = ParseVector3(positionField.value);
        var rotation = ParseVector3(rotationField.value);
        if (position != null && rotation != null)
        {
            CameraData.AddPosition(new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
            RefreshListView();
        }
    }

    private void UpdatePosition()
    {
        var position = ParseVector3(positionField.value);
        var rotation = ParseVector3(rotationField.value);
        if (position != null && rotation != null)
        {
            CameraData.UpdatePosition(_currentIndex, new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
            RefreshListView();
        }
    }

    private void RemovePosition()
    {
        // To be implemented
        // Withot an index, we cannot remove a specific position
    }

    private void ClearPositions()
    {
        CameraData.ClearPositions();
        RefreshListView();
    }

    private Vector3? ParseVector3(string input)
    {
        var parts = input.Split(',');
        if (parts.Length == 3 &&
            float.TryParse(parts[0], out var x) &&
            float.TryParse(parts[1], out var y) &&
            float.TryParse(parts[2], out var z))
        {
            return new Vector3(x, y, z);
        }
        return null;
    }
}
