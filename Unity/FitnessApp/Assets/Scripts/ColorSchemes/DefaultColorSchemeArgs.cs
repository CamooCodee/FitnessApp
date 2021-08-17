using System;

namespace ColorSchemes
{
    public class DefaultColorSchemeArgs : IColorSchemeEventArgs
    {
        private ColorScheme _scheme;

        public DefaultColorSchemeArgs(ColorScheme scheme)
        {
            if(scheme == null) throw new ArgumentException("Scheme can not be null!", $"{nameof(scheme)}");
            _scheme = scheme;
        }

        public ColorScheme GetScheme()
        {
            return _scheme;
        }
    }
}