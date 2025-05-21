using System;
using System.Threading;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Window
{
    public interface IWindowService
    {
        void Bind<TWindow, TController>()
            where TWindow : AbstractWindowBase
            where TController : IController<TWindow>;

        void Bind<TWindow, TController, TModel>()
            where TWindow : AbstractWindowBase
            where TModel : AbstractWindowModel
            where TController : IModelBindable;

        UniTask<TWindow> OpenWindowAsync<TWindow>(bool onTop = false,Action onOpened = null, float delay = 1f, CancellationToken token = default) 
            where TWindow : AbstractWindowBase;

        TWindow OpenWindow<TWindow>(bool onTop = false, Action onOpened = null) 
            where TWindow : AbstractWindowBase;

        void Close<TWindow>(Action onClosed = null) 
            where TWindow : AbstractWindowBase;

        void CloseAll(Action onAllClosed = null);
        TWindow OpenWindowInParent<TWindow>(Transform parent, bool onTop = false, Action onOpened = null) where TWindow : AbstractWindowBase;
        bool IsWindowOpen<TWindow>() where TWindow : AbstractWindowBase;
    }
}