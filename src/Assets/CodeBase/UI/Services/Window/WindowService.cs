using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.StaticData;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private const int BaseSortingOrder = 100;
        private const int TopSortingOrder = 1000;

        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIProvider _uiProvider;

        private readonly Dictionary<Type, WindowBindingInfo> _windowBindings = new();
        private readonly Dictionary<Type, (AbstractWindowBase Window, IController Controller)> _activeWindows = new();
        private int _currentSortingOrder = BaseSortingOrder;

        public WindowService(IInstantiator instantiator,
            IStaticDataService staticDataService,
            IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void Bind<TWindow, TController>()
            where TWindow : AbstractWindowBase
            where TController : IController<TWindow>
        {
            var windowType = typeof(TWindow);

            if (_windowBindings.ContainsKey(windowType))
            {
                Debug.LogWarning($"Window type {windowType.Name} is already bound.");
                return;
            }

            _windowBindings[windowType] = new WindowBindingInfo
            {
                WindowType = windowType,
                ControllerType = typeof(TController),
                ModelType = null,
                Prefab = _staticDataService.GetWindow<TWindow>()
            };
        }

        public void Bind<TWindow, TController, TModel>()
            where TWindow : AbstractWindowBase
            where TModel : AbstractWindowModel
            where TController : IModelBindable
        {
            var windowType = typeof(TWindow);

            if (_windowBindings.ContainsKey(windowType))
                throw new InvalidOperationException($"Window type {windowType.Name} is already bound.");

            _windowBindings[windowType] = new WindowBindingInfo
            {
                WindowType = windowType,
                ControllerType = typeof(TController),
                ModelType = typeof(TModel),
                Prefab = _staticDataService.GetWindow<TWindow>()
            };
        }
        
        public bool IsWindowOpen<TWindow>() where TWindow : AbstractWindowBase
        {
            Type windowType = typeof(TWindow);
            return _activeWindows.ContainsKey(windowType);
        }

        public async UniTask<TWindow> OpenWindowAsync<TWindow>(bool onTop = false, Action onOpened = null, float delay = 1f, CancellationToken token = default) where TWindow : AbstractWindowBase
        {
            await UniTask.WaitForSeconds(delay, cancellationToken: token);
            
            return OpenWindow<TWindow>(onTop,onOpened);
        }

        public TWindow OpenWindow<TWindow>(bool onTop = false, Action onOpened = null) where TWindow : AbstractWindowBase
        {
            return OpenWindowInternal<TWindow>(_uiProvider.MainUI, onTop, onOpened);
        }

        public TWindow OpenWindowInParent<TWindow>(Transform parent, bool onTop = false, Action onOpened = null) where TWindow : AbstractWindowBase
        {
            return OpenWindowInternal<TWindow>(parent, onTop, onOpened);
        }

        public void Close<TWindow>(Action onClosed = null) where TWindow : AbstractWindowBase
        {
            Type windowType = typeof(TWindow);

            if (!_activeWindows.TryGetValue(windowType, out (AbstractWindowBase Window, IController Controller) windowData))
            {
                onClosed?.Invoke();
                return;
            }

            windowData.Window.Close(onClosed);

            if (windowData.Controller is IDisposable disposable)
                disposable.Dispose();

            _activeWindows.Remove(windowType);
        }

        public void CloseAll(Action onAllClosed = null)
        {
            if (_activeWindows.Count == 0)
            {
                onAllClosed?.Invoke();
                return;
            }

            foreach ((AbstractWindowBase Window, IController Controller) windowData in _activeWindows.Values)
            {
                if (windowData.Window != null && windowData.Window.gameObject != null)
                {
                    windowData.Window.Close();
                }

                if (windowData.Controller is IDisposable disposable)
                    disposable.Dispose();
            }

            _activeWindows.Clear();
            onAllClosed?.Invoke();
            _currentSortingOrder = BaseSortingOrder;
        }

        private TWindow OpenWindowInternal<TWindow>(Transform parent, bool onTop = false, Action onOpened = null) where TWindow : AbstractWindowBase
        {
            Type windowType = typeof(TWindow);

            if (!_windowBindings.TryGetValue(windowType, out var bindingInfo))
                throw new InvalidOperationException($"No binding found for window type {windowType.Name}");

            if (TryGetActiveWindow(onTop, onOpened, windowType, out TWindow openWindowInternal)) 
                return openWindowInternal;

            TWindow createdWindow = _instantiator.InstantiatePrefabForComponent<TWindow>(bindingInfo.Prefab, parent);

            if(createdWindow is null)
                throw new ArgumentNullException(nameof(createdWindow));
            
            IController<TWindow> controller = (IController<TWindow>)_instantiator.Instantiate(bindingInfo.ControllerType);
            
            if(controller is null)
                throw new ArgumentNullException(nameof(controller));

            BindModelIfHas(bindingInfo, controller);

            InitWindow(controller, createdWindow, onOpened);

            FillActiveWindows(windowType, createdWindow, controller);

            SetWindowSortingOrder(createdWindow, onTop);

            return createdWindow;
        }

        private bool TryGetActiveWindow<TWindow>(bool onTop, Action onOpened, Type windowType, out TWindow openWindowInternal)
            where TWindow : AbstractWindowBase
        {
            openWindowInternal = null;
            
            if (_activeWindows.ContainsKey(typeof(TWindow)))
            {
                var window = (TWindow)_activeWindows[windowType].Window;
                SetWindowSortingOrder(window, onTop);
                onOpened?.Invoke();
                openWindowInternal = window;
                return true;
            }

            return false;
        }

        private static void InitWindow<TWindow>(IController<TWindow> controller, TWindow window, Action onOpened = null)
            where TWindow : AbstractWindowBase
        {
            controller.BindView(window);
            controller.Initialize();
            window.Open(onOpened);
        }

        private void BindModelIfHas(WindowBindingInfo bindingInfo, IController controller)
        {
            if (bindingInfo.ModelType != null)
            {
                var model = (AbstractWindowModel)_instantiator.Instantiate(bindingInfo.ModelType);

                if (controller is IModelBindable controllerWithModel)
                    controllerWithModel.BindModel(model);
            }
        }

        private void SetWindowSortingOrder(AbstractWindowBase window, bool onTop)
        {
            if (window.TryGetComponent<Canvas>(out var canvas))
            {
                canvas.sortingOrder = onTop ? TopSortingOrder : _currentSortingOrder++;
            }
        }

        private void FillActiveWindows<TWindow>(Type windowType, TWindow createdWindow, IController<TWindow> controller)
            where TWindow : AbstractWindowBase
        {
            _activeWindows[windowType] = (createdWindow, (controller));
        }
    }
}