using System;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class TimerBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private TextMeshProUGUI timerDisplay;
        
        private float _prevTime;
        private float _time;
        private float _length;
        
        private bool _isRunning;

        [SerializeField] private AudioClip timerSound;
        [SerializeField] private AudioClip timerSoundFinal;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            timerDisplay.Require(this);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Require(this);
        }

        private void Update()
        {
            if(!_isRunning) return;
            HandleSounds();
            if (_time <= 0) return;
            
            UpdateDisplay();

            _prevTime = _time;
            _time -= Time.deltaTime;
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
            timerLength = Mathf.Max(1, timerLength);
            _time = timerLength;
            _length = timerLength;
            _prevTime = timerLength;
            _nextSound = Mathf.Min(timerLength, 3);
        }

        public void ResetListener()
        {
            _isRunning = false;
        }

        int _nextSound = 3;
        
        void HandleSounds()
        {
            if (_time > _nextSound || _prevTime < _nextSound) return;

            if (_nextSound == 0) 
                _audioSource.PlayOneShot(timerSoundFinal);
            else
                _audioSource.PlayOneShot(timerSound);
            
            _nextSound--;
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