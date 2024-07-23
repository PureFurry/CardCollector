using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action<int,int> OnPlayerPowerUpdate;
    public static event Action<int,int> OnEnemyStatsUpdate;
    public int playerPower,enemyPower,playerHealth,enemyHealth;
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
        ResetState();
    }
    private void Update() {
        switch (gameStates)
        {
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
            playerHealth -= tempEnemyPower;
            enemyHealth -=  tempPlayerPower;
            gameStates = GameStates.RESET;
            break;

        }
    }
    public void UpgradePlayerPower(int _playerPower,int _playerHealth){
        
        OnPlayerPowerUpdate?.Invoke(_playerPower,_playerHealth);
    }
    public void UpgradeEnemyStats(int _enemyPower,int _enemyHealth){
        OnEnemyStatsUpdate?.Invoke(_enemyPower, _enemyHealth);
    }
    public void ResetState(){
        gameStates = GameStates.RESET;
    }
    public void GiveTurn(){
        if (player.isTurn == true)
        {
            gameStates = GameStates.ENEMYTURN;
        }
        if (enemy.isTurn == true)
        {
            gameStates = GameStates.PLAYERTURN;
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
