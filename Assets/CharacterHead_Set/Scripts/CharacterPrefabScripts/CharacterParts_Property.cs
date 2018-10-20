using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParts_Property : MonoBehaviour {

	// パーツの設定Script
	// Updateなどでの処理はしません

	[Header("反転許可")]
	public bool Can_xRevers = false;

	[Header("色変更の影響を受けない")]
	public bool colorInfluenceLock = false;

	public GameObject[] is_Parts;

	public void foreach_childParts (){
		//foreach (Transform child in GameObject){
		//}
	}

}
