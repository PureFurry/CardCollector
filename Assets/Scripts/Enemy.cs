using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : Collectors
{
    [SerializeField]GameObject playerMiddleField,playerLeftField,playerRightField;
    [SerializeField]GameObject enemyMiddleField,enemyLeftField,enemyRightField;
    List<Card> enemyDeck = new List<Card>();
    private void Start() {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform);
        FetchCards();
    }
    private void Update() {
        if (isTurn == true)
        {
            if (GameManager.Instance.currentEnemySupply >  0)
            {
                Debug.Log("While içi");
                Card playableCard = GetRandomCard();
                PlayCard(playableCard);

            }
            else if (GameManager.Instance.currentEnemySupply <=  0)
            {
                Debug.Log("if içi");
                GameManager.Instance.GiveTurn(ref GameManager.Instance.enemySupply,ref GameManager.Instance.currentEnemySupply,ref GameManager.Instance.turnCounter);
            }
            // foreach (Card card in enemyDeck)
            // {
            //     if (card.currentValue <= GameManager.Instance.currentEnemySupply && GameManager.Instance.currentEnemySupply > 0)
            //     {
            //         Card playableCard = GetRandomCard();
            //         PlayCard(playableCard);
            //     }
            //     else
            //     {
            //         GameManager.Instance.GiveTurn(ref GameManager.Instance.enemySupply,ref GameManager.Instance.currentEnemySupply,ref GameManager.Instance.turnCounter);
            //     }
            // }
        }
    } 

    void PlayCard(Card playedCard){
        
        if (playerLeftField.GetComponentsInChildren<Card>().Length > playerMiddleField.GetComponentsInChildren<Card>().Length + 2 ||playerLeftField.GetComponentsInChildren<Card>().Length > playerRightField.GetComponentsInChildren<Card>().Length + 2)
        {
            playedCard.transform.parent = enemyLeftField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyLeftField.GetComponent<FieldWar>().UpdatePowerText();
        }
        if (playerMiddleField.GetComponentsInChildren<Card>().Length > playerLeftField.GetComponentsInChildren<Card>().Length + 2 || playerMiddleField.GetComponentsInChildren<Card>().Length > playerRightField.GetComponentsInChildren<Card>().Length + 2)
        {
            playedCard.transform.parent = enemyMiddleField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyMiddleField.GetComponent<FieldWar>().UpdatePowerText();
        }
        if (playerRightField.GetComponentsInChildren<Card>().Length > playerLeftField.GetComponentsInChildren<Card>().Length + 2 || playerRightField.GetComponentsInChildren<Card>().Length > playerMiddleField.GetComponentsInChildren<Card>().Length + 2)
        {
            playedCard.transform.parent = enemyRightField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyRightField.GetComponent<FieldWar>().UpdatePowerText();
        }
        else{
            int ranodmNumber = Random.Range(0, 3);
            switch (ranodmNumber)
            {
                case 0:
                playedCard.transform.parent = enemyLeftField.transform;
                FetchCards();
                GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
                GameManager.Instance.UpdateEnemySupply();
                enemyLeftField.GetComponent<FieldWar>().UpdatePowerText();
                break;
                case 1:
                playedCard.transform.parent = enemyMiddleField.transform;
                FetchCards();
                GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
                GameManager.Instance.UpdateEnemySupply();
                enemyMiddleField.GetComponent<FieldWar>().UpdatePowerText();
                break;
                case 2:
                playedCard.transform.parent = enemyRightField.transform;
                FetchCards();
                GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
                GameManager.Instance.UpdateEnemySupply();
                enemyRightField.GetComponent<FieldWar>().UpdatePowerText();
                break;
            }

        }       
    }
    Card GetRandomCard(){
        Card tempPlayableCard;
        do
        {
            tempPlayableCard = enemyDeck[Random.Range(0,enemyDeck.Count)];
        } while (tempPlayableCard.currentValue <= GameManager.Instance.enemySupply && GameManager.Instance.enemySupply > 0);
       

        return tempPlayableCard;
            
            
    }
    void FetchCards(){
            enemyDeck.Clear();
            enemyDeck.AddRange(GetComponentsInChildren<Card>());
        }
}
