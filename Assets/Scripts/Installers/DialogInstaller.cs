using UnityEngine;
using Zenject;


public class DialogInstaller : MonoInstaller
{
    [SerializeField] private ErrorDialogView viewPrefab;

    public override void InstallBindings()
    {
        Container.Bind<ErrorDialogPresenter>().AsSingle().NonLazy();
        Container.Bind<ErrorDialogView>().FromInstance(viewPrefab).AsSingle();
    }
}