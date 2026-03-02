using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// speed of projectile
    /// </summary>
    [SerializeField] private float speed;
    private Vector3 target;
    /// <summary>
    /// Prevents a double hit issue where it would damage the player twice
    /// </summary>
    private bool hitSomething;
    [SerializeField] private float damage = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 5f);
        if (target != null) transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
        else transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.parent is not null && !hitSomething)
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
