using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private bool hasHitWall = false;

    [SerializeField] private float GroundDestory = 0;

    private Rigidbody rigidbody;
    private TrailRenderer trail;

    private bool hasHit = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        rigidbody.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitWall) return;

        
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            StopBullet();
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Attack.instance.AttackDamage(GunShot.instance.Damage, collision.gameObject);

            StopBullet();
        }

    }

    private void StopBullet()
    {
        hasHit = true;

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.isKinematic = true;

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        if(trail != null)
        {
            trail.emitting = false;
            Destroy(gameObject, trail.time);
        }
        else
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
