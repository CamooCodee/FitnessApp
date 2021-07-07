using System.Collections.Generic;
using UnityEngine;

namespace FitnessApp.BackEnd
{
    public class Workout
    {
        public List<IWorkoutElement> Elements { get; private set; }
        public List<IFollowAlongElement> FollowAlongElements { get; private set; }

        public Workout()
        {
            Elements = new List<IWorkoutElement>();
            FollowAlongElements = null;
        }

        public void AddElements(IWorkoutElement element)
        {
            if(Elements.Contains(element)) Debug.Log("Added same element twice");
            Elements.Add(element);
        }
        public void RemoveElements(IWorkoutElement element)
        {
            if(!Elements.Contains(element)) return;
            Elements.Remove(element);
        }
    }
}