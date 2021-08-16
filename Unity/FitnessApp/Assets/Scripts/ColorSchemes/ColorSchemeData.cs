using System;

namespace ColorSchemes
{
    [Serializable]
    public class ColorSchemeData
    {
        public ColorScheme colorScheme;

        private ColorSchemeColor _color;
        public ColorSchemeColor Color
        {
            get
            {
                if(colorScheme == null) throw new NullReferenceException("The Color Scheme has to be set in order to receive a belonging color.");
                if (_color == null)
                {
                    _color = colorScheme.GetDefaultColor();
                }
                return _color;
            }
            set
            {
                if(!colorScheme.ContainsColor(value)) return;
                _color = value;
            }
        }
    }
}