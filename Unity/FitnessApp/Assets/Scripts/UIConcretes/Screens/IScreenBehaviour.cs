using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.Screens
{
    public interface IScreenBehaviour<T> where T : Screen
    {
        /// <summary>
        /// Called on program start. Used for general screen event setup.
        /// </summary>
        public void Initialize(T screen);
        /// <summary>
        /// Called whenever the screen opens. Used for screen specific event setup.
        /// </summary>
        public void OnScreenOpen(T screen);
    }
}