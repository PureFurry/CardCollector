using UnityEngine;

public class Enemy : Collectors
{
    private void Start() {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform);
    }
}
