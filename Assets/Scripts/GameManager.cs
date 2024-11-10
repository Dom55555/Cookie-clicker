using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting.ReorderableList;
using System.Diagnostics;
public class GameManager : MonoBehaviour
{
    public TMP_Text basicAmountText;
    public TMP_Text basicPriceText;
    public TMP_Text stonesText;
    AudioSource buySound;

    int basicMiners = 0;
    int goldenMiners = 0;
    int stones;

    float clickTimer;
    float saveTimer;

    void Start()
    {
        buySound = GetComponent<AudioSource>();
        Load();
    }

    private void Update()
    {
        clickTimer += Time.deltaTime;
        saveTimer += Time.deltaTime;
        if (clickTimer >= 1)
        {
            for (int i = 0; i < basicMiners; i++)
            {
                addStones();
            }
            clickTimer = 0;
        }
        if (saveTimer >= 10)
        {
            Save();
            saveTimer = 0;
        }

        stonesText.text = stones.ToString();
        basicAmountText.text = basicMiners.ToString();
        basicPriceText.text = "Price: " + (Math.Ceiling(50*(Math.Pow(1.3,basicMiners)))).ToString();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void BuyBasicMiner()
    {
        int price = int.Parse(basicPriceText.text.Substring(7));
        if(!(int.Parse(stonesText.text)< price))
        {
            buySound.Play();
            basicMiners++;
            stones -= price;
        }
    }
    public void addStones()
    {
        stones++;
    }

    void Save()
    {
        PlayerPrefs.SetInt("stones", stones);
        PlayerPrefs.SetInt("basicMiners", basicMiners);
        PlayerPrefs.SetInt("goldenMiners",goldenMiners);
    }
    void Load()
    {
        stones = PlayerPrefs.GetInt("stones",0);
        basicMiners = PlayerPrefs.GetInt("basicMiners");
        goldenMiners = PlayerPrefs.GetInt("goldenMiners");
    }

}
