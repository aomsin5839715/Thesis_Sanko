using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemType
{
    IncreaseHealth,
    IncreaseMana
}

public class HealthAndManaIncreaseItem : MonoBehaviour
{
    public ItemType itemType; // Dropdown option for item type

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            var healthBar = other.GetComponent<HealthBar>();
            if (player != null)
            {
                if (itemType == ItemType.IncreaseHealth)
                {
                    healthBar.maxHealthIncrease();
                    healthBar.health += 99;
                    Debug.Log("Increased player's health");
                }
                else if (itemType == ItemType.IncreaseMana)
                {
                    healthBar.maxManaIncrease();
                    Debug.Log("Increased player's mana");
                }
                Debug.Log("Player entered trap area");

                Destroy(this.gameObject);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(HealthAndManaIncreaseItem))]
public class HealthAndManaIncreaseItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HealthAndManaIncreaseItem item = (HealthAndManaIncreaseItem)target;

    }
}
#endif
