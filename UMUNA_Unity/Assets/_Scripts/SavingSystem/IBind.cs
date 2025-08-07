using UUP.CustomDataTypes.Serializables;

namespace UMUNA.SavingSystem
{
    public interface IBind<TData>
    {
        GuidSRZ Id { get; set; }
        void Bind(TData data);
    }
}
