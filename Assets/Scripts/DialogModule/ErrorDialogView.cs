using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ErrorDialogView : MonoBehaviour
{
    [SerializeField] private Button gotItButton;
    private Action _onGotItClickedAction;

    private void Start()
    {
        gotItButton.onClick.AddListener(OnGotItClicked);
        gameObject.SetActive(false);       
    }

    private void OnDestroy()
    {
        gotItButton.onClick.RemoveListener(OnGotItClicked);
    }

    public void AddGotItButtonListener(Action action) => _onGotItClickedAction = action;

    private void OnGotItClicked()
    {
        _onGotItClickedAction?.Invoke();
    }
}