using UnityEngine;
using System.Collections;

public class WorldData : MonoBehaviour {

	public bool[, ,] unreachable;
	public int size;
	public Vector4[] obstacleData;
	public GameObject[] objectPrefabs;
	public GameObject[] instanceObjects;
	public WorldParse parseData;

	void Start () {
		parseData.Load();
		unreachable = new bool[size, size, size];
		obstacleData = parseData.aggregateData;
		instanceObjects = new GameObject[obstacleData.Length];
		for(int i = 0; i < obstacleData.Length; i++){
			Vector4 temp = obstacleData[i];
			GameObject prefab = objectPrefabs[(int)temp.w];
			instanceObjects[i] = Instantiate(prefab, new Vector3(temp.x, temp.y, temp.z) + prefab.transform.position, prefab.transform.rotation) as GameObject;
			if(temp.w == 0) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = true;
			}
			if(temp.w == 5) {
				instanceObjects[i].GetComponent<PickupType>().pickupType = 0;
				instanceObjects[i].GetComponent<PickupType>().hasChanged = true;
			}
			if(temp.w == 6) {
				instanceObjects[i].GetComponent<PickupType>().pickupType = 1;
				instanceObjects[i].GetComponent<PickupType>().hasChanged = true;
			}
		}
	}
}
