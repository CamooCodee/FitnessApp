using System;
using UnityEngine;

namespace FitnessApp.ColorSchemes
{
    public abstract class ColorSchemeMonoBehaviour : MonoBehaviour
    {
        private static ColorScheme globalColorScheme;
        private static Action<ColorScheme> onGlobalColorSchemeChange;

        public static void SetGlobalColorScheme(ColorScheme scheme, bool force = false)
        {
            if(!force && globalColorScheme == scheme) return;
            globalColorScheme = scheme;
            onGlobalColorSchemeChange?.Invoke(scheme);
        }

        protected void Init()
        {
            onGlobalColorSchemeChange += SetColorScheme;
        }
        
        protected abstract void SetColorScheme(ColorScheme scheme);
    }
}