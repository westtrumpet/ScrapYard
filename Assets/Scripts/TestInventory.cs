using UnityEngine;
using System.Collections;

public class TestInventory : MonoBehaviour {

	public int currentPickup;

	void OnTriggerEnter(Collider collider) {
		if(collider.tag == "Pickup") {
			Destroy(collider.gameObject);
			currentPickup++;
		}
	}

}
