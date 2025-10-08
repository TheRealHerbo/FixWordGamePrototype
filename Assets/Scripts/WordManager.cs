using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    [System.Serializable]
    public class WordData
    {
        public string word;
        public Sprite image;
    }

    public List<WordData> words;
    public Image wordImage;
    public Transform letterParent;
    public GameObject letterTilePrefab;

    private string currentWord;
    private List<LetterTile> currentTiles = new List<LetterTile>();
    private int currentIndex = 0;

    void Start()
    {
        LoadNextWord();
    }

    public void LoadNextWord()
    {
        if (currentIndex >= words.Count) currentIndex = 0;

        foreach (Transform child in letterParent)
            Destroy(child.gameObject);

        WordData w = words[currentIndex];
        currentWord = w.word.ToUpper();
        wordImage.sprite = w.image;

        List<char> scrambled = new List<char>(currentWord.ToCharArray());
        scrambled.Shuffle();

        foreach (char c in scrambled)
        {
            GameObject tile = Instantiate(letterTilePrefab, letterParent);
            LetterTile lt = tile.GetComponent<LetterTile>();
            lt.Setup(c, this);
            currentTiles.Add(lt);
        }
    }

    public string GetCurrentWord() => currentWord;

    public string GetPlayerAnswer()
    {
        string result = "";
        foreach (Transform child in letterParent)
        {
            LetterTile tile = child.GetComponent<LetterTile>();
            result += tile.letterChar;
        }
        return result;
    }

    public bool CheckAnswer()
    {
        string playerAnswer = GetPlayerAnswer();
        bool correct = playerAnswer == currentWord;
        return correct;
    }

    public void Next()
    {
        currentIndex++;
        LoadNextWord();
    }
}

public static class Extensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}

