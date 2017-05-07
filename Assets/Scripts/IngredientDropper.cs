using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDropper : MonoBehaviour {

[Header ("Items")]
[SerializeField]GameObject ingredientObject;

[Header ("DropParameters")]
[SerializeField] int numberDropped;


    Collider collider;	

	void Awake () {
        collider = GetComponent<Collider>();
        //DropStuff();
	}
	
    public void DropStuff()
    {
//        GameObject droppedItem = new GameObject("droppedItem");
//        droppedItem = ingredientObject;
    	for (int i = 0; i< numberDropped; i++)
        {   
            Vector3 randomPoint = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y), Random.Range(collider.bounds.min.z, collider.bounds.max.z));
            Instantiate(ingredientObject, randomPoint, Random.rotation);
        }

		this.gameObject.SetActive (false);
    }

	public void SetObject (GameObject newObject) {

		ingredientObject = newObject;
	}
}
