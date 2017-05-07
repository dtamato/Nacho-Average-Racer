using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ScoreCanvas : MonoBehaviour {

	[SerializeField] float fadeSpeed = 1;
	[SerializeField] float moveSpeed = 0.1f;

	[Space]

	[SerializeField] Sprite positivePointsSprite;
	[SerializeField] Sprite negativePointsSprite;

	Image scoreImage;


	void Awake () {

		scoreImage = this.GetComponentInChildren<Image> ();
	}

	void Update () {

		FadeText ();
		MoveText ();
	}

	void FadeText () {

		float newAlpha = scoreImage.color.a - fadeSpeed * Time.deltaTime;

		if (newAlpha > 0) {

			scoreImage.color = new Color (scoreImage.color.r, scoreImage.color.g, scoreImage.color.b, newAlpha);
		}
		else {

			Destroy (this.gameObject);
		}
	}

	void MoveText () {

		this.transform.Translate (moveSpeed * Time.deltaTime * Vector3.up);
	}

	public void SetSprite (bool isGood) {

		scoreImage.sprite = isGood ? positivePointsSprite : negativePointsSprite;
	}
}
