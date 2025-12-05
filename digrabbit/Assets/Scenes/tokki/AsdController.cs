using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections.Generic;

public class AsdController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("타일맵 컴포넌트")]
    public Tilemap tilemap;
    [Tooltip("플레이어의 Transform")]
    public Transform player;
    private Vector3 initialPlayerPosition;

    [Header("Player Visuals")]
    [Tooltip("플레이어 GameObject에 붙어있는 SpriteRenderer를 할당하세요.")]
    public SpriteRenderer playerSpriteRenderer;
    public Sprite spriteD1;
    public Sprite spriteD2;
    public Sprite spriteD3;
    public Sprite spriteD4;
    public Sprite spriteD5;

    [Header("Tile Definitions")]
    public TileBase groundTile;
    public TileBase tag2Tile;
    public TileBase tag3Tile;
    public TileBase tag4Tile;
    public TileBase tag5Tile;
    public TileBase tag6Tile;
    public TileBase tag7Tile;
    public TileBase tag8Tile;
    public TileBase tag9Tile;
    public TileBase tag10Tile;
    public TileBase tag11Tile;
    public TileBase tag12Tile;
    public TileBase tag13Tile;
    public TileBase tag14Tile;
    public TileBase tag15Tile;
    public TileBase tag16Tile;
    public TileBase tag17Tile;

    [Header("Tile Score Values")]
    public int tag6Score = 10;
    public int tag7Score = 20;
    public int tag8Score = 30;
    public int tag9Score = 40;
    public int tag10Score = 50;
    public int tag11Score = 60;
    public int tag12Score = 70;
    public int tag13Score = 80;
    public int tag14Score = 90;
    public int tag15Score = 100;
    public int tag16Score = 110;
    public int tag17Score = 120;
    
    [Header("UI Settings")]
    public GameObject miningGaugeObject;
    public Image miningGaugeImage;
    public Vector3 gaugeOffset = new Vector3(0, 0.5f, 0);

    [Header("Mining Settings")]
    public float miningRange = 4.0f;
    public Vector2 miningCenterOffset = new Vector2(0f, 1.0f);

    [Header("Player Mining Speed")]
    public float playerD1DestroyTime = 3.5f;
    public float playerD2DestroyTime = 1.0f;
    public float playerD3DestroyTime = 0.5f;
    public float playerD4DestroyTime = 0.3f;
    public float playerD5DestroyTime = 0.1f;

    // 아이템 드롭 설정
    [Header("Item Drop Settings")]
    [Tooltip("월드에 드롭할 아이템 프리팹 (ItemDrop 스크립트가 붙어 있어야 함)")]
    public GameObject itemDropPrefab;

    [System.Serializable]
    public struct TileDrop
    {
        public TileBase tile;
        public ItemData itemToDrop;
        [Range(0, 1)]
        public float dropChance;
    }

    [Tooltip("타일별 드롭 아이템 및 확률 설정")]
    public List<TileDrop> tileDropSettings = new List<TileDrop>();
    // 아이템 드롭 설정 끝

    private Vector3Int currentCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
    private float holdTime = 0f;
    private bool isDestroying = false;
    private string lastPlayerTag = "";

    [System.Serializable]
    public struct TileSpritePair
    {
        public TileBase tile;
        public Sprite sprite;
    }
    
    void Start()
    {
        if (miningGaugeObject != null)
            miningGaugeObject.SetActive(false);

        if (player != null)
        {
            initialPlayerPosition = player.position;
            UpdatePlayerSprite(player.tag);
            lastPlayerTag = player.tag;
        }
        else
        {
            Debug.LogError("Player Transform이 AsdController에 할당되지 않았습니다! 채굴이 작동하지 않습니다.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            string currentTag = player.tag;
            if (currentTag != lastPlayerTag)
            {
                UpdatePlayerSprite(currentTag);
                lastPlayerTag = currentTag;
            }
        }

        HandleRecallInput();

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (player == null) return;

            Vector3 miningCenter = player.position + (Vector3)miningCenterOffset;
            float distToCenter = Vector2.Distance(miningCenter, mousePos);

            if (distToCenter > miningRange)
            {
                ResetInteraction();
                UpdateGaugeUI(false, Vector3.zero, 0);
                return;
            }

            Vector3Int cellPos = tilemap.WorldToCell(mousePos);

            if (cellPos != currentCell)
            {
                currentCell = cellPos;
                holdTime = 0f;
                isDestroying = false;
            }

            if (tilemap.HasTile(cellPos))
            {
                string tileTag = GetTagForTile(cellPos);
                string playerTag = player.tag;
                float requiredHold = GetDestroyTimeByTag(tileTag, playerTag);

                if (requiredHold <= 0)
                {
                    ResetInteraction();
                    UpdateGaugeUI(false, Vector3.zero, 0);
                    return;
                }

                holdTime += Time.deltaTime;
                Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
                Vector3 gaugePos = cellCenterPos + gaugeOffset;
                float progress = Mathf.Clamp01(holdTime / requiredHold);
                UpdateGaugeUI(true, gaugePos, progress);

                if (!isDestroying && holdTime >= requiredHold)
                {
                    DestroyTile(cellPos);
                }
            }
            else
            {
                ResetInteraction();
                UpdateGaugeUI(false, Vector3.zero, 0);
                return;
            }
        }
        if (!Input.GetMouseButton(0))
        {
            ResetInteraction();
            UpdateGaugeUI(false, Vector3.zero, 0);
        }
    }

    private void HandleRecallInput()
    {
        if (player != null && Input.GetKeyDown(KeyCode.B))
        {
            player.position = initialPlayerPosition;
            ResetInteraction();
            UpdateGaugeUI(false, Vector3.zero, 0);
            Debug.Log("플레이어가 초기 위치로 귀환했습니다.");
        }
    }

    public void UpdatePlayerSprite(string tag)
    {
        if (playerSpriteRenderer == null) { return; }

        switch (tag)
        {
            case "d_1": if (spriteD1 != null) playerSpriteRenderer.sprite = spriteD1; break;
            case "d_2": if (spriteD2 != null) playerSpriteRenderer.sprite = spriteD2; break;
            case "d_3": if (spriteD3 != null) playerSpriteRenderer.sprite = spriteD3; break;
            case "d_4": if (spriteD4 != null) playerSpriteRenderer.sprite = spriteD4; break;
            case "d_5": if (spriteD5 != null) playerSpriteRenderer.sprite = spriteD5; break;
        }
    }

    private void UpdateGaugeUI(bool isActive, Vector3 position, float fillAmount)
    {
        if (miningGaugeObject == null || miningGaugeImage == null) return;
        miningGaugeObject.SetActive(isActive);
        if (isActive)
        {
            miningGaugeObject.transform.position = position;
            miningGaugeImage.fillAmount = fillAmount;
        }
    }

    private void DestroyTile(Vector3Int cellPos)
    {
        isDestroying = true;

        string tileTag = GetTagForTile(cellPos);
        TileBase destroyedTile = tilemap.GetTile(cellPos);

        tilemap.SetTile(cellPos, null); // 타일 제거
        holdTime = 0f;
        UpdateGaugeUI(false, Vector3.zero, 0);

        // 점수 추가 로직
        if (tileTag.StartsWith("tag") && int.TryParse(tileTag.Substring(3), out int tagNum) && tagNum >= 6 && tagNum <= 17)
        {
            int scoreValue = GetScoreForTileTag(tagNum);

            // ScoreManager는 외부 클래스이므로 인스턴스 체크 필요
            // if (ScoreManager.Instance != null) { ScoreManager.Instance.AddScore(scoreValue); }
        }

        // 아이템 드롭 로직 호출
        DropItem(destroyedTile, cellPos);
    }
    
    private void DropItem(TileBase tile, Vector3Int cellPos)
    {
        if (itemDropPrefab == null) return;
        
        foreach (var setting in tileDropSettings)
        {
            if (setting.tile == tile)
            {
                if (Random.Range(0f, 1f) <= setting.dropChance)
                {
                    if (setting.itemToDrop != null)
                    {
                        Vector3 dropPosition = tilemap.GetCellCenterWorld(cellPos);
                        
                        GameObject droppedItem = Instantiate(itemDropPrefab, dropPosition, Quaternion.identity);
                        
                        ItemDrop dropComponent = droppedItem.GetComponent<ItemDrop>();
                        if (dropComponent != null)
                        {
                            dropComponent.itemData = setting.itemToDrop;
                            SpriteRenderer sr = droppedItem.GetComponent<SpriteRenderer>();
                            if (sr != null && dropComponent.itemData.icon != null)
                            {
                                sr.sprite = dropComponent.itemData.icon;
                            }
                        }
                    }
                }
                break;
            }
        }
    }

    private void ResetInteraction()
    {
        currentCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
        holdTime = 0f;
        isDestroying = false;
    }

    private string GetTagForTile(Vector3Int cellPos)
    {
        TileBase tile = tilemap.GetTile(cellPos);

        if (tile == groundTile) return "Ground";
        if (tile == tag2Tile) return "tag2";
        if (tile == tag3Tile) return "tag3";
        if (tile == tag4Tile) return "tag4";
        if (tile == tag5Tile) return "tag5";
        if (tile == tag6Tile) return "tag6";
        if (tile == tag7Tile) return "tag7";
        if (tile == tag8Tile) return "tag8";
        if (tile == tag9Tile) return "tag9";
        if (tile == tag10Tile) return "tag10";
        if (tile == tag11Tile) return "tag11";
        if (tile == tag12Tile) return "tag12";
        if (tile == tag13Tile) return "tag13";
        if (tile == tag14Tile) return "tag14";
        if (tile == tag15Tile) return "tag15";
        if (tile == tag16Tile) return "tag16";
        if (tile == tag17Tile) return "tag17";

        return "Unknown";
    }
    
    private int GetScoreForTileTag(int tagNum)
    {
        switch (tagNum)
        {
            case 6: return tag6Score;
            case 7: return tag7Score;
            case 8: return tag8Score;
            case 9: return tag9Score;
            case 10: return tag10Score;
            case 11: return tag11Score;
            case 12: return tag12Score;
            case 13: return tag13Score;
            case 14: return tag14Score;
            case 15: return tag15Score;
            case 16: return tag16Score;
            case 17: return tag17Score;
            default: return 10; 
        }
    }

    private float GetDestroyTimeByTag(string tileTag, string playerTag)
    {
        bool canDestroy = false;
        bool isTag6To17 = (tileTag.StartsWith("tag") && int.TryParse(tileTag.Substring(3), out int tagNum) && tagNum >= 6 && tagNum <= 17);

        if (playerTag == "d_1")
        {
            if (tileTag == "Ground" || isTag6To17) canDestroy = true;
        }
        else if (playerTag == "d_2")
        {
            if (tileTag == "Ground" || tileTag == "tag2" || isTag6To17) canDestroy = true;
        }
        else if (playerTag == "d_3")
        {
            if (tileTag == "Ground" || tileTag == "tag2" || tileTag == "tag3" || isTag6To17) canDestroy = true;
        }
        else if (playerTag == "d_4")
        {
            if (tileTag == "Ground" || tileTag == "tag2" || tileTag == "tag3" || tileTag == "tag4" || isTag6To17) canDestroy = true;
        }
        else if (playerTag == "d_5")
        {
            if (tileTag == "Ground" || tileTag == "tag2" || tileTag == "tag3" || tileTag == "tag4" || tileTag == "tag5" || isTag6To17) canDestroy = true;
        }

        if (!canDestroy)
        {
            return 0f;
        }

        switch (playerTag)
        {
            case "d_1": return playerD1DestroyTime;
            case "d_2": return playerD2DestroyTime;
            case "d_3": return playerD3DestroyTime;
            case "d_4": return playerD4DestroyTime;
            case "d_5": return playerD5DestroyTime;
            default: return 1.0f;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Vector3 miningCenter = player.position + (Vector3)miningCenterOffset;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(miningCenter, miningRange);
        }
    }
}