using System;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.ExerciseDetails
{
    public interface IVisualComponentFactory
    {
        public GameObject InstantiateComponent(GameObject componentPrefab, Transform holder, Action<GameObject> destroyAction);
    }
}