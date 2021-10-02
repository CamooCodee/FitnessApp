﻿namespace ColorSchemes
{
    public interface IColorSchemeElement
    {
        ColorSchemeData GetData(int index);
        void OnSelectedColorUpdated();
        void CleanUp();
    }
}