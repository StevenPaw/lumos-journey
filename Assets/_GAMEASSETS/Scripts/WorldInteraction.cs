using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour
{
    /// <summary>
    /// is called once per physics updat
    /// To make OnTriggerStay work we need:
    /// any Collider to be attached to the GO
    /// the other GameObject we collide with to also have any kind of Collider attached
    /// the other Gameobject to have a Rigidbody Component
    /// the collider of the other GameObject tp be marked as "Is Trigger"
    /// This method will be called as long as our collider is overlapping/inside the other GO's collider
    /// </summary>
    void OnTriggerStay(Collider interactedObject)
    {
        if (interactedObject.gameObject.tag == "InteractableObject" && Input.GetKey(KeyCode.E)) 
            //Checking if the Object can be picked up (checking with comparing Tags) and if the "E" Key is being pressed
        {
            GetInteraction(interactedObject);
        }  
    }

    void GetInteraction(Collider interactedObject)
    {
        Debug.Log(interactedObject.name + " could now be picked up"); //not sure how to proceed here, so I am checking wheather or not its even working
    }
}
