Namespace Kasbi

Partial Class Reports
        Inherits PageBase


        Protected WithEvents grd As System.Web.UI.WebControls.DataGrid

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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sWhere$ = ""
            Dim sTitle$ = ""
            Dim sKeyword$
            If Request.QueryString.Count = 0 Then
                sKeyword = ""
            Else
                sKeyword = Request.QueryString(0)
            End If
            Const sTitle1 = "������ �������� �� ���������������. "
            Const sLike = "g.set_place like '%"
            Const sMarkets = sLike & "���������%' or " & sLike & "���������%' or " & sLike & "������%' or " & sLike & "�������%' or " & sLike & "������%' " ' & sLike & "%'"

            Select Case sKeyword
                Case "minsk-all"
                    sWhere = sLike & "�����%' or " & sMarkets
                    sTitle = "�����."
                Case "minsk-markets"
                    sWhere = sMarkets
                    sTitle = "����� ������."
                Case "minsk-others"
                    sWhere = sLike & "�����%'" & " and not(" & sMarkets & ")"
                    sTitle = "����� ����� ������."
                Case "republic-all"
                    sWhere = "(g.set_place is not null) and (len(ltrim(rtrim(g.set_place))) > 0)"
                    sTitle = ""
                Case "republic-others"
                    sWhere = "(g.set_place is not null) and (len(ltrim(rtrim(g.set_place))) > 0) and not (" & sLike & "�����%'" & ")"
                    sTitle = "��� ����� ������."
                Case "dinamo"
                    sWhere = sLike & "������%'"
                    sTitle = "����� ������."
                Case "akvabel"
                    sWhere = sLike & "�������%'"
                    sTitle = "����� �������."
                Case "zhdanovichi"
                    sWhere = sLike & "���������%'"
                    sTitle = "�������� ��� ���������."
                Case "malinovka"
                    sWhere = sLike & "���������%'"
                    sTitle = "����� ���������."
                Case "uruchye"
                    sWhere = sLike & "������%'"
                    sTitle = "������������ ����� ������."
                Case Else
                    sWhere = sLike & sKeyword & "%'"
                    sTitle = "�������� ����� '" & sKeyword & "'."
            End Select

            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader
            Dim iCustomer% = 0
            Dim iRowspan% = 0
            Dim iCount% = 0
            Dim iCountCash% = 0
            Dim s2, s3 As System.Text.StringBuilder

            Try
                cmd = New SqlClient.SqlCommand("get_report_for_support")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_where", sWhere)
                reader = dbSQL.GetReader(cmd)

                s2 = New System.Text.StringBuilder(300)
                s3 = New System.Text.StringBuilder(2000)
                While reader.Read()
                    iCountCash = iCountCash + 1
                    If iCustomer <> reader("customer_sys_id") Then
                        iCount = iCount + 1
                        If iCustomer <> 0 Then
                            s3.Append("<tr class='underline'><td class='cust'")
                            s3.Append(IIf(iRowspan > 1, " rowspan=" & iRowspan, "") & ">")
                            s3.Append(s2)
                            s2 = New System.Text.StringBuilder(300)
                        End If
                        iCustomer = reader("customer_sys_id")
                        iRowspan = 1
                        s2.Append("<b>" & iCount & "</b>. ")
                        s2.Append(reader("customer_details"))
                        s2.Append("</td>")
                    Else
                        s2.Append("<tr>")
                        iRowspan = iRowspan + 1
                    End If
                    s2.Append("<td class='cash'>")
                    s2.Append("<b>" & iCountCash & "</b>. ")
                    s2.Append(reader("cash_register"))
                    s2.Append("</td></tr>")
                End While
                If s2.Length > 0 Then
                    s3.Append("<tr class='underline'><td class='cust'")
                    s3.Append(IIf(iRowspan > 1, " rowspan=" & iRowspan, "") & ">")
                    s3.Append(s2)
                End If

                reader.Close()

                lblTitle.Text = sTitle1 & sTitle & " ����� ��������: " & iCount & "; ����: " & iCountCash & "."
                table.Text = s3.ToString

            Catch ex As Exception
                msg.Text = ex.ToString
                Exit Sub
            End Try

        End Sub

End Class

End Namespace
