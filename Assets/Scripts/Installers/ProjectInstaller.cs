using Zenject;
using ProCalculate.Dialog;


namespace ProCalculate.Calculator
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            // Declare signals
            Container.DeclareSignal<CalculationResultSignal>();
            Container.DeclareSignal<CalculationErrorSignal>();

            // Dialog signals (types in Dialog module)
            Container.DeclareSignal<ShowErrorPopupSignal>();
            Container.DeclareSignal<ErrorPopupClosedSignal>();

            // Services and implementations
            Container.Bind<IStorageService>().To<StorageService>().AsSingle();
            Container.Bind<ICalculationService>().To<CalculationService>().AsSingle();
            Container.Bind<ILogger>().To<UnityLogger>().AsSingle();

            // Presenters
            Container.BindInterfacesTo<CalculatorPresenter>().AsSingle();
            Container.BindInterfacesTo<ErrorPopupPresenter>().AsSingle();

            // Views (FromComponentInHierarchy)
            Container.Bind<ICalculatorView>().To<CalculatorView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IErrorPopupView>().To<ErrorPopupView>().FromComponentInHierarchy().AsSingle();
        }
    }
}