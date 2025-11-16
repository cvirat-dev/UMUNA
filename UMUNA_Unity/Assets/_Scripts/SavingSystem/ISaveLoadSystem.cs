using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using Umuna.Core.SharedData;
using UMUNA.Data;

namespace UMUNA.SavingSystem
{
    public interface ISaveLoadSystem
    {
        ExportNotes ExportNotes { get; set; }
        IFileSerializer<ExportNotes> ExportNotesDataService { get; }
        IFileSerializer<UmunaData> UmunaDataService { get; }
        SerializerType SerializerType { get; }

        void Load();
        void Save();
        void SaveAsync();

    }
}