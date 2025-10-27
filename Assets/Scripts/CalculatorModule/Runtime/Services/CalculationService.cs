using System;
using System.Collections.Generic;
using Zenject;


namespace ProCalculate.Calculator
{
    public class CalculationService : ICalculationService
    {
        readonly SignalBus _signalBus;
        readonly IStorageService _storage;
        readonly ILogger _logger;

        private readonly List<string> _history = new List<string>();
        public IReadOnlyList<string> History => _history.AsReadOnly();

        public string CurrentExpression { get; set; }

        public CalculationService(SignalBus signalBus, IStorageService storage, ILogger logger)
        {
            _signalBus = signalBus;
            _storage = storage;
            _logger = logger;

            try
            {
                var saved = _storage.LoadState();
                if (saved != null)
                {
                    CurrentExpression = saved.CurrentExpression;
                    if (saved.History != null)
                        _history.AddRange(saved.History);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"CalculationService restore error: {e}");
            }
        }

        public void RequestCalculate(string expression)
        {
            CurrentExpression = expression;

            if (!ExpressionParser.TryParse(expression, out var left, out var right))
            {
                _logger.Log($"Invalid expression: {expression}");
                _signalBus.Fire(new CalculationErrorSignal { Expression = expression, Message = "Invalid expression format" });
                _history.Add($"{expression} = ERROR");
                try
                {
                    _storage.SaveState(new StorageState
                    {
                        CurrentExpression = expression,
                        History = new List<string>(_history)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError($"Storage save error: {e}");
                }

                return;
            }

            try
            {
                checked
                {
                    var result = left + right;
                    var record = $"{expression} = {result}";
                    _history.Add(record);

                    try
                    {
                        _storage.SaveState(new StorageState
                        {
                            CurrentExpression = expression,
                            History = new List<string>(_history)
                        });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Storage save error: {e}");
                    }

                    _signalBus.Fire(new CalculationResultSignal { Expression = expression, Result = result });
                }
            }
            catch (OverflowException)
            {
                _logger.LogError($"Overflow when adding: {expression}");
                _signalBus.Fire(new CalculationErrorSignal { Expression = expression, Message = "Overflow" });
            }
            catch (Exception e)
            {
                _logger.LogError($"Calculation error: {e}");
                _signalBus.Fire(new CalculationErrorSignal { Expression = expression, Message = "Unknown error" });
            }
        }
    }
}