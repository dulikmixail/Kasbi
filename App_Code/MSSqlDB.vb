Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace Kasbi

    Public Class MSSqlDB
        Implements IDisposable

        Dim cn As SqlConnection
        Dim tr As SqlTransaction
        ' Keep track of when the object is disposed.
        Protected disposed As Boolean = False

        ' This method disposes the base object's resources.
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    If Not cn Is Nothing Then
                        If (cn.State = ConnectionState.Open) Then
                            cn.Close()
                        End If
                    End If
                    cn.Dispose()
                    cn = Nothing

                End If
                ' Insert code to free shared resources.
            End If
            Me.disposed = True
        End Sub


        Public Sub MSSqlDB()
            GetConnection()
        End Sub

#Region " IDisposable Support "
        ' Do not change or add Overridable to these methods.
        ' Put cleanup code in Dispose(ByVal disposing As Boolean).
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub
#End Region

        Private Function GetConnection() As SqlConnection
            If cn Is Nothing Then
                'Dim ConnectionString As String = "data source=192.168.11.14;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=198.168.11.14;packet size=4096;password=webdb;"
                cn = New SqlClient.SqlConnection(Config.SqlConnectionString)
            End If
            Try
                If Not (cn.State = ConnectionState.Open) Then
                    cn.Open()
                End If
                Return cn
            Catch ex As SqlException
                Throw (ex)
            End Try
        End Function

        Public Function GetDataAdapter(ByVal sCmd As String, ByVal isStoredProcedure As Boolean) As SqlDataAdapter
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            If (isStoredProcedure) Then
                cmd.CommandType = CommandType.StoredProcedure
            End If
            Return GetDataAdapter(cmd)
        End Function

        Public Function GetDataAdapter(ByVal sCmd As String) As SqlDataAdapter
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            Return GetDataAdapter(cmd)
        End Function

        Public Function GetDataAdapter(ByVal cmd As SqlCommand) As SqlDataAdapter
            If Not tr Is Nothing Then cmd.Transaction = tr
            cmd.Connection = GetConnection()
            Return New SqlDataAdapter(cmd)
        End Function

        Public Function GetReader(ByVal sCmd As String, ByVal isStoredProcedure As Boolean) As SqlDataReader
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            If (isStoredProcedure) Then
                cmd.CommandType = CommandType.StoredProcedure
            End If
            Return GetReader(cmd)
        End Function

        Public Function GetReader(ByVal sCmd As String) As SqlDataReader
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            Return GetReader(cmd)
        End Function

        Public Function GetReader(ByVal cmd As SqlCommand) As SqlDataReader
            If Not tr Is Nothing Then cmd.Transaction = tr
            cmd.Connection = GetConnection()
            Return cmd.ExecuteReader()
        End Function

        Public Function GetValue(ByVal sCmd As String, ByVal isStoredProcedure As Boolean) As Integer
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            If (isStoredProcedure) Then
                cmd.CommandType = CommandType.StoredProcedure
            End If
            Return GetValue(cmd)
        End Function

        Public Function GetValue(ByVal sCmd As String) As Object
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            Return GetValue(cmd)
        End Function

        Public Function GetValue(ByVal cmd As SqlCommand) As Object
            If Not tr Is Nothing Then cmd.Transaction = tr
            cmd.Connection = GetConnection()
            Return cmd.ExecuteScalar()
        End Function

        Public Function Execute(ByVal sCmd As String, ByVal isStoredProcedure As Boolean) As Integer
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            If (isStoredProcedure) Then
                cmd.CommandType = CommandType.StoredProcedure
            End If
            Return Execute(cmd)
        End Function

        Public Function Execute(ByVal sCmd As String) As Integer
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            Return Execute(cmd)
        End Function

        Public Function Execute(ByVal cmd As SqlCommand) As Integer
            If Not tr Is Nothing Then cmd.Transaction = tr
            cmd.Connection = GetConnection()
            Return cmd.ExecuteNonQuery()
        End Function

        Public Function ExecuteScalar(ByVal sCmd As String) As Object
            Dim cmd As SqlCommand = New SqlCommand(sCmd)
            Return ExecuteScalar(cmd)
        End Function

        Public Function ExecuteScalar(ByVal cmd As SqlCommand) As Object
            If Not tr Is Nothing Then cmd.Transaction = tr
            cmd.Connection = GetConnection()
            Return cmd.ExecuteScalar()
        End Function

        Public Function BeginTransaction() As Boolean
            If Not tr Is Nothing Then Return False
            tr = cn.BeginTransaction(IsolationLevel.ReadCommitted)
            Return True
        End Function

        Public Function RollbackTransaction() As Boolean
            If tr Is Nothing Then Return False
            tr.Rollback()
            tr = Nothing
            Return True
        End Function

        Public Function CommitTransaction() As Boolean
            If tr Is Nothing Then Return False
            tr.Commit()
            tr = Nothing
            Return True
        End Function
    End Class

End Namespace
