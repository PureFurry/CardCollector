using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collectors,IGetPower
{
    GameObject enemyFieldDropZone;
    List<Card> enemyDeck = new List<Card>();
    private void Awake() {
        enemyFieldDropZone = GameObject.Find("Enemy Field") as GameObject;
    }
    private void Start() {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform);
        enemyDeck.AddRange(GetComponentsInChildren<Card>());
    }
    private void Update() {
        if (isTurn == true)
        {
            int tempCardPower = 0;
            Card tempPlayableCard = new Card();
            bool isFirstLoop = true;
            for (int i = 0; i < enemyDeck.Count; i++)
            {
                if (tempCardPower <= enemyDeck[i].cardSO.cardDamage)
                {
                    if (isFirstLoop == true)
                    {
                        tempCardPower = enemyDeck[i].cardSO.cardDamage;
                        tempPlayableCard = enemyDeck[i];
                        isFirstLoop = false;
                    }
                    else
                    {
                        enemyDeck.Add(tempPlayableCard);
                        tempCardPower = enemyDeck[i].cardSO.cardDamage;
                        tempPlayableCard = enemyDeck[i];
                        enemyDeck.Remove(enemyDeck[i]);
                    }
                }
            }
            PlayCard(tempPlayableCard);
        }
    }
    void PlayCard(Card playedCard){
        int ranodmNumber = Random.Range(0, 100);
        Debug.Log(ranodmNumber);
        if (ranodmNumber < 50)
        {
            playedCard.CardFlip(false);
            playedCard.transform.parent = enemyFieldDropZone.transform;
            GameManager.Instance.UpgradeEnemyStats(GetPower());
            GameManager.Instance.GiveTurn();
        }
        else
        {
            playedCard.CardFlip(true);
            playedCard.transform.parent = enemyFieldDropZone.transform;
            // GameManager.Instance.UpgradeEnemyStats(GetPower(),GetHealth());
            GameManager.Instance.GiveTurn();
        }
        
    }
    // public int GetHealth()
    // {
    //     int tempHealth = 0;
    //     Card[] temp = enemyFieldDropZone.GetComponentsInChildren<Card>();
    //     for (int i = 0; i < temp.Length; i++)
    //     {
    //         if (temp[i].isCardOnBack == false)
    //         {
    //             tempHealth += temp[i].cardSO.cardHealth;
    //         }
    //     }
    //     return tempHealth;
    // }

    public int GetPower()
    {
        int tempPower = 0;
        Card[] temp = enemyFieldDropZone.GetComponentsInChildren<Card>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].isCardOnBack == false)
            {
                tempPower += temp[i].cardSO.cardDamage;
            }
        }
        return tempPower;
    }
}
