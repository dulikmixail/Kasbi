Imports System
Imports System.IO
Imports System.Xml

Namespace Kasbi.Admin

    Partial Class ImportDelivery
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents scrollPos As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents CurrentPage As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents Parameters As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents FindHidden As System.Web.UI.HtmlControls.HtmlInputHidden


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim table As DataTable
        Dim valid_count
        Dim query

        Function makedate(ByVal txtdate)
            Dim arr_date = Split(txtdate, "/")
            txtdate = arr_date(1) & "/" & arr_date(0) & "/" & arr_date(2)
            Return txtdate
        End Function

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            msgImport.Text = ""

            '������� ���-�� ����� � ����
            Dim countlog As String = dbSQL.ExecuteScalar("SELECT COUNT(*) FROM import_log")
            If countlog <> "0" Then
                lbl_countlog.Text = "� ���� ������� ������� <b>" & countlog & "</b> �������."
            Else
                btn_clearlog.Visible = False
                btn_delimport.Visible = False
            End If

        End Sub

        Private Sub btnLoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadData.Click
            Dim i% = 0
            Dim doc As XmlDocument
            Dim nodeList As XmlNode
            Dim node1 As XmlNode
            Dim node2 As XmlNode
            Dim node3 As XmlNode
            Dim root As XmlElement

            '���������� ��������
            Dim delivery_1cnum
            Dim delivery_date
            Dim supplier_unn
            Dim supplier_name
            Dim supplier_adress
            Dim supplier_okulp
            Dim supplier_phone
            '���������� ������
            Dim good_1ccode
            Dim good_name
            Dim good_artikul
            Dim good_amount
            Dim good_parentname
            Dim good_preiskurant
            Dim good_cost_uchet
            Dim good_cost_prod
            Dim buh = ""
            Dim buh_id As String
            Dim date_now

            date_now = makedate("" & Now.Day & "/" & Now.Month & "/" & Now.Year & "")


            lblconent.Text = ""
            Dim text_error As String = ""
            Dim node_error = 0 '���������� ���������� ������ �������
            Dim id_pricelist As String

            Try

                doc = New XmlDocument
                doc.Load(Server.MapPath("../XML/import/delivery.xml"))
                root = doc.DocumentElement
                nodeList = root.SelectSingleNode("/Dat�")

                '��� �������� �������

                Dim sale_1cnum '����� ��������� �������
                Dim sale_date '���� �������
                Dim client_unn '��� ����������
                Dim manager '��������

                Dim sale_deliverycode '��� ��������
                Dim sale_goodname '�������� ������
                Dim sale_artikul '�������
                Dim sale_amount '����������
                Dim sale_cost '���� �������

                Dim customer_sys_id As String
                Dim manager_id As String
                Dim id_delivery As String
                Dim id_good As String
                Dim trace As String

                '������ ������� ���� �������
                'query = dbSQL.ExecuteScalar("DELETE FROM import_log")


                For Each node1 In nodeList.ChildNodes
                    If node1.Name = "Document" Then

                        '������ ���� Document

                        node_error = 0 '�������� ������ ������ �����

                        For Each node2 In node1.ChildNodes

                            If node2.Name = "Cap" Then
                                '��� ���������� � ����������
                                For Each node3 In node2.ChildNodes
                                    If node3.Name = "Number" Then
                                        delivery_1cnum = node3.InnerText.ToString.Trim
                                    ElseIf node3.Name = "Date" Then
                                        delivery_date = makedate(node3.InnerText.ToString.Trim)
                                    ElseIf node3.Name = "Counterpart" Then
                                        supplier_unn = node3.Attributes.ItemOf("UNN").Value().ToString.Trim
                                        supplier_name = node3.Attributes.ItemOf("Name").Value().ToString.Trim
                                        supplier_adress = node3.Attributes.ItemOf("Adres").Value().ToString.Trim
                                        supplier_okulp = node3.Attributes.ItemOf("OKULP").Value().ToString.Trim
                                        supplier_phone = node3.Attributes.ItemOf("Tel").Value().ToString.Trim
                                    End If
                                Next
                                '������� ��� �� ����
                                '��������� ���
                                If supplier_name = "" Then
                                    node_error = 1
                                    text_error &= "�������� id " & delivery_1cnum & ": ��� �������� � ����������<br>"
                                End If
                                '��������� ��� ��������
                                Dim delivery_1cnum_test As String = dbSQL.ExecuteScalar("SELECT id1c FROM delivery WHERE id1c='" & delivery_1cnum & "'")
                                If delivery_1cnum_test <> "" Then
                                    node_error = 1
                                    text_error &= "�������� id " & delivery_1cnum & ": ��� ���� ������������� ������<br>"
                                End If
                            ElseIf node2.Name = "Table" Then
                                '��� ���������� � �������
                                If node_error <> 1 Then '���� �� ���������� ��� ������
                                    For Each node3 In node2.ChildNodes
                                        good_1ccode = node3.Attributes.ItemOf("Code").Value().ToString.Trim
                                        good_name = node3.Attributes.ItemOf("Goods").Value().ToString.Trim
                                        good_artikul = node3.Attributes.ItemOf("Artikul").Value().ToString.Trim
                                        'good_artikul = good_1ccode
                                        good_amount = node3.Attributes.ItemOf("Amount").Value().ToString.Trim
                                        good_cost_uchet = node3.Attributes.ItemOf("CostU").Value().ToString.Trim
                                        Try
                                            good_cost_prod = node3.Attributes.ItemOf("CostP").Value().ToString.Trim
                                        Catch ex As Exception
                                            good_cost_prod = ""
                                        End Try
                                        good_preiskurant = node3.Attributes.ItemOf("Preysk").Value().ToString.Trim

                                        '������� ��� �� ����
                                        If good_artikul = "" Then
                                            node_error = 1
                                            text_error &= "�������� id " & delivery_1cnum & ": � ������ '" & good_name & "' ��� ��������<br>"
                                        End If
                                        If good_cost_prod = "" Then
                                            node_error = 1
                                            text_error &= "�������� id " & delivery_1cnum & ": � ������ '" & good_name & "' ��� �������������� ����<br>"
                                        End If
                                        If good_preiskurant = "" Then
                                            node_error = 1
                                            text_error &= "�������� id " & delivery_1cnum & ": � ������ '" & good_name & "' ��� ������������<br>"
                                        End If
                                    Next

                                    If node_error <> 1 Then '���� ������ ��� ������ � ���� ����
                                        '
                                        '��������� ������
                                        '
                                        '������� id ����������, ���� ��� ������ - �������
                                        Dim id_supplier As String = dbSQL.ExecuteScalar("SELECT sys_id FROM supplier WHERE supplier_name ='" & supplier_name & "'")
                                        If id_supplier = "" Then
                                            '���� ��� ���� ������ ���������� - ������� ������
                                            id_supplier = dbSQL.ExecuteScalar("SELECT MAX(sys_id) FROM supplier")
                                            id_supplier += 1

                                            query = dbSQL.ExecuteScalar("INSERT INTO supplier (supplier_name,unn) " & _
                                            " VALUES ('" & supplier_name & "','" & supplier_unn & "')")

                                            id_supplier = dbSQL.ExecuteScalar("SELECT MAX(sys_id) FROM supplier")

                                            '��������� � ��� ������� ������
                                            query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                            "VALUES ('supplier','sys_id','" & id_supplier & "')")
                                        End If
                                        '��� ������� ��������
                                        id_delivery = dbSQL.ExecuteScalar("SELECT MAX(delivery_sys_id) FROM delivery")
                                        id_delivery += 1
                                        query = dbSQL.ExecuteScalar("INSERT INTO delivery (delivery_sys_id,delivery_date,info,id1c) VALUES ('" & id_delivery & "','" & delivery_date & "','" & supplier_name & "','" & delivery_1cnum & "')")

                                        id_delivery = dbSQL.ExecuteScalar("SELECT MAX(delivery_sys_id) FROM delivery")


                                        '��������� � ��� ������� ������
                                        query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                        "VALUES ('delivery','delivery_sys_id','" & id_delivery & "')")

                                        For Each node3 In node2.ChildNodes
                                            good_1ccode = node3.Attributes.ItemOf("Code").Value()
                                            good_name = node3.Attributes.ItemOf("Goods").Value()
                                            good_artikul = node3.Attributes.ItemOf("Artikul").Value().ToString.Trim
                                            'good_artikul = good_1ccode
                                            good_amount = node3.Attributes.ItemOf("Amount").Value()
                                            good_parentname = node3.Attributes.ItemOf("Parents").Value()
                                            good_cost_uchet = node3.Attributes.ItemOf("CostU").Value()

                                            If Not good_name.ToString.Contains("�����") Then

                                                Try
                                                    good_cost_prod = node3.Attributes.ItemOf("CostP").Value()
                                                Catch ex As Exception
                                                    good_cost_prod = ""
                                                End Try
                                                good_preiskurant = node3.Attributes.ItemOf("Preysk").Value().ToString.Trim

                                                '��������� �� ��������, ���������� �� ����� ��� ������, ���� ��� - �������
                                                id_good = ""
                                                id_good = dbSQL.ExecuteScalar("SELECT good_type_sys_id FROM good_type WHERE artikul='" & good_artikul & "'")

                                                If id_good = "" Then
                                                    '���������� ������ ������ �� ��������
                                                    Dim id_group = dbSQL.ExecuteScalar("SELECT good_group_sys_id FROM good_group WHERE name='" & good_parentname & "'")
                                                    '��������� ����� �����
                                                    id_good = dbSQL.ExecuteScalar("SELECT MAX(good_type_sys_id) FROM good_type")
                                                    id_good += 1

                                                    query = dbSQL.ExecuteScalar("INSERT INTO good_type (name,artikul,allowCTO,is_cashregister,nadbavka,garantia,good_group_sys_id) " & _
                                                    " VALUES ('" & good_name & "','" & good_artikul & "','0','0','0','0','" & id_group & "')")

                                                    id_good = dbSQL.ExecuteScalar("SELECT MAX(good_type_sys_id) FROM good_type")

                                                    '��������� � ��� ������� ������
                                                    query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                    "VALUES ('good_type','good_type_sys_id','" & id_good & "')")
                                                End If

                                                '������ ��������� ����� � ��������
                                                '������� id ������ ��������
                                                Dim id_delivery_detail As String = dbSQL.ExecuteScalar("SELECT MAX(delivery_detail_sys_id) FROM delivery_detail")
                                                id_delivery_detail += 1

                                                query = dbSQL.ExecuteScalar("INSERT INTO delivery_detail (delivery_sys_id,supplier_id,good_type_sys_id,price,quantity,unit_id,rur,artikul) " & _
                                                "VALUES ('" & id_delivery & "','" & id_supplier & "','" & id_good & "','" & good_cost_uchet & "','" & good_amount & "','1','0','" & good_1ccode & "')")

                                                id_delivery_detail = dbSQL.ExecuteScalar("SELECT MAX(delivery_detail_sys_id) FROM delivery_detail")

                                                '��������� � ��� ������� ������
                                                query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                "VALUES ('delivery_detail','delivery_detail_sys_id','" & id_delivery_detail & "')")

                                                '������ ��������� ����� � ���������
                                                id_pricelist = dbSQL.ExecuteScalar("SELECT TOP 1 pricelist_sys_id FROM pricelist WHERE pricelist_name='����������� " & good_preiskurant & "'")

                                                If id_pricelist = "" Then
                                                    '���������� ����� id ����������
                                                    id_pricelist = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
                                                Else
                                                    Dim id_pricelist2 = dbSQL.ExecuteScalar("SELECT TOP 1 pricelist_sys_id FROM pricelist WHERE pricelist_name='����������� " & good_preiskurant & "' AND good_type_sys_id='" & id_good & "'")
                                                    Try
                                                        If id_pricelist2 <> "" Then
                                                            id_pricelist = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
                                                        End If
                                                    Catch ex As Exception
                                                        id_pricelist = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
                                                    End Try
                                                End If
                                                good_cost_uchet = CInt(good_cost_uchet.ToString.Replace(".", ","))
                                                '��������� ������ ����������

                                                'trace = trace & vbCrLf & id_delivery & "-" & id_pricelist & "-" & good_artikul & "-" & id_good
                                                'MsgBox(trace)
                                                query = dbSQL.ExecuteScalar("INSERT INTO pricelist (pricelist_sys_id,good_type_sys_id,price,pricelist_name,pricelist_date,seq) " & _
                                                "VALUES ('" & id_pricelist & "','" & id_good & "','" & good_cost_prod & "','����������� " & good_preiskurant & "','01/01/2011',1)")

                                                '��������� � ��� ������� ������
                                                query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                "VALUES ('pricelist','pricelist_sys_id','" & id_pricelist & "')")

                                                '������ ��������� ������ � ������
                                                Dim id_goodsys As String = dbSQL.ExecuteScalar("SELECT MAX(good_sys_id) FROM good")
                                                id_goodsys += 1
                                                Dim good_key As String = dbSQL.ExecuteScalar("SELECT MAX(good_key) FROM good")
                                                good_key += 1
                                                query = dbSQL.ExecuteScalar("INSERT INTO good (good_sys_id, price,param_num,state,delivery_sys_id,good_type_sys_id) " & _
                                                "VALUES ('" & id_goodsys & "','" & good_cost_prod & "','" & good_amount & "','1','" & id_delivery & "','" & id_good & "')")

                                                id_goodsys = dbSQL.ExecuteScalar("SELECT MAX(good_sys_id) FROM good")

                                                '��������� ���
                                                query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                "VALUES ('good','good_sys_id','" & id_goodsys & "')")

                                            End If

                                        Next
                                    End If


                                End If

                            End If

                        Next

                        '//������ ���� Document

                    End If
                    'lblconent.Text = lblconent.Text + Document.Attributes.ItemOf("Number").Value()
                Next



                '
                '//
                '////��� ����� ��������� �������
                '//
                '

                'Try


                doc = New XmlDocument
                doc.Load(Server.MapPath("../XML/import/sales.xml"))
                root = doc.DocumentElement
                nodeList = root.SelectSingleNode("/Dat�")

                For Each node1 In nodeList.ChildNodes

                    If node1.Name = "Document" Then

                        '������ ���� Document

                        node_error = 0 '�������� ������ ������ �����

                        For Each node2 In node1.ChildNodes

                            If node2.Name = "Cap" Then
                                '��� ���������� � ����������
                                For Each node3 In node2.ChildNodes
                                    If node3.Name = "Number" Then
                                        sale_1cnum = node3.InnerText.ToString.Trim
                                    ElseIf node3.Name = "Date" Then
                                        sale_date = makedate(node3.InnerText.ToString.Trim)
                                    ElseIf node3.Name = "Counterpart" Then
                                        client_unn = node3.Attributes.ItemOf("UNN").Value().ToString.Trim
                                        manager = node3.Attributes.ItemOf("Manager").Value().ToString.Trim
                                        buh = ""
                                        Try
                                            buh = node3.Attributes.ItemOf("Buh").Value().ToString.Trim
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Next
                                '������� ��� �� ����
                                '��������� ���
                                If client_unn = "" Then
                                    node_error = 1
                                    text_error &= "������� id " & sale_1cnum & ": ��� ��� � �������<br>"
                                End If
                                '��������� ������� ��� � �����
                                customer_sys_id = dbSQL.ExecuteScalar("SELECT customer_sys_id FROM customer WHERE unn ='" & client_unn & "'")
                                If customer_sys_id = "" Then
                                    node_error = 1
                                    text_error &= "������� id " & sale_1cnum & ": �� ������ ������ � ��� '" & client_unn & "'<br>"
                                End If
                                '��������� ������� ���������
                                manager_id = dbSQL.ExecuteScalar("SELECT sys_id FROM employee WHERE Name ='" & manager & "'")
                                If manager_id = "" Then
                                    node_error = 1
                                    text_error &= "������� id " & sale_1cnum & ": �� ������ �������� '" & manager & "'<br>"
                                End If
                                '��������� ������� ����������
                                If buh <> "" Then
                                    buh_id = dbSQL.ExecuteScalar("SELECT sys_id FROM employee WHERE Name ='" & buh & "'")
                                    If buh_id = "" Then
                                        node_error = 1
                                        text_error &= "������� id " & sale_1cnum & ": �� ������ ��������� '" & buh & "'<br>"
                                    End If
                                End If
                                '��������� ������� ������� � ����� �������
                                Dim test_saleid As String = dbSQL.ExecuteScalar("SELECT id1c FROM sale WHERE id1c='" & sale_1cnum & "'")
                                If test_saleid <> "" Then
                                    node_error = 1
                                    text_error &= "������� id " & sale_1cnum & ": ��� ���� ��������� ������ '" & sale_1cnum & "'<br>"
                                End If
                            ElseIf node2.Name = "Table" Then
                                '��� ���������� � �������
                                If node_error <> 1 Then '���� �� ���������� ��� ������
                                    For Each node3 In node2.ChildNodes
                                        sale_deliverycode = node3.Attributes.ItemOf("Code").Value().ToString.Trim
                                        sale_goodname = node3.Attributes.ItemOf("Goods").Value().ToString.Trim
                                        sale_artikul = node3.Attributes.ItemOf("Code").Value().ToString.Trim
                                        sale_amount = node3.Attributes.ItemOf("Amount").Value().ToString.Trim

                                        If sale_goodname.ToString.Contains("���") And sale_goodname.ToString.Contains("�����") Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": ����� '" & sale_goodname & "' - ��� �����<br>"
                                        End If

                                        Try
                                            sale_cost = node3.Attributes.ItemOf("CostP").Value().ToString.Trim
                                        Catch ex As Exception
                                            sale_cost = ""
                                        End Try
                                        '������� ��� �� ����
                                        If sale_artikul = "" Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": � ������ '" & sale_goodname & "' ��� ��������<br>"
                                        End If
                                        If sale_cost = "" Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": � ������ '" & sale_goodname & "' ��� ����<br>"
                                        End If
                                        If sale_amount = "" Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": � ������ '" & sale_goodname & "' ��� ����������<br>"
                                        End If
                                        '��������� ��������
                                        id_delivery = dbSQL.ExecuteScalar("SELECT delivery_sys_id FROM delivery_detail WHERE artikul ='" & sale_deliverycode & "'")
                                        If sale_amount = "" Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": �� ������� �������� � ����� '" & sale_deliverycode & "'<br>"
                                        End If
                                        '��������� ���� �� ����� �������
                                        id_good = dbSQL.ExecuteScalar("SELECT good_type_sys_id FROM good_type WHERE artikul ='" & sale_artikul & "'")
                                        If id_good = "" Then
                                            node_error = 1
                                            text_error &= "������� id " & sale_1cnum & ": �� ������ ��� ������ � ��������� '" & sale_artikul & "'<br>"
                                        End If
                                        '��������� ������� ������
                                        If id_delivery And id_good Then
                                            Dim real_amount As Integer = dbSQL.ExecuteScalar("SELECT param_num FROM good WHERE delivery_sys_id ='" & id_delivery & "' AND good_type_sys_id ='" & id_good & "' AND (sale_sys_id is NULL OR sale_sys_id=0)")
                                            If real_amount < sale_amount Then
                                                node_error = 1
                                                text_error &= "������� id " & sale_1cnum & ": �� ������� ������ '" & sale_goodname & "'. � ����� � �������� " & sale_deliverycode & " �������� " & real_amount & ", � ���� " & sale_amount & "<br>"
                                            End If
                                        End If
                                    Next

                                    If node_error <> 1 Then '���� ������ ��� ������ � ���� ����
                                        '
                                        '��������� ������
                                        '
                                        '��������� �������
                                        '��������� ������
                                        Dim sale_sys_id As Integer = dbSQL.ExecuteScalar("SELECT MAX(sale_sys_id) FROM sale")
                                        sale_sys_id += 1

                                        'query = dbSQL.ExecuteScalar("INSERT INTO sale (customer_sys_id,sale_date,state,type,saler_sys_id,firm_sys_id,id1c,buh_sys_id) " & _
                                        '"VALUES ('" & customer_sys_id & "','" & sale_date & "','3','3','" & manager_id & "', '1','" & sale_1cnum & "','" & buh_id & "')")

                                        query = dbSQL.ExecuteScalar("INSERT INTO sale (customer_sys_id,sale_date,state,type,saler_sys_id,firm_sys_id,id1c,buh_sys_id) " & _
                                        "VALUES ('" & customer_sys_id & "','" & sale_date & "','3','3','" & manager_id & "', '1','" & sale_1cnum & "','" & buh_id & "')")
                                        '���������� id

                                        sale_sys_id = dbSQL.ExecuteScalar("SELECT MAX(sale_sys_id) FROM sale")

                                        '��������� ���
                                        query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                        "VALUES ('sale','sale_sys_id','" & sale_sys_id & "')")

                                        If node_error <> 1 Then '���� �� ���������� ��� ������
                                            For Each node3 In node2.ChildNodes
                                                sale_deliverycode = node3.Attributes.ItemOf("Code").Value().ToString.Trim
                                                sale_goodname = node3.Attributes.ItemOf("Goods").Value().ToString.Trim
                                                sale_artikul = node3.Attributes.ItemOf("Code").Value().ToString.Trim
                                                sale_amount = node3.Attributes.ItemOf("Amount").Value().ToString.Trim
                                                Try
                                                    sale_cost = node3.Attributes.ItemOf("CostP").Value().ToString.Trim
                                                Catch ex As Exception
                                                    sale_cost = ""
                                                End Try

                                                id_good = dbSQL.ExecuteScalar("SELECT good_type_sys_id FROM good_type WHERE artikul ='" & sale_artikul & "'")
                                                id_delivery = dbSQL.ExecuteScalar("SELECT delivery_sys_id FROM delivery_detail WHERE artikul ='" & sale_deliverycode & "'")

                                                '��������� �������
                                                '������� ������������ id �������
                                                Dim good_sys_id As String = dbSQL.ExecuteScalar("SELECT MAX(good_sys_id) FROM good")
                                                good_sys_id += 1
                                                Dim good_key As String = dbSQL.ExecuteScalar("SELECT MAX(good_key) FROM good")
                                                good_key += 1

                                                '��������� ������
                                                query = dbSQL.ExecuteScalar("INSERT INTO good (good_sys_id, good_type_sys_id, delivery_sys_id, sale_sys_id, price, param_num, state, worker) " & _
                                                "VALUES ('" & good_sys_id & "','" & id_good & "','" & id_delivery & "','" & sale_sys_id & "','" & sale_cost & "','" & sale_amount & "', '3', '" & manager_id & "')")

                                                good_sys_id = dbSQL.ExecuteScalar("SELECT MAX(good_sys_id) FROM good")

                                                '��������� ���
                                                query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                "VALUES ('good','good_sys_id','" & good_sys_id & "')")

                                                query = dbSQL.ExecuteScalar("UPDATE good SET param_num=param_num-" & sale_amount & " WHERE delivery_sys_id ='" & id_delivery & "' AND good_type_sys_id ='" & id_good & "' AND (sale_sys_id is NULL OR sale_sys_id=0)")

                                            Next
                                        End If

                                    End If

                                End If

                            End If

                        Next

                    End If

                Next

            Catch ex As Exception
            End Try


            '
            '//
            '////������ ���������
            '//
            '


            doc = New XmlDocument
            doc.Load(Server.MapPath("../XML/import/price.xml"))
            root = doc.DocumentElement
            nodeList = root.SelectSingleNode("/Dat�")


            id_pricelist = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
            Dim preisk As String


            For Each node1 In nodeList.ChildNodes
                If node1.Name = "Document" Then
                    For Each node2 In node1.ChildNodes

                        If node2.Name = "Table" Then

                            For Each node3 In node2.ChildNodes
                                Dim code1c = node3.Attributes.ItemOf("Artikul").Value().ToString.Trim
                                Dim cost = node3.Attributes.ItemOf("CostP").Value().ToString.Trim
                                Dim pname = node3.Attributes.ItemOf("Goods").Value().ToString.Trim
                                Dim pcount = node3.Attributes.ItemOf("Count").Value().ToString

                                If preisk = "" Then
                                    preisk = node3.Attributes.ItemOf("Preysk").Value().ToString
                                    preisk = "����������� � 1 �� " & date_now
                                    id_pricelist = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
                                End If

                                If pcount <> "0" Then
                                    If code1c <> "" Then
                                        '��������� �� ��������, ���������� �� ����� ��� ������
                                        Dim id_good = ""
                                        id_good = dbSQL.ExecuteScalar("SELECT good_type_sys_id FROM good_type WHERE artikul='" & code1c & "'")

                                        If id_good Then
                                            Try
                                                cost = CInt(cost.ToString.Replace(".", ","))
                                                query = dbSQL.ExecuteScalar("INSERT INTO pricelist (pricelist_sys_id,good_type_sys_id,price,pricelist_name,pricelist_date,seq) " & _
                                                "VALUES ('" & id_pricelist & "','" & id_good & "','" & cost & "','" & preisk & "','" & date_now & "',1)")
                                                '��������� � ��� ������� ������
                                                query = dbSQL.ExecuteScalar("INSERT INTO import_log (table_name,field_name,field_value) " & _
                                                "VALUES ('pricelist','pricelist_sys_id','" & id_pricelist & "')")
                                            Catch ex As Exception
                                            End Try
                                        Else
                                            text_error &= "���������: �� ������ ��� ������ � ��������� '" & code1c & "'<br>"
                                        End If
                                    Else
                                        text_error &= "���������: �� ������ ������� ��� ������ '" & pname & "'<br>"
                                    End If
                                End If


                            Next
                        End If
                    Next
                End If
            Next




            '
            '//
            '////����
            '//
            '

            If text_error <> "" Then
                lblerror.Text = "<b>� ����� �������� ������� ������:</b> <br>" & text_error
            End If


            '������� ���-�� ����� � ����
            Dim countlog As String = dbSQL.ExecuteScalar("SELECT COUNT(*) FROM import_log")
            If countlog <> "0" Then
                lbl_countlog.Text = "� ���� ������� ������� <b>" & countlog & "</b> �������."
                btn_clearlog.Visible = True
                btn_delimport.Visible = True
            Else
                btn_clearlog.Visible = False
                btn_delimport.Visible = False
            End If



            'Catch
            'msgImport.Text = "�������� � ��������� ������!<br>" & Err.Description
            'End Try
        End Sub

        Protected Sub btn_clearlog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clearlog.Click
            '������ ������� ���� �������
            query = dbSQL.ExecuteScalar("DELETE FROM import_log")
            lbl_countlog.Text = ""
            btn_clearlog.Visible = False
            btn_delimport.Visible = False
            lblerror.Text = ""
        End Sub

        Protected Sub btn_delimport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delimport.Click
            '������� ������ � ���� ������� ���� � ���� ���������� �������
            Dim query_del = ""

            Dim reader As SqlClient.SqlDataReader
            query = "SELECT * FROM import_log"
            reader = dbSQL.GetReader(query)
            While reader.Read()
                '��������� ������� ��������
                query_del &= "DELETE FROM " & reader("table_name") & " WHERE " & reader("field_name") & "='" & reader("field_value") & "';"
                If reader("table_name") = "good" Then
                    '��������� �������� �� ��������
                    'delsell(reader("field_value"))
                    query_del &= "UPDATE good SET param_num=param_num-(SELECT param_num FROM good WHERE good_sys_id='" & reader("field_value") & "') WHERE delivery_sys_id=(SELECT delivery_sys_id FROM good WHERE good_sys_id='" & reader("field_value") & "') AND good_type_sys_id=(SELECT good_type_sys_id FROM good WHERE good_sys_id='" & reader("field_value") & "') AND (sale_sys_id is NULL OR sale_sys_id=0);"
                End If
            End While
            reader.Close()

            '������� ������
            query = dbSQL.ExecuteScalar(query_del)

            '������ ������� ���� �������
            query = dbSQL.ExecuteScalar("DELETE FROM import_log")
            lbl_countlog.Text = ""
            btn_clearlog.Visible = False
            btn_delimport.Visible = False
            lblerror.Text = ""
        End Sub

        Function delsell(ByVal sale_id)
            Dim reader As SqlClient.SqlDataReader
            Dim sale_sys_id = dbSQL.ExecuteScalar("SELECT sale_sys_id FROM good WHERE good_sys_id ='" & sale_id & "'")
            If sale_sys_id <> "" Then
                Dim id_delivery = dbSQL.ExecuteScalar("SELECT delivery_sys_id FROM good WHERE good_sys_id ='" & sale_id & "'")
                Dim id_good = dbSQL.ExecuteScalar("SELECT good_type_sys_id FROM good WHERE good_sys_id ='" & sale_id & "'")
                Dim param_num = dbSQL.ExecuteScalar("SELECT param_num FROM good WHERE good_sys_id ='" & sale_id & "'")
                query = dbSQL.ExecuteScalar("UPDATE good SET param_num=param_num+" & param_num & " WHERE delivery_sys_id ='" & id_delivery & "' AND good_type_sys_id ='" & id_good & "' AND (sale_sys_id is NULL OR sale_sys_id=0)")
            End If
            Return True
        End Function



    End Class

End Namespace
