using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        //Метод для отрисовки индивидуального графа
        public void DrawGraph(PictureBox sheet)
        {
            using (Graphics g = sheet.CreateGraphics())
            {
                // Очистка pictureBox
                g.Clear(Color.White);
                Font weightFont = new Font("Arial", 12f);
                Font boldFont = new Font(weightFont, FontStyle.Bold);

                //Рисование ребер
                foreach (var edge in edges)
                {

                    Point start = vertices.Find(v => v.Number == edge.StartVertex).Position;
                    Point end = vertices.Find(v => v.Number == edge.EndVertex).Position;

                    // Рисование толстых линий
                    using (Pen edgePen = new Pen(Color.Black, 2f))
                    {
                        g.DrawLine(edgePen, start, end);
                    }

                    // Вычисление позиции для веса в первой трети ребра
                    float weightPositionX = start.X + (end.X - start.X) / 3;
                    float weightPositionY = start.Y + (end.Y - start.Y) / 3;

                    // Вывод веса ребра в первой трети
                    g.DrawString(edge.Weight.ToString(), boldFont, Brushes.DarkRed, weightPositionX, weightPositionY);
                }

                // Рисование вершин
                foreach (Vertex vertex in vertices)
                {

                    float textWidth = g.MeasureString(vertex.Number.ToString(), boldFont).Width;
                    float textHeight = g.MeasureString(vertex.Number.ToString(), boldFont).Height;

                    // Учитываем радиус круга при расчете координат центра
                    float x = vertex.Position.X - 10;  // Учитываем половину диаметра (20/2)
                    float y = vertex.Position.Y - 10;  // Учитываем половину диаметра (20/2)

                    // Рисование контура круга
                    using (Pen circlePen = new Pen(Color.Black, 3f))
                    {
                        g.DrawEllipse(circlePen, x, y, 20, 20);
                    }
                    // Закрашивание круга
                    g.FillEllipse(Brushes.LavenderBlush, x, y, 20, 20);

                    // Рисование номера вершины в центре
                    float textX = x - textWidth / 2 + 10;  // Учитываем половину ширины текста
                    float textY = y - textHeight / 2 + 10; // Учитываем половину высоты текста

                    g.DrawString(vertex.Number.ToString(), boldFont, Brushes.Black, textX, textY);
                }
            }
        }
    }

}
