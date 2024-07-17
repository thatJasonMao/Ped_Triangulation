using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriangulationUtils_NoHole
{
    /// <summary>
    /// NoHole的对外暴露接口
    /// </summary>
    public class NoHolePort : MonoBehaviour
    {
        private static NoHolePort instance;

        public static NoHolePort Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoHolePort();
                }
                return instance;
            }
        }

        public List<Vector3> BoundaryPoints;

        public void BulidTriangulation()
        { 
        
        }

        public void ClearTriangulation()
        { 
        
        }

        public void TrimBoundary()
        { 
        
        }
    }
}
