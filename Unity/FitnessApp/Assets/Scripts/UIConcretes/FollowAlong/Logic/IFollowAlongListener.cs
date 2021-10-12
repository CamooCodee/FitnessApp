using FitnessAppAPI;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public interface IFollowAlongListener
    {
        public void OnNewElement(IWorkoutDataElement[] elements, int current);
        public void ResetListener();
    }
}