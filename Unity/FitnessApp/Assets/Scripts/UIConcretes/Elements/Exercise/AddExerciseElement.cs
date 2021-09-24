using System.Collections.Generic;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class AddExerciseElement : ExerciseElement
    {
        private readonly List<UnityEvent<int>> _onAdd = new List<UnityEvent<int>>();

        public void ListenForAdd(UnityEvent<int> func) => _onAdd.Add(func);
        public void Add() => InvokeEvent(_onAdd);
    }
}