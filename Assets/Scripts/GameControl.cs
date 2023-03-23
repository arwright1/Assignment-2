using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    GameObject CardBack;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
    public static System.Random rnd = new System.Random();
    public int shuffleNum = 0;
    int[] visibleFaces = { -1, -2 };

    void Start()
    {
        int originalLength = faceIndexes.Count;
        float yPosition = 3.6f;
        float xPosition = -4.8f;
        for (int i = 0; i < 15; i++)
        {
            shuffleNum = rnd.Next(0, (faceIndexes.Count));
            var temp = Instantiate(CardBack, new Vector3(
                xPosition, yPosition, 0),
                Quaternion.identity);
            temp.GetComponent<mainCard>().faceIndex = faceIndexes[shuffleNum];
             faceIndexes.Remove(faceIndexes[shuffleNum]);
             xPosition = xPosition + 2.4f;
            if ((i + 1) % 4 == 0)
            {
                yPosition = yPosition - 2.5f;
                xPosition = -4.8f;
            }
        }
         CardBack.GetComponent<mainCard>().faceIndex = faceIndexes[0];
        
    }

    public bool TwoCardsUp()
    {
        bool cardsUp = false;
        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            cardsUp = true;
        }
        return cardsUp;
    }

    public void AddVisibleFace(int index)
    {
        if (visibleFaces[0] == -1)
        {
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }
    }

    public void RemoveVisibleFace(int index)
    {
        if (visibleFaces[0] == index)
        {
            visibleFaces[0] = -1;
        }
        else if (visibleFaces[1] == index)
        {
            visibleFaces[1] = -2;
        }
    }

    public bool CheckMatch()
    {
        bool success = false;
        if (visibleFaces[0] == visibleFaces[1])
        {
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;
            success = true;
    }
        return success;
    }

    private void Awake()
    {
        CardBack = GameObject.Find("CardBack");
    }

}