support digruberg@gmail.com

ElectricityLine3D

Created:
That would create an electrical line you need to call up the menu GameObject / Create Other / Electricity Line 3D, then the scene will create a fully configured object .
Next to assign the Object Inspector , which will be created between the line , or save them through a public method

Variables : the value of all variables is intuitive and requires no additional clarifications
nodal point of the line can

Public methods:

public void SetPoints (params Vector3 [] nPoints) - saves the nodal point of the line in the format Vector3, while hotspots Transform type reset
public void SetPoints (List <Vector3> nPoints) - saves the nodal point of the line in the format Vector3, while hotspots Transform type reset

public void SetPoints (params Transform [] nPoints) - saves the nodal point of the line in the format Transform, while hotspots peremeschatsya will store the object after
public void SetPoints (List <Transform> nPoints) - saves the nodal point of the line in the format Transform, while hotspots peremeschatsya will store the object after

public void AddPoints (params Vector3 [] nPoints) - adds points to existing
public void AddPoints (List <Vector3> nPoints) - adds points to existing
public void AddPoints (params Transform [] nPoints) - adds points to existing
public void AddPoints (List <Transform> nPoints) - adds points to existing

public Vector3 [] GetPoints () - returns nodes in Vector3 format
public Vector3 [] GetPointsPathLines - returns waypoint format Vector3
public Vector3 [] GetSmoothingPointsPathLines () - returns the point smoothed the way to format Vector3