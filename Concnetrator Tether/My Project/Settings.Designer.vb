﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("4000")>  _
        Public Property GraphLength() As Integer
            Get
                Return CType(Me("GraphLength"),Integer)
            End Get
            Set
                Me("GraphLength") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property PT1_Raw_Low() As Integer
            Get
                Return CType(Me("PT1_Raw_Low"),Integer)
            End Get
            Set
                Me("PT1_Raw_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property PT1_Raw_High() As Integer
            Get
                Return CType(Me("PT1_Raw_High"),Integer)
            End Get
            Set
                Me("PT1_Raw_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property PT2_Raw_Low() As Integer
            Get
                Return CType(Me("PT2_Raw_Low"),Integer)
            End Get
            Set
                Me("PT2_Raw_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property PT2_Raw_High() As Integer
            Get
                Return CType(Me("PT2_Raw_High"),Integer)
            End Get
            Set
                Me("PT2_Raw_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property PT3_Raw_Low() As Integer
            Get
                Return CType(Me("PT3_Raw_Low"),Integer)
            End Get
            Set
                Me("PT3_Raw_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property PT3_Raw_High() As Integer
            Get
                Return CType(Me("PT3_Raw_High"),Integer)
            End Get
            Set
                Me("PT3_Raw_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property PT1_Eng_Low() As Single
            Get
                Return CType(Me("PT1_Eng_Low"),Single)
            End Get
            Set
                Me("PT1_Eng_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property PT1_Eng_High() As Single
            Get
                Return CType(Me("PT1_Eng_High"),Single)
            End Get
            Set
                Me("PT1_Eng_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property PT2_Eng_Low() As Single
            Get
                Return CType(Me("PT2_Eng_Low"),Single)
            End Get
            Set
                Me("PT2_Eng_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property PT2_Eng_High() As Single
            Get
                Return CType(Me("PT2_Eng_High"),Single)
            End Get
            Set
                Me("PT2_Eng_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property P3_Eng_Low() As Single
            Get
                Return CType(Me("P3_Eng_Low"),Single)
            End Get
            Set
                Me("P3_Eng_Low") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1023")>  _
        Public Property P3_Eng_High() As Single
            Get
                Return CType(Me("P3_Eng_High"),Single)
            End Get
            Set
                Me("P3_Eng_High") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property File_Directory() As String
            Get
                Return CType(Me("File_Directory"),String)
            End Get
            Set
                Me("File_Directory") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("100")>  _
        Public Property Log_Time_Step() As Integer
            Get
                Return CType(Me("Log_Time_Step"),Integer)
            End Get
            Set
                Me("Log_Time_Step") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("NULL")>  _
        Public Property Dir_Script() As String
            Get
                Return CType(Me("Dir_Script"),String)
            End Get
            Set
                Me("Dir_Script") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("30")>  _
        Public Property Timer_Script_Step() As Integer
            Get
                Return CType(Me("Timer_Script_Step"),Integer)
            End Get
            Set
                Me("Timer_Script_Step") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Oxygen_CalDn() As Single
            Get
                Return CType(Me("Oxygen_CalDn"),Single)
            End Get
            Set
                Me("Oxygen_CalDn") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("22")>  _
        Public Property Oxygen_CalTemperature() As Single
            Get
                Return CType(Me("Oxygen_CalTemperature"),Single)
            End Get
            Set
                Me("Oxygen_CalTemperature") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("100")>  _
        Public Property Oxygen_CalO2Percent() As Single
            Get
                Return CType(Me("Oxygen_CalO2Percent"),Single)
            End Get
            Set
                Me("Oxygen_CalO2Percent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property Oxygen_CalFLow() As Single
            Get
                Return CType(Me("Oxygen_CalFLow"),Single)
            End Get
            Set
                Me("Oxygen_CalFLow") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Oxygen_IsCalibrated() As Boolean
            Get
                Return CType(Me("Oxygen_IsCalibrated"),Boolean)
            End Get
            Set
                Me("Oxygen_IsCalibrated") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Oxygen_IsEnabled() As Boolean
            Get
                Return CType(Me("Oxygen_IsEnabled"),Boolean)
            End Get
            Set
                Me("Oxygen_IsEnabled") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Oxygen_IsRunning() As Boolean
            Get
                Return CType(Me("Oxygen_IsRunning"),Boolean)
            End Get
            Set
                Me("Oxygen_IsRunning") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("365.8")>  _
        Public Property Oxygen_CalUP() As Single
            Get
                Return CType(Me("Oxygen_CalUP"),Single)
            End Get
            Set
                Me("Oxygen_CalUP") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Concentrator_Tether.My.MySettings
            Get
                Return Global.Concentrator_Tether.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
