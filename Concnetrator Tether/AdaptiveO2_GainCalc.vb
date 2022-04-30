Imports System.Threading
Public Class AdaptiveO2_GainCalc
    Dim PGainFactor As Single
    Dim IGainFactor As Single
    Dim Summed_Int_Signal As Single
    Public Sub New(ByVal Gain As Single, IGain As Single)
        PGainFactor = Gain
        IGainFactor = IGain
        Summed_Int_Signal = 0
    End Sub
    Public Function PCalcAdjustment(O2_1 As Single, O2_4 As Single) As Single
        ' Execute the Background Task
        Dim Adjustment As Single
        Adjustment = PGainFactor * (O2_1 - O2_4)
        Return Adjustment
    End Function
    Public Function ICalcAdjustment(O2_1 As Single, O2_4 As Single) As Single
        ' Execute the Background Task
        Dim Adjustment As Single
        Adjustment = IGainFactor * (O2_1 - O2_4)
        Return Adjustment
    End Function


    WriteOnly Property Prop_GainFactor As Single

        Set(value As Single)
            PGainFactor = value
        End Set
    End Property

    WriteOnly Property Integral_GainFactor As Single

        Set(value As Single)
            IGainFactor = value
        End Set
    End Property



End Class
