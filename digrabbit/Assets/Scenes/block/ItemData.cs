using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;

    // 엔딩용 아이템인지?
    public bool isEndingItem;

    // 엔딩용 아이템 번호 (1, 2, 3 중 하나)
    public int endingId;
}
