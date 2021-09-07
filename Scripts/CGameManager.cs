using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{
    public static CGameManager gameManager;
    public static bool pauseForAnimation;
    private int scrolledHeight;
    private int currentPoints;
    private int currentGameHeight;
    private int recordHeight;
    private bool jorbleSelected;
    private int selectedSlot;
    private int remainingJorbles;
    public static float hightlightAlpha;
    private bool alphaIncreasing;
    public static bool glowActive;
    private int pointMultiplier;
    private List<CTowerJorble> jorbleDeck = new List<CTowerJorble>();
    private List<CTowerJorble> jorbleDeckInst = new List<CTowerJorble>();
    public TextAsset jTextFile;
    public static CJorbleManager jManager;

    public int CurrentGameHeight
    {
        get
        {
            return currentGameHeight;
        }

        set
        {
            currentGameHeight = value;
        }
    }
    public bool JorbleSelected
    {
        get
        {
            return jorbleSelected;
        }

        set
        {
            jorbleSelected = value;
        }
    }
    public int SelectedSlot
    {
        get
        {
            return selectedSlot;
        }

        set
        {
            selectedSlot = value;
        }
    }
    public int RemainingJorbles
    {
        get
        {
            return remainingJorbles;
        }

        set
        {
            remainingJorbles = value;
        }
    }
    public int CurrentPoints
    {
        get
        {
            return currentPoints;
        }

        set
        {
            currentPoints = value;
        }
    }
    public int RecordHeight
    {
        get
        {
            return recordHeight;
        }

        set
        {
            recordHeight = value;
        }
    }
    public int ScrolledHeight
    {
        get
        {
            return scrolledHeight;
        }

        set
        {
            scrolledHeight = value;
        }
    }

    void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        pauseForAnimation = false;
        CurrentGameHeight = 0;
        ScrolledHeight = 0;
        SelectedSlot = -1;
        pointMultiplier = 1;
        hightlightAlpha = 0.9f;
        alphaIncreasing = false;
        jManager = new CJorbleManager(jTextFile);
        LoadGame();
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            CreateNewDeck();
        }
        //SaveGame();
            //if (!PlayerPrefs.HasKey("Load Game"))
            //{
            //    PlayerPrefs.SetInt("Points", 0);
            //    PlayerPrefs.SetInt("Record Height", 0);
            //    CreateNewDeck();
            //}

     }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(pauseForAnimation);
        }
        //Debug.Log(hightlightAlpha);
        if (SceneManager.GetActiveScene().name == "SLoad")
        {
            GetComponent<CUIChangeScene>().ChangeScene("SMenu");
        }
        if (glowActive)
        {
            HighlightGlow(alphaIncreasing);
        }
        glowActive = false;
	}

    //ONLY CALL AT THE END OF A GAME!!!
    public void ApplyEndGameData(bool gameWon)
    {
        //int gainedPoints = CurrentGameHeight * 10;
        //if (gameWon)
        //{
        //    if (CurrentGameHeight > RecordHeight)
        //    {
        //        RecordHeight = CurrentGameHeight;
        //    }
        //}
        //if (!gameWon)
        //{
        //    gainedPoints = CurrentGameHeight * -5;
        //}
        //CurrentPoints = CurrentPoints + gainedPoints;
        //if (CurrentPoints < 0)
        //{
        //    CurrentPoints = 0;
        //}
    }

    public void UpdateNewHeight(int height)
    {
        CurrentGameHeight = height;
        //if (CurrentGameHeight > RecordHeight)
        //{
        //    RecordHeight = CurrentGameHeight;
        //}
    }

    //THESE DEAL WITH PREPARING YOUR DECK

    public CTowerJorble ReturnJorbleFromDeck(int slot)
    {
        CTowerJorble jorb = jorbleDeckInst[slot];
        return jorb;
    }

    //CREATES A STARTER DECK WHEN YOU OPEN THE GAME
    public void CreateNewDeck()
    {
        jorbleDeck.Add(new CTowerJorble("blue"));
        jorbleDeck.Add(new CTowerJorble("blue"));
        jorbleDeck.Add(new CTowerJorble("blue"));
        jorbleDeck.Add(new CTowerJorble("red"));
        jorbleDeck.Add(new CTowerJorble("red"));
        jorbleDeck.Add(new CTowerJorble("red"));
        jorbleDeck.Add(new CTowerJorble("green"));
        jorbleDeck.Add(new CTowerJorble("green"));
        jorbleDeck.Add(new CTowerJorble("green"));
        for (int i = 0; i < jorbleDeck.Count; i++)
        {
            jManager.jorbleCollection.Add(new CTowerJorble(jorbleDeck[i]));
        }
    }

    //CREATES AN INSTANCE OF YOUR DECK TO BE USED IN PLAY
    public void DealNewDeckInstance()
    {
        jorbleDeckInst.Clear();
        for (int i = 0; i < jorbleDeck.Count; i++)
        {
            jorbleDeckInst.Add(new CTowerJorble(jorbleDeck[i]));
        }
        ShuffleDeck(jorbleDeckInst);
        RemainingJorbles = jorbleDeckInst.Count;
    }

    void ShuffleDeck(List<CTowerJorble> jList)
    {
        for (int i = 0; i < jList.Count; i++)
        {
            int randomJorble = (int) UnityEngine.Random.Range(0.0f, jList.Count - 1);
            CTowerJorble value = jList[randomJorble];
            jList[randomJorble] = jList[i];
            jList[i] = value;
        }
    }

    //THIS ADDS JORBLES TO YOUR COLLECTION FROM THE STORE
    public void AddNewJorbleToCollection(string newColor)
    {
        //TEMP I GUESS
        if (jManager.CheckMaxJorbles(newColor) > jManager.CheckJorblesInCollection(newColor))
        {
            int pointsInst = CurrentPoints;
            if (jManager.CheckJorbleCost(newColor) <= pointsInst)
            {
                int updatedPoints = pointsInst - jManager.CheckJorbleCost(newColor);
                CurrentPoints = updatedPoints;
                jManager.AddNewJorbleToCollection(newColor);
                SaveGame();
                //jorbleDeck.Add(new CTowerJorble(newColor));
            }
        }
    }

    //THIS ADDS JORBLES FROM YOUR COLLECTION TO THE DECK BEFORE ENTERING PLAY
    public void AddNewJorbleToDeck(string newColor)
    {
        int jCurrent = 0;
        foreach (CTowerJorble jorb in jorbleDeck)
        {
            if (jorb.JorbleColor == newColor)
            {
                jCurrent++;
            }
        }
        int jOwned = jManager.CheckJorblesInCollection(newColor);
        if (jOwned > jCurrent)
        {
            jorbleDeck.Add(new CTowerJorble(newColor));
        }
        SaveGame();
    }

    //THIS REMOVES JORBLES FROM YOUR DECK BEFORE ENTERING PLAY
    public void RemoveJorbleFromDeck(string removeColor)
    {
        if (jorbleDeck.Count > 0)
        {
            foreach (CTowerJorble jorb in jorbleDeck)
            {
                if (jorb.JorbleColor == removeColor)
                {
                    jorbleDeck.Remove(jorb);
                    break;
                }
            }
        }
    }

    //THESE DEAL WITH THE DECK MID GAMEPLAY
    public void RemoveJorbleFromDeckInstance(int slot)
    {
        jorbleDeckInst.RemoveAt(slot);
        //Debug.Log("REMOVED JORBLE");
        RemainingJorbles = jorbleDeckInst.Count;
    }

    public int CheckJorblesInDeck(string countColor)
    {
        int jNumber = 0;
        foreach(CTowerJorble jorb in jorbleDeck)
        {
            if (jorb.JorbleColor == countColor)
            {
                jNumber++;
            }
        }
        return jNumber;
    }

    public int CheckJorblesInDeck()
    {
        int jNumber;
        jNumber = jorbleDeck.Count;
        return jNumber;
    }

    public void SelectJorbleSlot(int slotNumber)
    {
        if (!pauseForAnimation)
        {
            SelectedSlot = slotNumber;
        }
        //Debug.Log("JORBLE SELECTED FROM SLOT");
    }

    public void ResetJorbleSlot()
    {
        SelectedSlot = -1;
        //Debug.Log("DESELECTED JORBLE");
    }

    public void FinishTower(bool hitBomb)
    {
        pauseForAnimation = false;
        int gainedPoints = CurrentGameHeight * 10;
        if (!hitBomb)
        {
            if (CurrentGameHeight > RecordHeight)
            {
                RecordHeight = CurrentGameHeight;
            }
        }
        if (hitBomb)
        {
            gainedPoints = CurrentGameHeight * -5;
        }
        CurrentPoints = CurrentPoints + gainedPoints;
        if (CurrentPoints < 0)
        {
            CurrentPoints = 0;
        }
        //MOVED TO CUIENDWINDOW
        //GetComponent<CUIChangeScene>().ChangeScene("SMenu");
        GameObject.FindGameObjectWithTag("Finish").GetComponent<CUIEndWindow>().ActivateWindow(hitBomb);
    }

    public void PauseForAnim(bool pauseGame)
    {
        if (pauseGame)
        {
            pauseForAnimation = true;
        }
        else
        {
            pauseForAnimation = false;
        }
    }

    private void HighlightGlow(bool up)
    {
        if (SelectedSlot < 0)
        {
            hightlightAlpha = 0;
            return;
        }
        if (!up)
        {
            if (hightlightAlpha <= 0.2)
            {
                alphaIncreasing = true;
                return;
            }
            if (hightlightAlpha > 0.2)
            {
                hightlightAlpha = hightlightAlpha - 0.01f;
            }
            return;
        }
        if (up)
        {
            if (hightlightAlpha >= 0.9)
            {
                alphaIncreasing = false;
                return;
            }
            if (hightlightAlpha < 0.9)
            {
                hightlightAlpha = hightlightAlpha + 0.01f;
            }
            return;
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.dataHeight = RecordHeight;
        data.dataPoints = CurrentPoints;
        data.collectionBlue = jManager.CheckJorblesInCollection("blue");
        data.collectionRed = jManager.CheckJorblesInCollection("red");
        data.collectionGreen = jManager.CheckJorblesInCollection("green");
        data.collectionTeal = jManager.CheckJorblesInCollection("teal");
        data.collectionPink = jManager.CheckJorblesInCollection("pink");
        data.collectionLime = jManager.CheckJorblesInCollection("lime");
        data.collectionPurple = jManager.CheckJorblesInCollection("purple");
        data.collectionOrange = jManager.CheckJorblesInCollection("orange");
        data.collectionYellow = jManager.CheckJorblesInCollection("yellow");
        data.deckBlue = CheckJorblesInDeck("blue");
        data.deckRed = CheckJorblesInDeck("red");
        data.deckGreen = CheckJorblesInDeck("green");
        data.deckTeal = CheckJorblesInDeck("teal");
        data.deckPink = CheckJorblesInDeck("pink");
        data.deckLime = CheckJorblesInDeck("lime");
        data.deckPurple = CheckJorblesInDeck("purple");
        data.deckOrange = CheckJorblesInDeck("orange");
        data.deckYellow = CheckJorblesInDeck("yellow");

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            RecordHeight = data.dataHeight;
            CurrentPoints = data.dataPoints;
            int addBlues = data.collectionBlue - jManager.CheckJorblesInCollection("blue");
            for (int i = 0; i < addBlues; i++)
            {
                jManager.AddNewJorbleToCollection("blue");
            }
            int addReds = data.collectionRed - jManager.CheckJorblesInCollection("red");
            for (int i = 0; i < addReds; i++)
            {
                jManager.AddNewJorbleToCollection("red");
            }
            int addGreens = data.collectionGreen - jManager.CheckJorblesInCollection("green");
            for (int i = 0; i < addGreens; i++)
            {
                jManager.AddNewJorbleToCollection("green");
            }
            int addTeals = data.collectionTeal - jManager.CheckJorblesInCollection("teal");
            for (int i = 0; i < addTeals; i++)
            {
                jManager.AddNewJorbleToCollection("teal");
            }
            int addPinks = data.collectionPink - jManager.CheckJorblesInCollection("pink");
            for (int i = 0; i < addPinks; i++)
            {
                jManager.AddNewJorbleToCollection("pink");
            }
            int addLimes = data.collectionLime - jManager.CheckJorblesInCollection("lime");
            for (int i = 0; i < addLimes; i++)
            {
                jManager.AddNewJorbleToCollection("lime");
            }
            int addPurples = data.collectionTeal - jManager.CheckJorblesInCollection("purple");
            for (int i = 0; i < addPurples; i++)
            {
                jManager.AddNewJorbleToCollection("purple");
            }
            int addOranges = data.collectionPink - jManager.CheckJorblesInCollection("orange");
            for (int i = 0; i < addOranges; i++)
            {
                jManager.AddNewJorbleToCollection("orange");
            }
            int addYellows = data.collectionLime - jManager.CheckJorblesInCollection("yellow");
            for (int i = 0; i < addYellows; i++)
            {
                jManager.AddNewJorbleToCollection("yellow");
            }
            List<CTowerJorble> LoadJorbleDeck = new List<CTowerJorble>();
            for (int i = 0; i < data.deckBlue; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("blue"));
            }
            for (int i = 0; i < data.deckRed; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("red"));
            }
            for (int i = 0; i < data.deckGreen; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("green"));
            }
            for (int i = 0; i < data.deckTeal; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("teal"));
            }
            for (int i = 0; i < data.deckPink; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("pink"));
            }
            for (int i = 0; i < data.deckLime; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("lime"));
            }
            for (int i = 0; i < data.deckPurple; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("purple"));
            }
            for (int i = 0; i < data.deckOrange; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("orange"));
            }
            for (int i = 0; i < data.deckYellow; i++)
            {
                LoadJorbleDeck.Add(new CTowerJorble("yellow"));
            }
            jorbleDeck = LoadJorbleDeck;
        }
    }

    [Serializable]
    class PlayerData
    {
        public int dataHeight;
        public int dataPoints;
        public int collectionBlue;
        public int collectionRed;
        public int collectionGreen;
        public int collectionTeal;
        public int collectionPink;
        public int collectionLime;
        public int collectionPurple;
        public int collectionOrange;
        public int collectionYellow;
        public int deckBlue;
        public int deckRed;
        public int deckGreen;
        public int deckTeal;
        public int deckPink;
        public int deckLime;
        public int deckPurple;
        public int deckOrange;
        public int deckYellow;
    }
}
