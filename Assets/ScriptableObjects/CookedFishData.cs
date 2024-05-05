using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookedFishData", menuName = "CookedFishData", order = 2)]
public class CookedFishData : ScriptableObject
{
    public string cookedFishName;
    public int cookedWorth; //How much money it gives from the merchant $
    public float staminaRefill; //How much stamina energy will it refill
}
