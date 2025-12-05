using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    [TextArea]
    public string description;

    // 새로 추가: 이 아이템의 가격 (판매/구매 공통으로 사용)
    public int price;
}
