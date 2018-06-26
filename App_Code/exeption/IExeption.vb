Namespace Exeption
    Public Interface IExeption
        Sub AddTextToList(text As String)
        Function GetTextList() As List(Of String)
        Function GetLastText() As String
        Function HaveAnyText() As Boolean
        Function ToString() As String
        Function GetAllTextString() As String
    End Interface
End Namespace