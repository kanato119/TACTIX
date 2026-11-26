using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float BlletHoleLifTime = 5f;

    [SerializeField] private bool hasHitWall = false;

    [SerializeField] private float GroundDestory = 0;

    [SerializeField] private GameObject bulletHole;

    private Rigidbody rg;
    private TrailRenderer trail;

    private bool hasHit = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        if (trail != null)
        {
            trail.emitting = false;
            trail.Clear();
            trail.emitting = true;
        }

        rg.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitWall) return;

        
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            StopBullet();
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Attack.instance.AttackDamage(GunShot.instance.Damage, collision.gameObject);

            StopBullet();
        }

    }

    private void StopBullet()
    {
        hasHit = true;

        rg.linearVelocity = Vector3.zero;
        rg.isKinematic = true;

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

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            var decalPos = hit.point + hit.normal * 0.01f;
            var decalRot = Quaternion.FromToRotation(-bulletHole.transform.forward, hit.normal);
            GameObject decal = Instantiate(bulletHole, decalPos, decalRot);

            Destroy(decal, BlletHoleLifTime);
        }
    }
}
