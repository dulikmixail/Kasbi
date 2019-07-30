Imports System.Data.SqlClient

Namespace Service
    Public Class ServiceRepair
        Inherits ServiceExeption
        Implements IService

        Const SoftwareVersionsPattern As String = "[v,V][0-9]"

        Public Function GetNewAktNumberByCashHistoryId(cashHistoryId As Integer) As String
            Using con = ServiceDbConnector.GetSharedConnection()
                Dim goodSysId As Integer =
                        Convert.ToInt32(
                            con.ExecuteScalar(
                                "SELECT good_sys_id FROM cash_history WHERE sys_id = " & cashHistoryId))

                Return GetNewAktNumberByGoodId(goodSysId)
            End Using
        End Function

        Public Function GetNewAktNumberByGoodId(goodId As Integer) As String
            'новый номер договора
            Try
                Using con = ServiceDbConnector.GetSharedConnection()
                    Using ds = New DataSet
                        Using cmd = New SqlCommand("get_next_repair_akt")
                            cmd.Parameters.AddWithValue("@good_sys_id", goodId)
                            cmd.CommandType = CommandType.StoredProcedure
                            Using adapt = con.GetDataAdapter(cmd)

                                adapt.Fill(ds)
                            End Using
                        End Using

                        Dim numRepairs As Integer = Convert.ToInt32(ds.Tables(0).Rows(0).Item("num_repairs")) + 1
                        Dim numCashregister As String = Trim(ds.Tables(0).Rows(0).Item("num_cashregister").ToString())

                        Return numCashregister & "/" & Date.Now.Month & "/" & numRepairs
                    End Using
                End Using
            Catch
                Return ""
            End Try
        End Function

        Public Function GetNewAktNumberByGoodId(goodId As Object) As String
            Return GetNewAktNumberByGoodId(Convert.ToInt32(goodId))
        End Function

        Public Function CreateCopyOfRepairFromRepair(copyCashHistoryId As Integer) As Integer
            Dim param As SqlParameter
            Dim copyRepairId As Integer
            Using con = ServiceDbConnector.GetSharedConnection()
                Using cmd = New SqlCommand("create_copy_of_repair_from_repair")
                    cmd.Parameters.AddWithValue("@pi_hc_sys_id", copyCashHistoryId)
                    cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
                    cmd.Parameters.AddWithValue("@pi_akt", GetNewAktNumberByCashHistoryId(copyCashHistoryId))
                    cmd.CommandType = CommandType.StoredProcedure
                    param = New SqlParameter
                    param.Direction = ParameterDirection.Output
                    param.ParameterName = "@po_insert_hc_sys_id"
                    param.SqlDbType = SqlDbType.Int
                    cmd.Parameters.Add(param)
                    con.ExecuteScalar(cmd)
                    copyRepairId = Convert.ToInt32(cmd.Parameters("@po_insert_hc_sys_id").Value)
                End Using
            End Using

            Return copyRepairId
        End Function

        Public Function CreateCopyOfRepairFromRepair(copyCashHistoryId As Object) As Integer
            Return CreateCopyOfRepairFromRepair(Convert.ToInt32(copyCashHistoryId))
        End Function

        Public Function CreateCopyOfRepairFromRepairWithNewRepairHistory(copyCashHistoryId As Integer) As Integer
            Dim param As SqlParameter
            Dim cmd = New SqlCommand("create_copy_of_repair_from_repair_with_new_repair_history")
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", copyCashHistoryId)
            cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
            cmd.Parameters.AddWithValue("@pi_akt", GetNewAktNumberByCashHistoryId(copyCashHistoryId))
            cmd.CommandType = CommandType.StoredProcedure
            param = New SqlParameter
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

        Public Sub TryAddGoodSoftwareVersion(cashHistoryId As Integer, goodSysId As Integer)
            Dim detailName As String
            Dim query As String
            Dim ds As DataSet
            Dim adapt As SqlDataAdapter
            Dim rows As DataRowCollection

            If IsLastRepair(cashHistoryId, goodSysId)
                ds = New DataSet()
                query =
                    "SELECT * FROM repair_info ri INNER JOIN details d ON ri.detail_id=d.detail_id  WHERE d.is_detail = 0 AND ri.hc_sys_id = " &
                    cashHistoryId
                adapt = dbSQL.GetDataAdapter(query)
                adapt.Fill(ds)

                If ds.Tables.Count > 0
                    If ds.Tables(0).Rows.Count > 0
                        rows = ds.Tables(0).Rows
                        For Each row As DataRow In rows
                            detailName = row("detail_name").ToString()
                            If Regex.IsMatch(detailName, softwareVersionsPattern)
                                dbSQL.Execute(
                                    String.Format(
                                        "UPDATE good SET software_version='{0}', sv_cash_history_id={1} WHERE good_sys_id = {2}",
                                        detailName, cashHistoryId, goodSysId))
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

        Public Function GetDetailListWithSoftwareVersion() As DataSet
            Dim ds As DataSet = New DataSet()
            Dim adapt As SqlDataAdapter = dbSQL.GetDataAdapter("SELECT * FROM details WHERE is_detail = 0")
            adapt.Fill(ds)
            Dim returnDs As DataSet = ds.Clone()
            If ds.Tables.Count > 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    If Regex.IsMatch(dr("detail_name").ToString(), softwareVersionsPattern)
                        returnDs.Tables(0).ImportRow(dr)
                    End If
                Next
            End If
            Return returnDs
        End Function

        Public Function IsLastRepair(cashHistoryId As Integer, goodSysId As Integer) As Boolean
            Dim lastCashHistoryId As Integer =
                    Convert.ToInt32(
                        dbSQL.ExecuteScalar(
                            String.Format(
                                "SELECT TOP 1 sys_id FROM cash_history WHERE state = 5 AND good_sys_id = {0} ORDER BY change_state_date DESC",
                                goodSysId)))
            Return cashHistoryId = lastCashHistoryId
        End Function
    End Class
End Namespace

