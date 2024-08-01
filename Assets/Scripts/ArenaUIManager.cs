using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    [SerializeField] GameObject playerField;
    [SerializeField] GameObject enemyField;
    [SerializeField] TMP_Text playerPowerText;
    [SerializeField] TMP_Text enemyPowerText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text enemyHealthText;
    [SerializeField] TMP_Text playerComboText;
    [SerializeField] TMP_Text enemyComboText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() {
        SuscribeUpdatePlayerText();
        SuscribeUpdateEnemyText();
        SuscribeUpdatePlayerComboText();
        SuscribeUpdateEnemyComboText();
    }
    private void OnDisable() {
        UnSuscribeUpdatePlayerText();
        UnSuscribeUpdateEnemyText();
        UnSuscribeUpdatePlayerComboText();
        UnSuscribeUpdateEnemyComboText();    
    }
    void SuscribeUpdatePlayerText(){
        GameManager.OnPlayerPowerUpdate += PlayerTextUpdate;
    }
    void UnSuscribeUpdatePlayerText(){
        GameManager.OnPlayerPowerUpdate -= PlayerTextUpdate;
    }
    void SuscribeUpdateEnemyText(){
        GameManager.OnEnemyStatsUpdate += EnemyTextUpdate;
    }
    void UnSuscribeUpdateEnemyText(){
        GameManager.OnEnemyStatsUpdate -= EnemyTextUpdate;
    }
    void SuscribeUpdatePlayerComboText(){
        GameManager.PlayerComboUpdate += PlayerComboTextUpdate;
    }
    void UnSuscribeUpdatePlayerComboText(){
        GameManager.PlayerComboUpdate -= PlayerComboTextUpdate;
    }
    void SuscribeUpdateEnemyComboText(){
        GameManager.EnemyComboUpdate += EnemyComboTextUpdate;
    }
    void UnSuscribeUpdateEnemyComboText(){
        GameManager.EnemyComboUpdate -= EnemyComboTextUpdate;
    }    
    
    private void PlayerComboTextUpdate(int comboAmount){
        playerComboText.text = "X" + comboAmount.ToString();
    }
    private void EnemyComboTextUpdate(int comboAmount){
        enemyComboText.text = "X" + comboAmount.ToString();
    }
    private void EnemyTextUpdate(int powerAmount, int healthAmount)
    {
        enemyPowerText.text = powerAmount.ToString();
        enemyHealthText.text = healthAmount.ToString();
    }

    private void PlayerTextUpdate(int powerAmount,int healthAmount)
    {
        playerPowerText.text = powerAmount.ToString();
        playerHealthText.text = healthAmount.ToString();
    }
}
