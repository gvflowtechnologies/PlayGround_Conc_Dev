﻿Imports System.IO.Ports
Imports System.Threading


Public Class Form1

    Enum commstatus
        Ready = 0
        Pending = 1
        Resend = 2
    End Enum

    Dim DataSent As commstatus
    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Public WithEvents Mycom As SerialPort

    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino
    Dim cycles(5) As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()

        DataSent = commstatus.Ready

        Newcommport()
        RetrieveSettings()

        With Chart1
            .Series(0).Points.Clear()
            .Series(1).Points.Clear()
            .Series(2).Points.Clear()
            .ChartAreas(0).AxisY.Maximum = 1024
            .ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength
            .ChartAreas(0).AxisY.MajorGrid.Interval = 100

        End With

        Dim v As String
        If System.Diagnostics.Debugger.IsAttached = False Then
            v = My.Application.Deployment.CurrentVersion.ToString
        Else
            v = "Unable to Determine Current Version"
        End If
        LblVersion.Text = "Version:" & v

    End Sub

    Public Function SendData(ByVal Packet As String) As Boolean
        ' Function flow chart in lucid

        Dim packetsendtries As Integer = 0
        Dim sendtimeout As Stopwatch
        sendtimeout = New Stopwatch
        Dim transfersucess As Boolean = False
        Do While packetsendtries < 3

            Mycom.Write(Packet)
            DataSent = commstatus.Pending
            packetsendtries = packetsendtries + 1
            sendtimeout.Start()

            Do While (sendtimeout.ElapsedMilliseconds < 1500)

                If DataSent = commstatus.Ready Then
                    transfersucess = True
                    Return transfersucess
                    Exit Function
                End If

                If (DataSent = commstatus.Resend) Then

                    Exit Do
                End If
                Thread.Sleep(1)
            Loop

        Loop

        transfersucess = False
        Return transfersucess

    End Function

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
                .ReceivedBytesThreshold = 2 ' one byte short of a complete messsage string of 16 asci characters   
                .WriteTimeout = 500
                .ReadTimeout = 100
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
            TextBox10.Text = IncomingData
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
                '   LBL_RawPT1.Text = datavalue(0)

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
        Dim datapacket As String
        Dim receivedstatus As Boolean
        'Dim cycles As Integer
        receivedstatus = False

        builder.Append("#")
        cycles(0) = CInt(TB_ProcTime1.Text) / timerperiod
        cycles(1) = CInt(TB_ProcTIme2.Text) / timerperiod
        cycles(2) = CInt(TB_ProcTime3.Text) / timerperiod
        cycles(3) = CInt(TB_ProcTime4.Text) / timerperiod
        cycles(4) = CInt(TB_ProcTime5.Text) / timerperiod
        cycles(5) = CInt(TB_ProcTIme6.Text) / timerperiod

        For Each cycletime In cycles







        Next


        'receivedstatus = SendData(datapacket)


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
        Dim checksum As Char
        cycles = CInt(TB_ProcTime4.Text) / timerperiod
        Dim length As Int16
        Dim receivedstatus As Boolean
        length = 0

        checksum = Chcksum(cycles, length)

        builder.Append("#")
        builder.Append("PT0")
        builder.Append(length)

        builder.Append(cycles.ToString)
        builder.Append(checksum)
        builder.Append("$")
        '#PT03200k$

        updatedtimes = builder.ToString
        receivedstatus = SendData(updatedtimes)
        If (receivedstatus = False) Then
            Button8.BackColor = SystemColors.ControlLight

        End If

    End Sub
    Function Chcksum(ByVal outgoingdata As Int32, ByRef Larray As Int16)
        Dim Stemp As String

        ' convert number to a string
        Stemp = outgoingdata.ToString()
        Dim sum As Byte = 0
        Dim Bmodule As Byte
        Dim diff As Byte
        ' convert each character in string to a byte asci
        Dim calcaray() As Char = Stemp.ToCharArray
        ' calculate checksum
        ' convert each character into asci value
        Larray = Stemp.Length
        For Each Character In calcaray
            sum = (sum + Convert.ToByte(Character)) And &HFF
        Next
        Bmodule = sum Mod 256
        diff = 253 - Bmodule
        'Convert check sum into character to send
        Dim Creturn As Char = Chr(diff) ' System.Text.Encoding.ASCII.GetChars(diff)
        Return Creturn




    End Function
End Class
