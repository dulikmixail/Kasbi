Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiceDbHelper
        Public Shared Function FixNullAndEmpty(dr As DataRow, listNames As String()) As String()
            Dim returListNames As List(Of String) = New List(Of String)
            For Each name As String In listNames
                If IsDBNull(dr(name))
                    returListNames.Add(String.Empty)
                ElseIf String.IsNullOrEmpty(Trim(dr(name).ToString()))
                    returListNames.Add(String.Empty)
                Else
                    returListNames.Add(dr(name).ToString())
                End If
            Next
            Return returListNames.ToArray()
        End Function

        Public Shared Function FixNullAndEmpty(dr As DataRow, name As String) As String
            Dim names(0) As String
            names(0) = name
            Return FixNullAndEmpty(dr, names)(0)
        End Function

    End Class
End Namespace