using UnityEngine;
using System.Collections;

public static class _Time
{
	private static int iLastFrame = 0;
	private static float fLastTime = 0.0f;
	private static float fDeltaTime = 0.0f;

	// 初期化：Start() で実行
	public static void StartDeltaTime()
	{
		fLastTime = Time.realtimeSinceStartup;
		iLastFrame = Time.frameCount;
	}

	// 更新：Update() で実行
	public static void UpdateDeltaTime()
	{
		int iCurrentFrame = Time.frameCount;
		if (iCurrentFrame != iLastFrame)
		{
			float fCurrentTime = Time.realtimeSinceStartup;
			fDeltaTime = fCurrentTime - fLastTime;
			fLastTime = fCurrentTime;
			iLastFrame = iCurrentFrame;
		}
	}

	// 取得：Time.deltaTime を _Time.deltaTime に置き換えて使用する
	public static float deltaTime
	{
		get {
			if (Time.timeScale == 0) return fDeltaTime;
			else return Time.deltaTime;
		}
	}
}