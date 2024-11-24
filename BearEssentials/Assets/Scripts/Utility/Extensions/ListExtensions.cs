using System;
using System.Collections.Generic;

namespace Bears.Core
{
    public static class ListExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        public static void Enqueue<T>(this IList<T> list, T item)
        {
            list.Add(item);
        }


        public static T Dequeue<T>(this IList<T> list)
        {
            T item = list[0];
            list.RemoveAt(0);
            return item;
        }
        

        public static T PopElement<T>(this List<T> list)
        {
            int lastIndex = list.Count - 1;
            T element = list[lastIndex];
            list.RemoveAt(lastIndex);
            return element;
        }


        public static T GetRandom<T>(this List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            T element = list[index];
            return element;
        }


        public static T RemoveRandom<T>(this List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            T element = list[index];
            list.RemoveAt(index);
            return element;
        }


        public static T RemoveFirst<T>(this List<T> list)
        {
            T element = list[0];
            list.RemoveAt(0);
            return element;
        }


        public static T PeakLast<T>(this List<T> list)
        {
            T element = list[list.Count - 1];
            return element;
        }
    }
}
