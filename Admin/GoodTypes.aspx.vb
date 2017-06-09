Namespace Kasbi.Admin

    Partial Class GoodTypes
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lstCountry As System.Web.UI.WebControls.ListBox


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Const ClearString = "-------"
        Const javascript = "javascript:"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            BindCountryList()
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Sub ShowDescription(ByVal sender As Object, ByVal e As System.EventArgs)
            If cbxShowDescription.Checked = True Then
                grdGoodTypes.Columns(7).Visible = True
            Else
                grdGoodTypes.Columns(7).Visible = False
            End If
        End Sub

        Private Sub grdGoodTypes_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdGoodTypes.SortCommand
            If ViewState("goodsort") = e.SortExpression Then
                ViewState("goodsort") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort") = e.SortExpression
            End If
            Bind()
        End Sub

        Sub BindPriceTOList(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            'составляем список групп цен на ТО
            Try
                adapt = dbSQL.GetDataAdapter("select * from priceto order by group_id")
                ds = New DataSet
                adapt.Fill(ds)
                With lst
                    .DataSource = ds.Tables(0).DefaultView
                    .DataTextField = "group_id"
                    .DataValueField = "group_id"
                    .DataBind()
                    .Items.Insert(0, New ListItem(ClearString, ""))
                    Try
                        .Items.FindByValue(s).Selected = True
                    Catch ex As Exception

                    End Try
                End With
            Catch
                msgError.Text = "Ошибка формирования списка цен на ТО!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand()
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter("prc_get_good_type", True)
                ds = New DataSet
                adapt.Fill(ds)

                If ViewState("goodsort") = "" Then
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("goodsort")
                End If

                grdGoodTypes.DataSource = ds.Tables(0).DefaultView
                grdGoodTypes.DataKeyField = "good_type_sys_id"
                grdGoodTypes.DataBind()
            Catch
                msgError.Text = "Ошибка загрузки информации о типах товара!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdGoodTypes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoodTypes.ItemDataBound
            Dim top% = 140
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If

            ElseIf e.Item.ItemType = ListItemType.Footer Then

                Dim btnCountryNew As Label = CType(e.Item.FindControl("btnCountryNew"), Label)
                Dim tbxCountryNew As TextBox = CType(e.Item.FindControl("tbxCountryNew"), TextBox)
                Dim sHide$ = lstCountryNew.ClientID & ".style.display='"
                Dim sSetValue$ = tbxCountryNew.ClientID & ".value=this.options[this.selectedIndex].text;"
                btnCountryNew.Attributes.Add("onclick", javascript & sHide & "block';" & lstCountryNew.ClientID & ".focus();" & lstCountryNew.ClientID & ".style.top=" & (top + grdGoodTypes.Items.Count * 20).ToString() & " ;")
                tbxCountryNew.Attributes.Add("ondblclick", javascript & sHide & "block';" & lstCountryNew.ClientID & ".focus();")
                lstCountryNew.Attributes.Add("onchange", javascript & sSetValue & sHide & "none';")
                lstCountryNew.Attributes.Add("onfocusout", javascript & sHide & "none';")

                Dim lst As DropDownList = CType(e.Item.FindControl("lstGoodGroupNew"), DropDownList)
                BindGoodGroupList(lst, 0)
                'ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim lst2 As DropDownList = CType(e.Item.FindControl("lstPriceTO"), DropDownList)
                BindPriceTOList(lst2, 0)

            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim btnCountryEdit As Label = CType(e.Item.FindControl("btnCountryEdit"), Label)
                Dim tbxCountryEdit As TextBox = CType(e.Item.FindControl("tbxCountryEdit"), TextBox)
                tbxCountryEdit.Text = e.Item.DataItem("country")

                Dim sHide$ = lstCountryEdit.ClientID & ".style.display='"
                Dim sSetValue$ = tbxCountryEdit.ClientID & ".value=this.options[this.selectedIndex].text;"
                btnCountryEdit.Attributes.Add("onclick", javascript & sHide & "block';" & lstCountryEdit.ClientID & ".focus();" & lstCountryEdit.ClientID & ".style.top=" & (top + e.Item.ItemIndex * 20).ToString() & " ;")
                tbxCountryEdit.Attributes.Add("ondblclick", javascript & sHide & "block';" & lstCountryEdit.ClientID & ".focus();")
                lstCountryEdit.Attributes.Add("onchange", javascript & sSetValue & sHide & "none';")
                lstCountryEdit.Attributes.Add("onfocusout", javascript & sHide & "none';")

                If IsDBNull(e.Item.DataItem("good_group_sys_id")) Then
                    BindGoodGroupList(CType(e.Item.FindControl("lstGoodGroupEdit"), DropDownList), ClearString)
                Else
                    BindGoodGroupList(CType(e.Item.FindControl("lstGoodGroupEdit"), DropDownList), Trim(e.Item.DataItem("group_name")))
                End If

                If IsDBNull(e.Item.DataItem("price_to")) Then
                    BindPriceTOList(CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList), ClearString)
                Else
                    BindPriceTOList(CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList), e.Item.DataItem("price_to"))
                End If
            End If
        End Sub

        Private Sub grdGoodTypes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodTypes.ItemCommand
            If e.CommandName = "AddGoodType" Then
                Dim txtName As TextBox = CType(e.Item.FindControl("tbxNameNew"), TextBox)
                If txtName.Text = String.Empty Then
                    msgError.Text = "Необходимо заполнить"
                    Exit Sub
                End If

                Dim reader As SqlClient.SqlDataReader
                Dim sql As String
                sql = String.Format("select * from good_type where name = '{0}'", txtName.Text.Trim)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msgError.Text = "Тип товара с таким наименованием уже есть."
                    Exit Sub
                End If

                reader.Close()

                Dim cbxNadbavka As CheckBox = CType(e.Item.FindControl("cbxNadbavkaNew"), CheckBox)
                Dim lstGoodGroup As DropDownList = CType(e.Item.FindControl("lstGoodGroupNew"), DropDownList)
                Dim lstPriceTO As DropDownList = CType(e.Item.FindControl("lstPriceTO"), DropDownList)

                Dim Nadbavka
                If cbxNadbavka.Checked = True Then
                    Nadbavka = 1
                Else
                    Nadbavka = 0
                End If

                Dim cbxAllowCTO As CheckBox = CType(e.Item.FindControl("cbxAllowCTONew"), CheckBox)
                Dim cbxIsCashregister As CheckBox = CType(e.Item.FindControl("cbxIsCashregisterNew"), CheckBox)
                Dim tbxDescription As TextBox = CType(e.Item.FindControl("tbxDescriptionNew"), TextBox)
                Dim tbxCountry As TextBox = CType(e.Item.FindControl("tbxCountryNew"), TextBox)
                Dim tbxGarantia As TextBox = CType(e.Item.FindControl("tbxGarantiaNew"), TextBox)
                Dim tbxPrice As TextBox = CType(e.Item.FindControl("tbxPriceNew"), TextBox)
                Dim tbxArtikul As TextBox = CType(e.Item.FindControl("tbxArtikul"), TextBox)
                Dim tbxidpNew As TextBox = CType(e.Item.FindControl("tbxidpNew"), TextBox)

                Try
                    If (lstGoodGroup.SelectedIndex > 0) Then
                        sql = String.Format("INSERT INTO good_type (name, is_cashregister, param_str_description, good_group_sys_id, allowCTO, garantia, country, price_to, nadbavka, artikul, idp) VALUES('{0}', {1}, '{2}', {3}, {4}, '{5}','{6}', '{7}', '{8}', '{9}', '{10}')", txtName.Text.Trim, CInt(cbxIsCashregister.Checked), tbxDescription.Text, lstGoodGroup.SelectedValue, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text.Trim(), lstPriceTO.SelectedValue, Nadbavka, tbxArtikul.Text, tbxidpNew.Text)
                    Else
                        sql = String.Format("INSERT INTO good_type (name, is_cashregister, param_str_description, good_group_sys_id, allowCTO, garantia, country, price_to, nadbavka, artikul, idp) VALUES('{0}', {1}, '{2}', null, {3}, '{4}','{5}','{6}', '{7}', '{8}', '{9}')", txtName.Text.Trim, CInt(cbxIsCashregister.Checked), tbxDescription.Text, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text.Trim(), lstPriceTO.SelectedValue, Nadbavka, tbxArtikul.Text, tbxidpNew.Text)
                    End If
                    dbSQL.Execute(sql)
                    Bind()
                Catch
                    msgError.Text = "Ошибка вставки нового типа товара !<br>" & Err.Description
                    Exit Sub
                End Try

            End If
        End Sub

        Private Sub grdGoodTypes_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodTypes.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from good_type where good_type_sys_id = {0}", grdGoodTypes.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
                Bind()
            Catch
                If Err.Number = 1 Then
                    msgError.Text = "Выбранную запись нельзя удалить!"
                Else
                    msgError.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
        End Sub

        Private Sub grdGoodTypes_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodTypes.CancelCommand
            grdGoodTypes.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdGoodTypes_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodTypes.UpdateCommand
            Dim sql$

            Dim tbxName As TextBox = CType(e.Item.FindControl("tbxNameEdit"), TextBox)
            Dim cbxNadbavka As CheckBox = CType(e.Item.FindControl("cbxNadbavkaEdit"), CheckBox)
            Dim lstGoodGroup As DropDownList = CType(e.Item.FindControl("lstGoodGroupEdit"), DropDownList)

            Dim cbxIsCashregister As CheckBox = CType(e.Item.FindControl("cbxIsCashregisterEdit"), CheckBox)
            Dim cbxAllowCTO As CheckBox = CType(e.Item.FindControl("cbxAllowCTOEdit"), CheckBox)
            Dim tbxDescription As TextBox = CType(e.Item.FindControl("tbxDescriptionEdit"), TextBox)
            Dim tbxCountry As TextBox = CType(e.Item.FindControl("tbxCountryEdit"), TextBox)
            Dim tbxGarantia As TextBox = CType(e.Item.FindControl("tbxGarantiaEdit"), TextBox)
            Dim tbxPrice As TextBox = CType(e.Item.FindControl("tbxPriceEdit"), TextBox)
            Dim tbxArtikul As TextBox = CType(e.Item.FindControl("tbxArtikulEdit"), TextBox)
            Dim tbxidpEdit As TextBox = CType(e.Item.FindControl("tbxidpEdit"), TextBox)

            Dim Nadbavka
            If cbxNadbavka.Checked = True Then
                Nadbavka = 1
            Else
                Nadbavka = 0
            End If

            If (lstGoodGroup.SelectedIndex > 0) Then
                sql = String.Format("update good_type set name='{0}', is_cashregister={1}, param_str_description='{2}', good_group_sys_id={3}, allowCTO={4}, garantia='{5}', country='{6}', price_to='{7}', nadbavka='{9}', artikul='{10}', idp='{11}' where good_type_sys_id = {8}", tbxName.Text, CInt(cbxIsCashregister.Checked), tbxDescription.Text, lstGoodGroup.SelectedValue, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text, CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList).Text, grdGoodTypes.DataKeys(e.Item.ItemIndex), Nadbavka, tbxArtikul.Text, tbxidpEdit.Text)
            Else
                sql = String.Format("update good_type set name='{0}', is_cashregister={1}, param_str_description='{2}', good_group_sys_id=null, allowCTO={3}, garantia='{4}', country='{5}', price_to='{6}', nadbavka='{8}', artikul='{9}', idp='{10}' where good_type_sys_id = {7}", tbxName.Text, CInt(cbxIsCashregister.Checked), tbxDescription.Text, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text, CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList).Text, grdGoodTypes.DataKeys(e.Item.ItemIndex), Nadbavka, tbxArtikul.Text, tbxidpEdit.Text)
            End If

            Try
                dbSQL.Execute(sql)
            Catch
                msgError.Text = "Ошибка обновления записи!<br>" & Err.Description
                msgError.Text = "Ошибка обновления записи!<br>"
            End Try
            grdGoodTypes.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdGoodTypes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdGoodTypes.PageIndexChanged
            grdGoodTypes.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdGoodTypes_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodTypes.EditCommand
            grdGoodTypes.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

        Sub BindCountryList()

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            'список стран производителей
            Try
                adapt = dbSQL.GetDataAdapter("select distinct country from good_type where country is not null and ltrim(country)<>'' order by country")
                ds = New DataSet
                adapt.Fill(ds)
                lstCountryEdit.DataSource = ds.Tables(0).DefaultView
                lstCountryEdit.DataTextField = "country"
                lstCountryEdit.DataValueField = "country"
                lstCountryEdit.DataBind()
                lstCountryNew.DataSource = ds.Tables(0).DefaultView
                lstCountryNew.DataTextField = "country"
                lstCountryNew.DataValueField = "country"
                lstCountryNew.DataBind()
            Catch
                msgError.Text = "Ошибка формирования списка стран производителя!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub BindGoodGroupList(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select good_group_sys_id,name group_name from good_group order by group_name")
                ds = New DataSet
                adapt.Fill(ds)
                lst.DataSource = ds.Tables(0).DefaultView
                lst.DataTextField = "group_name"
                lst.DataValueField = "good_group_sys_id"
                lst.DataBind()
                lst.Items.Insert(0, New ListItem(ClearString, ClearString))
                Try
                    lst.Items.FindByText(s).Selected = True
                Catch
                End Try
            Catch
                msgError.Text = "Ошибка формирования списка групп товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

    End Class
End Namespace
