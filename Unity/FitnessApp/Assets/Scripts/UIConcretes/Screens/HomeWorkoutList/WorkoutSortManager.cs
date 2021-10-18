using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeWorkoutList
{
    public class WorkoutSortManager : MonoBehaviour
    {
        private readonly IWorkoutArraySorter _aToZ = new AToZSorter();
        private readonly IWorkoutArraySorter _zToA = new ZToASorter();
        private readonly IWorkoutArraySorter _mostRecent = new MostRecentSorter();
        private readonly IWorkoutArraySorter _leastRecent = new LeastRecentSorter();
        
        public void SetAToZ(MainWorkoutList toSort)
        {
            toSort.SetSorter(_aToZ);
        }
        
        public void SetZToA(MainWorkoutList toSort)
        {
            toSort.SetSorter(_zToA);
        }
        
        public void SetMostRecent(MainWorkoutList toSort)
        {
            toSort.SetSorter(_mostRecent);
        }
        
        public void SetLeastRecent(MainWorkoutList toSort)
        {
            toSort.SetSorter(_leastRecent);
        }
    }
}