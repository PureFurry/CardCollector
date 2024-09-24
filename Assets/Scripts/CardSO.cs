using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardSO", menuName = "CardSO", order = 0)]
public class CardSO : ScriptableObject 
{
    public string cardName;
    public Sprite cardImage;
    public int cardValue;
    public int cardDamage;
    public int cardDefense;
    public CardRarity cardRarity;
}
public enum CardRarity
{
    COMMON,
    RARE,
    VERYRARE,
    EPIC,
    LEGENDARY
}

