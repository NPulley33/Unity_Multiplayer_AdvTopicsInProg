using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    public float Health { get => health; }
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100f;

    public bool Dead { get; private set; }

    //temp materials to show damage states with characters
    public Material Default;
    public Material Damaged;
    private Renderer renderer;


    private void Awake()
    {
        health = maxHealth;
        renderer = GetComponent<Renderer>();
        renderer.material = Default;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        //prevents issue where if owner dies client could damage self through owner
        //TODO if not owner && owner is dead
        if (!IsOwner) return;

        health -= amount;

        if (health <= 0)
        {
            Dead = true;
            OnDeath();
        }

        StartCoroutine(ShowDamage());
    }

    private void OnDeath()
    {
        FindFirstObjectByType<NetworkManagerUI>().ToggleDeathDirections(true);
        GetComponent<PlayerActions>().ToggleMove(false);

        GetComponent <MeshRenderer>().enabled = false;
        GetComponent <CapsuleCollider>().enabled = false;
        GetComponent <CharacterController>().enabled = false;
    }

    private IEnumerator ShowDamage()
    {
        //renderer.material = Damaged;
        UpdateDamagedMaterialRpc();
        yield return new WaitForSeconds(0.5f);
        //renderer.material = Default;
        UpdateDefaultMaterialRpc();
    }

    [Rpc(SendTo.Server)]
    private void UpdateDamagedMaterialRpc()
    { 
        this.renderer.material = Damaged;
    }

    [Rpc(SendTo.Server)]
    private void UpdateDefaultMaterialRpc()
    {
        this.renderer.material = Default;
    }
}
