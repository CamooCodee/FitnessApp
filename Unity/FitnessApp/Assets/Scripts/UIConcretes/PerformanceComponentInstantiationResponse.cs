using System;
using FitnessApp.UICore;
using UnityEngine;

namespace FitnessApp.UIConcretes
{
    public class PerformanceComponentInstantiationResponse : MonoBehaviour, IVisualComponentFactory
    {
        public GameObject InstantiateComponent(GameObject componentPrefab, Transform holder, Action<GameObject> destroyAction)
        {
            if (componentPrefab == null)
            {
                Debug.LogWarning("The given prefab was null. Cannot instantiate.");
                return null;
            }

            GameObject instance;

            if (holder == null) instance = Instantiate(componentPrefab);
            else instance = Instantiate(componentPrefab, holder);
            
            var buttonReference = instance.GetComponent<RuntimeElementButtonReference>();
            if (buttonReference == null)
            {
                Debug.LogWarning("A Visual Performance Component should have a button reference.");
                return null;
            }
            
            var button = buttonReference.Button;
            if (button == null)
            {
                Debug.LogWarning("Found reference was empty");
                return null;
            }
            
            button.onClick.AddListener(delegate { destroyAction.Invoke(instance); });
            
            return instance;
        }
    }
}