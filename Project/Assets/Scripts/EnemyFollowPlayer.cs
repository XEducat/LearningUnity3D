using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3f; // �������� �������� �����
    public int damageAmount = 10; // ����, ������� ���� ������� ������
    public float attackCooldown = 1f; // �������� ����� ������� �����

    private Transform player; // ������ �� ��������� ������
    private Rigidbody rb; // ��������� Rigidbody �����
    private bool canAttack = true; // ����, ����������� ����� ���������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>(); // �������� ������ �� Rigidbody �����
    }

    void Update()
    {
        // ���� ���� �����������, ��������� ������ � ��������� ��� ���������
        if (isKnockback)
        {
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= knockbackDuration)
            {
                isKnockback = false; // ����������� ���������
            }
        }
        else
        {
            // ����������� �������� ����� � ������ (������� ���������)
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;
            direction.Normalize();
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }


    public float knockbackForce = 5f; // ���� ����������� ������ ��� �����
    public float knockbackDuration = 0.2f; // ����������������� ����������� ������
    private bool isKnockback = false; // ����, ������������, ���������� �� �����������
    private float knockbackTimer = 0f; // ������ ��� ������������ ������� �����������
                                       // ��������� ������������ � �������


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������, ����� �� ���� ���������
            if (canAttack)
            {
                // ������� ���� ������
                Player playerHealth = collision.gameObject.GetComponent<Player>();
                playerHealth?.TakeDamage(damageAmount);

                // ��������� ������� ����������� ������
                if (!isKnockback)
                {
                    isKnockback = true;
                    knockbackTimer = 0f;

                    // ��������� ����������� �����������
                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    knockbackDirection.y = 0f; // ���������� ������ �� �����������

                    // ��������� ���� ����������� � ������
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }

                // ��������� ������ �������� ����� ��������� ������
                canAttack = false;
                Invoke("ResetAttackCooldown", attackCooldown);
            }
        }
    }

    // ���������� ������ �������� ����� ��������� ������
    void ResetAttackCooldown()
    {
        canAttack = true;
    }
}