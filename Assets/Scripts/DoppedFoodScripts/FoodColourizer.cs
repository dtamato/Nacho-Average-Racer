using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodColourizer : MonoBehaviour {

    [SerializeField] Material[] foodMat;
        
        void Start ()
        {
            GetComponent<MeshRenderer>().material = foodMat[Random.Range(0, foodMat.Length)];
        }   

}
