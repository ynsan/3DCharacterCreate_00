using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetGroup_BlendShapes : MonoBehaviour {

	// Original

	public bool DebugTest;

	// 未使用
	public bool OverWride_BlendShape = false;

	[Header("テンプレート顔モデルのBlendShapeをまとめるScript"), Header("顔のパーツの親をセット")]
	public Transform MeshParts_root;

	[Range(0, 100)]
	public float[] SetBlendShape_values;

	[Header("各メッシュのパーツ")]
	public BlendShapeMeshData[] BlendShape_MeshDatas;

	[Serializable]
	public class BlendShapeMeshData{
		private string bs_label;
		public SkinnedMeshRenderer skinnedMesh_r;
		public Mesh skinMesh;
		public int BlendShape_count;
	}

	private int bs_sum;


	void Awake () {
		if (!MeshParts_root) {
			MeshParts_root = this.transform;
		}
	}

	void Start() {
		bs_sum = 0;
		for (int i = 0; i < BlendShape_MeshDatas.Length; i++){
			// 各Meshが持つBlendShapeの数を保存
			BlendShape_MeshDatas [i].skinMesh = BlendShape_MeshDatas [i].skinnedMesh_r.sharedMesh;
			BlendShape_MeshDatas [i].BlendShape_count = BlendShape_MeshDatas [i].skinMesh.blendShapeCount;
			//
			if (bs_sum <= BlendShape_MeshDatas [i].BlendShape_count){
				bs_sum = BlendShape_MeshDatas [i].BlendShape_count;
			}
		}
		float[] SetBlendShape_values = new float[bs_sum];
	}

	void Update() {
		// BlendShapeの更新処理
		for (int i = 0; i < bs_sum; i++) {	// ブレンドシェイプを束ねる
			for (int i2 = 0; i2 < BlendShape_MeshDatas.Length; i2++) {	// 各ブレンドシェイプのMesh処理
				if (OverWride_BlendShape == !true) {
					BlendShape_MeshDatas [i2].skinnedMesh_r.SetBlendShapeWeight (i, SetBlendShape_values [i]);
				} else {
					/*
					BlendShape_MeshDatas [i2].skinnedMesh_r.SetBlendShapeWeight (
						i,
						BlendShape_MeshDatas [i2].skinnedMesh_r.bounds.size.y
						 += SetBlendShape_values [i], SetBlendShape_values [i])
					);
					*/
				}
				if (DebugTest){
					Debug.Log("BS_Debug i2 " + this.gameObject.name + ": OK " + i2);
				}
			}
			if (DebugTest){
				Debug.Log("BS_Debug1 i " + this.gameObject.name + ": OK " + i);
			}
		}
	}
}