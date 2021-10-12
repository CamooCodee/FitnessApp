using System.Collections.Generic;
using IWorkoutElement = UIConcretes.Elements.IWorkoutElement;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public interface IWorkoutElementList
    {
        // Used by buttons
        public void AddExercise(int id);
        public void AddPause();
        public void CopyElement(int index);
        public void RemoveElement(int index);
        public IWorkoutElement GetElementAt(int index);
        public IWorkoutElement GetLastElement();
        public IEnumerable<IWorkoutElement> GetElements();
        // Used by buttons
        public void UpdateList();
        public void Clear();
    }
}