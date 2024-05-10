using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellFish : MonoBehaviour
{
    public bool playerInRange = false;
    public GameObject text;
    public TMP_Text MoneyText;
    public PickUp pickUp;
    public int currentMoney = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pickUp.CurrentObject)
        {
            playerInRange = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            text.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInRange)
        {
            FishController fishController = pickUp.CurrentObject.GetComponent<FishController>();
            if(!fishController)
            {
                return;
            }
            if (!fishController.isCooked)
            {
                currentMoney += fishController.FishWorth;
            }
            else
            {
                currentMoney += fishController.CookedFishWorth;
            }
            MoneyText.SetText("Money: " + currentMoney.ToString() + "$");
            Destroy(pickUp.CurrentObject);
            pickUp.CurrentObject = null;
        }
    }
}
