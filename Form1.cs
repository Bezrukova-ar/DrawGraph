using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Vertex> vertices = new List<Vertex>(); //Коллекция вершин
        private List<Edge> edges = new List<Edge>(); //Коллекция ребер

        // Вложенные классы для представления вершин и ребер
        private class Vertex
        {
            public int Number { get; set; }
            public Point Position { get; set; }
        }

        private class Edge
        {
            public int StartVertex { get; set; }
            public int EndVertex { get; set; }
            public int Weight { get; set; }
        }
        //Загрузка графа-индивидуального задания
        private void BuildGraphButton_Click(object sender, EventArgs e)
        {
            // Очистка коллекций перед построением
            vertices.Clear();
            edges.Clear();

            //Параметры моего графа
            vertices.Add(new Vertex { Number = 1, Position = new Point(50, 50) });
            vertices.Add(new Vertex { Number = 2, Position = new Point(150, 50) });
            vertices.Add(new Vertex { Number = 3, Position = new Point(100, 150) });

            edges.Add(new Edge { StartVertex = 1, EndVertex = 2, Weight = 5 });
            edges.Add(new Edge { StartVertex = 2, EndVertex = 3, Weight = 8 });

            // Построение графа
            DrawGraph();
        }
        private void DrawGraph()
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                // Очистка pictureBox
                g.Clear(Color.White);

                // Рисование ребер
                foreach (var edge in edges)
                {
                    Point start = vertices.Find(v => v.Number == edge.StartVertex).Position;
                    Point end = vertices.Find(v => v.Number == edge.EndVertex).Position;

                    // Рисование толстых линий
                    using (Pen edgePen = new Pen(Color.Black, 2f))
                    {
                        g.DrawLine(edgePen, start, end);
                    }

                    // Вывод веса ребра рядом с ним
                    Point weightPosition = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
                    g.DrawString(edge.Weight.ToString(), Font, Brushes.Red, weightPosition);
                }

                // Рисование вершин
                foreach (var vertex in vertices)
                {
                    float textWidth = g.MeasureString(vertex.Number.ToString(), Font).Width;
                    float textHeight = g.MeasureString(vertex.Number.ToString(), Font).Height;

                    float x = vertex.Position.X - textWidth / 2;
                    float y = vertex.Position.Y - textHeight / 2;

                    float circleCenterX = vertex.Position.X - textWidth;
                    float circleCenterY = vertex.Position.Y - textHeight;
                    // Рисование контура круга
                    using (Pen circlePen = new Pen(Color.Black, 2f))
                    {
                        g.DrawEllipse(circlePen, circleCenterX, circleCenterY, 20, 20);
                    }

                    // Закрашивание круга
                    g.FillEllipse(Brushes.LightSkyBlue, circleCenterX, circleCenterY, 20, 20);

                    // Рисование номера вершины
                    g.DrawString(vertex.Number.ToString(), Font, Brushes.Black, x, y);
                }
            }
        }
    }
}
