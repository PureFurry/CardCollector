using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class Enemy : Collectors
{
    [SerializeField] private GameObject playerMiddleField, playerLeftField, playerRightField;
    [SerializeField] private GameObject enemyMiddleField, enemyLeftField, enemyRightField;
    private List<Card> enemyDeck = new List<Card>();
    public bool isPlaying = false;

    private void Start()
    {
        ShuffleDeck(collectorDeck);
        DisplayCards(cardObject, transform, 5);
        FetchCards();
    }

    private void Update()
    {
        if (!isTurn || isPlaying) return; // Kart oynanırken işlem yapılmaması için kontrol.

    if (GameManager.Instance.currentEnemySupply > 0)
    {
        Card playableCard = GetRandomCard();
        if (playableCard != null && GameManager.Instance.currentEnemySupply > 0)
        {
            isPlaying = true; // Kart oynandığı sırada diğer işlemleri engelle.
            PlayCard(playableCard);
        }
    }
    else
    {
        GameManager.Instance.GiveTurn(ref GameManager.Instance.enemySupply, ref GameManager.Instance.currentEnemySupply, ref GameManager.Instance.turnCounter);
    }
    }

    private void PlayCard(Card playedCard)
    {
        Transform targetField = GetTargetField();

        playedCard.transform.DOMove(transform.position, 0.2f).OnComplete(()=> playedCard.transform.DOScale(new Vector2(playedCard.transform.localScale.x + 1,playedCard.transform.localScale.y + 1),0.2f).OnComplete(()=> playedCard.transform.DOScale(new Vector2(playedCard.transform.localScale.x - 1,playedCard.transform.localScale.y - 1),0.2f))).OnComplete(() =>
        {
            playedCard.transform.SetParent(targetField);
            FetchCards();
            GameManager.Instance.currentEnemySupply -= playedCard.currentValue;
            GameManager.Instance.UpdateEnemySupply();
            targetField.GetComponent<FieldWar>().UpdatePowerText();
            
            // Kart oynandıktan sonra isPlaying'i tekrar false yaparak diğer işlemlere izin ver.
            isPlaying = false;
        });
    }

    private Transform GetTargetField()
    {
        int playerLeftCount = playerLeftField.GetComponentsInChildren<Card>().Length;
        int playerMiddleCount = playerMiddleField.GetComponentsInChildren<Card>().Length;
        int playerRightCount = playerRightField.GetComponentsInChildren<Card>().Length;

        // Compare and find the best target field for the card
        if (playerLeftCount > playerMiddleCount + 2 || playerLeftCount > playerRightCount + 2)
        {
            return enemyLeftField.transform;
        }
        if (playerMiddleCount > playerLeftCount + 2 || playerMiddleCount > playerRightCount + 2)
        {
            return enemyMiddleField.transform;
        }
        if (playerRightCount > playerLeftCount + 2 || playerRightCount > playerMiddleCount + 2)
        {
            return enemyRightField.transform;
        }

        // Default case if no field has a significant difference
        return Random.Range(0, 3) switch
        {
            0 => enemyLeftField.transform,
            1 => enemyMiddleField.transform,
            2 => enemyRightField.transform,
            _ => enemyMiddleField.transform
        };
    }

    private Card GetRandomCard()
    {
        if (enemyDeck.Count == 0) return null;

        // Oynanabilir kartları filtrele.
        List<Card> playableCards = enemyDeck.FindAll(card => card.currentValue <= GameManager.Instance.currentEnemySupply);
        if (playableCards.Count == 0) return null;

        return playableCards[Random.Range(0, playableCards.Count)];
    }

    private void FetchCards()
    {
        enemyDeck.Clear();
        enemyDeck.AddRange(GetComponentsInChildren<Card>());
    }
}
