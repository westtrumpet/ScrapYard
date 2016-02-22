using UnityEngine;
using System.Collections;

public class TestMove : MonoBehaviour {

	float xAxis;
	float yAxis;
	public GameObject player;
	public WorldData world;
	Vector3 upRight = new Vector3(0.0f, 0.0f, 1.0f);
	Vector3 downRight = new Vector3(1.0f, 0.0f, 0.0f);
	Vector3 downLeft = new Vector3(0.0f, 0.0f, -1.0f);
	Vector3 upLeft = new Vector3(-1.0f, 0.0f, 0.0f);
	int size;

	void Start () {
		world = FindObjectOfType<WorldData>();
		size = world.size;
	}
	
	void Update () {
		if(Input.anyKeyDown) OnMove();
		if(Input.GetKeyDown(KeyCode.Space)) useItem();
	}

	void OnMove() {
		xAxis = Input.GetAxis("Horizontal");
		yAxis = Input.GetAxis("Vertical");

		if(xAxis > 0 && yAxis > 0){
			if(canMove(upRight)) player.transform.Translate(upRight);
		}
		if(xAxis > 0 && yAxis < 0){
			if(canMove(downRight)) player.transform.Translate(downRight);
		}
		if(xAxis < 0 && yAxis > 0){
			if(canMove(upLeft)) player.transform.Translate(upLeft);
		}
		if(xAxis < 0 && yAxis < 0){
			if(canMove(downLeft)) player.transform.Translate(downLeft);
		}
		player.transform.position = new Vector3(Mathf.Round(player.transform.position.x), player.transform.position.y, Mathf.Round(player.transform.position.z));
	}	

	bool canMove(Vector3 dir){
		Vector3 target = dir + player.transform.position;
		target = new Vector3(target.x, Mathf.Round(target.y), target.z);
		if(dir.x == -1.0f){
			return (!world.unreachable[(int) target.x + size/2, (int) target.y, (int) target.z + size/2].UpperLeft) ;
		}
		else if(dir.z == 1.0f){
			return (!world.unreachable[(int) target.x + size/2, (int) target.y, (int) target.z + size/2].UpperRight) ;
		}
		else if(dir.z == -1.0f){
			return (!world.unreachable[(int) target.x + size/2, (int) target.y, (int) target.z + size/2].LowerLeft) ;
		}
		else{
			return (!world.unreachable[(int) target.x + size/2, (int) target.y, (int) target.z + size/2].LowerRight) ;
		}
	}

	void useItem() {
		Debug.Log("Use Item");
	}

}
