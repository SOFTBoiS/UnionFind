using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace UnionFind
{
    class QuickFind : IUnionFind
    {
        private int count;
        private int[] pointSets;

        public QuickFind(int count)
        {
            this.count = count;
            pointSets = new int[count];
            for (var i = 0; i < count; i++)
            {
                pointSets[i] = i;
            }
        }




        // connect p and q
        public void Union(int p, int q)
        {
            var setOfP = pointSets[p];
            var setOfQ = pointSets[q];
            if (Connected(p, q)) return;
            for (var i = 0; i < pointSets.Length; i++)
            {
                if (pointSets[i] == setOfP) pointSets[i] = setOfQ;
            }

            count--;
        }

        // find nearest node
        public int Find(int p)
        {
            return pointSets[p];
        }


        
        // see if p and q are connected
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        // return amount of grids
        public int Count()
        {
            return count;
        }
        
        

        public override string ToString()
        {
            var result = "";
            for (var i = 0; i < pointSets.Length; i++)
            {
                result += pointSets[i].ToString();
            }

            return result;
        }


        #region Read from text
        
        

        private IEnumerator<string> _enumerator;

        // Used to read from the text file from the assignment
        public QuickFind(string path)
        {
            // Read the data into an enumerator so we can iterate through the lines. First line is the array size.
            var data = File.ReadLines(path);
            _enumerator = data.GetEnumerator();
            _enumerator.MoveNext();
            
            // Set the count to the value in the first line of the enumerator
            this.count = int.Parse(_enumerator.Current);
            pointSets = new int[count];
            for (var i = 0; i < count; i++)
            {
                pointSets[i] = i;
            }
        }
        
        public void ConnectFromTextFile()
        {
            Console.WriteLine($"Count before: {count}");
            var i = 0;
            var stopwatch = Stopwatch.StartNew();
            while (_enumerator.MoveNext())
            {
                if (_enumerator.Current == null) break;
                i++;
                var points = _enumerator.Current.Split(" ");
                var p = int.Parse(points[0]);
                var q = int.Parse(points[1]);
                Union(p, q);
            }
            stopwatch.Stop();
            Console.WriteLine($"\nCount after: {count}");
            Console.WriteLine("QuickFind time:  " + stopwatch.Elapsed);
        }

        #endregion

    }
}