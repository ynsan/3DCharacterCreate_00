using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの表情を操作します(瞬きのみ)
/// 使い方によっては瞬き以外にも適応化
/// </summary>
public class CharacterFaceController : MonoBehaviour {

	private float TL = 2f;	// 瞬き用タイムレート
	private bool eyeClosed_bool = false;

	// 顔面上のBlendShapeパーツまとめたScript
	public SetGroup_BlendShapes SetBS_FaceTop;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 瞬き
		TL -= Time.deltaTime;
		if (TL <= 0f) {
			// SetBS_FaceTop.SetBlendShape_values [2] = 眼閉じBlendShape
			if (SetBS_FaceTop.SetBlendShape_values[2] <= 100 && eyeClosed_bool == false) {
				SetBS_FaceTop.SetBlendShape_values[2] += (Time.deltaTime * 200) * 5;
			}
			else {
				eyeClosed_bool = true;
			}
			if (SetBS_FaceTop.SetBlendShape_values[2] >= 0 && eyeClosed_bool == true) {
				SetBS_FaceTop.SetBlendShape_values[2] -= (Time.deltaTime * 200) * 5;
				if (SetBS_FaceTop.SetBlendShape_values[2] <= 0) {
					eyeClosed_bool = false;
					TL = Random.Range(0.5f, 5f);
				}
			}
		}
	}
}
