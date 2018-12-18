Imports System.IO.Ports
Imports System.Threading


Public Class Form1



    Private Delegate Sub accessformMarshaldelegate(ByVal texttodisplay As String)
    Public WithEvents Mycom As SerialPort

    Const timerperiod As Integer = 5 ' 5 millisecond time ISR period on arduino


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interop.No_Sleep()


        newcommport()
        RetrieveSettings()

        With Chart1
            .Series(0).Points.Clear()
            .Series(1).Points.Clear()
            .Series(2).Points.Clear()
            .ChartAreas(0).AxisY.Maximum = 1024
            .ChartAreas(0).AxisX.Maximum = My.Settings.GraphLengh

        End With

    End Sub

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
                .ReceivedBytesThreshold = 14 ' one byte short of a complete messsage string of 16 asci characters   
                .WriteTimeout = 500
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
        Dim length As Integer

        length = TextToDisplay.Length
        If (TextToDisplay(0) = "@") Then
            lbl_Returned_Times.Text = TextToDisplay
        Else

            ' you want to split this input string


            ' Split string based on comma
            Dim words As String() = TextToDisplay.Split(New Char() {","c})

            ' Use For Each loop over words and display them
            Dim word As String
            Dim datavalue(7) As Integer
            Dim i As Integer = 0

            For Each word In words
                Dim sucessess As Boolean = Int32.TryParse(word, datavalue(i))
                i = i + 1
            Next
            With Chart1
                .Series(0).Points.AddY(datavalue(0))
                .Series(1).Points.AddY(datavalue(1))
                .Series(2).Points.AddY(datavalue(2))
                If (.Series(0).Points.Count > My.Settings.GraphLengh) Then
                    .Series(0).Points.RemoveAt(0)
                    .Series(1).Points.RemoveAt(0)
                    .Series(2).Points.RemoveAt(0)
                End If
            End With

            '' Add points to the chart
            '  TextBox1.AppendText(TextToDisplay)
        End If
    End Sub



    Private Sub Form1_FormCLosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Portclosing()
        Interop.GOTOSLEEP()
    End Sub
End Class
