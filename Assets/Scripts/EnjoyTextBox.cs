using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnjoyTextBox : MonoBehaviour {

	[SerializeField] PlateController plateController;

	public void SlidePlate () {

		plateController.SlidePlate ();
	}
}
