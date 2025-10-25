using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class CalculatorView : MonoBehaviour
{
    private const int MaxHistorySize = 6;
    private const int HistoryPadding = 40;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button resultButton;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject resultItemPrefab;

    private Action<string> onResultAction;

    private void Start()
    {
        resultButton.onClick.AddListener(OnResultClicked);
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        resultButton.onClick.RemoveListener(OnResultClicked);
    }

    public void OnResultClicked()
    {
        onResultAction?.Invoke(inputField.text);
        inputField.text = string.Empty;
    }

    public void AddResultButtonListener(Action<string> action) => onResultAction = action;

    public void UpdateHistory(List<string> history)
    {
        foreach (Transform child in content.transform) Destroy(child.gameObject);
        foreach (var item in history)
        {
            GameObject resultItem = Instantiate(resultItemPrefab, content.transform);
            resultItem.GetComponent<TextMeshProUGUI>().text = item;
        }

        scrollRect.verticalNormalizedPosition = 0f; // Scroll to top
        layoutElement.preferredHeight = history.Count > MaxHistorySize ? HistoryPadding * MaxHistorySize : HistoryPadding * history.Count;
        Canvas.ForceUpdateCanvases();
    }
}