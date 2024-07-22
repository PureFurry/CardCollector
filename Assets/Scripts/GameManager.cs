using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action<int,int> OnPlayerPowerUpdate;
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
    
}
public enum GameStates
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    RESET,
    END
}
