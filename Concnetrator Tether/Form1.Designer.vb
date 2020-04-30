<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.RB_TimeCycle = New System.Windows.Forms.RadioButton()
        Me.RB_PressBal = New System.Windows.Forms.RadioButton()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Btn_Loging_Toggle = New System.Windows.Forms.Button()
        Me.TB_LogTimeStep = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.CB_GraphEngUnits = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_GraphDisplay = New System.Windows.Forms.TextBox()
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TB_ScriptStepLength = New System.Windows.Forms.TextBox()
        Me.Lbl_Script = New System.Windows.Forms.Label()
        Me.Lbl_PTime6 = New System.Windows.Forms.Label()
        Me.Lbl_PTime5 = New System.Windows.Forms.Label()
        Me.Lbl_PTime4 = New System.Windows.Forms.Label()
        Me.Lbl_PTime3 = New System.Windows.Forms.Label()
        Me.Lbl_PTime2 = New System.Windows.Forms.Label()
        Me.Lbl_PTime1 = New System.Windows.Forms.Label()
        Me.lbl_Returned_Times = New System.Windows.Forms.Label()
        Me.Btn_UpdateCycleTime = New System.Windows.Forms.Button()
        Me.TB_ProcTIme6 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime5 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime4 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime3 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTIme2 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TP_Calibration = New System.Windows.Forms.TabPage()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Btn_LogFiles = New System.Windows.Forms.Button()
        Me.Lbl_FileLocation = New System.Windows.Forms.Label()
        Me.TB_RotaryDelay = New System.Windows.Forms.TextBox()
        Me.Btn_RotaryStepDelay = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LBL_RawPT1 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Btn_PT1UpdateCalH = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Tmr_Scripting = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Calibration.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TP_Calibration)
        Me.TabControl1.Location = New System.Drawing.Point(3, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1249, 713)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.LblVersion)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Chart1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1241, 687)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.RB_TimeCycle)
        Me.GroupBox8.Controls.Add(Me.RB_PressBal)
        Me.GroupBox8.Location = New System.Drawing.Point(430, 429)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(124, 100)
        Me.GroupBox8.TabIndex = 26
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Cycle Control"
        '
        'RB_TimeCycle
        '
        Me.RB_TimeCycle.AutoSize = True
        Me.RB_TimeCycle.Location = New System.Drawing.Point(9, 62)
        Me.RB_TimeCycle.Name = "RB_TimeCycle"
        Me.RB_TimeCycle.Size = New System.Drawing.Size(83, 17)
        Me.RB_TimeCycle.TabIndex = 1
        Me.RB_TimeCycle.TabStop = True
        Me.RB_TimeCycle.Text = "Time Cycled"
        Me.RB_TimeCycle.UseVisualStyleBackColor = True
        '
        'RB_PressBal
        '
        Me.RB_PressBal.AutoSize = True
        Me.RB_PressBal.Location = New System.Drawing.Point(9, 34)
        Me.RB_PressBal.Name = "RB_PressBal"
        Me.RB_PressBal.Size = New System.Drawing.Size(108, 17)
        Me.RB_PressBal.TabIndex = 0
        Me.RB_PressBal.TabStop = True
        Me.RB_PressBal.Text = "Pressure Balance"
        Me.RB_PressBal.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Btn_Loging_Toggle)
        Me.GroupBox7.Controls.Add(Me.TB_LogTimeStep)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.CB_GraphEngUnits)
        Me.GroupBox7.Controls.Add(Me.Label1)
        Me.GroupBox7.Controls.Add(Me.TB_GraphDisplay)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 429)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(403, 100)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Graph and Logging Setup"
        '
        'Btn_Loging_Toggle
        '
        Me.Btn_Loging_Toggle.Location = New System.Drawing.Point(266, 59)
        Me.Btn_Loging_Toggle.Name = "Btn_Loging_Toggle"
        Me.Btn_Loging_Toggle.Size = New System.Drawing.Size(126, 23)
        Me.Btn_Loging_Toggle.TabIndex = 13
        Me.Btn_Loging_Toggle.Text = "Start Logging"
        Me.Btn_Loging_Toggle.UseVisualStyleBackColor = True
        '
        'TB_LogTimeStep
        '
        Me.TB_LogTimeStep.Location = New System.Drawing.Point(178, 55)
        Me.TB_LogTimeStep.Name = "TB_LogTimeStep"
        Me.TB_LogTimeStep.Size = New System.Drawing.Size(51, 20)
        Me.TB_LogTimeStep.TabIndex = 12
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(16, 59)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(155, 13)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "Log Data Increment (Seconds):"
        '
        'CB_GraphEngUnits
        '
        Me.CB_GraphEngUnits.AutoSize = True
        Me.CB_GraphEngUnits.Location = New System.Drawing.Point(266, 19)
        Me.CB_GraphEngUnits.Name = "CB_GraphEngUnits"
        Me.CB_GraphEngUnits.Size = New System.Drawing.Size(131, 17)
        Me.CB_GraphEngUnits.TabIndex = 6
        Me.CB_GraphEngUnits.Text = "Use Engineering Units"
        Me.CB_GraphEngUnits.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Graph Display (Seconds):"
        '
        'TB_GraphDisplay
        '
        Me.TB_GraphDisplay.Location = New System.Drawing.Point(149, 19)
        Me.TB_GraphDisplay.Name = "TB_GraphDisplay"
        Me.TB_GraphDisplay.Size = New System.Drawing.Size(50, 20)
        Me.TB_GraphDisplay.TabIndex = 2
        Me.TB_GraphDisplay.Text = "20"
        '
        'LblVersion
        '
        Me.LblVersion.AutoSize = True
        Me.LblVersion.Location = New System.Drawing.Point(1103, 654)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(42, 13)
        Me.LblVersion.TabIndex = 7
        Me.LblVersion.Text = "Version"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(926, 527)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(295, 103)
        Me.TextBox1.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TB_ScriptStepLength)
        Me.GroupBox1.Controls.Add(Me.Lbl_Script)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime6)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime5)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime4)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime3)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime2)
        Me.GroupBox1.Controls.Add(Me.Lbl_PTime1)
        Me.GroupBox1.Controls.Add(Me.lbl_Returned_Times)
        Me.GroupBox1.Controls.Add(Me.Btn_UpdateCycleTime)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTIme6)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTime5)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTime4)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTime3)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTIme2)
        Me.GroupBox1.Controls.Add(Me.TB_ProcTime1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 535)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 146)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Process Times"
        '
        'TB_ScriptStepLength
        '
        Me.TB_ScriptStepLength.Location = New System.Drawing.Point(433, 122)
        Me.TB_ScriptStepLength.Name = "TB_ScriptStepLength"
        Me.TB_ScriptStepLength.Size = New System.Drawing.Size(30, 20)
        Me.TB_ScriptStepLength.TabIndex = 22
        Me.TB_ScriptStepLength.Text = "30"
        '
        'Lbl_Script
        '
        Me.Lbl_Script.AutoSize = True
        Me.Lbl_Script.Location = New System.Drawing.Point(314, 129)
        Me.Lbl_Script.Name = "Lbl_Script"
        Me.Lbl_Script.Size = New System.Drawing.Size(98, 13)
        Me.Lbl_Script.TabIndex = 21
        Me.Lbl_Script.Text = "Script Step in (min):"
        '
        'Lbl_PTime6
        '
        Me.Lbl_PTime6.AutoSize = True
        Me.Lbl_PTime6.Location = New System.Drawing.Point(498, 79)
        Me.Lbl_PTime6.Name = "Lbl_PTime6"
        Me.Lbl_PTime6.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime6.TabIndex = 19
        Me.Lbl_PTime6.Text = "100"
        '
        'Lbl_PTime5
        '
        Me.Lbl_PTime5.AutoSize = True
        Me.Lbl_PTime5.Location = New System.Drawing.Point(406, 79)
        Me.Lbl_PTime5.Name = "Lbl_PTime5"
        Me.Lbl_PTime5.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime5.TabIndex = 18
        Me.Lbl_PTime5.Text = "100"
        '
        'Lbl_PTime4
        '
        Me.Lbl_PTime4.AutoSize = True
        Me.Lbl_PTime4.Location = New System.Drawing.Point(314, 79)
        Me.Lbl_PTime4.Name = "Lbl_PTime4"
        Me.Lbl_PTime4.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime4.TabIndex = 17
        Me.Lbl_PTime4.Text = "100"
        '
        'Lbl_PTime3
        '
        Me.Lbl_PTime3.AutoSize = True
        Me.Lbl_PTime3.Location = New System.Drawing.Point(222, 79)
        Me.Lbl_PTime3.Name = "Lbl_PTime3"
        Me.Lbl_PTime3.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime3.TabIndex = 16
        Me.Lbl_PTime3.Text = "100"
        '
        'Lbl_PTime2
        '
        Me.Lbl_PTime2.AutoSize = True
        Me.Lbl_PTime2.Location = New System.Drawing.Point(130, 79)
        Me.Lbl_PTime2.Name = "Lbl_PTime2"
        Me.Lbl_PTime2.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime2.TabIndex = 15
        Me.Lbl_PTime2.Text = "100"
        '
        'Lbl_PTime1
        '
        Me.Lbl_PTime1.AutoSize = True
        Me.Lbl_PTime1.Location = New System.Drawing.Point(38, 79)
        Me.Lbl_PTime1.Name = "Lbl_PTime1"
        Me.Lbl_PTime1.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_PTime1.TabIndex = 14
        Me.Lbl_PTime1.Text = "100"
        '
        'lbl_Returned_Times
        '
        Me.lbl_Returned_Times.AutoSize = True
        Me.lbl_Returned_Times.Location = New System.Drawing.Point(26, 119)
        Me.lbl_Returned_Times.Name = "lbl_Returned_Times"
        Me.lbl_Returned_Times.Size = New System.Drawing.Size(79, 13)
        Me.lbl_Returned_Times.TabIndex = 13
        Me.lbl_Returned_Times.Text = "Updated Times"
        '
        'Btn_UpdateCycleTime
        '
        Me.Btn_UpdateCycleTime.Location = New System.Drawing.Point(531, 119)
        Me.Btn_UpdateCycleTime.Name = "Btn_UpdateCycleTime"
        Me.Btn_UpdateCycleTime.Size = New System.Drawing.Size(75, 23)
        Me.Btn_UpdateCycleTime.TabIndex = 12
        Me.Btn_UpdateCycleTime.Text = "Update"
        Me.Btn_UpdateCycleTime.UseVisualStyleBackColor = True
        '
        'TB_ProcTIme6
        '
        Me.TB_ProcTIme6.Location = New System.Drawing.Point(498, 43)
        Me.TB_ProcTIme6.Name = "TB_ProcTIme6"
        Me.TB_ProcTIme6.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTIme6.TabIndex = 11
        Me.TB_ProcTIme6.Text = "320"
        Me.TB_ProcTIme6.Visible = False
        '
        'TB_ProcTime5
        '
        Me.TB_ProcTime5.Location = New System.Drawing.Point(406, 43)
        Me.TB_ProcTime5.Name = "TB_ProcTime5"
        Me.TB_ProcTime5.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime5.TabIndex = 10
        Me.TB_ProcTime5.Text = "480"
        Me.TB_ProcTime5.Visible = False
        '
        'TB_ProcTime4
        '
        Me.TB_ProcTime4.Location = New System.Drawing.Point(314, 43)
        Me.TB_ProcTime4.Name = "TB_ProcTime4"
        Me.TB_ProcTime4.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime4.TabIndex = 9
        Me.TB_ProcTime4.Text = "5"
        '
        'TB_ProcTime3
        '
        Me.TB_ProcTime3.Location = New System.Drawing.Point(225, 43)
        Me.TB_ProcTime3.Name = "TB_ProcTime3"
        Me.TB_ProcTime3.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime3.TabIndex = 8
        Me.TB_ProcTime3.Text = "50"
        '
        'TB_ProcTIme2
        '
        Me.TB_ProcTIme2.Location = New System.Drawing.Point(130, 43)
        Me.TB_ProcTIme2.Name = "TB_ProcTIme2"
        Me.TB_ProcTIme2.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTIme2.TabIndex = 7
        Me.TB_ProcTIme2.Text = "1600"
        '
        'TB_ProcTime1
        '
        Me.TB_ProcTime1.Location = New System.Drawing.Point(38, 43)
        Me.TB_ProcTime1.Name = "TB_ProcTime1"
        Me.TB_ProcTime1.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime1.TabIndex = 6
        Me.TB_ProcTime1.Text = "800"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(495, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Step 6"
        Me.Label6.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(403, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Step 5"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(311, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "PEEP"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(219, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Pmax"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(127, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Expiratory"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(35, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Inspiratory"
        '
        'Chart1
        '
        ChartArea2.AxisY.Crossing = -1.7976931348623157E+308R
        ChartArea2.AxisY.IsStartedFromZero = False
        ChartArea2.AxisY.MajorGrid.Interval = 0R
        ChartArea2.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Enabled = False
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(6, 27)
        Me.Chart1.Name = "Chart1"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Legend = "Legend1"
        Series4.Name = "PT1"
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Legend = "Legend1"
        Series5.Name = "PT2"
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Legend = "Legend1"
        Series6.Name = "PT3"
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Series.Add(Series5)
        Me.Chart1.Series.Add(Series6)
        Me.Chart1.Size = New System.Drawing.Size(1192, 409)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'TP_Calibration
        '
        Me.TP_Calibration.Controls.Add(Me.GroupBox9)
        Me.TP_Calibration.Controls.Add(Me.Label19)
        Me.TP_Calibration.Controls.Add(Me.TextBox10)
        Me.TP_Calibration.Controls.Add(Me.Button8)
        Me.TP_Calibration.Controls.Add(Me.GroupBox2)
        Me.TP_Calibration.Location = New System.Drawing.Point(4, 22)
        Me.TP_Calibration.Name = "TP_Calibration"
        Me.TP_Calibration.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Calibration.Size = New System.Drawing.Size(1241, 687)
        Me.TP_Calibration.TabIndex = 1
        Me.TP_Calibration.Text = "TabPage2"
        Me.TP_Calibration.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label27)
        Me.GroupBox9.Controls.Add(Me.Btn_LogFiles)
        Me.GroupBox9.Controls.Add(Me.Lbl_FileLocation)
        Me.GroupBox9.Controls.Add(Me.TB_RotaryDelay)
        Me.GroupBox9.Controls.Add(Me.Btn_RotaryStepDelay)
        Me.GroupBox9.Location = New System.Drawing.Point(819, 34)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(407, 647)
        Me.GroupBox9.TabIndex = 6
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Miscellaneous Controls"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(54, 37)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Rotary Delay (mSec)"
        '
        'Btn_LogFiles
        '
        Me.Btn_LogFiles.Location = New System.Drawing.Point(19, 130)
        Me.Btn_LogFiles.Name = "Btn_LogFiles"
        Me.Btn_LogFiles.Size = New System.Drawing.Size(75, 23)
        Me.Btn_LogFiles.TabIndex = 5
        Me.Btn_LogFiles.Text = "Data Folder"
        Me.Btn_LogFiles.UseVisualStyleBackColor = True
        '
        'Lbl_FileLocation
        '
        Me.Lbl_FileLocation.AutoSize = True
        Me.Lbl_FileLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_FileLocation.Location = New System.Drawing.Point(110, 135)
        Me.Lbl_FileLocation.Name = "Lbl_FileLocation"
        Me.Lbl_FileLocation.Size = New System.Drawing.Size(86, 15)
        Me.Lbl_FileLocation.TabIndex = 4
        Me.Lbl_FileLocation.Text = "Lbl_FileLocation"
        '
        'TB_RotaryDelay
        '
        Me.TB_RotaryDelay.Location = New System.Drawing.Point(174, 34)
        Me.TB_RotaryDelay.Name = "TB_RotaryDelay"
        Me.TB_RotaryDelay.Size = New System.Drawing.Size(50, 20)
        Me.TB_RotaryDelay.TabIndex = 1
        Me.TB_RotaryDelay.Text = "2"
        '
        'Btn_RotaryStepDelay
        '
        Me.Btn_RotaryStepDelay.Location = New System.Drawing.Point(230, 32)
        Me.Btn_RotaryStepDelay.Name = "Btn_RotaryStepDelay"
        Me.Btn_RotaryStepDelay.Size = New System.Drawing.Size(110, 23)
        Me.Btn_RotaryStepDelay.TabIndex = 0
        Me.Btn_RotaryStepDelay.Text = "Update Rotary"
        Me.Btn_RotaryStepDelay.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(228, 603)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(45, 13)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Label19"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(196, 388)
        Me.TextBox10.Multiline = True
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(158, 145)
        Me.TextBox10.TabIndex = 2
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(228, 550)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(75, 23)
        Me.Button8.TabIndex = 1
        Me.Button8.Text = "Button8"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 34)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(775, 373)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transducer Calibration"
        Me.GroupBox2.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Controls.Add(Me.TextBox8)
        Me.GroupBox6.Controls.Add(Me.Button6)
        Me.GroupBox6.Controls.Add(Me.Label17)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.TextBox9)
        Me.GroupBox6.Controls.Add(Me.Button7)
        Me.GroupBox6.Location = New System.Drawing.Point(583, 34)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(164, 244)
        Me.GroupBox6.TabIndex = 7
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Pressure Transducer 1"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(31, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Label13"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(6, 174)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(100, 20)
        Me.TextBox8.TabIndex = 5
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(9, 200)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 4
        Me.Button6.Text = "Cal Low"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 158)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 13)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Low Pressure Eng Units"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 58)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(122, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "High Pressure Eng Units"
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(6, 74)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(100, 20)
        Me.TextBox9.TabIndex = 1
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(6, 111)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 23)
        Me.Button7.TabIndex = 0
        Me.Button7.Text = "Cal High"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.TextBox6)
        Me.GroupBox5.Controls.Add(Me.Button4)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.Label15)
        Me.GroupBox5.Controls.Add(Me.TextBox7)
        Me.GroupBox5.Controls.Add(Me.Button5)
        Me.GroupBox5.Location = New System.Drawing.Point(394, 34)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(164, 244)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Pressure Transducer 1"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(31, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Label13"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(6, 174)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 20)
        Me.TextBox6.TabIndex = 5
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(9, 200)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Cal Low"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 158)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 13)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Low Pressure Eng Units"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 58)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(122, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "High Pressure Eng Units"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(6, 74)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(100, 20)
        Me.TextBox7.TabIndex = 1
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(6, 111)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "Cal High"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.TextBox4)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.TextBox5)
        Me.GroupBox4.Controls.Add(Me.Button3)
        Me.GroupBox4.Location = New System.Drawing.Point(205, 34)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(164, 244)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Pressure Transducer 1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(31, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Label13"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(6, 174)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 5
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(9, 200)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cal Low"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 158)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Low Pressure Eng Units"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(122, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "High Pressure Eng Units"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(6, 74)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 1
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(6, 111)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "Cal High"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LBL_RawPT1)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.Btn_PT1UpdateCalH)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(164, 244)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Pressure Transducer 1"
        '
        'LBL_RawPT1
        '
        Me.LBL_RawPT1.AutoSize = True
        Me.LBL_RawPT1.Location = New System.Drawing.Point(31, 27)
        Me.LBL_RawPT1.Name = "LBL_RawPT1"
        Me.LBL_RawPT1.Size = New System.Drawing.Size(45, 13)
        Me.LBL_RawPT1.TabIndex = 6
        Me.LBL_RawPT1.Text = "Label13"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(6, 174)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 200)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Cal Low"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 158)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(120, 13)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Low Pressure Eng Units"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "High Pressure Eng Units"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(6, 74)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 1
        '
        'Btn_PT1UpdateCalH
        '
        Me.Btn_PT1UpdateCalH.Location = New System.Drawing.Point(6, 111)
        Me.Btn_PT1UpdateCalH.Name = "Btn_PT1UpdateCalH"
        Me.Btn_PT1UpdateCalH.Size = New System.Drawing.Size(75, 23)
        Me.Btn_PT1UpdateCalH.TabIndex = 0
        Me.Btn_PT1UpdateCalH.Text = "Cal High"
        Me.Btn_PT1UpdateCalH.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Tmr_Scripting
        '
        Me.Tmr_Scripting.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1264, 729)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Calibration.ResumeLayout(False)
        Me.TP_Calibration.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TP_Calibration As TabPage
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TB_ProcTIme6 As TextBox
    Friend WithEvents TB_ProcTime5 As TextBox
    Friend WithEvents TB_ProcTime4 As TextBox
    Friend WithEvents TB_ProcTime3 As TextBox
    Friend WithEvents TB_ProcTIme2 As TextBox
    Friend WithEvents TB_ProcTime1 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TB_GraphDisplay As TextBox
    Friend WithEvents Btn_UpdateCycleTime As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lbl_Returned_Times As Label
    Friend WithEvents CB_GraphEngUnits As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Btn_PT1UpdateCalH As Button
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents LBL_RawPT1 As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label16 As Label
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents Button6 As Button
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents Button7 As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents LblVersion As Label
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents Lbl_PTime1 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Lbl_PTime6 As Label
    Friend WithEvents Lbl_PTime5 As Label
    Friend WithEvents Lbl_PTime4 As Label
    Friend WithEvents Lbl_PTime3 As Label
    Friend WithEvents Lbl_PTime2 As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Label20 As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents TB_LogTimeStep As TextBox
    Friend WithEvents Btn_Loging_Toggle As Button
    Friend WithEvents Lbl_FileLocation As Label
    Friend WithEvents Btn_LogFiles As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents RB_TimeCycle As RadioButton
    Friend WithEvents RB_PressBal As RadioButton
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents Label27 As Label
    Friend WithEvents TB_RotaryDelay As TextBox
    Friend WithEvents Btn_RotaryStepDelay As Button
    Friend WithEvents Tmr_Scripting As Timer
    Friend WithEvents TB_ScriptStepLength As TextBox
    Friend WithEvents Lbl_Script As Label
End Class
