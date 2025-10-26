using UnityEngine;
using System;


namespace ProCalculate.Calculator
{
    public class StorageService : IStorageService
    {
        private const string Key = "procalc_state_v1";

        public void SaveState(StorageState state)
        {
            try
            {
                var json = JsonUtility.ToJson(state);
                PlayerPrefs.SetString(Key, json);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"StorageService.SaveState error: {e}");
            }
        }

        public StorageState LoadState()
        {
            if (!PlayerPrefs.HasKey(Key)) return null;

            try
            {
                var json = PlayerPrefs.GetString(Key);
                return JsonUtility.FromJson<StorageState>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"StorageService.LoadState error: {e}");
                return null;
            }
        }

        public void ClearState()
        {
            if (PlayerPrefs.HasKey(Key)) PlayerPrefs.DeleteKey(Key);
        }
    }
}