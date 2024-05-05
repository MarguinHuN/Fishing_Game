using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float CastCost;
    public float ChargeRate;

    private Coroutine recharge;

    public FishingRod fishingRod;
    public Chair chair;

    void Start()
    {
        StaminaBar.fillAmount = Stamina / MaxStamina;
    }

    void Update()
    {
        if (fishingRod.isCasted && Input.GetMouseButtonDown(0)) //checks if the player initiates a casting into the water, that way the stamina bar is only drained when the player throws in the bait
        {
            OnCasting();
        }

        if (chair.playerInRange && !fishingRod.isCasted && !fishingRod.isPulling) //checks if the player is not fishing, and is in range with the chair
        {
            OnSitting();
        }
        else
        {
            if (recharge != null)
            {
                Debug.Log("Stopping recharge");
                StopCoroutine(recharge);
                recharge = null;
            }
        }
    }

    private void OnCasting()
    {
        Debug.Log("The player have casted their bait, resulting in losing some stamina.");            
        
        Stamina -= CastCost;
        if (Stamina < 0) Stamina = 0; //not letting the stamina bar go beyond 0
        StaminaBar.fillAmount = Stamina / MaxStamina; //decreases the staminabar's vertical length according to the stamina loss
    }

    private void OnSitting()
    {
        if (recharge == null)
        {
            Debug.Log("Starting recharge");
            recharge = StartCoroutine(RechargeStamina());
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
