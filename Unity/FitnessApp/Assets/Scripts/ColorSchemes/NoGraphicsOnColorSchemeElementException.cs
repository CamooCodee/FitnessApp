using System;

namespace ColorSchemes
{
    public class NoGraphicsOnColorSchemeElementException : Exception
    {
        public NoGraphicsOnColorSchemeElementException(string objectName) : base(objectName)
        {
            
        }
    }
}