using UnityEngine;
using System.Collections;

public class StartUpdateSetting : MonoBehaviour {

	void Awake(){
		InputTouch.ClearDown ();
	}

	void Update() {
		_Time.StartDeltaTime ();
		InputTouch.Update ();
	}

	void LateUpdate() {
		_Time.UpdateDeltaTime ();
		InputTouch.LateUpdate ();
	}
}
