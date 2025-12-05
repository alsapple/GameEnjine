using UnityEngine;

public class ShopItemClicker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("클릭 히트: " + hit.collider.name);

                var shopItem = hit.collider.GetComponent<ShopWorldItem>();
                if (shopItem != null)
                {
                    Debug.Log("ShopWorldItem 발견, TryBuy 호출");
                    shopItem.TryBuy();
                }
            }
        }
    }
}
