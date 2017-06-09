Imports Infragistics.WebUI.UltraWebGrid


Namespace Kasbi.Reports

    Partial Class TurnoverListByGood
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents grdTurnoverList As System.Web.UI.WebControls.DataGrid
        Protected WithEvents GoodName As System.Web.UI.WebControls.Label


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim iNumber%, good_type_id%
        Dim rest As Double
        Dim Good_name$

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Try
                Dim ch() As Char = {","}
                good_type_id = GetPageParam("g_t")
            Catch
                msg.Text = "Неверный запрос"
                Exit Sub
            End Try
            If Not IsPostBack Then
                Bind()
                Me.ugrdTurnoverListByGood.DisplayLayout.ActiveRow = Me.ugrdTurnoverListByGood.Rows(0)
            End If
            lbGoodName.Text = Session("Good_name")

        End Sub

        Private Sub Bind()
            Dim cmd, cmd1 As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rel As DataRelation

            cmd = New SqlClient.SqlCommand("prc_rpt_TurnoverListByGood")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", good_type_id)
            cmd.Parameters.AddWithValue("@pi_type", "Приход")

            adapter = dbSQL.GetDataAdapter(cmd)
            adapter.Fill(ds, "Delivery")
            cmd1 = New SqlClient.SqlCommand("prc_rpt_TurnoverListByGood")
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.CommandTimeout = 0
            cmd1.Parameters.AddWithValue("@pi_good_type_sys_id", good_type_id)
            cmd1.Parameters.AddWithValue("@pi_type", "Расход")
            adapter = dbSQL.GetDataAdapter(cmd1)
            adapter.Fill(ds, "Sales")
            rel = ds.Relations.Add("DeliverySales", ds.Tables("Delivery").Columns("delivery"), ds.Tables("Sales").Columns("delivery"))
            iNumber = 0
            rest = 0
            ugrdTurnoverListByGood.DataSource = ds.Tables("Delivery").DefaultView
            ugrdTurnoverListByGood.DataBind()
        End Sub

        Private Sub grdTurnoverListByGood_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdTurnoverListByGood.ItemDataBound
            Dim s As String
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.ItemIndex = 0 Then
                    lbGoodName.Text = e.Item.DataItem("good_name")
                End If

                'Dates
                If Not IsDBNull(e.Item.DataItem("DateDoc")) Then
                    s = Format(e.Item.DataItem("DateDoc"), "dd.MM.yyyy")
                Else
                    s = ""
                End If
                CType(e.Item.FindControl("lnkDateDoc"), Label).Text = s

                ' Информация о плательщике ТО
                s = ""
                If e.Item.DataItem("typeDoc") = "Приход" Then
                    s = "Поставка от " & Format(e.Item.DataItem("DateDoc"), "dd.MM.yyyy")
                    CType(e.Item.FindControl("lnkDocRecord"), HyperLink).NavigateUrl = "../Admin/Delivery.aspx?"
                    CType(e.Item.FindControl("lnkDocRecord"), HyperLink).Text = e.Item.DataItem("Document")
                Else
                    s = "Продажа №" & " 123 " & " от " & Format(e.Item.DataItem("DateDoc"), "dd.MM.yyyy")

                    CType(e.Item.FindControl("lnkDocRecord"), HyperLink).NavigateUrl = "../CustomerSales.aspx?" & e.Item.DataItem("customer")
                    CType(e.Item.FindControl("lnkDocRecord"), HyperLink).Text = e.Item.DataItem("Document")
                End If
                s = ""
                If Not IsDBNull(e.Item.DataItem("customer")) Then
                    s = (e.Item.DataItem("customer_name"))
                End If
                CType(e.Item.FindControl("lblCustomer"), Label).Text = s
                iNumber = iNumber + 1
                rest = rest + (e.Item.DataItem("ostatok"))
                CType(e.Item.FindControl("lblOstatok"), Label).Text = CStr(rest)

                CType(e.Item.FindControl("lblRecordNum"), Label).Text = iNumber
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblCurrentRest"), Label).Text = "Остаток на " + Format(Now, "dd.MM.yyyy")
                CType(e.Item.FindControl("lblCurrentOstatok"), Label).Text = CStr(rest)
                rest = 0

            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/TurnoverList.aspx"))
        End Sub

        Private Sub ugrdTurnoverListByGood_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles ugrdTurnoverListByGood.InitializeLayout
            With e.Layout
                .Bands(0).Columns.FromKey("good_name").HeaderText = "Товар"
                .Bands(0).Columns.FromKey("typeDoc").HeaderText = "Вид документа"
                .Bands(0).Columns.FromKey("dateDoc").HeaderText = "Дата документа"
                .Bands(0).Columns.FromKey("dateDoc").Format = "dd.MM.yyyy"
                .Bands(0).Columns.FromKey("document").HeaderText = "№ документа"
                '.Bands(0).Columns.FromKey("document").CellStyle.ForeColor = SystemColors.HotTrack
                '.Bands(0).Columns.FromKey("document").CellStyle.Font.Underline = True
                '.Bands(0).Columns.FromKey("document").CellStyle.Cursor = Infragistics.WebUI.UltraWebGrid.Cursors.Hand

                .Bands(0).Columns.FromKey("customer_name").HeaderText = "От кого получено, кому отпущено"
                .Bands(0).Columns.FromKey("prichod_Kol").HeaderText = "Приход"
                .Bands(0).Columns.FromKey("rashod_Kol").HeaderText = "Расход"
                .Bands(0).Columns.FromKey("Ostatok").HeaderText = "Остаток (начало)"
                .Bands(0).Columns.FromKey("Ostatok_Kol").HeaderText = "Остаток (конец)"

                .Bands(0).Columns.FromKey("good_name").IsGroupByColumn = True
                .Bands(0).Columns.FromKey("typeDoc").IsGroupByColumn = True

                .Bands(0).Columns.FromKey("typeDoc").Width = New Unit(100)
                .Bands(0).Columns.FromKey("dateDoc").Width = New Unit(83)
                .Bands(0).Columns.FromKey("document").Width = New Unit(300)
                .Bands(0).Columns.FromKey("customer_name").Width = New Unit(300)
                .Bands(0).Columns.FromKey("prichod_Kol").Width = New Unit(70)
                .Bands(0).Columns.FromKey("prichod_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("rashod_Kol").Width = New Unit(70)
                .Bands(0).Columns.FromKey("rashod_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("Ostatok").Width = New Unit(60)
                .Bands(0).Columns.FromKey("Ostatok").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("Ostatok_Kol").Width = New Unit(60)
                .Bands(0).Columns.FromKey("Ostatok_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right

                .Bands(0).Columns(0).Hidden = True
                .Bands(0).Columns(1).Hidden = True
                .Bands(0).Columns(2).Hidden = True
                .Bands(0).Columns.FromKey("good_type_sys_id").Hidden = True
                .Bands(0).Columns.FromKey("typeDoc").Hidden = True
                .Bands(0).Columns.FromKey("good_name").Hidden = True
                .Bands(0).Columns.FromKey("DocID").Hidden = True
                .Bands(0).Columns.FromKey("Customer").Hidden = True
                '.Bands(0).Columns.FromKey("Ostatok_Kol").Hidden = True
                .Bands(0).Columns.FromKey("delivery").Hidden = True
                .Bands(0).RowStyle.Font.Italic = True
                .Bands(0).RowStyle.Font.Bold = True
                .Bands(0).RowAlternateStyle.Font.Italic = True
                .Bands(0).RowAlternateStyle.Font.Bold = True

                .Bands(1).Columns.FromKey("dateDoc").HeaderText = "Дата документа"
                .Bands(1).Columns.FromKey("dateDoc").Format = "dd.MM.yyyy"

                .Bands(1).Columns.FromKey("document").HeaderText = "№ документа"
                '.Bands(1).Columns.FromKey("document").CellStyle.ForeColor = SystemColors.HotTrack
                '.Bands(1).Columns.FromKey("document").CellStyle.Font.Underline = True
                '.Bands(1).Columns.FromKey("document").CellStyle.Cursor = Infragistics.WebUI.UltraWebGrid.Cursors.Hand

                .Bands(1).Columns.FromKey("customer_name").HeaderText = "Кому отпущено"
                .Bands(1).Columns.FromKey("prichod_Kol").HeaderText = "Приход"
                .Bands(1).Columns.FromKey("rashod_Kol").HeaderText = "Расход"
                .Bands(1).Columns.FromKey("Ostatok").HeaderText = "Остаток (начало)"
                .Bands(1).Columns.FromKey("Ostatok_Kol").HeaderText = "Остаток (конец)"

                .Bands(1).Columns.FromKey("typeDoc").HeaderText = "Вид документа"
                .Bands(1).Columns.FromKey("typeDoc").IsGroupByColumn = True

                .Bands(1).Columns.FromKey("typeDoc").Width = New Unit(100)
                .Bands(1).Columns.FromKey("dateDoc").Width = New Unit(80)
                .Bands(1).Columns.FromKey("document").Width = New Unit(300)
                .Bands(1).Columns.FromKey("customer_name").Width = New Unit(300)
                .Bands(1).Columns.FromKey("prichod_Kol").Width = New Unit(70)
                .Bands(1).Columns.FromKey("prichod_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("rashod_Kol").Width = New Unit(70)
                .Bands(1).Columns.FromKey("rashod_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("Ostatok").Width = New Unit(60)
                .Bands(1).Columns.FromKey("Ostatok").CellStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("Ostatok_Kol").Width = New Unit(60)
                .Bands(1).Columns.FromKey("Ostatok_Kol").CellStyle.HorizontalAlign = HorizontalAlign.Right

                .Bands(1).Columns.FromKey("good_type_sys_id").Hidden = True
                .Bands(1).Columns.FromKey("typeDoc").Hidden = True
                .Bands(1).Columns.FromKey("good_name").Hidden = True
                .Bands(1).Columns.FromKey("DocID").Hidden = True
                .Bands(1).Columns.FromKey("Customer").Hidden = True
                '.Bands(1).Columns.FromKey("Ostatok").Hidden = True
                '.Bands(1).Columns.FromKey("Ostatok_Kol").Hidden = True
                .Bands(1).Columns.FromKey("delivery").Hidden = True
                .Bands(1).ColHeadersVisible = ShowMarginInfo.No
                .Bands(1).RowSelectors() = RowSelectors.No
                .Bands(1).Indentation = 1

                .ColFootersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes
                .Bands(0).Columns.FromKey("customer_name").FooterText = "Остаток на " & Format(Now, "dd.MM.yyyy") & " : "
                .Bands(0).Columns.FromKey("customer_name").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("customer_name").FooterStyle.Wrap = True

                '.Bands(0).Columns.FromKey("Ostatok").FooterTotal = SummaryInfo.Sum
                .Bands(0).Columns.FromKey("Ostatok").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("Ostatok").FooterStyle.Wrap = True
                .Bands(0).Columns.FromKey("Ostatok_Kol").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(0).Columns.FromKey("Ostatok_Kol").FooterStyle.Wrap = True

                .Bands(1).Columns.FromKey("rashod_Kol").FooterTotal = SummaryInfo.Sum
                .Bands(1).Columns.FromKey("rashod_Kol").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("rashod_Kol").FooterStyle.Wrap = True
                '.Bands(1).Columns.FromKey("Ostatok").FooterTotal = SummaryInfo.Min
                .Bands(1).Columns.FromKey("Ostatok").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("Ostatok").FooterStyle.Wrap = True
                .Bands(1).Columns.FromKey("Ostatok_Kol").FooterStyle.HorizontalAlign = HorizontalAlign.Right
                .Bands(1).Columns.FromKey("Ostatok_Kol").FooterStyle.Wrap = True

                .Bands(1).ColFootersVisible = ShowMarginInfo.No
                .Bands(1).FooterStyle.BackColor = Color.FromArgb(255, 255, 204)
                .Bands(1).FooterStyle.ForeColor = Color.Navy
                .Bands(0).FooterStyle.BackColor = Color.FromArgb(255, 255, 204)
                .Bands(0).FooterStyle.ForeColor = Color.Navy
            End With
        End Sub

        Private Sub ugrdTurnoverListByGood_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ugrdTurnoverListByGood.PreRender
            ugrdTurnoverListByGood.ExpandAll()
        End Sub

        Private Sub UltraGrid1_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles ugrdTurnoverListByGood.InitializeRow
            Dim objRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            '   Expand the first row in band zero
            objRow = e.Row.PrevRow
            If objRow Is Nothing And e.Row.Band.Index = 0 Then
                e.Row.Expanded = True
            End If
            '   Set the display text of the manufacturer's name cell
            If e.Row.Band.Index = 0 Then
                Session("Good_name") = e.Row.Cells.FromKey("good_name").Value
            End If
            '' Информация о плательщике ТО
            If e.Row.Cells.FromKey("typeDoc").Value = "Приход" Then
                e.Row.Cells.FromKey("document").Text = e.Row.Cells.FromKey("document").Value '"Поставка от " & Format(e.Row.Cells.FromKey("DateDoc").Value, "dd.MM.yyyy")
                '    e.Row.Cells.FromKey("document").Value = "../Admin/Delivery.aspx"
            Else
                e.Row.Cells.FromKey("document").Text = e.Row.Cells.FromKey("document").Value
                '    e.Row.Cells.FromKey("document").Value = "../CustomerSales.aspx?" & e.Row.Cells.FromKey("Customer").Value
            End If
            If e.Row.Band.Index = 0 Then
                'e.Row.Cells.FromKey("rashod_Kol").Value = rest
                If e.Row.Index = 0 Then
                    e.Row.Cells.FromKey("Ostatok").Value = e.Row.Cells.FromKey("prichod_Kol").Value
                    If e.Row.Rows.Count > 0 Then
                        e.Row.Cells.FromKey("Ostatok_Kol").Value = e.Row.Rows(e.Row.Rows.Count - 1).Cells.FromKey("Ostatok_Kol").Value
                    Else
                        e.Row.Cells.FromKey("Ostatok_Kol").Value = e.Row.Cells.FromKey("Ostatok").Value
                    End If
                Else
                    If objRow.Rows.Count > 0 Then
                        e.Row.Cells.FromKey("Ostatok").Value = objRow.Rows(objRow.Rows.Count - 1).Cells.FromKey("Ostatok_Kol").Value + e.Row.Cells.FromKey("prichod_Kol").Value
                    Else
                        e.Row.Cells.FromKey("Ostatok").Value = objRow.Cells.FromKey("Ostatok_Kol").Value + e.Row.Cells.FromKey("prichod_Kol").Value
                    End If
                    e.Row.Cells.FromKey("Ostatok_Kol").Value = e.Row.Cells.FromKey("Ostatok").Value - rest
                End If
                e.Row.Cells.FromKey("rashod_Kol").Value = rest
                rest = 0
            End If
            If e.Row.Band.Index = 1 Then
                rest = rest + e.Row.Cells.FromKey("rashod_Kol").Value
                If e.Row.Index = 0 Then
                    If e.Row.ParentRow.PrevRow Is Nothing Then
                        e.Row.Cells.FromKey("Ostatok").Value = e.Row.ParentRow.Cells.FromKey("Ostatok").Value
                    Else
                        e.Row.Cells.FromKey("Ostatok").Value = e.Row.ParentRow.Cells.FromKey("Ostatok").Value + e.Row.ParentRow.PrevRow.Cells.FromKey("Ostatok_Kol").Value
                    End If
                    e.Row.Cells.FromKey("Ostatok_Kol").Value = e.Row.Cells.FromKey("Ostatok").Value - e.Row.Cells.FromKey("rashod_Kol").Value
                Else
                    e.Row.Cells.FromKey("Ostatok").Value = objRow.Cells.FromKey("Ostatok_Kol").Value
                    e.Row.Cells.FromKey("Ostatok_Kol").Value = e.Row.Cells.FromKey("Ostatok").Value - e.Row.Cells.FromKey("rashod_Kol").Value
                End If
            End If
        End Sub

        Private Sub ugrdTurnoverListByGood_InitializeFooter(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.FooterEventArgs) Handles ugrdTurnoverListByGood.InitializeFooter
            If e.Rows.Band.Index = 1 Then
                e.Rows.Band.Columns.FromKey("Ostatok_Kol").FooterTotal = SummaryInfo.Text
                e.Rows.Band.Columns.FromKey("Ostatok_Kol").FooterText = e.Rows(e.Rows.Count - 1).Cells.FromKey("Ostatok_Kol").Value
            End If
            If e.Rows.Band.Index = 0 Then
                If e.Rows(e.Rows.Count - 1).Rows.Count > 0 Then
                    e.Rows.Band.Columns.FromKey("Ostatok_Kol").FooterText = e.Rows(e.Rows.Count - 1).Rows(e.Rows(e.Rows.Count - 1).Rows.Count - 1).Cells.FromKey("Ostatok_Kol").Value()
                Else
                    e.Rows.Band.Columns.FromKey("Ostatok_Kol").FooterText = e.Rows(e.Rows.Count - 1).Cells.FromKey("Ostatok_Kol").Value()
                End If
            End If
        End Sub



        'Private Sub ugrdTurnoverListByGood_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs) Handles ugrdTurnoverListByGood.Click
        '    If e.Cell.Column.Key = "document" Then
        '        If e.Cell.Row.Cells.FromKey("typeDoc").Value = "Приход" Then
        '            Response.Redirect("../Admin/Delivery.aspx")
        '        Else
        '            Response.Redirect("../CustomerSales.aspx?" & e.Cell.Row.Cells.FromKey("Customer").Value)
        '        End If

        '    End If


        'End Sub

        Private Sub lnkRestReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRestReport.Click
            Dim strRequest$ = "../documents.aspx?t=52&good_type_id=" & good_type_id
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub
    End Class

End Namespace
