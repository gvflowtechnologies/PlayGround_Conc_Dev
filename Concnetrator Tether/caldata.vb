Option Explicit On
Imports System.IO

Module caldata
    ' Calibration data file and writing 

    Public cancelclicked As Boolean
    Dim swlogcaldata As StreamWriter


    Public Sub WritecalfileHeader()
        'Create File Name

        'Dim Logfile As String
        'If My.Settings.Caldirectory = "" Then
        '    My.Settings.Caldirectory = "c:\CalDirectory"
        '    My.Settings.Save()
        'End If

        'Logfile = My.Settings.Caldirectory & "\AVCalRecord.csv"


        ''Write

        'If Not Directory.Exists(My.Settings.Caldirectory) Then
        '    Directory.CreateDirectory(My.Settings.Caldirectory)
        'End If

        'If Not File.Exists(Logfile) Then

        '    swlogcaldata = New StreamWriter(Logfile, False)
        '    swlogcaldata.WriteLine("Altaviz Calibration Record File")
        '    swlogcaldata.WriteLine("Last Cal Date, Cal Weight ID Used, Scale Reading as Received, Scale Reading as Returned, Calibration Due Date, Operator ID")

        'Else
        '    swlogcaldata = New StreamWriter(Logfile, True)

        'End If

    End Sub

    Public Sub Writecalrecord(ByVal calid As String, ByVal asreceived As String, ByVal asreturned As String, ByVal opid As String)

        ''date time, cal weight ID#, before, after, new due date, operator ID
        'WritecalfileHeader()

        'swlogcaldata.Write(DateTime.Now.ToString & ", ")
        'swlogcaldata.Write(calid.ToString & ", ")
        'swlogcaldata.Write(asreceived & ", ")
        'swlogcaldata.Write(asreturned & ", ")
        'swlogcaldata.Write(My.Settings.LastCalDate.AddMonths(My.Settings.CalFrequency).ToString("d") & ", ") ' Next CalDate
        'swlogcaldata.WriteLine(opid)
        'swlogcaldata.Close()


    End Sub

    Sub selectcalfolder()
        ' Selects, creates if necessary, and saves folder location for calibration data.

        'Dim calfolder As String = ""


        'calfolder = My.Settings.Caldirectory

        'Dim fd = New FolderBrowserDialog
        'Dim result As DialogResult
        'result = DialogResult.Abort
        'If calfolder = "" Then
        '    fd.SelectedPath = "C:\"
        'Else
        '    If Directory.Exists(calfolder) Then

        '        fd.SelectedPath = calfolder
        '    Else
        '        fd.SelectedPath = "C:\"
        '    End If

        'End If

        'Do Until result = DialogResult.OK
        '    result = fd.ShowDialog()
        '    If result = DialogResult.Cancel Then Exit Sub
        'Loop



        'My.Settings.Caldirectory = fd.SelectedPath
        'calfolder = My.Settings.Caldirectory
        'My.Settings.Save()
        'Manual_Weight.Lbl_CalFolder.Text = calfolder

        'If Not Directory.Exists(calfolder) Then
        '    Directory.CreateDirectory(calfolder)
        'End If




        'fd.Dispose()


    End Sub

    Sub SelectDataFolder()
        Dim DataFolder As String = ""
        Dim scompleteddata As String = ""
        Dim sfweigtdata As String = ""
        Dim sfarchive As String = ""
        DataFolder = My.Settings.File_Directory

        Dim fd = New FolderBrowserDialog
        Dim result As DialogResult
        result = DialogResult.Abort
        If DataFolder = "" Then
            fd.SelectedPath = "C:\"
        Else
            If Directory.Exists(DataFolder) Then
                fd.SelectedPath = DataFolder
            Else
                fd.SelectedPath = "C:\"
            End If

        End If

        Do Until result = DialogResult.OK

            result = fd.ShowDialog()
            If result = DialogResult.Cancel Then Exit Sub
        Loop


        My.Settings.File_Directory = fd.SelectedPath
        DataFolder = My.Settings.File_Directory
        My.Settings.Save()

        If Not Directory.Exists(DataFolder) Then
            Directory.CreateDirectory(DataFolder)
        End If

        'scompleteddata = DataFolder & "\Completed"
        'sfweigtdata = DataFolder & "\In Process"
        'sfarchive = DataFolder & "\Archived"

        'If Not Directory.Exists(sfweigtdata) Then
        '    Directory.CreateDirectory(sfweigtdata)
        'End If
        'If Not Directory.Exists(scompleteddata) Then
        '    Directory.CreateDirectory(scompleteddata)
        'End If
        'If Not Directory.Exists(sfarchive) Then
        '    Directory.CreateDirectory(sfarchive)
        'End If



        fd.Dispose()


    End Sub
End Module
