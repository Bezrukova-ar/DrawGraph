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
            //координаты вершин
            vertices.Add(new Vertex { Number = 1, Position = new Point(300, 45) });
            vertices.Add(new Vertex { Number = 2, Position = new Point(385, 90) });
            vertices.Add(new Vertex { Number = 3, Position = new Point(430, 175) });
            vertices.Add(new Vertex { Number = 4, Position = new Point(385, 260) });
            vertices.Add(new Vertex { Number = 5, Position = new Point(300, 305) });
            vertices.Add(new Vertex { Number = 6, Position = new Point(215, 260) });
            vertices.Add(new Vertex { Number = 7, Position = new Point(170, 175) });          
            vertices.Add(new Vertex { Number = 8, Position = new Point(215, 90) });

            //какие вершины соединяют ребра                
            edges.Add(new Edge { StartVertex = 1, EndVertex = 2, Weight = 2 });
            edges.Add(new Edge { StartVertex = 1, EndVertex = 8, Weight = 5 });
            edges.Add(new Edge { StartVertex = 2, EndVertex = 8, Weight = 3 });
            edges.Add(new Edge { StartVertex = 2, EndVertex = 6, Weight = 1 });
            edges.Add(new Edge { StartVertex = 2, EndVertex = 4, Weight = 1 });
            edges.Add(new Edge { StartVertex = 3, EndVertex = 7, Weight = 7 });
            edges.Add(new Edge { StartVertex = 3, EndVertex = 4, Weight = 5 });
            edges.Add(new Edge { StartVertex = 3, EndVertex = 4, Weight = 5 });
            edges.Add(new Edge { StartVertex = 4, EndVertex = 8, Weight = 2 });
            edges.Add(new Edge { StartVertex = 4, EndVertex = 5, Weight = 1 });
            edges.Add(new Edge { StartVertex = 5, EndVertex = 6, Weight = 2 });

            // Построение графа
            DrawGraph();
        }
        private void DrawGraph()
        {
            using (Graphics g = sheet.CreateGraphics())
            {
                // Очистка pictureBox
                g.Clear(Color.White);
                Font weightFont = new Font(Font.FontFamily, 12f);
                Font boldFont = new Font(Font, FontStyle.Bold);
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
                    g.DrawString(edge.Weight.ToString(), weightFont, Brushes.Red, weightPositionX, weightPositionY);
                }

                // Рисование вершин
                foreach (var vertex in vertices)
                {
                    float textWidth = g.MeasureString(vertex.Number.ToString(), Font).Width;
                    float textHeight = g.MeasureString(vertex.Number.ToString(), Font).Height;

                    float x = vertex.Position.X - textWidth / (float)4;
                    float y = vertex.Position.Y - textHeight / (float)2;

                    float circleCenterX = vertex.Position.X - textWidth/ (float)1.5;
                    float circleCenterY = vertex.Position.Y - textHeight/ (float)1.5;


                    // Рисование контура круга
                    using (Pen circlePen = new Pen(Color.Black, 3f))
                    {
                        g.DrawEllipse(circlePen, circleCenterX, circleCenterY, 25, 25);
                    }

                    // Закрашивание круга
                    g.FillEllipse(Brushes.LightCyan, circleCenterX, circleCenterY, 25, 25);

                    // Рисование номера вершины
                    g.DrawString(vertex.Number.ToString(), boldFont, Brushes.Black, x, y);
                }
            }
        }
    }
}
