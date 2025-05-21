using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Loading;
using UnityEngine;

namespace CodeBase.UI.LoadingCurtains
{
    public class LoadingCurtainWindow : AbstractWindowBase
    {
        [SerializeField] private LoadingView _loadingView;

        protected override void OnOpenStarted()
        {
            _loadingView.Show();
        }

        protected override void OnClose()
        {
            _loadingView.Hide();
        }
    }
}