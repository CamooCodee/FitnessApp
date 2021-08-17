using System;

namespace ColorSchemes
{
    public class ColorSchemeDoesntContainColorException : Exception
    {
        public ColorSchemeDoesntContainColorException()
        {
            
        }
        public ColorSchemeDoesntContainColorException(string colorTag) : base($"The given tag was: '{colorTag}'")
        {
            
        }
    }
}