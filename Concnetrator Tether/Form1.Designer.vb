<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TB_ProcTime1 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTIme2 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime3 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime4 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTime5 = New System.Windows.Forms.TextBox()
        Me.TB_ProcTIme6 = New System.Windows.Forms.TextBox()
        Me.TB_GraphDisplay = New System.Windows.Forms.TextBox()
        Me.Btn_UpdateCycleTime = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Update_Graph = New System.Windows.Forms.Button()
        Me.lbl_Returned_Times = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1249, 713)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Btn_Update_Graph)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TB_GraphDisplay)
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
        'Chart1
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(6, 31)
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
        Me.Chart1.Size = New System.Drawing.Size(1192, 173)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1522, 636)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(53, 482)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(593, 132)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Process Times"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(495, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Step 6"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(403, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Step 5"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(311, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Step 4"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(127, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Step 2"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(219, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Step 3"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(35, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Step 1"
        '
        'TB_ProcTime1
        '
        Me.TB_ProcTime1.Location = New System.Drawing.Point(29, 68)
        Me.TB_ProcTime1.Name = "TB_ProcTime1"
        Me.TB_ProcTime1.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime1.TabIndex = 6
        Me.TB_ProcTime1.Text = "1000"
        '
        'TB_ProcTIme2
        '
        Me.TB_ProcTIme2.Location = New System.Drawing.Point(121, 68)
        Me.TB_ProcTIme2.Name = "TB_ProcTIme2"
        Me.TB_ProcTIme2.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTIme2.TabIndex = 7
        Me.TB_ProcTIme2.Text = "1000"
        '
        'TB_ProcTime3
        '
        Me.TB_ProcTime3.Location = New System.Drawing.Point(213, 68)
        Me.TB_ProcTime3.Name = "TB_ProcTime3"
        Me.TB_ProcTime3.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime3.TabIndex = 8
        Me.TB_ProcTime3.Text = "1000"
        '
        'TB_ProcTime4
        '
        Me.TB_ProcTime4.Location = New System.Drawing.Point(305, 68)
        Me.TB_ProcTime4.Name = "TB_ProcTime4"
        Me.TB_ProcTime4.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime4.TabIndex = 9
        Me.TB_ProcTime4.Text = "1000"
        '
        'TB_ProcTime5
        '
        Me.TB_ProcTime5.Location = New System.Drawing.Point(397, 68)
        Me.TB_ProcTime5.Name = "TB_ProcTime5"
        Me.TB_ProcTime5.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTime5.TabIndex = 10
        Me.TB_ProcTime5.Text = "1000"
        '
        'TB_ProcTIme6
        '
        Me.TB_ProcTIme6.Location = New System.Drawing.Point(489, 68)
        Me.TB_ProcTIme6.Name = "TB_ProcTIme6"
        Me.TB_ProcTIme6.Size = New System.Drawing.Size(50, 20)
        Me.TB_ProcTIme6.TabIndex = 11
        Me.TB_ProcTIme6.Text = "1000"
        '
        'TB_GraphDisplay
        '
        Me.TB_GraphDisplay.Location = New System.Drawing.Point(183, 381)
        Me.TB_GraphDisplay.Name = "TB_GraphDisplay"
        Me.TB_GraphDisplay.Size = New System.Drawing.Size(50, 20)
        Me.TB_GraphDisplay.TabIndex = 2
        Me.TB_GraphDisplay.Text = "20"
        '
        'Btn_UpdateCycleTime
        '
        Me.Btn_UpdateCycleTime.Location = New System.Drawing.Point(479, 95)
        Me.Btn_UpdateCycleTime.Name = "Btn_UpdateCycleTime"
        Me.Btn_UpdateCycleTime.Size = New System.Drawing.Size(75, 23)
        Me.Btn_UpdateCycleTime.TabIndex = 12
        Me.Btn_UpdateCycleTime.Text = "Update"
        Me.Btn_UpdateCycleTime.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 385)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Graph Display (Seconds):"
        '
        'Btn_Update_Graph
        '
        Me.Btn_Update_Graph.Location = New System.Drawing.Point(101, 417)
        Me.Btn_Update_Graph.Name = "Btn_Update_Graph"
        Me.Btn_Update_Graph.Size = New System.Drawing.Size(132, 23)
        Me.Btn_Update_Graph.TabIndex = 4
        Me.Btn_Update_Graph.Text = "Update Graph"
        Me.Btn_Update_Graph.UseVisualStyleBackColor = True
        '
        'lbl_Returned_Times
        '
        Me.lbl_Returned_Times.AutoSize = True
        Me.lbl_Returned_Times.Location = New System.Drawing.Point(39, 115)
        Me.lbl_Returned_Times.Name = "lbl_Returned_Times"
        Me.lbl_Returned_Times.Size = New System.Drawing.Size(79, 13)
        Me.lbl_Returned_Times.TabIndex = 13
        Me.lbl_Returned_Times.Text = "Updated Times"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(874, 609)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(295, 20)
        Me.TextBox1.TabIndex = 5
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
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
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
    Friend WithEvents Btn_Update_Graph As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lbl_Returned_Times As Label
End Class
