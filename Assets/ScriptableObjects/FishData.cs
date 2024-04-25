using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FishData", menuName = "FishData", order = 1)]
public class FishData : ScriptableObject
{
    public string fishName;
    public int worth; //How much money it gives from the merchant $
    public int probability; //Chance to catch this fish in percentage %
}
