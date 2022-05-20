using System;
using System.Collections.Generic;
using System.Linq;

namespace Course_Schedule_II_Using_Dictionary
{
  class Program
  {
    static void Main(string[] args)
    {
      int numCourses = 5;
      int[][] prerequisites = new int[5][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 3, 4 } };
      Program p = new Program();
      var result = p.FindCourseOrder(numCourses, prerequisites);
      Console.WriteLine(string.Join(",", result));
    }

    public int[] FindCourseOrder(int numCourses, int[][] prerequisites)
    {
      // build the adjacency list
      Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
      HashSet<int> result = new HashSet<int>();
      HashSet<int> visited = new HashSet<int>();

      for (int i = 0; i < numCourses; i++)
      {
        adj.Add(i, new List<int>());
      }

      foreach(var edge in prerequisites)
      {
        int c1 = edge[0];
        int c2 = edge[1];
        var existing = adj[c1];
        existing.Add(c2);
        adj[c1] = existing;
      }

      // loop through for each course
      for (int i = 0; i < numCourses; i++)
      {
        if (!DFS(adj, result, visited, i)) return new int[0];
      }

      return result.ToArray();
    }

    private bool DFS(Dictionary<int, List<int>> adj, HashSet<int> result, HashSet<int> visited, int course)
    {
      if (visited.Contains(course)) return false;
      // no cycle found, which means the current course can be taken and so we added in the result.
      if (adj[course] == null || !adj[course].Any())
      {
        result.Add(course);
        return true;
      }

      // mark as start processing
      visited.Add(course);
      var coursesToBeCompleted = adj[course];
      foreach(int c in coursesToBeCompleted)
      {
        if (!DFS(adj, result, visited, c))
        {
          return false;
        }
      }

      // marked as processed
      visited.Remove(course);
      result.Add(course);
      adj[course] = null;
      return true;
    }
  }
}
