using UnityEngine;
using System.Collections;

public class PickupType : MonoBehaviour {

	public GameObject[] pickupModels;
	public int pickupType;
	public GameObject model;
	public bool hasChanged;

	void Start () {
		GameObject temp = pickupModels[pickupType];
		model = Instantiate(temp, transform.position, temp.transform.rotation) as GameObject;
		model.transform.parent = transform;
	}
	
	void Update () {
		if(hasChanged) {
			Destroy (model);
			GameObject temp = pickupModels[pickupType];
			model = Instantiate(temp, transform.position, temp.transform.rotation) as GameObject;
			model.transform.parent = transform;
			hasChanged = false;
		}
		transform.Rotate (new Vector3 (0.0f, 20.0f, 0.0f) * Time.deltaTime);
	}

	void OnDestroy () {
		Destroy (model);
	}
}
