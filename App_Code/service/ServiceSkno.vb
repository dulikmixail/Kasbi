Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiceSkno
        Inherits ServiceExeption
        Implements IService

        Public Function CheckSettableStatusSkno(idGood As Integer, statusSkno As Integer) As Boolean
            Dim cmd As SqlCommand,
                adapt = New SqlDataAdapter(),
                ds = New DataSet()
            cmd = New SqlCommand("get_last_skno_history")
            cmd.Parameters.AddWithValue("@pi_good_sys_id", idGood)
            cmd.CommandType = CommandType.StoredProcedure
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds)

            Return _
                Convert.ToBoolean(Iif(GetLastSknoStatus(idGood) = statusSkno, False, True))
        End Function

        Public Function GetLastSknoStatus(idGood As Integer) As Integer
            Dim cmd As SqlCommand,
                adapt = New SqlDataAdapter(),
                ds = New DataSet()
            cmd = New SqlCommand("get_last_skno_history")
            cmd.Parameters.AddWithValue("@pi_good_sys_id", idGood)
            cmd.CommandType = CommandType.StoredProcedure
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds)
            Return Convert.ToInt32(Iif(ds.Tables(0).Rows.Count > 0, ds.Tables(0).Rows(0).Item("state_skno"), 0))
        End Function
    End Class
End Namespace


