using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    [SerializeField] GameObject playerField;
    [SerializeField] GameObject enemyField;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text enemyHealthText;
    [SerializeField] TMP_Text playerSupplyText;
    [SerializeField] TMP_Text enemySupplyText;
    [SerializeField] TMP_Text playerDeckText;
    [SerializeField] TMP_Text enemyDeckText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() {
        SuscribeUpdatgePlayerHeatlhText();
        SuscribeUpdatgeEnemyHeatlhText();
        SuscribeUpdatePlayerSuppylyText();
        SuscribeUpdateEnemySuppylyText();
        SuscribePlayerDecsizeUpdate();
        SuscribeEnemyDeckSizeUpdate();
    }
    private void OnDisable() {
        UnSuscribeUpdatgePlayerHeatlhText();
        UnSuscribeUpdatgeEnemyHeatlhText();
        UnSuscribeUpdatePlayerSuppylyText();
        UnSuscribeUpdateEnemySuppylyText();
        UnSuscribePlayerDeckSizeUpdate();
        UnSuscribeEnemyDeckSizeUpdate();
    }

    #region  DeckSizeUpdate
    
    void SuscribePlayerDecsizeUpdate(){
        GameManager.OnPlayerDeckSize += PlayerDeckSizeText;
    }
    void SuscribeEnemyDeckSizeUpdate(){
        GameManager.OnEnemyDeckSize += EnemyDeckSizeText;
    }
    void UnSuscribePlayerDeckSizeUpdate(){
        GameManager.OnPlayerDeckSize -= PlayerDeckSizeText;
    }
    void UnSuscribeEnemyDeckSizeUpdate(){
        GameManager.OnEnemyDeckSize -= EnemyDeckSizeText;
    }
    void PlayerDeckSizeText(int deckSize){
        playerDeckText.text = deckSize.ToString();
    }
    void EnemyDeckSizeText(int deckSize){
        enemyDeckText.text = deckSize.ToString();
    }
    #endregion

    #region SupplyUpdate
    void SuscribeUpdatePlayerSuppylyText(){
        GameManager.OnPlayerSupplyUpdate += PlayerSuppylyTextUpdate;
    }

    void UnSuscribeUpdatePlayerSuppylyText(){
        GameManager.OnPlayerSupplyUpdate -= PlayerSuppylyTextUpdate;
    }
    void SuscribeUpdateEnemySuppylyText(){
        GameManager.OnEnemySupplyUpdate += EnemySupplyTextUpdate;
    }
    void UnSuscribeUpdateEnemySuppylyText(){
        GameManager.OnEnemySupplyUpdate -= EnemySupplyTextUpdate;
    }
    private void PlayerSuppylyTextUpdate(int supplyAmount)
    {
        playerSupplyText.text = supplyAmount.ToString();
    }
    private void EnemySupplyTextUpdate(int supplyAmount)
    {
        enemySupplyText.text = supplyAmount.ToString();
    }

    #endregion


    #region HealthUpdate
    void SuscribeUpdatgePlayerHeatlhText(){
        GameManager.OnPlayerHealthUpdate += PlayerTextUpdate;
    }
    void UnSuscribeUpdatgePlayerHeatlhText(){
        GameManager.OnPlayerHealthUpdate -= PlayerTextUpdate;
    }
    void SuscribeUpdatgeEnemyHeatlhText(){
        GameManager.OnEnemyHealthUpdate += EnemyTextUpdate;
    }
    void UnSuscribeUpdatgeEnemyHeatlhText(){
        GameManager.OnEnemyHealthUpdate -= EnemyTextUpdate;
    }
    private void EnemyTextUpdate(int healthAmount)
    {
        enemyHealthText.text = healthAmount.ToString();
    }

    private void PlayerTextUpdate(int healthAmount)
    {
        playerHealthText.text = healthAmount.ToString();
    }
    #endregion
}
