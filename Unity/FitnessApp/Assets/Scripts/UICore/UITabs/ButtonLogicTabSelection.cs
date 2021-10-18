using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FitnessApp.UICore.UITabs
{
    [AddComponentMenu("Ui Tabs/Button Logic Swapper - Tabs")]
    [RequireComponent(typeof(Button))]
    public class ButtonLogicTabSelection : MonoBehaviour, ITabSelectionListener
    {
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private UnityEvent[] buttonActions;
        private Button _target;

        private int _selected;
        
        private void Awake()
        {
            _target = GetComponent<Button>();
            _target.Require(this);
            _target.onClick.AddListener(OnTargetClick);

            tabGroup.Require(this);
            tabGroup.AddListener(this);
        }

        public void SelectTab(int index)
        {
            _selected = index;
        }

        void OnTargetClick()
        {
            buttonActions[_selected].Invoke();
        }
    }
}