using System;

namespace UnionFind
{
    public class Program
    {
        static void Main(string[] args)
        {
        // Note: The text file unions do not make a fully connected tree.
        var pathToSmallFile = "/home/adam/Downloads/tinyUF.txt";
        var pathToMediumFile = "/home/adam/Downloads/mediumUF.txt";
        var pathToBigFile = "/home/adam/Downloads/largeUF.txt";
        
        var qf = new QuickFind(pathToBigFile);
        qf.ConnectFromTextFile();
        Console.WriteLine(qf.ToString());

        // To make a unified array of the small file uncomment the two lines bellow
        qf.Union(8, 1);
        Console.WriteLine(qf.ToString());
        
        // var qu = new QuickUnion(pathToBigFile);
        // qu.ConnectFromTextFile();

        // var wqu = new WeightedQuickUnion(pathToBigFile);
        // wqu.ConnectFromTextFile();
    }
}

}