using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Src.MathKit;
using UnityEngine;
using UnityEditor;

namespace Src.MathKit.Editor.Test
{
    public static class Test
    {
        [MenuItem("Tools/LRU")]
        static void TestLRU()
        {
            LRUCache<int, int> lru = new LRUCache<int, int>(3);
            lru.Put(1, 1);
            lru.Put(2, 2);
            lru.Put(3, 3);
            Debug.Log("count:" + lru.Count);
            lru.Put(4, 4);
            Debug.Log("count:" + lru.Count);
            Debug.Log("Get:2," + (lru.TryGet(2, out var res) ? res : "null"));
            Debug.Log("count:" + lru.Count);
            lru.Put(5, 5);
            Debug.Log("count:" + lru.Count);
            Debug.Log("Get:1," + (lru.TryGet(1, out res) ? res : "null"));
        }
    }
}