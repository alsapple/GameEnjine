using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TileDestroyerWithTag : MonoBehaviour
{
    public Tilemap tilemap;

    // 태그별 파괴 시간 정의
    public float tag1DestroyTime = 0.5f;
    public float tag2DestroyTime = 1.0f;
    public float tag3DestroyTime = 2.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mousePos);

            if (tilemap.HasTile(cellPos))
            {
                string tag = GetTagForTile(cellPos);
                float delay = GetDestroyTimeByTag(tag);
                StartCoroutine(DestroyAfterDelay(cellPos, delay));
            }
        }
        
    }

    private string GetTagForTile(Vector3Int cellPos)
    {
        // 여기서 타일 종류에 따라 태그를 반환
        // 예: 좌표 기준으로 임의 구분 (실제 환경에 맞게 수정 필요)
        if (cellPos.x < 0) return "Ground";
        else if (cellPos.x == 0) return "tag2";
        else return "tag3";
    }

    private float GetDestroyTimeByTag(string tag)
    {
        if (tag == "Ground") return tag1DestroyTime;
        else if (tag == "tag2") return tag2DestroyTime;
        else if (tag == "tag3") return tag3DestroyTime;
        else return 0.5f; // 기본 대기 시간
    }

    private IEnumerator DestroyAfterDelay(Vector3Int cellPos, float delay)
    {
        yield return new WaitForSeconds(delay);
        tilemap.SetTile(cellPos, null);
    }
}
