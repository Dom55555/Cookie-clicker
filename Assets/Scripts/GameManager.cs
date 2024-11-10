using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Globalization;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public TMP_Text AmountBasicMiners_Text;
    public TMP_Text BasicMinerPrice_Text;
    public TMP_Text StonesAmount_Text;
    public TMP_Text CPS_Text;

    AudioSource buySound;

    int basicMiners = 0;
    int basicMinerPrice;
    int goldenMiners = 0;
    int goldenMinerPrice;

    int stones;
    int cps;

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

        cps = basicMiners*368 + goldenMiners * 13578; // TEMP VALUES FOR FORMAT TEST

        if (clickTimer >= 1)
        {
            addStones(cps);
            clickTimer = 0;
        }
        if (saveTimer >= 10)
        {
            Save();
            saveTimer = 0;
        }

        StonesAmount_Text.text = digitFormat(stones);
        CPS_Text.text = digitFormat(cps) + " stones/sec";
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void BuyBasicMiner()
    {
        if(!(stones < basicMinerPrice))
        {
            buySound.Play();
            basicMiners++;
            stones -= basicMinerPrice;
            AmountBasicMiners_Text.text = basicMiners.ToString();
            basicMinerPrice = (int)(basicMinerPrice * 1.3);
            BasicMinerPrice_Text.text = "Price: " + digitFormat(basicMinerPrice);
        }
    }
    public void addStones(int amount = 1)
    {
        stones+= amount;
    }

    string digitFormat(int number)
    {
        string newNumber = "";
        int length = number.ToString().Length;
        if (length < 4)
        {
            return number.ToString();
        }
        int first = int.Parse(number.ToString().Substring(0, (length - 1) % 3+1));
        newNumber += first.ToString() + ".";
        int second = int.Parse(number.ToString().Substring((length-1)%3+1,2));
        newNumber += second.ToString();
        if(length < 7)
        {
            newNumber += "k";
        }
        else if(length < 10)
        {
            newNumber += "m";
        }
        return newNumber;
    }

    void Save()
    {
        PlayerPrefs.SetInt("stones", stones);
        PlayerPrefs.SetInt("basicMiners", basicMiners);
        PlayerPrefs.SetInt("basicMinerPrice",basicMinerPrice);
        PlayerPrefs.SetInt("goldenMiners",goldenMiners);
        PlayerPrefs.SetInt("goldenMinerPrice",goldenMinerPrice);
    }
    void Load()
    {
        stones = PlayerPrefs.GetInt("stones",0);
        basicMiners = PlayerPrefs.GetInt("basicMiners",0);
        basicMinerPrice = PlayerPrefs.GetInt("basicMinerPrice",50);
        goldenMiners = PlayerPrefs.GetInt("goldenMiners",0);
        goldenMinerPrice = PlayerPrefs.GetInt("goldenMinerPrice",500);

        AmountBasicMiners_Text.text = basicMiners.ToString();
        BasicMinerPrice_Text.text = "Price: " + digitFormat(basicMinerPrice);
    }

}
