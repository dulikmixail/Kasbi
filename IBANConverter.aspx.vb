Imports System.IO
Imports System.Xml

Namespace Kasbi
    Partial Class IBANConverter
        Inherits PageBase

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        Protected Sub lnkLoadXMLFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLoadXMLFile.Click
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader

                cmd = New SqlClient.SqlCommand("get_xml_bank_details")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\iban_conver_file.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)

                While rs.Read
                    Print(1, rs(0))
                End While

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Protected Sub lnkUpdateBankAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUpdateBankAccount.Click
            Dim filePath As Stream
            Dim doc As XmlDocument
            Dim cmd As SqlClient.SqlCommand

            If (fileUpload.FileName <> "") Then
                filePath = fileUpload.FileContent


                doc = New XmlDocument
                doc.Load(filePath)

                Dim elemList As XmlNodeList = doc.GetElementsByTagName("item")
                Dim returnQuery As String


                Dim i As Integer
                Dim j As Integer
                Dim c As Integer
                For i = 0 To elemList.Count - 1
                    If elemList(i).ChildNodes.Item(3).InnerText.Trim() <> "UNDEFINED" And elemList(i).ChildNodes.Item(4).InnerText.Trim() <> "UNDEFINED" Then
                        j = j + 1
                        cmd = New SqlClient.SqlCommand("update_bank_details")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_kodbank", elemList(i).ChildNodes.Item(0).InnerText.Trim())
                        cmd.Parameters.AddWithValue("@pi_account_old", elemList(i).ChildNodes.Item(2).InnerText.Trim())
                        cmd.Parameters.AddWithValue("@pi_bic", elemList(i).ChildNodes.Item(3).InnerText.Trim())
                        cmd.Parameters.AddWithValue("@pi_accountiban", elemList(i).ChildNodes.Item(4).InnerText.Trim())
                        returnQuery = dbSQL.Execute(cmd)
                        c = c + Int32.Parse(returnQuery)
                        msgBoxError.Text &= elemList.Count - 1
                        msgBoxError.Text &= " " & elemList(i).ChildNodes.Item(0).InnerText.Trim() & " " & elemList(i).ChildNodes.Item(2).InnerText.Trim() & " " & elemList(i).ChildNodes.Item(3).InnerText.Trim() & " " & elemList(i).ChildNodes.Item(4).InnerText.Trim() & " "
                    End If
                Next i
                txtLoadInfo.Text = "Количество полей на апдейт: " & j & "Апдейтов: " & c
            Else
                msgBoxError.Text = "Ошибка Файл не выбран. Выберите файл."
            End If
        End Sub

    End Class
End Namespace