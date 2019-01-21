Public Class PressureDecay
    Enum Sstate
        peakdection
        inslope
        CompleteSlope
    End Enum

    Private Slopestate As Sstate
    Private Const IcPeriod As Single = 0.01 ' Timeperiod between Samples
    Dim PressureMax As Integer

    Dim SlopeMax As Integer
    Dim SlopeAvg As Integer
    Dim Pressuremin As Integer
    Dim ICount As Integer 'number of times count has come
    Dim runningpressure(3) As Integer
    Dim Imincount As Integer ' minnimum detector

    Public Sub New()

        Slopestate = Sstate.peakdection
        PressureMax = 0
        SlopeMax = 0
        SlopeAvg = 0
        ICount = 0
        Imincount = 0

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

                If Current > PressureMax Then
                    PressureMax = Current
                    ICount = 0
                End If
                If ICount > 10 Then
                    Slopestate = Sstate.inslope
                    Pressuremin = PressureMax
                    Imincount = 0
                End If

            Case Sstate.inslope
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
                If Imincount > 5 Then
                    Slopestate = Sstate.CompleteSlope

                End If


            Case Sstate.CompleteSlope






        End Select

    End Sub
    Public Sub Dispose()
        Me.Finalize()
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
    Public ReadOnly Property PMaxSlope As Single

        Get
            Dim Slopereturn As Single

            If Slopestate <> Sstate.peakdection Then
                Slopereturn = SlopeMax
            Else
                Slopereturn = 0
            End If

            Return Slopereturn
        End Get
    End Property




End Class
