Public Class PressureDecay
    Enum Sstate
        peakdection
        inslope
        CompleteSlope
    End Enum

    Private Slopestate As Sstate

    Dim SlopePeak As Integer

    Dim SlopeMax As Integer
    Dim SlopeAvg As Integer
    Dim ICount As Integer 'number of times count has come

    Public Sub New()

        Slopestate = Sstate.peakdection
        SlopePeak = 0

        SlopeMax = 0
        SlopeAvg = 0
        ICount = 0

    End Sub

    Public Sub Detect(ByVal pressure As Integer)
        Dim Current As Integer

        Current = pressure
        ICount += 1
        Select Case Slopestate
            Case Sstate.peakdection

                If Current > SlopePeak Then
                    SlopePeak = Current
                    ICount = 0
                End If
                If ICount > 10 Then
                    ICount = 0
                    Slopestate = Sstate.inslope
                End If

            Case Sstate.inslope




        End Select
    End Sub
    Public Sub Dispose()
        Me.Finalize()
    End Sub


End Class
