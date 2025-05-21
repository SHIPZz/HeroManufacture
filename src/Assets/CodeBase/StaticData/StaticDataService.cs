using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Constants;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Sound;
using CodeBase.UI.Sound.Configs;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<Type, AbstractWindowBase> _windows;
        private Dictionary<SoundTypeId, SoundConfig> _soundConfigs;
        private SoundPlayerView _soundPlayerView;

        public void LoadAll()
        {
            _windows = Resources.LoadAll<AbstractWindowBase>(AssetPath.Windows)
                .ToDictionary(x => x.GetType(), x => x);
            
            _soundConfigs = Resources.LoadAll<SoundConfig>(AssetPath.Sound)
                .ToDictionary(x => x.Id, x => x);

            _soundPlayerView = Resources.Load<SoundPlayerView>(AssetPath.SoundPlayerView);
        }

        public SoundConfig GetSoundConfig(SoundTypeId soundTypeId)
        {
            if(!_soundConfigs.TryGetValue(soundTypeId, out SoundConfig config))
                throw new ArgumentNullException($"The type {soundTypeId} is not registered.");
            
            return config;
        }

        public TWindow GetWindow<TWindow>() where TWindow : AbstractWindowBase
        {
            if(!_windows.TryGetValue(typeof(TWindow), out var window))
                throw new ArgumentNullException($"The type {typeof(TWindow).Name} is not registered.");
            
            return window as TWindow;
        }

        public SoundPlayerView GetSoundPlayerViewPrefab() => _soundPlayerView;
    }
}