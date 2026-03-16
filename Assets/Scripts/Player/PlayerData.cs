using System.Collections;
using UnityEngine;

public class PlayerData : MonoBehaviour
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
        Debug.Log("took damage");
        if (health <= 0)
        {
            OnDeath();
        }

        health -= amount;
        StartCoroutine(ShowDamage());

        if (health <= 0) Dead = true;
    }

    private void OnDeath()
    {
        FindFirstObjectByType<NetworkManagerUI>().ToggleDeathDirections(true);
        GetComponent<PlayerActions>().ToggleMove(false);
    }

    private IEnumerator ShowDamage()
    {
        renderer.material = Damaged;
        yield return new WaitForSeconds(1f);
        renderer.material = Default;
    }

}
