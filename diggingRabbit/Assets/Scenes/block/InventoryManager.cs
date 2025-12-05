// InventoryManager.cs
using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // ⭐ 안정적인 Singleton 패턴
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
                if (_instance == null)
                {
                    Debug.LogError("[Inventory Error] 씬에서 InventoryManager를 찾을 수 없습니다! 빈 게임 오브젝트를 만들어 이 스크립트를 붙여야 합니다.");
                }
            }
            return _instance;
        }
    }

    [Header("Inventory Settings")]
    public int maxSlots = 20;
    public List<ItemData> inventoryItems = new List<ItemData>();

    [Header("UI References")]
    [Tooltip("Grid Layout Group이 있는 슬롯 부모 오브젝트")]
    public GameObject slotContainer;

    [Tooltip("InventorySlot.cs가 붙어있는 UI 슬롯 프리팹")]
    public GameObject slotPrefab;

    private InventorySlot[] uiSlots;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Start()
    {
        // 게임 시작 시 UI 슬롯들을 생성합니다.
        CreateUISlots();
        UpdateUI();
    }

    // 아이템 추가 및 UI 업데이트 호출
    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("추가하려는 아이템 데이터(ItemData)가 Null입니다. AsdController의 Tile Drop 설정을 확인하세요.");
            return false;
        }

        if (inventoryItems.Count < maxSlots)
        {
            inventoryItems.Add(item);
            Debug.Log($"아이템 획득 성공: {item.itemName}");
            UpdateUI(); // 아이템 추가 후 UI 갱신
            return true;
        }

        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }

    // ✅ 새로 추가: 아이템 제거 + UI 갱신
    public void RemoveItem(ItemData item)
    {
        if (item == null) return;

        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            Debug.Log($"아이템 제거: {item.itemName}");
            UpdateUI(); // 리스트에서 제거 후 UI 다시 그림
        }
    }

    // UI 슬롯을 생성하는 함수
    private void CreateUISlots()
    {
        if (slotPrefab == null || slotContainer == null)
        {
            Debug.LogError("Inventory UI Prefab 또는 Container가 InventoryManager에 할당되지 않았습니다!");
            return;
        }

        uiSlots = new InventorySlot[maxSlots];

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotContainer.transform);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            uiSlots[i] = slot;
            slot.ClearSlot();
        }

        Debug.Log($"인벤토리 UI 슬롯 {maxSlots}개 생성 완료.");
    }

    // 인벤토리 데이터에 맞춰 UI를 새로 고치는 함수
    private void UpdateUI()
    {
        // 1. 아이템이 있는 슬롯 업데이트
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (i < uiSlots.Length)
            {
                uiSlots[i].SetItem(inventoryItems[i]);
            }
        }

        // 2. 남은 빈 슬롯은 Clear
        for (int i = inventoryItems.Count; i < maxSlots; i++)
        {
            if (i < uiSlots.Length)
            {
                uiSlots[i].ClearSlot();
            }
        }
    }
}
