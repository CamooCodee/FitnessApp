using UnityEditor;
using static UnityEditor.AssetDatabase;
using static System.IO.File;
using static System.IO.Path;
using static UnityEngine.Application;

namespace MenuItems
{
    public static class Tools
    {
        [MenuItem("Tools/Reimport Core Dll")]
        static void ReImportCoreDll()
        {
            string destRoot = $"{dataPath}/API";
            string destMetaFile = "FitnessAppAPI.dll.meta";
            string destFile = "FitnessAppAPI.dll";
            
            string srcPath = @"X:\RiderProjects\FitnessAppCore\FitnessAppAPI\bin\Debug\FitnessAppAPI.dll";
            string destMetaFilePath = Combine(destRoot, destMetaFile);
            string destFilePath = Combine(destRoot, destFile);
            
            Delete(destMetaFilePath);
            Delete(destFilePath);
            Copy(srcPath, destFilePath);
            Refresh();
        }
    }
}