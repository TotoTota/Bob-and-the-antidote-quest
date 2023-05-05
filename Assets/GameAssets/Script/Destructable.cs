using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    public float lifeTime;
    public ParticleSystem particle;
    public WallBehaviour wallBehaviour;
    public int index;

    private Shake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            DestroyBreakable();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        shake.CamShake();
        Debug.Log("I've took damage");
    }

    void DestroyBreakable()
    {
        Destroy(gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
        wallBehaviour.RemoveItem(ref wallBehaviour.enemies, index);
    }
}
