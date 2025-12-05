using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    bool sold1;
    bool sold2;
    bool sold3;

    [SerializeField] string endingSceneName = "EndingScene";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 필요하면:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 아이템이 하나 팔릴 때마다 InventorySlot에서 호출
    public void OnItemSold(ItemData item)
    {
        if (item == null)
            return;

        if (!item.isEndingItem)
            return;

        if (item.endingId == 1)
            sold1 = true;
        else if (item.endingId == 2)
            sold2 = true;
        else if (item.endingId == 3)
            sold3 = true;

        Debug.Log($"[Ending] 상태: 1={sold1}, 2={sold2}, 3={sold3}");

        if (sold1 && sold2 && sold3)
        {
            Debug.Log("[Ending] 엔딩 조건 달성 (3개 다 판매)");
            if (!string.IsNullOrEmpty(endingSceneName))
            {
                SceneManager.LoadScene(endingSceneName);
            }
        }
    }
}
