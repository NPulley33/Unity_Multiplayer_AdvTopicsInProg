using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// speed of projectile
    /// </summary>
    [SerializeField] private float speed;
    /// <summary>
    /// Prevents a double hit issue where it would damage the player twice
    /// </summary>
    private bool hitSomething;
    [SerializeField] private float damage = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hitSomething)
            {
                hitSomething = true; //prevents double hit issue
                other.GetComponent<PlayerData>().TakeDamage(damage);
            }
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
