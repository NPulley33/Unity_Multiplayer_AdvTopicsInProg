using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
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

        DestroyThisServerRpc();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyThisServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void DestroyThisServerRpc()
    {
        this.GetComponent<NetworkObject>().Despawn();
        this.OnNetworkDespawn();
        this.OnDestroy();
    }
}
