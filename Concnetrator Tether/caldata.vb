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


    Sub ReturnFolder(ByRef Foldergettingsetup As Form1.foldertype)
        ' Creates a folder for either the Log Files of the script files.

        Dim DataFolder As String = ""
        Dim scompleteddata As String = ""
        Dim sfweigtdata As String = ""
        Dim sfarchive As String = ""

        ' Put data here based on setting.
        If Foldergettingsetup = Form1.foldertype.ScriptFile Then
            DataFolder = My.Settings.Dir_Script

        Else
            DataFolder = My.Settings.File_Directory

        End If

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

        If Foldergettingsetup = Form1.foldertype.ScriptFile Then
            My.Settings.Dir_Script = fd.SelectedPath
            DataFolder = My.Settings.Dir_Script

        Else

            My.Settings.File_Directory = fd.SelectedPath
            DataFolder = My.Settings.File_Directory

        End If

        My.Settings.Save()

        If Not Directory.Exists(DataFolder) Then
            Directory.CreateDirectory(DataFolder)
        End If


        fd.Dispose()

    End Sub

    Function ScriptFile() As String
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFileName As String = ""

        fd.Title = "Open File Dialog"
        fd.InitialDirectory = "C:\"
        fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            strFileName = fd.FileName
        End If

        Return strFileName

    End Function

End Module
