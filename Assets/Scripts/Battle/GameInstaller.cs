using System;
using Battle;
using Battle.Buildings;
using Battle.GameStart;
using Battle.Player;
using Battle.Resources;
using GridNamesapace;
using Battle.Minerals;
using UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(UIPanelService)).To<BattleUIPanelService>().AsSingle();
        Container.Bind<UIService>().To<UIService>().AsSingle().NonLazy();
        BindBuildingFactories();
        Container.Bind(typeof(BuildUIService), typeof(IInitializable), typeof(IDisposable)).To<BuildUIService>().AsSingle();
        Container.Bind(typeof(DragService), typeof(IInitializable), typeof(IDisposable)).To<DragService>().AsSingle();
        Container.Bind(typeof(ClickService), typeof(IInitializable), typeof(IDisposable)).To<ClickService>().AsSingle();
        Container.Bind(typeof(PlayerSelectService), typeof(IInitializable), typeof(IDisposable)).To<PlayerSelectService>().AsSingle();
        Container.Bind(typeof(GridService), typeof(IInitializable), typeof(IDisposable), typeof(IDataProvider<Grid<IBuildable>>)).To<GridService>().AsSingle();
        Container.Bind(typeof(GridVisualizationService), typeof(IInitializable), typeof(IDisposable)).To<GridVisualizationService>().AsSingle();
        Container.Bind(typeof(BuildService), typeof(IInitializable), typeof(IDisposable)).To<BuildService>().AsSingle();
        Container.Bind(typeof(PlayerService), typeof(IInitializable), typeof(IDisposable)).To<PlayerService>().AsSingle();
        BindMinerals();
        Container.Bind(typeof(PlayersBuildingsService), typeof(IInitializable), typeof(IDisposable)).To<PlayersBuildingsService>().AsSingle();
        Container.Bind(typeof(ResourcesWalletService), typeof(IInitializable), typeof(IDisposable)).To<ResourcesWalletService>().AsSingle();
        Container.Bind(typeof(WalletUiService), typeof(IInitializable), typeof(IDisposable)).To<WalletUiService>().AsSingle();
      
        
        
        
        
        
        
        
        
        
        
        
        Container.Bind(typeof(GameStartService), typeof(IInitializable), typeof(IDisposable)).To<GameStartService>().AsSingle();
    }

    private void BindBuildingFactories()
    {
        Container.BindFactory<CommandCenter, InternalCommandCenterFactory>();
        Container.BindFactory<Barrack, InternalBarrackFactory>();
        Container.Bind<IBuildingFactory>().To<CommandCenterFactory>().AsSingle();
        Container.Bind<IBuildingFactory>().To<BarrackFactory>().AsSingle();

        
        Container.Bind<BuildingFactory>().To<BuildingFactory>().AsSingle().NonLazy();
    }

    private void BindMinerals()
    {
        Container.BindFactory<Mineral, MineralFactory>();
        Container.Bind(typeof(MineralsService), typeof(IInitializable), typeof(IDisposable)).To<MineralsService>().AsSingle();
    }
}