using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class RecipeBoxController : MonoBehaviour {

    [Header("UI References")] 
    [SerializeField] Text intsructionsText;
    [SerializeField] Text ingredientText;

    [Header("UI Design Variables")]
    [SerializeField] Color[] ingredientTextColor;
    [SerializeField] string[] recipeInstruction;
    // TEMPTEMPTEMPTEMP
    [SerializeField] string[] ingredients;

    Animator animator;
    Vector2 startPos;

	// Use this for initialization
	void Awake () {
        animator = this.GetComponent<Animator>();
        startPos = this.GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ShowInstructions();
        }
	}


    public void ShowInstructions() {

        int rand = 0;
        animator.enabled = true;
        animator.Rebind();

        // Set the instruction text
        rand = Random.Range(0, recipeInstruction.Length);
        intsructionsText.text = recipeInstruction[rand];

        // Set the ingredient text and color
        rand = Random.Range(0, ingredients.Length);
        ingredientText.text = ingredients[rand];
        rand = Random.Range(0, ingredientTextColor.Length);
        ingredientText.color = ingredientTextColor[rand];

        animator.SetTrigger("Move");
    }

    public void ResetTextBox() {
        Debug.Log("Resetting");
        animator.enabled = false;
        this.GetComponent<RectTransform>().anchoredPosition = startPos;
    }
}
