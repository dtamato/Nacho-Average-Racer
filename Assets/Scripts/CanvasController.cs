using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CanvasController : MonoBehaviour {

    GameController gc;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject game;
    [SerializeField] float startGameDelay;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] Text goodNachoText;
    [SerializeField] Text badNachoText;
    [SerializeField] Text totalNachoText;
    [SerializeField] Text scoreText;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            EndGame();
        }*/
	}

    public void StartGame() {
        mainMenu.GetComponent<Animator>().SetTrigger("Move");
        gc.StartGame();
        StartCoroutine(ShowGame());
    }

    public void EndGame() {
        goodNachoText.text = gc.GetGoodNachos().ToString();
        badNachoText.text = gc.GetBadNachos().ToString();
        int total = gc.GetGoodNachos() + gc.GetBadNachos();
        totalNachoText.text = total.ToString();
        scoreText.text = gc.GetScore().ToString();
        endGamePanel.GetComponent<Animator>().SetTrigger("Move");
    }

    IEnumerator ShowGame() {
        yield return new WaitForSeconds(startGameDelay);
        game.SetActive(true);
    }
}
