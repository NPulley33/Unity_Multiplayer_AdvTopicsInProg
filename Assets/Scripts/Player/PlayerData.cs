using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float Health { get => health; }

    private float health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }


}
