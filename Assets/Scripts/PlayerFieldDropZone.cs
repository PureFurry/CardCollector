using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFieldDropZone : DropZone
{
    public override void OnDrop(PointerEventData eventData){
        base.OnDrop(eventData);
        GameManager.Instance.UpgradePlayerPower(GetPower(),GetHealth());
    }
}
