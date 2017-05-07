using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CanvasController : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject game;
    [SerializeField] float startGameDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        mainMenu.GetComponent<Animator>().SetTrigger("Move");
        StartCoroutine(ShowGame());
    }

    IEnumerator ShowGame() {
        yield return new WaitForSeconds(startGameDelay);
        game.SetActive(true);
    }
}
