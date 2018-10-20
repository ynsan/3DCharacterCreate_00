using UnityEngine;
using System.Collections;

public class TargetCameraController : MonoBehaviour {

	public Camera m_camera;
	public float speed= 1;
	private Vector3 vec = new Vector3(0,0,0);

	Vector3 start_mouseposition;
	bool rotate_flg = false;	//マウスを押しているときtrue


	void Start () {
		this.transform.eulerAngles = m_camera.transform.eulerAngles;
		this.transform.parent = this.gameObject.transform;
		vec = this.transform.eulerAngles;
	}
		
	void Update () {

		//マウスをおしたとき
		if(Input.GetMouseButtonDown(0)){
			rotate_flg = true;
			start_mouseposition = Input.mousePosition;
		}
		//マウスを放したとき
		if(Input.GetMouseButtonUp(0)){
			rotate_flg = false;
		}

		// 水平方向のキー入力取得
		float hForce = Input.GetAxis("Horizontal");
		// 垂直方向のキー入力取得
		float vForce = Input.GetAxis("Vertical");

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		camera_zoom(scroll);


		if(rotate_flg){
			camera_rotate(-Input.mousePosition + start_mouseposition);
		} else if (hForce != 0 || vForce != 0){
			camera_rotate(new Vector3(hForce * 4, vForce * 4, 0));
		}


	}

	void camera_rotate(Vector3 rotate_vec){

		vec += new Vector3 (-rotate_vec.y, -rotate_vec.x, 0) / 10 * speed;
		/*
		if(vec.x > 90){
			vec = new Vector3(90, vec.y,vec.z);
		}else if(vec.x < -90){
			vec = new Vector3(-90,vec.y,vec.z);
		}*/
		//Debug.Log(vec);
		this.transform.localEulerAngles = vec;
		start_mouseposition = Input.mousePosition;
	}

	void camera_zoom(float zoom_num){
		Vector3 direction = (this.transform.position - m_camera.transform.position).normalized;
		m_camera.transform.position += direction * zoom_num * 0.5f;
	}
}
