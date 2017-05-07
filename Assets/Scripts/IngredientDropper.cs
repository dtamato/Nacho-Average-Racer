using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDropper : MonoBehaviour {

[Header ("Items")]
[SerializeField]GameObject nachoObject;

[Header ("DropParameters")]
[SerializeField] int numberDropped;


    Collider collider;	

	void Start () {
        collider = GetComponent<Collider>();
        DropStuff();
	}
	
    public void DropStuff()
    {
        GameObject droppedItem = new GameObject("droppedItem");
        droppedItem = nachoObject;
    for (int i = 0; i< numberDropped; i++)
        {   
            Vector3 randomPoint = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y), Random.Range(collider.bounds.min.z, collider.bounds.max.z));
            Instantiate(droppedItem, randomPoint, Random.rotation);
        }
    }
	
}
