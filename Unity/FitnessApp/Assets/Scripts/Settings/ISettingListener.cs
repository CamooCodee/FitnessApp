﻿
namespace FitnessApp.Setting
{
    public interface ISettingListener<T> where T : ISettingsEventArgs
    {
        void Execute(T args);
    }
}