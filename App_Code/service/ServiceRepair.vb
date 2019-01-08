Imports System.Data.SqlClient
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceRepair
        Inherits ServiceExeption
        Implements IService

        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()

        Public Function GetNewAktNumberByCashHistoryId(cashHistoryId As Integer) As String
            Dim goodSysId As Integer =
                    Convert.ToInt32(
                        _sharedDbSql.ExecuteScalar(
                            "SELECT good_sys_id FROM cash_history WHERE sys_id = " & cashHistoryId))
            Return GetNewAktNumberByGoodId(goodSysId)
        End Function

        Public Function GetNewAktNumberByGoodId(goodId As Integer) As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            'новый номер договора
            Try
                cmd = New SqlClient.SqlCommand("get_next_repair_akt")
                cmd.Parameters.AddWithValue("@good_sys_id", goodId)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = _sharedDbSql.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                Dim numRepairs As Integer = Convert.ToInt32(ds.Tables(0).Rows(0).Item("num_repairs")) + 1
                Dim numCashregister As String = Trim(ds.Tables(0).Rows(0).Item("num_cashregister").ToString())

                Return numCashregister & "/" & Date.Now.Month & "/" & numRepairs
            Catch
                Return ""
            End Try
        End Function

        Public Function GetNewAktNumberByGoodId(goodId As Object) As String
            Return GetNewAktNumberByGoodId(Convert.ToInt32(goodId))
        End Function

        Public Function CreateCopyOfRepairFromRepair(copyCashHistoryId As Integer) As Integer
            Dim param As SqlClient.SqlParameter
            Dim cmd = New SqlCommand("create_copy_of_repair_from_repair")
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", copyCashHistoryId)
            cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
            cmd.Parameters.AddWithValue("@pi_akt", GetNewAktNumberByCashHistoryId(copyCashHistoryId))
            cmd.CommandType = CommandType.StoredProcedure
            param = New SqlClient.SqlParameter
            param.Direction = ParameterDirection.Output
            param.ParameterName = "@po_insert_hc_sys_id"
            param.SqlDbType = SqlDbType.Int
            cmd.Parameters.Add(param)
            dbSQL.ExecuteScalar(cmd)
            Dim copyRepairId As Integer = Convert.ToInt32(cmd.Parameters("@po_insert_hc_sys_id").Value)
            Return copyRepairId
        End Function

        Public Function CreateCopyOfRepairFromRepair(copyCashHistoryId As Object) As Integer
            Return CreateCopyOfRepairFromRepair(Convert.ToInt32(copyCashHistoryId))
        End Function

        Public Function CreateCopyOfRepairFromRepairWithNewRepairHistory(copyCashHistoryId As Integer) As Integer
            Dim param As SqlClient.SqlParameter
            Dim cmd = New SqlCommand("create_copy_of_repair_from_repair_with_new_repair_history")
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", copyCashHistoryId)
            cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
            cmd.Parameters.AddWithValue("@pi_akt", GetNewAktNumberByCashHistoryId(copyCashHistoryId))
            cmd.CommandType = CommandType.StoredProcedure
            param = New SqlClient.SqlParameter
            param.Direction = ParameterDirection.Output
            param.ParameterName = "@po_insert_hc_sys_id"
            param.SqlDbType = SqlDbType.Int
            cmd.Parameters.Add(param)
            dbSQL.ExecuteScalar(cmd)
            Dim copyRepairId As Integer = Convert.ToInt32(cmd.Parameters("@po_insert_hc_sys_id").Value)
            Return copyRepairId
        End Function

        Public Function CreateCopyOfRepairFromRepairWithNewRepairHistory(copyCashHistoryId As Object) As Integer
            Return CreateCopyOfRepairFromRepairWithNewRepairHistory(Convert.ToInt32(copyCashHistoryId))
        End Function

        Public Sub UpdateRepairDateIn(currentUserId As Integer, goodId As Integer, Optional cashHistoryId As Integer = 0)
            Dim cmd = New SqlCommand("update_repairdate_in")
            cmd.Parameters.AddWithValue("@pi_repairdate_in", Now)
            cmd.Parameters.AddWithValue("@pi_executor", currentUserId)
            cmd.Parameters.AddWithValue("@pi_good_sys_id", goodId)
            cmd.Parameters.AddWithValue("@pi_cash_history_sys_id", cashHistoryId)
            cmd.CommandType = CommandType.StoredProcedure
            dbSQL.Execute(cmd)
        End Sub

        Public Sub UpdateRepairDateIn(currentUserId As Integer, goodId As Object, Optional cashHistoryId As Object = 0)
            UpdateRepairDateIn(currentUserId, Convert.ToInt32(goodId), Convert.ToInt32(cashHistoryId))
        End Sub
    End Class
End Namespace

