using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action<int> OnPlayerPowerUpdate;
    public static event Action<int> OnEnemyStatsUpdate;
    public static event Action<int> OnPlayerHealthUpdate;
    public static event Action<int> OnEnemyHealthUpdate;
    public static event Action<int> PlayerComboUpdate;
    public static event Action<int> EnemyComboUpdate;
    public int playerPower,enemyPower,playerHealth,enemyHealth, currentPlayerHealth;
    public bool isPlayerAttacking, isEnemyAttacking, isItFirstTurn;
    public Dictionary<int, int> combo;
    public int playerCombo,enemyCombo;
    public int playerAttackCount, enemyAttackCount, playerComboCounter, enemyComboCounter;
    public Enemy enemy;
    public Player player;
    public GameStates gameStates;
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
        combo = new Dictionary<int, int>();
    }
    private void Update() {
        switch (gameStates)
        {
            case GameStates.START:
            isItFirstTurn = true;
            playerHealth = 20;
            enemyHealth = 20;
            OnPlayerHealthUpdate?.Invoke(playerHealth);
            OnEnemyHealthUpdate?.Invoke(enemyHealth);
            combo.Add(1,1);
            combo.Add(2,1);
            combo.Add(3,2);
            combo.Add(4,2);
            combo.Add(5,2);
            combo.Add(6,3);
            playerComboCounter = 1;
            enemyComboCounter = 1;
            playerCombo = 1;
            enemyCombo = 1;
            PlayerComboUpdate?.Invoke(playerCombo);
            gameStates = GameStates.PLAYERTURN;
            break;
            case GameStates.RESET:
            playerPower = 0;
            enemyPower = 0;
            gameStates = GameStates.PLAYERTURN;
            break;
            case GameStates.PLAYERTURN:
            enemy.isTurn = false;
            player.isTurn = true;
            break;
            case GameStates.ENEMYTURN:
            enemy.isTurn = true;
            player.isTurn = false;
            break;
            case GameStates.END:
            int tempPlayerPower = playerPower;
            int tempEnemyPower = enemyPower;
            gameStates = GameStates.RESET;
            break;
        }
    }
    public void UpgradePlayerPower(int _playerPower){
        OnPlayerPowerUpdate?.Invoke(_playerPower);
        playerPower = _playerPower;
    }
    public void UpgradeEnemyStats(int _enemyPower){
        OnEnemyStatsUpdate?.Invoke(_enemyPower);
        enemyPower = _enemyPower;
    }
    public void ResetState(){
        gameStates = GameStates.RESET;
    }
    public void GiveTurn(){
        if (GameObject.FindGameObjectWithTag("AskPanel") == null)
        {
            if (player.isTurn == true)
            {
                isPlayerAttacking = GameObject.Find("Player Attack").GetComponent<Toggle>().isOn;
                if (isPlayerAttacking && playerAttackCount > 0)
                {
                    Card[] playerCardonField = GameObject.Find("Player Field").GetComponentsInChildren<Card>();
                    Card[] enemyCardsonField = GameObject.Find("Enemy Field").GetComponentsInChildren<Card>();
                    if (isItFirstTurn)
                    {
                        isItFirstTurn = false;
                    }
                    if (playerPower > enemyPower)
                    {
                        for (int i = 0; i < enemyCardsonField.Length; i++)
                        {
                            for (int j = 0; j < playerCardonField.Length; j++)
                            {
                                if (playerCardonField[i].cardSO.cardValue == enemyCardsonField[i].cardSO.cardValue && playerCardonField[i].cardSO.cardDamage > enemyCardsonField[i].cardSO.cardDamage)
                                {
                                    playerComboCounter++;
                                    playerCombo = combo[playerComboCounter];
                                    PlayerComboUpdate?.Invoke(playerCombo);
                                    Destroy(enemyCardsonField[i].gameObject); 
                                }
                            }
                        }
                    }
                    else
                    {
                        playerHealth -= playerPower /3 +2;
                        gameStates = GameStates.ENEMYTURN;
                    }
                }
                else
                {
                    gameStates = GameStates.ENEMYTURN;
                } 
            }
            if (enemy.isTurn == true)
            {
                isEnemyAttacking = GameObject.Find("Enemy Attack").GetComponent<Toggle>().isOn;
                if (isEnemyAttacking)
                {
                    playerHealth -= enemyPower;
                    gameStates = GameStates.PLAYERTURN;
                }
                else
                {
                    gameStates = GameStates.PLAYERTURN;
                }
            }
        }
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
