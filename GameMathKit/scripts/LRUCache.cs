using System;
using System.Collections;
using System.Collections.Generic;

namespace Src.MathKit
{
    public class LRUCache<T, T2>
    {
        private struct Item
        {
            public T key;
            public T2 value;
        }

        private int capacity;
        public int Count => dic.Count;
        private LinkedList<Item> list;
        private Dictionary<T, LinkedListNode<Item>> dic;
        public LRUCache(int _capacity = 100)
        {
            capacity = _capacity;
            list = new();
            dic = new();
        }

        //��ȡ����
        public bool TryGet(T key, out T2 res)
        {
            if (dic.TryGetValue(key, out var node))
            {
                list.Remove(node);
                list.AddFirst(node);
                res = node.Value.value;
                return true;
            }
            res = default;
            return false;
        }

        //��ӻ�������
        public void Put(T key, T2 value)
        {
            if (dic.Count >= capacity)
            {
                dic.Remove(list.Last.Value.key);
                list.RemoveLast();
            }

            if (dic.TryGetValue(key, out var node))
            {
                var item = node.Value;
                item.value = value;
                list.Remove(node);
            }
            else
            {
                node = new LinkedListNode<Item>(new Item { key = key, value = value });
                dic.Add(key, node);
            }
            list.AddFirst(node);
        }

    }
}