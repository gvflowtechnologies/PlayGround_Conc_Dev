Option Explicit On
Imports System.IO

Module caldata
    ' Calibration data file and writing 

    Public cancelclicked As Boolean
    Dim swlogcaldata As StreamWriter





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
