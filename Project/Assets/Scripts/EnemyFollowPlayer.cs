using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3f; // �������� �������� �����
    public int damageAmount = 10; // ����, ������� ���� ������� ������

    private Transform player; // ������ �� ��������� ������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }


    public float knockbackForce = 5f; // ���� ����������� ������ ��� �����
    public float knockbackDuration = 0.2f; // ����������������� ����������� ������
    private bool isKnockback = false; // ����, ������������, ���������� �� �����������
    private float knockbackTimer = 0f; // ������ ��� ������������ ������� �����������
    // ��������� ������������ � �������
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������� ���� ������
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

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
            }
        }
    }
}