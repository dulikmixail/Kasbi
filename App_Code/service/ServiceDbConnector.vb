Imports Kasbi
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceDbConnector
        Inherits ServiceExeption
        Implements IService

        Private Shared Property Connection As MSSqlDB = Nothing
        Private Shared Property Connection2 As MSSqlDB = Nothing

        Public Shared Function GetConnection() As MSSqlDB
            If IsNothing(Connection)
                Return New MSSqlDB()
            Else
                Return Connection
            End If
        End Function

        Public Shared Function GetConnection2() As MSSqlDB
            If IsNothing(Connection2)
                Return New MSSqlDB()
            Else
                Return Connection2
            End If
        End Function
    End Class
End Namespace