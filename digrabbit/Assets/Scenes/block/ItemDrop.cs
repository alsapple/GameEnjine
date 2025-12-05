// ItemDrop.cs
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ItemData itemData;
    private float lifetime = 15f; 
    
    private void Start()
    {
        if (itemData == null)
        {
            Debug.LogError($"[ItemDrop Error] 드롭된 아이템 오브젝트 '{gameObject.name}'에 ItemData가 할당되지 않았습니다!");
            Destroy(gameObject); 
            return;
        }
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 태그 (d_1, d_2, d_3, d_4, d_5) 확인 로직
        string otherTag = other.tag;
        bool isPlayerTag = otherTag == "d_1" || 
                           otherTag == "d_2" || 
                           otherTag == "d_3" || 
                           otherTag == "d_4" || 
                           otherTag == "d_5";

        if (isPlayerTag) 
        {
            if (itemData == null)
            {
                Debug.LogWarning("ItemData가 Null입니다. 획득 시도 중단.");
                Destroy(gameObject); 
                return;
            }

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("[Inventory Error] InventoryManager.Instance를 찾을 수 없습니다! 씬 배치 및 싱글톤 확인.");
                return; 
            }

            // 아이템 획득 시도
            if (InventoryManager.Instance.AddItem(itemData))
            {
                // 획득 성공 시 월드에서 제거
                Destroy(gameObject);
            }
        }
    }
}