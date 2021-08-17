using System;
using UnityEngine;

[Serializable]
public class ColorSchemeColor
{
    public string tag;
    public Color color = default;
    
    public ColorSchemeColor()
    {
        
    }

    public ColorSchemeColor(Color defaultColor)
    {
        color = defaultColor;
    }
}