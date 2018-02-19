Imports System.Collections.Generic

Namespace Kasbi

    Partial Class CustomerSales
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink

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


        Dim cust%
        Dim dolg As Integer

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim s$ = ""

            cust = 0
            s = Request.Params(0)
            Try
                cust = CInt(s)
                Session("Customer") = cust
            Catch
                Try
                    cust = CInt(Session("Customer"))
                Catch
                    msg.Text = "�������� ���������"
                    Exit Sub
                End Try
            End Try
            lnkAddSale.NavigateUrl = GetAbsoluteUrl("~/NewRequest.aspx?" & cust)
            lblCustomerInfo.Text = GetInfo(cust)
            Session("CustomerInfo") = lblCustomerInfo.Text

            Session("AddSaleForCustomer") = cust
            Bind()

        End Sub

        Function GetInfo(ByVal cust As Integer) As String

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
                            s = s & "������������ : " & sTmp & "<br>"
                        End If
                        sTmp = .Item("accountant")
                        If sTmp.Length > 0 Then
                            s = s & "���������: " & sTmp & "<br>"
                        End If
                        sTmp = .Item("unn")
                        If sTmp.Length > 0 Then
                            s = s & "���: " & sTmp & "<br>"
                        End If
                        sTmp = .Item("registration")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                        sTmp = "�� " & .Item("tax_inspection")
                        If s.Length > 0 Then
                            s = s & sTmp & "<br>"
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

                        dolg = .Item("dolg")

                        If txtSum.Text.Length = 0 Then
                            txtSum.Text = dolg
                        End If
                        lblDolg.Text = "����� �����:&nbsp;" & dolg & "&nbsp;&nbsp;"

                    End With
                End If

            Catch
                msgClientInfo.Text = Err.Description
            End Try
            GetInfo = s
        End Function

        Public Sub Bind()
            Dim cmdSales As SqlClient.SqlCommand
            Dim readerSales As SqlClient.SqlDataReader
            Dim ctrl As Kasbi.Sale 'ASP.sale_ascx 
            Dim ctrl1 As Kasbi.RebillingGrid 'ASP.rebillinggrid_ascx 
            Dim s$, sale%, sDogovor$, sDogovorOld$
            Dim support, cto, dogovorIsVisible, dogovorOldIsVisible, dogovorDopdIsVisible As Boolean
            Dim controls As New List(Of Control)
            Dim saleHaveCashregCount As Integer = 0
            sDogovorOld = ""
            sDogovor = ""




            Try
                cmdSales = New SqlClient.SqlCommand("get_sales_by_customer")
                cmdSales.CommandType = CommandType.StoredProcedure
                cmdSales.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                readerSales = dbSQL.GetReader(cmdSales)

                While readerSales.Read
                    sDogovorOld = sDogovor
                    sale = readerSales.Item("sale_sys_id")
                    sDogovor = readerSales.Item("subdogovor")
                    If sDogovor.Trim.Length > 0 Then
                        sDogovor = readerSales.Item("dogovor") & sDogovor
                    Else
                        sDogovor = readerSales.Item("dogovor")
                    End If

                    Dim b, rebill As Boolean
                    If readerSales.Item("state") = 1 Then
                        s = "&nbsp;����� �" & sDogovor
                        'b = readerSales.Item("cto") <> 1
                        If readerSales.Item("type") = 0 Then
                            b = True
                        Else
                            b = readerSales.Item("cto") <> 1
                        End If
                        rebill = False
                    ElseIf readerSales.Item("state") = 4 Then
                        s = "&nbsp;�������������� �" & sDogovor
                        b = False
                        rebill = True
                    Else
                        s = "&nbsp;������� �" & sDogovor
                        If readerSales.Item("type") = 1 Then
                            s = s & "(���/���)"
                        ElseIf readerSales.Item("type") = 3 Then
                            s = s & "(���������)"
                        Else
                            s = s & "(��������)"
                        End If
                        b = False
                        rebill = False
                    End If



                    If readerSales.Item("is_have_cashregister") = 1 Then
                        If DateTime.Parse(readerSales.Item("sale_date").ToString()) > DateTime.Parse("20.02.2018") Then
                            If saleHaveCashregCount = 0 Then
                                dogovorIsVisible = True
                                dogovorDopdIsVisible = False
                                dogovorOldIsVisible = False
                                saleHaveCashregCount += 1
                            Else
                                dogovorIsVisible = False
                                dogovorDopdIsVisible = True
                                dogovorOldIsVisible = False
                            End If
                        Else
                            dogovorIsVisible = False
                            dogovorDopdIsVisible = False
                            dogovorOldIsVisible = True
                        End If

                    Else
                        dogovorIsVisible = False
                        dogovorDopdIsVisible = False
                        dogovorOldIsVisible = False
                    End If

                    If rebill Then
                        ctrl1 = CType(LoadControl("~/RebillingGrid.ascx"), Kasbi.RebillingGrid)
                        'ctrl1 = CType(LoadControl("~/Controls/RebillingGrid.ascx"), ASP.controls_rebillinggrid_ascx)
                        ctrl1.iSale = sale
                        ctrl1.iCustomer = cust
                        CType(ctrl1.FindControl("HeaderSale"), Label).Text = s & "&nbsp;&nbsp;&nbsp;&nbsp;UID:&nbsp;" & readerSales("sale_sys_id")
                        CType(ctrl1.FindControl("DateSale"), Label).Text = Format(readerSales.Item("sale_date"), "dd.MM.yyyy�.")
                        If readerSales.IsDBNull(readerSales.GetOrdinal("saler_info")) Then
                            s = ""
                        Else
                            s = readerSales.Item("saler_info")
                        End If
                        CType(ctrl1.FindControl("Saler"), Label).Text = s
                        If dogovorOldIsVisible Then
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=6")
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_Dop"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=57")
                        Else
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO"), HyperLink).Visible = False
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_Dop"), HyperLink).Visible = False
                        End If
                        If dogovorIsVisible Then
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_2"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=58")
                        Else
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_2"), HyperLink).Visible = False

                        End If

                        If dogovorDopdIsVisible Then
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_Dop_2"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=59")
                        Else
                            CType(ctrl1.FindControl("lnkDogovor_Na_TO_Dop_2"), HyperLink).Visible = False
                        End If
                        CType(ctrl1.FindControl("lnkSpisok_KKM"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=7")
                        CType(ctrl1.FindControl("lnkZayavlenie_IMNS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=18")

                            If Not ctrl1 Is Nothing Then
                                controls.Add(ctrl1)
                                'pnlSales.Controls.Add(ctrl1)
                            End If
                        Else
                            ctrl = CType(LoadControl("~/sale.ascx"), Kasbi.Sale) 'ASP.sale_ascx)
                        ctrl.iSale = sale
                        ctrl.iCustomer = cust
                        ctrl.FindControl("btnConfirm").Visible = b
                        ctrl.FindControl("optBeznal").Visible = b
                        ctrl.FindControl("optNal").Visible = b
                        ctrl.FindControl("optSberkassa").Visible = b
                        CType(ctrl.FindControl("HeaderSale"), Label).Text = s & "&nbsp;&nbsp;&nbsp;&nbsp;UID:&nbsp;" & readerSales("sale_sys_id")
                        CType(ctrl.FindControl("DateSale"), Label).Text = Format(readerSales.Item("sale_date"), "dd.MM.yyyy�.")
                        If readerSales.IsDBNull(readerSales.GetOrdinal("saler_info")) Then
                            s = ""
                        Else
                            s = readerSales.Item("saler_info")
                        End If
                        CType(ctrl.FindControl("Saler"), Label).Text = s
                        CType(ctrl.FindControl("lnkInvoice"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=1")
                        CType(ctrl.FindControl("lnkZayavlenieNaKniguKassira"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=2")
                        CType(ctrl.FindControl("lnkZayavlenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=3")
                        If dogovorOldIsVisible Then
                            CType(ctrl.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=0&c=" & cust & "&s=" & sale & "&t=6")
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_Dop"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=0&c=" & cust & "&s=" & sale & "&t=57")
                        Else
                            CType(ctrl.FindControl("lnkDogovor_Na_TO"), HyperLink).Visible = False
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_Dop"), HyperLink).Visible = False
                        End If

                        If dogovorIsVisible Then
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_2"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=58")
                        Else
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_2"), HyperLink).Visible = False

                        End If

                        If dogovorDopdIsVisible Then
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_Dop_2"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=59")
                        Else
                            CType(ctrl.FindControl("lnkDogovor_Na_TO_Dop_2"), HyperLink).Visible = False
                        End If

                        CType(ctrl.FindControl("lnkSpisok_KKM"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=7")

                        CType(ctrl.FindControl("lnkTTN"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=5")
                        'CType(ctrl.FindControl("btnTTN"), LinkButton).TabIndex = sale

                        CType(ctrl.FindControl("lnkInvoiceNDS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=0")
                        'CType(ctrl.FindControl("btnInvoiceNDS"), LinkButton).TabIndex = sale

                        CType(ctrl.FindControl("lnkGarantia"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=17")
                        CType(ctrl.FindControl("lnkZayavlenie_IMNS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=18")
                        CType(ctrl.FindControl("lnkTTN_Transport"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=35")

                        'CType(ctrl.FindControl("btnTTNT"), LinkButton).TabIndex = sale

                        CType(ctrl.FindControl("lnkIzveschenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?vidplateza=0&c=" & cust & "&s=" & sale & "&t=41")

                        If Not ctrl Is Nothing Then
                            controls.Add(ctrl)
                            'pnlSales.Controls.Add(ctrl)
                        End If
                    End If
                    dolg = readerSales.Item("dolg")
                    support = (readerSales.Item("support") = 0) Or (readerSales.Item("cto") = 1)
                    cto = (readerSales.Item("cto") = 1)
                End While

                controls.Reverse()
                For Each control As Control In controls
                    pnlSales.Controls.Add(control)
                Next

                If support = False Then
                    lnkAddSupport.Enabled = False
                    'Else
                    '    lnkAddSupport.NavigateUrl = GetAbsoluteUrl("~/Support.aspx?" & cust)
                End If
                custUID.Text = "�" & cust
                lblDolg.Text = "����� �����:&nbsp;" & CStr(dolg) & "&nbsp;&nbsp;"

                ' If cto Then
                If txtSum.Text.Length = 0 Then
                    txtSum.Text = dolg
                End If

                lnkIzveschenie.NavigateUrl = GetAbsoluteUrl("~/documents.aspx?vidplateza=1&c=" & cust & "&s=0&t=41")
                lnkDefectAct.NavigateUrl = GetAbsoluteUrl("documents.aspx?t=42&c=" & cust)

                If CurrentUser.permissions = 4 Then
                    txtSum.Enabled = True
                    btnCanceling.Enabled = True
                Else
                    txtSum.Enabled = False
                    btnCanceling.Enabled = False
                End If

                ' Else
                'txtSum.Visible = False
                'btnCanceling.Visible = False
                '
                'End If
                If pnlSales.Controls.Count = 0 Then
                    pnlSales.Controls.Add(New Label)
                    CType(pnlSales.Controls(0), Label).Text = "������ ��� ������� �����������."
                End If
            Catch
                msgDocumentsPanel.Text = Err.Description
            Finally
                If Not readerSales.IsClosed Then
                    readerSales.Close()
                End If
            End Try
        End Sub

        Private Sub btnCanceling_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanceling.Click
            Dim cmd As SqlClient.SqlCommand

            '���������� ����� � ����
            Try
                cmd = New SqlClient.SqlCommand("update_customer_dolg")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                cmd.Parameters.AddWithValue("@pi_dolg", -CInt(txtSum.Text))
                dbSQL.Execute(cmd)
            Catch
                msgClientInfo.Text = "������ ������ ������!<br>" & Err.Description
            End Try
            Session("CurrentPage") = "CustomerSales"
            Response.Redirect(GetAbsoluteUrl("~/CustomerSales.aspx?" & cust))
        End Sub

        'Private Sub btnCreateDocuments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '    Dim doc_num() As Integer = New Integer(5) {6, 7, 4, 8, 9, 18}
        '    Dim docs As New Kasbi.Migrated_Documents
        '    docs.ProcessDocuments(doc_num, cust, iSale, 1)
        '    docs = Nothing
        'End Sub
    End Class

End Namespace
