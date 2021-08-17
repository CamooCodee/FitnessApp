using UnityEngine;

namespace ColorSchemes
{
    public interface IColorSchemeElement
    {
        ColorSchemeData GetData(int index);
        void OnSelectedColorUpdated();
    }
}