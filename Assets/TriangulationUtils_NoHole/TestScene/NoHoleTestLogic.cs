using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoHoleTestLogic : MonoBehaviour
{
    public List<Vector2> Points = new List<Vector2>();

    private static string points_path = "Assets/StreamingAssets/BoundaryPoints.txt";

    private StreamReader _reader = new StreamReader(points_path);

    private void Awake()
    {
        LoadBoundaryPoints();
        FreezeReader();
    }

    private void LoadBoundaryPoints()
    {
        int _counter = 0;
        while (!_reader.EndOfStream)
        {
            string line = _reader.ReadLine();
            _counter++;

            var locations = line.Split(',');
            string str_x = locations[0];
            string str_y = locations[1];

            Vector2 new_bd = new Vector2(float.Parse(str_x), float.Parse(str_y));
            Debug.Log($"新增边界点 X: {new_bd.x} Y: {new_bd.y}");
            Points.Add(new_bd);
        }
        Debug.Log($"读取完成 当前配置文件共读取 {_counter} 行");
    }

    private void FreezeReader()
    {
        _reader?.Close();
        _reader = null;
    }
}
