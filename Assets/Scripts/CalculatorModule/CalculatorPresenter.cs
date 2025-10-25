using System.Collections.Generic;
using Zenject;


public class CalculatorPresenter
{
    private readonly CalculatorModel model;
    private readonly CalculatorView view;

    public CalculatorPresenter(CalculatorModel model, CalculatorView view, SignalBus signalBus)
    {
        this.model = model;
        this.view = view;

        view.UpdateHistory(model.CalculationHistory);
        view.AddResultButtonListener(OnResultButtonClicked);
    }

    private void OnResultButtonClicked(string input)
    {
        model.Calculate(input, out bool isError);
        view.UpdateHistory(model.CalculationHistory);
    }
}