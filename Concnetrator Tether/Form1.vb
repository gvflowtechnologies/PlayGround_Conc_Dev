﻿Imports System.IO.Ports
Imports System.Threading


Public Class Form1



    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Public WithEvents Mycom As SerialPort

    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()


        newcommport()
        RetrieveSettings()

        With Chart1
            .Series(0).Points.Clear()
            .Series(1).Points.Clear()
            .Series(2).Points.Clear()
            .ChartAreas(0).AxisY.Maximum = 1024
            .ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength
            .ChartAreas(0).AxisY.MajorGrid.Interval = 100

        End With

    End Sub

    Public Sub RetrieveSettings()




    End Sub

    Private Sub Newcommport()

        Dim myportnames() As String
        myportnames = SerialPort.GetPortNames
        If IsNothing(Mycom) Then
            Mycom = New SerialPort


            AddHandler Mycom.DataReceived, AddressOf Mycom_Datareceived ' handler for data received event

            With Mycom
                .PortName = "COM5" ' gets port name from static data set
                .BaudRate = 115200
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None  ' Need to think here
                .DataBits = 8
                .ReceivedBytesThreshold = 14 ' one byte short of a complete messsage string of 16 asci characters   
                .WriteTimeout = 500
                .WriteBufferSize = 500
            End With
        End If
        If (Not Mycom.IsOpen) Then

            Try
                Mycom.Open()
                Mycom.DiscardInBuffer()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Mycom_Datareceived(ByVal sendor As Object, ByVal e As SerialDataReceivedEventArgs) Handles Mycom.DataReceived
        ' Handles data when it comes in on serial port.
        Dim sweight As String
        sweight = Mycom.ReadLine
        AccessformMarshal(sweight)
    End Sub

    Private Sub Portclosing()
        If IsNothing(Mycom) Then Exit Sub
        If Mycom.IsOpen = True Then
            Mycom.ReceivedBytesThreshold = 1500
            Thread.Sleep(1)
            Do Until Mycom.BytesToRead < 1
                'Application.DoEvents()
                Mycom.DiscardInBuffer()
            Loop
            Mycom.DtrEnable = False
            Mycom.Close()
            Do Until Mycom.IsOpen = False
                Application.DoEvents()
                '   Thread.Sleep(1)
            Loop
        End If
    End Sub

    Private Sub AccessformMarshal(ByVal texttodisplay As String)
        Dim args() As Object = {texttodisplay}
        Dim AccessFormMarshaldelegate1 As New accessformMarshaldelegate(AddressOf AccessForm)
        MyBase.BeginInvoke(AccessFormMarshaldelegate1, args)

    End Sub
    Private Sub AccessForm(ByVal TextToDisplay As String)

        ParseIncoming(TextToDisplay)

    End Sub


    Private Sub ParseIncoming(ByRef IncomingData As String)

        Dim length As Integer

        length = IncomingData.Length
        If (IncomingData(0) = "@") Then

            lbl_Returned_Times.Text = IncomingData
        Else

            ' you want to split this input string


            ' Split string based on comma
            Dim words As String() = IncomingData.Split(New Char() {","c})

            ' Use For Each loop over words and display them
            Dim word As String
            Dim datavalue(8) As Integer
            Dim i As Integer = 0
            For Each word In words
                Dim sucessess As Boolean = Int32.TryParse(word, datavalue(i))
                i = i + 1
            Next
            If TP_Calibration.Visible = True Then
                LBL_RawPT1.Text = datavalue(0)

            End If

            GraphIncoming(datavalue)

            '' Add points to the chart
            TextBox1.AppendText(IncomingData)
        End If


    End Sub

    Private Sub GraphIncoming(ByVal datatograph() As Int32)
        With Chart1
            .Series(0).Points.AddY(datatograph(0))
            .Series(1).Points.AddY(datatograph(1))
            .Series(2).Points.AddY(datatograph(2))
            If (.Series(0).Points.Count > My.Settings.GraphLength) Then
                .Series(0).Points.RemoveAt(0)
                .Series(1).Points.RemoveAt(0)
                .Series(2).Points.RemoveAt(0)
            End If
        End With

    End Sub


    Private Sub Form1_FormCLosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Portclosing()
        Interop.GOTOSLEEP()
    End Sub

    Private Sub Btn_UpdateCycleTime_Click(sender As Object, e As EventArgs) Handles Btn_UpdateCycleTime.Click
        Dim updatedtimes As String
        Dim builder As New System.Text.StringBuilder
        Dim cycles As Integer

        builder.Append("#")
        cycles = CInt(TB_ProcTime1.Text) / timerperiod
        builder.Append(cycles)
        builder.Append(":")
        cycles = CInt(TB_ProcTIme2.Text) / timerperiod
        builder.Append(cycles)
        builder.Append(":")
        cycles = CInt(TB_ProcTime3.Text) / timerperiod
        builder.Append(cycles)
        builder.Append(":")
        cycles = CInt(TB_ProcTime4.Text) / timerperiod
        builder.Append(cycles)
        builder.Append(":")
        cycles = CInt(TB_ProcTime5.Text) / timerperiod
        builder.Append(cycles)
        builder.Append(":")
        cycles = CInt(TB_ProcTIme6.Text) / timerperiod
        builder.Append(cycles)
        builder.Append("Z")

        updatedtimes = builder.ToString
        Mycom.Write(updatedtimes)
    End Sub

    Private Sub Btn_Update_Graph_Click(sender As Object, e As EventArgs) Handles Btn_Update_Graph.Click
        Dim newtime As Integer
        newtime = CInt(TB_GraphDisplay.Text) * 100
        My.Settings.GraphLength = newtime
        My.Settings.Save()
        Chart1.ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength

    End Sub

    Private Sub Btn_PT1UpdateCalH_Click(sender As Object, e As EventArgs) Handles Btn_PT1UpdateCalH.Click

    End Sub

    Private Sub TP_Calibration_Click(sender As Object, e As EventArgs) Handles TP_Calibration.Click

    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim updatedtimes As String
        Dim builder As New System.Text.StringBuilder
        Dim cycles As Integer

        builder.Append("#")
        builder.Append("PT0")
        builder.Append("2")
        cycles = CInt(TB_ProcTime4.Text) / timerperiod
        builder.Append("$")
        '#PT000000023465$
        updatedtimes = builder.ToString
        Mycom.Write(updatedtimes)

    End Sub
End Class
