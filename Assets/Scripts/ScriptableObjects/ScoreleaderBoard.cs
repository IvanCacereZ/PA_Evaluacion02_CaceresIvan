using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score10", menuName = "Scriptableobjects/score10", order = 1)]
public class ScoreleaderBoard : ScriptableObject
{
    [SerializeField] private float[] HighScore;
    private void OnEnable()
    {
        if (HighScore != null)
        {
            HighScore = new float[10];
        }
    }
    public void registryNewScore(float newScore)
    {
        float[] newMaxScore = new float[10];
        bool isChanged = false;
        for (int i = 0; i < 10; i++)
        {
            float safePrevius = HighScore[i];
            if (newScore > HighScore[i] && !isChanged) {
                newMaxScore[i] = newScore;
                i++;
                isChanged = true;
            }
            newMaxScore[i] = safePrevius;
        }
        HighScore = newMaxScore;
    }
}
