using UnityEngine;

public class movecon : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // ���� ���� �� �� ���� - Animator ������Ʈ ã�Ƽ� ����
        animator = GetComponent<Animator>();

        // �����: ����� ã�Ҵ��� Ȯ��
        if (animator != null)
        {
            Debug.Log("Animator ������Ʈ�� ã�ҽ��ϴ�!");
        }
        else
        {
            Debug.LogError("Animator ������Ʈ�� �����ϴ�!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // WŰ - ���� �̵�
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * 7 * Time.deltaTime);
            Debug.Log("���� �̵� ��!");
        }

        // AŰ - �������� �̵�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 7 * Time.deltaTime);
            Debug.Log("�������� �̵� ��!");
            spriteRenderer.flipX = true;
        }

        // SŰ - �Ʒ��� �̵�
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * 7 * Time.deltaTime);
            Debug.Log("�Ʒ��� �̵� ��!");
        }

        // DŰ - ���������� �̵�
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 7 * Time.deltaTime);
            Debug.Log("���������� �̵� ��!");
            spriteRenderer.flipX = false;
        }
    }

}

