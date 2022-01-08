Option Explicit On
Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Text


Public Class Form1

    Enum commstatus
        Ready = 0
        Pending = 1
        Resend = 2
    End Enum
    Enum LOGSTATUS

        Wating
        Logging
        Closing

    End Enum

    'Enum Cycles
    '    Step1 = 0
    '    Step2 = 1
    '    Step3 = 2
    '    Step4 = 3
    '    Step5 = 4
    '    Step6 = 5
    'End Enum

    Enum foldertype
        LogFile = 1
        ScriptFile = 2
    End Enum

    Enum SerialCommands
        ProcessStep1 = 0
        ProcessStep2 = 1
        ProcessStep3 = 2
        ProcessStep4 = 3
        ProcessStep5 = 4
        ProcessStep6 = 5
        Accept = 6
        PressureMatchCycle = 7
        TimeMatchCycle = 8
        RotaryTImeMaxSpeed = 9
        StartSerial = 10
        StopSerial = 11

    End Enum


    Dim enteringcycle As Boolean
    Dim DataSent As commstatus
    Dim RunScript As Boolean
    'Oxygen Sensor Variables
    Dim My_Oxygen_Sensor As TimeOfFlightCalculator
    Public _TackTImer As Timers.Timer

    ReadOnly FolderbeingSent As foldertype

    Public WithEvents Mycom As SerialPort
    Dim currentcycle As Int32
    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino
    Const receivecycle As Integer = 2 ' Receiving data every 2 cycles
    ReadOnly cycles(5) As Integer

    Const FrameStart As Byte = &H7E
    Const packetesc As Byte = &H7E
    Const packetesc1 As Byte = &H7D
    Const DataEsc As Byte = &H5E
    Const DataEsc1 As Byte = &H5D
    Const ZeroOut As Byte = &H0
    Const CalO2Percent As Single = 94


    Dim RotaryDelay As Integer
    Dim State1decay As PressureDecay
    Dim State4decay As PressureDecay

    Dim S_ScriptArray(,) As String

    'Time tracking for Spripting
    Dim ScriptStepTimeLimit As TimeSpan
    Dim ScriptRunTime As Stopwatch

    ' Logging Tracking Variables
    Dim Logging As Boolean ' True if logging is turned on
    Dim LoggingStatus As LOGSTATUS
    Dim I_CLogging As Integer ' Counter to track number of data receive events between last logging event
    Dim F_Logging As String
    ReadOnly SW_Logging As StreamWriter

    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Private Delegate Sub accessformMarshaldelegate1(ByVal Sensed_Temp As String, ByVal Sensed_Concentration As String)



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()

        DataSent = commstatus.Ready
        LoggingStatus = LOGSTATUS.Wating
        currentcycle = 1
        Logging = False
        Newcommport()
        RetrieveSettings()
        enteringcycle = True
        CreateTImer()

        'Load Variables for Decay pressure monitoring
        State1decay = New PressureDecay
        State4decay = New PressureDecay
        RunScript = False

        ScriptRunTime = New Stopwatch
        ScriptRunTime.Stop()
        Tmr_Scripting.Enabled = True
        Tmr_Scripting.Start()


        Dim v As String

        If System.Diagnostics.Debugger.IsAttached = False Then
            v = My.Application.Deployment.CurrentVersion.ToString
        Else
            v = "Unable to Determine Current Version"
        End If
        LblVersion.Text = "Version:" & v

    End Sub

    Private Sub DataLogging(ByVal loggingdata() As Single) ' Call this procudure every nth cycle

        Select Case LoggingStatus ' Finite State machine for logging data
            Case LOGSTATUS.Wating
                If Logging Then

                    F_Logging = ""
                    ' Create a file name based on current times
                    Dim sbname As New StringBuilder()

                    sbname.Append("DataLog M")
                    sbname.Append(DateTime.Now.Month).Append("_D")
                    sbname.Append(DateTime.Now.Day).Append("_h")
                    sbname.Append(DateTime.Now.Hour).Append("_m")
                    sbname.Append(DateTime.Now.Minute)

                    F_Logging = sbname.ToString & ".csv"

                    F_Logging = My.Settings.File_Directory & "\" & F_Logging
                    ' Open a file
                    ' Write Header for File
                    If Not File.Exists(F_Logging) Then
                        Using SW_Logging As StreamWriter = New StreamWriter(F_Logging, False)

                            With SW_Logging
                                .WriteLine("Concentrator DataStream,")
                                .Write("File Start Date,")
                                .WriteLine(DateTime.Now.ToShortDateString)
                                .Write("File Start Time, ")
                                .WriteLine(DateTime.Now.ToShortTimeString)
                                .WriteLine("P1, P ProdTank, P3, P4, Oxy, Temp1, Temp2, Flow, Stage#, Cycle count, MicroAvg1, MicroAvg 4, MVG Avg 1,  MVG AVG 4")

                            End With
                        End Using
                    End If

                    LoggingStatus = LOGSTATUS.Logging
                    Exit Select
                End If
            Case LOGSTATUS.Logging


                Using SW_Logging As StreamWriter = New StreamWriter(F_Logging, True)

                    With SW_Logging
                        Dim Iloggingvalue As Single
                        For Each Iloggingvalue In loggingdata

                            .Write(Iloggingvalue.ToString("F2"))
                            .Write(", ")

                        Next
                        .WriteLine(" ")
                    End With
                End Using

                ' Write to log file

                ' close when flag set
                If Not Logging Then
                    LoggingStatus = LOGSTATUS.Closing
                    Exit Select
                End If

            Case LOGSTATUS.Closing
                ' Get rid of filename
                LoggingStatus = LOGSTATUS.Wating

        End Select

    End Sub

#Region "Input Validation"

    Private Sub TB_ProcTime1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTime1.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTime1.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTime1, errormsg)

        End If
    End Sub

    Private Sub TB_ProcTime1_Validated(sender As Object, e As EventArgs) Handles TB_ProcTime1.Validated
        ErrorProvider1.SetError(TB_ProcTime1, "")
        cycles(0) = CInt(TB_ProcTime1.Text) / timerperiod
    End Sub



    Private Sub TB_ProcTIme2_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTIme2.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTIme2.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTIme2, errormsg)

        End If


    End Sub

    Private Sub TB_ProcTIme2_Validated(sender As Object, e As EventArgs) Handles TB_ProcTIme2.Validated
        ErrorProvider1.SetError(TB_ProcTIme2, "")
        cycles(1) = CInt(TB_ProcTIme2.Text) / timerperiod
    End Sub

    Private Sub TB_ProcTime3_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTime3.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTime3.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTime3, errormsg)

        End If
    End Sub

    Private Sub TB_ProcTime3_Validated(sender As Object, e As EventArgs) Handles TB_ProcTime3.Validated
        ErrorProvider1.SetError(TB_ProcTime3, "")
        cycles(2) = CInt(TB_ProcTime3.Text) / timerperiod
    End Sub
    Private Sub TB_ProcTime4_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTime4.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTime4.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTime4, errormsg)

        End If
    End Sub

    Private Sub TB_ProcTime4_Validated(sender As Object, e As EventArgs) Handles TB_ProcTime4.Validated
        ErrorProvider1.SetError(TB_ProcTime4, "")
        cycles(3) = CInt(TB_ProcTime4.Text) / timerperiod
    End Sub

    Private Sub TB_ProcTime5_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTime5.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTime5.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTime5, errormsg)

        End If
    End Sub

    Private Sub TB_ProcTime5_Validated(sender As Object, e As EventArgs) Handles TB_ProcTime5.Validated
        ErrorProvider1.SetError(TB_ProcTime5, "")
        cycles(4) = CInt(TB_ProcTime5.Text) / timerperiod
    End Sub

    Private Sub TB_ProcTIme6_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ProcTIme6.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim ProcessTime As Integer = 0

        Testresult = Validtimes(TB_ProcTIme6.Text, errormsg, ProcessTime)

        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ProcTIme6, errormsg)

        End If
    End Sub

    Private Sub TB_ProcTIme6_Validated(sender As Object, e As EventArgs) Handles TB_ProcTIme6.Validated
        ErrorProvider1.SetError(TB_ProcTIme6, "")
        cycles(5) = CInt(TB_ProcTIme6.Text) / timerperiod
    End Sub


    Private Sub TB_LogTimeStep_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_LogTimeStep.Validating

        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim LogTime As Single = 0
        Testresult = True

        Testresult = Single.TryParse(TB_LogTimeStep.Text, LogTime)

        If Not Testresult Then
            errormsg = "Not a Number"
            Testresult = False
        End If

        If LogTime < 0.0099 Then
            errormsg = "Logging Time less than 10mSec"
            Testresult = False
        End If


        If LogTime > 60 Then
            errormsg = "Logging Time greater than 60 Sec"
            Testresult = False
        End If


        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_LogTimeStep, errormsg)

        End If


    End Sub
    Private Sub TB_LogTimeStep_Validated(sender As Object, e As EventArgs) Handles TB_LogTimeStep.Validated
        Dim Logtime As Integer

        ' Log Time Step 
        ' Sample if we put in every 2 seconds.  Means that we are logging every 200th datapoint

        ErrorProvider1.SetError(TB_LogTimeStep, "")

        Logtime = CInt(TB_LogTimeStep.Text * 100)
        If Logtime = 0 Then Logtime = 1
        My.Settings.Log_Time_Step = Logtime
        My.Settings.Save()

    End Sub
    Private Function Validtimes(ByVal ProcessTime As String, ByRef errorMessage As String, ByRef ITime As Integer) As Boolean
        ' Function to check the serial number entered is 10 charaters long
        Dim Pass As Boolean

        Pass = Integer.TryParse(ProcessTime, ITime)

        If Not Pass Then
            errorMessage = "Not a valid Ineger"
            Return False
        End If

        If ITime < 100 Then
            errorMessage = "Process Time less than 100mSec"
            Return False
        End If


        If ITime > 15000 Then
            errorMessage = "Process Time greater than 15 Sec"
            Return False
        End If


        Return True

    End Function
    Private Sub TB_GraphDisplay_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_GraphDisplay.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean
        Dim LogTime As Single


        Testresult = Single.TryParse(TB_GraphDisplay.Text, LogTime)

        If Not Testresult Then
            errormsg = "Not a Number"
            Testresult = False
        End If

        If LogTime < 2 Then
            errormsg = "Logging Time less than 2 Sec"
            Testresult = False
        End If


        If LogTime > 3000 Then
            errormsg = "Logging Time greater than 3000 Sec"
            Testresult = False
        End If


        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_GraphDisplay, errormsg)

        End If
    End Sub

    Private Sub TB_GraphDisplay_Validated(sender As Object, e As EventArgs) Handles TB_GraphDisplay.Validated
        Dim newtime As Integer

        ErrorProvider1.SetError(TB_LogTimeStep, "")

        newtime = CInt(TB_GraphDisplay.Text) * 100
        ' Log Time Step 
        ' Sample if we put in every 2 seconds.  Means that we are logging every 200th datapoint

        My.Settings.GraphLength = newtime
        My.Settings.Save()
        ' Chart1.ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength

        ResetGraph()

    End Sub

    Private Sub TB_RotaryDelay_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_RotaryDelay.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean = True
        Dim LogTIme As Single = 0
        Dim DelayTime As Single = 0

        Testresult = Single.TryParse(TB_GraphDisplay.Text, LogTIme)

        If Not Testresult Then
            errormsg = "Not a Number"
            Testresult = False
        End If

        If DelayTime < 2 Then
            errormsg = "Time delay less than 2 mSec"
            Testresult = False
        End If


        If DelayTime > 500 Then
            errormsg = "Time delay greater than 500 mSec"
            Testresult = False
        End If


        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_GraphDisplay, errormsg)

        End If
    End Sub

    Private Sub TB_RotaryDelay_Validate(sender As Object, e As EventArgs) Handles TB_RotaryDelay.Validated

        ErrorProvider1.SetError(TB_LogTimeStep, "")
        RotaryDelay = CInt(TB_RotaryDelay.Text) * 1000

    End Sub

    Private Sub TB_ScriptStepLength_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_ScriptStepLength.Validating
        Dim errormsg As String = ""
        Dim Testresult As Boolean = True
        Dim LogTime As Integer = 0


        Testresult = Integer.TryParse(TB_ScriptStepLength.Text, LogTime)

        If Not Testresult Then
            errormsg = "Not a Integer Minute"
            Testresult = False

        End If

        If LogTime < 2 And LogTime > 0 Then
            errormsg = "Time delay less than 2 Min"
            Testresult = False
        End If


        If LogTime > 500 Then
            errormsg = "Time delay greater than 500 Min"
            Testresult = False
        End If


        If Not Testresult Then

            e.Cancel = True
            Me.ErrorProvider1.SetError(TB_ScriptStepLength, errormsg)

        End If
    End Sub

    Private Sub TB_ScriptStepLength_Validated(sender As Object, e As EventArgs) Handles TB_ScriptStepLength.Validated
        ErrorProvider1.SetError(TB_ScriptStepLength, "")
        My.Settings.Timer_Script_Step = CInt(TB_ScriptStepLength.Text)
        My.Settings.Save()
    End Sub

#End Region



    Public Sub RetrieveSettings()
        Lbl_FileLocation.Text = My.Settings.File_Directory.ToString
        TB_LogTimeStep.Text = (My.Settings.Log_Time_Step / 100).ToString
        TB_GraphDisplay.Text = (My.Settings.GraphLength / 100).ToString


        TB_O2_Cal_TimeUP.Text = My.Settings.Oxygen_CalUP
        TB_O2_Cal_Time_Dwn.Text = My.Settings.Oxygen_CalDn
        TB_O2_cal_Temp.Text = My.Settings.Oxygen_CalTemperature
        TB_O2_Cal_Flow.Text = My.Settings.Oxygen_CalFLow

        ResetGraph()

    End Sub

    Public Sub ResetGraph()
        With Chart1
            .Series(0).Points.Clear()
            .Series(1).Points.Clear()
            .Series(2).Points.Clear()

            With .ChartAreas(0)
                .AxisX.Maximum = My.Settings.GraphLength

                With .AxisY
                    .Maximum = 20
                    .MajorTickMark.IntervalOffset = 0
                    .MajorGrid.Interval = 5
                    .MinorGrid.Interval = .MajorGrid.Interval / 5
                    .MinorGrid.Enabled = True
                    .MinorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
                    .Crossing = 0
                    .Minimum = 0
                End With
            End With
        End With
    End Sub

#Region "Communication"

    Private Sub Newcommport()

        Dim myportnames() As String
        myportnames = SerialPort.GetPortNames
        If IsNothing(Mycom) Then
            Mycom = New SerialPort


            AddHandler Mycom.DataReceived, AddressOf Mycom_Datareceived ' handler for data received event

            With Mycom
                .PortName = "COM7" ' gets port name from static data set
                .BaudRate = 115200
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None  ' Need to think here
                .DataBits = 8
                .ReceivedBytesThreshold = 2 ' one byte short o
                .WriteTimeout = 100
                .ReadTimeout = 10000
                .WriteBufferSize = 500
            End With
        End If
        If (Not Mycom.IsOpen) Then

            Try
                Mycom.Open()
                Mycom.DiscardInBuffer()
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Mycom_Datareceived(ByVal sendor As Object, ByVal e As SerialDataReceivedEventArgs) Handles Mycom.DataReceived
        ' Handles data when it comes in on serial port.

        Dim AccessFormMarshaldelegate1 As New accessformMarshaldelegate(AddressOf AccessForm)
        Me.BeginInvoke(AccessFormMarshaldelegate1, Mycom.ReadLine)


    End Sub

    Private Sub Portclosing()
        My_Oxygen_Sensor.dispose()
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


    Private Sub AccessForm(ByVal TextToDisplay As String)

        ParseIncoming(TextToDisplay)

    End Sub

    Public Function Send_Binary_Data(ByVal Packet() As Byte)
        Dim packetsendtries As Integer = 0
        Dim sendtimeout As Stopwatch
        sendtimeout = New Stopwatch
        Dim transfersucess As Boolean = False
        Do While packetsendtries < 10

            Mycom.Write(Packet, 0, Packet.Length)
            DataSent = commstatus.Pending
            packetsendtries += 1
            sendtimeout.Restart()
            lbl_Returned_Times.Text = packetsendtries.ToString

            Do While sendtimeout.ElapsedMilliseconds < 250

                If DataSent = commstatus.Ready Then
                    transfersucess = True
                    Return transfersucess
                    Exit Function
                End If

                If (DataSent = commstatus.Resend) Then
                    Exit Do
                End If
                Thread.Sleep(5)
                Application.DoEvents()
            Loop
            Thread.Sleep(1)
        Loop

        transfersucess = False
        Return transfersucess


    End Function

#End Region

    Private Sub ParseIncoming(ByRef IncomingData As String)

        Dim length As Integer
        CheckTime()
        length = IncomingData.Length
        If (IncomingData(0) = "#") Then

            lbl_Returned_Times.Text += IncomingData
            TextBox10.Text += IncomingData
            TextBox1.Text += IncomingData
            Select Case IncomingData(1)
                Case "P"
                    Label19.Text = "Yes"
                    DataSent = commstatus.Ready
                Case "R"
                    DataSent = commstatus.Resend
                    Label19.Text = "Resend"
                Case Else
                    Label19.Text = "No Command"
            End Select

        Else

            I_CLogging += 1 ' Update count on logging interval
            ' you want to split this input string
            ' Split string based on comma
            Dim words As String() = IncomingData.Split(New Char() {","c})

            ' Use For Each loop over words and display them
            Dim word As String
            Dim datavalue(9) As Single 'was 14
            Dim i As Integer = 0
            For Each word In words
                Dim sucessess As Boolean = Single.TryParse(word, datavalue(i))
                i += 1
                If i = 8 Then datavalue(i) += 1

            Next
            If TP_Calibration.Visible = True Then
                '   LBL_RawPT1.Text = datavalue(0)

            End If


            If datavalue(3) = 1 Or datavalue(3) = 4 Then
                ' Using Datavalue(1) for slope detection
                If enteringcycle Then

                    If datavalue(3) = 1 Then
                        State1decay.Reset()
                    End If

                    If datavalue(3) = 4 Then
                        State4decay.Reset()
                    End If

                    enteringcycle = False

                Else ' In cycle

                    If datavalue(3) = 1 Then
                        State1decay.Detect(datavalue(1))
                        datavalue(7) = State1decay.PMvgAvgSlope
                        ' datavalue(14) = State1decay.PAvGslope
                        LB_DecayAvg1.Text = datavalue(7).ToString("F1")
                        Lb_Mi1Slope.Text = datavalue(5).ToString("F1")
                        Lb_DecayCurr1.Text = State1decay.PAvGslope.ToString("F1")
                    End If

                    If datavalue(3) = 4 Then
                        State4decay.Detect(datavalue(1))
                        datavalue(8) = State4decay.PMvgAvgSlope
                        Lb_DecayAvg4.Text = datavalue(8).ToString("F1")
                        Lb_Mic4slope.Text = datavalue(6).ToString("F1")
                        Lb_DecayCurr4.Text = State4decay.PAvGslope.ToString("F1")
                        Lbl_CycleTime.Text = (datavalue(4) * timerperiod).ToString ' Showing cycle time for mapping.
                        Lbl_PeakPressure.Text = datavalue(1).ToString  ' Showing peak pressure for mapping.
                    End If
                End If


            End If
            If datavalue(3) <> currentcycle Then ' Only update when needed
                currentcycle = datavalue(3)
                Lbl_CycleStage.Text = currentcycle
                enteringcycle = True
            End If
            If DataSent <> commstatus.Pending Then
                GraphIncoming(datavalue)             '' Add points to the chart
            Else
                ResetGraph()
            End If
            If I_CLogging >= My.Settings.Log_Time_Step Then ' Test to see if we should call the logging routine
                DataLogging(datavalue)
                I_CLogging = 0
            End If

        End If


    End Sub

    Private Sub GraphIncoming(ByVal datatograph() As Single)
        With Chart1
            .Series(0).Points.AddY(datatograph(0))
            .Series(1).Points.AddY(datatograph(1))
            .Series(2).Points.AddY(datatograph(2))
            If (.Series(0).Points.Count > My.Settings.GraphLength) Then
                .Series(0).Points(0).Dispose()
                .Series(1).Points(0).Dispose()
                .Series(2).Points(0).Dispose()

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

        With Btn_UpdateCycleTime
            .Enabled = False
            .BackColor = SystemColors.ButtonFace
            .Visible = False
        End With

        Sub_Update_Cycle_Times()

        With Btn_UpdateCycleTime
            .Enabled = True
            .Visible = True
        End With


    End Sub

    Private Sub Sub_Update_Cycle_Times()
        Dim command As String
        Dim builder As New System.Text.StringBuilder
        Dim receivedstatus As Boolean

        Dim cyclescount As Integer
        receivedstatus = False
        TextBox1.Text = " "
        lbl_Returned_Times.Text = ""

        cycles(0) = UInt16.Parse(TB_ProcTime1.Text) / timerperiod
        cycles(1) = UInt16.Parse(TB_ProcTIme2.Text) / timerperiod
        cycles(2) = UInt16.Parse(TB_ProcTime3.Text) / timerperiod
        cycles(3) = UInt16.Parse(TB_ProcTime4.Text) / timerperiod
        cycles(4) = UInt16.Parse(TB_ProcTime5.Text) / timerperiod
        cycles(5) = UInt16.Parse(TB_ProcTIme6.Text) / timerperiod
        cyclescount = 0
        command = ""


        For Each cycletime In cycles

            'Break message value into two bytes and check for ESC key
            Dim Param_Value(2) As Byte
            Dim asize As Int16
            Dim Parameter As Byte
            Dim CRCValue As Byte
            Dim arraycounter As Int16 ' Counter for the test packet

            asize = 4 ' Size of the array with no esc characters. 3 without CRC.  4 with 8 bit crc 


            ' High BYTE is 0 Low Byte is 1 in the array for the parameter value
            Param_Value(1) = cycles(cyclescount) And &HFF& ' low byte
            asize = CheckforEsc(asize, Param_Value(1))

            Param_Value(0) = (cycles(cyclescount) >> 8)  ' High Byte
            asize = CheckforEsc(asize, Param_Value(1))

            Parameter = Convert.ToByte(cyclescount)

            'Create a packet to get a CRC Value with

            Dim datapack(asize - 1) As Byte ' Sized without CRC.
            arraycounter = 0 ' Initialize counter to the beginning of the array
            'Command Parameter
            datapack(arraycounter) = (Parameter)
            arraycounter += 1


            'Command Value
            Select Case Param_Value(0) ' Sending High Byte First
                Case packetesc1
                    datapack(arraycounter) = packetesc1
                    arraycounter += 1
                    datapack(arraycounter) = DataEsc1

                Case packetesc
                    datapack(arraycounter) = packetesc1
                    arraycounter += 1
                    datapack(arraycounter) = DataEsc

                Case Else
                    datapack(arraycounter) = Param_Value(0) ' Sending high byte first.

            End Select

            arraycounter += 1

            Select Case Param_Value(1) ' Sending High Byte First
                Case packetesc1
                    datapack(arraycounter) = packetesc1
                    arraycounter += 1
                    datapack(arraycounter) = DataEsc1

                Case packetesc
                    datapack(arraycounter) = packetesc1
                    arraycounter += 1
                    datapack(arraycounter) = DataEsc

                Case Else
                    datapack(arraycounter) = Param_Value(1) ' Sending high byte first.fullbytecommand(arraycounter) = testcommand(1) ' Sending high byte first.

            End Select
            datapack(asize - 1) = 0 ' 

            'Calculate CRC Value
            CRCValue = CRC_8Bit.CRC_8(datapack)
            'Determine if the CRC Value is an escape key.
            asize = CheckforEsc(asize, CRCValue)
            '*****************************************************************
            '*****************************************************************
            ' Dimension the full datacommand array and zero out the arrays.
            '*****************************************************************
            '*****************************************************************

            Dim fullbytecommand(asize) As Byte
            arraycounter = 0

            For Each bytevalues In fullbytecommand
                bytevalues = ZeroOut
            Next


            'Start Frame
            arraycounter = 0 ' Initialize counter to the beginning of the array
            fullbytecommand(arraycounter) = FrameStart

            'Command
            arraycounter += 1
            fullbytecommand(arraycounter) = Parameter

            'Data
            arraycounter += 1

            Select Case Param_Value(0) ' Sending High Byte First
                Case packetesc1
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc1

                Case packetesc
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc

                Case Else
                    fullbytecommand(arraycounter) = Param_Value(0) ' Sending high byte first.

            End Select

            arraycounter += 1

            Select Case Param_Value(1) ' Sending High Byte First
                Case packetesc1
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc1

                Case packetesc
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc

                Case Else
                    fullbytecommand(arraycounter) = Param_Value(1) ' Sending high byte first.fullbytecommand(arraycounter) = testcommand(1) ' Sending high byte first.

            End Select

            'crcbyte
            arraycounter += 1

            Select Case CRCValue ' Sending High Byte First
                Case packetesc1
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc1

                Case packetesc
                    fullbytecommand(arraycounter) = packetesc1
                    arraycounter += 1
                    fullbytecommand(arraycounter) = DataEsc

                Case Else
                    fullbytecommand(arraycounter) = CRCValue ' Sending high byte first.fullbytecommand(arraycounter) = testcommand(1) ' Sending high byte first.

            End Select

            receivedstatus = Send_Binary_Data(fullbytecommand)

            If receivedstatus = False Then Exit For ' If transfer failed alert
            cyclescount += 1
            Dim updatelable() As Control
            updatelable = Controls.Find("Lbl_PTime" & cyclescount, True)
            If updatelable.Length > 0 Then
                updatelable(0).Text = (cycles(cyclescount - 1) * timerperiod)
            End If
        Next

        If receivedstatus = True Then

            ' Send an Accept Data Command

            Dim CommandArray(5) As Byte
            CommandArray(0) = FrameStart
            CommandArray(1) = SerialCommands.Accept
            CommandArray(2) = 24
            CommandArray(3) = 56
            CommandArray(4) = 254
            receivedstatus = Send_Binary_Data(CommandArray)
        Else
            Btn_UpdateCycleTime.BackColor = Color.Red
        End If




    End Sub








    Private Sub Btn_LogFiles_Click(sender As Object, e As EventArgs) Handles Btn_LogFiles.Click

        caldata.ReturnFolder(foldertype.LogFile)
        Lbl_FileLocation.Text = My.Settings.File_Directory
    End Sub


    Private Sub Btn_Loging_Toggle_Click(sender As Object, e As EventArgs) Handles Btn_Loging_Toggle.Click
        ' Toggle between logging and not logging
        If Not Logging Then ' Begin logging
            Logging = True
            I_CLogging = 0
            Btn_Loging_Toggle.Text = "Stop Logging"
        Else ' Now not logging
            Logging = False
            Btn_Loging_Toggle.Text = "Start Logging"
        End If




    End Sub

    Private Sub RB_PressBal_CheckedChanged(sender As Object, e As EventArgs) Handles RB_PressBal.CheckedChanged
        If RB_PressBal.Checked Then

            Dim receivedstatus As Boolean
            Dim CommandArray(4) As Byte
            CommandArray(0) = FrameStart
            CommandArray(1) = SerialCommands.PressureMatchCycle
            CommandArray(2) = 24
            CommandArray(3) = 56

            receivedstatus = Send_Binary_Data(CommandArray)


            If receivedstatus = False Then
                RB_PressBal.ForeColor = Color.Red
            Else
                RB_PressBal.ForeColor = SystemColors.ControlText
            End If

        End If
    End Sub

    Private Sub RB_TimeCycle_CheckedChanged(sender As Object, e As EventArgs) Handles RB_TimeCycle.CheckedChanged
        If RB_TimeCycle.Checked Then
            Dim receivedstatus As Boolean


            Dim CommandArray(4) As Byte
            CommandArray(0) = FrameStart
            CommandArray(1) = SerialCommands.TimeMatchCycle
            CommandArray(2) = 24
            CommandArray(3) = 56

            receivedstatus = Send_Binary_Data(CommandArray)

            If receivedstatus = False Then
                RB_PressBal.ForeColor = Color.Red
            Else
                RB_PressBal.ForeColor = SystemColors.ControlText
            End If

        End If
    End Sub
    Private Sub RB_SerialOn_CheckedChanged(sender As Object, e As EventArgs) Handles RB_SerialOn.CheckedChanged
        If RB_SerialOn.Checked Then
            Dim receivedstatus As Boolean

            Dim CommandArray(4) As Byte
            CommandArray(0) = FrameStart
            CommandArray(1) = SerialCommands.StartSerial
            CommandArray(2) = 24
            CommandArray(3) = 56

            receivedstatus = Send_Binary_Data(CommandArray)

            If receivedstatus = False Then
                RB_SerialOn.ForeColor = Color.Red
            Else
                RB_SerialOn.ForeColor = SystemColors.ControlText
            End If

        End If
    End Sub

    Private Sub RB_SerialOff_CheckedChanged(sender As Object, e As EventArgs) Handles RB_SerialOff.CheckedChanged
        If RB_SerialOff.Checked Then
            Dim receivedstatus As Boolean

            Dim CommandArray(4) As Byte
            CommandArray(0) = FrameStart
            CommandArray(1) = SerialCommands.StopSerial
            CommandArray(2) = 24
            CommandArray(3) = 56

            receivedstatus = Send_Binary_Data(CommandArray)

            If receivedstatus = False Then
                RB_SerialOff.ForeColor = Color.Red
            Else
                RB_SerialOff.ForeColor = SystemColors.ControlText
            End If

        End If
    End Sub
    Public Function CheckforEsc(ByVal size As Int16, ByVal testchar As Byte) As Int16

        Dim isize As Int16
        isize = size
        If testchar = packetesc Or testchar = packetesc1 Then
            isize += 1
        End If
        Return isize

    End Function

    Private Sub Btn_RotaryStepDelay_Click(sender As Object, e As EventArgs) Handles Btn_RotaryStepDelay.Click


        Dim receivedstatus As Boolean
        Dim CommandArray(4) As Byte
        CommandArray(0) = FrameStart
        CommandArray(1) = SerialCommands.RotaryTImeMaxSpeed
        CommandArray(2) = 24
        CommandArray(3) = 56

        receivedstatus = Send_Binary_Data(CommandArray)


        If receivedstatus = False Then
            RB_PressBal.ForeColor = Color.Red
        Else
            RB_PressBal.ForeColor = SystemColors.ControlText
        End If


    End Sub

    Private Sub Btn_Script_Click(sender As Object, e As EventArgs) Handles Btn_Script.Click
        'Establish file location.  (My.Setting.Dir_Script) 
        ' Button to start, if no location exists in setting

        'Check to see if file location has been established, if not.  Create one.
        'Make file location persist (Store as a setting)
        'Create ability to select file.
        'Create ability to change time for each run.  (My.Settings.Timer_Script_Step)
        'Track time.

        'Read in file.
        'Parse file.
        ' Send to Arduino using existing routine.  Sub_Update_Cycle_Times()

        Dim scriptfilename As String
        Dim S_ScriptElements() As String
        Dim Scripts As New List(Of String)
        Dim S_Scriptlines() As String 'An array of scripts to run.  Each line is a full setup.  all timing values


        My.Settings.Timer_Script_Step = CInt(TB_ScriptStepLength.Text)
        My.Settings.Save()
        ScriptStepTimeLimit = New TimeSpan(0, My.Settings.Timer_Script_Step, 0) ' Time 


        If My.Settings.Dir_Script = "NULL" Then
            caldata.ReturnFolder(foldertype.ScriptFile)
        End If

        scriptfilename = caldata.ScriptFile(My.Settings.Dir_Script)
        If Not File.Exists(scriptfilename) Then
            Return

        Else
            Using Reader As StreamReader = New StreamReader(scriptfilename)
                While Reader.EndOfStream = False
                    Scripts.Add(Reader.ReadLine())
                End While
            End Using
            S_Scriptlines = Scripts.ToArray
        End If
        S_ScriptElements = S_Scriptlines(0).Split(",")

        ReDim S_ScriptArray(S_Scriptlines.Length - 2, S_ScriptElements.Length - 1)
        ' Create an array of scripts to run.  
        For i = 1 To S_Scriptlines.Length - 1
            S_ScriptElements = S_Scriptlines(i).Split(",")
            For J = 0 To S_ScriptElements.Length - 1
                ' Create an array of scripts to run.  
                S_ScriptArray(i - 1, J) = S_ScriptElements(J)

            Next

        Next
        Dim ParseOK As Boolean
        Dim Sample As Integer
        For i = 1 To S_Scriptlines.Length - 2
            S_ScriptElements = S_Scriptlines(i).Split(",")
            For J = 0 To S_ScriptElements.Length - 1
                ' Create an array of scripts to run.  
                ParseOK = UInt16.TryParse(S_ScriptArray(i, J), Sample)

            Next

        Next

        If ParseOK Then

            PopulateWindow(0)
            Sub_Update_Cycle_Times()
            ScriptRunTime.Start()
            RunScript = True

        End If


    End Sub

    Private Sub CheckTime()

        Static iCounter As Integer = 0
        'Routine runs once a second.
        If RunScript Then
            If ScriptRunTime.Elapsed > ScriptStepTimeLimit Then
                ' run Next step
                iCounter += 1
                ScriptRunTime.Reset()


                'If All files have run then quit
                If iCounter > S_ScriptArray.GetLength(0) - 1 Then


                    Exit Sub
                End If


                ' Else keep running
                PopulateWindow(iCounter)
                Sub_Update_Cycle_Times()
                ScriptRunTime.Start()


            End If
        End If

    End Sub

    Private Sub Tmr_Scripting_Tick(sender As Object, e As EventArgs) Handles Tmr_Scripting.Tick

        CheckTime()



    End Sub
    Private Sub PopulateWindow(ByVal ScriptStep As Integer)

        TB_ProcTime1.Text = S_ScriptArray(ScriptStep, 0)
        TB_ProcTIme2.Text = S_ScriptArray(ScriptStep, 1)
        TB_ProcTime3.Text = S_ScriptArray(ScriptStep, 2)
        TB_ProcTime4.Text = S_ScriptArray(ScriptStep, 3)
        TB_ProcTime5.Text = S_ScriptArray(ScriptStep, 4)
        TB_ProcTIme6.Text = S_ScriptArray(ScriptStep, 5)

    End Sub
#Region "Oxygen Sensor"
    Private Sub CB_O2sens_Enabled_CheckedChanged(sender As Object, e As EventArgs) Handles CB_O2sens_Enabled.CheckedChanged

        ' Establish connection to an Oxygen Sensor and puts in calibration values.


        If CB_O2sens_Enabled.Checked Then

            My_Oxygen_Sensor = New TimeOfFlightCalculator(My.Settings.Oxygen_CalUP, My.Settings.Oxygen_CalDn, My.Settings.Oxygen_CalTemperature, CalO2Percent)
            CB_O2Sens_isRunning.Enabled = True
            CB_O2Sens_isRunning.Visible = True
        Else

            My_Oxygen_Sensor.dispose()
            CB_O2Sens_isRunning.Enabled = False
            CB_O2Sens_isRunning.Visible = False
        End If
    End Sub
    Private Sub CB_O2Sens_isRunning_CheckedChanged(sender As Object, e As EventArgs) Handles CB_O2Sens_isRunning.CheckedChanged
        'Start measuring Oxygen
        If CB_O2Sens_isRunning.Checked Then
            _TackTImer.Enabled = True


        Else
            _TackTImer.Enabled = False


        End If

    End Sub


    Private Sub Btn_Update_O2_Calibration_Click(sender As Object, e As EventArgs) Handles Btn_Update_O2_Calibration.Click

        My.Settings.Oxygen_CalUP = CSng(TB_O2_Cal_TimeUP.Text)
        My.Settings.Oxygen_CalDn = CSng(TB_O2_Cal_Time_Dwn.Text)
        My.Settings.Oxygen_CalO2Percent = CalO2Percent
        My.Settings.Oxygen_CalTemperature = CSng(TB_O2_cal_Temp.Text)
        My.Settings.Oxygen_CalFLow = CSng(TB_O2_Cal_Flow.Text)

        CB_O2sens_Enabled.Enabled = True
        CB_O2sens_Enabled.Visible = True
        Lbl_o2_Enabled.Visible = True

        My.Settings.Save()



    End Sub

    Private Sub CreateTImer()
        _TackTImer = New Timers.Timer(250) With {
            .Enabled = False
        }
        AddHandler _TackTImer.Elapsed, AddressOf Timer_Elapsed
    End Sub
    Private Sub Timer_Elapsed(Sender As Object, e As EventArgs)

        If My_Oxygen_Sensor.Measurement_Complete Then
            Dim frmDelegate As New accessformMarshaldelegate1(AddressOf O2Concentration_Update)
            My_Oxygen_Sensor.PerformMeasurement()

            Me.BeginInvoke(frmDelegate, My_Oxygen_Sensor.Temperature.ToString(), My_Oxygen_Sensor.O2_Percent.ToString())

        End If
    End Sub


    Private Sub O2Concentration_Update(ByVal SensorTemp As String, ByVal Sensor_O2 As String)
        Lbl_Sensed_Temp.Text = SensorTemp
        Lbl_Sensed_O2.Text = Sensor_O2
    End Sub

    Private Sub TB_O2_Cal_TimeUP_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_O2_Cal_TimeUP.Validating

    End Sub

    Private Sub TB_O2_Cal_TimeUP_Validated(sender As Object, e As EventArgs) Handles TB_O2_Cal_TimeUP.Validated

    End Sub

    Private Sub TB_O2_Cal_Time_Dwn_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_O2_Cal_Time_Dwn.Validating

    End Sub

    Private Sub TB_O2_Cal_Time_Dwn_Validated(sender As Object, e As EventArgs) Handles TB_O2_Cal_Time_Dwn.Validated

    End Sub

    Private Sub TB_O2_Cal_Flow_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_O2_Cal_Flow.Validating

    End Sub

    Private Sub TB_O2_Cal_Flow_Validated(sender As Object, e As EventArgs) Handles TB_O2_Cal_Flow.Validated

    End Sub



    Private Sub TB_O2_cal_Temp_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_O2_cal_Temp.Validating

    End Sub

    Private Sub TB_O2_cal_Temp_Validated(sender As Object, e As EventArgs) Handles TB_O2_cal_Temp.Validated

    End Sub


    Private Function Validate_O2Cal(ByVal Value As String, ByRef errorMessage As String, ByRef ITime As Integer) As Boolean
        ' Function to check the serial number entered is 10 charaters long
        Dim Pass As Boolean

        Pass = Single.TryParse(Value, ITime)

        If Not Pass Then
            errorMessage = "Not a valid Integer"
            Return False
        End If

        If ITime < 100 Then
            errorMessage = "Process Time less than 100mSec"
            Return False
        End If


        If ITime > 15000 Then
            errorMessage = "Process Time greater than 15 Sec"
            Return False
        End If


        Return True

    End Function







#End Region
End Class
