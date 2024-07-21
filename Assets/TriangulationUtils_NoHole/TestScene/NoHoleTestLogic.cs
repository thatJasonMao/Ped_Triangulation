using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TriangulationUtils_NoHole;
using UnityEditor;

public class NoHoleTestLogic : MonoBehaviour
{
    public List<Vector2> Points = new List<Vector2>();

    private static string points_path = "Assets/StreamingAssets/BoundaryPoints.txt";
    private static string mesh_path = "Assets/TriangulationUtils_NoHole/Demo/Prefabs/DemoMesh.prefab";

    private StreamReader _reader = new StreamReader(points_path);

    private Stopwatch _stopwatch = new Stopwatch();

    private void Awake()
    {
        LoadBoundaryPoints();
        FreezeReader();
        TrangulateBoundary();
        BuildTriangleEntity();
    }

    private void LoadBoundaryPoints()
    {
        _stopwatch?.Reset();
        _stopwatch.Start();

        int _counter = 0;
        while (!_reader.EndOfStream)
        {
            string line = _reader.ReadLine();
            line = line.Replace(" ", "");
            _counter++;

            var locations = line.Split(',');
            string str_x = locations[0];
            string str_y = locations[1];

            Vector2 new_bd = new Vector2(float.Parse(str_x), float.Parse(str_y));
            //耗性能 暂时全干掉
            //Debug.Log($"新增边界点 X: {new_bd.x} Y: {new_bd.y}");
            Points.Add(new_bd);
        }
        _stopwatch.Stop();
        double time_ms = _stopwatch.Elapsed.TotalMilliseconds;
        Debug.Log($"读取完成 当前配置文件共读取 {_counter} 行 总耗时 {time_ms} ms");
    }

    private void FreezeReader()
    {
        _stopwatch?.Reset();
        _stopwatch.Start();

        _reader?.Close();
        _reader = null;
        double time_ms = _stopwatch.Elapsed.TotalMilliseconds;
        Debug.Log($"解耦配置文件 总耗时 {time_ms} ms");
    }

    private void TrangulateBoundary()
    {
        if (Points != null)
        {
            _stopwatch?.Reset();
            _stopwatch.Start();

            NoHolePort.Instance.BulidTriangulation(Points);
            double time_ms = _stopwatch.Elapsed.TotalMilliseconds;
            Debug.Log($"剖分边界 总耗时 {time_ms} ms");
        }
    }

    private void BuildTriangleEntity()
    {
        _stopwatch?.Reset();
        _stopwatch.Start();

        if (NoHolePort.TriangulationResult != null) { Debug.Log("Confirm Finish! 剖分结果不为空!"); }
        else
        {
            Debug.LogError("Confirm Error! 剖分结果为空");
            return;
        }

        Mesh _mesh = new Mesh();
        _mesh = NoHolePort.TriangulationResult.Build();
        GameObject mesh_root = new GameObject("TriangleMesh");
        InitMesh(mesh_root, _mesh);

        double time_ms = _stopwatch.Elapsed.TotalMilliseconds;
        Debug.Log($"构建实体三角形 总耗时 {time_ms} ms");
    }

    private void InitMesh(GameObject _root, Mesh _mesh)
    {
        GameObject mesh_obj = AssetDatabase.LoadAssetAtPath("Assets/TriangulationUtils_NoHole/Demo/Prefabs/DemoMesh.prefab", typeof(GameObject)) as GameObject;
        if (mesh_obj == null) { Debug.LogError("Mesh Prefab Is Null!"); }

        GameObject new_obj = Instantiate(mesh_obj) as GameObject;
        new_obj.transform.SetParent(_root.transform);
        new_obj.GetComponent<MeshFilter>().sharedMesh = _mesh;

        new_obj.GetComponent<DemoMesh>().SetStill();
        new_obj.GetComponent<DemoMesh>().triangles = NoHolePort.TriangulationResult.Triangles;
    }
}
