Imports Exeption
Imports Microsoft.VisualBasic


Namespace Service
    Public Class ServiceTelNumber
        Inherits ServiceExeption
        Implements IService
        Dim ReadOnly _validTelCode As List(Of String) = New List(Of String) From {"25", "29", "33", "44"}

        Public Function IsValidNumber(number As String, Optional require As Boolean = True) As Boolean
            Dim exeption As BaseExeption = New BaseExeption()
            number = Trim(number)

            If String.IsNullOrEmpty(number) And require
                exeption.AddTextToList("Вы не ввели номер телефона оповещения.")
            End If
            If number.Length > 0 And number.Length < 9
                exeption.AddTextToList("Введен некорректный мобильный телефон!")
            Else If number.Length > 1
                If Not _validTelCode.Contains(number.Substring(0, 2))
                    exeption.AddTextToList("Введен некорректный код оператора мобильного телефона!")
                End If
            End If
            If exeption.HaveAnyText()
                AddExeption(exeption)
                Return False
            Else
                Return True
            End If
        End Function
    End Class
End Namespace

