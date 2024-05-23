using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;

    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    public Image[] manas;
    public Sprite fullMana;
    public Sprite emptyMana;

    void UpdateUI(Image[] images, Sprite fullSprite, Sprite emptySprite, int currentValue, int maxValue)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < currentValue)
            {
                images[i].sprite = fullSprite;
            }
            else
            {
                images[i].sprite = emptySprite;
            }

            images[i].enabled = i < maxValue;
        }
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        mana = Mathf.Clamp(mana, 0, maxMana);

        UpdateUI(hearts, fullHearth, emptyHearth, health, maxHealth);
        UpdateUI(manas, fullMana, emptyMana, mana, maxMana);
    }

    public void TakeDamage()
    {
        var player = GetComponent<PlayerController>();
        player.rb.velocity += Vector2.up * 1;
        health -= 1;
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public void UseMana(int amount)
    {
        mana -= amount;
    }

    public void RechargeMana(int amount)
    {
        mana += amount;
    }

    public void maxHealthIncrease()
    {
        maxHealth += 1;
    }

    public void maxManaIncrease()
    {
        maxMana += 1;
    }
}
