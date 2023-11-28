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
using System.Drawing.Imaging;

namespace DrawGraph
{
    public partial class DrawGraphForm : Form
    {

        public DrawGraphForm()
        {
            InitializeComponent();
        }

        int selectedStartVertex = -1; //Выбор вершины для рисования ребер
        private Graph graph = new Graph(); // экземпляр класса Graph для доступа к коллекциям через этот экземпляр
        private List<string> cyclesList = new List<string>(); // Коллекция для хранения циклов

        
        //Загрузка графа-индивидуального задания
        private void BuildGraphButton_Click(object sender, EventArgs e)
        {
            // Очистка коллекций перед построением
            graph.vertices.Clear();
            graph.edges.Clear();

            //Параметры моего графа
            //координаты вершин
            graph.vertices.Add(new Graph.Vertex(1, new Point(300, 45)));
            graph.vertices.Add(new Graph.Vertex(2, new Point(385, 90)));
            graph.vertices.Add(new Graph.Vertex(3, new Point(430, 175)));
            graph.vertices.Add(new Graph.Vertex(4, new Point(385, 260)));
            graph.vertices.Add(new Graph.Vertex(5, new Point(300, 305)));
            graph.vertices.Add(new Graph.Vertex(6, new Point(215, 260)));
            graph.vertices.Add(new Graph.Vertex(7, new Point(170, 175)));
            graph.vertices.Add(new Graph.Vertex(8, new Point(215, 90)));

            //какие вершины соединяют ребра                
            graph.edges.Add(new Graph.Edge(1, 2, 2));
            graph.edges.Add(new Graph.Edge(1, 8, 5));
            graph.edges.Add(new Graph.Edge(2, 8, 3));
            graph.edges.Add(new Graph.Edge(2, 6, 1));
            graph.edges.Add(new Graph.Edge(2, 4, 1));
            graph.edges.Add(new Graph.Edge(3, 7, 7));
            graph.edges.Add(new Graph.Edge(3, 4, 5));
            graph.edges.Add(new Graph.Edge(4, 8, 2));
            graph.edges.Add(new Graph.Edge(4, 5, 1));
            graph.edges.Add(new Graph.Edge(5, 6, 2));

            // Построение графа
            DrawGraph();

            IndividualTaskForm individualTaskForm = new IndividualTaskForm();
            individualTaskForm.Show();
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
                foreach (var edge in graph.edges)
                {

                    Point start = graph.vertices.Find(v => v.Number == edge.StartVertex).Position;
                    Point end = graph.vertices.Find(v => v.Number == edge.EndVertex).Position;

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
                foreach (var vertex in graph.vertices)
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
                Graph.Vertex newVertex = new Graph.Vertex(graph.vertices.Count + 1, clickPosition);

                // Добавление вершины в коллекцию vertices
                graph.vertices.Add(newVertex);

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
                                Graph.Edge newEdge = new Graph.Edge(selectedStartVertex, selectedEndVertex, weight);
                                graph.edges.Add(newEdge);

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
            if (editingEdgeWeightRB.Checked)//Редактирование веса ребра
            {
                // Получить координаты щелчка
                Point clickPoint = sheet.PointToClient(Cursor.Position);

                // Проверьте, находится ли щелчок на каком-то ребре
                foreach (Graph.Edge edge in graph.edges)
                {
                    Graph.Vertex startVertex = graph.vertices.Find(v => v.Number == edge.StartVertex);
                    Graph.Vertex endVertex = graph.vertices.Find(v => v.Number == edge.EndVertex);

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
                if (clickedElement is Graph.Vertex)
                {
                    Graph.Vertex clickedVertex = (Graph.Vertex)clickedElement;
                    ShowVertexInfo(clickedVertex);
                }
                else if (clickedElement is Graph.Edge)
                {
                    Graph.Edge clickedEdge = (Graph.Edge)clickedElement;
                    ShowEdgeInfo(clickedEdge);
                }
            }
            if (deleteElementRB.Checked) //Удаление элемента
            {
                // Получить позицию щелчка мыши
                Point mouseClick = sheet.PointToClient(Cursor.Position);
                // Определите тип элемента (вершина или ребро) 
                object clickedElement = GetClickedElement(mouseClick);
                // Удалить выбранный элемент
                if (clickedElement is Graph.Vertex)
                {
                    Graph.Vertex clickedVertex = (Graph.Vertex)clickedElement;
                    //Метод удаления вершины
                    deleteVertex(clickedVertex);
                    sheet.Invalidate();
                }
                else if (clickedElement is Graph.Edge)
                {
                    Graph.Edge clickedEdge = (Graph.Edge)clickedElement;
                    //Метод удаления ребра
                    deleteEdge(clickedEdge);
                    sheet.Invalidate();
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
            foreach (var edge in graph.edges)
            {

                Point start = graph.vertices.Find(v => v.Number == edge.StartVertex).Position;
                Point end = graph.vertices.Find(v => v.Number == edge.EndVertex).Position;

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
            foreach (Graph.Vertex vertex in graph.vertices)
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
            return graph.edges.Any(edge =>
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
            foreach (Graph.Vertex vertex in graph.vertices)
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
                graph.vertices.Clear();
                graph.edges.Clear();
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
            foreach (Graph.Vertex vertex in graph.vertices)
            {

                if (IsPointInCircle(clickPosition, vertex.Position, 10))
                {
                    return vertex;
                }
            }
            // Проверьте, находится ли щелчок на каком-то ребре
            foreach (Graph.Edge edge in graph.edges)
            {
                Graph.Vertex startVertex = graph.vertices.Find(v => v.Number == edge.StartVertex);
                Graph.Vertex endVertex = graph.vertices.Find(v => v.Number == edge.EndVertex);

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
        private void ShowVertexInfo(Graph.Vertex vertex)
        {
            MessageBox.Show($"Номер вершины: {vertex.Number}\nСтепень вершины: {GetVertexDegree(vertex)}", "Информация об элементе");
        }

        //вывод информации если элемент - ребро
        private void ShowEdgeInfo(Graph.Edge edge)
        {
            MessageBox.Show($"Вес ребра: {edge.Weight}\nПервая вершина: {edge.StartVertex}\nВторая вершина: {edge.EndVertex}", "Информация об элементе");
        }

        // Функция для получения степени вершины 
        private int GetVertexDegree(Graph.Vertex vertex)
        {
            int degree = 0;

            // количесвто вхождений как ак первая вершина
            degree += graph.edges.Count(edge => edge.StartVertex == vertex.Number);

            // количесвто вхождений как вторая вершина
            degree += graph.edges.Count(edge => edge.EndVertex == vertex.Number);

            return degree;
        }

        //Функция удаления ребра
        private void deleteEdge(Graph.Edge edge)
        {
            graph.edges.Remove(edge);
        }

        //Функция удаления верщины
        private void deleteVertex(Graph.Vertex vertex)
        {
            double x = vertex.Position.X;
            double y = vertex.Position.Y;
            Graph.Vertex vertexToRemove = graph.vertices.Find(v => v.Position.X == x && v.Position.Y == y);
            //int maxVertexNumber = 0; //для определения максимального номера вершины в коллекции

            if (vertexToRemove != null)
            {
                int removedVertexNumber = vertexToRemove.Number;
                graph.vertices.Remove(vertexToRemove);


                //Перенумерация вершин
                for (int i = 0; i < graph.vertices.Count; i++)
                {
                    graph.vertices[i].Number = i + 1;
                }

                graph.edges.RemoveAll(e => e.StartVertex == vertexToRemove.Number || e.EndVertex == vertexToRemove.Number);
                //перенумерация
                foreach (var edge in graph.edges)
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

        //Событие вычисления матрицы смежности
        private void calculationOfVertexAdjacencyMatrixBTN_Click(object sender, EventArgs e)
        {
            int[,] adjacencyMatrix = GetAdjacencyMatrixVertex();
            vertexAdjacencyMatrixLB.Items.Clear();
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                string row = "";

                for (int j = 0; j < graph.vertices.Count; j++)
                {
                    row += $"{adjacencyMatrix[i, j],6}";
                }

                vertexAdjacencyMatrixLB.Items.Add(row);
            }
        }

        //Событие вычисление матрицы весов
        private void weightMatrixCalculationBTN_Click(object sender, EventArgs e)
        {
            int[,] adjacencyMatrix = GetAdjacencyMatrixWeight();
            weightMatrixLB.Items.Clear();
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                string row = ""; 

                for (int j = 0; j < graph.vertices.Count; j++)
                {
                    row += $"{adjacencyMatrix[i, j],6}";
                }

                weightMatrixLB.Items.Add(row);
            }
        }

        //Событие поиска элементарных циклов
        private void searchForElementaryCyclesBTN_Click(object sender, EventArgs e)
        {
            cyclesList.Clear(); // очистка массива перед новым поиском
                                // 1-white 2-black
            int[] color = new int[graph.vertices.Count];
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                for (int k = 0; k < graph.vertices.Count; k++)
                    color[k] = 1;

                List<int> cycle = new List<int>();
                cycle.Add(i + 1);
                DFScycle(i, i, color, -1, cycle);
            }
            if (cyclesList.Count > 0)
            {
                string result = "Найденные элементарные циклы:\n";
                foreach (string cycleString in cyclesList)
                {
                    result += cycleString + "\n";
                }
                MessageBox.Show(result);
            }
            else
            {
                MessageBox.Show("Элементарные циклы не найдены.");
            }
        }

        //Событие поиска всех путей
        private void buildingALLPathsBTN_Click(object sender, EventArgs e)
        {
            string allPaths = "";
            for (int i = 1; i <= graph.vertices.Count; i++)
            {
                for (int j = 1; j <= graph.vertices.Count; j++)
                {
                    if (i != j)
                    {
                        List<List<int>> bfsPaths = FindElementaryPathsBFS(i, j);

                        // Добавляем результаты в общую строку
                        string result="";
                        foreach (List<int> path in bfsPaths)
                        {
                            result += string.Join(" -> ", path) + "\n";
                        }

                        allPaths += result;
                    }
                }
            }
            MessageBox.Show(allPaths, "Все элементарные цепи");
        }

        //Метод вычисления матрицы весов
        public int[,] GetAdjacencyMatrixWeight()
        {
            // Получаем количество вершин
            int vertexCount = graph.vertices.Count;

            // Создаем двумерный массив для матрицы весов
            int[,] adjacencyMatrix = new int[vertexCount, vertexCount];

            // Заполняем матрицу весов
            foreach (Graph.Edge edge in graph.edges)
            {
                // Вес ребра
                int weight = edge.Weight;

                // Устанавливаем связь между начальной и конечной вершинами
                adjacencyMatrix[edge.StartVertex - 1, edge.EndVertex - 1] = weight;
                adjacencyMatrix[edge.EndVertex - 1, edge.StartVertex - 1] = weight; // Для неориентированного графа
            }

            return adjacencyMatrix;
        }

        //Метод вычисления матрицы смежности вершин
        public int[,] GetAdjacencyMatrixVertex()
        {
            // Получаем количество вершин
            int vertexCount = graph.vertices.Count;
            // Создаем двумерный массив для матрицы смежности
            int[,] adjacencyMatrix = new int[vertexCount, vertexCount];
            // Заполняем матрицу смежности
            foreach (Graph.Edge edge in graph.edges)
            {
                // Устанавливаем связь между начальной и конечной вершинами
                adjacencyMatrix[edge.StartVertex - 1, edge.EndVertex - 1] = 1;
                adjacencyMatrix[edge.EndVertex - 1, edge.StartVertex - 1] = 1; // Для неориентированного графа
            }
            return adjacencyMatrix;

        }

        //Поиск элементарных циклов
        private void DFScycle(int u, int endV, int[] color, int unavailableEdge, List<int> cycle)
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

            foreach (var edge in graph.edges)
            {
                int w = graph.edges.IndexOf(edge);
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

                foreach (Graph.Edge edge in graph.edges)
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

        //Событие сохранения графа
        private void saveGrathBTN_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {

                Bitmap bitmap = new Bitmap(sheet.Width, sheet.Height);
                sheet.DrawToBitmap(bitmap, sheet.Bounds);

                switch (saveDialog.FilterIndex)
                {
                    case 1:
                        bitmap.Save(saveDialog.FileName, ImageFormat.Jpeg);
                        break;
                    case 2:
                        bitmap.Save(saveDialog.FileName, ImageFormat.Png);
                        break;
                    case 3:
                        bitmap.Save(saveDialog.FileName, ImageFormat.Bmp);
                        break;
                }
            }
        }
    }
}

