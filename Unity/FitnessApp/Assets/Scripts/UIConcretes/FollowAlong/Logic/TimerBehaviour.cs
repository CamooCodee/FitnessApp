using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class TimerBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private TextMeshProUGUI timerDisplay;
        
        
        private float _time;
        private float _length;
        
        private bool _isRunning;
        
        private void Update()
        {
            if(!_isRunning) return;
            if (_time <= 0) return;

            _time -= Time.deltaTime;
            UpdateDisplay();
        }

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            var element = elements[current];
            if (element == null) return;
            
            var timerLength = GetTimerLength(element);
            if (timerLength == -1)
            {
                _isRunning = false;
                return;
            }

            _isRunning = true;
            _time = timerLength;
            _length = timerLength;
        }

        public void ResetListener()
        {
            _isRunning = false;
        }

        int GetTimerLength(IWorkoutDataElement element)
        {
            if (element is PauseData pause) return pause.length;
            else if (element is OffsetExerciseData ex)
            {
                for (var i = 0; i < ex.performance.Count; i++)
                    if (ex.performance[i].GetPerformanceType() == PerformanceType.Time)
                        return ((TimeComponentArgs) ex.performance[i]).Length;
            }

            return -1;
        }

        public void AddSeconds(int amount)
        {
            _time += amount;
            if (!_isRunning) UpdateDisplay();
        }

        public void ResetToOriginalLength()
        {
            _time = _length;
            if (!_isRunning) UpdateDisplay();
        }

        void UpdateDisplay()
        {
            int timeInt = Mathf.RoundToInt(_time);
            Extensions.ParseTime(timeInt, out string s);
            timerDisplay.text = s;
        }
        
        public void TogglePause()
        {
            _isRunning = !_isRunning;
        }
    }
}