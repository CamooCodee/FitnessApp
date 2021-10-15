using System;
using TMPro;
using UnityEngine;

namespace FitnessApp.UICore
{
    public class ConfirmationPopup : MonoBehaviour
    {
        public static ConfirmationPopup instance;

        [SerializeField] private GameObject popUp;
        [SerializeField] private GameObject optionsBox;
        [SerializeField] private TextMeshProUGUI optionsHeader;
        [SerializeField] private TextMeshProUGUI optionsDescription;
        [SerializeField] private GameObject noOptionsBox;
        [SerializeField] private TextMeshProUGUI noOptionsHeader;
        [SerializeField] private TextMeshProUGUI noOptionsDescription;

        private event Action onCancel;
        private event Action onOk;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }
            
            popUp.SetActive(false);
        }

        public void PopUp(string headerContent, string descriptionContent, Action cancel, Action ok)
        {
            if (popUp.activeSelf) return;
            Activate(true);
            
            optionsBox.SetActive(true);
            noOptionsBox.SetActive(false);
            optionsHeader.text = headerContent;
            optionsDescription.text = descriptionContent;
            
            onCancel = null;
            onOk = null;
            if (cancel != null) onCancel += cancel;
            if (ok != null) onOk += ok;
        }
        
        public void PopUpNoOptions(string headerContent, string descriptionContent, Action ok)
        {
            if (popUp.activeSelf) return;
            Activate(true);
            
            noOptionsBox.SetActive(true);
            optionsBox.SetActive(false);
            noOptionsHeader.text = headerContent;
            noOptionsDescription.text = descriptionContent;
            
            onOk = null;
            if (ok != null) onOk += ok;
        }

        public void Cancel()
        {
            onCancel?.Invoke();
        }

        public void Ok()
        {
            onOk?.Invoke();
        }
        
        public void Close()
        {
            Activate(false);
        }

        void Activate(bool state)
        {
            if(!state) transform.SetAsFirstSibling();
            else transform.SetAsLastSibling();
            
            popUp.SetActive(state);
            gameObject.SetActive(state);
        }
    }
}