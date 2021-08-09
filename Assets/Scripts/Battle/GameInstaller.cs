using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(GridService), typeof(IInitializable), typeof(IDisposable), typeof(IDataProvider<Grid>)).To<GridService>().AsSingle();
        Container.Bind(typeof(GridVisualizationService), typeof(IInitializable), typeof(IDisposable)).To<GridVisualizationService>().AsSingle();
        //Container.Bind(typeof(MoveClientService), typeof(IInitializable), typeof(IDisposable)).To<MoveClientService>().AsSingle();
        //Container.Bind(typeof(UiService), typeof(IInitializable), typeof(IDisposable)).To<UiService>().AsSingle();
        //Container.Bind(typeof(CoinsClientService), typeof(IInitializable), typeof(IDisposable)).To<CoinsClientService>().AsSingle();
        //Container.Bind(typeof(GameFlowClientService), typeof(IInitializable), typeof(IDisposable)).To<GameFlowClientService>().AsSingle();
    }   
}