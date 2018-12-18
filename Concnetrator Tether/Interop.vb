Imports System.Runtime.InteropServices

Module Interop
    Public Enum EXECUTION_STATE As UInteger ' Define the API Execution states
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum

    Private Declare Function SetThreadExecutionState Lib "Kernel32" (ByVal esflags As EXECUTION_STATE) As EXECUTION_STATE

    Function No_Sleep() As EXECUTION_STATE
        Return SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_DISPLAY_REQUIRED Or EXECUTION_STATE.ES_CONTINUOUS)
    End Function
    Function GOTOSLEEP() As EXECUTION_STATE
        Return SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Function



End Module
