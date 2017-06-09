Namespace Kasbi

    Partial Class MarketingView
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim ser

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ser = Request.Params(0)
            GetInfo(ser)
        End Sub


        Function GetInfo(ByVal cust As Integer) As String
            Dim reader As SqlClient.SqlDataReader
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            s = ""

            If cust = 0 Then
                GetInfo = s
                Exit Function
            End If
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).DefaultView(0)
                        Dim sTmp$
                        sTmp = .Item("customer_name")
                        If sTmp.Length > 0 Then
                            s = s & "<b>" & sTmp & "</b><br>"
                        End If
                        sTmp = .Item("boos_name")
                        If sTmp.Length > 0 Then
                            s = s & "Руководитель : " & sTmp & "<br>"
                        End If
                        sTmp = .Item("accountant")
                        If sTmp.Length > 0 Then
                            s = s & "Бухгалтер: " & sTmp & "<br>"
                        End If
                        sTmp = .Item("customer_address")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "&nbsp;&nbsp;"
                            sTmp = .Item("customer_phone")
                            If sTmp.Length > 0 Then
                                s = s & sTmp
                            End If
                            s = s & "<br>"
                        End If
                        sTmp = .Item("bank")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                    End With
                End If
                msgClientInfo.Text = s
                custUID.text = ser
                
                'Достаем дополнительную инфу о пользователе
                
                Dim query = "SELECT (SELECT name FROM employee WHERE sys_id=c.manager) AS manag, (SELECT adv_name FROM advertising WHERE advertise_id=c.advertise_id) AS advertis FROM customer c WHERE customer_sys_id='" & cust & "'"

                reader = dbSQL.GetReader(query)
                If reader.Read() Then
                    Try
                        Manager.Text = reader.Item(0)
                        Advertise.Text = reader.Item(1)
                    Catch
                    End Try
                Else
                End If
                reader.Close()

                query = "SELECT COUNT(*) AS num, SUM(dbo.good.price) AS summ FROM dbo.sale INNER JOIN dbo.good ON dbo.sale.sale_sys_id = dbo.good.sale_sys_id WHERE (dbo.sale.customer_sys_id = '" & cust & "')"

                reader = dbSQL.GetReader(query)
                If reader.Read() Then
                    Try
                        num_sales.Text = reader.Item(0)
                        summ_sales.Text = reader.Item(1)
                    Catch
                    End Try
                Else
                End If
                reader.Close()

                'Достаем историю работы с клиентом

                Get_history(cust)
                Get_dates(cust)
                Get_activity(cust)
                Get_notes(cust)
                Get_interests(cust)
            Catch
                msgClientInfo.Text = s
            End Try
            GetInfo = s
        End Function


        Sub Get_history(ByVal cust)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim query = "SELECT *, (SELECT name FROM employee WHERE sys_id=employee) AS manager FROM client_history WHERE customer_sys_id='" & cust & "' ORDER BY id DESC"

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdHistory.DataSource = ds.Tables(0).DefaultView
            grdHistory.DataKeyField = "id"
            grdHistory.DataBind()
        End Sub

        Sub Get_dates(ByVal cust)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim query = "SELECT * FROM client_date WHERE customer_sys_id='" & cust & "'"

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdDates.DataSource = ds.Tables(0).DefaultView
            grdDates.DataKeyField = "id"
            grdDates.DataBind()
        End Sub

        Sub Get_activity(ByVal cust)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim query = "SELECT dbo.client_activity_type.name, dbo.client_activity_type.id_activity_type AS id FROM dbo.client_activity INNER JOIN dbo.client_activity_type ON dbo.client_activity.id_activity_type = dbo.client_activity_type.id_activity_type WHERE (dbo.client_activity.customer_sys_id = '" & cust & "')"

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdActivity.DataSource = ds.Tables(0).DefaultView
            grdActivity.DataKeyField = "id"
            grdActivity.DataBind()
        End Sub

        Sub Get_notes(ByVal cust)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim query = "SELECT * FROM client_notes WHERE customer_sys_id='" & cust & "'"

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdNotes.DataSource = ds.Tables(0).DefaultView
            grdNotes.DataKeyField = "id"
            grdNotes.DataBind()
        End Sub

        Sub Get_interests(ByVal cust)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim query = "SELECT dbo.client_activity_type.name, dbo.client_activity_type.id_activity_type AS id FROM dbo.client_interests INNER JOIN dbo.client_activity_type ON dbo.client_interests.id_activity_type = dbo.client_activity_type.id_activity_type WHERE (dbo.client_interests.customer_sys_id = '" & cust & "')"

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdInterests.DataSource = ds.Tables(0).DefaultView
            grdInterests.DataKeyField = "id"
            grdInterests.DataBind()
        End Sub
    End Class

End Namespace
