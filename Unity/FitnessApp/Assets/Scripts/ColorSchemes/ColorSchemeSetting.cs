using FitnessApp.ColorSchemes;
using UnityEngine;

[AddComponentMenu("UI/Color Schemes/Color Scheme Setting - ColorSchemes")]
[DefaultExecutionOrder(1)]
public class ColorSchemeSetting : MonoBehaviour
{
    [SerializeField] private ColorScheme defaultScheme;

    private static bool firstInstance = true;
    
    public void Awake()
    {
        if (firstInstance) firstInstance = false;
        else
        {
            Debug.LogWarning("You have two Color Scheme Settings in the scene. This is not recommended.", this);
        }
        ColorSchemeMonoBehaviour.SetGlobalColorScheme(defaultScheme, true);
    }
    
    [ContextMenu("Light Mode")]
    public void SetLightMode()
    {
        ColorSchemeMonoBehaviour.SetGlobalColorScheme(ColorScheme.Light);
    }
    
    [ContextMenu("Dark Mode")]
    public void SetDarkMode()
    {
        ColorSchemeMonoBehaviour.SetGlobalColorScheme(ColorScheme.Dark);
    }
}
