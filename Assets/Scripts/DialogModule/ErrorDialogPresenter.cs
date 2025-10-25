using UnityEngine;
using Zenject;

public class ErrorDialogPresenter
{
    private readonly ErrorDialogView view;

    public ErrorDialogPresenter(ErrorDialogView view)
    {
        this.view = view;
        view.AddGotItButtonListener(OnGotItClicked);
    }

    public void ShowDialog() => view.gameObject.SetActive(true);

    private void OnGotItClicked()
    {
        view.gameObject.SetActive(false);
    }
}