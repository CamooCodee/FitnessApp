using System.Collections.Generic;
using FitnessApp.UIConcretes.Elements.Exercise;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public interface IWorkoutElementList
    {
        // Used by buttons
        public void AddExercise(int id);
        public void CopyExercise(int index);
        public void RemoveExercise(int index);
        public WorkoutExerciseElement GetExerciseElementAt(int index);
        public WorkoutExerciseElement GetLastElement();
        public IEnumerable<WorkoutExerciseElement> GetElements();
        // Used by buttons
        public void UpdateList();
        public void Clear();
    }
}