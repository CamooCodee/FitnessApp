using ColorSchemes;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(DefaultColorSchemeSetting))]
[AddComponentMenu("")]
[DefaultExecutionOrder(0)]
public class AutoColorSchemeInitializer : MonoBehaviour
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
        Target.ReInitAll();
    }
#endif

}
