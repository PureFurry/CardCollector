using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class Collectors : MonoBehaviour
{
    public bool isTurn;
    protected List<CardSO> collectorDeck;
    protected GameObject cardObject;
    [SerializeField]protected int listLenght;
    private void Awake() {
        cardObject = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Card.prefab",typeof(CardSO)) as GameObject;
    }
    protected void ShuffleDeck(List<CardSO> _deck){
        // Fisher-Yates karıştırma algoritması kullanarak kartları karıştırın
        for (int i = _deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CardSO temp = _deck[i];
            _deck[i] = _deck[j];
            _deck[j] = temp;
            collectorDeck.Add(_deck[j]);
        }
    }
    public void DisplayCards(GameObject cardPrefab,Transform cardContainer){
        for (int i = 0; i < listLenght; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab,cardContainer.transform.position, Quaternion.identity);
            createdCard.TryGetComponent<Card>(out Card card);
            card.cardSO = collectorDeck[i];
            createdCard.transform.parent = cardContainer.transform;
        }
    }
}
