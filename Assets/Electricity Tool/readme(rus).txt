support digruberg@gmail.com

ElectricityLine3D

—оздание:
„то бы создать электрическую линию вам необходимо вызвать меню GameObject/Create Other/Electricity Line 3D, после чего в сцене создастс€ полностью настроенный обьект.
ƒалее необходимо назначить в инспекторе обьекты, между которыми будет создана лини€, или сохранить их через публичный метод

ѕеременные:значение всех переменных интуитивно пон€тно и не требует дополнительного разь€снени€

ѕубличные методы:

public void SetPoints(params Vector3[] nPoints) - сохран€ет узловые точки линии в формате Vector3, при этом узловые точки типа Transform обнул€ютс€
public void SetPoints(List<Vector3> nPoints) -   сохран€ет узловые точки линии в формате Vector3, при этом узловые точки типа Transform обнул€ютс€

public void SetPoints(params Transform[] nPoints) - сохран€ет узловые точки линии в формате Transform,при этом узловые точки будут перемещатс€ вслед за сохраненным обьектом
public void SetPoints(List<Transform> nPoints) - сохран€ет узловые точки линии в формате Transform,при этом узловые точки будут перемещатс€ вслед за сохраненным обьектом

public void AddPoints(params Vector3[] nPoints) - добавл€ет точки к уже имеющимс€
public void AddPoints(List<Vector3> nPoints) - добавл€ет точки к уже имеющимс€
public void AddPoints(params Transform[] nPoints) - добавл€ет точки к уже имеющимс€
public void AddPoints(List<Transform> nPoints) - добавл€ет точки к уже имеющимс€

public Vector3[] GetPoints() - возвращает узловые точки в формате Vector3
public Vector3[] GetPointsPathLines - возвращает точки пути в формате Vector3
public Vector3[] GetSmoothingPointsPathLines() - возвращает точки сглаженного пути в формате Vector3