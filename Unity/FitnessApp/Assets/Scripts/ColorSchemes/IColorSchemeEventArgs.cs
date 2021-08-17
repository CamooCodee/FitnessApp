using FitnessApp;

namespace ColorSchemes
{
    public interface IColorSchemeEventArgs : ISettingsEventArgs
    {
        ColorScheme GetScheme();
    }
}