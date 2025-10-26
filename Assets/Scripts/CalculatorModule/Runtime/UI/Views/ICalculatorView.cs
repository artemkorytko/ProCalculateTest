using System;


namespace ProCalculate.Calculator
{
    public interface ICalculatorView
    {
        event Action<string> OnResultRequested;
        void SetInput(string expression);
        string GetInput();
        void AddHistoryEntry(string text);
        void ClearHistory();
    }
}