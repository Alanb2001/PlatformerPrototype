using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int coinsInLevel;
    private int coinsCollected;

    private void OnEnable()
    {
        Coin.OnCoinCollected += CoinCount;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCount;
    }

    private void Awake()
    {
        coinsInLevel = FindObjectsOfType<Coin>().Length;

        DontDestroyOnLoad(this);
    }
    
    IEnumerator LevelChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1); 
    }

    private void CoinCount()
    {
        coinsCollected++;
        
        if (coinsCollected == coinsInLevel)
        {
            StartCoroutine(LevelChange());
            print("Next level");
        }
    }
}
