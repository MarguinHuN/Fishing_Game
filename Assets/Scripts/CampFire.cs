using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public bool playerInRange = false;
    public GameObject text;
    public PickUp pickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pickUp.CurrentObject)
        {
            pickUp.CurrentObject.BroadcastMessage("OnCook", true, SendMessageOptions.DontRequireReceiver);
            playerInRange = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && pickUp.CurrentObject)
        {
            playerInRange = false;
            text.SetActive(false);
        }
    }
}
