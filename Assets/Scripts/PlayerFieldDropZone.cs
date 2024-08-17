using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerFieldDropZone : DropZone
{
    public override void OnDrop(PointerEventData eventData){
        base.OnDrop(eventData);
        GameObject askPanel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/AskPanel.prefab");
        Card gettingCard = eventData.pointerDrag.GetComponent<Card>();
        GameObject createdPanel = Instantiate(askPanel,transform.position,Quaternion.identity);
        createdPanel.transform.parent = GameObject.Find("Canvas").transform;
        Button hideButton = GameObject.FindGameObjectWithTag("HideAnswer").GetComponent<Button>();
        Button openButton = GameObject.FindGameObjectWithTag("OpenAnswer").GetComponent<Button>();
        //Fonksiyonlar butonlara atanmıyor
        hideButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => gettingCard.CardFlip(true)));
        hideButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => GameManager.Instance.UpgradePlayerPower(GetPower())));
        hideButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => Destroy(GameObject.FindGameObjectWithTag("AskPanel"))));
        openButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => gettingCard.CardFlip(false)));
        openButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>GameManager.Instance.UpgradePlayerPower(GetPower())));
        openButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => Destroy(GameObject.FindGameObjectWithTag("AskPanel"))));
        // GameManager.Instance.UpgradePlayerPower(GetPower(),GetHealth());
    }
}
