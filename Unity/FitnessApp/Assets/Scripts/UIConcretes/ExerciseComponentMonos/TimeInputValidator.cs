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

        private bool _receivedNew = false;
        private string _previousVal = "";
        
        public void OnNewValue(string text)
        {
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
            
            switch (input.Length)
            {
                case 0:
                    text = "00:00";
                    break;
                case 1:
                    text = $"00:0{input}";
                    break;
                case 2:
                    text = $"00:{input}";
                    break;
                case 3:
                    text = $"0{input[0]}:{input[1]}{input[2]}";
                    break;
                case 4:
                    text = $"{input[0]}{input[1]}:{input[2]}{input[3]}";
                    break;
            }

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