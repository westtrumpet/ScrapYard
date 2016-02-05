using UnityEngine;
using System.Collections;

public class WorldData : MonoBehaviour {

	public struct direction{
		public bool UpperLeft;
		public bool UpperRight;
		public bool LowerLeft;
		public bool LowerRight;
		
		public direction(bool UL, bool UR, bool LL, bool LR) {
			UpperLeft = UL;
			UpperRight = UR;
			LowerLeft = LL;
			LowerRight = LR;
		}
	}

	public direction[, ,] unreachable;
	public int size;
	public Vector4[] obstacleData;
	public GameObject[] objectPrefabs;
	public GameObject[] instanceObjects;
	public WorldParse parseData;

	void Start () {
		parseData.Load();
		unreachable = new direction[size, size, size];
		obstacleData = parseData.aggregateData;
		instanceObjects = new GameObject[obstacleData.Length];
		for(int i = 0; i < obstacleData.Length; i++){
			Vector4 temp = obstacleData[i];
			GameObject prefab = objectPrefabs[(int)temp.w];
			instanceObjects[i] = Instantiate(prefab, new Vector3(temp.x, temp.y, temp.z) + prefab.transform.position, prefab.transform.rotation) as GameObject;

			// piece numbers are defined as:
			// 0: impassable block
			// 1: slope -x
			// 2: slope +z
			// 3: slope -z
			// 4: slope +x
			// 5: pickup type 1 
			// 6: pickup type 2

			//Impassable Block
			if(temp.w == 0) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = new direction(true, true, true, true);
			}
			//Slope -x
			if(temp.w == 1) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = new direction(false, true, true, true);
			}
			//Slope +z
			if(temp.w == 2) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = new direction(true, false, true, true);
			}
			//Slope -z
			if(temp.w == 3) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = new direction(true, true, false, true);
			}
			//Slope +x
			if(temp.w == 4) {
				unreachable[(int)temp.x + size/2, (int)temp.y, (int)temp.z + size/2] = new direction(true, true, true, false);
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
