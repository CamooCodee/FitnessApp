using TMPro;
using UnityEngine;

namespace FitnessApp.Setting
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class WeightUnitDisplay : MonoBehaviour, ISettingListener<WeightUnitArgs>
    {
        private TextMeshProUGUI _target;

        private void Awake()
        {
            _target = GetComponent<TextMeshProUGUI>();
            _target.Require(this);
        }

        private void OnEnable() => WeightUnitSetting.Instance.AddListenerForSettingsUpdate(this);
        private void OnDisable() => WeightUnitSetting.Instance.RemoveListenerForSettingsUpdate(this);

        public void Execute(WeightUnitArgs args)
        {
            _target.text = args.unit;
        }
    }
}