using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour {

    [SerializeField] float gameTime;
    [SerializeField] GameObject game;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject timer;

	int badNachosCount;
	int goodNachosCount;
	int score;

    bool endGame = false;
    bool gameStarted = false;

	void Update () {

        if (gameTime > 0 && gameStarted) {
            gameTime -= Time.deltaTime;
            int minutes = (int)(gameTime / 60);
            int seconds = 0;
            if (minutes > 0) {
                //Debug.Log("Found Minutes");
                seconds = (int)(gameTime - (60 * minutes));
            }
            else {
                //Debug.Log("No Minutes");
                seconds = (int)gameTime;
            }
            timer.GetComponent<Text>().text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else if (gameTime <= 0 && endGame == false) {
            game.SetActive(false);
            endGame = true;
            canvas.GetComponent<CanvasController>().EndGame();
        }
	}

	public void IncrementBadNachos () {

		badNachosCount++;
	}

	public void IncrementGoodNachos () {

		goodNachosCount++;
	}

	public void AddToScore(int scoreAdding) {

		score += scoreAdding;
	}

    public void StartGame() {
        gameStarted = true;
    }

    public int GetBadNachos() {
        return badNachosCount;
    }

    public int GetGoodNachos() {
        return goodNachosCount;
    }

    public int GetScore() {
        return score;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
