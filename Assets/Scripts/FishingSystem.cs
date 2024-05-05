using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaterSource {
    Lake
}

public class FishingSystem : MonoBehaviour
{
    public static FishingSystem Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public List<FishData> lakeFishList;

    public bool isThereABite;
    bool hasPulled;

    public static event Action OnFishingEnd;

    public GameObject minigame;

    internal void StartFishing(WaterSource waterSource)
    {
        StartCoroutine(FishingCoroutine(waterSource));
    }

    IEnumerator FishingCoroutine(WaterSource waterSource)
    {
        yield return new WaitForSeconds(5f);

        FishData fish = CalculateBite(waterSource);

        if (fish.fishName == "NoBite")
        {
            Debug.LogWarning("No fish caught");
            EndFishing();
        }
        else
        {
            Debug.LogWarning(fish.fishName + " is biting");
            StartCoroutine(StartFishStruggle(fish));
        }
    }

    IEnumerator StartFishStruggle(FishData fish)
    {
        isThereABite = true;

        while (!hasPulled)
        {
            yield return null;
        }

        Debug.LogWarning("Start Minigame");
        StartMinigame();
    }

    private void StartMinigame()
    {
        minigame.SetActive(true);
    }

    public void SetHasPulled()
    {
        hasPulled = true;
    }

    private void EndFishing()
    {
        isThereABite = false;
        hasPulled = false;

        //Trigger end fishing event
        OnFishingEnd?.Invoke();
    }

    private FishData CalculateBite(WaterSource waterSource)
    {
        List<FishData> availableFish = GetAvailableFish(waterSource);

        //Calculate total probability
        float totalProbability = 0f;
        foreach (FishData fish in availableFish)
        {
            totalProbability += fish.probability;
        }

        //Generate random number between 0 and total probability
        int randomValue = UnityEngine.Random.Range(0, Mathf.FloorToInt(totalProbability) + 1);
        Debug.Log("Random value is " + randomValue);

        //Loop through the fish and check if the random number falls into their probability range
        float cumulativeProbability = 0f;
        foreach(FishData fish in availableFish)
        {
            cumulativeProbability += fish.probability;
            if (randomValue <= cumulativeProbability)
            {
                //The fish is biting
                return fish;
            }
        }

        //This should never happen - random value out of bounds
        return null;
    }

    private List<FishData> GetAvailableFish(WaterSource waterSource)
    {
        //Here we can define cases upon different water sources if we make some, but because there is only one water source in the game (which is hard coded), we only return the only list we've created
        return lakeFishList;
    }

    internal void EndMinigame(bool success)
    {
        minigame.gameObject.SetActive(false);

        if (success)
        {
            EndFishing();
        }
        else
        {
            EndFishing();
        }
    }
}
