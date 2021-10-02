using System;
using System.Collections.Generic;
using ColorSchemes;
using FitnessApp;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorScheme", menuName = "Scriptable Objects/Color Scheme")]
public class ColorScheme : ScriptableObject
{
    public const string COLOR_SCHEMES_PATH = "ColorSchemes";

    [SerializeField] private List<ColorSchemeColor> colors = new List<ColorSchemeColor>();
    public int ColorCount => colors.Count;
    
    
    List<Color> _previousColors = new List<Color>();
    private void OnValidate()
    {
        bool colorsChanged = false;
        if (_previousColors.Count != colors.Count)
        {
            RenameDuplicateTags(logRename: false);
            colorsChanged = true;
        }
        if (_previousColors.Count == 0) colorsChanged = true;
        
        var allTags = new List<string>();

        for (var i = 0; i < colors.Count; i++)
        {
            if (allTags.Contains(colors[i].tag))
            { 
                Debug.LogError($"Tag '{colors[i].tag}' on '{name}' already exists. Choose a new one."); 
                continue;
            } 
            allTags.Add(colors[i].tag);
            
            if (!colorsChanged && colors[i].color != _previousColors[i]) 
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

    private void OnEnable()
    {
        RenameDuplicateTags();
    }
    
    void RenameDuplicateTags(bool logRename = true)
    {
        var allTags = new List<string>();
        for (var i = 0; i < colors.Count; i++)
        {
            if (colors[i].tag.IsNullOrWhitespace()) colors[i].tag = "Tag";
            while (allTags.Contains(colors[i].tag))
            {
                string newTag = colors[i].tag + "1";
                if(logRename) Debug.LogWarning($"Changing tag '{colors[i].tag}' on '{name}' to '{newTag}'!");
                colors[i].tag = newTag;
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
        if (colors.Count == 0) return new ColorSchemeColor(Color.black);
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