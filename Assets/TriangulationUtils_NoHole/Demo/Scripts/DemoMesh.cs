using UnityEngine;
using Random = UnityEngine.Random;

namespace TriangulationUtils_NoHole
{
	[RequireComponent (typeof(MeshFilter))]
	[RequireComponent (typeof(Rigidbody))]
	public class DemoMesh : MonoBehaviour {

		[SerializeField] Material lineMat;

		public Triangle2D[] triangles;

		public static bool isBackwards = true;

		public static bool isRotate = true;

		void Start () {
			var body = GetComponent<Rigidbody>();
			if (isBackwards)
			{ body.AddForce(Vector3.forward * Random.Range(150f, 160f)); }
			if (isRotate)
			{ body.AddTorque(Random.insideUnitSphere * Random.Range(10f, 20f)); }
		}

		void Update () {}

		public void SetTriangulation (Triangulation2D triangulation) {
			var mesh = triangulation.Build();
			GetComponent<MeshFilter>().sharedMesh = mesh;
			this.triangles = triangulation.Triangles;
		}

		public void SetStill()
		{
			isBackwards = false;
			isRotate = false;
        }

		void OnRenderObject () {
			if(triangles == null) return;

			GL.PushMatrix();
			GL.MultMatrix (transform.localToWorldMatrix);

			lineMat.SetColor("_Color", Color.black);
			lineMat.SetPass(0);
			GL.Begin(GL.LINES);
			for(int i = 0, n = triangles.Length; i < n; i++) {
				var t = triangles[i];
				GL.Vertex(t.s0.a.Coordinate); GL.Vertex(t.s0.b.Coordinate);
				GL.Vertex(t.s1.a.Coordinate); GL.Vertex(t.s1.b.Coordinate);
				GL.Vertex(t.s2.a.Coordinate); GL.Vertex(t.s2.b.Coordinate);
			}
			GL.End();
			GL.PopMatrix();
		}
	}
}
