
using UUP.CustomDataTypes.Serializables;

namespace UMUNA.SavingSystem
{
    public interface ISaveable<T>
    {
        GuidSRZ Id { get; set; }
        void LoadData(T data);
        void LoadData(string jsonString);
    }
}
