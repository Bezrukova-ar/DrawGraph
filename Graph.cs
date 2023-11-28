using Microsoft.VisualBasic;
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
        public List<string> cyclesList = new List<string>(); // Коллекция для хранения циклов
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
        // Обход в ширину для поиска элементарных цепей
        public List<List<int>> FindElementaryPathsBFS(int startVertex, int endVertex)
        {
            List<List<int>> paths = new List<List<int>>();
            Queue<List<int>> queue = new Queue<List<int>>();

            queue.Enqueue(new List<int> { startVertex });

            while (queue.Count > 0)
            {
                List<int> path = queue.Dequeue();
                int currentVertex = path[path.Count - 1];

                if (currentVertex == endVertex)
                {
                    paths.Add(path);
                    continue;
                }

                foreach (Edge edge in edges)
                {
                    if (edge.StartVertex == currentVertex && !path.Contains(edge.EndVertex))
                    {
                        List<int> newPath = new List<int>(path);
                        newPath.Add(edge.EndVertex);
                        queue.Enqueue(newPath);
                    }
                }
            }
            return paths;
        }
        //Поиск элементарных циклов
        public void DFScycle(int u, int endV, int[] color, int unavailableEdge, List<int> cycle)
        {
            if (u != endV)
                color[u] = 2;
            else
            {
                if (cycle.Count >= 2)
                {
                    cycle.Reverse();
                    string s = cycle[0].ToString();
                    for (int i = 1; i < cycle.Count; i++)
                        s += "-" + cycle[i].ToString();

                    bool flag = false; // есть ли палиндром для этого цикла графа в массиве?
                    foreach (string storedCycle in cyclesList)
                        if (storedCycle == s)
                        {
                            flag = true;
                            break;
                        }

                    if (!flag)
                    {
                        cycle.Reverse();
                        s = cycle[0].ToString();
                        for (int i = 1; i < cycle.Count; i++)
                            s += "-" + cycle[i].ToString();
                        cyclesList.Add(s); // добавляем цикл в массив
                    }
                    return;
                }
            }

            foreach (var edge in edges)
            {
                int w = edges.IndexOf(edge);
                if (w == unavailableEdge)
                    continue;

                if (color[edge.EndVertex - 1] == 1 && edge.StartVertex - 1 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(edge.EndVertex);
                    DFScycle(edge.EndVertex - 1, endV, color, w, cycleNEW);
                    color[edge.EndVertex - 1] = 1;
                }
                else if (color[edge.StartVertex - 1] == 1 && edge.EndVertex - 1 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(edge.StartVertex);
                    DFScycle(edge.StartVertex - 1, endV, color, w, cycleNEW);
                    color[edge.StartVertex - 1] = 1;
                }
            }
        }

        //Метод вычисления матрицы смежности вершин
        public int[,] GetAdjacencyMatrixVertex()
        {
            // Получаем количество вершин
            int vertexCount = vertices.Count;
            // Создаем двумерный массив для матрицы смежности
            int[,] adjacencyMatrix = new int[vertexCount, vertexCount];
            // Заполняем матрицу смежности
            foreach (Graph.Edge edge in edges)
            {
                // Устанавливаем связь между начальной и конечной вершинами
                adjacencyMatrix[edge.StartVertex - 1, edge.EndVertex - 1] = 1;
                adjacencyMatrix[edge.EndVertex - 1, edge.StartVertex - 1] = 1; // Для неориентированного графа
            }
            return adjacencyMatrix;

        }
        //Метод вычисления матрицы весов
        public int[,] GetAdjacencyMatrixWeight()
        {
            // Получаем количество вершин
            int vertexCount = vertices.Count;

            // Создаем двумерный массив для матрицы весов
            int[,] adjacencyMatrix = new int[vertexCount, vertexCount];

            // Заполняем матрицу весов
            foreach (Graph.Edge edge in edges)
            {
                // Вес ребра
                int weight = edge.Weight;

                // Устанавливаем связь между начальной и конечной вершинами
                adjacencyMatrix[edge.StartVertex - 1, edge.EndVertex - 1] = weight;
                adjacencyMatrix[edge.EndVertex - 1, edge.StartVertex - 1] = weight; // Для неориентированного графа
            }

            return adjacencyMatrix;
        }
        //Функция удаления верщины
        public void deleteVertex(Vertex vertex)
        {
            double x = vertex.Position.X;
            double y = vertex.Position.Y;
            Graph.Vertex vertexToRemove = vertices.Find(v => v.Position.X == x && v.Position.Y == y);
            //int maxVertexNumber = 0; //для определения максимального номера вершины в коллекции

            if (vertexToRemove != null)
            {
                int removedVertexNumber = vertexToRemove.Number;
               vertices.Remove(vertexToRemove);


                //Перенумерация вершин
                for (int i = 0; i < vertices.Count; i++)
                {
                    vertices[i].Number = i + 1;
                }

                edges.RemoveAll(e => e.StartVertex == vertexToRemove.Number || e.EndVertex == vertexToRemove.Number);
                //перенумерация
                foreach (var edge in edges)
                {
                    if (edge.StartVertex > removedVertexNumber)
                    {
                        edge.StartVertex -= 1;
                    }
                    if (edge.EndVertex > removedVertexNumber)
                    {
                        edge.EndVertex -= 1;
                    }
                }
            }

        }
        // Функция для получения степени вершины 
        public int GetVertexDegree(Vertex vertex)
        {
            int degree = 0;

            // количесвто вхождений как ак первая вершина
            degree += edges.Count(edge => edge.StartVertex == vertex.Number);

            // количесвто вхождений как вторая вершина
            degree += edges.Count(edge => edge.EndVertex == vertex.Number);

            return degree;
        }
        // Проверка того, что щелк внутри вершины
        public bool IsPointInCircle(Point point, Point center, int radius)
        {
            int distanceSquared = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y);
            return distanceSquared <= radius * radius;
        }

        //вывод информации если элемент - вершина
        public void ShowVertexInfo(Graph.Vertex vertex)
        {
            MessageBox.Show($"Номер вершины: {vertex.Number}\nСтепень вершины: {GetVertexDegree(vertex)}", "Информация об элементе");
        }

        //вывод информации если элемент - ребро
        public void ShowEdgeInfo(Graph.Edge edge)
        {
            MessageBox.Show($"Вес ребра: {edge.Weight}\nПервая вершина: {edge.StartVertex}\nВторая вершина: {edge.EndVertex}", "Информация об элементе");
        }

        //Функция удаления ребра
        public void deleteEdge(Graph.Edge edge)
        {
            edges.Remove(edge);
        }

        // Функция для определения типа элемента
        public object GetClickedElement(Point clickPosition)
        {
            // Проверка на то, что щелчок внутри вершины
            foreach (Graph.Vertex vertex in vertices)
            {

                if (IsPointInCircle(clickPosition, vertex.Position, 10))
                {
                    return vertex;
                }
            }
            // Проверьте, находится ли щелчок на каком-то ребре
            foreach (Graph.Edge edge in edges)
            {
                Graph.Vertex startVertex = vertices.Find(v => v.Number == edge.StartVertex);
                Graph.Vertex endVertex = vertices.Find(v => v.Number == edge.EndVertex);

                // Проверьте, лежит ли точка щелчка на отрезке ребра с погрешностью 5 пикселей
                if (IsPointNearLineSegment(clickPosition, startVertex.Position, endVertex.Position, 5))
                {
                    return edge;
                }
            }
            return null; //Не найден элемент
        }
        // Метод для проверки, лежит ли точка на отрезке
        public bool IsPointNearLineSegment(Point point, Point start, Point end, int tolerance)
        {
            double distance = PointToLineDistance(point, start, end);

            // Проверка, находится ли точка в пределах погрешности
            return distance <= tolerance;
        }

        // Метод для расчета расстояния от точки до линии, проходящей через start и end
        public double PointToLineDistance(Point point, Point start, Point end)
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

        // Метод для получения номера вершины по ее позиции на PictureBox
        public int GetVertexNumberByPosition(Point position)
        {
            foreach (Graph.Vertex vertex in vertices)
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
        // Метод для проверки, существует ли ребро между двумя вершинами
        public bool EdgeExists(int startVertex, int endVertex)
        {
            return edges.Any(edge =>
                (edge.StartVertex == startVertex && edge.EndVertex == endVertex) ||
                (edge.StartVertex == endVertex && edge.EndVertex == startVertex));
        }

        // Метод для получения веса ребра от пользователя
        public int GetWeightFromUserInput()
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



    }

}
