using UnityEngine;
using System.Collections;

public class SceneTitle : Scene {

	enum State{
		None,
		SceneMove,
		QuitGame,
	}
	State state;

	void Start() {
		state = State.None;
	}

	public void TapGameStart ()
	{
		Fade ();
		state = State.SceneMove;
	}

	public void TapQuit ()
	{
		Fade ();
		state = State.QuitGame;
	}

	protected override void FadeOutEnd ()
	{
		switch(state){
		case State.SceneMove:
			Application.LoadLevel ("MainSample");
			break;
		case State.QuitGame:
			Quit ();
			break;
		}
	}

	public void Quit ()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
