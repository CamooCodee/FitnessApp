using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace FitnessApp.UICore
{
    public class FpsDisplay : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        float _deltaTime;

        public float updateTime;
        private float _startUpdateTime;

        private void OnValidate()
        {
            updateTime = Mathf.Max(0f, updateTime);
        }

        private void Awake()
        {
            _startUpdateTime = updateTime;
        }

        void Update () 
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            
            if (updateTime <= 0f)
            {
                updateTime = _startUpdateTime;
                UpdateDisplay(fps);
                return;
            }

            updateTime -= Time.deltaTime;
        }

        void UpdateDisplay(float val)
        {
            fpsText.text = Mathf.Ceil(val).ToString(CultureInfo.InvariantCulture);
        }
    }
}