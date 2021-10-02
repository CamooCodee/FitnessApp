using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class TimeInputValidator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        private void Update()
        {
            inputField.caretPosition = inputField.text.Length;
        }

        private void LateUpdate()
        {
            _receivedNew = false;
        }

        private bool _receivedNew;
        private string _previousVal = "";
        
        public void OnNewValue(string text)
        {
            // Whenever the value gets set in code => can't work with the input character
            if (!inputField.isFocused)
            {
                var isAlreadyCorrectFormat = Extensions.CanBeParsedIntoTime(text);
                if (isAlreadyCorrectFormat)
                {
                    _previousVal = text;
                    inputField.text = _previousVal;
                }
                else
                {
                    Extensions.TryParseTime(text, out _previousVal);
                    inputField.text = _previousVal;
                }
            }
            
            if(_receivedNew) return;
            _receivedNew = true;

            if (text.Length > 0)
            {
                char ch = text[text.GetLastIndex()];

                if (!ch.IsDigit())
                {
                    inputField.text = _previousVal;
                    return;
                }
            }
            
            string input = GetInputByText(text);

            if (input.Length > 4)
            {
                inputField.text = _previousVal;
                return;
            }

            text = input.Length switch
            {
                0 => "00:00",
                1 => $"00:0{input}",
                2 => $"00:{input}",
                3 => $"0{input[0]}:{input[1]}{input[2]}",
                4 => $"{input[0]}{input[1]}:{input[2]}{input[3]}",
                _ => text
            };

            inputField.text = text;
            _previousVal = text;
        }

        string GetInputByText(string text)
        {
            if (text.Length == 0) return "";
            text = FitnessAppAPI.Extensions.Remove(text, ':');
            
            while (text.Length > 0 && text[0] == '0')
            {
                text = text.Remove(0, 1);
            }
            return text;
        }
    }
}