using UnityEngine;


[CreateAssetMenu(fileName = "CardSO", menuName = "CardSO", order = 0)]
public class CardSO : ScriptableObject 
{
    public string cardName;
    public Sprite cardImage;
    public int cardValue;
    public int cardDamage;
    public int cardHealth;
    public CardRarity cardRarity;
    public CardType cardType;
}
public enum CardRarity
{
    COMMON,
    RARE,
    VERYRARE,
    EPIC,
    LEGENDARY
}
public enum CardType
{
    FIRE,
    ROCK,
    WATER,
    DARK,
    LIGHT
}
