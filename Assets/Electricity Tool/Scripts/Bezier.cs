using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Bezier {
	bool closeLine = false;
    Vector3[] points;

    public Bezier(Vector3[] waypoints, bool CloseLine = false)
    {
        closeLine = CloseLine;
        points = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
            points[i] = waypoints[i];
    }
    public Bezier(Transform[] waypoints, bool CloseLine = false)
    {
        closeLine = CloseLine;
        points = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
            points[i] = waypoints[i].position;
    }
    public Bezier() { }

    public Vector3 GetPointAtTime(float t)
    {
        return CreateBenzierForPoint(t);
    }
	public Vector3[] GetSmoothingPoints(int count){
		List<Vector3> listSmoothingPoints = new List<Vector3> ();
		for (int i = 0; i < count; i ++) {
			float currentTime = (float)((float)i / ((float)count ));
			listSmoothingPoints.Add(CreateBenzierForPoint(currentTime));			
		}
		return listSmoothingPoints.ToArray ();
	}
	public Vector3[] GetSmoothingPoints(float minDistance){
		float fullDistance = 0;
		for (int i = 0; i < points.Length; i ++) {
			if(i != points.Length -1){
				fullDistance += Vector3.Distance(points[i],points[i+1]);
			}	
		}
		int count = (int)(fullDistance / minDistance);
		List<Vector3> listSmoothingPoints = new List<Vector3> ();
		for (int i = 0; i < count; i ++) {
			float currentTime = (float)i / ((float)count - 1);
			listSmoothingPoints.Add(CreateBenzierForPoint(currentTime));			
		}
		return listSmoothingPoints.ToArray ();
	}

    private Vector3 CreateBenzierForPoint(float t)
	{
		int x = (int) (t * (float)points.Length);

        if (x == points.Length && closeLine)
		    x = 0;
		
		float newT = (t * (float)points.Length) - (float)x;
		
		Vector3 prevl = points[FindCurrentIndex(x, points.Length)];
		Vector3 thisl = points[FindCurrentIndex(x + 1, points.Length)];
		Vector3 nextl = points[FindCurrentIndex(x + 2, points.Length)];
		Vector3 farl = points[FindCurrentIndex(x + 3, points.Length)];

        Vector3 delta1 = (nextl - prevl) * .166f;
        Vector3 delta2 = (farl - thisl) * .166f;

        if (closeLine) return CalculateBezierForPoints(newT, new Vector3[] { thisl, thisl + delta1, nextl - delta2, nextl });
        else if (x == points.Length -1)
            return CalculateBezierForPoints(newT, new Vector3[] { prevl });

        else 
            return CalculateBezierForPoints(newT, new Vector3[] { prevl, prevl + delta1, thisl - delta2, thisl });

	}

	private int FindCurrentIndex(int index, int count)
	{
		if (index >= count) {
			if (!closeLine)
				return count - 1;
			else
				return index % count;
				}
		else
			return index;
		
	}

    private Vector3 CalculateBezierForPoints(float t, Vector3[] locPoints)
    {
        Vector3 resultPoint = new Vector3(0,0,0);
        int lastIndex = locPoints.Length - 1;

        for (int i = 0; i <= lastIndex; i++)
        {
            resultPoint += BinomialCoefficient(lastIndex, i) * Mathf.Pow(1f - t, lastIndex - i) * Mathf.Pow(t, i) * locPoints[i];
        }

        return resultPoint;
    }

    #region Standard Maths methods
    private float BinomialCoefficient(int n, int k)
    {
        if ((k < 0) || (k > n)) return 0;
        k = (k > n / 2) ? n - k : k;
        return (float) FallingPower(n, k) / Factorial(k);
    }

    private int Factorial(int n)
    {
        if (n == 0) return 1;
        int t = n;
        while (n-- > 2) 
            t *= n;
        return t;
    }

    private int FallingPower(int n, int p)
    {
        int t = 1;
        for (int i = 0; i < p; i++) t *= n--;
        return t;
    }
    #endregion

}
