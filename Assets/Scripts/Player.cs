using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Collectors
{
    private void Start() {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform);
    }
}
