using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour {

	int badNachosCount;
	int goodNachosCount;
	int score;


	void Update () {


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
}
