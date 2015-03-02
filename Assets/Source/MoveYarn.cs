using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[RequireComponent(typeof(LineRenderer))]
public class MoveYarn : MonoBehaviour {
	
	private const float MOVE_TIME_LIMIT = 0.04f;

	public Transform center;
	public Transform pointPath;

	private int pointDictionaryNumber;
	private SortedDictionary<int,Transform> pointDictionary = new SortedDictionary<int,Transform>();
	private ElectricityLine3D electricityLine3D;
	private bool isMoved;
	private float timeLimit;
	private Vector3 beforeMovePoint;
	private Vector3 movePoint = Vector3.zero;
	
	void Start () {
		electricityLine3D = GetComponent<ElectricityLine3D> ();
		MakeList ();
		Init ();
		SetLine ();
	}

	void Update() {
		
		if (isMoved) {
			Move ();
			return;
		}
		
		if (Input.GetMouseButtonDown(0)) {
			movePoint =  GetMouseWorldPoint() - center.position;
			center.position += movePoint;
			Init ();
			isMoved =true;
		}
	}
	
	void Init() {
		isMoved = false;
		timeLimit = Time.time;
		pointDictionaryNumber = 0;
		beforeMovePoint = pointDictionary [pointDictionaryNumber].position;
	}
	
	void MakeList() {
		Regex regex = new Regex(@"[^0-9]");
		foreach (Transform child in pointPath) 	{
			child.renderer.enabled = false;
			pointDictionary.Add(int.Parse (regex.Replace(child.name, "")), child);
		};
	}
	
	void SetLine(){
		electricityLine3D.AllClear ();
		foreach (KeyValuePair<int, Transform> pair in pointDictionary)
		{
			electricityLine3D.AddPoints (pair.Value.transform);
		}
	}
	
	Vector3 GetMouseWorldPoint () {
		Vector3 vec = Input.mousePosition;
		vec.z = 10f;
		return Camera.main.ScreenToWorldPoint (vec);
	}
	
	void Move() {
		pointDictionary [pointDictionaryNumber].position += (movePoint / MOVE_TIME_LIMIT) * Time.deltaTime;
		if (Time.time > timeLimit + MOVE_TIME_LIMIT) {
			timeLimit = Time.time;
			pointDictionary [pointDictionaryNumber].position = beforeMovePoint + movePoint;
			if (pointDictionary.Count <= ++pointDictionaryNumber) {
				pointDictionaryNumber = 0;
				isMoved = false;
			}
			beforeMovePoint = pointDictionary [pointDictionaryNumber].position;
		}
	}
	
}
