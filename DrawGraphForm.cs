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
            graph.DrawGraph(sheet);

            IndividualTaskForm individualTaskForm = new IndividualTaskForm();
            individualTaskForm.Show();
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
                int selectedVertexNumber = graph.GetVertexNumberByPosition(e.Location); // Получение номера вершины по ее позиции

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
                        if (!graph.EdgeExists(selectedStartVertex, selectedEndVertex))
                        {
                            if (selectedStartVertex == selectedEndVertex)
                            {
                                MessageBox.Show("Программа не поддерживает рисование петлей, выберите пару разных ребер");
                            }
                            else
                            {
                                // Вывод окошка для ввода веса ребра
                                int weight = graph.GetWeightFromUserInput();

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
                    if (graph.IsPointNearLineSegment(clickPoint, startVertex.Position, endVertex.Position, 5))
                    {
                        // При щелчке по ребру, откройте диалоговое окно для ввода нового веса
                        int newWeight = graph.GetWeightFromUserInput();

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
                object clickedElement = graph.GetClickedElement(mouseClick);

                // Отображение информации о выбранном элементе 
                if (clickedElement is Graph.Vertex)
                {
                    Graph.Vertex clickedVertex = (Graph.Vertex)clickedElement;
                    graph.ShowVertexInfo(clickedVertex);
                }
                else if (clickedElement is Graph.Edge)
                {
                    Graph.Edge clickedEdge = (Graph.Edge)clickedElement;
                    graph.ShowEdgeInfo(clickedEdge);
                }
            }
            if (deleteElementRB.Checked) //Удаление элемента
            {
                // Получить позицию щелчка мыши
                Point mouseClick = sheet.PointToClient(Cursor.Position);
                // Определите тип элемента (вершина или ребро) 
                object clickedElement = graph.GetClickedElement(mouseClick);
                // Удалить выбранный элемент
                if (clickedElement is Graph.Vertex)
                {
                    Graph.Vertex clickedVertex = (Graph.Vertex)clickedElement;
                    //Метод удаления вершины
                    graph.deleteVertex(clickedVertex);
                    sheet.Invalidate();
                }
                else if (clickedElement is Graph.Edge)
                {
                    Graph.Edge clickedEdge = (Graph.Edge)clickedElement;
                    //Метод удаления ребра
                    graph.deleteEdge(clickedEdge);
                    sheet.Invalidate();
                }
            }
        }
        // Обработчик события отрисовки содержимого PictureBox
        private void sheet_Paint(object sender, PaintEventArgs e)
        {
            // Создайте объект Graphics для рисования на PictureBox
            Graphics g = e.Graphics;
            Font weightFont = new Font("Arial", 12f);
            Font boldFont = new Font(weightFont, FontStyle.Bold);
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
                g.DrawString(edge.Weight.ToString(), boldFont, Brushes.DarkRed, weightPositionX, weightPositionY);
            }
           
            // Рисование каждой вершины из коллекции vertices
            foreach (Graph.Vertex vertex in graph.vertices)
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
        
        //Событие вычисления матрицы смежности
        private void calculationOfVertexAdjacencyMatrixBTN_Click(object sender, EventArgs e)
        {
            int[,] adjacencyMatrix = graph.GetAdjacencyMatrixVertex();
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
            int[,] adjacencyMatrix = graph.GetAdjacencyMatrixWeight();
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
            graph.cyclesList.Clear(); // очистка массива перед новым поиском
                                // 1-white 2-black
            int[] color = new int[graph.vertices.Count];
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                for (int k = 0; k < graph.vertices.Count; k++)
                    color[k] = 1;

                List<int> cycle = new List<int>();
                cycle.Add(i + 1);
                graph.DFScycle(i, i, color, -1, cycle);
            }
            if (graph.cyclesList.Count > 0)
            {
                string result = "Найденные элементарные циклы:\n";
                foreach (string cycleString in graph.cyclesList)
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
                        List<List<int>> bfsPaths = graph.FindElementaryPathsBFS(i, j);

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

