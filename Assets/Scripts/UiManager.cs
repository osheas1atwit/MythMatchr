using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Image sr;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] List<TextMeshProUGUI> hintTexts;
    private int currentHint = 0;
    private Entity currentEntity;

    [SerializeField] GameObject correctPanel;

    [SerializeField] Transform iconParent;
    [SerializeField] GameObject lifeIcon;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI soundVolume;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    private Coroutine correctRoutine;
    private Coroutine gameOverRoutine;

    public void InitUi(Player player, Entity newEnt)
    {
        for (int i = 0; i < player.maxLives; i++)
        {
            Instantiate(lifeIcon, iconParent);
        }
        // first ent
        sr.sprite = newEnt.Image;
        nameText.text = newEnt.name;
        descriptionText.text = newEnt.Description;
        currentEntity = newEnt;
        currentHint = 0;

        for (int i = 0; i < hintTexts.Count; i++)
        {
            hintTexts[i].text = "";
        }
    }

    public void SetNewEnity(Entity newEnt, bool correct, int score)
    {
        // Answered correctly so now we set a new entity
        sr.sprite = newEnt.Image;
        nameText.text = newEnt.name;
        descriptionText.text = newEnt.Description;
        currentEntity = newEnt;
        currentHint = 0;

        for (int i = 0; i < hintTexts.Count; i++)
        {
            hintTexts[i].text = "";
        }

        if (correct)
        {
            scoreText.text = "Score " + score;
            // Also tell the player they are correct
            if(correctRoutine != null)
            {
                StopCoroutine(correctRoutine);
            }
            StartCoroutine(CorrectWait());
        }
    }
    
    public void nextHint()
    {
        hintTexts[currentHint].text = currentEntity.Hints[currentHint];
        currentHint++;
    }

    public IEnumerator CorrectWait()
    {
        correctPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        correctPanel.SetActive(false);
    }

    public void Hurt()
    {
        Destroy(iconParent.GetChild(iconParent.childCount-1).gameObject);
    }

    public void updateVolumeText(float vol)
    {
        string volume = string.Format("Volume: {0:.0}", vol);
        soundVolume.text = volume;
    }

    public void gameOver(int score)
    {
        if (gameOverRoutine != null)
        {
            StopCoroutine(gameOverRoutine);
        }
        StartCoroutine(endGame(score));
    }

    public IEnumerator endGame(int score)
    {
        gameOverScoreText.text = "Score " + score;
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneGuy.instance.LoadMainMenu();
    }

    public void quitGame()
    {
        SceneGuy.instance.LoadMainMenu();
    }

}
