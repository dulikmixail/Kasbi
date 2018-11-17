Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceCustomer
        Inherits ServiceExeption
        Implements IService

        Public Function AddCustomerTelNotice(customerSysId As Integer, tel As String) As Boolean
            If customerSysId<1
                Return False
            End If
            Dim cmd = New SqlCommand("set_customer_tel_notice")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customerSysId)
            cmd.Parameters.AddWithValue("@pi_tel_notice", tel)
            Return Convert.ToBoolean(dbSQL.Execute(cmd))
        End Function
    End Class
End Namespace
