using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public TMP_Text basicAmountText;
    public TMP_Text basicPriceText;
    public TMP_Text stonesText;
    AudioSource buySound;
    int basicMiners = 0;
    int stones = 0;

    void Start()
    {
        buySound = GetComponent<AudioSource>();
    }
    public void BuyBasicMiner()
    {
        int price = int.Parse(basicPriceText.text.Substring(7));
        if(!(int.Parse(stonesText.text)< price))
        {
            buySound.Play();
            basicMiners++;
            stones -= price;
            basicAmountText.text = basicMiners.ToString();
            stonesText.text = stones.ToString();
            basicPriceText.text = "Price: " + (Math.Ceiling(price * 1.3)).ToString();

        }
    }
    public void addStones()
    {
        stones++;
        stonesText.text = stones.ToString();
    }
}
