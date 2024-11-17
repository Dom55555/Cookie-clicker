using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Globalization;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Timeline;
public class GameManager : MonoBehaviour
{
    public TMP_Text AmountBasicMiners_Text;
    public TMP_Text BasicMinerPrice_Text;
    public TMP_Text AmountGoldenMiners_Text;
    public TMP_Text GoldenMinerPrice_Text;
    public TMP_Text AmountUpgrades_Text;
    public TMP_Text UpgradePrice_Text;

    public TMP_Text StonesAmount_Text;
    public TMP_Text CPS_Text;

    public Button BasicMinerButton;
    public Button GoldenMinerButton;
    public Button UpgradeButton;

    int basicMiners;
    int basicMinerPrice;

    int goldenMiners;
    int goldenMinerPrice;

    int upgradesAmount;
    int upgradePrice;
    int clickPower;

    public int stones;
    int cps;

    float clickTimer;
    float saveTimer;

    void Start()
    {
        Load();
        clickPower = (int)(1 * Math.Pow(2,upgradesAmount));
    }

    private void Update()
    {
        clickTimer += Time.deltaTime;
        saveTimer += Time.deltaTime;

        cps = basicMiners*368 + goldenMiners * 13578; // TEMP VALUES FOR FORMAT TEST

        if (clickTimer >= 1)
        {
            stones += cps;
            clickTimer = 0;
        }
        if (saveTimer >= 10)
        {
            Save();
            saveTimer = 0;
        }

        BasicMinerButton.interactable = stones >= basicMinerPrice;
        GoldenMinerButton.interactable = stones >= goldenMinerPrice;
        UpgradeButton.interactable= stones >= upgradePrice;

        StonesAmount_Text.text = digitFormat(stones);
        CPS_Text.text = digitFormat(cps) + " stones/sec";
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void BuyBasicMiner()
    {
        basicMiners++;
        stones -= basicMinerPrice;
        AmountBasicMiners_Text.text = basicMiners.ToString();
        basicMinerPrice = (int)(basicMinerPrice * 1.3);
        BasicMinerPrice_Text.text = "Price: " + digitFormat(basicMinerPrice);
    }
    public void BuyGoldenMiner()
    {
        goldenMiners++;
        stones -= goldenMinerPrice;
        AmountGoldenMiners_Text.text = goldenMiners.ToString();
        goldenMinerPrice = (int)(goldenMinerPrice * 1.2);
        GoldenMinerPrice_Text.text = "Price: " + digitFormat(goldenMinerPrice);
    }
    public void BuyClickUpgrade()
    {
        upgradesAmount++;
        clickPower *= 2;
        stones -= upgradePrice;
        upgradePrice *= 4;
        UpgradePrice_Text.text = "Price: "+digitFormat(upgradePrice);
        AmountUpgrades_Text.text = upgradesAmount.ToString();
    }

    public void addStones()
    {
        stones += clickPower;
    }

    string digitFormat(int number)
    {
        string newNumber = "";
        int length = number.ToString().Length;
        if (length < 4)
        {
            return number.ToString();
        }
        int firstpart = int.Parse(number.ToString().Substring(0, (length - 1) % 3+1));
        int secondpart = int.Parse(number.ToString().Substring((length-1)%3+1 , 2));
        newNumber = firstpart + "." + secondpart;
        if(length < 7)
        {
            newNumber += "k";
        }
        else if(length < 10)
        {
            newNumber += "m";
        }
        StonesAmount_Text.fontSize = 150 - 4 * newNumber.Count();
        return newNumber;
    }

    void Save()
    {
        PlayerPrefs.SetInt("stones", stones);

        PlayerPrefs.SetInt("basicMiners", basicMiners);
        PlayerPrefs.SetInt("basicMinerPrice",basicMinerPrice);

        PlayerPrefs.SetInt("goldenMiners",goldenMiners);
        PlayerPrefs.SetInt("goldenMinerPrice",goldenMinerPrice);

        PlayerPrefs.SetInt("upgradesAmount", upgradesAmount);
        PlayerPrefs.SetInt("upgradePrice", upgradePrice);
    }
    void Load()
    {
        stones = PlayerPrefs.GetInt("stones",0);

        basicMiners = PlayerPrefs.GetInt("basicMiners",0);
        basicMinerPrice = PlayerPrefs.GetInt("basicMinerPrice",50);

        goldenMiners = PlayerPrefs.GetInt("goldenMiners",0);
        goldenMinerPrice = PlayerPrefs.GetInt("goldenMinerPrice",500);

        upgradesAmount = PlayerPrefs.GetInt("upgradesAmount", 0);
        upgradePrice = PlayerPrefs.GetInt("upgradePrice", 20);

        AmountBasicMiners_Text.text = basicMiners.ToString();
        AmountGoldenMiners_Text.text = goldenMiners.ToString();
        AmountUpgrades_Text.text = upgradesAmount.ToString();
        BasicMinerPrice_Text.text = "Price: " + digitFormat(basicMinerPrice);
        GoldenMinerPrice_Text.text = "Price: " + digitFormat(goldenMinerPrice);
        UpgradePrice_Text.text = "Price: " + digitFormat(upgradePrice);
    }

}
