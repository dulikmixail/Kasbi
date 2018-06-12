Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Web.UI.WebControls.Expressions
Imports Kasbi

Namespace service
    Public Class ServiceGood
        Inherits PageBase
        Private exeptionText As String = String.Empty
        Private listGoodExeption As New List(Of GoodExeption)


        Public Function CheckTransferDelivery(ByVal idDeliveryFrom As Integer, ByVal idDeliveryTo As Integer, ByVal goodType As Integer, ByVal count As Integer) As Boolean
            exeptionText = String.Empty
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim dsFrom As DataSet
            Dim dsTo As DataSet

            If dbSQL.ExecuteScalar("SELECT COUNT(*) FROM delivery WHERE delivery_sys_id = " & idDeliveryFrom) = 0 Then
                exeptionText &= "Исходяшая поставка не найдена. "
            End If
            If dbSQL.ExecuteScalar("SELECT COUNT(*) FROM delivery WHERE delivery_sys_id = " & idDeliveryTo) = 0 Then
                exeptionText &= "Конечная поставка не найдена. "
            End If
            If dbSQL.ExecuteScalar("SELECT COUNT(*) FROM good_type WHERE good_type_sys_id = " & goodType) = 0 Then
                exeptionText &= "Выбранный товар не найдена. "
            End If

            cmd = New SqlClient.SqlCommand("get_good_type_by_delivery_info")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", goodType)
            cmd.Parameters.AddWithValue("@pi_delivery_sys_id", idDeliveryFrom)
            adapt = dbSQL.GetDataAdapter(cmd)
            dsFrom = New DataSet

            cmd = New SqlClient.SqlCommand("get_good_type_by_delivery_info")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", goodType)
            cmd.Parameters.AddWithValue("@pi_delivery_sys_id", idDeliveryTo)
            adapt = dbSQL.GetDataAdapter(cmd)
            dsTo = New DataSet
            adapt.Fill(dsTo)

            If dsFrom.Tables(0).Rows.Count > 0 Then
                With dsFrom.Tables(0).DefaultView(0)
                    Dim ostatok As Integer = CInt(.Item("ostatok"))
                    Dim rashod As Integer = CInt(.Item("rashod"))
                    Dim prihod As Integer = CInt(.Item("prihod"))
                    If rashod < count Then
                        exeptionText &= "В исходящей поставке нет данного количества товара. "
                    End If
                    If prihod = 0 Then
                        exeptionText &= "Приход равен 0. "
                    End If
                End With
            Else
                exeptionText &= "В исходяшей поставке нет данного товара. "
            End If

            If dsTo.Tables(0).Rows.Count > 0 Then
                With dsTo.Tables(0).DefaultView(0)
                    Dim ostatok As Integer = CInt(.Item("ostatok"))
                    If ostatok < 0 Then
                        exeptionText &= "Конечная поставка переполнена или произошел сбой. Пожалуйста сообщите об этой ошибке Администратору. ID поставки = " & idDeliveryTo & ". "
                    End If
                    If ostatok = 0 Then
                        exeptionText &= "Конечная поставка уже полная и закрыта. "
                    Else
                        If ostatok < count Then
                            exeptionText &= "В Конечная поставке нет такого количества свободного товара. "
                        End If
                    End If
                End With
            Else
                exeptionText &= "В конечной поставке нет данного товара. "
            End If

            If String.IsNullOrEmpty(exeptionText) Then
                Return True
            Else
                listGoodExeption.Add(New GoodExeption(goodType, idDeliveryFrom, idDeliveryTo, exeptionText))
                Return False
            End If

        End Function


        Public Function GetLastExeption() As String
            Return exeptionText
        End Function

        Public Function GetListToExeption() As List(Of GoodExeption)
            Return listGoodExeption
        End Function
        Public Function GetListStringGoodTypeSysId() As String()
            Dim list As String() = New String(listGoodExeption.Count) {}
            For j = 0 To listGoodExeption.Count - 1
                list(j) = listGoodExeption(j).idGoodType.ToString()
            Next
            Return list
        End Function

        Public Function GetExeptionTextByGoodId(idGoodType As Integer) As String
            For Each toExeption In listGoodExeption
                If toExeption.idGoodType = idGoodType Then
                    Return toExeption.text
                End If
            Next
            Return ""
        End Function

    End Class
End Namespace