using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var coroutineExecutor = new GameObject("CoroutineExecutor").AddComponent<CoroutineExecutor>();
        DontDestroyOnLoad(coroutineExecutor);
        Container.Bind<ICoroutineExecutor>().To<CoroutineExecutor>().FromInstance(coroutineExecutor).AsSingle();

        var unityEventProvider = new GameObject("UnityEventProvider").AddComponent<UnityEventProvider>();
        DontDestroyOnLoad(unityEventProvider);
        Container.Bind<UnityEventProvider>().FromInstance(unityEventProvider).AsSingle();

        var clickGo = new GameObject("ClickHandler");
        var longHandler = clickGo.AddComponent<LongClick>();
        var doubleClickHandler = clickGo.AddComponent<DoubleClick>();
        var clickHandler = clickGo.AddComponent<Click>();
        DontDestroyOnLoad(clickGo);
        Container.Bind<LongClick>().FromInstance(longHandler).AsSingle();
        Container.Bind<DoubleClick>().FromInstance(doubleClickHandler).AsSingle();
        Container.Bind<Click>().FromInstance(clickHandler).AsSingle();
        
        
        Container.Bind<SignalBus>().AsSingle();
        Container.Bind<ResourceLoaderService>().AsSingle();
        Container.BindInterfacesAndSelfTo<SettingsService>().AsSingle();

        SceneManager.LoadScene("GameScene");
    }
}
