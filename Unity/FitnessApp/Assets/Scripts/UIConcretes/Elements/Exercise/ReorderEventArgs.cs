namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ReorderEventArgs
    {
        public readonly int oldIndex;
        public readonly int newIndex;

        public ReorderEventArgs(int oldIndex, int newIndex)
        {
            this.oldIndex = oldIndex;
            this.newIndex = newIndex;
        }
    }
}