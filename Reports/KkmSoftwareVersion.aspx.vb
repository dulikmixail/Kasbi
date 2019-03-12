
Imports Newtonsoft.Json.Linq
Imports Service

Namespace Kasbi.Reports
    Partial Class KkmSoftwareVersion
        Inherits PageBase
        Const ClearString = "-------"
        Const TextError = " Ошибка загрузки"
        Protected NumRow As Integer = 0
        Dim ReadOnly _serviceRepair As ServiceRepair = New ServiceRepair()
        Dim ReadOnly _serviceGood As ServiceGood = New ServiceGood()

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ClearError()
            If Not IsPostBack Then
                LoadData()
            End If
        End Sub

        Private Sub LoadData()
            LoadSoftwareVersions()
            LoadCashRegister()
            InitComonents()
        End Sub

        Private Sub InitComonents()
            lstSoftwareVersion.Items.Insert(0, New ListItem(ClearString, ClearString))
            lstCashRegister.Items.Insert(0, New ListItem(ClearString, ClearString))
        End Sub

        Private Sub Bind()
            Dim softwareVersionList As List(Of String) = GetSelectedValueList(lstSoftwareVersion, True)
            Dim cashRegisterList As List(Of String) = GetSelectedValueList(lstCashRegister)
            Dim softwareVersion As String = String.Join(",", softwareVersionList)
            Dim cashRegister As String = String.Join(",", cashRegisterList)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet = New DataSet

            Try
                cmd = New SqlClient.SqlCommand("prc_rpt_kkm_software_version")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_software_versions", softwareVersion)
                cmd.Parameters.AddWithValue("@pi_good_type_ids", cashRegister)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                If ViewState("reportsort") = "" Then
                    ds.Tables(0).DefaultView.Sort = " good_name"
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("reportsort")
                End If
                grdSoftwareVersion.DataSource = ds.Tables(0).DefaultView
                grdSoftwareVersion.DataKeyField = "good_name"
                grdSoftwareVersion.DataBind()
            Catch
                lblError.Text = "Ошибка при формировании отчета!<br>" & Err.Description
            End Try
        End Sub

        Private Sub BuildTotalTable()
            Dim softwareVersionList As List(Of String) = GetSelectedValueList(lstSoftwareVersion, True)
            Dim cashRegisterList As List(Of String) = GetSelectedValueList(lstCashRegister)
            Dim softwareVersion As String = String.Join(",", softwareVersionList)
            Dim cashRegister As String = String.Join(",", cashRegisterList)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet = New DataSet
            Dim goodName As String = String.Empty
            plhTotalItog.Controls.Clear()
            Dim table As Table
            Dim tr As TableRow
            Dim tc As TableCell
            Dim itogCouter As Integer = 0

            Try
                cmd = New SqlClient.SqlCommand("prc_rpt_kkm_software_version_itog")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_software_versions", softwareVersion)
                cmd.Parameters.AddWithValue("@pi_good_type_ids", cashRegister)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)

                If ds.Tables.Count > 0
                    If ds.Tables(0).Rows.Count > 0
                                            goodName = ds.Tables(0).Rows(0).Item("name").ToString()
                    table = New Table()
                    table.Attributes.Add("class", "itog-table")
                    tr = New TableRow()
                    tc = New TableCell()
                    tc.Text = "<strong>" & goodName & "</strong>"
                    tc.ColumnSpan = 2
                    tr.Cells.Add(tc)
                    table.Rows.Add(tr)
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If dr("name").ToString() <> goodName
                            tr = New TableRow()
                            tc = New TableCell()
                            tc.Text = "&nbsp;&nbsp;&nbsp;<strong><em>Всего: </em></strong>"
                            tr.Cells.Add(tc)
                            tc = New TableCell()
                            tc.Text = itogCouter.ToString()
                            itogCouter = 0
                            tr.Cells.Add(tc)
                            table.Rows.Add(tr)

                            goodName = dr("name").ToString()
                            tr = New TableRow()
                            tc = New TableCell()
                            tc.Text = "<strong>" & goodName & "</strong>"
                            tc.ColumnSpan = 2
                            tr.Cells.Add(tc)
                            table.Rows.Add(tr)
                        End If
                        tr = New TableRow()
                        tc = New TableCell()
                        tc.Text = "&nbsp;&nbsp;&nbsp;" & dr("software_version").ToString()
                        tr.Cells.Add(tc)
                        tc = New TableCell()
                        tc.Text = dr("count").ToString()
                        itogCouter += Convert.ToInt32(dr("count").ToString())
                        tr.Cells.Add(tc)
                        table.Rows.Add(tr)
                    Next
                    tr = New TableRow()
                    tc = New TableCell()
                    tc.Text = "&nbsp;&nbsp;&nbsp;<strong><em>Всего: </em></strong>"
                    tr.Cells.Add(tc)
                    tc = New TableCell()
                    tc.Text = itogCouter.ToString()
                    tr.Cells.Add(tc)
                    table.Rows.Add(tr)
                    plhTotalItog.Controls.Add(table)
                    End If
                End If
            Catch
                lblError.Text = "Ошибка при формировании итоговой таблицы!<br>" & Err.Description
            End Try
        End Sub


        Protected Sub grdSoftwareVersion_ItemDataBound(ByVal sender As Object,
                                                       ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdSoftwareVersion.ItemDataBound
            NumRow = NumRow + 1
        End Sub

        Private Sub grdSoftwareVersion_SortCommand(ByVal source As System.Object,
                                                   ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
            Handles grdSoftwareVersion.SortCommand
            If ViewState("reportsort") = e.SortExpression Then
                ViewState("reportsort") = e.SortExpression & " DESC"
            Else
                ViewState("reportsort") = e.SortExpression
            End If
            Bind()
            BuildTotalTable()
        End Sub

        Private Sub LoadSoftwareVersions()
            Dim ds As DataSet = _serviceRepair.GetDetailListWithSoftwareVersion()
            Try
                lstSoftwareVersion.DataSource = ds.Tables(0).DefaultView
                lstSoftwareVersion.DataTextField = "detail_name"
                lstSoftwareVersion.DataValueField = "detail_name"
                lstSoftwareVersion.DataBind()

            Catch
                lblError.Text &= lblSoftwareVersion.Text & TextError
            End Try
        End Sub

        Private Sub LoadCashRegister()
            Dim ds As DataSet = _serviceGood.GetGoodsByType(1)
            Try
                lstCashRegister.DataSource = ds.Tables(0).DefaultView
                lstCashRegister.DataTextField = "name"
                lstCashRegister.DataValueField = "good_type_sys_id"
                lstCashRegister.DataBind()
            Catch
                lblError.Text &= lblCashRegister.Text & TextError
            End Try
        End Sub

        Private Function GetSelectedValueList(listBox As ListBox, Optional isString As Boolean = False) _
            As List(Of String)
            Dim selectedListItems As List(Of String) = New List(Of String)()

            If listBox.SelectedIndex <> - 1 And listBox.SelectedIndex <> 0
                For Each item As ListItem In listBox.Items
                    If item.Value <> ClearString Then
                        If item.Selected Then _
                            selectedListItems.Add(IIf(isString, "'" & item.Value & "'", item.Value).ToString())
                    End If
                Next
            End If
            Return selectedListItems
        End Function


        Private Sub ClearError()
            lblError.Text = "&nbsp;"
        End Sub

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnView.Click
            Bind()
            BuildTotalTable()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub
    End Class
End Namespace