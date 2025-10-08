using UnityEngine;
using TMPro;

public class SpellSystem : MonoBehaviour
{
    public WordManager wordManager;
    public Customer customer;
    public TextMeshProUGUI resultText;

    public int correctDamage = 30;
    public int wrongDamage = 10;

    public void CastSpell()
    {
        bool correct = wordManager.CheckAnswer();

        if (correct)
        {
            customer.TakeDamage(correctDamage);
            resultText.text = "Perfect! Spell hits hard!";
        }
        else
        {
            customer.TakeDamage(wrongDamage);
            resultText.text = "Close! Weak hit...";
        }

        // Load the next word after a small delay
        Invoke(nameof(NextWord), 2f);
    }

    private void NextWord()
    {
        wordManager.Next();
        resultText.text = "";
    }
}

