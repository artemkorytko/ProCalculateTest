using Zenject;
using System;


namespace ProCalculate.Dialog
{
    public class ErrorPopupPresenter : IInitializable, IDisposable
    {
        readonly IErrorPopupView _view;
        readonly SignalBus _signalBus;

        private string _lastShownExpression;

        public ErrorPopupPresenter(IErrorPopupView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _view.OnCloseRequested += OnCloseRequested;
            _signalBus.Subscribe<ShowErrorPopupSignal>(OnShowErrorPopup);
        }

        void OnShowErrorPopup(ShowErrorPopupSignal s)
        {
            _lastShownExpression = s.Expression;
            _view.Show(s.Expression, s.Message);
        }

        void OnCloseRequested()
        {
            _signalBus.Fire(new ErrorPopupClosedSignal { Expression = _lastShownExpression });
        }

        public void Dispose()
        {
            _view.OnCloseRequested -= OnCloseRequested;
            _signalBus.Unsubscribe<ShowErrorPopupSignal>(OnShowErrorPopup);
        }
    }
}