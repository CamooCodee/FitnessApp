using System;
using System.Collections.Generic;
using ColorSchemes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorScheme", menuName = "Scriptable Objects/Color Schemes/Scheme")]
public class ColorScheme : ScriptableObject
{
    public const string COLOR_SCHEMES_PATH = "ColorSchemes";

    [SerializeField] private List<ColorSchemeColor> colors = new List<ColorSchemeColor>();
    public int ColorCount => colors.Count;
    
    
    List<Color> _previousColors = new List<Color>();
    private void OnValidate()
    {
        bool colorsChanged = false;
        if (_previousColors.Count == 0) colorsChanged = true;
        var allTags = new List<string>();
        for (var i = 0; i < colors.Count; i++)
        {
            if (allTags.Contains(colors[i].tag))
            {
                Debug.LogError("Tag already exists. Choose a new one.");
                continue;
            }
            allTags.Add(colors[i].tag);

            if (colorsChanged == false && colors[i].color != _previousColors[i])
                colorsChanged = true;
        }

        if (colorsChanged && ColorSchemeMonoBehaviour.GlobalSetting != null)
            ColorSchemeMonoBehaviour.GlobalSetting.RefreshInEditor();

        _previousColors.Clear();
        for (var i = 0; i < colors.Count; i++)
        {
            _previousColors.Add(colors[i].color);
        }
    }

    private void Awake()
    {
        RenameDuplicateTags();
    }

    void RenameDuplicateTags()
    {
        var allTags = new List<string>();
        for (var i = 0; i < colors.Count; i++)
        {
            if (allTags.Contains(colors[i].tag))
            {
                Debug.LogWarning($"Changing tag {colors[i].tag}");
                string newTag = colors[i].tag + "1";
                colors[i].tag = newTag;
                allTags.Add(newTag);
                continue;
            }
            allTags.Add(colors[i].tag);
        }
    }
    
    public ColorSchemeColor GetDefaultColor()
    {
        if (colors.Count == 0) throw new EmptySchemeException();
        return colors[0];
    }

    public bool ContainsColor(ColorSchemeColor schemeColor)
    {
        for (var i = 0; i < colors.Count; i++)
        {
            if (colors[i].color == schemeColor.color)
            {
                return true;
            }
        }

        return false;
    }

    public ColorSchemeColor GetColor(int index)
    {
        return colors[index];
    }
    public ColorSchemeColor GetColor(string keyTag)
    {
        for (var i = 0; i < colors.Count; i++)
        {
            if (colors[i].tag == keyTag) return colors[i];
        }
        
        throw new Exception($"Can't find color with tag: '{keyTag}'");
    }
}