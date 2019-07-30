Imports System.Data.SqlClient
Imports System.Net.Configuration
Imports Exeption
Imports Kasbi
Imports Models

Namespace Service
    Public Class ServiceSale
        Inherits ServiceExeption
        Implements IService

        Public Function GetNextSaleDogovorByCustomer(ByVal idCustomer As Integer) As Integer
            Dim exeption As SaleExeption = New SaleExeption()
            Dim resQuery As String = String.Empty
            Dim sSubDogovor As Integer
            Try

                Dim reader As SqlDataReader = dbSQL.GetReader("select top 1 dogovor from sale where customer_sys_id= " & idCustomer & " order by sale_sys_id desc")
                If (reader.Read()) Then
                    resQuery = reader.Item(0).ToString()
                End If
                reader.Close()
                Int32.TryParse(resQuery, sSubDogovor)
                If (sSubDogovor = -1) Then
                    sSubDogovor = 0
                End If
                sSubDogovor += 1
            Catch
                exeption.AddTextToList("Ошибка определения номера приложения к договору!")
            End Try
            If exeption.HaveAnyText() Then
                AddExeption(exeption)
            End If
            Return sSubDogovor
        End Function

        Public Function GetFirstSaleWhereUnpEqualNumDogovor(ByVal idCustomer As Integer) As DataRow
            Dim exeption As SaleExeption = New SaleExeption()
            Dim reader As SqlDataReader = Nothing
            Dim dt As DataTable = New DataTable()
            Try
                reader = dbSQL.GetReader("select top 1 s.* from sale s inner join customer c on s.customer_sys_id=c.customer_sys_id where s.customer_sys_id= " & idCustomer & " and s.dogovor > '0' and c.unn=c.dogovor order by sale_sys_id desc")
                If Not (reader.HasRows) Then
                    exeption.AddTextToList("Не найдена первая продажа")
                End If
                dt.Load(reader)
                reader.Close()
            Catch
                exeption.AddTextToList("Ошибка получения первой продажи")
                reader.Close()
            End Try
            If exeption.HaveAnyText() Then
                AddExeption(exeption)
            End If
            Return dt.Rows(0)
        End Function

    End Class
End Namespace
