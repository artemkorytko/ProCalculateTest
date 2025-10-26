using UnityEngine;


namespace ProCalculate.Calculator
{
    public class UnityLogger : ILogger
    {
        public void Log(string message) => Debug.Log($"[ProCalc] {message}");
        public void LogWarning(string message) => Debug.LogWarning($"[ProCalc] {message}");
        public void LogError(string message) => Debug.LogError($"[ProCalc] {message}");
    }
}