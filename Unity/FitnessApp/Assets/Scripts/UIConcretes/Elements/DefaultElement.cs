using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIConcretes.Elements
{
    public class DefaultElement : DropdownElementMono
    {
        public int Id { get; protected set; }

        protected readonly List<UnityEvent<int>> onDelete = new List<UnityEvent<int>>();
        protected readonly List<UnityEvent<int>> onCopy = new List<UnityEvent<int>>();
        protected readonly List<UnityEvent<int>> onEdit = new List<UnityEvent<int>>();
        
        #region Dropdown Functionallity
        
        public void ListenForDelete(UnityEvent<int> func) => onDelete.Add(func);
        public void ListenForEdit(UnityEvent<int> func) => onEdit.Add(func);
        public void ListenForCopy(UnityEvent<int> func) => onCopy.Add(func);

        public virtual void Delete() => InvokeEvent(onDelete);
        public virtual void Edit() => InvokeEvent(onEdit);
        public virtual void Copy() => InvokeEvent(onCopy);

        protected void InvokeEvent(List<UnityEvent<int>> onEvent)
        {
            InvokeEvent(onEvent, Id);
        }
        protected static void InvokeEvent(List<UnityEvent<int>> onEvent, int parameter)
        {
            for (var i = 0; i < onEvent.Count; i++)
            {
                for(int j = 0 ; j < onEvent[i].GetPersistentEventCount();j++ )
                {    
                    ((MonoBehaviour)onEvent[i].GetPersistentTarget(j))
                        .SendMessage(onEvent[i].GetPersistentMethodName(j), parameter);
                }
            }
        }
        
        #endregion
    }
}