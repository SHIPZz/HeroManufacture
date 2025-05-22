using CodeBase.Gameplay.Inventories;
using CodeBase.UI.Controllers;
using CodeBase.UI.Inventories.Views;
using CodeBase.UI.Services.Window;
using UniRx;

namespace CodeBase.UI.Inventories.Controllers
{
    public class InventoryWindowController : IController<InventoryWindow>, IModelBindable<InventoryWindowModel>
    {
        private readonly IWindowService _windowService;
        private readonly CompositeDisposable _disposables = new();

        private InventoryWindow _window;
        private InventoryWindowModel _inventory;

        public InventoryWindowController(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Initialize()
        {
            _window.OnCloseClicked
                .Subscribe(_ => OnCloseClicked())
                .AddTo(_disposables);

            _inventory.Title
                .Subscribe(title => _window.SetTitle(title))
                .AddTo(_disposables);

            _inventory.IsFull
                .Subscribe(isFull => _window.SetFullState(isFull))
                .AddTo(_disposables);

            _inventory.Resources
                .ObserveAdd()
                .Subscribe(addEvent =>
                {
                    _window.SetItemsContainerActive(true);
                    _window.AddResource(addEvent.Key, addEvent.Value);
                })
                .AddTo(_disposables);

            _inventory.Resources
                .ObserveRemove()
                .Subscribe(removeEvent => _window.RemoveResource(removeEvent.Key))
                .AddTo(_disposables);

            _inventory.Resources
                .ObserveReplace()
                .Subscribe(replaceEvent => _window.UpdateResourceAmount(replaceEvent.Key, replaceEvent.NewValue))
                .AddTo(_disposables);


            _window.SetItemsContainerActive(_inventory.Resources.Count > 0);

            foreach (var resource in _inventory.Resources)
            {
                _window.AddResource(resource.Key, resource.Value);
            }
        }

        public void BindView(InventoryWindow window) => _window = window;

        public void BindModel(InventoryWindowModel model)
        {
            _inventory = model;
        }

        public void Dispose() => _disposables.Dispose();

        private void OnCloseClicked()
        {
            _windowService.Close<InventoryWindow>();
        }
    }
}