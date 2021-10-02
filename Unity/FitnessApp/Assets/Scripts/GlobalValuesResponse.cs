using System;
using UnityEngine;

namespace FitnessApp
{
    [DefaultExecutionOrder(-1001)]    
    public class GlobalValuesResponse : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
            GlobalValues.appIsRunning = true;
        }
        
        private void Update()
        {
            GlobalValues.appIsFullyRunning = true;
        }

        private void OnApplicationQuit()
        {
            GlobalValues.appIsRunning = false;
        }
    }
}