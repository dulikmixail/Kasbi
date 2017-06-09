Namespace Kasbi

    Partial Class Repair
        Inherits PageBase
        Dim sCaptionAddSupport As String = "Поставить на ТО"
        Dim CurrentCustomer%, iNewCust%
        Dim iCash%
        Dim sCaptionRemoveSupport As String = "Снять с ТО"

        Protected WithEvents lnkRepairIN_OUT As System.Web.UI.WebControls.LinkButton

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
            Dim action = CInt(IIf(Request.Params("action") <> "", Request.Params("action"), 0))
            Dim param = CInt(IIf(Request.Params("param") <> "", Request.Params("param"), 0))


            'Ограничение прав
            If Session("rule21") = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)

            Try
                iCash = Request.Params(0)
            Catch
                msg.Text = "Неверный запрос"
                Exit Sub
            End Try
            If Not IsPostBack Then
                LoadGoodInfo()
                BindGrid()
            End If
        End Sub

        Sub LoadGoodInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Try
                cmd = New SqlClient.SqlCommand("get_cashregister_info")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                reader = dbSQL.GetReader(cmd)
                If Not reader.Read Then
                    msg.Text = "Ошибка загрузки информации о товаре!<br>"
                    Exit Sub
                End If
                lblCashType.Text = reader("name") & "&nbsp;&nbsp;"
                lblCash.Text = "№" & reader("num_cashregister")
                Dim s$, sTmp$
                Dim b As Boolean

                s = Trim(reader("num_control_reestr"))
                sTmp = Trim(reader("num_control_pzu"))
                s = s & " / " & sTmp
                sTmp = Trim(reader("num_control_mfp"))
                s = s & " / " & sTmp
                'If reader("good_type_sys_id") = Config.Kasbi04_ID Then
                '    lblCaptionNumbers.Text = "СК Реестра/ПЗУ/МФП/ЦП:"
                sTmp = Trim(reader("num_control_cp"))
                s = s & " / " & sTmp
                ' End If

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

                s = Trim(reader("worker"))
                b = s.Length > 0
                If b Then lblWorker.Text = s
                lblWorker.Visible = b
                lblCaptionWorker.Visible = b

                s = Trim(reader("set_place"))
                b = s.Length > 0
                If b Then lblSetPlace.Text = s
                lblSetPlace.Visible = b
                lblCaptionSetPlace.Visible = b

                CurrentCustomer = CInt(IIf(IsDBNull(reader("owner_sys_id")), 0, reader("owner_sys_id")))
                If CurrentCustomer = 0 And Not IsDBNull(reader("customer_sys_id")) Then
                    CurrentCustomer = CInt(reader("customer_sys_id"))
                End If
                b = CurrentCustomer > 0 And Not IsDBNull(reader("owner_sys_id"))
                If b Then
                    s = Trim(reader("current_Owner"))
                    lnkOwner.Text = s
                    lnkOwner.NavigateUrl = GetAbsoluteUrl("~/Customer.aspx?" & CurrentCustomer & "&" & iCash)
                End If
                Parameters.Value = CurrentCustomer
                lblOwner.Visible = b
                lnkOwner.Visible = b
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
                        ' imgSupport.NavigateUrl = GetAbsoluteUrl("~/Support.aspx?" & reader("owner_sys_id"))
                    Else
                        If reader("stateTO") = "2" Then
                            lblSupport.Text = "Снят с ТО"
                        ElseIf reader("stateTO") = "3" Then
                            lblSupport.Text = "Снят с ТО и в ИМНС"
                        End If
                        imgSupport.Visible = False
                    End If

                End If

                If IsDBNull(reader("alert")) OrElse Trim(reader("alert")).Length = 0 Then
                    imgAlert.Visible = False
                Else
                    imgAlert.Visible = True
                    imgAlert.ToolTip = reader("alert")
                End If
                reader.Close()
                ShowRepairImage()
            Catch
                msg.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try

        End Sub

        Sub BindGrid()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_repair_history")
                cmd.Parameters.AddWithValue("@good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                grdRepairs.DataSource = ds.Tables(0).DefaultView
                grdRepairs.DataKeyField = "sys_id"
                grdRepairs.DataBind()
            Catch
                msgHistory.Text = "Ошибка загрузки истории ремонтов товара!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub ShowRepairImage()
            Try
                imgRepair.Visible = dbSQL.ExecuteScalar("select dbo.udf_repair(" & iCash & ")") > 0
            Catch
            End Try
        End Sub

        Function GetNewAktNumber() As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            CurrentCustomer = Parameters.Value
            'новый номер договора
            Try
                cmd = New SqlClient.SqlCommand("get_next_repair_akt")
                cmd.Parameters.AddWithValue("@good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                Dim s
                Dim num_cashregister
                s = ds.Tables(0).Rows(0).Item("num_repairs")
                num_cashregister = ds.Tables(0).Rows(0).Item("num_cashregister")
                num_cashregister = Trim(num_cashregister)
                s = s + 1

                GetNewAktNumber = num_cashregister & "/" & Date.Now.Month & "/" & s
            Catch
                Return ""
            End Try
        End Function

        Private Sub lnkRepairIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRepairIN.Click

            Dim cmd As SqlClient.SqlCommand
            CurrentCustomer = Parameters.Value
            Try
                Dim akt$ = GetNewAktNumber()
                If akt Is Nothing Then
                    akt = ""
                End If

                cmd = New SqlClient.SqlCommand("new_repair")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.Parameters.AddWithValue("@pi_owner_sys_id", CurrentCustomer)
                cmd.Parameters.AddWithValue("@pi_date_in", Now)
                cmd.Parameters.AddWithValue("@pi_date_out", DBNull.Value)
                cmd.Parameters.AddWithValue("@pi_marka_cto_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_pzu_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_pzu_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_mfp_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_mfp_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_reestr_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_reestr_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto2_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto2_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_cp_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cp_out", "")
                cmd.Parameters.AddWithValue("@pi_zreport_in", "")
                cmd.Parameters.AddWithValue("@pi_zreport_out", "")
                cmd.Parameters.AddWithValue("@pi_itog_in", "")
                cmd.Parameters.AddWithValue("@pi_itog_out", "")
                cmd.Parameters.AddWithValue("@pi_details", "")
                cmd.Parameters.AddWithValue("@pi_akt", akt)
                cmd.Parameters.AddWithValue("@pi_summa", "")
                cmd.Parameters.AddWithValue("@pi_info", "")
                cmd.Parameters.AddWithValue("@pi_repair_info", "")
                cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@pi_repair_in", 1)
                cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@repare_in_info", " ")
                cmd.CommandType = CommandType.StoredProcedure
                dbSQL.Execute(cmd)
                msgHistory.Text = "Запись о ремонте успешно добавлена!"

                BindGrid()
                ShowRepairImage()

                Dim query = dbSQL.ExecuteScalar("Update good SET inrepair='1' WHERE good_sys_id='" & iCash & "'")

            Catch
                msgHistory.Text = "Ошибка добавления информации о ремонте!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub grdRepairs_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRepairs.ItemDataBound
            Dim s$



            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim bOneOfAll As Boolean = False
                Dim isLabelShow As Boolean = False


                'Dates
                If Not IsDBNull(e.Item.DataItem("updateDate")) Then
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID") & "<br>" & Format(e.Item.DataItem("updateDate"), "dd.MM.yyyy HH:mm")
                Else
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID")
                End If
                CType(e.Item.FindControl("lblUpdateRec"), Label).Text = s

                s = Format(e.Item.DataItem("date_in"), "dd.MM.yyyy HH:mm") & " / "
                If IsDBNull(e.Item.DataItem("date_out")) Then
                    s = s & "??.??.????"
                    isLabelShow = False
                    CType(e.Item.FindControl("lnkActRepairIN_OUT"), HyperLink).Text = "Акт о принятии в ремонт"
                    CType(e.Item.FindControl("lnkActRepairIN_OUT"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=31&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))

                Else
                    s = s & Format(e.Item.DataItem("date_out"), "dd.MM.yyyy ")
                    CType(e.Item.FindControl("lnkActRepairIN_OUT"), HyperLink).Text = "Акт о принятии в ремонт"
                    CType(e.Item.FindControl("lnkActRepairIN_OUT"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=31&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                    Dim sale%
                    If IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                        sale = e.Item.DataItem("sale_sys_id_first")
                    Else
                        sale = e.Item.DataItem("sale_sys_id")
                    End If
                    CType(e.Item.FindControl("lnkRepairAct"), HyperLink).Text = "Акт"
                    CType(e.Item.FindControl("lnkRepairAct"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & sale & "&t=16&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                    Dim garantia As Boolean = True
                    garantia = (IsDBNull(e.Item.DataItem("summa")) Or e.Item.DataItem("summa") = "") Or (e.Item.DataItem("garantia_repair"))

                    If garantia = False Then
                        CType(e.Item.FindControl("lnkActRepairRealization"), HyperLink).Text = "Акт о проведении ремонта"
                        CType(e.Item.FindControl("lnkActRepairRealization"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=32&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                        'Documents.aspx?t=32&c=" & CurrentCustomer & "&g=" & iCash & "&details=" & s & "&w=" & WorkNotCall & "'
                        CType(e.Item.FindControl("lnkTTNRepair"), HyperLink).Text = "Накладная"
                        CType(e.Item.FindControl("lnkTTNRepair"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=33&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                        CType(e.Item.FindControl("lnkInvoiceNDS"), HyperLink).Text = "Счет-фактура по НДС"
                        CType(e.Item.FindControl("lnkInvoiceNDS"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?t=34&c=" & e.Item.DataItem("owner_sys_id") & "&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))

                    End If

                    CType(e.Item.FindControl("btnDeleteRepairDoc"), LinkButton).Text = "Удалить<br>документы"

                    If CurrentUser.is_admin = True Then
                        CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                    Else
                        CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                    End If
                    isLabelShow = True
                End If
                CType(e.Item.FindControl("lblDates"), Label).Text = s

                s = "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") & "<br>"
                If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                    s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " & e.Item.DataItem("marka_cto2_out") & "<br>"
                End If
                s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " & e.Item.DataItem("marka_reestr_out") & "<br>"
                s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") & "<br>"
                s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") & "<br>"
                'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") & "<br>"
                'End If
                s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") & "<br>"
                s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                CType(e.Item.FindControl("lblStatus"), Label).Text = s

                ''Марка ЦТО
                'CType(e.Item.FindControl("lblMarkaCTOIn"), Label).Text = e.Item.DataItem("marka_cto_in")
                'CType(e.Item.FindControl("lblMarkaCTOOut"), Label).Text = e.Item.DataItem("marka_cto_out")
                ''Марка ЦТО2
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
                ''Марка ЦП
                'CType(e.Item.FindControl("lblMarkaCPIn"), Label).Text = e.Item.DataItem("marka_cp_in")
                'CType(e.Item.FindControl("lblMarkaCPOut"), Label).Text = e.Item.DataItem("marka_cp_out")


                ''Номер Z-отчёта
                'CType(e.Item.FindControl("lblZReportIn"), Label).Text = e.Item.DataItem("zreport_in")
                'CType(e.Item.FindControl("lblZReportOut"), Label).Text = e.Item.DataItem("zreport_out")

                ''сумма необнуляемого итога
                'CType(e.Item.FindControl("lblItogIn"), Label).Text = e.Item.DataItem("itog_in")
                'CType(e.Item.FindControl("lblItogOut"), Label).Text = e.Item.DataItem("itog_out")

                'Summa
                CType(e.Item.FindControl("lblCost"), Label).Text = IIf(IsDBNull(e.Item.DataItem("summa")) Or e.Item.DataItem("summa") = "", "по гарантии", CStr(e.Item.DataItem("summa")))

                'исполнитель
                If Session("rule22") = 0 Or Session("rule22") = "" Then
                    e.Item.FindControl("cmdDelete").Visible = False
                End If
                If Session("rule28") = 0 Or Session("rule28") = "" Then
                    e.Item.FindControl("cmdEdit").Visible = False
                End If



                'почемуто иногда бывает пустая строка
                Try

                    CType(e.Item.FindControl("lblExecutor"), Label).Text = e.Item.DataItem("executor")
                Catch
                End Try


                'Details
                CType(e.Item.FindControl("lblDetails"), Label).Text = e.Item.DataItem("details")

                'Работы
                CType(e.Item.FindControl("lblInfo"), Label).Text = e.Item.DataItem("info")
                'Доп информация
                CType(e.Item.FindControl("lblRepairInfo"), Label).Text = e.Item.DataItem("repair_info")

                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "return confirm('Вы действительно хотите удалить запись о ремонте?');")

            End If
        End Sub

        Private Sub grdRepairs_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRepairs.EditCommand
            grdRepairs.EditItemIndex = CInt(e.Item.ItemIndex)
            Response.Redirect(GetAbsoluteUrl("~/RepairNew.aspx?cash=" & iCash & "&hc=" & grdRepairs.DataKeys(e.Item.ItemIndex)))
        End Sub

        Private Sub grdRepairs_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRepairs.DeleteCommand
            'Ограничение прав на удаление
            If Session("rule22") = "1" Then
                Dim cmd As SqlClient.SqlCommand

                Try
                    cmd = New SqlClient.SqlCommand("remove_repair")
                    cmd.Parameters.AddWithValue("@pi_hc_sys_id", grdRepairs.DataKeys(e.Item.ItemIndex))
                    cmd.CommandType = CommandType.StoredProcedure
                    dbSQL.Execute(cmd)

                    Dim query = dbSQL.ExecuteScalar("UPDATE good SET inrepair=null WHERE good_sys_id='" & iCash & "'")
                Catch
                    msgHistory.Text = "Ошибка удаления записи!<br>" & Err.Description
                End Try
                grdRepairs.EditItemIndex = -1
                BindGrid()
                ShowRepairImage()
            End If
        End Sub

        Private Sub grdRepairs_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRepairs.ItemCommand
            If e.CommandName = "DeleteDoc" Then
                Dim d As New Kasbi.Migrated_Documents
                Try
                    d.DeleteHistoryDocument(grdRepairs.DataKeys(e.Item.ItemIndex))
                Catch
                    msg.Text = "Ошибка удаления документов!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
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
