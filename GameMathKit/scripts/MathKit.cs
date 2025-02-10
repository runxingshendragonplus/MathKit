using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace Src.MathKit
{

    public static class BezierKit
    {
        private static readonly Dictionary<int, List<int>> dicPascalTriangle = new Dictionary<int, List<int>>()
        {
            {1,new List<int>(){1 } },
            {2,new List<int>(){1,1 } },
            {3,new List<int>(){1,2,1 } },
            {4,new List<int>(){1,3,3,1 } },
            {5,new List<int>(){1,4,6,4,1 } },
            {6,new List<int>(){1,5,10,10,5,1 } },
            {7,new List<int>(){1,6,15,20,15,6,1 } },
            {8,new List<int>(){1,7,21,35,35,21,7,1 } },
        };

        //杨辉三角数列
        private static ReadOnlyCollection<int> GetPascalTriangle(int num)
        {
            if (num <= 0)
            {
                return null;
            }
            if (dicPascalTriangle.TryGetValue(num, out var list))
            {
                return list.AsReadOnly();
            }
            var list2 = new List<int>(num);
            int maxKey = 8;
            list2.AddRange(dicPascalTriangle[maxKey]);
            for (int i = maxKey; i < num; i++)
            {
                for (int j = list2.Count - 1; j > 0; j--)
                {
                    list2[j] = list2[j] + list2[j - 1];
                }
                list2.Add(1);
            }
            return list2.AsReadOnly();
        }

        //获取贝塞尔曲线各个顶点的系数
        public static List<float> GetBezierKey(float t, int num = 3)
        {
            t = Mathf.Clamp01(t);
            List<float> list = new List<float>(num);
            list.Add(1);
            list.Add(1 - t);
            for (int i = 2; i < num; i++)
            {
                list.Add(list[^1] * list[1]);
            }
            for (int i = list.Count - 1; i > 0; i--)
            {
                list[i] *= list[0];
                list[0] *= t;
            }
            var keys = GetPascalTriangle(num);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] *= keys[i];
            }
            return list;
        }

        //计算贝塞尔曲线的顶点
        public static Vector3 CalculateBezierPoint(float t, Vector3 p1, Vector3 p2, params Vector3[] pArr)
        {
            int num = pArr == null ? 2 : pArr.Length + 2;
            var list = GetBezierKey(t, num);
            Vector3 res = p1 * list[0];
            for (int i = 0; i < pArr.Length; i++)
            {
                res += pArr[i] * list[i + 1];
            }
            res += p2 * list[^1];

            return res;
        }
    }

}