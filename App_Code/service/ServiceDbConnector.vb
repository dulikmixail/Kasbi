Imports Kasbi
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceDbConnector
        Inherits ServiceExeption
        Implements IService

        Private Shared Property Conection As MSSqlDB = Nothing

        Public Shared Function GetConnection() As MSSqlDB
            If IsNothing(Conection)
                Return New MSSqlDB()
            Else
                Return Conection
            End If
        End Function
    End Class
End Namespace