Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace Exeption
    Public Class BaseExeption
        Implements IExeption
        Private ReadOnly _texts As List(Of String) = New List(Of String)

        Public Sub AddTextToList(text As String) Implements IExeption.AddTextToList
            _texts.Add(text)
        End Sub

        Public Function GetTextList() As List(Of String) Implements IExeption.GetTextList
            Return _texts
        End Function

        Public Function GetLastText() As String Implements IExeption.GetLastText
            Return _texts.LastOrDefault()
        End Function

        Public Function HaveAnyText() As Boolean Implements IExeption.HaveAnyText
            If _texts.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overloads Function ToString() As String Implements IExeption.ToString
            Dim str As String = String.Empty
            For Each text As String In _texts
                str &= text
            Next
            Return str
        End Function

        Public Function GetAllTextString() As String Implements IExeption.GetAllTextString
            Return String.Join(" ", _texts)
        End Function
    End Class
End Namespace

