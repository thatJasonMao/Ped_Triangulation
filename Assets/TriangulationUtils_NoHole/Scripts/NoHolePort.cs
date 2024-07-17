using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriangulationUtils_NoHole
{
    /// <summary>
    /// NoHole�Ķ��Ⱪ¶�ӿ�
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

        private List<Vector3> BoundaryPoints;

        /// <summary>
        /// ���������������С�ڽ�
        /// </summary>
        private float MinAngle = 20f;

        /// <summary>
        /// ���������������̱߳�
        /// </summary>
        private float MinSegement = 1.5f;

        public void BulidTriangulation(List<Vector3> v3_boundary, float f_angle, float f_segement)
        { 
        
        }

        public void BulidTriangulation(List<Vector3> v3_boundary)
        {
            BulidTriangulation(v3_boundary, MinAngle, MinSegement);
        }

        public void ClearTriangulation()
        { 
        
        }

        public void TrimBoundary()
        { 
        
        }
    }
}
