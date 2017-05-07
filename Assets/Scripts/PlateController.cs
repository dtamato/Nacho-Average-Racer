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

	[Header("Ingredients")]
	[SerializeField] GameObject chipsPrefab;
	[SerializeField] GameObject cheesePrefab;
	[SerializeField] GameObject tomatoesPrefab;
	[SerializeField] GameObject jalapenosPrefab;
	[SerializeField] GameObject greenOnionsPrefab;

	List<GameObject> ingredientsNeeded;
	List<GameObject> toppingsList;
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
	}

	public void SetupPlate () {

		AddExtraToppings ();
		SortIngredientsNeeded ();
		ShowNextIngredient ();
	}

	void AddExtraToppings () {

		List<GameObject> extraToppingsList = toppingsList;
		int extraToppingsCount = Random.Range (0, (int)ExtraTopping.ToppingsCount);

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

		// Delete existing bowls
		for (int i = 0; i < spawnTransforms.Length; i++) {

			if (spawnTransforms[i].childCount > 0) {
				
				Destroy (spawnTransforms[i].GetChild (0).gameObject);
			}
		}

		if (ingredientsNeeded.Count > 0) {

			//Debug.Log ("Next Ingredient: " + ingredientsNeeded [ingredientsNeeded.Count - 1]);
			GameObject nextIngredient = ingredientsNeeded [ingredientsNeeded.Count - 1];
			ingredientsNeeded.Remove (nextIngredient);

			silhoutteImage.sprite = nextIngredient.GetComponent<Ingredient> ().GetSilhoutteSprite ();

			// Spawn required ingredient
			GameObject newIngredient = Instantiate (nextIngredient, GetEmptySpawn ()) as GameObject;
			newIngredient.transform.localPosition = Vector3.zero;

			// Spawn opposing ingredient
			GameObject opposingIngredient = Instantiate (nextIngredient.GetComponent<Ingredient> ().GetOpposingIngredient (), GetEmptySpawn ()) as GameObject;
			opposingIngredient.transform.localPosition = Vector3.zero;

			// Fill the rest of the spots?
		}
		else {

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

	Transform GetEmptySpawn () {

		Transform spawnTransform = null;

		do {

			Transform randomSpawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Length)];

			if(randomSpawnTransform.childCount == 0) {

				spawnTransform = randomSpawnTransform;
			}

		} while(spawnTransform == null);

		return spawnTransform;
	}

	public void AddIngredient (Sprite inNachosSprite, bool isGood) {

		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = null;

		GameObject newIngredient = new GameObject ();
		newIngredient.AddComponent(typeof(SpriteRenderer));
		newIngredient.GetComponent<SpriteRenderer> ().sprite = inNachosSprite;
		newIngredient.GetComponent<SpriteRenderer> ().sortingOrder = this.transform.childCount + 1;
		newIngredient.transform.SetParent (this.transform);
		newIngredient.transform.localScale = 0.5f * Vector3.one;
		newIngredient.name = inNachosSprite.name;
		if (goodPlate && !isGood) { goodPlate = false; }
		int scoreToAdd = isGood ? 100 : -100;
		gameController.AddToScore (scoreToAdd);
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
