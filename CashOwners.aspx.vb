Namespace Kasbi
    Partial Class CashOwners
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnSupport As System.Web.UI.WebControls.LinkButton
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents btnRepair As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lstDayDismissal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lstMonthDismissal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lstYearDismissal As System.Web.UI.WebControls.DropDownList


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim iNumber%, iCash%
        Dim CurrentCustomer%, iNewCust%
        'Const ClearString = "-------"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Dim ch() As Char = {","}
                iCash = Request.Params(0).Split(ch).GetValue(0)
                iNewCust = 0
                iNewCust = CInt(IIf(Request.Params("cashowner") <> "", Request.Params("cashowner"), 0))
            Catch
                msg.Text = "Неверный запрос"
                Exit Sub
            End Try

            If Not IsPostBack Then
                Parameters.Value = 0
                LoadGoodInfo()
                LoadSKNOInfo()
            Else
                lnkNewRepair.NavigateUrl = GetAbsoluteUrl("~/Repair.aspx?" & iCash)
                lnkSupportConduct.NavigateUrl = GetAbsoluteUrl("~/NewSupportConduct.aspx?" & iCash)
            End If
        End Sub

        Sub LoadSKNOInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Try
                cmd = New SqlClient.SqlCommand("get_last_skno_history")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                reader = dbSQL.GetReader(cmd)
                If reader.Read Then
                    If reader("state_SKNO") = 1 Then
                        lblSupportSKNO.Text = ", установлено СКНО"
                    End If
                End If
                reader.Close()
            Catch
                msg.Text = "Ошибка загрузки информации о установке СКНО!1<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try
        End Sub

        Sub LoadGoodInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            'Try
            cmd = New SqlClient.SqlCommand("get_cashregister_info")
            cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
            cmd.CommandType = CommandType.StoredProcedure
            reader = dbSQL.GetReader(cmd)
            If Not reader.Read Then
                msg.Text = "Ошибка загрузки информации о товаре! Невозможно выполнить запрос!<br>"
                Exit Sub
            End If
            lblCashType.Text = reader("name") & "&nbsp;&nbsp;"
            lblCash.Text = "№" & reader("num_cashregister")

            Dim s$, sTmp$
            Dim b As Boolean

            Dim data_embed As Boolean
            If lblCash.Enabled = True Then
                data_embed = False
            Else
                data_embed = True
            End If

            s = Trim(reader("num_control_reestr"))
            sTmp = Trim(reader("num_control_pzu"))
            s = s & " / " & sTmp
            sTmp = Trim(reader("num_control_mfp"))
            s = s & " / " & sTmp
            'If reader("good_type_sys_id") = Config.Kasbi04_ID Then
            'lblCaptionNumbers.Text = "СК Реестра/ПЗУ/МФП/ЦП:"
            sTmp = Trim(reader("num_control_cp"))
            s = s & " / " & sTmp
            'End If
            b = s.Length > 0
            If b Then lblNumbers.Text = s
            lblNumbers.Visible = b
            lblCaptionNumbers.Visible = b

            s = Trim(reader("marka"))
            If reader("good_type_sys_id") = Config.Kasbi04_ID Then
                sTmp = Trim(reader("num_control_cto2"))
                s = s & " / " & sTmp
                lblCaptionMarka.Text = "Марки ЦТО/ЦТО2:"
            End If
            b = s.Length > 0
            If b Then lblMarka.Text = s
            lblMarka.Visible = b
            lblCaptionMarka.Visible = b

            s = Trim(reader("date_created"))
            b = s.Length > 0
            If b Then lblDateCreated.Text = s
            lblDateCreated.Visible = b
            lblCaptionDateCreated.Visible = b

            Try
                s = Trim(reader("worker"))
            Catch
            End Try

            b = s.Length > 0
            If b Then lblWorker.Text = s
            lblWorker.Visible = b
            lblCaptionWorker.Visible = b

            s = Trim(reader("set_place"))
            b = s.Length > 0
            If b Then
                lblSetPlace.Text = s
            End If
            lblSetPlace.Visible = b
            lblCaptionSetPlace.Visible = b

            s = Trim(reader("kassir"))
            b = s.Length > 0
            If b Then lblKassir.Text = s
            lblKassir.Visible = b
            lblCaptionKassir.Visible = b

            CurrentCustomer = CInt(IIf(IsDBNull(reader("owner_sys_id")), 0, reader("owner_sys_id")))
            b = CurrentCustomer > 0

            If b Then
                s = Trim(reader("current_Owner"))
                lnkOwner.Text = s
                lnkOwner.NavigateUrl = GetAbsoluteUrl("~/Customer.aspx?" & CurrentCustomer & "&" & iCash)
            End If
            Parameters.Value = CurrentCustomer
            lblOwner.Visible = b
            lnkOwner.Visible = b
            lnkSupportConduct.NavigateUrl = GetAbsoluteUrl("~/NewSupportConduct.aspx?" & iCash)
            lblCash.NavigateUrl = GetAbsoluteUrl("~/CashOwners.aspx?" & iCash & "&cashowner=" & CurrentCustomer)
            s = Trim(reader("sale"))
            b = s.Length > 0
            If b Then
                lblSale.Text = s
                lblSale.NavigateUrl = GetAbsoluteUrl("~/CustomerSales.aspx?" & reader("customer_sys_id"))
            End If
            lblCaptionSale.Visible = b

            If Not IsDBNull(reader("garantia")) Then
                lblGarantia.Text = reader("garantia")
            End If
            If IsDBNull(reader("support")) OrElse reader("support") = 0 Then
                lblSupport.Text = "Не заключен договор на ТО"
                lblSupport.Enabled = False
                imgSupport.Visible = False
            Else
                If reader("support") = "1" Then
                    If reader("stateTO") = "0" Or reader("stateTO") = "1" Or reader("stateTO") = "4" Then
                        lblSupport.Text = "Находится на ТО"
                    ElseIf reader("stateTO") = "6" Then
                        lblSupport.Text = "ТО приостановлено"
                    ElseIf reader("stateTO") = "5" Then
                        lblSupport.Text = "Находится в ремонте"
                    End If

                    imgSupport.Visible = True
                    'imgSupport.NavigateUrl = GetAbsoluteUrl("~/Support.aspx?" & reader("owner_sys_id"))
                Else
                    If reader("stateTO") = "2" Then
                        lblSupport.Text = "Снят с ТО"
                    ElseIf reader("stateTO") = "3" Then
                        lblSupport.Text = "Снят с ТО и в ИМНС"
                    End If
                    imgSupport.Visible = False
                End If

            End If
            imgRepair.NavigateUrl = GetAbsoluteUrl("~/Repair.aspx?" & iCash)
            lnkNewRepair.NavigateUrl = GetAbsoluteUrl("~/Repair.aspx?" & iCash)

            If IsDBNull(reader("alert")) OrElse Trim(reader("alert")).Length = 0 Then
                imgAlert.Visible = False
            Else
                imgAlert.Visible = True
                imgAlert.ToolTip = reader("alert")
            End If
            reader.Close()
            ShowRepairImage()
            'Catch
            'msg.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
            'Exit Sub
            'End Try
            GetHistory(iCash)
            GetRepairHistory(iCash)
        End Sub

        Sub ShowRepairImage()
            Try
                Dim st% = dbSQL.Execute("select dbo.udf_repair(" & iCash & ")")
                imgRepair.Visible = st > 0
            Catch
            End Try
        End Sub

        Sub GetRepairHistory(ByVal cashRegister As Integer)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_repair_history")
                cmd.Parameters.AddWithValue("@good_sys_id", cashRegister)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).DefaultView.Count = 0 Then
                    msgRepairHistory.Text = ""
                    lblRepairInfos.Text = "Информация по ремонту отсутствует"
                    grdRepairs.Visible = False
                Else
                    grdRepairs.Visible = True
                    msgRepairHistory.Text = ""
                    iNumber = 1
                    grdRepairs.DataSource = ds.Tables(0).DefaultView
                    grdRepairs.DataKeyField = "sys_id"
                    grdRepairs.DataBind()
                End If
            Catch
                msgRepairHistory.Text = "Ошибка загрузки истории ремонтов товара!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub GetHistory(ByVal cashRegister As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_cashowner_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@good_sys_id", cashRegister)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).DefaultView.Count = 0 Then
                    msgOwners.Text = ""
                    grdCashOwnerHistory.Visible = False
                    lblOwnerInfo.Text = "Договор на ТО не заключен"
                Else
                    grdCashOwnerHistory.Visible = True
                    msgOwners.Text = ""
                    iNumber = 1
                    grdCashOwnerHistory.DataSource = ds
                    grdCashOwnerHistory.DataKeyField = "sys_id"
                    grdCashOwnerHistory.DataBind()
                End If
            Catch
                msgOwners.Text = Err.Description
            End Try
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String =
                    {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ",
                     " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDateFull(ByVal d As Date) As String
            Dim m() As String =
                    {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ",
                     " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDateFull = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Private Sub grdCashOwnerHistory_ItemDataBound(ByVal sender As Object,
                                                      ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdCashOwnerHistory.ItemDataBound
            Dim s As String

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                'Dates
                If Not IsDBNull(e.Item.DataItem("updateDate")) Then
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID") & "<br>" &
                        Format(e.Item.DataItem("updateDate"), "dd.MM.yyyy HH:mm")
                Else
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID")
                End If
                CType(e.Item.FindControl("lblUpdateRec"), Label).Text = s

                'определяем тип ТО
                Dim d As Date
                If Not IsDBNull(e.Item.DataItem("state")) Then
                    ' проведение ТО
                    If e.Item.DataItem("state") = 6 Then
                        'приостановка ТО
                        s = ""
                        If Not IsDBNull(e.Item.DataItem("start_date")) Then
                            d = e.Item.DataItem("start_date")
                            s = GetRussianDateFull(d)
                        Else
                            s = ""
                        End If
                        s = "ТО приостановлено c <br>" & s & " на " & e.Item.DataItem("period").ToString & " мес."
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 2 Then
                        'снятие с ТО
                        If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                            d = e.Item.DataItem("dismissal_date")
                            s = "Снят с ТО " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=14&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=19&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).Text = "(Письмо имнс)"
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=55&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id") & "&addp=4")

                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " &
                                e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " &
                                    e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") &
                                "<br>"
                            s = s & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " &
                                e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If

                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 3 Then
                        'снятие с ТО и в ИМНС
                        If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                            d = e.Item.DataItem("dismissal_date")
                            s = "Снят с ТО и в ИМНС " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=15&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=20&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).Text = "(Письмо имнс)"
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=55&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id") & "&addp=4")
                            CType(e.Item.FindControl("lnkZayav"), HyperLink).Text = "(Заявление на снятие)"
                            CType(e.Item.FindControl("lnkZayav"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=56&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id") & "&addp=4")

                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " &
                                e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " &
                                    e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") &
                                "<br>"
                            s = s & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " &
                                e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If

                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 4 Then
                        'постановка на ТО
                        If Not IsDBNull(e.Item.DataItem("support_date")) Then
                            d = e.Item.DataItem("support_date")
                            s = "Поставлен на ТО " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=11&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=12&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkDogovor_Na_TO"), HyperLink).Text = "Дог. на ТО"
                            CType(e.Item.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=13&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).Text = "(Письмо имнс)"
                            CType(e.Item.FindControl("lnkLetter_IMNS"), HyperLink).NavigateUrl =
                                GetAbsoluteUrl(
                                    "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                                    e.Item.DataItem("sale_sys_id") & "&t=55&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                                    e.Item.DataItem("sys_id") & "&addp=2")


                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " &
                                e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " &
                                    e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") &
                                "<br>"
                            s = s & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " &
                                e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If

                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    End If

                End If
                ' Информация о плательщике ТО
                s = ""
                If Not IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    s = GetInfo(CInt(e.Item.DataItem("owner_sys_id")), False)
                End If

                CType(e.Item.FindControl("lnkPayer"), HyperLink).Text = s
                If (e.Item.DataItem("state") = 4) Then
                    CType(e.Item.FindControl("lnkPayer"), HyperLink).NavigateUrl =
                        GetAbsoluteUrl("~/CustomerSales.aspx?" & e.Item.DataItem("owner_sys_id"))
                End If
                ' Информация о исполнителе
                s = ""
                If Not IsDBNull(e.Item.DataItem("executor")) Then
                    s = e.Item.DataItem("executor")
                End If

                CType(e.Item.FindControl("lblExecutorTO"), Label).Text = s


                ' дополнительная информация
                CType(e.Item.FindControl("lblAddInfo"), Label).Text = e.Item.DataItem("info")
                'дата когда закрыто ТО за месяц
                s = ""
                If Not IsDBNull(e.Item.DataItem("change_state_date")) Then
                    s = s & Format(e.Item.DataItem("change_state_date"), "dd/MM/yyyy")
                End If
                CType(e.Item.FindControl("lblCloseDate"), Label).Text = s
            End If
        End Sub

        Private Sub grdRepairs_ItemDataBound(ByVal sender As Object,
                                             ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdRepairs.ItemDataBound
            Dim s$

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim bOneOfAll As Boolean = False
                Dim isLabelShow As Boolean = False

                'Dates
                If Not IsDBNull(e.Item.DataItem("updateDate")) Then
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID") & "<br>" &
                        Format(e.Item.DataItem("updateDate"), "dd.MM.yyyy HH:mm")
                Else
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID")
                End If
                CType(e.Item.FindControl("lblUpdateRec1"), Label).Text = s
                If IsDBNull(e.Item.DataItem("date_in"))
                    s = "??.??.????" & " / "
                Else
                    s = Format(e.Item.DataItem("date_in"), "dd.MM.yyyy") & " / "
                End If
                If IsDBNull(e.Item.DataItem("date_out")) Then
                    s = s & "??.??.????"
                    isLabelShow = False
                Else
                    s = s & Format(e.Item.DataItem("date_out"), "dd.MM.yyyy")

                    'CType(e.Item.FindControl("lnkActRepairRealization"), HyperLink).Text = "Акт о проведении ремонта"
                    'CType(e.Item.FindControl("lnkActRepairRealization"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=32&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))

                    CType(e.Item.FindControl("lnkRepairAct"), HyperLink).Text = "Акт"
                    CType(e.Item.FindControl("lnkRepairAct"), HyperLink).NavigateUrl =
                        GetAbsoluteUrl(
                            "~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" &
                            e.Item.DataItem("sale_sys_id") & "&t=16&g=" & e.Item.DataItem("good_sys_id") & "&h=" &
                            e.Item.DataItem("sys_id"))

                    CType(e.Item.FindControl("btnDeleteRepairDoc"), LinkButton).Text = "Удалить<br>документы"
                    isLabelShow = True
                End If
                CType(e.Item.FindControl("lblDates"), Label).Text = s
                s = "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") &
                    "<br>"
                If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                    s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " &
                        e.Item.DataItem("marka_cto2_out") & "<br>"
                End If
                s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " &
                    e.Item.DataItem("marka_reestr_out") & "<br>"
                s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") &
                    "<br>"
                s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") &
                    "<br>"
                'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") &
                    "<br>"
                'End If
                s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") &
                    "<br>"
                s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                CType(e.Item.FindControl("lblStatus"), Label).Text = s

                ''Марка ЦТО
                'CType(e.Item.FindControl("lblMarkaCTOIn"), Label).Text = e.Item.DataItem("marka_cto_in")
                'CType(e.Item.FindControl("lblMarkaCTOOut"), Label).Text = e.Item.DataItem("marka_cto_out")
                ''Марка ЦТО
                'CType(e.Item.FindControl("lblMarkaCTO2In"), Label).Text = e.Item.DataItem("marka_cto2_in")
                'CType(e.Item.FindControl("lblMarkaCTO2Out"), Label).Text = e.Item.DataItem("marka_cto2_out")
                ''Марка Reestr
                'CType(e.Item.FindControl("lblMarkaReestrIn"), Label).Text = e.Item.DataItem("marka_reestr_in")
                'CType(e.Item.FindControl("lblMarkaReestrOut"), Label).Text = e.Item.DataItem("marka_reestr_out")

                ''Марка ПЗУ
                'CType(e.Item.FindControl("lblMarkaPZUIn"), Label).Text = e.Item.DataItem("marka_pzu_in")
                'CType(e.Item.FindControl("lblMarkaPZUOut"), Label).Text = e.Item.DataItem("marka_pzu_out")
                ''Марка МФП
                'CType(e.Item.FindControl("lblMarkaMFPIn"), Label).Text = e.Item.DataItem("marka_mfp_in")
                'CType(e.Item.FindControl("lblMarkaMFPOut"), Label).Text = e.Item.DataItem("marka_mfp_out")
                ''Марка МФП
                'CType(e.Item.FindControl("lblMarkaCPIn"), Label).Text = e.Item.DataItem("marka_cp_in")
                'CType(e.Item.FindControl("lblMarkaCPOut"), Label).Text = e.Item.DataItem("marka_cp_out")

                ''Номер Z-отчёта
                'CType(e.Item.FindControl("lblZReportIn"), Label).Text = e.Item.DataItem("zreport_in")
                'CType(e.Item.FindControl("lblZReportOut"), Label).Text = e.Item.DataItem("zreport_out")

                ''Марка необнуляемого итога
                'CType(e.Item.FindControl("lblItogIn"), Label).Text = e.Item.DataItem("itog_in")
                'CType(e.Item.FindControl("lblItogOut"), Label).Text = e.Item.DataItem("itog_out")

                'Summa
                CType(e.Item.FindControl("lblCost"), Label).Text =
                    IIf(IsDBNull(e.Item.DataItem("summa")), "по гарантии", e.Item.DataItem("summa")).ToString()
                'ProcessControl(e.Item, "summa", "Cost", bOneOfAll, isLabelShow)
                'исполнитель
                'ProcessControl(e.Item, "executor", "Executor", bOneOfAll, isLabelShow)

                'почемуто иногда бывает пустая строка
                Try
                    CType(e.Item.FindControl("lblExecutor"), Label).Text =
                        IIf(IsDBNull(e.Item.DataItem("executor")), "", e.Item.DataItem("executor")).ToString()
                Catch
                End Try

                'Details
                CType(e.Item.FindControl("lblDetails"), Label).Text =
                    IIf(IsDBNull(e.Item.DataItem("details")), "", e.Item.DataItem("details")).ToString()
                'ProcessControl(e.Item, "details", "Details", bOneOfAll, isLabelShow)
                'Работы
                CType(e.Item.FindControl("lblInfo"), Label).Text =
                    IIf(IsDBNull(e.Item.DataItem("info")), "", e.Item.DataItem("info")).ToString()
                'Доп информация
                CType(e.Item.FindControl("lblRepairInfo"), Label).Text =
                    IIf(IsDBNull(e.Item.DataItem("repair_info")), "", e.Item.DataItem("repair_info")).ToString()

                'ProcessControl(e.Item, "info", "Info", bOneOfAll, isLabelShow)
                ' Информация о плательщике ТО
                s = ""
                If Not IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    s = GetInfo(CInt(e.Item.DataItem("owner_sys_id")), False)
                End If
                CType(e.Item.FindControl("lblPayerRepair"), Label).Text = s
            End If
        End Sub

        Function GetInfo(ByVal cust As Integer, Optional ByVal flag As Boolean = True) As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            s = ""
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).DefaultView(0)
                        'Dim d As Date = .Item("end_date")
                        Dim sTmp$

                        sTmp = .Item("customer_name")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                        sTmp = .Item("unn")
                        If sTmp.Length > 0 Then
                            s = s & "УНП: " & sTmp & "<br>"
                        End If

                        sTmp = .Item("registration")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = "по " & .Item("tax_inspection")
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


                    End With
                End If
            Catch
                msgOwners.Text = Err.Description
            End Try
            GetInfo = s
        End Function

        Private Sub btnExpand1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnExpand1.Click
            Expand(pnlOwnerHistory_body, btnExpand1)
        End Sub

        Private Sub btnExpand2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnExpand2.Click
            Expand(pnlRepairInfo_body, btnExpand2)
        End Sub

        Private Sub Expand(ByVal section As System.Web.UI.WebControls.Panel,
                           ByVal button As System.Web.UI.WebControls.ImageButton)
            If section.Visible = True Then
                section.Visible = False
                button.ImageUrl = "Images/collapsed.gif"
                button.ToolTip = "Показать"
            Else
                section.Visible = True
                button.ImageUrl = "Images/expanded.gif"
                button.ToolTip = "Скрыть"
            End If
        End Sub

        Private Sub grdCashOwnerHistory_DeleteCommand(ByVal source As System.Object,
                                                      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdCashOwnerHistory.DeleteCommand
            Dim docs As New Kasbi.Migrated_Documents
            Try
                docs.DeleteHistoryDocument(grdCashOwnerHistory.DataKeys(e.Item.ItemIndex))
            Catch
                msg.Text = "Ошибка удаления документов!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub grdRepairs_DeleteCommand(ByVal source As System.Object,
                                             ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdRepairs.DeleteCommand
            Dim docs As New Kasbi.Migrated_Documents
            Try
                docs.DeleteHistoryDocument(grdRepairs.DataKeys(e.Item.ItemIndex))
            Catch
                msg.Text = "Ошибка удаления документов!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub lnkGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkGoods.Click
            Dim sFilter$ = "num_cashregister like '%" & lblCash.Text.TrimStart("№").Trim() & "%'"
            If sFilter.Length > 0 Then
                Session("FilterGood") = sFilter
            End If
            Response.Redirect(GetAbsoluteUrl("~/GoodList.aspx"))
        End Sub
    End Class
End Namespace
