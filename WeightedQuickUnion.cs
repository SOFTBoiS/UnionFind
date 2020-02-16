using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace UnionFind
{
    public class WeightedQuickUnion
    {
        private int count;
        private int[] pointSets;
        private int[] treeSizes;

        public WeightedQuickUnion(int count)
        {
            this.count = count;
            pointSets = new int[count];
            treeSizes = new int[count];
            for (var i = 0; i < count; i++)
            {
                pointSets[i] = i;
                treeSizes[i] = 1;
            }
        }


        // connect root of p and root of q
        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            if (rootP == rootQ)
                return;
            // If P is the smallest tree
            if (treeSizes[rootP] < treeSizes[rootQ])
            {
                // add the smaller tree (p) to the bigger tree (q)
                pointSets[rootP] = rootQ;
                // Update the size value of the bigger tree (q)
                treeSizes[rootQ] += treeSizes[rootP];
            }
            else
            {
                pointSets[rootQ] = rootP;
                treeSizes[rootP] += treeSizes[rootQ];
            }

            count--;
        }

        // find nearest node
        public int Find(int p)
        {
            // Climb up the tree and look at the next value if p does not equal the current pointSet value
            while (p != pointSets[p]) p = pointSets[p];
            return p;
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
            foreach (var point in pointSets)
            {
                result += point.ToString();
            }

            return result;
        }


        #region Read from text file

        private IEnumerator<string> _enumerator;

        private int numberOfLinesInFile;

        // Used to read from the text file from the assignment
        public WeightedQuickUnion(string path)
        {
            // Read the data into an enumerator so we can iterate through the lines. First line is the array size.
            var data = File.ReadLines(path);
            _enumerator = data.GetEnumerator();
            _enumerator.MoveNext();
            // Set the count to the value in the first line of the enumerator
            this.count = int.Parse(_enumerator.Current);
            pointSets = new int[count];
            treeSizes = new int[count];
            for (var i = 0; i < count; i++)
            {
                pointSets[i] = i;
                treeSizes[i] = 1;
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
            Console.WriteLine("Weighted Quick Union time: " + stopwatch.Elapsed);

        }

        #endregion
    }
}