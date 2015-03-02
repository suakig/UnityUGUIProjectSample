using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

	protected Animator animator;
	private string moveSceneName = "";

	void Awake(){
		animator = GetComponent<Animator>();
	}

	protected void Fade(string _moveSceneName = "") {
		Time.timeScale = 0;
		moveSceneName = _moveSceneName;
		animator.SetTrigger ("FadeOut");
	}

	protected virtual void FadeOutEnd () {
		if (moveSceneName != "") {
			Application.LoadLevel (moveSceneName);
			moveSceneName = "";
		}
	}

	protected virtual void FadeInEnd () {
		Time.timeScale = 1;
	}
}
