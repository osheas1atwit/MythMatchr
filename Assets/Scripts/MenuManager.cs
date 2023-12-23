using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] List<Entity> allEntities;
    [SerializeField] EntityType type;
    
    [SerializeField] List<Continent> selectedContinents;
    [SerializeField] List<Continent> allContinents;

    [SerializeField] int roundTime; // in seconds (0 == infinite)
    [SerializeField] int numLives; // (0 == infinite)
    [SerializeField] TMP_Text roundText;
    [SerializeField] TMP_Text guessText;

    private Coroutine loadingRoutine;


    private void Awake()
    {
        // added this code to help prevent menumanager from duplicating itself
        if (instance != null)
        {
            Debug.Log("Error: 2 menumanagers");
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        // setup continent default stuff (ALL SELECTED)
        allContinents = new List<Continent>();
        for (int i = 0; i < 6; i++)
            allContinents.Add((Continent)i);

        selectedContinents = new List<Continent>(allContinents);
    }

    public void wipeSelection()
    {
        // called when pressing "back" button from singleplayer config back to main menu
        selectedContinents.Clear();
        // is supposed to help the menu manager maintain a clean slate each time the player enters "play single player"
    }


    public void populateSelections()
    {
        // should be called each time player presses "play single player", so that all continents are considered selected
        selectedContinents = new List<Continent>(allContinents);

        // sets the default stuff for easy mode
        type = EntityType.Creature;
        roundTime = 0;
        roundText.text = roundTime.ToString();
        numLives = 0;
        guessText.text = numLives.ToString();
    }

    #region SET GAME SCOPE
    public void setContinent(int i)
    {
        if (selectedContinents.Contains((Continent)i))
            selectedContinents.Remove((Continent)i);
        else
            selectedContinents.Add((Continent)i); 
    }

    public void setType(int newType)
    {
        type = (EntityType) newType;
    }
    #endregion 

    #region SET GAME MODE
    public void setEasyMode()
    {
        roundTime = 0;
        numLives = 0;
        roundText.text = roundTime.ToString();
        guessText.text = numLives.ToString();
    }

    public void setNormalMode()
    {
        roundTime = 30;
        numLives = 3;
        roundText.text = roundTime.ToString();
        guessText.text = numLives.ToString();
    }

    public void setRoundTime(bool add)
    {
        if (add)
            roundTime += 5;
        else if (roundTime > 0)
            roundTime -= 5;
        roundText.text = roundTime.ToString();
    }

    public void setNumGuesses(bool add)
    {
        if (add)
            numLives += 1;
        else if(numLives > 0)
            numLives -= 1;
        guessText.text = numLives.ToString();
    }
    #endregion

    #region LAUNCH GAMEPLAY
    public void launchGame()
    {
        // filter out entities based on user selection
        List<Entity> package = new List<Entity>();
        for(int i = 0; i < allEntities.Count; i++)
        {
            if (allEntities[i].IsCreature != this.type)
                continue;

            if (selectedContinents.Contains((Continent) allEntities[i].Continent))
                package.Add(allEntities[i]);
        }

        selectedContinents.Clear();

        if (loadingRoutine != null)
            StopCoroutine(loadingRoutine);

        loadingRoutine = StartCoroutine(loadGame(package));

    }

    public IEnumerator loadGame(List<Entity> entities)
    {
        Debug.Log(entities.Count);
        SceneGuy.instance.LoadGameScene();
        yield return new WaitForSeconds(1f);
        GM.instance.InitGM(entities, roundTime, numLives);
        Destroy(this.gameObject);
    }
    #endregion

    // called from pause menu
    public void quit()
    {
        Application.Quit();
    }

}
