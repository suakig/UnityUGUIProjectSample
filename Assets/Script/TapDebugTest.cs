using UnityEngine;
using System.Collections;

public class TapDebugTest : MonoBehaviour {

	bool[] test = new bool[3];

	//InputTouchはUpdate内でしか使用できない
	void Update () {
		test[0] = InputTouch.GetMouseButton (0);
		test[1] = InputTouch.GetMouseButtonDown (0);
		test[2] = InputTouch.GetMouseButtonUp (0);
	}

	void OnGUI() {
		GUI.Label ( new Rect (0, 0, 600, 50), "InputTouch.GetMouseButton(0)" 		+ test[0]);
		GUI.Label ( new Rect (0, 30, 600, 50), "InputTouch.GetMouseButtonDown(0)"	+ test[1]);
		GUI.Label ( new Rect (0, 60, 600, 50), "InputTouch.GetMouseButtonUp(0)"		+ test[2]);
	}
}