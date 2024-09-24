using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public static event Action<int> OnPlayerHealthUpdate;
    public static event Action<int> OnEnemyHealthUpdate;
    public static event Action<int> OnPlayerSupplyUpdate;
    public static event Action<int> OnEnemySupplyUpdate;
    public static event Action<int> OnPlayerDeckSize;
    public static event Action<int> OnEnemyDeckSize;
    public static event Action OnEndTurn;
    public Enemy enemy;
    public Player player;
    public GameStates gameStates;
    public int globalWeath;
    public int playerHealth,enemyHealth,playerSupply,enemySupply,currentPlayerSupply,currentEnemySupply;
    public int startingHealthPoint,startingSupplyPoint;
    public int turnCounter;
    public bool isPlayerFirstTurn = true,isEnemyFirstTurn = true;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;            
        }
    }
    private void Start() {
        gameStates = GameStates.START;
        player = GameObject.Find("Player Hand").GetComponent<Player>();
        enemy = GameObject.Find("Enemy Hand").GetComponent<Enemy>();
    }
    private void Update() {
        switch (gameStates)
        {
            case GameStates.START:
            playerHealth = startingHealthPoint;
            enemyHealth = startingHealthPoint;
            playerSupply = startingSupplyPoint;
            currentPlayerSupply = playerSupply;
            currentEnemySupply = enemySupply;
            playerSupply++;
            currentPlayerSupply = playerSupply;
            OnPlayerSupplyUpdate?.Invoke(currentPlayerSupply);
            OnEnemySupplyUpdate?.Invoke(currentEnemySupply);
            OnPlayerHealthUpdate?.Invoke(playerHealth);
            OnEnemyHealthUpdate?.Invoke(enemyHealth);
            OnPlayerDeckSize?.Invoke(player.collectorDeck.Count);
            OnEnemyDeckSize?.Invoke(player.collectorDeck.Count);
            gameStates = GameStates.PLAYERTURN;
            break;

            case GameStates.RESET:

            gameStates = GameStates.PLAYERTURN;
            break;

            case GameStates.PLAYERTURN:
            // Debug.Log("PLayer Turn");
            OnPlayerSupplyUpdate?.Invoke(currentPlayerSupply);
            OnEnemySupplyUpdate?.Invoke(currentEnemySupply);
            OnPlayerHealthUpdate?.Invoke(playerHealth);
            OnEnemyHealthUpdate?.Invoke(enemyHealth);
            enemy.isTurn = false;
            player.isTurn = true;
            break;
            case GameStates.ENEMYTURN:
            Debug.Log(currentEnemySupply);
            // Debug.Log("Enemy Turn");
            OnPlayerSupplyUpdate?.Invoke(currentPlayerSupply);
            OnEnemySupplyUpdate?.Invoke(currentEnemySupply);
            OnPlayerHealthUpdate?.Invoke(playerHealth);
            OnEnemyHealthUpdate?.Invoke(enemyHealth);
            enemy.isTurn = true;
            player.isTurn = false;
            break;
            case GameStates.END:
            gameStates = GameStates.RESET;
            break;
        }
    }
    public bool GiveTurn(ref int supply, ref int currentSupply,ref int turnCounter){
        supply++;
        currentSupply = supply;
        turnCounter++;
        player.DisplayCards(player.cardObject,player.transform,1);
        OnPlayerDeckSize?.Invoke(player.collectorDeck.Count);
        if (isEnemyFirstTurn) isEnemyFirstTurn = false;
        else OnEndTurn?.Invoke();   
        gameStates = GameStates.PLAYERTURN; 
    
        return true;
    }
    public void PlayerGiveTurn(){
        playerSupply++;
        currentPlayerSupply = playerSupply;
        turnCounter++;
        if (isPlayerFirstTurn){
        isPlayerFirstTurn = false;
        }
        else OnEndTurn?.Invoke();
        enemy.DisplayCards(enemy.cardObject,enemy.transform,1);
        OnEnemyDeckSize?.Invoke(player.collectorDeck.Count);
        gameStates = GameStates.ENEMYTURN;
    }
    public void UpdateEnemySupply(){
        OnEnemySupplyUpdate?.Invoke(currentEnemySupply);
    }
    public void UpdatePlayerSupply(){
        OnPlayerSupplyUpdate?.Invoke(currentPlayerSupply);
    }
    private IEnumerator WaitAndEndTurn(){
    Debug.Log("enımator calıştı");
    yield return new WaitForSeconds(0.2f); // 0.1 saniye bekleme, ihtiyaca göre artırılabilir
    gameStates = GameStates.ENEMYTURN;
}
}
public enum GameStates
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    RESET,
    END
}
