using UnityEditor;
using UnityEngine;


public class GridDrawing : MonoBehaviour
{
	private Mesh _mesh;

	[SerializeField] private int _gridSizeX;
	[SerializeField] private int _gridSizeY;

	// Start is called before the first frame update
	private Vector3[] _vertices;

	public void DrawGrid()
	{
		_mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = _mesh;

		_vertices = new Vector3[(_gridSizeX + 1) * (_gridSizeY + 1)];
		var verticesIndex = 0;
		for (int i = 0; i < _gridSizeY; i++)
		{
			for (int j = 0; j < _gridSizeX; j++)
			{
				_vertices[verticesIndex++] = new Vector3(i, j);
			}
		}

		_mesh.vertices = _vertices;

		DrawDebugSphere();
	}

	public void DrawDebugSphere()
	{
		ClearDebugSphere();
		var sphereParent = new GameObject("Sphere Parent");
		foreach (var vertex in _vertices)
		{
			var spherePoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			spherePoint.transform.localScale = Vector3.one * .1f;
			spherePoint.transform.position = vertex;
			spherePoint.name = $"_{vertex}";
			spherePoint.transform.parent = sphereParent.transform;
		}
	}

	public void ClearDebugSphere()
	{
		var sphereParent = GameObject.Find("Sphere Parent");
		if(sphereParent != null)
			DestroyImmediate(sphereParent);
	}

}

#region Editor

[CustomEditor(typeof(GridDrawing))]
public class GridDrawingEdior : Editor
{
	private GridDrawing _gridDrawing;
	private void Awake()
	{
		_gridDrawing = (GridDrawing)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Draw Grid"))
		{
			_gridDrawing.DrawGrid();
		}
		if (GUILayout.Button("Draw Debug Sphere"))
		{
			_gridDrawing.DrawDebugSphere();
		}
		if (GUILayout.Button("Clear Debug Sphere"))
		{
			_gridDrawing.ClearDebugSphere();
		}
	}
}
#endregion
