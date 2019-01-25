Public Class PressureDecay
    Enum Sstate
        peakdection
        inslope
        CompleteSlope
    End Enum

    Private Slopestate As Sstate
    Private Const IcPeriod As Single = 0.01 ' Timeperiod between Samples
    Dim PressureMax As Integer
    Dim BStateEnter As Boolean
    Dim SlopeMax As Integer
    Dim SlopeAvg As Integer
    Dim Pressuremin As Integer
    Dim ICount As Integer 'number of times count has come
    Dim runningpressure(3) As Integer
    Dim Imincount As Integer ' minnimum detector
    Private LastSlopes(2) As Integer
    Private MvgAvgSlope As Single
    Private slopecount As Integer

    Public Sub New()
        slopecount = 0
        For Each Islope In LastSlopes
            Islope = 0
        Next
        Reset()
        'Slopestate = Sstate.peakdection
        'PressureMax = 0
        'SlopeMax = 0
        'SlopeAvg = 0
        'ICount = 0
        'Imincount = 0

    End Sub

    Public Sub Detect(ByVal pressure As Integer)
        Dim Current As Integer
        Dim time As Single

        Current = pressure
        ICount += 1
        Imincount += 1
        time = ICount * IcPeriod
        Select Case Slopestate
            Case Sstate.peakdection
                If BStateEnter Then
                    BStateEnter = False

                End If
                If Current > PressureMax Then
                    PressureMax = Current
                    ICount = 0
                End If
                If ICount > 10 Then
                    Slopestate = Sstate.inslope
                    BStateEnter = True
                    Pressuremin = PressureMax
                    Imincount = 0
                End If

            Case Sstate.inslope
                If BStateEnter Then
                    BStateEnter = False

                End If
                Dim tempslope As Single
                tempslope = (PressureMax - Current) / time
                SlopeAvg = tempslope
                If tempslope > SlopeMax Then
                    SlopeMax = tempslope
                End If

                'Min detector
                If Current < Pressuremin Then
                    Pressuremin = Current
                    Imincount = 0
                End If

                If Imincount > 5 And time > 0.5 Then

                    tempslope = (PressureMax - Current) / (time - (Imincount * IcPeriod))
                    SlopeAvg = tempslope
                    Slopestate = Sstate.CompleteSlope
                    BStateEnter = True

                End If

            Case Sstate.CompleteSlope
                If BStateEnter Then
                    BStateEnter = False
                    MvgAvgSlope = 0
                    'Add to array
                    LastSlopes(slopecount) = SlopeAvg
                    'calc average

                    For Each islope In LastSlopes
                        MvgAvgSlope += CSng(islope)
                    Next
                    MvgAvgSlope = MvgAvgSlope / CSng(LastSlopes.Length)

                    'Increment count

                    If slopecount < 2 Then
                        slopecount += 1
                    Else
                        slopecount = 0
                    End If

                End If

        End Select

    End Sub
    Public Sub Dispose()
        Me.Finalize()
    End Sub

    Public Sub Reset()
        Slopestate = Sstate.peakdection
        PressureMax = 0
        SlopeMax = 0
        SlopeAvg = 0
        ICount = 0
        Imincount = 0
        BStateEnter = True
    End Sub

    Public ReadOnly Property PAvGslope As Single
        Get
            Dim Slopereturn As Single

            If Slopestate <> Sstate.peakdection Then
                Slopereturn = SlopeAvg
            Else
                Slopereturn = 0
            End If

            Return Slopereturn
        End Get
    End Property
    Public ReadOnly Property PMvgAvgSlope As Single

        Get
            Dim Slopereturn As Single

            If Slopestate <> Sstate.peakdection Then
                Slopereturn = MvgAvgSlope
            Else
                Slopereturn = 0
            End If

            Return Slopereturn
        End Get
    End Property




End Class
