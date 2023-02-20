
namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class ViewColumnsEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabViewColumns = new System.Windows.Forms.TabControl();
            this.tabViewEditor = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbEntityType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchUrl = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkEnableCartable = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIsHierarchy = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFetchUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tvViewModels = new System.Windows.Forms.TreeView();
            this.tabColumnEditor = new System.Windows.Forms.TabPage();
            this.grpColProperties = new System.Windows.Forms.GroupBox();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbVisibility = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkAllowFiltering = new System.Windows.Forms.CheckBox();
            this.chkIsNullable = new System.Windows.Forms.CheckBox();
            this.chkAllowSorting = new System.Windows.Forms.CheckBox();
            this.chkIsFixedLength = new System.Windows.Forms.CheckBox();
            this.spnMinLength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.spnLength = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbScriptType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbDotNetType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lbxColumns = new System.Windows.Forms.CheckedListBox();
            this.tabViewColumns.SuspendLayout();
            this.tabViewEditor.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.tabColumnEditor.SuspendLayout();
            this.grpColProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnLength)).BeginInit();
            this.SuspendLayout();
            // 
            // tabViewColumns
            // 
            this.tabViewColumns.Controls.Add(this.tabViewEditor);
            this.tabViewColumns.Controls.Add(this.tabColumnEditor);
            this.tabViewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabViewColumns.Location = new System.Drawing.Point(0, 0);
            this.tabViewColumns.Name = "tabViewColumns";
            this.tabViewColumns.SelectedIndex = 0;
            this.tabViewColumns.Size = new System.Drawing.Size(810, 666);
            this.tabViewColumns.TabIndex = 0;
            this.tabViewColumns.SelectedIndexChanged += new System.EventHandler(this.ViewColumns_SelectedIndexChanged);
            // 
            // tabViewEditor
            // 
            this.tabViewEditor.Controls.Add(this.btnCancel);
            this.tabViewEditor.Controls.Add(this.btnGenerate);
            this.tabViewEditor.Controls.Add(this.grpProperties);
            this.tabViewEditor.Controls.Add(this.label1);
            this.tabViewEditor.Controls.Add(this.tvViewModels);
            this.tabViewEditor.Location = new System.Drawing.Point(4, 29);
            this.tabViewEditor.Name = "tabViewEditor";
            this.tabViewEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewEditor.Size = new System.Drawing.Size(802, 633);
            this.tabViewEditor.TabIndex = 0;
            this.tabViewEditor.Text = "View";
            this.tabViewEditor.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(695, 592);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 34);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(13, 592);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(149, 34);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Generate Scripts";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.txtEntityName);
            this.grpProperties.Controls.Add(this.label17);
            this.grpProperties.Controls.Add(this.cmbEntityType);
            this.grpProperties.Controls.Add(this.label5);
            this.grpProperties.Controls.Add(this.label4);
            this.grpProperties.Controls.Add(this.txtSearchUrl);
            this.grpProperties.Controls.Add(this.txtName);
            this.grpProperties.Controls.Add(this.chkEnableCartable);
            this.grpProperties.Controls.Add(this.label3);
            this.grpProperties.Controls.Add(this.chkIsHierarchy);
            this.grpProperties.Controls.Add(this.label2);
            this.grpProperties.Controls.Add(this.txtFetchUrl);
            this.grpProperties.Location = new System.Drawing.Point(422, 37);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(367, 537);
            this.grpProperties.TabIndex = 9;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "View Properties";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtEntityName.Location = new System.Drawing.Point(14, 222);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(341, 27);
            this.txtEntityName.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 199);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 20);
            this.label17.TabIndex = 26;
            this.label17.Text = "Entity Name :";
            // 
            // cmbEntityType
            // 
            this.cmbEntityType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityType.FormattingEnabled = true;
            this.cmbEntityType.Items.AddRange(new object[] {
            "(not set)",
            "Core",
            "Fiscal",
            "Base",
            "Operational"});
            this.cmbEntityType.Location = new System.Drawing.Point(15, 423);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(159, 28);
            this.cmbEntityType.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 398);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Entity type :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Search URL :";
            // 
            // txtSearchUrl
            // 
            this.txtSearchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchUrl.Location = new System.Drawing.Point(14, 353);
            this.txtSearchUrl.Name = "txtSearchUrl";
            this.txtSearchUrl.Size = new System.Drawing.Size(341, 27);
            this.txtSearchUrl.TabIndex = 19;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtName.Location = new System.Drawing.Point(14, 156);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(341, 27);
            this.txtName.TabIndex = 15;
            // 
            // chkEnableCartable
            // 
            this.chkEnableCartable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnableCartable.AutoSize = true;
            this.chkEnableCartable.Location = new System.Drawing.Point(15, 500);
            this.chkEnableCartable.Name = "chkEnableCartable";
            this.chkEnableCartable.Size = new System.Drawing.Size(162, 24);
            this.chkEnableCartable.TabIndex = 23;
            this.chkEnableCartable.Text = "Cartable-integrated";
            this.chkEnableCartable.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Name :";
            // 
            // chkIsHierarchy
            // 
            this.chkIsHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIsHierarchy.AutoSize = true;
            this.chkIsHierarchy.Location = new System.Drawing.Point(15, 467);
            this.chkIsHierarchy.Name = "chkIsHierarchy";
            this.chkIsHierarchy.Size = new System.Drawing.Size(110, 24);
            this.chkIsHierarchy.TabIndex = 22;
            this.chkIsHierarchy.Text = "Hierarchical";
            this.chkIsHierarchy.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Fetch URL :";
            // 
            // txtFetchUrl
            // 
            this.txtFetchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFetchUrl.Location = new System.Drawing.Point(14, 288);
            this.txtFetchUrl.Name = "txtFetchUrl";
            this.txtFetchUrl.Size = new System.Drawing.Size(341, 27);
            this.txtFetchUrl.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "All view models :";
            // 
            // tvViewModels
            // 
            this.tvViewModels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvViewModels.HideSelection = false;
            this.tvViewModels.Indent = 15;
            this.tvViewModels.Location = new System.Drawing.Point(13, 46);
            this.tvViewModels.Name = "tvViewModels";
            this.tvViewModels.Size = new System.Drawing.Size(387, 528);
            this.tvViewModels.TabIndex = 7;
            this.tvViewModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ViewModels_AfterSelect);
            this.tvViewModels.DoubleClick += new System.EventHandler(this.ViewModels_DoubleClick);
            // 
            // tabColumnEditor
            // 
            this.tabColumnEditor.Controls.Add(this.grpColProperties);
            this.tabColumnEditor.Controls.Add(this.btnSelectAll);
            this.tabColumnEditor.Controls.Add(this.btnDeselectAll);
            this.tabColumnEditor.Controls.Add(this.btnMoveDown);
            this.tabColumnEditor.Controls.Add(this.btnMoveUp);
            this.tabColumnEditor.Controls.Add(this.label9);
            this.tabColumnEditor.Controls.Add(this.lbxColumns);
            this.tabColumnEditor.Location = new System.Drawing.Point(4, 29);
            this.tabColumnEditor.Name = "tabColumnEditor";
            this.tabColumnEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumnEditor.Size = new System.Drawing.Size(802, 633);
            this.tabColumnEditor.TabIndex = 1;
            this.tabColumnEditor.Text = "Columns";
            this.tabColumnEditor.UseVisualStyleBackColor = true;
            // 
            // grpColProperties
            // 
            this.grpColProperties.Controls.Add(this.txtExpression);
            this.grpColProperties.Controls.Add(this.label16);
            this.grpColProperties.Controls.Add(this.cmbVisibility);
            this.grpColProperties.Controls.Add(this.label11);
            this.grpColProperties.Controls.Add(this.txtGroupName);
            this.grpColProperties.Controls.Add(this.label10);
            this.grpColProperties.Controls.Add(this.chkAllowFiltering);
            this.grpColProperties.Controls.Add(this.chkIsNullable);
            this.grpColProperties.Controls.Add(this.chkAllowSorting);
            this.grpColProperties.Controls.Add(this.chkIsFixedLength);
            this.grpColProperties.Controls.Add(this.spnMinLength);
            this.grpColProperties.Controls.Add(this.label7);
            this.grpColProperties.Controls.Add(this.spnLength);
            this.grpColProperties.Controls.Add(this.label6);
            this.grpColProperties.Controls.Add(this.cmbScriptType);
            this.grpColProperties.Controls.Add(this.label8);
            this.grpColProperties.Controls.Add(this.cmbStorageType);
            this.grpColProperties.Controls.Add(this.label12);
            this.grpColProperties.Controls.Add(this.cmbDotNetType);
            this.grpColProperties.Controls.Add(this.label13);
            this.grpColProperties.Controls.Add(this.cmbType);
            this.grpColProperties.Controls.Add(this.label14);
            this.grpColProperties.Controls.Add(this.txtColumnName);
            this.grpColProperties.Controls.Add(this.label15);
            this.grpColProperties.Location = new System.Drawing.Point(315, 30);
            this.grpColProperties.Name = "grpColProperties";
            this.grpColProperties.Size = new System.Drawing.Size(472, 582);
            this.grpColProperties.TabIndex = 29;
            this.grpColProperties.TabStop = false;
            this.grpColProperties.Text = "Column Properties";
            // 
            // txtExpression
            // 
            this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression.Location = new System.Drawing.Point(14, 524);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(448, 47);
            this.txtExpression.TabIndex = 48;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 495);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 20);
            this.label16.TabIndex = 47;
            this.label16.Text = "Expression :";
            // 
            // cmbVisibility
            // 
            this.cmbVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisibility.FormattingEnabled = true;
            this.cmbVisibility.Items.AddRange(new object[] {
            "(not set)",
            "Visible",
            "AlwaysVisible",
            "Hidden",
            "AlwaysHidden"});
            this.cmbVisibility.Location = new System.Drawing.Point(14, 258);
            this.cmbVisibility.Name = "cmbVisibility";
            this.cmbVisibility.Size = new System.Drawing.Size(213, 28);
            this.cmbVisibility.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "Visibility :";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(14, 456);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(448, 27);
            this.txtGroupName.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 433);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 20);
            this.label10.TabIndex = 44;
            this.label10.Text = "GroupName :";
            // 
            // chkAllowFiltering
            // 
            this.chkAllowFiltering.AutoSize = true;
            this.chkAllowFiltering.Location = new System.Drawing.Point(140, 400);
            this.chkAllowFiltering.Name = "chkAllowFiltering";
            this.chkAllowFiltering.Size = new System.Drawing.Size(125, 24);
            this.chkAllowFiltering.TabIndex = 43;
            this.chkAllowFiltering.Text = "Allow filtering";
            this.chkAllowFiltering.UseVisualStyleBackColor = true;
            // 
            // chkIsNullable
            // 
            this.chkIsNullable.AutoSize = true;
            this.chkIsNullable.Location = new System.Drawing.Point(140, 367);
            this.chkIsNullable.Name = "chkIsNullable";
            this.chkIsNullable.Size = new System.Drawing.Size(87, 24);
            this.chkIsNullable.TabIndex = 42;
            this.chkIsNullable.Text = "Nullable";
            this.chkIsNullable.UseVisualStyleBackColor = true;
            // 
            // chkAllowSorting
            // 
            this.chkAllowSorting.AutoSize = true;
            this.chkAllowSorting.Location = new System.Drawing.Point(16, 400);
            this.chkAllowSorting.Name = "chkAllowSorting";
            this.chkAllowSorting.Size = new System.Drawing.Size(119, 24);
            this.chkAllowSorting.TabIndex = 41;
            this.chkAllowSorting.Text = "Allow sorting";
            this.chkAllowSorting.UseVisualStyleBackColor = true;
            // 
            // chkIsFixedLength
            // 
            this.chkIsFixedLength.AutoSize = true;
            this.chkIsFixedLength.Location = new System.Drawing.Point(16, 367);
            this.chkIsFixedLength.Name = "chkIsFixedLength";
            this.chkIsFixedLength.Size = new System.Drawing.Size(112, 24);
            this.chkIsFixedLength.TabIndex = 40;
            this.chkIsFixedLength.Text = "Fixed length";
            this.chkIsFixedLength.UseVisualStyleBackColor = true;
            // 
            // spnMinLength
            // 
            this.spnMinLength.Location = new System.Drawing.Point(130, 326);
            this.spnMinLength.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.spnMinLength.Name = "spnMinLength";
            this.spnMinLength.Size = new System.Drawing.Size(97, 27);
            this.spnMinLength.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(128, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 20);
            this.label7.TabIndex = 37;
            this.label7.Text = "Minimum length :";
            // 
            // spnLength
            // 
            this.spnLength.Location = new System.Drawing.Point(16, 326);
            this.spnLength.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.spnLength.Name = "spnLength";
            this.spnLength.Size = new System.Drawing.Size(97, 27);
            this.spnLength.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 35;
            this.label6.Text = "Length :";
            // 
            // cmbScriptType
            // 
            this.cmbScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScriptType.FormattingEnabled = true;
            this.cmbScriptType.Items.AddRange(new object[] {
            "number",
            "string",
            "boolean",
            "Date",
            "Object"});
            this.cmbScriptType.Location = new System.Drawing.Point(249, 190);
            this.cmbScriptType.Name = "cmbScriptType";
            this.cmbScriptType.Size = new System.Drawing.Size(213, 28);
            this.cmbScriptType.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(249, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 20);
            this.label8.TabIndex = 32;
            this.label8.Text = "Script type :";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Items.AddRange(new object[] {
            "nvarchar",
            "datetime",
            "date",
            "time",
            "bit",
            "tinyint",
            "smallint",
            "int",
            "bigint",
            "money",
            "float"});
            this.cmbStorageType.Location = new System.Drawing.Point(14, 190);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(213, 28);
            this.cmbStorageType.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 20);
            this.label12.TabIndex = 28;
            this.label12.Text = "SQL type :";
            // 
            // cmbDotNetType
            // 
            this.cmbDotNetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDotNetType.FormattingEnabled = true;
            this.cmbDotNetType.Items.AddRange(new object[] {
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.Single",
            "System.Double",
            "System.Decimal",
            "System.String",
            "System.DateTime",
            "System.Boolean",
            "System.Object"});
            this.cmbDotNetType.Location = new System.Drawing.Point(249, 122);
            this.cmbDotNetType.Name = "cmbDotNetType";
            this.cmbDotNetType.Size = new System.Drawing.Size(213, 28);
            this.cmbDotNetType.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(249, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 20);
            this.label13.TabIndex = 27;
            this.label13.Text = ".NET type :";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "(not set)",
            "Money",
            "Currency",
            "Default"});
            this.cmbType.Location = new System.Drawing.Point(14, 122);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(213, 28);
            this.cmbType.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 20);
            this.label14.TabIndex = 26;
            this.label14.Text = "Type :";
            // 
            // txtColumnName
            // 
            this.txtColumnName.Location = new System.Drawing.Point(14, 58);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(448, 27);
            this.txtColumnName.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 20);
            this.label15.TabIndex = 25;
            this.label15.Text = "Name :";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.BackgroundImage = global::SPPC.Tools.SystemDesigner.Properties.Resources.icnCheckAll;
            this.btnSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectAll.Location = new System.Drawing.Point(189, 555);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(53, 51);
            this.btnSelectAll.TabIndex = 27;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeselectAll.BackgroundImage = global::SPPC.Tools.SystemDesigner.Properties.Resources.icnUncheckAll;
            this.btnDeselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeselectAll.Location = new System.Drawing.Point(129, 555);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(53, 51);
            this.btnDeselectAll.TabIndex = 26;
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.DeselectAll_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveDown.BackgroundImage = global::SPPC.Tools.SystemDesigner.Properties.Resources.icnMoveDown;
            this.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveDown.Location = new System.Drawing.Point(69, 555);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(53, 51);
            this.btnMoveDown.TabIndex = 25;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.MoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveUp.BackgroundImage = global::SPPC.Tools.SystemDesigner.Properties.Resources.icnMoveUp;
            this.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveUp.Location = new System.Drawing.Point(9, 555);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(53, 51);
            this.btnMoveUp.TabIndex = 24;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.MoveUp_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 20);
            this.label9.TabIndex = 28;
            this.label9.Text = "Available columns :";
            // 
            // lbxColumns
            // 
            this.lbxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxColumns.FormattingEnabled = true;
            this.lbxColumns.Location = new System.Drawing.Point(11, 39);
            this.lbxColumns.Name = "lbxColumns";
            this.lbxColumns.Size = new System.Drawing.Size(284, 510);
            this.lbxColumns.TabIndex = 23;
            this.lbxColumns.SelectedIndexChanged += new System.EventHandler(this.Columns_SelectedIndexChanged);
            // 
            // ViewColumnsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 666);
            this.Controls.Add(this.tabViewColumns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewColumnsEditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add View with Columns";
            this.tabViewColumns.ResumeLayout(false);
            this.tabViewEditor.ResumeLayout(false);
            this.tabViewEditor.PerformLayout();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.tabColumnEditor.ResumeLayout(false);
            this.tabColumnEditor.PerformLayout();
            this.grpColProperties.ResumeLayout(false);
            this.grpColProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabViewColumns;
        private System.Windows.Forms.TabPage tabViewEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvViewModels;
        private System.Windows.Forms.TabPage tabColumnEditor;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.ComboBox cmbEntityType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchUrl;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkEnableCartable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIsHierarchy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFetchUrl;
        private System.Windows.Forms.GroupBox grpColProperties;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox lbxColumns;
        private System.Windows.Forms.ComboBox cmbVisibility;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkAllowFiltering;
        private System.Windows.Forms.CheckBox chkIsNullable;
        private System.Windows.Forms.CheckBox chkAllowSorting;
        private System.Windows.Forms.CheckBox chkIsFixedLength;
        private System.Windows.Forms.NumericUpDown spnMinLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown spnLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbScriptType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbDotNetType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtColumnName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Label label17;
    }
}