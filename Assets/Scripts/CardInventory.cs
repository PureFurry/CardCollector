using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public static CardInventory Instance;
    public List<CardSO> cardList; 
    public Transform cardContainer;
    public GameObject cardPrefab;
    public int listLenght;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;            
        }
        CreateInventory();
    }
    public void CreateInventory(){
        for (int i = 0; i < listLenght; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab,cardContainer.transform.position, Quaternion.identity);
            createdCard.TryGetComponent<Card>(out Card card);
            card.cardSO = cardList[i];
            createdCard.transform.parent = cardContainer.transform;
        }
    }


}
