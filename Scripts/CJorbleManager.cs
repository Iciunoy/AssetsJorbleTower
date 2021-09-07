using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CJorbleManager
{
    public List<CTowerJorble> jorbleCollection = new List<CTowerJorble>();
    private Dictionary<string, int> collectionReference = new Dictionary<string, int>();
    private Dictionary<string, int> maxJorbles = new Dictionary<string, int>();
    private Dictionary<string, int> costJorbles = new Dictionary<string, int>();
    private TextAsset textFile;

    // Use this for initialization
    public CJorbleManager(TextAsset tf)
    {
        textFile = tf;
        if (textFile != null)
        {
            //SPLITS TEXT FILE BY NEW LINES
            string[] tempText = textFile.text.Split('\n');
            string[][] tempDialogs = new string[tempText.Length][];
            int lineNum = 0;
            foreach (string d in tempText)
            {
                //SPLITS EACH LINE BY '-' SEPERATING DATA
                tempDialogs[lineNum] = d.Split('.');
                string jorbleColor = tempDialogs[lineNum][0];
                int jorbleMax = int.Parse(tempDialogs[lineNum][1]);
                int jorbleCost = int.Parse(tempDialogs[lineNum][2]);
                maxJorbles.Add(jorbleColor, jorbleMax);
                costJorbles.Add(jorbleColor, jorbleCost);
                lineNum++;
            }
        }
        collectionReference["blue"] = 3;
        collectionReference["red"] = 3;
        collectionReference["green"] = 3;
        collectionReference["teal"] = 0;
        collectionReference["pink"] = 0;
        collectionReference["lime"] = 0;
        collectionReference["purple"] = 0;
        collectionReference["orange"] = 0;
        collectionReference["yellow"] = 0;
    }

    public void AddNewJorbleToCollection(string newColor)
    {
        //CHECK FOR MAX JORBLE COUNT
        //if (CheckMaxJorbles(newColor) > CheckJorblesInCollection(newColor))
        //{
        //    int currentPoints = PlayerPrefs.GetInt("Points");
        //    if (costJorbles[newColor] <= currentPoints)
        //    {
        //        int updatedPoints = currentPoints - costJorbles[newColor];
        //        PlayerPrefs.SetInt("Points", updatedPoints);
        //        jorbleCollection.Add(new CTowerJorble(newColor));
        //        collectionReference[newColor] = collectionReference[newColor] + 1;
        //    }
        //}
        
        jorbleCollection.Add(new CTowerJorble(newColor));
        collectionReference[newColor] = collectionReference[newColor] + 1;
    }

    
    public int CheckMaxJorbles(string jColor)
    {
        int maxJorbleReturn = 0;
        if (maxJorbles.ContainsKey(jColor))
        {
            maxJorbleReturn = maxJorbles[jColor];
        }
        if (!collectionReference.ContainsKey(jColor))
        {
            Debug.Log("THATS NOT A COLOR");
        }
        return maxJorbleReturn;
    }

    public int CheckJorbleCost(string jColor)
    {
        int costJorbleReturn = 0;
        if (costJorbles.ContainsKey(jColor))
        {
            costJorbleReturn = costJorbles[jColor];
        }
        if (costJorbleReturn == 0)
        {
            Debug.Log("ERROR NO FREE JORBLES");
        }
        return costJorbleReturn;
    }

    public int CheckJorblesInCollection(string jColor)
    {
        int jorbleReturn = 0;
        if (collectionReference.ContainsKey(jColor))
        {
            jorbleReturn = collectionReference[jColor];
        }
        if (!collectionReference.ContainsKey(jColor))
        {
            Debug.Log("THATS NOT A COLOR");
        }
        return jorbleReturn;
    }

}
