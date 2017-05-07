using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[DisallowMultipleComponent]
public class PlateController : MonoBehaviour {
	
	[SerializeField] SpriteRenderer silhoutteImage;
	[SerializeField] Transform[] spawnTransforms;
	[SerializeField] GameController gameController;
	[SerializeField] IngredientDropper ingredientDropper;
	[SerializeField] Animator andEnjoyAnimator;

	[Header("Ingredients")]
	[SerializeField] GameObject chipsPrefab;
	[SerializeField] GameObject cheesePrefab;
	[SerializeField] GameObject tomatoesPrefab;
	[SerializeField] GameObject jalapenosPrefab;
	[SerializeField] GameObject greenOnionsPrefab;

	List<GameObject> ingredientsNeeded;
	List<GameObject> toppingsList;
	IngredientType ingredientRequired;
	bool goodPlate;

	void Awake () {

		Initialize ();
		SetupPlate ();
	}

	void Initialize () {
		
		ingredientsNeeded = new List<GameObject> ();
		toppingsList = new List<GameObject> ();

		toppingsList.Add (cheesePrefab);
		toppingsList.Add (tomatoesPrefab);
		toppingsList.Add (jalapenosPrefab);
		toppingsList.Add (greenOnionsPrefab);

		ingredientsNeeded.Add (chipsPrefab);

		for(int i = this.transform.childCount - 1; i > 0; i--) {

			Destroy(this.transform.GetChild(i).gameObject);
		}

		goodPlate = true;
		this.GetComponent<Animator>().SetBool("Slide", false);
	}

	public void SetupPlate () {

		AddExtraToppings ();
		SortIngredientsNeeded ();
		ShowNextIngredient ();
	}

	void AddExtraToppings () {

		List<GameObject> extraToppingsList = toppingsList;
		int extraToppingsCount = Random.Range (1, (int)ExtraTopping.ToppingsCount);

		for (int i = 0; i < extraToppingsCount; i++) {

			int randomToppingIndex = Random.Range (0, extraToppingsList.Count);
			ingredientsNeeded.Add (extraToppingsList[randomToppingIndex]);
			extraToppingsList.RemoveAt (randomToppingIndex);
		}
	}

	void SortIngredientsNeeded () {

		List<GameObject> tempList = new List<GameObject> ();
		int listSize = ingredientsNeeded.Count;

		for (int i = 0; i < listSize; i++) {
		
			int randomIndex = Random.Range (0, ingredientsNeeded.Count);
			tempList.Add (ingredientsNeeded[randomIndex]);
			ingredientsNeeded.RemoveAt (randomIndex);
		}

		ingredientsNeeded = tempList;
		//for (int i = 0; i < ingredientsNeeded.Count; i++) { Debug.Log ("ingredient: " + ingredientsNeeded [i]); }
	}

	public void ShowNextIngredient () {
		//Debug.Log ("Showing next ingredient");
		// Delete existing bowls

		if (ingredientsNeeded.Count > 0) {
			//Debug.Log ("Getting next ingredient");
			//Debug.Log ("Next Ingredient: " + ingredientsNeeded [ingredientsNeeded.Count - 1]);
			GameObject nextIngredient = ingredientsNeeded [ingredientsNeeded.Count - 1];
			ingredientsNeeded.Remove (nextIngredient);

			ingredientRequired = nextIngredient.GetComponent<Ingredient> ().GetIngredientType ();
			silhoutteImage.sprite = nextIngredient.GetComponent<Ingredient> ().GetSilhoutteSprite ();

			// Spawn required ingredient
			GameObject newIngredient = Instantiate (nextIngredient, GetEmptySpawn()) as GameObject;
			newIngredient.transform.localPosition = Vector3.zero;

			// Spawn opposing ingredient
			GameObject opposingIngredient = Instantiate (nextIngredient.GetComponent<Ingredient> ().GetOpposingIngredient (), GetEmptySpawn()) as GameObject;
			opposingIngredient.transform.localPosition = Vector3.zero;

			// Fill the rest of the spots?
			for (int i = 0; i < spawnTransforms.Length; i++) {

				if (spawnTransforms [i].childCount == 0 || (spawnTransforms[i].childCount > 0 && spawnTransforms[i].name == "Destroying")) {

					//GameObject extraTopping = Instantiate (toppingsList[Random.Range(0, toppingsList.Count)], 15 * Vector3.one, Quaternion.identity) as GameObject;
					GameObject extraTopping = Instantiate (toppingsList[Random.Range(0, toppingsList.Count)], spawnTransforms [i]) as GameObject;
					extraTopping.transform.localPosition = Vector3.zero;
				}
			}
		}
		else {
			
			// Delete existing toppings
			for (int i = 1; i < this.transform.childCount; i++) {

				Destroy (this.transform.GetChild (i).gameObject);
			}

			// Score plate
			if (goodPlate) {

				gameController.IncrementGoodNachos ();
			}
			else {

				gameController.IncrementBadNachos ();
			}

			// Set up next plate
			Initialize ();
			SetupPlate ();
		}
	}

	public void DeleteExistingBowls () {

		for (int i = 0; i < spawnTransforms.Length; i++) {
			//Debug.Log ("Checking bowl");
			if (spawnTransforms[i].childCount > 0) {
				spawnTransforms [i].transform.GetChild(0).name = "Destroying";
				Destroy (spawnTransforms[i].GetChild (0).gameObject);
				//spawnTransforms[i].GetChild(0).gameObject.SetActive(false);
			}
		}
	}

	Transform GetEmptySpawn () {

		Transform spawnTransform = null;

		do {

			Transform randomSpawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Length)];

			if(randomSpawnTransform.childCount == 0 || (randomSpawnTransform.childCount > 0 && randomSpawnTransform.GetChild(0).name == "Destroying")) {

				spawnTransform = randomSpawnTransform;
			}

		} while(spawnTransform == null);

		return spawnTransform;
	}

	public void AddIngredient (GameObject ingredientDrop, bool ingredientsMatched) {

		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = null;

		ingredientDropper.SetObject (ingredientDrop);
		ingredientDropper.gameObject.SetActive (true);
		ingredientDropper.DropStuff ();

		if (goodPlate && !ingredientsMatched) { goodPlate = false; }
		int scoreToAdd = ingredientsMatched ? 100 : -100;
		gameController.AddToScore (scoreToAdd);
	}

	public void ShowEnjoyText () {

		if (ingredientsNeeded.Count == 0) {

			andEnjoyAnimator.SetTrigger ("Move");
		}
	}

	public void SlidePlate () {

		if (ingredientsNeeded.Count == 0) {
			
			this.GetComponent<Animator>().SetBool("Slide", true);
		}
	}

	public bool isIngredientRight (IngredientType ingredientAdded) {

		return (ingredientRequired == ingredientAdded);
	}

//	public void SpawnIngredients () {
//
//		// Delete existing bowls
//		for (int i = 0; i < spawnParent.transform.childCount; i++) {
//
//			if (spawnParent.transform.GetChild (i).childCount > 0) {
//				
//				Destroy (spawnParent.transform.GetChild (i).GetChild (0).gameObject);
//			}
//		}
//
//		// Create new bowls
//		List<GameObject> ingredientsList = ingredientsArray.ToList ();
//		for (int i = 0; i < spawnParent.childCount; i++) {
//
//			//bowlParent.transform.GetChild (i).GetComponent<Ingredient> ().ResetIngredient();
//			Vector3 spawnPosition = spawnParent.GetChild (i).position + new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), 0);
//			int newIndex = Random.Range (0, ingredientsList.Count);
//			GameObject newIngredient = Instantiate (ingredientsList[newIndex], spawnPosition, Quaternion.identity) as GameObject;
//			newIngredient.transform.SetParent (spawnParent.transform.GetChild(i));
//			ingredientsList.RemoveAt (newIndex);
//		}
//	}
}
