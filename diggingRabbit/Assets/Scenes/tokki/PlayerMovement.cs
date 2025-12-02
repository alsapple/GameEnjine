using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    
    [Header("점프 설정")]
    public float jumpForce = 10.0f;
    
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조 추가
    private bool isGrounded = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 컴포넌트 가져오기
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다!");
        }

        // SpriteRenderer가 없으면 경고 출력
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer가 없습니다! 캐릭터 플립을 할 수 없습니다.");
        }
    }
    
    void Update()
    {
        // 좌우 이동 입력
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        
        // 물리 기반 이동
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        // ⭐ 캐릭터 방향 플립 ⭐
        HandleCharacterFlip(moveX);

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // linearVelocity를 사용하여 점프 구현
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            Debug.Log("점프!");
        }
        
        // 애니메이션
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    /// <summary>
    /// 이동 방향에 따라 캐릭터의 SpriteRenderer를 플립합니다.
    /// </summary>
    private void HandleCharacterFlip(float moveX)
    {
        if (spriteRenderer != null)
        {
            if (moveX > 0) // D 키 (오른쪽 이동)
            {
                // 오른쪽을 바라보게 (기본 방향)
                spriteRenderer.flipX = false;
            }
            else if (moveX < 0) // A 키 (왼쪽 이동)
            {
                // 왼쪽을 바라보게 (X축 뒤집기)
                spriteRenderer.flipX = true;
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 "Ground" Tag를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("바닥에 착지!");
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("바닥에서 떨어짐");
            isGrounded = false;
        }
    }
}