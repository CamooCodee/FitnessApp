using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FitnessApp.UICore.UITabs
{
    [AddComponentMenu("Ui Tabs/Button - Tabs")]
    public class TabButton : Button
    {
        private TabGroup _group;
        
        protected override void Awake()
        {
            base.Awake();
            if(!GlobalValues.appIsRunning) return;

            InitGroup();
            _group.AddButton(this, transform.GetSiblingIndex());
            onClick.AddListener(DeselectThis);
        }

        void InitGroup()
        {
            if(_group == null) TryFindingGroupInParents();
            var groupIsFound = _group.Require(this, false);
            if (!groupIsFound)
            {
                Debug.LogError($"Cannot find group for '{this.name}'. This button has to be a child of it.");
                Destroy(this);
            }
        }
        
        void TryFindingGroupInParents()
        {
            var currentParent = transform.parent;
            
            while (currentParent != null)
            {
                _group = currentParent.GetComponent<TabGroup>();
                if(_group != null) return;

                currentParent = currentParent.parent;
            }
        }

        void DeselectThis()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                Debug.LogWarning("No event system found");
                return;
            }
            
            eventSystem.SetSelectedGameObject(null);
        }
    }
}