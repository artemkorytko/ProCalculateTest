namespace ProCalculate.Calculator
{
    public interface IStorageService
    {
        void SaveState(StorageState state);
        StorageState LoadState();
        void ClearState();
    }
}