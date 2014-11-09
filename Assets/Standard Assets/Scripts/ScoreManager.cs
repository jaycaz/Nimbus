using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public int goodPoints;
    public int badPoints;
    public int totalStoredPoints;

    public static int smallBad = 15;
    public static int avgBad = 30;
    public static int largeBad = 60;

    public static int smallGood = 15;
    public static int avgGood = 30;
    public static int largeGood = 60;

    public void smallBadReward()
    {
        badPoints += smallBad;
    }
    public void avgBadReward()
    {
        badPoints += avgBad;
    }
    public void largeBadReward()
    {
        badPoints += largeBad;
    }
    public void smallGoodReward()
    {
        goodPoints += smallGood;
    }
    public void avgGoodReward()
    {
        goodPoints += avgGood;
    }
    public void largeGoodReward()
    {
        goodPoints += largeGood;
    }

    public int TotalLevelScore { get { return goodPoints + (int)(1.5 * badPoints); } }
    public int convertLevelScores()
    {
        goodPoints = 0;
        badPoints = 0;

        int levelScore = TotalLevelScore;
        totalStoredPoints += levelScore;
        return levelScore;
    }

    public bool spend(int amount)
    {
        if(totalStoredPoints >= amount)
        {
            totalStoredPoints -= amount;
            return true;
        }

        return false;
    }

	// Use this for initialization
	void Start () {
        badPoints = 0;
        goodPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
