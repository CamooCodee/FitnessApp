using System;
using FitnessApp;
using FitnessAppAPI;
using TMPro;
using UnityEngine;
using Extensions = FitnessApp.Extensions;

namespace UIConcretes.Elements.Pause
{
    public class PauseElement : DefaultElement, IWorkoutElement
    {
        [SerializeField] private TMP_InputField valueField;
        [SerializeField] private TextMeshProUGUI valueDisplay;

        public int Length
        {
            get
            {
                if (Extensions.TryParseTime(valueField.text, out int intVal))
                    return intVal;
                else
                    throw new Exception("The value field content cannot be converted into an integer!");
            }
            set
            {
                valueField.onValueChanged.Invoke(value.ToString());
                valueDisplay.text = valueField.text;
            }
        }
        
        private void Awake()
        {
            valueDisplay.Require(this);
            valueField.Require(this);
            InitializeDropdown();
            ListenForOnHide(UpdateDisplayValue);
        }

        public override void Edit()
        {
            
        }

        public override void Copy() => InvokeEvent(onCopy, transform.GetSiblingIndex());
        public override void Delete() => InvokeEvent(onDelete, transform.GetSiblingIndex());

        public void ReadInto(SimpleWorkoutData data)
        {
            data.elements.Add(new PauseData(Length));
        }

        public void Populate(SimpleWorkoutData data)
        {
            var elements = data.elements.ToArray();

            if (!(elements[transform.GetSiblingIndex()] is PauseData elementData))
                throw new Exception("Failed to populate. The data was null or not a pause.");
            
            Length = elementData.length;
            Id = elementData.id;
        }

        void UpdateDisplayValue()
        {
            valueDisplay.text = valueField.text;
        }
    }
}