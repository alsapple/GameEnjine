using UnityEngine;

public class ShopWorldItem : MonoBehaviour
{
    public int price = 100;
    public string targetTag = "d_2";
    public GameObject player;

    public void TryBuy()
{
    Debug.Log($"{name}: TryBuy 호출됨");

    if (player == null || PlayerMoney.Instance == null)
        return;

    if (PlayerMoney.Instance.TrySpendMoney(price))
    {
        player.tag = targetTag;
        Debug.Log($"구매 성공, 태그 변경: {targetTag}");

        // 여기 추가
        gameObject.SetActive(false);   // 또는 Destroy(gameObject);
    }
    else
    {
        Debug.Log("돈 부족, 구매 실패");
    }
}

}
