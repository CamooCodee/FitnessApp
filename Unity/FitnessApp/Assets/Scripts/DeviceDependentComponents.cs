using UnityEngine;

namespace DefaultNamespace
{
    [DefaultExecutionOrder(-1000)]
    public class DeviceDependentComponents : MonoBehaviour
    {
        [SerializeField] private Component mobileComponent;
        [SerializeField] private Component desktopComponent;

        private static bool IsMobile {
            get
            {
#if !UNITY_EDITOR
                return Application.platform == RuntimePlatform.Android ||
                       Application.platform == RuntimePlatform.IPhonePlayer;
#endif

                return false;
            }
        } 

        private static bool IsDesktop
        {
            get
            {
#if !UNITY_EDITOR
                return Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.OSXPlayer;
#endif

                return true;
            }
        }

        private void Awake()
        {
            if (IsMobile)
            {
                if(desktopComponent == null) return;
                Destroy(desktopComponent);
            }
            else if (IsDesktop)
            {
                if(mobileComponent == null) return;
                Destroy(mobileComponent);
            }
        }
    }
}