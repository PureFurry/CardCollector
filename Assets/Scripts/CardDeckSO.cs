using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDeckSO", menuName = "CardDeck", order = 1)]
public class CardDeckSO : ScriptableObject {
    public List<CardSO> cardDeck;
}
 