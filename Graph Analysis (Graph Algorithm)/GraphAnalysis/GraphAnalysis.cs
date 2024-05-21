using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class GraphAnalysis
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <param name="vertices"></param>
        /// <param name="edges"></param>
        /// <param name="startVertex"></param>
        /// <param name="outputs"></param>

        /// <summary>
        /// Analyze the edges of the given DIRECTED graph by applying DFS starting from the given "startVertex" and count the occurrence of each type of edges
        /// NOTE: during search, break ties (if any) by selecting the vertices in ASCENDING alpha-numeric order
        /// </summary>
        /// <param name="vertices">array of vertices in the graph</param>
        /// <param name="edges">array of edges in the graph</param>
        /// <param name="startVertex">name of the start vertex to begin from it</param>
        /// <returns>return array of 3 numbers: outputs[0] number of backward edges, outputs[1] number of forward edges, outputs[2] number of cross edges</returns>

        public static int[] AnalyzeEdges(string[] vertices, KeyValuePair<string, string>[] edges, string startVertex)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();

            int t = 0;
            Dictionary<string, int> dt = new Dictionary<string, int>();
            Dictionary<string, char> clor = new Dictionary<string, char>();
            int[] output = new int[3] { 0, 0, 0 };
            Dictionary<string, List<string>> gr = new Dictionary<string, List<string>>();
            int y = vertices.Length;
            for (int i = 0; i <y ; i++)
            {
                gr[vertices[i]] = new List<string>();
            }

            foreach (var edge in edges)
            {
                string src = edge.Key;
                string dest = edge.Value;

                if (!gr.ContainsKey(src))
                {
                    gr[src] = new List<string>();
                }
                gr[src].Add(dest);

            }

            foreach (var b in gr.Keys)
            {
                clor[b] = 'w';
            }
            DFS(startVertex);

            void DFS(string ver)
            {
                clor[ver] = 'G';
                t++;
                dt[ver] = t;
                foreach (var n in gr[ver].OrderBy(a => a))
                {
                    if (clor[n] == 'w')
                        DFS(n);
                    else if (clor[n] == 'G')
                        output[0]++;
                    else if (clor[n] == 'B' && dt[n] > dt[ver])
                        output[1]++;
                    else 
                        output[2]++;

                }
                clor[ver] = 'B';
            }

            return output;
        }



        #endregion
    }
}
