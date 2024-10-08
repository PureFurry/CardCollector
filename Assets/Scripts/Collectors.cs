using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collectors : MonoBehaviour
{
    public bool isTurn;
    public CardDeckSO cardDeckSO;
    public List<CardSO> collectorDeck;
    public GameObject cardObject;
    public int listLenght;
    public bool isAttacking;
    private void Awake() {
        foreach (CardSO card in cardDeckSO.cardDeck)
        {
            if (card != null)
            {
                collectorDeck.Add(card);
            }
        }
    }
    private void Start() {
        
    }
    protected void ShuffleDeck(List<CardSO> _deck){
        List<CardSO> tempdeck;
        tempdeck = collectorDeck;
        // Fisher-Yates karıştırma algoritması kullanarak kartları karıştırın
        for (int i = _deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CardSO temp = _deck[i];
            _deck[i] = _deck[j];
            _deck[j] = temp;
            tempdeck.Add(_deck[j]);
        }
        collectorDeck = tempdeck;
    }
    public void DisplayCards(GameObject cardPrefab,Transform cardContainer,int drawTime){
        
        for (int i = 0; i < drawTime; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab,cardContainer.transform.position, Quaternion.identity);
            createdCard.TryGetComponent<Card>(out Card card);
            card.cardSO = collectorDeck[i];
            collectorDeck.Remove(collectorDeck[i]);
            createdCard.transform.SetParent(cardContainer.transform);
        }
    }
}
