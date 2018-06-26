Imports Exeption
Imports Kasbi
Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiceExeption
        Inherits PageBase
        Private ReadOnly _listExeption As New List(Of IExeption)

        Public Sub AddExeption(exeption As IExeption)
            _listExeption.Add(exeption)
        End Sub
        Public Function GetLastExeption() As IExeption
            Return _listExeption.LastOrDefault()
        End Function

        Public Function GetListAllExeption() As List(Of IExeption)
            Return _listExeption
        End Function
        Public Function GetListStringAllExeption() As String()
            Dim list As String() = New String(_listExeption.Count) {}
            For j = 0 To _listExeption.Count - 1
                list(j) = _listExeption(j).ToString()
            Next
            Return list
        End Function
        Public Function GetStringAllExeption() As String
            Dim str As String = String.Empty
            For j = 0 To _listExeption.Count - 1
                str &= _listExeption(j).ToString()
            Next
            Return str
        End Function

        Public Function GetTextStringAllExeption() As String
            Dim str As String = String.Empty
            For Each ex As IExeption In _listExeption
                str &= ex.GetAllTextString() & " "
            Next
            Return str
        End Function

        Public Function HaveAnyExeption() As Boolean
            Return _listExeption.Count <> 0
        End Function

    End Class

End Namespace
