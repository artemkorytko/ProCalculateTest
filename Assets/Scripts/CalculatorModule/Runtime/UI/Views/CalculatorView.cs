using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace ProCalculate.Calculator
{
    // [RequireComponent(typeof(Canvas))]
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [Header("Input / Controls")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _resultButton;

        [Header("History Area")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _historyContent;
        [SerializeField] private UIHistoryEntry _historyEntryPrefab;
        [SerializeField] private int _maxVisibleEntries = 6;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private float _layoutElementPreferredHeight = 40;

        public event Action<string> OnResultRequested;

        private int _entryCount;

        private void Awake()
        {
            if (_resultButton)
                _resultButton.onClick.AddListener(OnResultClicked);
        }

        private void OnDestroy()
        {
            if (_resultButton)
                _resultButton.onClick.RemoveListener(OnResultClicked);
        }

        private void OnResultClicked()
        {
            var expr = GetInput();
            OnResultRequested?.Invoke(expr);
        }

        public void SetInput(string expression)
        {
            if (_inputField)
                _inputField.text = expression ?? string.Empty;
        }

        public string GetInput()
        {
            return _inputField ? _inputField.text : string.Empty;
        }

        public void AddHistoryEntry(string text)
        {
            if (!_historyEntryPrefab || !_historyContent)
                return;

            var entry = Instantiate(_historyEntryPrefab, _historyContent, false);
            entry.Setup(text);

            _entryCount++;

            if (_scrollRect != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_historyContent as RectTransform);
                Canvas.ForceUpdateCanvases();
                _scrollRect.verticalNormalizedPosition = _entryCount > _maxVisibleEntries ? 0f : 1f;

                if (_layoutElement)
                {
                    _layoutElement.preferredHeight = _entryCount > _maxVisibleEntries
                        ? _layoutElementPreferredHeight * _maxVisibleEntries
                        : _layoutElementPreferredHeight * _entryCount;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_verticalLayoutGroup.transform as RectTransform);

                }
            }
        }

        public void ClearHistory()
        {
            if (!_historyContent) return;

            for (int i = _historyContent.childCount - 1; i >= 0; i--)
                Destroy(_historyContent.GetChild(i).gameObject);

            _entryCount = 0;

            if (_scrollRect)
                _scrollRect.verticalNormalizedPosition = 1f;
            if (_layoutElement)
            {
                _layoutElement.preferredHeight = 0;
            }
        }
    }
}