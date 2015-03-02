using UnityEngine;
using System.Collections;

public class RotateThis : MonoBehaviour {
    public Vector3 speedRotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speedRotate * Time.deltaTime);
	}
}
