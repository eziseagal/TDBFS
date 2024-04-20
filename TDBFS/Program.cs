using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private int V;
    private List<int>[] adj;

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[v + 1];
        for (int i = 1; i <= v; ++i)
        {
            adj[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
        adj[w].Add(v);
    }

    private void DFSUtil(int v, bool[] visited, List<int> order)
    {
        visited[v] = true;
        order.Add(v);

        foreach (int i in adj[v])
        {
            if (!visited[i])
            {
                DFSUtil(i, visited, order);
            }
        }
    }

    public List<int> DFS(int start)
    {
        bool[] visited = new bool[V + 1];
        List<int> order = new List<int>();

        DFSUtil(start, visited, order);
        return order;
    }

    public List<int> BFS(int start)
    {
        bool[] visited = new bool[V + 1];
        List<int> order = new List<int>();
        Queue<int> queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Any())
        {
            int v = queue.Dequeue();
            order.Add(v);

            foreach (int i in adj[v])
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    queue.Enqueue(i);
                }
            }
        }

        return order;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());

        for (int test_case = 1; test_case <= t; test_case++)
        {
            string[] line = Console.ReadLine().Split(' ');
            int n = int.Parse(line[0]);
            Graph g = new Graph(n);

            for (int i = 1; i <= n; i++)
            {
                line = Console.ReadLine().Split(' ');
                int m = int.Parse(line[1]);
                for (int j = 2; j <= m + 1; j++)
                {
                    int adjacent = int.Parse(line[j]);
                    g.AddEdge(i, adjacent);
                }
            }

            Console.WriteLine($"graph {test_case}");

            while (true)
            {
                line = Console.ReadLine().Split(' ');
                int v = int.Parse(line[0]);
                int i = int.Parse(line[1]);

                if (v == 0 && i == 0)
                    break;

                List<int> order;
                if (i == 0)
                    order = g.DFS(v);
                else
                    order = g.BFS(v);

                foreach (int vertex in order)
                {
                    Console.Write($"{vertex} ");
                }
                Console.WriteLine();
            }
        }
    }
}