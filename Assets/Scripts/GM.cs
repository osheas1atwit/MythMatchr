using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance;
    [SerializeField] UiManager UM;

    [SerializeField] private Entity currentEntity;
    [SerializeField] private List<Entity> entities;

    [SerializeField] Clock clock;

    public Player player;
    private int currentGuess = 0;
    public int roundTime;
    public int numLives;


    [SerializeField] AudioSource correctAnswerSound;
    [SerializeField] AudioSource incorrectAnswerSound;
    [SerializeField] float volume;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error, 2 GM's");
            Destroy(gameObject);
        }
        instance = this;
    }

    public void InitGM(List<Entity> listEntitites, int roundTime, int numLives)
    {
        this.roundTime = roundTime;
        this.numLives = numLives;
        this.entities = listEntitites;
        volume = 1.0f;
        UM.updateVolumeText(volume);

        SetPlayer(numLives); 
        currentEntity = NewEnt(false);
        UM.InitUi(player, currentEntity);

        clock.InitTimer(roundTime);
    }

    public void SetPlayer(int lives)
    {
        player = new Player(lives);
    }

    public void Answer(string country)
    {
        if(country == currentEntity.Country)
        {
            // Correct
            // Say Correct and GIve new Entity
            correctAnswerSound.Play();
            player.Correct(100);
            currentEntity = NewEnt(true);
        }
        else
        {
            // Incorrect
            incorrectAnswerSound.Play();
            currentGuess++;
            if (currentGuess <= 2)
            {
                UM.nextHint();
            }
            else
            {
                // too many incorrect Guesses
                player.Hurt();

                // Check if Dead
                if (player.Dead())
                {
                    // Player is dead
                    UM.gameOver(player.score);
                }
                else
                {
                    if (!player.EasyMode())
                    {
                        UM.Hurt();
                    }
                    // Player just takes damage
                    currentEntity = NewEnt(false);
                }
            }
        }
    }

    public void TimerEnded()
    {
        if(player.EasyMode())
        {
            currentEntity = NewEnt(false);
            return;
        }

        player.Hurt();
        if (player.Dead())
        {
            // Player is dead
            UM.gameOver(player.score);
        }
        else
        {
            UM.Hurt();
            currentEntity = NewEnt(false);
        }
    }
    // Called whenevr we need to set a new ENtity, like the Initiation of the GM
    private Entity NewEnt(bool correct)
    {
        int rand = Random.Range(0, entities.Count);
        UM.SetNewEnity(entities[rand], correct, player.score);
        currentGuess = 0;
        clock.ClockReset();
        return entities[rand];
    }

    public void pauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void changeVolume(bool up)
    {
        if (up && volume < 1.0f)
            volume += .1f;
        else if(!up && volume > 0f)
            volume -= .1f;

        correctAnswerSound.volume = volume;
        incorrectAnswerSound.volume = volume;

        UM.updateVolumeText(volume);
    }
    public void quitGame()
    {
        entities.Clear();
        UM.quitGame();
    }
}
