using System;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UIConcretes.Screens.HomeExerciseList;
using FitnessApp.UIConcretes.Screens.HomeWorkoutList;
using UIConcretes.Elements.Workout;

namespace FitnessApp.UIConcretes.Screens
{
    public class ZToASorter : IExerciseArraySorter, IWorkoutArraySorter
    {
        public void Sort(ExerciseElement[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (String.Compare(array[i].ExerciseName, array[j].ExerciseName, StringComparison.Ordinal) < 0)
                    {
                        var tempRef = array[i];
                        array[i] = array[j];
                        array[j] = tempRef;
                    }
                }
                
                array[i].transform.SetSiblingIndex(i);
            }
        }
        
        public void Sort(WorkoutElement[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (String.Compare(array[i].WorkoutName, array[j].WorkoutName, StringComparison.Ordinal) < 0)
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