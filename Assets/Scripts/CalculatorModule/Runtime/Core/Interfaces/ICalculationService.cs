using System.Collections.Generic;


namespace ProCalculate.Calculator
{
    public interface ICalculationService
    {
        IReadOnlyList<string> History { get; }
        string CurrentExpression { get; set; }
        void RequestCalculate(string expression);
    }
}