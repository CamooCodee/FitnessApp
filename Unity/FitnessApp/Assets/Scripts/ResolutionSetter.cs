using UnityEngine;

[DefaultExecutionOrder(-1005)]
public class ResolutionSetter : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(574, 1020, false);
    }
}
