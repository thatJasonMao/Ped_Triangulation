using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TriangulationUtils_NoHole;

public class NoHoleTestLogic : MonoBehaviour
{
    public List<Vector2> Points = new List<Vector2>();

    private static string points_path = "Assets/StreamingAssets/BoundaryPoints.txt";

    private StreamReader _reader = new StreamReader(points_path);

    private Stopwatch _stopwatch = new Stopwatch();

    private void Awake()
    {
        LoadBoundaryPoints();
        FreezeReader();
        TrangulateBoundary();
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
}
