using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Common.Services.SaveLoad;
using CodeBase.Data;
using Cysharp.Threading.Tasks;

namespace CodeBase.Common.Services.Persistent
{
    public class PersistentService : IPersistentService, IDisposable
    {
        private readonly List<IProgressWatcher> _progressWatchers;

        private readonly ISaveLoadSystem _saveLoadSystem;
        
        private ProgressData _currentProgress;

        public PersistentService(ISaveLoadSystem saveLoadSystem, IEnumerable<IProgressWatcher> progressWatchers)
        {
            _progressWatchers = progressWatchers.ToList();
            _saveLoadSystem = saveLoadSystem;
        }

        public async UniTaskVoid Load()
        {
            _currentProgress = await _saveLoadSystem.Load();
        }

        public void LoadAll()
        {
            Load().Forget();

            foreach (IProgressWatcher progressWatcher in _progressWatchers)
            {
                progressWatcher.Load(_currentProgress);
            }
        }

        public void Save()
        {
            foreach (IProgressWatcher progressWatcher in _progressWatchers)
            {
                progressWatcher.Save(_currentProgress);
            }

            _saveLoadSystem.Save(_currentProgress);
        }

        public void Dispose()
        {
            Save();
        }
    }
}