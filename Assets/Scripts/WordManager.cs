using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public GameObject letterSlotPrefab;

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

        // remove old slots/tiles
        foreach (Transform child in letterParent)
            Destroy(child.gameObject);
        currentTiles.Clear();

        WordData w = words[currentIndex];
        currentWord = w.word.ToUpper();
        wordImage.sprite = w.image;

        List<char> scrambled = new List<char>(currentWord.ToCharArray());
        scrambled.Shuffle();

        foreach (char c in scrambled)
        {
            GameObject slot = Instantiate(letterSlotPrefab, letterParent);

            RectTransform slotRt = slot.GetComponent<RectTransform>();
            if (slotRt != null)
            {
                slotRt.anchoredPosition = Vector2.zero;
            }
            else
            {
                slot.transform.localPosition = Vector3.zero;
            }

            GameObject tile = Instantiate(letterTilePrefab, slot.transform);

            RectTransform tileRt = tile.GetComponent<RectTransform>();
            if (tileRt != null)
            {
                tileRt.anchoredPosition = Vector2.zero;
            }
            else
            {
                tile.transform.localPosition = Vector3.zero;
            }

            LetterTile lt = tile.GetComponent<LetterTile>();
            if (lt == null)
            {
                Debug.LogError($"WordManager: prefab '{letterTilePrefab.name}' does not have a LetterTile component attached.");
                continue;
            }

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
            // child is the slot; find the tile inside it
            LetterTile tile = child.GetComponentInChildren<LetterTile>();
            if (tile == null) continue;
            result += tile.letterChar;
        }

        Debug.Log("Player Answer: " + result);

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
