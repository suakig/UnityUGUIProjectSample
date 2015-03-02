using UnityEngine;
using System.Collections;

public class FadeSetAsFirstSibling : MonoBehaviour {
	void LateUpdate () {
		transform.SetAsLastSibling();
	}
}
