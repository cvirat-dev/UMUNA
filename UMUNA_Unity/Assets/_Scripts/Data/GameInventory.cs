//using System;
//using System.Collections.Generic;
//using UMUNA.SavingSystem;
//using UUP.CustomDataTypes;
//using UUP.CustomDataTypes.Serializables;
//using UUP.Enums;

//namespace UMUNA.Data
//{
//    [Serializable]
//    public class GameInventory : SaveableBase<GameInventory>
//    {
//        public string GameName = "default_name";
//        public string UserName = "default_user_name";
//        public UserData UserData = new();
//        public CameraData CameraData = new();
//        public SettingsData SettingsData = new();
//    }

//    [Serializable]
//    public class UserData : SaveableBase<UserData>
//    {
//        public string PlayerName = "default_name";
//    }

//    [Serializable]
//    public class CameraData : SaveableBase<CameraData>
//    {
//        public SpatialOrientationSRZ LastCameraPosition { get; set; } = new();
//        public int CurrentCameraIndex { get; set; }
//        public List<SpatialOrientationSRZ> SavedPositions { get; set; } = new();

//        public void SetNextCameraIndex()
//        {
//            CurrentCameraIndex++;
//            if (CurrentCameraIndex >= SavedPositions.Count)
//            {
//                CurrentCameraIndex = 0;
//            }
//        }

//        public void ModifySavedPositions(ListOperation op, SpatialOrientation? spatialOrientation)
//        {
//            switch (op)
//            {
//                case ListOperation.Add:
//                    if (spatialOrientation != null)
//                    {
//                        SavedPositions.Add(new SpatialOrientationSRZ(spatialOrientation));
//                    }
//                    break;
//                case ListOperation.Update:
//                    if (spatialOrientation != null)
//                    {
//                        if (CurrentCameraIndex >= 0 && CurrentCameraIndex < SavedPositions.Count)
//                        {
//                            SavedPositions[CurrentCameraIndex] = new SpatialOrientationSRZ(spatialOrientation);
//                        }
//                    }
//                    break;
//                case ListOperation.Remove:
//                    if (spatialOrientation != null)
//                    {
//                        SavedPositions.Remove(new SpatialOrientationSRZ(spatialOrientation));
//                    }
//                    break;
//                case ListOperation.Clear:
//                    SavedPositions.Clear();
//                    CurrentCameraIndex = -1;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
//            }
//        }
//    }

//    [Serializable]
//    public class SettingsData : SaveableBase<SettingsData>
//    {
//        public bool IsSoundOn = true;
//        public bool IsMusicOn = true;
//        public bool ResetCameraToLastPosition = true;
//    }

//}
