Module CRC_8Bit

    Function CRC_8(ByRef inputdatapayload As Byte()) As Byte
        Const Generator As Byte = &H1D&
        Dim CRC As Byte
        CRC = 0
        For Each currbyte In inputdatapayload
            CRC = CRC Xor currbyte
            For i = 0 To 7
                If ((CRC And CByte(&H80&)) = 0) Then

                    CRC <<= 1


                Else

                    CRC = (CRC << 1)
                    CRC = (CRC Xor Generator)


                End If

            Next
        Next

        Return CRC

    End Function


End Module
