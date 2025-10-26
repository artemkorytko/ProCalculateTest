using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ProCalculate.Dialog
{
    public class ErrorPopupView : MonoBehaviour, IErrorPopupView
    {
        [SerializeField] private GameObject _rootPanel;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _okButton;

        public event Action OnCloseRequested;

        private void Awake()
        {
            if (_okButton)
                _okButton.onClick.AddListener(OnOkClicked);

            if (_rootPanel)
                _rootPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_okButton)
                _okButton.onClick.RemoveListener(OnOkClicked);
        }

        public void Show(string expression, string message)
        {
            if (_rootPanel)
                _rootPanel.SetActive(true);

            if (_messageText)
                _messageText.text = $"{message}";
        }

        public void Hide()
        {
            if (_rootPanel)
                _rootPanel.SetActive(false);
        }

        private void OnOkClicked()
        {
            OnCloseRequested?.Invoke();
            Hide();
        }
    }
}