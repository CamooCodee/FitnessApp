namespace FitnessApp
{
    public interface ISetting<T> where T : ISettingsEventArgs
    {
        public void AddListenerForSettingsUpdate(ISettingListener<T> listener);
        public void RemoveListenerForSettingsUpdate(ISettingListener<T> listener);
        public void RefreshInEditor();
    }
}