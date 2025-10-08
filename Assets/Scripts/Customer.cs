using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI feedbackText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateUI();

        feedbackText.text = "Enemy takes " + amount + " damage!";

        if (currentHealth == 0)
            feedbackText.text = "Enemy defeated!";
    }

    private void UpdateUI()
    {
        healthBar.value = (float)currentHealth / maxHealth;
    }
}
