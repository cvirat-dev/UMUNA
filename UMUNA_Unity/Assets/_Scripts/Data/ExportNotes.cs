using System;

namespace UMUNA.Data
{
    [System.Serializable]
    public class ExportNotes
    {
        public string ExportName = "default_name";
        public string UserName = "default_user_name";
        public DateTime ExportDate = DateTime.Now;
        public int NumberOfExports = 0;
        public int NumberOfCameraPositions = 0;
        public string ExportNotesText = "default_notes";
    }
}
