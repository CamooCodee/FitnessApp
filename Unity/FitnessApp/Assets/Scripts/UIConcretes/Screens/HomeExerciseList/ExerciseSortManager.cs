using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeExerciseList
{
    public class ExerciseSortManager : MonoBehaviour
    {
        private readonly IExerciseArraySorter _aToZ = new AToZSorter();
        private readonly IExerciseArraySorter _zToA = new ZToASorter();
        
        public void SetAToZ(MainExerciseList toSort)
        {
            toSort.SetSorter(_aToZ);
        }
        
        public void SetZToA(MainExerciseList toSort)
        {
            toSort.SetSorter(_zToA);
        }
    }
}