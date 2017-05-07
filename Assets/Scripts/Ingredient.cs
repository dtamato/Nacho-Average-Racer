using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExtraTopping { 

	Cheese, 
	Tomatoes, 
	Jalaps,
	GreenOnions,
	ToppingsCount
};

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Ingredient : MonoBehaviour {

	[SerializeField] string ingredientName;
	[SerializeField] bool isGood;
	[SerializeField] Sprite inNachosSprite;
	[SerializeField] Sprite silhoutteSprite;
	[SerializeField] GameObject droppedVersion;
	[SerializeField] GameObject opposingIngredient;
	[SerializeField] GameObject scoreCanvasPrefab;

    GameObject topRecipeBox;
    GameObject bottomRecipeBox;
	bool canDrag;


	void Awake () {
		
        topRecipeBox = GameObject.FindGameObjectWithTag("TopRecipe");
        bottomRecipeBox = GameObject.FindGameObjectWithTag("BottomRecipe");
		ResetIngredient ();
    }

	void OnMouseDrag () {

		if (Input.GetMouseButton (0) && canDrag) {
			
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
			mouseWorldPosition = new Vector3 (mouseWorldPosition.x, mouseWorldPosition.y, this.transform.position.z);
			this.transform.position = mouseWorldPosition;
		}
	}

	void OnTriggerStay(Collider other) {

		if (other.CompareTag ("Plate")) {

			float distance = Vector3.Distance (this.transform.position, other.transform.position);
			//Debug.Log ("distance: " + distance);
			if (canDrag && distance < 2.5f) {
				
				canDrag = false;

				GameObject newScoreCanvas = Instantiate (scoreCanvasPrefab, this.transform.position + Vector3.up, Quaternion.identity) as GameObject;
				newScoreCanvas.GetComponentInChildren<ScoreCanvas> ().SetSprite (isGood);

				other.GetComponentInChildren<PlateController> ().AddIngredient (droppedVersion, isGood);
				this.transform.GetChild (0).gameObject.SetActive (false);
				this.transform.position = Vector3.zero;

                int rand = Random.Range(0, 2);
                if (rand == 0) {
                    bottomRecipeBox.GetComponent<RecipeBoxController>().ShowInstructions(ingredientName);
                } else {
                    topRecipeBox.GetComponent<RecipeBoxController>().ShowInstructions(ingredientName);
                }

				Camera.main.GetComponent<CameraEffects> ().StartCoroutine ("CameraCut");
				Destroy (this.gameObject);
			}
		}
	}

	public void ResetIngredient () {
		canDrag = true;
		this.transform.localScale = Random.Range (1f, 1.75f) * Vector3.one;
	}

	public string GetIngredientName () {

		return ingredientName;
	}

	public Sprite GetSilhoutteSprite () {

		return silhoutteSprite;
	}

	public GameObject GetOpposingIngredient () {

		return opposingIngredient;
	}
}
