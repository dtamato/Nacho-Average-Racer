using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType { 

	Chips,
	FishHead,
	Cheese, 
	Carrots,
	Tomatoes,
	Marshmallows,
	Jalapenos,
	Pepperoni,
	GreenOnions,
	Pasta,
	ToppingsCount
};

public enum ExtraTopping {

	Cheese, 
	Tomatoes,
	Jalapenos,
	GreenOnions,
	ToppingsCount
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Ingredient : MonoBehaviour {

	[SerializeField] string ingredientName;
	[SerializeField] IngredientType ingredientType;
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

			Bounds ingredientBounds = this.GetComponent<Collider> ().bounds;
			Bounds plateBounds = other.GetComponent<Collider> ().bounds;
			bool ingredientInside = ingredientBounds.min.x > plateBounds.min.x && ingredientBounds.max.x < plateBounds.max.x && 
									ingredientBounds.min.y > plateBounds.min.y && ingredientBounds.max.y < plateBounds.max.y;
			
			if (canDrag && ingredientInside) {
				
				canDrag = false;

				GameObject newScoreCanvas = Instantiate (scoreCanvasPrefab, this.transform.position + Vector3.up, Quaternion.identity) as GameObject;

				bool ingredientsMatched = other.GetComponent<PlateController> ().isIngredientRight (ingredientType);
				newScoreCanvas.GetComponentInChildren<ScoreCanvas> ().SetSprite (ingredientsMatched);

				other.GetComponentInChildren<PlateController> ().AddIngredient (droppedVersion, ingredientsMatched);
				this.transform.GetChild (0).gameObject.SetActive (false);
				this.transform.position = Vector3.zero;

                int rand = Random.Range(0, 2);
                if (rand == 0) {
					bottomRecipeBox.GetComponent<RecipeBoxController>().ShowInstructions(ingredientName.ToUpper());
                } 
				else {
					topRecipeBox.GetComponent<RecipeBoxController>().ShowInstructions(ingredientName.ToUpper());
                }

				other.GetComponent<PlateController> ().ShowEnjoyText ();
				other.GetComponent<PlateController> ().DeleteExistingBowls ();
				Camera.main.GetComponent<CameraEffects> ().StartCoroutine ("CameraCut");
				Destroy (this.gameObject);
			}
		}
	}

	public void ResetIngredient () {
		canDrag = true;
		this.transform.localScale = Random.Range (1f, 1.5f) * Vector3.one;
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

	public IngredientType GetIngredientType () {

		return ingredientType;
	}
}
