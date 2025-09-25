using UnityEngine;

public class movecon : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 게임 시작 시 한 번만 - Animator 컴포넌트 찾아서 저장
        animator = GetComponent<Animator>();

        // 디버그: 제대로 찾았는지 확인
        if (animator != null)
        {
            Debug.Log("Animator 컴포넌트를 찾았습니다!");
        }
        else
        {
            Debug.LogError("Animator 컴포넌트가 없습니다!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // W키 - 위로 이동
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * 7 * Time.deltaTime);
            Debug.Log("위로 이동 중!");
        }

        // A키 - 왼쪽으로 이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 7 * Time.deltaTime);
            Debug.Log("왼쪽으로 이동 중!");
            spriteRenderer.flipX = true;
        }

        // S키 - 아래로 이동
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * 7 * Time.deltaTime);
            Debug.Log("아래로 이동 중!");
        }

        // D키 - 오른쪽으로 이동
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 7 * Time.deltaTime);
            Debug.Log("오른쪽으로 이동 중!");
            spriteRenderer.flipX = false;
        }
    }

}

