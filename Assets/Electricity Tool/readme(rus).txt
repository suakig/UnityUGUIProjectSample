support digruberg@gmail.com

ElectricityLine3D

��������:
��� �� ������� ������������� ����� ��� ���������� ������� ���� GameObject/Create Other/Electricity Line 3D, ����� ���� � ����� ��������� ��������� ����������� ������.
����� ���������� ��������� � ���������� �������, ����� �������� ����� ������� �����, ��� ��������� �� ����� ��������� �����

����������:�������� ���� ���������� ���������� ������� � �� ������� ��������������� �����������

��������� ������:

public void SetPoints(params Vector3[] nPoints) - ��������� ������� ����� ����� � ������� Vector3, ��� ���� ������� ����� ���� Transform ����������
public void SetPoints(List<Vector3> nPoints) -   ��������� ������� ����� ����� � ������� Vector3, ��� ���� ������� ����� ���� Transform ����������

public void SetPoints(params Transform[] nPoints) - ��������� ������� ����� ����� � ������� Transform,��� ���� ������� ����� ����� ����������� ����� �� ����������� ��������
public void SetPoints(List<Transform> nPoints) - ��������� ������� ����� ����� � ������� Transform,��� ���� ������� ����� ����� ����������� ����� �� ����������� ��������

public void AddPoints(params Vector3[] nPoints) - ��������� ����� � ��� ���������
public void AddPoints(List<Vector3> nPoints) - ��������� ����� � ��� ���������
public void AddPoints(params Transform[] nPoints) - ��������� ����� � ��� ���������
public void AddPoints(List<Transform> nPoints) - ��������� ����� � ��� ���������

public Vector3[] GetPoints() - ���������� ������� ����� � ������� Vector3
public Vector3[] GetPointsPathLines - ���������� ����� ���� � ������� Vector3
public Vector3[] GetSmoothingPointsPathLines() - ���������� ����� ����������� ���� � ������� Vector3