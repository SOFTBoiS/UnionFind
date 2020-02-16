namespace UnionFind
{
    public interface IUnionFind
    {
        void Union(int p, int q);
        int Find(int p);
        bool Connected(int p, int q);
        int Count();
    }
}