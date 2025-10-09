using UnityEngine;
using TMPro;

public class SpellSystem : MonoBehaviour
{
    public WordManager wordManager;
    public Customer customer;
    public TextMeshProUGUI resultText;

    public int correct = 30;
    public int wrong = 10;

    public void ForgeItem()
    {
        bool correct = wordManager.CheckAnswer();

        if (correct)
        {
            customer.ProgressTool(this.correct);
            resultText.text = "Perfect!";
        }
        else
        {
            customer.ProgressTool(wrong);
            resultText.text = "Close!\nThe word was:\n" + wordManager.GetCurrentWord();
        }

        Invoke(nameof(NextWord), 2f);
    }

    private void NextWord()
    {
        wordManager.Next();
        resultText.text = "";
        customer.feedbackText.text = "";
    }
}

