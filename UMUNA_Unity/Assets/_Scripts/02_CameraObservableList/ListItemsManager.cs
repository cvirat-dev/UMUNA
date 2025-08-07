using System.Collections.Generic;
using System.Linq;
using UMUNA.EventManagement;
using UMUNA.Extensions;
using UMUNA.ScriptableObjects;
using UnityEngine;
using UUP.CustomDataTypes;
using static UMUNA._Scripts.CameraObservableList.ListItemController;

namespace UMUNA._Scripts.CameraObservableList
{
    public class ListItemsManager : MonoBehaviour
    {
        [SerializeField] private GameObject listItemPrefab;
        [SerializeField] private List<ItemData> itemsData = new();
        [SerializeField] CameraDataSO cameraDataSO;

        private GameObject newItem;
        private Dictionary<GameObject, ListItemController> _itemsDictionary = new();

        public int CurrentCameraIndex
        {
            get
            {
                return cameraDataSO.CameraData.CurrentCameraIndex;
            }
            set
            {
                cameraDataSO.CameraData.CurrentCameraIndex = value;
            }
        }

        private void OnEnable()
        {
            EventManager.CameraSystem.OnNotifyCameraMovement.AddListener(DeselectAll);
            cameraDataSO.OnCameraDataChanged += SyncWithList;
        }

        private void OnDisable()
        {
            EventManager.CameraSystem.OnNotifyCameraMovement.RemoveListener(DeselectAll);
            cameraDataSO.OnCameraDataChanged -= SyncWithList;
        }

        void Start()
        {
            SyncWithList();
        }

        #region private methods
        private void SyncWithList()
        {

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _itemsDictionary.Clear();

            for (int i = 0; i < cameraDataSO.CameraData.SavedPositions.Count; i++)
            {
                AddItem(i, cameraDataSO.CameraData.SavedPositions[i].ToSpatialOrientation());
            }

            int currentCameraPosition = cameraDataSO.CameraData.CurrentCameraIndex;

            if (currentCameraPosition >= 0)
            {
                SetSelectionState(currentCameraPosition);
            }

            Debug.Log("List synced");
        }

        private void AddItem(int index, SpatialOrientation itemData)
        {
            // Create a new item and set its data
            newItem = Instantiate(listItemPrefab, transform);
            var listItemController = newItem.GetComponent<ListItemController>();
            listItemController.OnInit(itemData, index);

            // Set the name of the item
            //string newText = "Camera Position " + transform.childCount;
            string newText = "Camera Position " + index;

            newItem.name = newText;
            listItemController.SetText(newText);

            // Add the item data to the list
            var itemDataValue = listItemController.ItemDataF;
            itemsData.Add(itemDataValue);

            // Register the event
            listItemController.OnSelected += HandleItemSelectionState;

            // Add the item to the dictionary
            _itemsDictionary.Add(newItem, listItemController);
        }

        private void OnListCleared()
        {
            itemsData.Clear();
            
            foreach (var keyValueItem in _itemsDictionary)
            {
                // Deregister the event
                keyValueItem.Value.OnSelected -= HandleItemSelectionState;

                Destroy(keyValueItem.Key);
            }

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _itemsDictionary.Clear();
        }

        private void RemoveLastItem()
        {
            if (_itemsDictionary.Count > 0)
            {
                // Deregister the event
                var lastPair = _itemsDictionary.Last();
                lastPair.Value.OnSelected -= HandleItemSelectionState;

                _itemsDictionary.Remove(lastPair.Key);
                Destroy(lastPair.Key);
            }
        }

        private void RemoveItem(int index)
        {
            if(index < 0 || index >= _itemsDictionary.Count)
            {
                Debug.LogWarning("Index out of range");
                return;
            }

            // Deregister the event
            var itemToRemove = _itemsDictionary.ElementAt(index);
            itemToRemove.Value.OnSelected -= HandleItemSelectionState;

            _itemsDictionary.Remove(itemToRemove.Key);
            Destroy(itemToRemove.Key);

            // Update the names of the items
            for (int i = 0; i < _itemsDictionary.Count; i++)
            {
                var gameObject = _itemsDictionary.ElementAt(i).Key;
                var listItemController = _itemsDictionary.ElementAt(i).Value;

                string newText = "Camera Position " + (i + 1);
                gameObject.name = newText;
                listItemController.SetText(newText);
            }
        }

        private void DeselectAll()
        {
            if(CurrentCameraIndex == -1)
            {
                return;
            }

            Debug.Log("DeselectAll");
            foreach (var keyValuePair in _itemsDictionary)
            {
                keyValuePair.Value.DeSelectItem();
            }

            CurrentCameraIndex = -1;
        }

        private void HandleItemSelectionState(ListItemController listItemController)
        {
            var selectedIndex = listItemController.ItemDataF.Index;

            foreach (var keyValuePair in _itemsDictionary)
            {
                ListItemController itemController = keyValuePair.Value;
                var itemIndex = itemController.ItemDataF.Index;

                if (itemIndex != selectedIndex)
                {
                    itemController.DeSelectItem();
                }
            }

            if (listItemController.ItemDataF.IsSelected)
            {
                EventManager.Ui.OnPositionSelected.Invoke(selectedIndex);
            }
        }


        private void SetSelectionState(int index)
        {
            if (index < 0 || index >= _itemsDictionary.Count)
            {
                Debug.LogWarning("Index out of range");
                return;
            }
            foreach (var keyValuePair in _itemsDictionary)
            {
                ListItemController itemController = keyValuePair.Value;
                var itemIndex = itemController.ItemDataF.Index;
                if (itemIndex == index)
                {
                    itemController.SelectItem();
                    CurrentCameraIndex = index;
                    EventManager.Ui.OnPositionSelected.Invoke(index);
                }
                else
                {
                    itemController.DeSelectItem();
                }
            }
        }
        #endregion
    }
}