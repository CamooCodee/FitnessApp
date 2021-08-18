using FitnessApp.Setting;
using UnityEngine;

namespace ColorSchemes
{
    [DefaultExecutionOrder(-1)]
    public abstract class ColorSchemeMonoBehaviour : MonoBehaviour, ISettingListener<IColorSchemeEventArgs>
    {
        public static ISetting<IColorSchemeEventArgs> GlobalSetting { get; private set; }
        private bool _canBeAutoAssigned = true;

        private void Awake()
        {
            _canBeAutoAssigned = false;
            GlobalSetting = null;
        }

        protected virtual void Setup()
        {
            ListenForSettingsUpdate();
            _canBeAutoAssigned = false;
        }
        protected virtual void Teardown()
        {
            StopListeningForSettingsUpdated();
            _canBeAutoAssigned = true;
        }

        public static bool TryToActAsSettingsObject(ISetting<IColorSchemeEventArgs> setting, bool forceOverwrite = false)
        {
            if (GlobalSetting == null || forceOverwrite)
            {
                bool ret = false;
                
                if (GlobalSetting == null || GlobalSetting.Equals(setting)) ret = true;

                GlobalSetting = setting;
                return ret;
            }

            if (GlobalSetting.Equals(setting)) return true;
            return false;
        }

        protected void ListenForSettingsUpdate()
        {
            if (GlobalSetting != null)
            {
                GlobalSetting.AddListenerForSettingsUpdate(this);
            }
            else Debug.LogWarning("Didn't add listener due to missing setting. There should be ONE settings object in the scene. You can find it at 'Add Component > Color Schemes > Setting - Color Schemes'.");
        }
        protected void StopListeningForSettingsUpdated()
        {
            if (GlobalSetting != null)
            {
                GlobalSetting.RemoveListenerForSettingsUpdate(this);
            }
            else Debug.LogWarning("Didn't remove listener due to missing setting. There should be ONE settings object in the scene. You can find it at 'Add Component > Color Schemes > Setting - Color Schemes'.");
        }

        public abstract void Execute(IColorSchemeEventArgs args);
        public bool IsMonoBehaviour()
        {
            return true;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public virtual bool CanBeAutoAssigned() => _canBeAutoAssigned;
    }
}