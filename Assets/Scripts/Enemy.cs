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
        DisplayCards(cardObject, transform,5);
        FetchCards();
    }
    private void Update() {
        if (isTurn == true)
        {
            if (GameManager.Instance.currentEnemySupply >  0)
            {
                Debug.Log("enemy current SUpply Okey");
                Card playableCard = GetRandomCard();
                PlayCard(playableCard);

            }
            else if (GameManager.Instance.currentEnemySupply <=  0)
            {
                Debug.Log("enemy current SUpply Low");
                GameManager.Instance.GiveTurn(ref GameManager.Instance.enemySupply,ref GameManager.Instance.currentEnemySupply,ref GameManager.Instance.turnCounter);
            }
        }
    } 
    // Plays a card from the enemy's deck, determining the field to place it in based on the number of cards in each player field.
    // If a player field has significantly more cards than the others, the card is placed in the corresponding enemy field.
    // Otherwise, a random enemy field is chosen.
    // The card's value is subtracted from the enemy's current supply, and the supply is updated.
    // The power text of the chosen enemy field is also updated.
    void PlayCard(Card playedCard){
        
        if (playerLeftField.GetComponentsInChildren<Card>().Length > playerMiddleField.GetComponentsInChildren<Card>().Length + 2 ||playerLeftField.GetComponentsInChildren<Card>().Length > playerRightField.GetComponentsInChildren<Card>().Length + 2)
        {
            Debug.Log("Cokluktan gelen sol");
            playedCard.transform.parent = enemyLeftField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyLeftField.GetComponent<FieldWar>().UpdatePowerText();
            return;
        }
        if (playerMiddleField.GetComponentsInChildren<Card>().Length > playerLeftField.GetComponentsInChildren<Card>().Length + 2 || playerMiddleField.GetComponentsInChildren<Card>().Length > playerRightField.GetComponentsInChildren<Card>().Length + 2)
        {
            Debug.Log("Cokluktan gelen orta");
            playedCard.transform.parent = enemyMiddleField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyMiddleField.GetComponent<FieldWar>().UpdatePowerText();
            return;
        }
        if (playerRightField.GetComponentsInChildren<Card>().Length > playerLeftField.GetComponentsInChildren<Card>().Length + 2 || playerRightField.GetComponentsInChildren<Card>().Length > playerMiddleField.GetComponentsInChildren<Card>().Length + 2)
        {
            Debug.Log("Cokluktan gelen sağ");
            playedCard.transform.parent = enemyRightField.transform;
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            enemyRightField.GetComponent<FieldWar>().UpdatePowerText();
            return;
        }
        else{
            int ranodmNumber = Random.Range(0, 3);
            switch (ranodmNumber)
            {
                case 0:
                Debug.Log("Direkt sol");
                playedCard.transform.parent = enemyLeftField.transform;
                FetchCards();
                GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
                GameManager.Instance.UpdateEnemySupply();
                enemyLeftField.GetComponent<FieldWar>().UpdatePowerText();
                break;
                case 1:
                Debug.Log("Direkt orta");
                playedCard.transform.parent = enemyMiddleField.transform;
                FetchCards();
                GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
                GameManager.Instance.UpdateEnemySupply();
                enemyMiddleField.GetComponent<FieldWar>().UpdatePowerText();
                break;
                case 2:
                Debug.Log("Direkt sağ");
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
        Debug.Log(tempPlayableCard.cardValueText);

        return tempPlayableCard;
            
            
    }
    void FetchCards(){
            enemyDeck.Clear();
            enemyDeck.AddRange(GetComponentsInChildren<Card>());
        }
}
