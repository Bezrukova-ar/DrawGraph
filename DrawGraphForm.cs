using Microsoft.VisualBasic;
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
    public partial class DrawGraphForm : Form
    {
        public DrawGraphForm()
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

            // Конструктор для установки номера и позиции вершины
            public Vertex(int number, Point position)
            {
                Number = number;
                Position = position;
            }
        }
       
        private class Edge
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
        //Загрузка графа-индивидуального задания
        private void BuildGraphButton_Click(object sender, EventArgs e)
        {
            // Очистка коллекций перед построением
            vertices.Clear();
            edges.Clear();

            //Параметры моего графа
            //координаты вершин
            vertices.Add(new Vertex(1, new Point(300, 45)));
            vertices.Add(new Vertex(2, new Point(385, 90)));
            vertices.Add(new Vertex(3, new Point(430, 175)));
            vertices.Add(new Vertex ( 4, new Point(385, 260) ));
            vertices.Add(new Vertex ( 5, new Point(300, 305) ));
            vertices.Add(new Vertex ( 6, new Point(215, 260) ));
            vertices.Add(new Vertex ( 7, new Point(170, 175) ));          
            vertices.Add(new Vertex ( 8,  new Point(215, 90) ));

            //какие вершины соединяют ребра                
            edges.Add(new Edge ( 1, 2,  2 ));
            edges.Add(new Edge ( 1, 8, 5 ));
            edges.Add(new Edge ( 2, 8, 3 ));
            edges.Add(new Edge ( 2, 6, 1 ));
            edges.Add(new Edge ( 2, 4, 1 ));
            edges.Add(new Edge ( 3, 7, 7 ));
            edges.Add(new Edge ( 3, 4, 5 ));
            edges.Add(new Edge ( 3, 4, 5 ));
            edges.Add(new Edge ( 4, 8, 2 ));
            edges.Add(new Edge ( 4, 5, 1 ));
            edges.Add(new Edge ( 5, 6, 2 ));

            // Построение графа
            DrawGraph();
        }
        //Метод для отрисовки индивидуального графа
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
                    g.DrawString(edge.Weight.ToString(), weightFont, Brushes.DarkRed, weightPositionX, weightPositionY);
                }

                // Рисование вершин
                foreach (var vertex in vertices)
                {
                    float textWidth = g.MeasureString(vertex.Number.ToString(), Font).Width;
                    float textHeight = g.MeasureString(vertex.Number.ToString(), Font).Height;

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

                    g.DrawString(vertex.Number.ToString(), Font, Brushes.Black, textX, textY);
                }
            }
        }

        //обработчик щелчка мыши по pictureBox
        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawVertexRB.Checked) // Выполняется если выбран drawVertexRB - отвечает за отрисовку вершины
            {
                // Получение позиции щелчка мыши относительно PictureBox
                Point clickPosition = e.Location;

                // Создание вершины на месте щелчка мыши с учетом номера и позиции
                Vertex newVertex = new Vertex(vertices.Count + 1, clickPosition);

                // Добавление вершины в коллекцию vertices
                vertices.Add(newVertex);

                // Перерисовка содержимого PictureBox для отображения добавленной вершины
                sheet.Invalidate();
            }
            if (drawEdgeRB.Checked) //Выполняется если выбран drawEdgeRB -отвечает за отрисовку рёбер
            {
                   
            }
        }


        // Обработчик события отрисовки содержимого PictureBox
        private void sheet_Paint(object sender, PaintEventArgs e)
        {
            // Создайте объект Graphics для рисования на PictureBox
            Graphics g = e.Graphics;
          
            // Рисование каждой вершины из коллекции vertices
            foreach (Vertex vertex in vertices)
            {

                float textWidth = g.MeasureString(vertex.Number.ToString(), Font).Width;
                float textHeight = g.MeasureString(vertex.Number.ToString(), Font).Height;

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

                g.DrawString(vertex.Number.ToString(), Font, Brushes.Black, textX, textY);
            }

        }
       
    }
}
