using System;
using UIConcretes.Elements.Workout;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeWorkoutList
{
    public class LeastRecentSorter : IWorkoutArraySorter
    {
        public void Sort(WorkoutElement[] array)
        {
            var orientationDate = DateTime.Now;
            
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (Mathf.Abs((orientationDate - array[j].LastSession).Seconds) >
                        Mathf.Abs((orientationDate - array[i].LastSession).Seconds))
                    {
                        var tempRef = array[i];
                        array[i] = array[j];
                        array[j] = tempRef;
                    }
                }
                
                array[i].transform.SetSiblingIndex(i);
            }
        }
    }
}