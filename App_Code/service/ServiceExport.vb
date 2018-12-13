Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiceExport
        Inherits ServiceExeption
        Implements IService

        Public Sub LockCashHistory(cashHistories As ArrayList, Optional exportType As ExportType = ExportType.NotKnown)
            Dim cmd As SqlClient.SqlCommand
            If cashHistories.Count > 0
                cmd = New SqlClient.SqlCommand("insert_export_info_list")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_ch_sys_id_list", String.Join(",", cashHistories.ToArray()))
                cmd.Parameters.AddWithValue("@pi_executor_sys_id", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@pi_export_type_sys_id", ExportType.OneC)
                dbSQL.ExecuteScalar(cmd)
            End If
        End Sub

        Public Function IsLockCashHistory(cashHistorySysId As Integer) As Boolean
            Dim cmd As SqlClient.SqlCommand =
                    New SqlClient.SqlCommand(
                        "SELECT COUNT(*) as cout FROM export_info WHERE ch_sys_id = " & cashHistorySysId)
            Dim res = Convert.ToInt32(dbSQL.ExecuteScalar(cmd))
            Return res > 0
        End Function
    End Class
End Namespace