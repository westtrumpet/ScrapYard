using UnityEngine;
using System.Collections;

public class TestInventory : MonoBehaviour {

	public int currentPickup;
	public PickupType tempPickup;
	public int[] inventory;

	void Start() {
		inventory = new int[4];
		tempPickup = null;
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.tag == "Pickup") {
			tempPickup = collider.gameObject.GetComponent<PickupType>();
			inventory[tempPickup.pickupType] ++;
			Destroy(collider.gameObject);
			tempPickup = null;
			currentPickup++;
		}
	}

}
