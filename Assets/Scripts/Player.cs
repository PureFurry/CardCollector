using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Collectors
{
    private void Start() {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform,5);
    }
    private void Update() {
        if (isTurn == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
