using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int healthMax;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if(health <= 0)
        {
            Debug.Log("Player died");
        }

        if(health > healthMax)
        {
            health = healthMax;
        }

        for(int i=0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < healthMax)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    public void RegenerateHealth(int generateAmount)
    {
        health += generateAmount;
    }
}
