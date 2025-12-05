using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;

    private ItemData currentItem;

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (currentItem != null && currentItem.icon != null)
        {
            itemIcon.sprite = currentItem.icon;
            itemIcon.color = Color.white;
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
{
    if (currentItem == null)
        return;

    if (PlayerMoney.Instance != null)
        PlayerMoney.Instance.AddMoney(currentItem.price);

    if (InventoryManager.Instance != null)
        InventoryManager.Instance.RemoveItem(currentItem);
}

}
