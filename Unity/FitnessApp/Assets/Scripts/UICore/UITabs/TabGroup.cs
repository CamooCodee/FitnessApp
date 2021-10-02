using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FitnessApp.UICore.UITabs
{
    [AddComponentMenu("Ui Tabs/Group - Tabs")]
    public class TabGroup : MonoBehaviour
    {
        readonly Dictionary<TabButton, int> _unsortedButtonPriorities = new Dictionary<TabButton, int>();
        private TabButton[] _sortedButtons;
        private readonly List<ITabSelectionListener> _listeners = new List<ITabSelectionListener>();

        private bool _buttonActionsAreInitialized = false;
        
        private void Start()
        {
            SortButtons();
            InitializeButtonActions();
            InvokeAllListeners(0);
        }

        public void AddButton(TabButton button, int priority)
        {
            if (_buttonActionsAreInitialized)
            {
                Debug.LogWarning("Buttons can only be added in Awake!");
                return;
            }
            if (button == null || priority < 0 || _unsortedButtonPriorities.ContainsKey(button) ||
                _unsortedButtonPriorities.ContainsValue(priority))
            {
                Debug.LogWarning($"Can't add button. The values were invalid. Button: '{button}', Priority'{priority}'");
                return;
            }

            _unsortedButtonPriorities.Add(button, priority);
        }

        void SortButtons()
        {
            if(_sortedButtons != null) return;
            
            _sortedButtons = new TabButton[_unsortedButtonPriorities.Count];
            
            for (int i = 0; i < _sortedButtons.Length; i++)
            {
                int lowestPriority = int.MaxValue;
                TabButton lowestPriorityButton = null;
                foreach (var buttonPriorityPair in _unsortedButtonPriorities)
                {
                    if (buttonPriorityPair.Value < lowestPriority && !_sortedButtons.Contains(buttonPriorityPair.Key))
                    {
                        lowestPriority = buttonPriorityPair.Value;
                        lowestPriorityButton = buttonPriorityPair.Key; 
                    }
                }
                if(lowestPriorityButton == null) continue;

                _sortedButtons[i] = lowestPriorityButton;
            }
        }

        void InitializeButtonActions()
        {
            for (var i = 0; i < _sortedButtons.Length; i++)
            {
                var iCopy = i;
                _sortedButtons[i].onClick.AddListener(delegate { InvokeAllListeners(iCopy); });
            }
        }

        public void InvokeAllListeners(int index)
        {
            for (var i = 0; i < _listeners.Count; i++)
            {
                if(_listeners[i] == null) continue;
                _listeners[i].SelectTab(index);
            }
        }

        public void AddListener(ITabSelectionListener listener)
        {
            if(listener != null && !_listeners.Contains(listener))
                _listeners.Add(listener);
        }
        
        public void RemoveListener(ITabSelectionListener listener)
        {
            if(listener != null && _listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}