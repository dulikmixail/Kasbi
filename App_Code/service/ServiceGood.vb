Imports System.Net.Configuration
Imports Exeption
Imports Kasbi
Imports Models

Namespace Service
    Public Class ServiceGood
        Inherits ServiceExeption
        Implements IService

        Private Function GetInfoCounterGoodByDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer) As InfoCounterGood
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim infoCounterGood As InfoCounterGood

            cmd = New SqlClient.SqlCommand("get_good_type_by_delivery_info")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", idGoodType)
            cmd.Parameters.AddWithValue("@pi_delivery_sys_id", idDelivery)
            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)
            With ds.Tables(0).DefaultView(0)
                infoCounterGood = New InfoCounterGood(idDelivery, idGoodType, .Item("name").ToString(), CInt(.Item("prihod")), CInt(.Item("rashod")), CInt(.Item("ostatok")))
            End With

            Return infoCounterGood
        End Function

        Private Function IsExistDelivery(ByVal idDelivery As Integer) As Boolean
            If dbSQL.ExecuteScalar("SELECT COUNT(*) FROM delivery WHERE delivery_sys_id = " & idDelivery) <> 0 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function IsExistGoodType(ByVal idGoodType As Integer) As Boolean
            If dbSQL.ExecuteScalar("SELECT COUNT(*) FROM good_type WHERE good_type_sys_id = " & idGoodType) <> 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        Public Function CanAddGoodToDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer, ByVal count As Integer) As Boolean
            Dim exeption As GoodTransferExeption = New GoodTransferExeption(idGoodType, Nothing, idDelivery)
            Dim infoCounterGood As InfoCounterGood = GetInfoCounterGoodByDelivery(idDelivery, idGoodType)

            If infoCounterGood.ostatok >= count Then
                Return True
            Else
                exeption.AddTextToList("Нельзя добавить товар, т.к в поставке уже закрыта. Увеличьте количество товаров в поставке или переместите товар в другую поставку.")
                AddExeption(exeption)
                Return False
            End If

        End Function
        Public Function CanTakeGoodFromDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer, ByVal count As Integer) As Boolean
            Dim exeption As GoodTransferExeption = New GoodTransferExeption(idGoodType, idDelivery, Nothing)
            Dim infoCounterGood As InfoCounterGood = GetInfoCounterGoodByDelivery(idDelivery, idGoodType)

            If infoCounterGood.rashod >= count Then
                Return True
            Else
                exeption.AddTextToList("Нельзя взять товар из поставки, т.к нет введенных товаров в этой поставке.")
                AddExeption(exeption)
                Return False
            End If
        End Function

        Public Function CheckTransferDelivery(ByVal idDeliveryOld As Integer, ByVal idDeliveryNew As Integer, ByVal idGoodTypeOld As Integer, ByVal idGoodTypeNew As Integer, ByVal count As Integer) As Boolean
            Dim exeption As GoodTransferExeption = New GoodTransferExeption(idGoodTypeOld, idDeliveryOld, idDeliveryNew)
            Dim infoCounterGood As InfoCounterGood

            If IsExistDelivery(idDeliveryOld) Then
                exeption.AddTextToList("Исходяшая поставка не найдена.")
            End If
            If IsExistDelivery(idDeliveryNew) Then
                exeption.AddTextToList("Конечная поставка не найдена.")
            End If
            If IsExistGoodType(idGoodTypeOld) Then
                exeption.AddTextToList("Исходный товар не найдена.")
            End If
            If IsExistGoodType(idGoodTypeNew) Then
                exeption.AddTextToList("Конечный товар не найдена.")
            End If


            infoCounterGood = GetInfoCounterGoodByDelivery(idDeliveryOld, idGoodTypeOld)
            If Not IsNothing(infoCounterGood) Then
                If infoCounterGood.rashod < count Then
                    exeption.AddTextToList("В исходящей поставке нет данного количества товара.")
                End If
                If infoCounterGood.prihod = 0 Then
                    exeption.AddTextToList("Приход равен 0.")
                End If
                If infoCounterGood.prihod < 0 Then
                    exeption.AddTextToList("Приход меньше 0. Сообщите об этой ошибке Администратору")
                End If
            Else
                exeption.AddTextToList("В исходяшей поставке нет данного товара.")
            End If
            infoCounterGood = GetInfoCounterGoodByDelivery(idDeliveryNew, idGoodTypeNew)
            If Not IsNothing(infoCounterGood) Then
                If infoCounterGood.ostatok < 0 Then
                    exeption.AddTextToList("Конечная поставка переполнена или произошел сбой. Пожалуйста сообщите об этой ошибке Администратору. ID поставки = " & idDeliveryNew & ".")
                End If
                If infoCounterGood.ostatok = 0 Then
                    exeption.AddTextToList("Конечная поставка уже полная и закрыта.")
                Else
                    If infoCounterGood.ostatok < count Then
                        exeption.AddTextToList("В конечной поставке нет такого количества свободного товара.")
                    End If
                End If
            Else
                exeption.AddTextToList("В конечной поставке нет данного товара.")
            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
                Return False
            Else
                Return True
            End If

        End Function

        Public Function CheckNumbers(ByVal number As String, ByVal type As NumbersCashRegister, ByVal goodTypeId As String) As Boolean
            Dim exeption As BaseExeption = New BaseExeption()
            Select Case type
                Case NumbersCashRegister.Number
                    If IsEmptyString(number) Then
                        exeption.AddTextToList("Введите заводской номер.")
                    Else
                        If number.Length = 8 Or number.Length = 13 Then
                            If dbSQL.ExecuteScalar("select count(*) from good where num_cashregister='" & number & "' and good_type_sys_id='" & goodTypeId & "'") > 0 Then
                                exeption.AddTextToList("Введенный заводской номер уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный заводской номер.")
                        End If

                    End If
                    Exit Select
                Case NumbersCashRegister.Register
                    If IsEmptyString(number) Then
                        exeption.AddTextToList("Введите номер СК реестра.")
                    Else
                        If number.Length = 11 Then
                            If dbSQL.ExecuteScalar("select count(*) from good where num_control_reestr='" & number & "'") > 0 Then
                                exeption.AddTextToList("Введенный номер СК реестра уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК реестра.")
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.ROM
                    If Not dbSQL.ExecuteScalar("select name from good_type where good_type_sys_id='" & goodTypeId & "'").ToString.Contains("Касби-03МФ") Then
                        If IsEmptyString(number) Then
                            exeption.AddTextToList("Введите номер ПЗУ реестра.")
                        Else
                            If number.Length = 11 Then
                                If dbSQL.ExecuteScalar("select count(*) from good where num_control_pzu='" & number & "'") > 0 Then
                                    exeption.AddTextToList("Введенный номер СК ПЗУ уже занесен в базу.")
                                End If
                            Else
                                exeption.AddTextToList("Введите корректный номер СК ПЗУ.")
                            End If
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.FiscalMemory
                    If IsEmptyString(number) Then
                        exeption.AddTextToList("Введите номер МФП реестра.")
                    Else
                        If number.Length = 11 Then
                            If dbSQL.ExecuteScalar("select count(*) from good where num_control_mfp='" & number & "'") > 0 Then
                                exeption.AddTextToList("Введенный номер СК МФП уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК МФП.")
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.CPU
                    If number.Length = 11 Then
                        If dbSQL.ExecuteScalar("select count(*) from good where num_control_cp='" & number & "'") > 0 Then
                            exeption.AddTextToList("Введенный номер СК ЦП уже занесен в базу.")
                        End If
                    Else
                        exeption.AddTextToList("Введите корректный номер СК ЦП.")
                    End If
                    Exit Select
                Case NumbersCashRegister.SC1
                    If Not IsEmptyString(number) Then
                        If number.Length = 11 Or number.Length = 0 Then
                            If dbSQL.ExecuteScalar("select count(*) from good where num_control_cto='" & number & "'") > 0 Then
                                exeption.AddTextToList("Введенный номер СК ЦТО уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК ЦТО.")
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.SC2
                    If Not IsEmptyString(number) Then
                        If number.Length = 11 And number.Length = 0 Then
                            If dbSQL.ExecuteScalar("select count(*) from good where num_control_cto2='" & number & "'") > 0 Then
                                exeption.AddTextToList("Введенный номер СК ЦТО 2 уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК ЦТО 2.")
                        End If
                    End If
                    Exit Select
            End Select
            If exeption.HaveAnyText() Then
                AddExeption(exeption)
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsEmptyString(ByVal str As String) As Boolean
            Return str.Trim() = String.Empty
        End Function

    End Class
End Namespace