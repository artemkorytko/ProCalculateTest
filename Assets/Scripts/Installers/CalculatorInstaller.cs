using UnityEngine;
using Zenject;


public class CalculatorInstaller : MonoInstaller
{
    [SerializeField] private CalculatorView viewPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        SignalsInstaller.Install(Container);
        Container.Bind<CalculatorModel>().AsSingle().NonLazy();
        Container.Bind<CalculatorPresenter>().AsSingle().NonLazy();
        Container.Bind<CalculatorView>().FromInstance(viewPrefab).AsSingle();
    }
}