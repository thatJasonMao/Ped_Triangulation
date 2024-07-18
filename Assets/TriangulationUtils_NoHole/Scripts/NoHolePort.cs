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

        private List<Vector2> BoundaryPoints;

        /// <summary>
        /// ���������������С�ڽ�
        /// </summary>
        private float MinAngle = 20f;

        /// <summary>
        /// ���������������̱߳�
        /// </summary>
        private float MinSegement = 1.5f;

        /// <summary>
        /// ��������ı߽�����������ʷ�
        /// </summary>
        /// <param name="v2_boundary"></param>
        /// <param name="f_angle"></param>
        /// <param name="f_segement"></param>
        public void BulidTriangulation(List<Vector2> v2_boundary, float f_angle, float f_segement)
        {
            BoundaryPoints = v2_boundary;
            Trim();

            BoundaryPoints = Utils2D.Constrain(BoundaryPoints, f_segement);
            var polygon = Polygon2D.Contour(BoundaryPoints.ToArray());

            var vertices = polygon.Vertices;

            //Error
            if (vertices.Length < 3) return;

            var triangulation = new Triangulation2D(polygon, f_angle);
        }

        /// <summary>
        /// ��������ı߽�����������ʷ�
        /// </summary>
        /// <param name="v2_boundary"></param>
        public void BulidTriangulation(List<Vector2> v2_boundary)
        {
            BulidTriangulation(v2_boundary, MinAngle, MinSegement);
        }

        /// <summary>
        /// ����ʷֽ��
        /// </summary>
        public void ClearTriangulation()
        {
            BoundaryPoints.Clear();
        }

        /// <summary>
        /// �޼��߽� ȷ��û�м����С�ĵ�
        /// </summary>
        public void Trim()
        {
            Debug.Log($"Trim Start, Current Boundary Points Count: {BoundaryPoints.Count}");
            List<Vector2> TrimedBoundary = new List<Vector2>();
            for (int i = 0; i < BoundaryPoints.Count; i++)
            {
                if (i == 0)
                {
                    TrimedBoundary.Add(BoundaryPoints[i]);
                }
                else
                {
                    if (Vector2.Distance(BoundaryPoints[i], TrimedBoundary[TrimedBoundary.Count - 1]) > MinSegement)
                    {
                        TrimedBoundary.Add(BoundaryPoints[i]);
                    }
                }
            }

            BoundaryPoints = TrimedBoundary;
            Debug.Log($"Trim Finish, Current Boundary Points Count: {BoundaryPoints.Count}");
        }
    }
}
