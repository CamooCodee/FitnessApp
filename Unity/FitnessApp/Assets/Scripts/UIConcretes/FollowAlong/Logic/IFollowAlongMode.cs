namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public interface IFollowAlongMode
    {
        void StartMode(SimpleWorkoutData workout);
        void CancelMode();
    }
}