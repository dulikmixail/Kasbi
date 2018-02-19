Namespace Kasbi

    Partial Class SalesNeopl
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
                    msg.Text = "Неверные параметры"
                    Exit Sub
                End Try
            End Try

            Session("AddSaleForCustomer") = cust
            Session("insalesneopl") = 1
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
            Dim s$, sale%, sDogovor$
            Dim dolg As Integer
            Dim support, cto As Boolean

            Try
                cmdSales = New SqlClient.SqlCommand("get_sales_neopl")
                cmdSales.CommandType = CommandType.StoredProcedure
                cmdSales.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                readerSales = dbSQL.GetReader(cmdSales)
                While readerSales.Read
                    sale = readerSales.Item("sale_sys_id")
                    sDogovor = readerSales.Item("subdogovor")

                    If sDogovor.Trim.Length > 0 Then
                        sDogovor = readerSales.Item("dogovor") & sDogovor
                    Else
                        sDogovor = readerSales.Item("dogovor")
                    End If

                    Dim b, rebill As Boolean

                    s = "&nbsp;<span style='size:25'>" & readerSales.Item("customer_name") & "</span> (заказ №" & sDogovor & ")"
                    b = readerSales.Item("cto") <> 1
                    'b = True
                    cust = readerSales.Item("customer_sys_id")
                    rebill = False

                    If rebill Then

                        ctrl1 = CType(LoadControl("~/RebillingGrid.ascx"), Kasbi.RebillingGrid)

                        'ctrl1 = CType(LoadControl("~/Controls/RebillingGrid.ascx"), ASP.controls_rebillinggrid_ascx)
                        ctrl1.iSale = sale
                        ctrl1.iCustomer = cust
                        CType(ctrl1.FindControl("HeaderSale"), Label).Text = s & "&nbsp;&nbsp;&nbsp;&nbsp;UID:&nbsp;" & readerSales("sale_sys_id")
                        CType(ctrl1.FindControl("DateSale"), Label).Text = Format(readerSales.Item("sale_date"), "dd.MM.yyyyг.")
                        If readerSales.IsDBNull(readerSales.GetOrdinal("saler_info")) Then
                            s = ""
                        Else
                            s = readerSales.Item("saler_info")
                        End If
                        CType(ctrl1.FindControl("Saler"), Label).Text = s
                        CType(ctrl1.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=6")
                        CType(ctrl1.FindControl("lnkSpisok_KKM"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=7")
                        CType(ctrl1.FindControl("lnkZayavlenie_IMNS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & cust & "&s=" & sale & "&t=18")

                        If Not ctrl1 Is Nothing Then
                            pnlSales.Controls.Add(ctrl1)
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
                        CType(ctrl.FindControl("DateSale"), Label).Text = Format(readerSales.Item("sale_date"), "dd.MM.yyyyг.")
                        If readerSales.IsDBNull(readerSales.GetOrdinal("saler_info")) Then
                            s = ""
                        Else
                            s = readerSales.Item("saler_info")
                        End If
                        CType(ctrl.FindControl("Saler"), Label).Text = s
                        CType(ctrl.FindControl("lnkInvoice"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=1")
                        CType(ctrl.FindControl("lnkZayavlenieNaKniguKassira"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=2")
                        CType(ctrl.FindControl("lnkZayavlenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=3")
                        'CType(ctrl.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=6")
                        CType(ctrl.FindControl("lnkSpisok_KKM"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=7")
                        CType(ctrl.FindControl("lnkTTN"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=5")
                        CType(ctrl.FindControl("lnkInvoiceNDS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=0")
                        CType(ctrl.FindControl("lnkGarantia"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=17")
                        CType(ctrl.FindControl("lnkZayavlenie_IMNS"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=18")
                        CType(ctrl.FindControl("lnkTTN_Transport"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & cust & "&s=" & sale & "&t=35")
                        CType(ctrl.FindControl("lnkIzveschenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?vidplateza=0&c=" & cust & "&s=" & sale & "&t=41")

                        'Срываем панель для вывода накладных
                        CType(ctrl.FindControl("pnl_docs"), Panel).Visible = False

                        If Not ctrl Is Nothing Then
                            pnlSales.Controls.Add(ctrl)
                        End If

                    End If
                    dolg = readerSales.Item("dolg")
                    support = (readerSales.Item("support") = 0) Or (readerSales.Item("cto") = 1)
                    cto = (readerSales.Item("cto") = 1)

                End While
                If support = False Then
                    'Else
                    'lnkAddSupport.NavigateUrl = GetAbsoluteUrl("~/Support.aspx?" & cust)
                End If

                ' If cto Then
                ' Else
                'txtSum.Visible = False
                'btnCanceling.Visible = False
                '
                'End If
                If pnlSales.Controls.Count = 0 Then
                    pnlSales.Controls.Add(New Label)
                    CType(pnlSales.Controls(0), Label).Text = "Неоплаченные заказы отсутствуют..."
                End If
            Catch
                msgDocumentsPanel.Text = Err.Description
            Finally
                If Not readerSales.IsClosed Then
                    readerSales.Close()
                End If
            End Try
        End Sub



        'Private Sub btnCreateDocuments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '    Dim doc_num() As Integer = New Integer(5) {6, 7, 4, 8, 9, 18}
        '    Dim docs As New Kasbi.Migrated_Documents
        '    docs.ProcessDocuments(doc_num, cust, iSale, 1)
        '    docs = Nothing
        'End Sub

    End Class
End Namespace
