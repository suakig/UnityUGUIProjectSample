using UnityEngine;
using System.Collections;

/*********************************************
 * 
 * バージョン２
 * public float NOW { get { return this.Now; } }
 * public float DEL { get { return this.Del; } }
 * を追加
 * 
 * おんなじ処理何度も書くのめんどいから作った。
 * 最初に経過時間の初期値を入れれば後は定期的にtrueを返してくれるクラスです。
 * 
 *********************************************/

public class TimeDo
{
	private bool run  = true;
	private float Del = 0.0f;//ショットが消えるまでの時間
	private float Now = 0.0f;

	/********************************************************************
     * true   最初にtrueを返す
     * false  最初にfalseを返す
     * ******************************************************************/
	public void Init(float _Del, bool first = false)
	{
		Del = _Del;
		if (first) Now = _Del;
		else zero();
	}
	/********************************************************************
     * 最大時間変更
     * ******************************************************************/
	public void Change(float _Del)
	{
		Del = _Del;
	}
	/********************************************************************
     * 時間加算
     * ******************************************************************/
	public void Add(float _Del)
	{
		Del += _Del;
	}
	/********************************************************************
     * 
     * loop:true   ずっとtrueを返す。
     * loop:false  一定の時間置きにtrueを出す。
     * 
     * AnyTimeDo:true  時間がとまってもやる
     * AnyTimeDo:false 時間が止まったら止まる
     * 
     * ******************************************************************/
	public bool Count(bool loop = false, bool AnyTimeDo = false)
	{
		if (run)
		{
			if (AnyTimeDo)  Now += _Time.deltaTime;
			else            Now +=  Time.deltaTime;

			if (Del <= Now)
			{
				if (!loop)
				{
					Now -= Del;// 0.0f;
				}
				return true;
			}
		}
		return false;
	}
	public bool FixedCount(bool zero)
	{
		if (run)
		{
			Now += Time.fixedDeltaTime;

			if (Del <= Now)
			{
				if (!zero)
				{
					Now -= Del;// 0.0f;
				}
				return true;
			}
		}
		return false;
	}
	/********************************************************************
     * 確認
     * ******************************************************************/
	public bool Check()
	{
		if (Del < Now)
		{
			return true;
		}
		return false;
	}
	/********************************************************************
     * 再開する
     * ******************************************************************/
	public void start()
	{
		run = true;
	}
	/********************************************************************
     * 停止する
     * ******************************************************************/
	public void stop()
	{
		run = false;
	}
	/********************************************************************
     * 経過時間を初期化する
     * ******************************************************************/
	public void zero()
	{
		Now = 0.0f;
	}
	/********************************************************************
     * 経過時間を最大にする
     * ******************************************************************/
	public void max()
	{
		Now = Del;
	}

	public float Rate 
	{
		get{
			if (Del == 0) return 0;
			return  1.0f - (Now/Del);
		}
	}

	public float NOW { get { return this.Now; } }
	public float DEL { get { return this.Del; } }
}