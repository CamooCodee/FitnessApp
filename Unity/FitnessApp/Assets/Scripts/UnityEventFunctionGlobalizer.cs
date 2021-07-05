using System;
using UnityEngine;

namespace FitnessApp
{
    public class UnityEventFunctionGlobalizer : MonoBehaviour
    {
        public static event Action GlobalUpdate;
        
        public void Update()
        {
            InvokeGlobalUpdate();
        }

        void InvokeGlobalUpdate()
        {
            GlobalUpdate?.Invoke();
        }
    }
}