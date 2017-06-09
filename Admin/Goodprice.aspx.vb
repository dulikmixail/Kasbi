Imports Excel = Microsoft.Office.Interop.Excel

Namespace Kasbi.Admin

    Partial Class Goodprice
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
                grdGoodTypes.Columns(3).Visible = True
            Else
                grdGoodTypes.Columns(3).Visible = False
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
            'составляем список групп цен на ТО
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand()
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter("prc_get_pricegood", True)
                ds = New DataSet
                adapt.Fill(ds)

                If ViewState("goodsort") = "" Then
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("goodsort")
                End If

                grdGoodTypes.DataSource = ds.Tables(0).DefaultView
                grdGoodTypes.DataKeyField = "id"
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

                Try
                    If (lstGoodGroup.SelectedIndex > 0) Then
                        sql = String.Format("INSERT INTO good_type (name, is_cashregister, param_str_description, good_group_sys_id, allowCTO, garantia, country, price_to, nadbavka, artikul) VALUES('{0}', {1}, '{2}', {3}, {4}, '{5}','{6}', '{7}', '{8}', '{9}')", txtName.Text.Trim, CInt(cbxIsCashregister.Checked), tbxDescription.Text, lstGoodGroup.SelectedValue, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text.Trim(), lstPriceTO.SelectedValue, Nadbavka, tbxArtikul.Text)
                    Else
                        sql = String.Format("INSERT INTO good_type (name, is_cashregister, param_str_description, good_group_sys_id, allowCTO, garantia, country, price_to, nadbavka, artikul) VALUES('{0}', {1}, '{2}', null, {3}, '{4}','{5}','{6}', '{7}', '{8}')", txtName.Text.Trim, CInt(cbxIsCashregister.Checked), tbxDescription.Text, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text.Trim(), lstPriceTO.SelectedValue, Nadbavka, tbxArtikul.Text)
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
                Dim sql$ = String.Format("delete from pricelist_good where id = {0}", grdGoodTypes.DataKeys(e.Item.ItemIndex))
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

            Dim Nadbavka
            If cbxNadbavka.Checked = True Then
                Nadbavka = 1
            Else
                Nadbavka = 0
            End If

            If (lstGoodGroup.SelectedIndex > 0) Then
                sql = String.Format("update good_type set name='{0}', is_cashregister={1}, param_str_description='{2}', good_group_sys_id={3}, allowCTO={4}, garantia='{5}', country='{6}', price_to='{7}', nadbavka='{9}', artikul='{10}' where id = {8}", tbxName.Text, CInt(cbxIsCashregister.Checked), tbxDescription.Text, lstGoodGroup.SelectedValue, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text, CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList).Text, grdGoodTypes.DataKeys(e.Item.ItemIndex), Nadbavka, tbxArtikul.Text)
            Else
                sql = String.Format("update good_type set name='{0}', is_cashregister={1}, param_str_description='{2}', good_group_sys_id=null, allowCTO={3}, garantia='{4}', country='{5}', price_to='{6}', nadbavka='{8}', artikul='{9}' where id = {7}", tbxName.Text, CInt(cbxIsCashregister.Checked), tbxDescription.Text, CInt(cbxAllowCTO.Checked), tbxGarantia.Text, tbxCountry.Text, CType(e.Item.FindControl("lstPriceTOEdit"), DropDownList).Text, grdGoodTypes.DataKeys(e.Item.ItemIndex), Nadbavka, tbxArtikul.Text)
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

        End Sub

        Sub BindGoodGroupList(ByRef lst As DropDownList, ByVal s As String)

        End Sub

        Protected Sub btnLoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadData.Click
            'Импортируем прайс
            'Try
            Dim Excel = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
            Dim wb = Excel.Workbooks.Open(Server.MapPath("../XML/PriceRamok.xls"))
            Excel.Visible = True

            wb.Activate()

            Dim ws = wb.Sheets(1)
            Dim wr = ws.Range("A1", "AB25")
            Dim id_price
            Dim artikul
            Dim name
            Dim description
            Dim id_group
            Dim maker
            Dim price
            Dim available


            Dim i As Integer
            i = 2
            'Try
            While i < 10
                id_price = wr.Cells.FormulaR1C1(0 + i, 1)
                If id_price <> "" Then
                    artikul = wr.Cells.FormulaR1C1(0 + i, 2)
                    name = wr.Cells.FormulaR1C1(0 + i, 3)
                    description = wr.Cells.FormulaR1C1(0 + i, 4)
                    id_group = wr.Cells.FormulaR1C1(0 + i, 5)
                    price = wr.Cells.FormulaR1C1(0 + i, 6)
                    maker = wr.Cells.FormulaR1C1(0 + i, 7)
                    available = wr.Cells.FormulaR1C1(0 + i, 8)

                    'Проверяем, есть ли уже запись
                    Dim countlog As String = dbSQL.ExecuteScalar("SELECT id FROM pricelist_good WHERE id_price='" & id_price & "'")
                    If countlog <> "" Then
                        'Редактируем
                        dbSQL.ExecuteScalar("UPDATE pricelist_good SET artikul='" & artikul & "', name='" & name & "', description='" & description & "', id_group='" & id_group & "', maker='" & maker & "', price='" & price & "', available='" & available & "' WHERE id_price='" & id_price & "'")
                    Else
                        'Вставляем
                        dbSQL.ExecuteScalar("INSERT INTO pricelist_good (id_price,artikul,name,description,id_group,price,maker,available) " & _
                        "VALUES ('" & id_price & "', '" & artikul & "','" & name & "','" & description & "','" & id_group & "','" & price & "','" & maker & "','" & available & "')")
                    End If

                End If
                i = i + 1
            End While
            'Catch ex As Exception
            'End Try


            'Catch ex As Exception
            'MsgBox("Error: " + ex.ToString())
            'End Try

            Bind()
        End Sub

        Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
            'Очищаем прайс
            dbSQL.ExecuteScalar("TRUNCATE TABLE pricelist_good")
            Bind()
        End Sub


    End Class
End Namespace
