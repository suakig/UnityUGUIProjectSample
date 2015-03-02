using UnityEngine;
using System.Collections;

public class GUI_Control_Demonstration : MonoBehaviour {
    public GUIStyle style;
    public ElectricityLine3D[] line;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width - 300, 15, 300, 50));
        if (GUILayout.Button("level 1")) Application.LoadLevel(0);
        if(GUILayout.Button("level 2"))Application.LoadLevel(1);
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(15, 15, 230, 400), style);
        GUILayout.Label("Rate");
        line[0].frequence = GUILayout.HorizontalSlider(line[0].frequence, 3.2f, 60);
        GUILayout.Label("Max Turbulence");
        line[0].centralAmplitude = GUILayout.HorizontalSlider(line[0].centralAmplitude, 0, 1.6f);
        GUILayout.Label("Min Turbulence");
        line[0].peripheryAmplititude = GUILayout.HorizontalSlider(line[0].peripheryAmplititude, 0, 1.6f);
        GUILayout.Label("Speed Turbulence");
        line[0].speedChangeState = GUILayout.HorizontalSlider(line[0].speedChangeState, 0, 100);
        GUILayout.Label("Smoothing");
        line[0].smoothing = GUILayout.HorizontalSlider(line[0].smoothing, 0, 20);
        GUILayout.Label("Width");
        line[0].width = GUILayout.HorizontalSlider(line[0].width, 0, 5);
        GUILayout.Label("On Light");
        line[0].onLight = GUILayout.Toggle(line[0].onLight, "on light");
        GUILayout.Label("Light intesity");
        line[0].lightIntesity = GUILayout.HorizontalSlider(line[0].lightIntesity, 0.1f, 3);
        if (GUILayout.Button("Random color")) {
            line[0].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }
        GUILayout.EndArea();

        //copy to all elements
        for (int i = 1; i < line.Length ; i++) {
            line[i].frequence = line[0].frequence;
            line[i].centralAmplitude = line[0].centralAmplitude;
            line[i].peripheryAmplititude = line[0].peripheryAmplititude;
            line[i].speedChangeState = line[0].speedChangeState;
            line[i].smoothing = line[0].smoothing;
            line[i].width = line[0].width;
            line[i].onLight = line[0].onLight;
            line[i].lightIntesity = line[0].lightIntesity;
            line[i].color = line[0].color;
        }
    }
}
