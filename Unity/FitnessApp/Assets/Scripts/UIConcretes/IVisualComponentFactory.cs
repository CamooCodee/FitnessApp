using System;
using UnityEngine;

namespace FitnessApp.UIConcretes
{
    public interface IVisualComponentFactory
    {
        public GameObject InstantiateComponent(GameObject componentPrefab, Transform holder, Action<GameObject> destroyAction);
    }
}