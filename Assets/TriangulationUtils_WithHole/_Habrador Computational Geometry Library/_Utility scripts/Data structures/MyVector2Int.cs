using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrangulationUtils_WithHole
{
    [System.Serializable]
    public struct MyVector2Int
    {
        public int x;

        public int y;

        public MyVector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
