using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Sound;
using CodeBase.UI.Sound.Configs;

namespace CodeBase.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        SoundConfig GetSoundConfig(SoundTypeId soundTypeId);
        TWindow GetWindow<TWindow>() where TWindow : AbstractWindowBase;
        SoundPlayerView GetSoundPlayerViewPrefab();
    }
}