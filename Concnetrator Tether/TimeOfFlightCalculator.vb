﻿Option Explicit On

Imports System.IO.Ports
Imports System.Threading
Imports System.Text
Imports System.Timers
Imports System.Diagnostics



Public Class TimeOfFlightCalculator
    Public WithEvents SP_Internal As SerialPort
    Private mystop As Stopwatch
    Private TimeUpstream(5) As Integer
    Private ClockCountUp(5) As Integer
    Private UpCalibration(2) As Integer


    Private Cal_Temp As Single ' Temperature recorded during calibration
    Private Cal_Tup As Single ' Time Upstreaem recorded during calibration
    Private Cal_Tdwn As Single ' TIme difference between up and down at zero flow during calibration.
    'Always calibrating at 100% oxygen.

    Private _TOFUPs(3) As Single
    Private _TOFDNs(3) As Single
    Private _TempTOF(4) As Single

    Private _SendReadings As Boolean
    Private _FilteredTemp As Single
    Private _Temperature As Single
    Private _Flow As Single
    Private _Concentration As Single
    Private _TOF_AVG As Single ' Average time of flight used to calculate oxygen concnetration.
    Private _TOF_UP_Minus_Down
    Private _Measurement_Finished As Boolean
    Private _TimeRequired As Single

    Private Firsttemp As Boolean ' When starting seed the filter with the current temperature.
    Private DataFLagTD_7200 As TD_7200_Values
    Const ClockFrequency As Single = 10 * 10 ^ 6
    Const RTempCoef As Single = 3.85
    Const R_Ref As Single = 1000
    Const E_Filter_Constant As Single = 0.022 'At a time step of 0.25 has a 11 sec time constant and -3db frequency response at (1 Min Freq) 0.016 hz
    Const CalUpwindow As Single = 3 ' number of microseconds we should be looking up.
    Const Caldnwindow As Single = 19 ' number of microseconds we should be looking down.
    Const Flow_Window_Max As Single = 10 'Number of microseconds we should be looking up.
    Const Flow_Window_Min As Single = 10 'Number of microseconds we should be looking down.
    Const o2concslope As Single = 5.0 ' was 4.6 Concentration Slope
    Const o2cono2cal As Single = 100 'Concentration Cal % or should it be 94
    Const Waves_Delayed As Short = 4 ' Number of waves between theoretical transit time and measured Transit Time. 

    'Private Serial_Recieve_Status As Receiving_State

    Public Property ReceiveStatus As MeasurementStatus = MeasurementStatus.S_Waiting
    Private ReceivingData As Receiving_State

    Enum Receiving_State ' Inside receiving loop.  For every command we wait for flag telling us to go to next step.
        R_Wating
        R_Received
    End Enum

    Enum TD_7200_Values ' Outside receiving loop.  For every High level command cycles through the following 4 command cycles.
        S_Command ' Wating for echo indicating that command string was received.
        S_UpStream ' Read upstream Buffers
        S_DownStream ' Read downstream Buffers
        S_RTD ' Read RTD.
    End Enum

    Enum MeasurementStatus ' For trackign outside the object.  
        S_Waiting
        S_Inprocess
        S_Complete
    End Enum

    Public Sub New(ByVal CalTimeUP As Single, ByVal CalTimedown As Single, ByVal CalTemp As Single, ByVal Cal_o2percent As Single)
        NewO2Comm()
        Interop.No_Sleep()
        ReceivingData = Receiving_State.R_Received
        Cal_Temp = CalTemp
        Cal_Tup = CalTimeUP
        Cal_Tdwn = CalTimedown
        mystop = New Stopwatch()
        Firsttemp = False
        _Measurement_Finished = True
        _SendReadings = False



    End Sub

    Public Sub dispose()
        Portclosing()
    End Sub



#Region "SerialPort Controls"
    Private Sub NewO2Comm()

        Dim myportnames() As String
        myportnames = SerialPort.GetPortNames
        If IsNothing(SP_Internal) Then
            SP_Internal = New SerialPort


            AddHandler SP_Internal.DataReceived, AddressOf SP_Internal_Datareceived ' handler for data received event

            With SP_Internal
                .PortName = "COM10" ' gets port name from static data set
                .BaudRate = 19200
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None  ' Need to think here
                .DataBits = 8
                .ReceivedBytesThreshold = 3 ' one byte short o
                .WriteTimeout = 100
                .ReadTimeout = 4000
                .ReadBufferSize = 200
                .WriteBufferSize = 500
            End With
        End If
        If (Not SP_Internal.IsOpen) Then
            Try
                SP_Internal.Open()
                SP_Internal.DiscardInBuffer()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
        ' Give Command to Stop Sending Data
        Dim BPacketToSend(1) As Byte
        BPacketToSend(0) = &H1B
        SP_Internal.Write(BPacketToSend, 0, 1)
        SP_Internal.DiscardInBuffer()

    End Sub

    Private Sub Portclosing()
        If IsNothing(SP_Internal) Then Exit Sub
        If SP_Internal.IsOpen = True Then
            SP_Internal.ReceivedBytesThreshold = 1500
            '  Thread.Sleep(1)
            Do Until SP_Internal.BytesToRead < 1
                'Application.DoEvents()
                SP_Internal.DiscardInBuffer()
            Loop
            SP_Internal.DtrEnable = False
            SP_Internal.Close()
            Do Until SP_Internal.IsOpen = False
                Application.DoEvents()
                '   Thread.Sleep(1)
            Loop
        End If
    End Sub

    Public Sub Send_Binary_Data(ByVal Packet() As Byte, datadesired As TD_7200_Values)

        DataFLagTD_7200 = datadesired
        ' SP_Internal.DiscardInBuffer()
        SP_Internal.Write(Packet, 0, Packet.Length)

    End Sub

    Private Sub SP_Internal_Datareceived(ByVal sendor As Object, ByVal e As SerialDataReceivedEventArgs) Handles SP_Internal.DataReceived
        ' Handles data on a backgrouund thread when it comes in on serial port.

        Thread.Sleep(2)
        Dim Bte_Incoming() As Byte
        Dim Byt_Count As Integer
        Byt_Count = 3 'SP_Internal.BytesToRead - 1
        Dim countup As Integer = 0
        '  Byt_Count = SP_Internal.BytesToRead - 1
        Dim ValidPacketStart As Boolean
        ReDim Bte_Incoming(Byt_Count)

        ' If SP_Internal.ReadByte = 0 Then Exit Sub
        ValidPacketStart = True

        Do Until countup > Byt_Count
            'If SP_Internal.ReadByte = 0 Then Exit Sub
            '   If SP_Internal.BytesToRead = 0 Then Exit Sub


            Try

                Bte_Incoming(countup) = SP_Internal.ReadByte

            Catch ex As Exception

                '   MessageBox.Show(ex.Message)
                MessageBox.Show("No data to receive_ERROR_GREG " & countup.ToString)

            End Try

            If Bte_Incoming.Length = 0 Then
                ValidPacketStart = False
                Thread.Sleep(5)
                SP_Internal.DiscardInBuffer()
                Exit Do
            End If

            If Bte_Incoming(0) <> 6 Then
                ValidPacketStart = False
                Thread.Sleep(5)
                SP_Internal.DiscardInBuffer()
                Exit Do
            End If

            If countup = 1 Then
                Byt_Count = Bte_Incoming(1) - 1
                ReDim Preserve Bte_Incoming(Byt_Count)
            End If
            countup += 1
        Loop

        TOF_ConvertfromBYTE(Bte_Incoming, ValidPacketStart)

        _Measurement_Finished = True

    End Sub


#End Region

    Private Sub TOF_ConvertfromBYTE(ByVal TextToDisplay() As Byte, ByVal ValidPacketStrt As Boolean) 'Parses Byte Array into array of values for computing times.
        Static Found_SOH As Boolean = False
        Static InputStringSize As Integer = 0
        Dim ByteToInt(2) As Byte

        If Not ValidPacketStrt Then
            ReceivingData = Receiving_State.R_Received
            Exit Sub
        End If

        If TextToDisplay.Length < 2 Then
            ReceivingData = Receiving_State.R_Received
            Exit Sub
        End If

        Dim CmDValue As Integer = CInt(TextToDisplay(2))

        Select Case CmDValue

            Case = 7
                DataFLagTD_7200 = TD_7200_Values.S_Command
            Case = 4
                DataFLagTD_7200 = TD_7200_Values.S_UpStream
            Case = 5
                DataFLagTD_7200 = TD_7200_Values.S_DownStream
            Case = 6
                DataFLagTD_7200 = TD_7200_Values.S_RTD
            Case Else
                ReceivingData = Receiving_State.R_Received
                Exit Sub

        End Select


        Select Case DataFLagTD_7200

            Case TD_7200_Values.S_Command
                ReceivingData = Receiving_State.R_Received 'We have received the incoming data

            Case 1 To 2
                If TextToDisplay.Length < (38 + (3 * 1)) Then Exit Sub
                For i = 0 To 4 ' Count Times
                    ByteToInt(0) = TextToDisplay((6 * i) + 3)
                    ByteToInt(1) = TextToDisplay((6 * i) + 4)
                    ByteToInt(2) = TextToDisplay((6 * i) + 5)

                    TimeUpstream(i) = BuildInteger(ByteToInt)

                Next

                For i = 0 To 4 ' Count Clock Counts

                    ByteToInt(0) = TextToDisplay((6 * i) + 6)
                    ByteToInt(1) = TextToDisplay((6 * i) + 7)
                    ByteToInt(2) = TextToDisplay((6 * i) + 8)
                    ClockCountUp(i) = BuildInteger(ByteToInt)

                Next

                For i = 0 To 1 ' Get Calibration 2 and 1

                    ByteToInt(0) = TextToDisplay((3 * i) + 36)
                    ByteToInt(1) = TextToDisplay((3 * i) + 37)
                    ByteToInt(2) = TextToDisplay((3 * i) + 38)
                    UpCalibration(i) = BuildInteger(ByteToInt)

                Next

                If DataFLagTD_7200 = TD_7200_Values.S_UpStream Then
                    For i = 0 To 2
                        _TOFUPs(i) = Time_of_Flight(UpCalibration, TimeUpstream, ClockCountUp, i + 1)
                    Next
                End If

                If DataFLagTD_7200 = TD_7200_Values.S_DownStream Then
                    For i = 0 To 2
                        _TOFDNs(i) = Time_of_Flight(UpCalibration, TimeUpstream, ClockCountUp, i + 1)
                    Next
                End If
                ReceivingData = Receiving_State.R_Received 'We have received the inco

            Case TD_7200_Values.S_RTD
                If TextToDisplay.Length < (38 + (3 * 1)) Then Exit Sub


                For i = 0 To 4 ' Count Times

                    ByteToInt(0) = TextToDisplay((6 * i) + 3)
                    ByteToInt(1) = TextToDisplay((6 * i) + 4)
                    ByteToInt(2) = TextToDisplay((6 * i) + 5)

                    TimeUpstream(i) = BuildInteger(ByteToInt)

                Next

                For i = 0 To 4 ' Count Clock Counts

                    ByteToInt(0) = TextToDisplay((6 * i) + 6)
                    ByteToInt(1) = TextToDisplay((6 * i) + 7)
                    ByteToInt(2) = TextToDisplay((6 * i) + 8)
                    ClockCountUp(i) = BuildInteger(ByteToInt)

                Next


                For i = 0 To 1 ' Get Calibration 2 and 1

                    ByteToInt(0) = TextToDisplay((3 * i) + 36)
                    ByteToInt(1) = TextToDisplay((3 * i) + 37)
                    ByteToInt(2) = TextToDisplay((3 * i) + 38)
                    UpCalibration(i) = BuildInteger(ByteToInt)

                Next
                For i = 0 To 4
                    _TempTOF(i) = Time_of_Flight(UpCalibration, TimeUpstream, ClockCountUp, i + 1)
                Next

                Calc_Temp(_TempTOF) ' Convet times into temperature.

                Calc_oxygenadndFLow(_TOFUPs, _TOFDNs, _FilteredTemp) 'Convert the times to oxygen and flow correcting for Temperature




                Thread.Sleep(1)
                ' ReceivingData = Receiving_State.R_Received 'We have received the incomin Temperature Data


                ReceivingData = Receiving_State.R_Received 'We have re

        End Select

    End Sub
#Region "Temperature Calculation and FIltering"
    Private Sub Calc_Temp(ByVal temperature_tof() As Single)
        'Calculates the current temperature
        Dim Resistance_Delta As Single
        Dim RTD_Value As Single
        Dim RTD_TimeofFlight As Single

        RTD_TimeofFlight = temperature_tof(2) - temperature_tof(1)

        If RTD_TimeofFlight < 20 Then
            RTD_TimeofFlight = temperature_tof(3) - temperature_tof(1)
        End If


        RTD_Value = 1000 * (temperature_tof(0) / RTD_TimeofFlight)

        Resistance_Delta = RTD_Value - R_Ref

        _Temperature = Resistance_Delta / RTempCoef
        _FilteredTemp = Filter_Temp(_Temperature)

    End Sub

    Private Function Filter_Temp(ByVal CurrentTemp As Single) As Single
        'Filters the curretn temperature
        Static Filtered_Temp As Single
        If Not Firsttemp Then ' First time through set filtered temp as current temp.
            Filtered_Temp = CurrentTemp
            Firsttemp = True
        End If

        ' Test to throw out crazytown numbers.
        If CurrentTemp - Filtered_Temp > 10 Then
            CurrentTemp = Filtered_Temp + 10
        End If
        If CurrentTemp - Filtered_Temp < -10 Then
            CurrentTemp = Filtered_Temp - 10
        End If

        Filtered_Temp = (CurrentTemp * E_Filter_Constant) + (Filtered_Temp * (1 - E_Filter_Constant))

        Return Filtered_Temp
    End Function

    ' **** Need to make this generic so up and down can be calculated.

    Private Function TOF_Cal_TempCorrected(ByVal TOF_UporDn As Single, ByVal Temp_Correction_Ratio As Single, ByVal WavePeriod As Single) As Single
        'Corrects the time of flight during calibration to the time of flight at current temperature at 100% oxygen.

        Dim TempCorrected_TOF_at_Cal As Single
        TOF_UporDn -= Waves_Delayed * WavePeriod

        TOF_UporDn *= Temp_Correction_Ratio

        TempCorrected_TOF_at_Cal = TOF_UporDn + 4 * WavePeriod


        Return TempCorrected_TOF_at_Cal 'TempCorrected_TOF_at_Cal

    End Function

    Private Function Tof_Temp_Ratio(ByVal TempMeasures As Single) As Single
        'Approximates the square root of the absolute temperature range
        Dim TempRatio As Single
        Dim TempRatioSqrt As Single
        TempRatio = (TempMeasures + 273) / (Cal_Temp + 273) '(TempMeasures + 273) / (Cal_Temp + 273) ' R

        TempRatioSqrt = 1 'Seed Guess.

        'Two iterations of square root get you quiet close.
        TempRatioSqrt = 0.5 * (TempRatioSqrt + (TempRatio / TempRatioSqrt))
        TempRatioSqrt = 0.5 * (TempRatioSqrt + (TempRatio / TempRatioSqrt))

        Return TempRatioSqrt

    End Function

    Private Sub Correct_TOF_to_CalTemp(ByRef TimeUP() As Single, ByRef TimeDn() As Single, ByVal Temperature As Single)

        Dim TimeUP_WavePeriod As Single
        Dim TimeDN_WavePeriod As Single
        Dim WaveCOunter As Integer
        Dim TemperatureCorrectionRatio As Single

        ' 1. Calculate Lamda

        TimeUP_WavePeriod = CalcWavePeriod(TimeUP)
        TimeDN_WavePeriod = CalcWavePeriod(TimeDn)

        ' Calculate Temperature Ratio
        TemperatureCorrectionRatio = Tof_Temp_Ratio(Temperature)

        WaveCOunter = 0
        ' Subtract 4 lambda,  Convert TOF to Calibration Temperature, Add 4 Lambda
        For Each WaveDetected In TimeUP


            WaveDetected = TOF_Cal_TempCorrected(WaveDetected, TemperatureCorrectionRatio, TimeUP_WavePeriod)
            TimeUP(WaveCOunter) = WaveDetected
            WaveCOunter += 1

        Next

        WaveCOunter = 0
        For Each WaveDetected In TimeDn

            WaveDetected = TOF_Cal_TempCorrected(WaveDetected, TemperatureCorrectionRatio, TimeDN_WavePeriod)
            TimeDn(WaveCOunter) = WaveDetected
            WaveCOunter += 1

        Next

    End Sub

    Private Function CalcWavePeriod(Times() As Single) As Single

        'Determine the time difference between first wave and second wave (time between waves)
        'for time up and Time down.
        'Range check the time between waves should be between 23 and 27 micro seconds
        Dim WaveCount As Int16
        Dim WavePeriod As Single
        WaveCount = 0
        Do

            WavePeriod = Times(WaveCount + 1) - Times(WaveCount)
            WaveCount += 1

        Loop Until (WaveCount > 2 Or (WavePeriod > 23 And WavePeriod < 27))

        Return WavePeriod

    End Function

    Private Function FirstGoodWave(Times() As Single) As Single
        Dim WaveCount As Int16
        Dim WavePeriod As Single
        Dim TOF_Baseline As Single

        WaveCount = 0
        Do
            WavePeriod = Times(WaveCount + 1) - Times(WaveCount)
            TOF_Baseline = Times(WaveCount)
            WaveCount += 1

        Loop Until (WaveCount > 2 Or (WavePeriod > 23 And WavePeriod < 27))

        Return TOF_Baseline

    End Function



#End Region

    Private Sub Calc_oxygenadndFLow(ByVal TimeUp() As Single, TimeDn() As Single, Temp As Single)
        'Array of times pulled in with time up, time down, and temp.


        Dim TOF_AVG_PureO2_Calibrated As Single
        Dim TimeUP_WavePeriod As Single
        Dim TimeDN_WavePeriod As Single

        'Times

        Dim TimeUP_MaxTime As Single
        Dim TimeUp_MinTime As Single

        Dim TOF_UP As Single
        Dim TOF_DN As Single
        Dim TOF_UP_BaseLine As Single ' First solid zero crossing received during operation
        Dim TOF_DN_Baseline As Single 'First Solid zero crossing received during operation

        Dim Concentration_Trial As Single

        'Correcting the current time of flight to what the current TOF would be at the calibration temperature.
        Correct_TOF_to_CalTemp(TimeUp, TimeDn, Temp)
        ' TOF is now at Calibration Temperature     


        'Find the first wave at times corrected to calibration Temperature
        TOF_UP_BaseLine = FirstGoodWave(TimeUp)
        TOF_DN_Baseline = FirstGoodWave(TimeDn)

        'Calculate WavePeriod
        TimeUP_WavePeriod = CalcWavePeriod(TimeUp)
        TimeDN_WavePeriod = CalcWavePeriod(TimeDn)

        ' Calculate Max and Min Windows for Time of FLight based on 100% oxygen.
        TOF_AVG_PureO2_Calibrated = (Cal_Tup + Cal_Tdwn) / 2
        TimeUP_MaxTime = TOF_AVG_PureO2_Calibrated + CalUpwindow
        TimeUp_MinTime = TOF_AVG_PureO2_Calibrated - Caldnwindow


        ' Pick wave that falls into the window established during calibration.
        For WaveCount = 4 To -4 Step -1
            TOF_UP = TOF_UP_BaseLine + WaveCount * TimeUP_WavePeriod
            If TOF_UP > TimeUp_MinTime And TOF_UP < TimeUP_MaxTime Then
                Exit For
            End If
        Next WaveCount

        'Select Matching Wave from Down stream sensor to the up stream sensor..
        'Working Here now
        For WaveCount = 4 To -4 Step -1
            TOF_DN = TOF_DN_Baseline + WaveCount * TimeDN_WavePeriod
            If TOF_DN > (TOF_UP - Flow_Window_Min) And TOF_DN < (TOF_UP + Flow_Window_Max) Then
                Exit For
            End If
        Next WaveCount

        ' Calcutlate Average TOF

        _TOF_AVG = (TOF_UP + TOF_DN) / 2

        _TOF_UP_Minus_Down = TOF_UP - TOF_DN

        Concentration_Trial = (_TOF_AVG - TOF_AVG_PureO2_Calibrated) * o2concslope + o2cono2cal
        If Single.IsNaN(Concentration_Trial) Then

            _Concentration = _Concentration
        Else
            _Concentration = Concentration_Trial
        End If

    End Sub

    Private Function Calc_Avg(ByVal Timeup() As Single, TimeDn() As Single) As Single
        ' Select correct time up based on cal and temp.
        Dim AverageTime As Single


        Return AverageTime
    End Function


    Public Sub PerformMeasurement()

        _Measurement_Finished = False
        'Set Receiving to waiting and send string with the initial command for reading.
        ReceivingData = Receiving_State.R_Wating

        'Send Command
        Dim datatosend = New Byte() {&H1, &H6, &H7, &H4, &H1, &H53}
        DataFLagTD_7200 = TD_7200_Values.S_Command
        Send_Binary_Data(datatosend, TD_7200_Values.S_Command)

        Do Until ReceivingData = Receiving_State.R_Received
            Application.DoEvents()
            Thread.Sleep(1)
        Loop
        'Read Upstream Buffer
        ReceivingData = Receiving_State.R_Wating
        DataFLagTD_7200 = TD_7200_Values.S_UpStream
        datatosend = {&H1, &H4, &H4, &H23}
        Send_Binary_Data(datatosend, TD_7200_Values.S_UpStream)

        Do Until ReceivingData = Receiving_State.R_Received
            Application.DoEvents()
            Thread.Sleep(1)
        Loop

        'Read Downstream Buffer
        ReceivingData = Receiving_State.R_Wating
        DataFLagTD_7200 = TD_7200_Values.S_DownStream
        datatosend = {&H1, &H4, &H5, &H24}
        Send_Binary_Data(datatosend, TD_7200_Values.S_DownStream)

        Do Until ReceivingData = Receiving_State.R_Received
            Application.DoEvents()
            Thread.Sleep(1)
        Loop

        ReceivingData = Receiving_State.R_Wating
        DataFLagTD_7200 = TD_7200_Values.S_RTD
        datatosend = {&H1, &H4, &H6, &H2D}
        Send_Binary_Data(datatosend, TD_7200_Values.S_RTD)

        Do Until ReceivingData = Receiving_State.R_Received
            Application.DoEvents()
            Thread.Sleep(1)
        Loop

    End Sub

    Private Sub StartMeasurement()
        Do While (_SendReadings = True)
            PerformMeasurement()
            Application.DoEvents()
            Thread.Sleep(2)
        Loop
    End Sub


    Private Function Time_of_Flight(ByVal calibration() As Integer, ByVal Time() As Integer, ByVal ClockCount() As Integer, ByRef Period_Num As Integer) As Single
        Dim calCount As Single
        Dim normLSB As Single
        Dim TimeOfFlight As Single

        Dim ClkPeriod As Single

        ClkPeriod = 1 / ClockFrequency

        calCount = (CSng(calibration(1)) - CSng(calibration(0))) / 9

        normLSB = ClkPeriod / calCount

        TimeOfFlight = (CSng(Time(0)) * normLSB) + (CSng(ClockCount(Period_Num - 1)) * ClkPeriod) - (CSng(Time(Period_Num)) * normLSB)

        TimeOfFlight = TimeOfFlight * 1000 * 1000


        Return TimeOfFlight

    End Function

    Private Function BuildInteger(ByVal ByInBytes As Byte()) As Integer

        'Bring in Byte Array and Return Integer
        Dim IntTime As Integer = ByInBytes(0)
        IntTime <<= 8
        IntTime = IntTime Or ByInBytes(1)
        IntTime <<= 8
        IntTime = IntTime Or ByInBytes(2)

        Return IntTime

    End Function
#Region "TOF Calculator Properties"
    ReadOnly Property Time_Upstream(i As Integer) As Single

        Get
            Return _TOFUPs(i)
        End Get

    End Property

    ReadOnly Property Time_Downstream(i As Integer) As Single
        Get
            Return _TOFDNs(i)
        End Get
    End Property

    ReadOnly Property Temperature As Single
        Get
            Return _FilteredTemp
        End Get
    End Property

    ReadOnly Property TOF_AVG_TempCorrected As Single
        Get
            Return _TOF_AVG
        End Get
    End Property

    ReadOnly Property TOF_UP_Minus_Dwn As Single
        Get
            Return _TOF_UP_Minus_Down
        End Get
    End Property

    ReadOnly Property O2_Percent As Single
        Get
            Return _Concentration
        End Get
    End Property

    ReadOnly Property Measurement_Complete As Boolean
        Get
            Return _Measurement_Finished
        End Get
    End Property

    Property SendReadings As Boolean
        Get
            Return _SendReadings
        End Get
        Set(ByVal active As Boolean)
            _SendReadings = active
            If active Then StartMeasurement()

        End Set
    End Property

    WriteOnly Property CAL_TIME_UP As Single
        Set(ByVal current As Single)
            Cal_Tup = current
        End Set
    End Property

    WriteOnly Property CAL_TIME_Dn As Single
        Set(ByVal current As Single)
            Cal_Tdwn = current
        End Set
    End Property

    WriteOnly Property CAL_Temperature As Single
        Set(ByVal current As Single)
            Cal_Temp = current
        End Set
    End Property
#End Region

End Class
