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
    [SerializeField] private float destroyAfter = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        destroyAfter -= Time.fixedDeltaTime;

        if (destroyAfter <= 0) DestroyThisServerRpc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hitSomething)
            {
                hitSomething = true; //prevents double hit issue
                other.GetComponent<PlayerData>().TakeDamage(damage);
                Debug.Log("hit player");
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
        //this.OnNetworkDespawn();
        //this.OnDestroy();
    }
}
