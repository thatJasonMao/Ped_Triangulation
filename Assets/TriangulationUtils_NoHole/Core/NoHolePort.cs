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

        private List<Vector2> BoundaryPoints;

        /// <summary>
        /// 生成三角网格的最小内角
        /// </summary>
        private float MinAngle = 20f;

        /// <summary>
        /// 生成三角网格的最短边长
        /// </summary>
        private float MinSegement = 0.8f;

        /// <summary>
        /// 根据输入的边界点生成三角剖分
        /// </summary>
        /// <param name="v2_boundary"></param>
        /// <param name="f_angle"></param>
        /// <param name="f_segement"></param>
        public void BulidTriangulation(List<Vector2> v2_boundary, float f_angle, float f_segement)
        {
            BoundaryPoints = v2_boundary;
            Trim();

            //优化内部过密的点
            BoundaryPoints = Utils2D.Constrain(BoundaryPoints, f_segement);

            //生成多边形
            var polygon = Polygon2D.Contour(BoundaryPoints.ToArray());
            var vertices = polygon.Vertices;

            //构建三角剖分 此时点密度的问题已经处理完毕
            var triangulation = new Triangulation2D(polygon, f_angle);
        }

        /// <summary>
        /// 根据输入的边界点生成三角剖分
        /// </summary>
        /// <param name="v2_boundary"></param>
        public void BulidTriangulation(List<Vector2> v2_boundary)
        {
            BulidTriangulation(v2_boundary, MinAngle, MinSegement);
        }

        /// <summary>
        /// 清除剖分结果
        /// </summary>
        public void ClearTriangulation()
        {
            BoundaryPoints.Clear();
        }

        /// <summary>
        /// 修剪边界 确保没有间隔过小的点
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
