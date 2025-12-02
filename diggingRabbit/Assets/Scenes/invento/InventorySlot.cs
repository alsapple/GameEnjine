// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Tooltip("아이템 스프라이트가 표시될 Image 컴포넌트")]
    public Image itemIcon; 
    
    private ItemData currentItem;

    // 슬롯을 활성화하고 아이템 정보를 표시합니다.
    public void SetItem(ItemData item)
    {
        currentItem = item;
        if (currentItem != null && currentItem.icon != null)
        {
            itemIcon.sprite = currentItem.icon;
            itemIcon.color = Color.white; // 아이콘 보이게 설정
        }
        else
        {
            ClearSlot();
        }
    }

    // 슬롯을 비웁니다.
    public void ClearSlot()
    {
        currentItem = null;
        itemIcon.sprite = null;
        // 투명하게 설정하여 아이콘이 없는 상태를 표시
        itemIcon.color = new Color(1, 1, 1, 0); 
    }
}