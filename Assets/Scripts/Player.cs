using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int score;
    public int maxLives;
    private int currentLives;
    public bool easyMode;

    public Player(int lives)
    {
        maxLives = lives;
        currentLives = lives;
        score = 0;
        if (lives == 0)
            easyMode = true;
        else
            easyMode = false;
    }

    public void Hurt()
    {
        if (easyMode)
            return;
        currentLives -= 1;
    }
    public bool Dead()
    {
        if (easyMode)
            return false;
        else if (currentLives <= 0)
            return true;
        else
            return false;
    }
    public void Correct(int score)
    {
        this.score += score;
    }

    public bool EasyMode()
    {
        return easyMode;
    }
}
