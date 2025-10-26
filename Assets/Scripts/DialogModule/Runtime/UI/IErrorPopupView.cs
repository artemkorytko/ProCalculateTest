using System;


namespace ProCalculate.Dialog
{
    public interface IErrorPopupView
    {
        event Action OnCloseRequested;
        void Show(string expression, string message);
        void Hide();
    }
}