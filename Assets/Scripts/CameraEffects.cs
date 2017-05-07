using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CameraEffects : MonoBehaviour {

	[SerializeField] float openingPause = 1f;
	[SerializeField] float transitionSpeed = 5;
	[SerializeField] float transitionPause = 0.1f;
	[SerializeField] Image cameraOverlay;
	[SerializeField] PlateController plateController;

	public IEnumerator CameraCut () {

		if (cameraOverlay) {

			// Opening pause
			yield return new WaitForSeconds(openingPause);

			// Fade to black
			while (cameraOverlay.color.a < 1) {

				float newAlpha = cameraOverlay.color.a + transitionSpeed * Time.deltaTime;
				cameraOverlay.color = new Color (0, 0, 0, newAlpha);
				yield return null;
			}

			// Pause
			yield return new WaitForSeconds (transitionPause);
			plateController.ShowNextIngredient ();

			// Fade to clear
			while (cameraOverlay.color.a > 0) {

				float newAlpha = cameraOverlay.color.a - transitionSpeed * Time.deltaTime;
				cameraOverlay.color = new Color (0, 0, 0, newAlpha);
				yield return null;
			}
		}
	}
}
