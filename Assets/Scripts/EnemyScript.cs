using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    PlayerCharacterMovement player;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speed;
    public int HP;


    private void Start()
    {
        player = PlayerCharacterMovement.Instance;
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
        transform.LookAt(player.transform.position, Vector3.up);
    }

    private void MoveToPlayer()
    {
        Vector2 m_moveDir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z).normalized;
        rb.AddForce(new Vector3(m_moveDir.x, 0, m_moveDir.y) * speed, ForceMode.VelocityChange);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            HurtPlayer();
        }

        if (collision.transform.tag == "PProjectile")
        {
            TakeDamage(1);
        }
    }

    private void HurtPlayer()
    {
        player.HealthPoints--;
    }

    private void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
