using Cysharp.Threading.Tasks;

namespace CodeBase.Common.Services.Persistent
{
    public interface IPersistentService
    {
        void Save();
        UniTaskVoid Load();
        void LoadAll();
    }
}