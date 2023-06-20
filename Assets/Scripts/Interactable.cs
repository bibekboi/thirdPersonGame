using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Add or remove InteractionEvent compontent to this gameObject.  
    public bool useEvents;
    //display message to users when looking at an interactable
    [SerializeField]
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        if(useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke(); 
        }
        Interact();
    }

    protected virtual void Interact()
    {
        // to be overwritten by subclasses
    }
}
