using UnityEngine;
using System.Collections;

public class ViewingDirection : MonoBehaviour {

	// UnityChan_AssetのViewingDirectionを改造
	// 自作シェーダ対応改造Script

	[Header("<< Script by UnityChan Asset")]

	public bool DebugViewing = false;

	// 手動操作の許可
	public bool LookControl_On = false;

	// 視線はTargetを見て操作する仕様
	public Transform Look_Target;

	public bool ViewingReverseX = false;
	private int viewingReverseX_val = -1;

	//目のメッシュ設定
	[Header("目の")]
	public MeshRenderer eyeObjL;
	public MeshRenderer eyeObjR;

	[Range(-0.5f, 0.5f)]
	public float x_val = 0;
	[Range(-0.64f, 0.8f)]
	public float y_val = 0;

	private Vector2 eyeOffset;

	void Start() {
		eyeObjL = GameObject.Find("EyePl_L").GetComponent<MeshRenderer>();
		eyeObjR = GameObject.Find("EyePl_R").GetComponent<MeshRenderer>();
		eyeOffset = Vector2.zero;

		if (ViewingReverseX == true)
			viewingReverseX_val = 1;
		else
			viewingReverseX_val = -1;
	}

	void Update() {
		if (LookControl_On != true && Look_Target) {
			x_val = viewingReverseX_val * Look_Target.position.x / 2;
			y_val = -1 * Look_Target.position.y / 2;    // 視線の上下向きを逆にするため*-1する

			if (DebugViewing)
				Debug.Log("視線_" + x_val + "_" + y_val);

			// 視線の制御
			if (x_val > 0.5f) {
				x_val = 0.5f;
			} else if (x_val < -0.5f) {
				x_val = -0.5f;
			}
			if (y_val > 0.8f) {
				y_val = 0.8f;
			} else if (y_val < -0.64f) {
				y_val = -0.64f;
			}
		}

		eyeOffset = new Vector2(x_val * 0.2f, y_val * 0.08f);

		// 要効率化修正
		eyeObjL.material.SetTextureOffset("_EyeTex", eyeOffset);
		eyeObjR.material.SetTextureOffset("_EyeTex", eyeOffset);
		eyeObjL.material.SetTextureOffset("_HighLightTex", eyeOffset);
		eyeObjR.material.SetTextureOffset("_HighLightTex", eyeOffset);
		eyeObjL.material.SetTextureOffset("_SpecialTex", eyeOffset);
		eyeObjR.material.SetTextureOffset("_SpecialTex", eyeOffset);
	}
}