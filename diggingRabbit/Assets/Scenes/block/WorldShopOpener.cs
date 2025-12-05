using UnityEngine;

public class WorldShopOpener : MonoBehaviour
{
    public Transform player;
    public GameObject shopItems;     // Item_d2..d5의 부모
    public float openDistance = 2f;

    bool isOpen = false;

    void Start()
    {
        if (shopItems != null)
            shopItems.SetActive(false);
    }

    void Update()
    {
        if (player == null || shopItems == null) return;

        float dist = Vector2.Distance(player.position, transform.position);

        if (dist <= openDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            shopItems.SetActive(isOpen);
        }

        if (dist > openDistance && isOpen)
        {
            isOpen = false;
            shopItems.SetActive(false);
        }
    }
}
