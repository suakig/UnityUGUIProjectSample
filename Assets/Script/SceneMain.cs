using UnityEngine;
using System.Collections;

public class SceneMain : Scene {

	public void TapRetry ()
	{
		Fade ("MainSample");
	}

	public void TapBackToTitle ()
	{
		Fade ("TitleSample");
	}
}
