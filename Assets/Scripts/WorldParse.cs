using UnityEngine;
using System.Collections;
using System.IO;

public class WorldParse : MonoBehaviour {

	public int[] data;
	public Vector4[] aggregateData;
	public string fileName;
	
	// the data is given by single integers separated by commas
	// the integers define a certain piece of the map in 3d space in order
	// for instance, 1, 1, 1, 3, define that a piece number 3 is located at (1, 1, 1) of the map

	// piece numbers are defined as:
	// 0: impassable block
	// 1: slope -x
	// 2: slope +z
	// 3: slope -z
	// 4: slope +x
	// 5: pickup type 1 
	// 6: pickup type 2

	public void Load () {
		
		string path = Application.dataPath;

		StreamReader reader = File.OpenText(path + fileName);
		
		int dataCount = 0;
		 
		string line;
		StreamReader lineReader = File.OpenText(path + fileName);
		while ((line = lineReader.ReadLine()) != null) {
			string[] dataLine = line.Split();
			foreach(string number in dataLine) {
				dataCount++;
			}
		}

		data = new int[dataCount];

		int itemNumber = 0;
		while ((line = reader.ReadLine()) != null) {
			string[] items = line.Split(',');
			foreach (string item in items) {
				int dataPoint = int.Parse(item);
				data[itemNumber] = dataPoint;
				itemNumber++;
			}
		}

		aggregateData = new Vector4[dataCount/4];

		for (int i =0; i < (dataCount/4); i++) {
			aggregateData[i] = new Vector4((float) data[4*i], (float)data[4*i+1], (float)data[4*i+2], (float)data[4*i+3]);
		}

		lineReader.Close();
		reader.Close();

		lineReader.Dispose();
		reader.Dispose();
	}
}
