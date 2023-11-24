﻿using Microsoft.VisualBasic;
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
        int selectedStartVertex = -1; //Выбор вершины для рисования ребер
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
                int selectedVertexNumber = GetVertexNumberByPosition(e.Location); // Получение номера вершины по ее позиции

                if (selectedVertexNumber != -1)
                {
                    if (selectedStartVertex == -1)
                    {
                        // Если еще нет выбранной вершины, то выберем ее
                        selectedStartVertex = selectedVertexNumber;
                    }
                    else
                    {
                        // Если уже есть выбранная вершина, то выберем вторую вершину
                        int selectedEndVertex = selectedVertexNumber;

                        // Проверка, что между выбранными вершинами еще нет ребра
                        if (!EdgeExists(selectedStartVertex, selectedEndVertex))
                        {
                            if (selectedStartVertex == selectedEndVertex)
                            {
                                MessageBox.Show("Программа не поддерживает рисование петлей, выберите пару разных ребер");
                            }
                            else 
                            {
                                // Вывод окошка для ввода веса ребра
                                int weight = GetWeightFromUserInput();

                                // Создание нового ребра и добавление его в коллекцию ребер
                                Edge newEdge = new Edge(selectedStartVertex, selectedEndVertex, weight);
                                edges.Add(newEdge);
    
                                // Сброс выбранных вершин
                                selectedStartVertex = -1;
    
                                // Перерисовка PictureBox 
                                sheet.Invalidate(); 
                            }
                            
                        }
                        else
                        {
                            // Обработка случая, когда между вершинами уже есть ребро
                            MessageBox.Show("Ребро между выбранными вершинами уже существует.");

                            // Сброс выбранной вершины, чтобы выбрать новую пару
                            selectedStartVertex = -1;
                        }
                    }
                }
            }
            if(editingEdgeWeightRB.Checked)//Редактирование веса ребра
            {
                // Получить координаты щелчка
                Point clickPoint = sheet.PointToClient(Cursor.Position);

                // Проверьте, находится ли щелчок на каком-то ребре
                foreach (Edge edge in edges)
                {
                    Vertex startVertex = vertices.Find(v => v.Number == edge.StartVertex);
                    Vertex endVertex = vertices.Find(v => v.Number == edge.EndVertex);

                    // Проверьте, лежит ли точка щелчка на отрезке ребра с погрешностью 5 пикселей
                    if (IsPointNearLineSegment(clickPoint, startVertex.Position, endVertex.Position, 5))
                    {
                        // При щелчке по ребру, откройте диалоговое окно для ввода нового веса
                        int newWeight = GetWeightFromUserInput();

                        // Обновите вес ребра в коллекции
                        edge.Weight = newWeight;

                        //Перерисовки PictureBox с обновленными данными
                        sheet.Invalidate();

                        break;
                    }
                }
            }
            if (selectElementRB.Checked) //Вывод информации об элементе
            {
                // Получить позицию щелчка мыши
                Point mouseClick = sheet.PointToClient(Cursor.Position);

                // Определите тип элемента (вершина или ребро) 
                object clickedElement = GetClickedElement(mouseClick);

                // Отображение информации о выбранном элементе 
                if (clickedElement is Vertex)
                {
                    Vertex clickedVertex = (Vertex)clickedElement;
                    ShowVertexInfo(clickedVertex);
                }
                else if (clickedElement is Edge)
                {
                    Edge clickedEdge = (Edge)clickedElement;
                    ShowEdgeInfo(clickedEdge);
                }
            }
        }
        // Обработчик события отрисовки содержимого PictureBox
        private void sheet_Paint(object sender, PaintEventArgs e)
        {
            // Создайте объект Graphics для рисования на PictureBox
            Graphics g = e.Graphics;
            Font weightFont = new Font(Font.FontFamily, 12f);
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
        // Метод для проверки, существует ли ребро между двумя вершинами
        private bool EdgeExists(int startVertex, int endVertex)
        {
            return edges.Any(edge =>
                (edge.StartVertex == startVertex && edge.EndVertex == endVertex) ||
                (edge.StartVertex == endVertex && edge.EndVertex == startVertex));
        }

        // Метод для получения веса ребра от пользователя
        private int GetWeightFromUserInput()
        {
            int weight = 0;
            bool validInput = false;

            while (!validInput)
            {
                string input = Interaction.InputBox("Введите вес ребра (целое положительное число):", "Ввод веса ребра");
                // Проверка на отмену ввода
                if (input == "")
                {
                    MessageBox.Show("Ввод отменен. Попробуйте еще раз.");
                    continue;
                }
                // Попытка преобразовать введенное значение в целое число
                if (int.TryParse(input, out weight))
                {
                    // Проверка на положительное число
                    if (weight > 0)
                    {
                        validInput = true;
                    }
                    else
                    {
                        MessageBox.Show("Введите положительное число.");
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное целое число.");
                }
            }
            return weight;
        }

        // Метод для получения номера вершины по ее позиции на PictureBox
        private int GetVertexNumberByPosition(Point position)
        {
            foreach (Vertex vertex in vertices)
            {
                int vertexRadius = 10; 

                // Проверка, находится ли позиция в пределах вершины
                if (Math.Pow(position.X - vertex.Position.X, 2) + Math.Pow(position.Y - vertex.Position.Y, 2) <= Math.Pow(vertexRadius, 2))
                {
                    return vertex.Number;
                }
            }
            // Если не найдено ни одной вершины в указанной позиции
            return -1;
        }

        //Когда нажата кнопка удалить все
        private void deleteALLGraphRB_CheckedChanged(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите удалить граф полностью?", "Удаление графа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Проверяем результат нажатия кнопки
            if (result == DialogResult.Yes)
            {
                vertices.Clear();
                edges.Clear();
                sheet.Invalidate();
            }
        }
        // Метод для проверки, лежит ли точка на отрезке
        private bool IsPointNearLineSegment(Point point, Point start, Point end, int tolerance)
        {
            double distance = PointToLineDistance(point, start, end);

            // Проверка, находится ли точка в пределах погрешности
            return distance <= tolerance;
        }

        // Метод для расчета расстояния от точки до линии, проходящей через start и end
        private double PointToLineDistance(Point point, Point start, Point end)
        {
            double a = point.X - start.X;
            double b = point.Y - start.Y;
            double c = end.X - start.X;
            double d = end.Y - start.Y;

            double dotProduct = a * c + b * d;
            double lengthSquared = c * c + d * d;

            double param = dotProduct / lengthSquared;

            double closestX, closestY;

            if (param < 0 || (start.X == end.X && start.Y == end.Y))
            {
                closestX = start.X;
                closestY = start.Y;
            }
            else if (param > 1)
            {
                closestX = end.X;
                closestY = end.Y;
            }
            else
            {
                closestX = start.X + param * c;
                closestY = start.Y + param * d;
            }

            double dx = point.X - closestX;
            double dy = point.Y - closestY;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        // Функция для определения типа элемента
        private object GetClickedElement(Point clickPosition)
        {
            // Проверка на то, что щелчок внутри вершины
            foreach (Vertex vertex in vertices)
            {
                
                if (IsPointInCircle(clickPosition, vertex.Position, 10))
                {
                    return vertex;
                }
            }
            // Проверьте, находится ли щелчок на каком-то ребре
            foreach (Edge edge in edges)
            {
                Vertex startVertex = vertices.Find(v => v.Number == edge.StartVertex);
                Vertex endVertex = vertices.Find(v => v.Number == edge.EndVertex);

                // Проверьте, лежит ли точка щелчка на отрезке ребра с погрешностью 5 пикселей
                if (IsPointNearLineSegment(clickPosition, startVertex.Position, endVertex.Position, 5))
                {
                    return edge;
                }
            }
            return null; //Не найден элемент
        }
        // Проверка того, что щелк внутри вершины
        private bool IsPointInCircle(Point point, Point center, int radius)
        {
            int distanceSquared = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y);
            return distanceSquared <= radius * radius;
        }

        //вывод информации если элемент - вершина
        private void ShowVertexInfo(Vertex vertex)
        {
            MessageBox.Show($"Номер вершины: {vertex.Number}\nСтепень вершины: {GetVertexDegree(vertex)}", "Информация об элементе");
        }
        //вывод информации если элемент - ребро
        private void ShowEdgeInfo(Edge edge)
        {
            MessageBox.Show($"Вес ребра: {edge.Weight}\nПервая вершина: {edge.StartVertex}\nВторая вершина: {edge.EndVertex}", "Информация об элементе");
        }
        // Функция для получения степени вершины 
        private int GetVertexDegree(Vertex vertex)
        {
            int degree = 0;

            // количесвто вхождений как ак первая вершина
            degree += edges.Count(edge => edge.StartVertex == vertex.Number);

            // количесвто вхождений как вторая вершина
            degree += edges.Count(edge => edge.EndVertex == vertex.Number);

            return degree;
        }
    }
}
