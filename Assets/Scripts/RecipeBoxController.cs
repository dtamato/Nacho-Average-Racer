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
	}


    public void ShowInstructions(string ingredient) {

        int rand = 0;
        animator.enabled = true;
        animator.Rebind();

        // Set the instruction text
        rand = Random.Range(0, recipeInstruction.Length);
        intsructionsText.text = recipeInstruction[rand];

        // Set the ingredient text and color
        ingredientText.text = ingredient;
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
