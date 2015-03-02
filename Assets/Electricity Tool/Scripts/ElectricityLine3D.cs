using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Effects/Electricity Line")]
public class ElectricityLine3D: MonoBehaviour
{
    #region Setting variables
	public Transform [] pointsTransform = new Transform[0];
    [Range(0.1f, 5)]
    public float scale = 1;
    [Range(1, 60)]
    public float frequence = 37;
    [Range(0, 5)]
    public float centralAmplitude = 0.4f;
    [Range(0, 5)]
    public float peripheryAmplititude = 0f;
    [Range(0, 100)]
    public float speedChangeState = 48;
    [Range(0, 20)]
    public float smoothing = 20;
    [Range(0, 5f)]
    public float width = 1.5f;
    public Color color = new Color32(0, 181, 255, 255);
    public bool onLight = true;
    [Range(0.1f, 3)]
    public float lightIntesity = 1.7f;
    public bool drawGizmos = false;
    public Material material;
    #endregion

    #region Components
	Light[] lights;
    LineRenderer lineRenderer;
    #endregion

    #region Private variables
    Vector3[] points = new Vector3[2];
    List<Vector3> listBasePointsPatch = new List<Vector3>();
    List<Vector3> listSmoothingPointsPatch = new List<Vector3>();
    List<Vector3> previewListSmoothingPointsPatch = new List<Vector3>();
    float lastTimeChangePosition = -1;
    int previewCountPoints = 0;
    int previewCountPointsTransform = 0;
    #endregion

    #region Property
    float minDistanceBeetwenSmoothingPoints
    {
        get
        {
            return (minDistanceBeetwenPoints / (1 + smoothing)) * scale;
        }
    }
	float minDistanceBeetwenPoints{
		get{
			return (10 / (1 + frequence)) * scale;
		}
	}
	float Width{
		get{
			return (width / (1 + frequence)) * scale;
		}
	}
    float delayChangedPosition {
        get {
            return 1 / speedChangeState;
        }
    }
    #endregion

    void Awake(){
		if (!GetComponent<LineRenderer>())
			gameObject.AddComponent<LineRenderer> ();
		lineRenderer = GetComponent<LineRenderer> ();

        if(material != null)
            lineRenderer.material = material;
        else
            lineRenderer.material = new Material(Shader.Find("Particles/~Additive-Multiply"));
		

		if (pointsTransform.Length > 1) 
			SetPoints(pointsTransform);
		
		lineRenderer.renderer.sortingOrder = 10; 

		
	}
	void Update () {
        //reset array transform
        if (TestNullReferenceTransform()) {
            pointsTransform = ReCalculateArrayTransform();
        }
        if (pointsTransform.Length < 2 && previewCountPointsTransform != pointsTransform.Length)
        {
            points = new Vector3[0];
            lineRenderer.SetVertexCount(0);
        }
        previewCountPointsTransform = pointsTransform.Length;
            
        //inherit points for array transform
        if (pointsTransform.Length > 1 ) {
            points = new Vector3[pointsTransform.Length];
            for (int i = 0; i < pointsTransform.Length; i++)
                points[i] = pointsTransform[i].position;
        }
        //exit if not met the basic conditions
        if (points.Length < 2)
            return;

        //create lights
        if (points.Length != previewCountPoints && onLight || onLight && lights == null)
        {
            DestroyLights();
            lights = new Light[points.Length - 1];
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 midPoint = Vector3.Lerp(points[i], points[i + 1], 0.5f);
                GameObject go = new GameObject("light " + i.ToString());
                go.transform.position = midPoint;
                go.transform.parent = gameObject.transform;
                lights[i] = go.AddComponent<Light>();
            }
        }
        previewCountPoints = points.Length;
        //Destroy lights
        if (!onLight && lights != null)
            DestroyLights();

        if (Time.time > lastTimeChangePosition + delayChangedPosition)
        {
            lastTimeChangePosition = Time.time;
            //save prewiev state line
            if (listSmoothingPointsPatch.Count > 0)
            {
                previewListSmoothingPointsPatch.Clear();
                previewListSmoothingPointsPatch.AddRange(listSmoothingPointsPatch);
            }
            listSmoothingPointsPatch.Clear();
            listBasePointsPatch.Clear();
            AnimationCurve curveTurbulent = new AnimationCurve(new Keyframe(0, peripheryAmplititude * scale), new Keyframe(1, centralAmplitude * scale), new Keyframe(2, peripheryAmplititude * scale));
            for (int i = 0; i < points.Length - 1; i++)
            {
                //create point for line electricity
                List<Vector3> listBasePoints = new List<Vector3>();
                Vector3 directionToPoint2 = points[i + 1] - points[i];
                float fullDistance = Vector3.Distance(points[i], points[i + 1]);
                int countPoints = (int)(fullDistance / minDistanceBeetwenPoints) + 1;
                float currentDistance = (fullDistance / countPoints);
                for (int g = 0; g < countPoints + 1; g++)
                {
                    Vector3 newPointPosition = points[i] + (directionToPoint2.normalized * (currentDistance * g));
                    float coeffEvolute = ((float)g / (float)(countPoints )) * 2;
                    Vector3 axisRandom = Quaternion.AngleAxis(Random.Range(-180, 180), directionToPoint2) * new Vector3(directionToPoint2.y, directionToPoint2.x * -1, 0) ;
                    newPointPosition = newPointPosition + (axisRandom.normalized * (Random.Range(-curveTurbulent.Evaluate(coeffEvolute), curveTurbulent.Evaluate(coeffEvolute))));
                    listBasePoints.Add(newPointPosition);
                }
                listBasePointsPatch.AddRange(listBasePoints);
                //Move lights to midle points line electricity
                if (onLight)
                    lights[i].transform.position = Vector3.Lerp(points[i], points[i + 1], 0.5f);
                //create points smoothing
                Bezier bezier = new Bezier(listBasePoints.ToArray());
                int countSmoothingPoints = (int)(fullDistance / minDistanceBeetwenSmoothingPoints);
                listSmoothingPointsPatch.AddRange(bezier.GetSmoothingPoints(countSmoothingPoints));

            }
        }
        //apply light setting
        if (onLight)
            foreach (Light l in lights)
            {
                l.color = color;
                l.intensity = (Random.Range(centralAmplitude, centralAmplitude * 0.8f) * lightIntesity) *scale;
            }

        // lerp prewiev state to next state
        List<Vector3> mixedListSmoothingPointsPatch = new List<Vector3>();
        if (previewListSmoothingPointsPatch.Count > 0)
        {
            float mixCoefficient = (Time.time - lastTimeChangePosition) / delayChangedPosition;
            for (int i = 0; i < Mathf.Min(previewListSmoothingPointsPatch.Count, listSmoothingPointsPatch.Count); i++)
                mixedListSmoothingPointsPatch.Add(Vector3.Lerp(previewListSmoothingPointsPatch[i], listSmoothingPointsPatch[i], mixCoefficient));
        }
        else
            mixedListSmoothingPointsPatch.AddRange(listSmoothingPointsPatch);

        //set parametrs to lineRenderer
        lineRenderer.SetVertexCount(mixedListSmoothingPointsPatch.Count);
        for (int p = 0; p < mixedListSmoothingPointsPatch.Count; p++)
            lineRenderer.SetPosition(p, mixedListSmoothingPointsPatch[p]);

        lineRenderer.SetWidth(Width, Width);
        lineRenderer.SetColors(color, color);

	}
    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            //Draw lines
            for (int i = 0; i < listSmoothingPointsPatch.Count - 1; i++)
            {
                Gizmos.color = Color.blue;
                if (i != listSmoothingPointsPatch.Count - 1) Gizmos.DrawLine(listSmoothingPointsPatch[i], listSmoothingPointsPatch[i + 1]);
            }
            //Draw main points
            for (int i = 0; i < listBasePointsPatch.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(listBasePointsPatch[i], (minDistanceBeetwenPoints / 7) * scale);
            }
        }
    }
    void DestroyLights() {
        if (lights == null) return;
        for (int i = 0; i < lights.Length; i++)
        {
            Destroy(lights[i].gameObject);
        }
        lights = null;
        
    }
    bool TestNullReferenceTransform() {
        foreach (Transform t in pointsTransform) {
            if (t == null) return true;
        }
        return false;
    }
    Transform[] ReCalculateArrayTransform() {
        List<Transform> result = new List<Transform>();
        foreach (Transform t in pointsTransform)
        {
            if (t != null) result.Add(t);
        }
        return result.ToArray();
    }

    #region public metods
    /// <summary>
    /// Save base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void SetPoints(params Vector3[] nPoints)
    {
        pointsTransform = new Transform[0];
        points = nPoints;
    }
    /// <summary>
    /// Save base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void SetPoints(params Transform[] nPoints)
    {
        pointsTransform = nPoints;

    }
    /// <summary>
    /// Save base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void SetPoints(List<Vector3> nPoints)
    {
        pointsTransform = new Transform[0];
        points = nPoints.ToArray();
    }
    /// <summary>
    /// Save base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void SetPoints(List<Transform> nPoints)
    {
        pointsTransform = nPoints.ToArray();

    }
    /// <summary>
    /// Add base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void AddPoints(params Vector3[] nPoints) {
        pointsTransform = new Transform[0];
        List<Vector3> SummArray = new List<Vector3>();
        SummArray.AddRange(points);
        SummArray.AddRange(nPoints);
        points = SummArray.ToArray();
    }
    /// <summary>
    /// Add base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void AddPoints(List<Vector3> nPoints)
    {
        pointsTransform = new Transform[0];
        List<Vector3> SummArray = new List<Vector3>();
        SummArray.AddRange(points);
        SummArray.AddRange(nPoints);
        points = SummArray.ToArray();

    }
    /// <summary>
    /// Add base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void AddPoints(params Transform[] nPoints)
    {
        List<Transform> SummArray = new List<Transform>();
        SummArray.AddRange(pointsTransform);
        SummArray.AddRange(nPoints);
        pointsTransform = SummArray.ToArray();
    }
    /// <summary>
    /// Add base points electricity line
    /// </summary>
    /// <param name="nPoints"></param>
    public void AddPoints(List<Transform> nPoints)
    {
        List<Transform> SummArray = new List<Transform>();
        SummArray.AddRange(pointsTransform);
        SummArray.AddRange(nPoints);
        pointsTransform = SummArray.ToArray();

    }
    /// <summary>
    /// return base points electricity line
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetPoints(){
		return points;
	}
    /// <summary>
    /// return points patch electricity line
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetPointsPathLines()
    {
        Vector3[] result = new Vector3[listBasePointsPatch.Count];
        for (int i = 0; i < listBasePointsPatch.Count; i++)
            result[i] = listBasePointsPatch[i];

        return result;
    }
    /// <summary>
    /// return points smoothing patch electricity line
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetSmoothingPointsPathLines()
    {
        Vector3[] result = new Vector3[ listSmoothingPointsPatch.Count];
        for (int i = 0; i < listSmoothingPointsPatch.Count; i++)
            result[i] = listSmoothingPointsPatch[i];

        return result;
    }
    //Destroy all points electricity line
    public void Clear() {
        points = new Vector3[0];
        pointsTransform = new Transform[0];
    }

	// lerp prewiev state to next stateで座標が残っているので、
	//二つ線を引いた時に前に引いたラインから移動してしまうのでそれを回避した
	public void AllClear() {
		Clear ();
		
		// lerp prewiev state to next stateで座標が残っているので、
		//二つ線を引いた時に前に引いたラインから移動してしまうのでそれを回避した
		previewListSmoothingPointsPatch.Clear();
		listSmoothingPointsPatch.Clear();
		listBasePointsPatch.Clear();
	}
    #endregion



    
}