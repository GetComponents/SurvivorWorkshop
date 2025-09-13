using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    public int Damage;

    public void ShootMe(Vector3 dir, float force, float despawnTime)
    {
        rb.velocity = dir * force;
        StartCoroutine(Despawn(despawnTime));
    }

    private IEnumerator Despawn(float despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
