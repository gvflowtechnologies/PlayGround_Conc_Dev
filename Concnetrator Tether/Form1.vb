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
    Enum Sstate
        peakdection
        inslope
        CompleteSlope
    End Enum

    Dim Decay As Sstate
    Dim DataSent As commstatus
    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Public WithEvents Mycom As SerialPort
    Dim currentcycle As Int32
    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino
    Const receivecycle As Integer = 2 ' Receiving data every 2 cycles
    Dim cycles(5) As Integer
    Dim State1decay As PressureDecay
    Dim State4decay As PressureDecay
    Dim enteringcycle As Boolean ' Entering Cycle


    ' Logging Tracking Variables
    Dim Logging As Boolean ' True if logging is turned on
    Dim LoggingStatus As LOGSTATUS
    Dim I_CLogging As Integer ' Counter to track number of data receive events between last logging event
    Dim F_Logging As String
    Dim SW_Logging As StreamWriter

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()

        DataSent = commstatus.Ready
        LoggingStatus = LOGSTATUS.Wating
        currentcycle = 1
        Logging = False
        Newcommport()
        RetrieveSettings()
        enteringcycle = True

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

    Private Sub DataLogging(ByVal loggingdata() As Integer) ' Call this procudure every nth cycle

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
                                .WriteLine("P1, P2, P3, P4, Oxy, Temp1, Temp2, Flow, Stage#")

                            End With
                        End Using
                    End If

                    LoggingStatus = LOGSTATUS.Logging
                    Exit Select
                End If
            Case LOGSTATUS.Logging


                Using SW_Logging As StreamWriter = New StreamWriter(F_Logging, True)

                    With SW_Logging
                        Dim Iloggingvalue As Integer
                        For Each Iloggingvalue In loggingdata

                            .Write(Iloggingvalue.ToString)
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
        Dim LogTime As Single = 0
        Testresult = True

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
        Chart1.ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength



    End Sub

#End Region


    Public Function Createpacket(ByRef command As String, ByRef datavalue As Int16) As String

        Dim Spacket As String
        Dim builder As New System.Text.StringBuilder
        Dim checksum As Char

        Dim length As Int16

        length = 0

        checksum = Chcksum(datavalue, length)

        builder.Append("#")
        builder.Append(command)
        builder.Append(length)
        builder.Append(datavalue.ToString)
        builder.Append(checksum)
        builder.Append("$")
        Spacket = builder.ToString
        Return Spacket

    End Function


    Public Function SendData(ByVal Packet As String) As Boolean
        ' Function flow chart in lucid

        Dim packetsendtries As Integer = 0
        Dim sendtimeout As Stopwatch
        sendtimeout = New Stopwatch
        Dim transfersucess As Boolean = False
        Do While packetsendtries < 6

            Mycom.Write(Packet)
            DataSent = commstatus.Pending
            packetsendtries = packetsendtries + 1
            sendtimeout.Restart()
            lbl_Returned_Times.Text = packetsendtries.ToString

            Do While sendtimeout.ElapsedMilliseconds < 1000

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

    Public Sub RetrieveSettings()
        Lbl_FileLocation.Text = My.Settings.File_Directory.ToString
        TB_LogTimeStep.Text = (My.Settings.Log_Time_Step / 100).ToString
        TB_GraphDisplay.Text = (My.Settings.GraphLength / 100).ToString

    End Sub

#Region "Communication"

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
                .WriteTimeout = 100
                .ReadTimeout = 1000
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

#End Region

    Private Sub ParseIncoming(ByRef IncomingData As String)

        Dim length As Integer
        ' TextBox1.Text += IncomingData
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

            ' you want to split this input string
            I_CLogging += 1

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
            ' Start Peak Detection
            If datavalue(8) = 1 Or datavalue(8) = 4 Then
                ' Using Datavalue(1) for slope detection
                If enteringcycle Then
                    Decay = Sstate.peakdection
                    If datavalue(8) = 1 Then
                        State1decay = New PressureDecay
                    End If

                    If datavalue(8) = 4 Then
                        State4decay = New PressureDecay
                    End If

                    enteringcycle = False

                Else ' In cycle

                    If datavalue(8) = 1 Then
                        State1decay.Detect(datavalue(1))
                        Lb_DecayMax1.Text = State1decay.PMaxSlope.ToString
                        Lb_DecayAvg1.Text = State1decay.PAvGslope.ToString
                    End If

                    If datavalue(8) = 4 Then
                        State4decay.Detect(datavalue(1))
                        Lb_DecayMax4.Text = State4decay.PMaxSlope.ToString
                        Lb_DecayAve4.Text = State4decay.PAvGslope.ToString
                    End If
                End If

            End If



            If datavalue(8) <> currentcycle Then ' Only update when needed
                currentcycle = datavalue(8)
                Lbl_CycleStage.Text = currentcycle

                If currentcycle = 2 Then
                    If Not State1decay Is Nothing Then
                        State1decay.Dispose()
                    End If
                End If
                    If currentcycle = 5 Then
                    If Not State4decay Is Nothing Then
                        State4decay.Dispose()
                    End If
                End If
                enteringcycle = True
            End If
                GraphIncoming(datavalue)             '' Add points to the chart
            'TextBox1.AppendText(IncomingData)

            If I_CLogging >= My.Settings.Log_Time_Step Then ' Test to see if we should call the logging routine
                DataLogging(datavalue)
                I_CLogging = 0
            End If


        End If


    End Sub

    Private Sub GraphIncoming(ByVal datatograph() As Int32)
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
        Dim command As String
        Dim builder As New System.Text.StringBuilder
        Dim datapacket As String
        Dim receivedstatus As Boolean

        Dim cyclescount As Integer
        receivedstatus = False
        TextBox1.Text = " "
        lbl_Returned_Times.Text = ""
        With Btn_UpdateCycleTime
            .Enabled = False
            .BackColor = SystemColors.ButtonFace
            .Visible = False

        End With
        cycles(0) = CInt(TB_ProcTime1.Text) / timerperiod
        cycles(1) = CInt(TB_ProcTIme2.Text) / timerperiod
        cycles(2) = CInt(TB_ProcTime3.Text) / timerperiod
        cycles(3) = CInt(TB_ProcTime4.Text) / timerperiod
        cycles(4) = CInt(TB_ProcTime5.Text) / timerperiod
        cycles(5) = CInt(TB_ProcTIme6.Text) / timerperiod
        cyclescount = 0
        command = ""

        For Each cycletime In cycles

            command = "PT" & cyclescount.ToString    ' Create Command code
            datapacket = Createpacket(command, cycletime)
            receivedstatus = SendData(datapacket) 'Send String
            If receivedstatus = False Then Exit For ' If transfer failed alert
            cyclescount = cyclescount + 1
            Dim updatelable() As Control
            updatelable = Controls.Find("Lbl_PTime" & cyclescount, True)
            If updatelable.Length > 0 Then
                updatelable(0).Text = (cycles(cyclescount - 1) * timerperiod)
            End If
        Next

        If receivedstatus = True Then
            command = "ACC"
            datapacket = Createpacket(command, 1000)
            receivedstatus = SendData(datapacket)
        Else
            Btn_UpdateCycleTime.BackColor = Color.Red
        End If

        With Btn_UpdateCycleTime
            .Enabled = True
            .Visible = True
        End With

    End Sub

    Private Sub Btn_Update_Graph_Click(sender As Object, e As EventArgs)
        Dim newtime As Integer

        newtime = CInt(TB_GraphDisplay.Text) * 100
        ' Log Time Step 
        ' Sample if we put in every 2 seconds.  Means that we are logging every 200th datapoint

        My.Settings.GraphLength = newtime
        My.Settings.Save()
        Chart1.ChartAreas(0).AxisX.Maximum = My.Settings.GraphLength

    End Sub

    Private Sub Btn_PT1UpdateCalH_Click(sender As Object, e As EventArgs) Handles Btn_PT1UpdateCalH.Click

    End Sub

    Private Sub TP_Calibration_Click(sender As Object, e As EventArgs) Handles TP_Calibration.Click

    End Sub



    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        'Dim updatedtimes As String
        'Dim builder As New System.Text.StringBuilder
        'Dim cycles As Integer
        'Dim checksum As Char
        'cycles = 20 ' CInt(TB_ProcTime4.Text) / timerperiod
        'Dim length As Int16
        'Dim receivedstatus As Boolean
        'length = 0
        'TextBox10.Text = ""
        'checksum = Chcksum(cycles, length)
        'Button8.Text = "SET"

        'builder.Append("#")
        'builder.Append("PT0")
        'builder.Append(length)

        'builder.Append(cycles.ToString)
        'builder.Append(checksum)
        'builder.Append("$")
        'updatedtimes = builder.ToString


        'receivedstatus = SendData(updatedtimes)
        'transfer procedure


        'Thread.Sleep(5)
        'If DataSent = commstatus.Ready Then
        '    Button8.Text = "Success"

        'Else
        '    Button8.Text = "Fail"

        'End If

    End Sub
    Function Chcksum(ByVal outgoingdata As Int32, ByRef Larray As Int16)
        ' Function returns an check sum
        ' Function also updates length by reference.

        Dim Stemp As String

        ' convert number to a string
        Stemp = outgoingdata.ToString()
        Dim sum As Byte = 0
        Dim Bmodule As Byte

        ' convert each character in string to a byte asci
        Dim calcaray() As Char = Stemp.ToCharArray
        ' calculate checksum
        ' convert each character into asci value
        Larray = Stemp.Length
        For Each Character In calcaray
            Dim temp As Byte = Convert.ToByte(Character)
            sum = (sum + temp) And &HFF
        Next
        Bmodule = sum Mod 128

        'Convert check sum into character to send
        Dim Creturn As Char = Chr(Bmodule) ' System.Text.Encoding.ASCII.GetChars(diff)
        Return Creturn




    End Function

    Private Sub Btn_LogFiles_Click(sender As Object, e As EventArgs) Handles Btn_LogFiles.Click
        caldata.SelectDataFolder()
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
    Public Sub Detectslope(ByVal pressure As Integer)

        Select Case Decay
            Case Sstate.peakdection

            Case Sstate.inslope

            Case Sstate.CompleteSlope




        End Select




    End Sub

End Class
