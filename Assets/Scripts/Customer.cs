using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    public int done = 100;
    public int currentProgress;
    public TextMeshProUGUI feedbackText;

    void Start()
    {
        currentProgress = 0;
    }

    public void ProgressTool(int amount)
    {
        currentProgress += amount;


        if (currentProgress >= 100)
            feedbackText.text = "Thank you!";
    }
}
