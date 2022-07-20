using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parentTransform;
    }

    [SerializeField] private List<Pool> listOfPools;

    private List<GameObject> letterBoxes = new List<GameObject>();
    private int amountBoxes = 0;

    private List<GameObject> wordsToSearch = new List<GameObject>();
    private int amountOfWordsToSearch = 0;

    private void Awake()
    {
        CreatePoolGameObjects();
    }

    private void CreatePoolGameObjects()
    {

        foreach (Pool pool in listOfPools)
        {
            if (pool.tag == "Box")
            {
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject box = Instantiate(pool.prefab);
                    box.SetActive(false);
                    box.transform.SetParent(pool.parentTransform);
                    letterBoxes.Add(box);
                }
            }

            else if (pool.tag == "WordToSearch")
            {
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject searchBox = Instantiate(pool.prefab);
                    searchBox.SetActive(false);
                    searchBox.transform.SetParent(pool.parentTransform);
                    wordsToSearch.Add(searchBox);
                }
            }
        }
    }

    public GameObject GetTheNextLetterBoxObjectFromListQueue()
    {
        if (amountBoxes < letterBoxes.Count)
        {
            amountBoxes++;
            return letterBoxes[amountBoxes - 1];
        }

        return null;
    }

    public void ResetLetterBoxListIndex()
    {
        for (int i = 0; i < letterBoxes.Count; i++)
        {
            letterBoxes[i].SetActive(false);
        }
        amountBoxes = 0;
    }

    public GameObject GetTheNextWordToSearchObjectFromListQueue()
    {
        amountOfWordsToSearch++;
        return wordsToSearch[amountOfWordsToSearch - 1];
    }

    public void ResetWordToSearchListIndex()
    {
        for (int i = 0; i < wordsToSearch.Count; i++)
        {
            wordsToSearch[i].SetActive(false);
        }

        amountOfWordsToSearch = 0;
    }
}