Imports Microsoft.VisualBasic
Imports Service


Namespace Service
    Public Class ServiceApi
        Inherits ServiceExeption
        Implements IService

        Public Function GetDebtByUnp(unp As Integer) As Double
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            cmd = New SqlClient.SqlCommand("get_customer_info_by_unp")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_unp", unp)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).DefaultView(0)
                    Return .Item("dolg")
                End With
            Else
                Return 0
            End If
        End Function
    End Class

End Namespace
