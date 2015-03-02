using UnityEngine;
using System.Collections;

public class InputTouch {

	private static bool enable = true;
	private static bool[] down = new bool[3];

	public static void Enabled(bool _enable) {
		enable = _enable;
		ClearDown ();
	}

	public static void ClearDown () {
		for (int i = 0; i < down.Length; i++) {
			down [i] = false;
		}
	}

	public static void Update() {
		for (int i = 0; i < down.Length; i++) {
			if (Input.GetMouseButtonDown (i)) {
				down[i] = true;
			}
		}
	}

	public static void LateUpdate() {
		for (int i = 0; i < down.Length; i++) {
			if(Input.GetMouseButtonUp (i)){
				down[i] = false;
			}
		}
	}

	public static bool GetMouseButtonDown (int number) {
		return Input.GetMouseButtonDown (number) && EnableCheck();
	}

	public static bool GetMouseButton (int number) {
		if (!down [number]) {
			return false;
		}

		return Input.GetMouseButton (number) && EnableCheck();
	}

	public static bool GetMouseButtonUp (int number) {

		if (!down [number]) {
			return false;
		}

		return Input.GetMouseButtonUp (number) && EnableCheck();
	}

	private static bool EnableCheck() {
		return enable && Time.timeScale != 0;
	}
}
