Imports Kasbi
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceDbConnector
        Inherits ServiceExeption
        Implements IService

        Public Shared Function GetSharedConnection() As MSSqlDB
            Return New MSSqlDB()
        End Function
    End Class
End Namespace