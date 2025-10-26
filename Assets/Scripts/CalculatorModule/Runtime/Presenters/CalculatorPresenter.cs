using Zenject;
using ProCalculate.Dialog;
using System;


namespace ProCalculate.Calculator
{
    public class CalculatorPresenter : IInitializable, IDisposable
    {
        readonly ICalculatorView _view;
        readonly ICalculationService _calc;
        readonly SignalBus _signalBus;
        readonly ILogger _logger;

        public CalculatorPresenter(ICalculatorView view, ICalculationService calc, SignalBus signalBus, ILogger logger)
        {
            _view = view;
            _calc = calc;
            _signalBus = signalBus;
            _logger = logger;
        }

        public void Initialize()
        {
            _view.OnResultRequested += OnResultRequested;
            _signalBus.Subscribe<CalculationResultSignal>(OnCalcResult);
            _signalBus.Subscribe<CalculationErrorSignal>(OnCalcError);
            _signalBus.Subscribe<ErrorPopupClosedSignal>(OnErrorPopupClosed);

            _view.ClearHistory();
            if (!string.IsNullOrEmpty(_calc.CurrentExpression))
                _view.SetInput(_calc.CurrentExpression);

            foreach (var item in _calc.History)
                _view.AddHistoryEntry(item);
        }

        private void OnResultRequested(string expr)
        {
            _logger.Log($"User requested calculation: {expr}");
            _calc.RequestCalculate(expr);
        }

        private void OnCalcResult(CalculationResultSignal s)
        {
            _logger.Log($"Calc result: {s.Expression} = {s.Result}");
            _view.AddHistoryEntry($"{s.Expression} = {s.Result}");
        }

        private void OnCalcError(CalculationErrorSignal s)
        {
            _logger.LogWarning($"Calc error for '{s.Expression}': {s.Message}");
            _view.AddHistoryEntry($"{s.Expression} = ERROR");
            _signalBus.Fire(
                new ShowErrorPopupSignal { Expression = s.Expression, Message = "Please check the expression you just entered" });
        }

        private void OnErrorPopupClosed(ErrorPopupClosedSignal s)
        {
            _logger.Log($"Error popup closed. Restore expression: {s.Expression}");
            _view.SetInput(s.Expression);
        }

        public void Dispose()
        {
            _view.OnResultRequested -= OnResultRequested;
            _signalBus.Unsubscribe<CalculationResultSignal>(OnCalcResult);
            _signalBus.Unsubscribe<CalculationErrorSignal>(OnCalcError);
            _signalBus.Unsubscribe<ErrorPopupClosedSignal>(OnErrorPopupClosed);
        }
    }
}