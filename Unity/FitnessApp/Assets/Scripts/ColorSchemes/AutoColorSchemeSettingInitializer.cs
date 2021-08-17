using ColorSchemes;
using UnityEngine;

[ExecuteAlways]
[AddComponentMenu("")]
[DefaultExecutionOrder(0)]
public class AutoColorSchemeSettingInitializer : MonoBehaviour
{
    private DefaultColorSchemeSetting _target = null;

    private DefaultColorSchemeSetting Target
    {
        get
        {
            if (_target == null) _target = GetComponent<DefaultColorSchemeSetting>();
            return _target;
        }
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        this.hideFlags = HideFlags.HideInInspector;
        if (Target == null) return;
        Target.ReInitAll();
    }
#endif
    
    public void DestroyAll()
    {
#if UNITY_EDITOR
        DestroyImmediate(Target);
        DestroyImmediate(this);
#endif
    }
}
