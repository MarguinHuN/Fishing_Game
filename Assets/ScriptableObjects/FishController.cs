using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishController : MonoBehaviour
{
    public FishData fishData;
    public int FishWorth;
    public int CookedFishWorth;
    public bool isCooked;
    public CampFire campFire;
    public bool canCook = false;

    void Start()
    {
        FishWorth = fishData.worth;
        CookedFishWorth = fishData.cookedWorth;
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && canCook)
        {
            isCooked = true;
            Material m_Material;
            m_Material = gameObject.GetComponent<Renderer>().material;
            m_Material.color = new Color(190f/255f, 118f/255f, 33f/255f, 1f);
            canCook = false;
        }
    }

    void OnCook()
    {
        if(!isCooked)
        {
            canCook = true;
        }
    }
}
