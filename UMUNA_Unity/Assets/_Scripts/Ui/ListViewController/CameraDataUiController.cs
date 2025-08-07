using System.Collections.Generic;
using Umuna.Core.Data.Umuna;
using UMUNA.EventManagement;
using UMUNA.Extensions;
using UMUNA.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using UUP.CustomDataTypes;

public class CameraDataUiController : MonoBehaviour
{
    [SerializeField] private CameraDataSO cameraData;

    private ListView savedPositionsListView;
    private TextField positionField;
    private TextField rotationField;
    private Button addButton;
    private Button updateButton;
    private Button removeButton;
    private Button clearButton;

    private int _currentIndex = -1;

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

        // Populate list view
        RefreshListView();

        // Add button listeners
        addButton.clicked += AddPosition;
        updateButton.clicked += UpdatePosition;
        removeButton.clicked += RemovePosition;
        clearButton.clicked += ClearPositions;

        savedPositionsListView.selectionChanged += OnPositionChange;
        cameraData.OnCameraDataChanged += RefreshListView;
        EventManager.CameraSystem.OnNotifyCameraMovement.AddListener(DeselectAll);
    }

    private void OnDisable()
    {
        // Unbind UI elements
        savedPositionsListView.selectionChanged -= OnPositionChange;
        cameraData.OnCameraDataChanged -= RefreshListView;
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

        if (index < 0 || index >= cameraData.CameraData.SavedPositions.Count)
        {
            _currentIndex = -1; // Reset current index if selection is invalid
            return;
        }

        // Raise the event with the selected index
        EventManager.Ui.OnPositionSelected.Invoke(index);
    }

    private void RefreshListView()
    {
        savedPositionsListView.itemsSource = cameraData.CameraData.SavedPositions;
        savedPositionsListView.makeItem = () => new Label();
        savedPositionsListView.bindItem = (element, i) =>
        {
            var label = (Label)element;
            label.text = cameraData.CameraData.SavedPositions[i].ToSpatialOrientation().ToString();

            // Apply style class based on selection
            if (i == cameraData.CameraData.CurrentCameraIndex)
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
            cameraData.ModifySavedPositions(ListOperation.Add, new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
            RefreshListView();
        }
    }

    private void UpdatePosition()
    {
        var position = ParseVector3(positionField.value);
        var rotation = ParseVector3(rotationField.value);
        if (position != null && rotation != null)
        {
            cameraData.ModifySavedPositions(ListOperation.Update, new SpatialOrientation(position.Value, Quaternion.Euler(rotation.Value)));
            RefreshListView();
        }
    }

    private void RemovePosition()
    {
        cameraData.ModifySavedPositions(ListOperation.Remove, null);
        RefreshListView();
    }

    private void ClearPositions()
    {
        cameraData.ModifySavedPositions(ListOperation.Clear, null);
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
