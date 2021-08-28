using System.Collections.Generic;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.Domain
{
    public class MyFitnessDomain : FitnessAppMonoBehaviour
    {
        private List<IDomainCommand> _commandList = new List<IDomainCommand>();
        private bool _isRecording = false;
        
        public void Record()
        {
            if (_isRecording)
            {
                Debug.LogWarning("Domain is already recording.");
                return;
            }
            _commandList.Clear();
            _isRecording = true;
        }

        public void Record(IDomainCommand command)
        {
            if (!_isRecording)
            {
                Debug.LogWarning("Cannot record a command while the record mode isn't active.");
                return;
            }
            
            if(command == null) Debug.LogWarning("Cannot record command since it's null.");
            else if(!_commandList.Contains(command)) _commandList.Add(command);
            else Debug.LogWarning($"Cannot record command of type '{command.GetType().Name}'. It's already in the current stack.");
        }

        public void UndoRecord(IDomainCommand command)
        {
            if (!_isRecording)
            {
                Debug.LogWarning("Cannot undo a command record while the record mode isn't active.");
                return;
            }
            
            if(command == null) Debug.LogWarning("Cannot undo command since it's null.");
            else if(_commandList.Contains(command)) _commandList.Remove(command);
            else Debug.LogWarning($"Cannot undo command of type '{command.GetType().Name}'. It's not in the current stack.");
        }

        public void DeleteRecord()
        {
            if (!_isRecording)
            {
                Debug.LogWarning("Cannot delete what is recorded while the record mode isn't active.");
                return;
            }
            
            _commandList.Clear();
        }
        
        public void Replay()
        {
            if (!_isRecording)
            {
                Debug.LogWarning("Cannot replay. Did you forget to record?");
                return;
            }
            
            for (var i = 0; i < _commandList.Count; i++)
            {
                if(_commandList[i] != null) _commandList[i].Execute(AppAPI);
            }

            _isRecording = false;
        }
        
        public FitnessApiFacade PerformSingleAction()
        {
            if (_isRecording)
            {
                Debug.LogWarning("Cannot perform single actions while the record mode is active.");
                return new FitnessApiFacade();
            }

            return AppAPI;
        }

        [ContextMenu("Log Exercises")]
        public void LogExerciseList()
        {
            var exercises = AppAPI.GetExerciseData();

            foreach (var data in exercises)
            {
                string performanceString = "";
                
                for (var i = 0; i < data.performanceValues.Length; i++)
                {
                    performanceString += data.performance[i].GetPerformanceType().ToString() + ": ";
                    performanceString += data.performanceValues[i];
                    if (i < data.performanceValues.Length - 1) performanceString += ", ";
                }
                
                Debug.Log($"Name: '{data.name}', Performance '{performanceString}', Id: {data.id}");
            }
        }
    }
}