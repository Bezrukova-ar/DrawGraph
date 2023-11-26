
namespace DrawGraph
{
    partial class DrawGraphForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buildGraphButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteALLGraphRB = new System.Windows.Forms.RadioButton();
            this.deleteElementRB = new System.Windows.Forms.RadioButton();
            this.editingEdgeWeightRB = new System.Windows.Forms.RadioButton();
            this.drawEdgeRB = new System.Windows.Forms.RadioButton();
            this.drawVertexRB = new System.Windows.Forms.RadioButton();
            this.selectElementRB = new System.Windows.Forms.RadioButton();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveGrathBTN = new System.Windows.Forms.Button();
            this.buildingALLPathsBTN = new System.Windows.Forms.Button();
            this.weightMatrixCalculationBTN = new System.Windows.Forms.Button();
            this.weightMatrixLB = new System.Windows.Forms.ListBox();
            this.searchForElementaryCyclesBTN = new System.Windows.Forms.Button();
            this.vertexAdjacencyMatrixLB = new System.Windows.Forms.ListBox();
            this.calculationOfVertexAdjacencyMatrixBTN = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buildGraphButton
            // 
            this.buildGraphButton.BackColor = System.Drawing.Color.Thistle;
            this.buildGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buildGraphButton.Location = new System.Drawing.Point(379, 28);
            this.buildGraphButton.Margin = new System.Windows.Forms.Padding(5);
            this.buildGraphButton.Name = "buildGraphButton";
            this.buildGraphButton.Size = new System.Drawing.Size(280, 57);
            this.buildGraphButton.TabIndex = 0;
            this.buildGraphButton.Text = "Загрузить индивидуальное задание";
            this.buildGraphButton.UseVisualStyleBackColor = false;
            this.buildGraphButton.Click += new System.EventHandler(this.BuildGraphButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.03831F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.96169F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1044, 681);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.GhostWhite;
            this.groupBox1.Controls.Add(this.deleteALLGraphRB);
            this.groupBox1.Controls.Add(this.deleteElementRB);
            this.groupBox1.Controls.Add(this.editingEdgeWeightRB);
            this.groupBox1.Controls.Add(this.drawEdgeRB);
            this.groupBox1.Controls.Add(this.drawVertexRB);
            this.groupBox1.Controls.Add(this.selectElementRB);
            this.groupBox1.Controls.Add(this.buildGraphButton);
            this.groupBox1.Controls.Add(this.sheet);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(668, 671);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Команды для работы с графом";
            // 
            // deleteALLGraphRB
            // 
            this.deleteALLGraphRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.deleteALLGraphRB.AutoSize = true;
            this.deleteALLGraphRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.deleteALLGraphRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteALLGraphRB.Image = global::DrawGraph.Properties.Resources.trashcan_delete_remove_trash_icon_178327;
            this.deleteALLGraphRB.Location = new System.Drawing.Point(315, 29);
            this.deleteALLGraphRB.Name = "deleteALLGraphRB";
            this.deleteALLGraphRB.Size = new System.Drawing.Size(56, 56);
            this.deleteALLGraphRB.TabIndex = 10;
            this.deleteALLGraphRB.TabStop = true;
            this.deleteALLGraphRB.UseVisualStyleBackColor = true;
            this.deleteALLGraphRB.Click += new System.EventHandler(this.deleteALLGraphRB_CheckedChanged);
            // 
            // deleteElementRB
            // 
            this.deleteElementRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.deleteElementRB.AutoSize = true;
            this.deleteElementRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.deleteElementRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteElementRB.Image = global::DrawGraph.Properties.Resources.close_circle_remove_delete_icon_149506;
            this.deleteElementRB.Location = new System.Drawing.Point(253, 29);
            this.deleteElementRB.Name = "deleteElementRB";
            this.deleteElementRB.Size = new System.Drawing.Size(56, 56);
            this.deleteElementRB.TabIndex = 9;
            this.deleteElementRB.TabStop = true;
            this.deleteElementRB.UseVisualStyleBackColor = true;
            // 
            // editingEdgeWeightRB
            // 
            this.editingEdgeWeightRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.editingEdgeWeightRB.AutoSize = true;
            this.editingEdgeWeightRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.editingEdgeWeightRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editingEdgeWeightRB.Image = global::DrawGraph.Properties.Resources.pencil_striped_symbol_for_interface_edit_buttons_icon_icons_com_56782;
            this.editingEdgeWeightRB.Location = new System.Drawing.Point(191, 29);
            this.editingEdgeWeightRB.Name = "editingEdgeWeightRB";
            this.editingEdgeWeightRB.Size = new System.Drawing.Size(56, 56);
            this.editingEdgeWeightRB.TabIndex = 8;
            this.editingEdgeWeightRB.TabStop = true;
            this.editingEdgeWeightRB.UseVisualStyleBackColor = true;
            // 
            // drawEdgeRB
            // 
            this.drawEdgeRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.drawEdgeRB.AutoSize = true;
            this.drawEdgeRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.drawEdgeRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawEdgeRB.Image = global::DrawGraph.Properties.Resources.line_icon_151229;
            this.drawEdgeRB.Location = new System.Drawing.Point(129, 29);
            this.drawEdgeRB.Name = "drawEdgeRB";
            this.drawEdgeRB.Size = new System.Drawing.Size(56, 56);
            this.drawEdgeRB.TabIndex = 7;
            this.drawEdgeRB.TabStop = true;
            this.drawEdgeRB.UseVisualStyleBackColor = true;
            // 
            // drawVertexRB
            // 
            this.drawVertexRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.drawVertexRB.AutoSize = true;
            this.drawVertexRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.drawVertexRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawVertexRB.Image = global::DrawGraph.Properties.Resources.circle_icon_211799;
            this.drawVertexRB.Location = new System.Drawing.Point(67, 29);
            this.drawVertexRB.Name = "drawVertexRB";
            this.drawVertexRB.Size = new System.Drawing.Size(56, 56);
            this.drawVertexRB.TabIndex = 6;
            this.drawVertexRB.TabStop = true;
            this.drawVertexRB.UseVisualStyleBackColor = true;
            // 
            // selectElementRB
            // 
            this.selectElementRB.Appearance = System.Windows.Forms.Appearance.Button;
            this.selectElementRB.AutoSize = true;
            this.selectElementRB.FlatAppearance.CheckedBackColor = System.Drawing.Color.Thistle;
            this.selectElementRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectElementRB.Image = global::DrawGraph.Properties.Resources.pointinghand_100160;
            this.selectElementRB.Location = new System.Drawing.Point(8, 29);
            this.selectElementRB.Name = "selectElementRB";
            this.selectElementRB.Size = new System.Drawing.Size(53, 56);
            this.selectElementRB.TabIndex = 5;
            this.selectElementRB.TabStop = true;
            this.selectElementRB.UseVisualStyleBackColor = true;
            // 
            // sheet
            // 
            this.sheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sheet.BackColor = System.Drawing.Color.White;
            this.sheet.Location = new System.Drawing.Point(0, 95);
            this.sheet.Margin = new System.Windows.Forms.Padding(5);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(668, 576);
            this.sheet.TabIndex = 4;
            this.sheet.TabStop = false;
            this.sheet.Paint += new System.Windows.Forms.PaintEventHandler(this.sheet_Paint);
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.saveGrathBTN);
            this.groupBox2.Controls.Add(this.buildingALLPathsBTN);
            this.groupBox2.Controls.Add(this.weightMatrixCalculationBTN);
            this.groupBox2.Controls.Add(this.weightMatrixLB);
            this.groupBox2.Controls.Add(this.searchForElementaryCyclesBTN);
            this.groupBox2.Controls.Add(this.vertexAdjacencyMatrixLB);
            this.groupBox2.Controls.Add(this.calculationOfVertexAdjacencyMatrixBTN);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(681, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 675);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вычисления:";
            // 
            // saveGrathBTN
            // 
            this.saveGrathBTN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.saveGrathBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveGrathBTN.Location = new System.Drawing.Point(9, 601);
            this.saveGrathBTN.Margin = new System.Windows.Forms.Padding(5);
            this.saveGrathBTN.Name = "saveGrathBTN";
            this.saveGrathBTN.Size = new System.Drawing.Size(343, 66);
            this.saveGrathBTN.TabIndex = 17;
            this.saveGrathBTN.Text = "Сохранить граф";
            this.saveGrathBTN.UseVisualStyleBackColor = false;
            // 
            // buildingALLPathsBTN
            // 
            this.buildingALLPathsBTN.BackColor = System.Drawing.Color.Thistle;
            this.buildingALLPathsBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buildingALLPathsBTN.Location = new System.Drawing.Point(8, 486);
            this.buildingALLPathsBTN.Margin = new System.Windows.Forms.Padding(5);
            this.buildingALLPathsBTN.Name = "buildingALLPathsBTN";
            this.buildingALLPathsBTN.Size = new System.Drawing.Size(343, 66);
            this.buildingALLPathsBTN.TabIndex = 16;
            this.buildingALLPathsBTN.Text = "Построение всех путей";
            this.buildingALLPathsBTN.UseVisualStyleBackColor = false;
            // 
            // weightMatrixCalculationBTN
            // 
            this.weightMatrixCalculationBTN.BackColor = System.Drawing.Color.Thistle;
            this.weightMatrixCalculationBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.weightMatrixCalculationBTN.Location = new System.Drawing.Point(3, 210);
            this.weightMatrixCalculationBTN.Margin = new System.Windows.Forms.Padding(5);
            this.weightMatrixCalculationBTN.Name = "weightMatrixCalculationBTN";
            this.weightMatrixCalculationBTN.Size = new System.Drawing.Size(102, 171);
            this.weightMatrixCalculationBTN.TabIndex = 15;
            this.weightMatrixCalculationBTN.Text = "Расчет матрицы весов";
            this.weightMatrixCalculationBTN.UseVisualStyleBackColor = false;
            this.weightMatrixCalculationBTN.Click += new System.EventHandler(this.weightMatrixCalculationBTN_Click);
            // 
            // weightMatrixLB
            // 
            this.weightMatrixLB.FormattingEnabled = true;
            this.weightMatrixLB.ItemHeight = 21;
            this.weightMatrixLB.Location = new System.Drawing.Point(113, 210);
            this.weightMatrixLB.Name = "weightMatrixLB";
            this.weightMatrixLB.Size = new System.Drawing.Size(247, 172);
            this.weightMatrixLB.TabIndex = 14;
            // 
            // searchForElementaryCyclesBTN
            // 
            this.searchForElementaryCyclesBTN.BackColor = System.Drawing.Color.Thistle;
            this.searchForElementaryCyclesBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.searchForElementaryCyclesBTN.Location = new System.Drawing.Point(9, 410);
            this.searchForElementaryCyclesBTN.Margin = new System.Windows.Forms.Padding(5);
            this.searchForElementaryCyclesBTN.Name = "searchForElementaryCyclesBTN";
            this.searchForElementaryCyclesBTN.Size = new System.Drawing.Size(343, 66);
            this.searchForElementaryCyclesBTN.TabIndex = 13;
            this.searchForElementaryCyclesBTN.Text = "Поиск элементарных циклов";
            this.searchForElementaryCyclesBTN.UseVisualStyleBackColor = false;
            this.searchForElementaryCyclesBTN.Click += new System.EventHandler(this.searchForElementaryCyclesBTN_Click);
            // 
            // vertexAdjacencyMatrixLB
            // 
            this.vertexAdjacencyMatrixLB.FormattingEnabled = true;
            this.vertexAdjacencyMatrixLB.ItemHeight = 21;
            this.vertexAdjacencyMatrixLB.Location = new System.Drawing.Point(113, 28);
            this.vertexAdjacencyMatrixLB.Name = "vertexAdjacencyMatrixLB";
            this.vertexAdjacencyMatrixLB.Size = new System.Drawing.Size(247, 172);
            this.vertexAdjacencyMatrixLB.TabIndex = 12;
            // 
            // calculationOfVertexAdjacencyMatrixBTN
            // 
            this.calculationOfVertexAdjacencyMatrixBTN.BackColor = System.Drawing.Color.Thistle;
            this.calculationOfVertexAdjacencyMatrixBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calculationOfVertexAdjacencyMatrixBTN.Location = new System.Drawing.Point(2, 29);
            this.calculationOfVertexAdjacencyMatrixBTN.Margin = new System.Windows.Forms.Padding(5);
            this.calculationOfVertexAdjacencyMatrixBTN.Name = "calculationOfVertexAdjacencyMatrixBTN";
            this.calculationOfVertexAdjacencyMatrixBTN.Size = new System.Drawing.Size(103, 171);
            this.calculationOfVertexAdjacencyMatrixBTN.TabIndex = 11;
            this.calculationOfVertexAdjacencyMatrixBTN.Text = "Расчет матрицы смежности вершин";
            this.calculationOfVertexAdjacencyMatrixBTN.UseVisualStyleBackColor = false;
            this.calculationOfVertexAdjacencyMatrixBTN.Click += new System.EventHandler(this.calculationOfVertexAdjacencyMatrixBTN_Click);
            // 
            // DrawGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1044, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1060, 720);
            this.MinimumSize = new System.Drawing.Size(1060, 720);
            this.Name = "DrawGraphForm";
            this.Text = "Графопостроитель";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buildGraphButton;
        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton deleteALLGraphRB;
        private System.Windows.Forms.RadioButton deleteElementRB;
        private System.Windows.Forms.RadioButton editingEdgeWeightRB;
        private System.Windows.Forms.RadioButton drawEdgeRB;
        private System.Windows.Forms.RadioButton drawVertexRB;
        private System.Windows.Forms.RadioButton selectElementRB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox vertexAdjacencyMatrixLB;
        private System.Windows.Forms.Button calculationOfVertexAdjacencyMatrixBTN;
        private System.Windows.Forms.Button saveGrathBTN;
        private System.Windows.Forms.Button buildingALLPathsBTN;
        private System.Windows.Forms.Button weightMatrixCalculationBTN;
        private System.Windows.Forms.ListBox weightMatrixLB;
        private System.Windows.Forms.Button searchForElementaryCyclesBTN;
    }
}

