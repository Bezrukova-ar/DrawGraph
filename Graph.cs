using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph
{
    
    class Graph
    {
        public List<Vertex> vertices = new List<Vertex>(); //Коллекция вершин
        public List<Edge> edges = new List<Edge>(); //Коллекция ребер
        // Вложенные классы для представления вершин и ребер
        public class Vertex
        {
            public int Number { get; set; }
            public Point Position { get; set; }

            // Конструктор для установки номера и позиции вершины
            public Vertex(int number, Point position)
            {
                Number = number;
                Position = position;
            }
        }

        public class Edge
        {
            public int StartVertex { get; set; }
            public int EndVertex { get; set; }
            public int Weight { get; set; }

            public Edge(int startVertex, int endVertex, int weight)
            {
                StartVertex = startVertex;
                EndVertex = endVertex;
                Weight = weight;
            }
        }
    }
}
