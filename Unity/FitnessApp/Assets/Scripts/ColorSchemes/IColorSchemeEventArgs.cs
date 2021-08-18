using FitnessApp.Setting;

namespace ColorSchemes
{
    public interface IColorSchemeEventArgs : ISettingsEventArgs
    {
        ColorScheme GetScheme();
    }
}