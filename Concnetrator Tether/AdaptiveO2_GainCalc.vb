Imports System.Threading
Public Class AdaptiveO2_GainCalc
    Dim GainFactor As Single
    Public Sub New(ByVal Gain As Single)
        GainFactor = Gain

    End Sub
    Public Function CalcAdjustment(O2_1 As Single, O2_4 As Single) As Single
        ' Execute the Background Task
        Dim Adjustment As Single
        Adjustment = GainFactor * (O2_1 - O2_4)
        Return Adjustment
    End Function

    WriteOnly Property Prop_GainFactor As Single

        Set(value As Single)
            GainFactor = value
        End Set
    End Property




End Class
