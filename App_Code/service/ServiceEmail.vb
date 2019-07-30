Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Reflection
Imports MimeKit
Imports Models

Namespace Service
    Public Class ServiceEmail
        Inherits ServiceExeption
        Implements IService

        Private Const Host As String = "smtp.yandex.ru"
        Private Const EnableSsl As Boolean = True
        Private Const Port As Integer = 465
        Private Const UserName As String = "info@ramok.by"
        Private Const Password As String = "Ramok30121992"
        Private Const FromEmail As String = "info@ramok.by"
        Private Const FromName As String = "Администрация Рамок - Ramok.by"

        Public Sub SendEmail(email As String, subject As String, messageAsHtml As String,
                             Optional attachmentPaths As String() = Nothing)
            Try
                Using client = New MailKit.Net.Smtp.SmtpClient()
                    client.Connect(Host, Port, EnableSsl)
                    client.Authenticate(UserName, Password)
                    client.Send(BuildEmailMessage(email, subject, messageAsHtml, attachmentPaths))
                    client.Disconnect(True)
                End Using
            Catch ex As Exception
                ServiceLogger.Write(MethodBase.GetCurrentMethod(), ex.Message)
            End Try
        End Sub

        Private Function BuildEmailMessage(email As String, subject As String, messageAsHtml As String,
                                           Optional attachmentPaths As String() = Nothing) As MimeMessage
            Dim builder = New BodyBuilder With {
                    .HtmlBody = messageAsHtml
                    }
            If Not attachmentPaths Is Nothing
                For Each attachmentPath As String In attachmentPaths
                    builder.Attachments.Add(attachmentPath)
                Next
            End If
            Dim emailMessage = New MimeMessage()
            emailMessage.From.Add(New MailboxAddress(FromName, FromEmail))
            emailMessage.[To].Add(New MailboxAddress(email))
            emailMessage.Subject = subject
            emailMessage.Body = builder.ToMessageBody()
            Return emailMessage
        End Function

        Public Sub AddDillersAktForSend(historyCashId As Integer, updateUserId As Integer)
            Dim dr As DataRow
            Dim emails As String()
            Dim goodId As Integer
            Dim customerId As Integer

            Dim emailSubject = "Ваш ремонтный Акт от " & Now.ToString("dd.MM.yyyy") & " - УП Рамок"
            Const emailText =
                      "Здравствуйте. Направляем Вам ремонтный акт, который был сформирован. Просим Вас приезжать с подписанным актами и доверенностью или печатью."

            'Dim ds As DataSet = New DataSet()
            Dim query =
                    "SELECT * FROM cash_history ch INNER JOIN customer c ON ch.owner_sys_id = c.customer_sys_id WHERE ch.state = 5 AND ch.summa IS NOT NULL AND ch.summa > 0 AND c.cto=1 AND ch.repairdate_out IS NOT NULL AND ch.garantia <> 1 AND ch.sys_id =" &
                    historyCashId
            Using ds = New DataSet()
                Using con = ServiceDbConnector.GetSharedConnection()
                    Using adapt = con.GetDataAdapter(query)
                        adapt.Fill(ds)
                    End Using
                End Using


                If ds.Tables(0).Rows.Count > 0
                    dr = ds.Tables(0).Rows(0)
                    emails = dr("email").ToString().Split(New Char() {","c, ";"c, " "c})
                    goodId = Convert.ToInt32(dr("good_sys_id"))
                    customerId = Convert.ToInt32(dr("owner_sys_id"))
                    For Each email As String In emails
                        Try
                            Dim m = New MailAddress(Trim(email))
                            InserOrUpdateEmailSend(historyCashId, m.Address, emailSubject, emailText, updateUserId,
                                                   EmailTypes.SendRepairAkt, False, goodId, customerId)
                        Catch ex As Exception
                        End Try

                    Next
                End If
            End Using
        End Sub

        Private Sub InserOrUpdateEmailSend(historyCashId As Integer, email As String, emailSubject As String,
                                           emailText As String, updateUserId As Integer, emailType As EmailTypes,
                                           isSend As Boolean, Optional goodId As Integer = 0,
                                           Optional customerId As Integer = 0)
            Using cmd = New SqlCommand("insert_or_update_email_send")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_hc_sys_id", historyCashId)
                cmd.Parameters.AddWithValue("@pi_good_sys_id", IIf(goodId > 0, goodId, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", IIf(customerId > 0, customerId, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_recipient", email)
                cmd.Parameters.AddWithValue("@pi_email_subject", emailSubject)
                cmd.Parameters.AddWithValue("@pi_email_text", emailText)
                cmd.Parameters.AddWithValue("@pi_executor", updateUserId)
                cmd.Parameters.AddWithValue("@pi_email_type_sys_id", emailType)
                cmd.Parameters.AddWithValue("@pi_is_send", isSend)
                Using con = ServiceDbConnector.GetSharedConnection()
                    con.Execute(cmd)
                End Using
            End Using
        End Sub
    End Class
End Namespace

