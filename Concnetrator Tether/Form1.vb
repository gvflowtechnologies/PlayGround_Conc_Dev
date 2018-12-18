Imports System.IO.Ports
Imports System.Threading


Public Class Form1



    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Public WithEvents mycom As SerialPort

    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()



    End Sub



    Private Sub Form1_FOrmCLosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Interop.GOTOSLEEP()
    End Sub
End Class
