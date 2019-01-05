Imports System.Data.SqlClient
Imports System.Net.Configuration
Imports Exeption
Imports Kasbi
Imports Models

Namespace Service
    Public Class ServiceGood
        Inherits ServiceExeption
        Implements IService

        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()

        Dim ReadOnly _
            _allowedStatesTransitions As Dictionary(Of Integer, Integer()) = New Dictionary(Of Integer, Integer())

        Public Sub New()
            InitDictStates()
        End Sub

        Private Sub InitDictStates()
            _allowedStatesTransitions.Add(0, New Integer() {1, 10})
            _allowedStatesTransitions.Add(1, New Integer() {2})
            _allowedStatesTransitions.Add(2, New Integer() {3, 12})
            _allowedStatesTransitions.Add(3, New Integer() {0})
            _allowedStatesTransitions.Add(10, New Integer() {1, 11})
            _allowedStatesTransitions.Add(11, New Integer() {12, 21})
            _allowedStatesTransitions.Add(12, New Integer() {13, 22})
            _allowedStatesTransitions.Add(13, New Integer() {23})
            _allowedStatesTransitions.Add(21, New Integer() {31, 22})
            _allowedStatesTransitions.Add(22, New Integer() {23, 32})
            _allowedStatesTransitions.Add(23, New Integer() {33})
            _allowedStatesTransitions.Add(31, New Integer() {0})
            _allowedStatesTransitions.Add(32, New Integer() {33})
            _allowedStatesTransitions.Add(33, New Integer() {0})
        End Sub

        Private Function GetInfoCounterGoodByDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer) _
            As InfoCounterGood
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim infoCounterGood As InfoCounterGood

            cmd = New SqlClient.SqlCommand("get_good_type_by_delivery_info")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", idGoodType)
            cmd.Parameters.AddWithValue("@pi_delivery_sys_id", idDelivery)
            adapt = _sharedDbSql.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)
            With ds.Tables(0).DefaultView(0)
                infoCounterGood = New InfoCounterGood(idDelivery, idGoodType, .Item("name").ToString(),
                                                      CInt(.Item("prihod")), CInt(.Item("rashod")),
                                                      CInt(.Item("ostatok")))
            End With

            Return infoCounterGood
        End Function

        Private Function IsExistDelivery(ByVal idDelivery As Integer) As Boolean
            If _sharedDbSql.ExecuteScalar("SELECT COUNT(*) FROM delivery WHERE delivery_sys_id = " & idDelivery) <> 0 _
                Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function IsExistGoodType(ByVal idGoodType As Integer) As Boolean
            If _sharedDbSql.ExecuteScalar("SELECT COUNT(*) FROM good_type WHERE good_type_sys_id = " & idGoodType) <> 0 _
                Then
                Return True
            Else
                Return False
            End If
        End Function


        Public Function CanAddGoodToDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer,
                                             ByVal count As Integer) As Boolean
            Dim exeption As GoodTransferExeption = New GoodTransferExeption(idGoodType, Nothing, idDelivery)
            Dim infoCounterGood As InfoCounterGood = GetInfoCounterGoodByDelivery(idDelivery, idGoodType)

            If infoCounterGood.ostatok >= count Then
                Return True
            Else
                exeption.AddTextToList(
                    "Нельзя добавить товар, т.к в поставке уже закрыта. Увеличьте количество товаров в поставке или переместите товар в другую поставку.")
                AddExeption(exeption)
                Return False
            End If
        End Function

        Public Function CanTakeGoodFromDelivery(ByVal idDelivery As Integer, ByVal idGoodType As Integer,
                                                ByVal count As Integer) As Boolean
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

        Public Function CheckTransferDelivery(ByVal idDeliveryOld As Integer, ByVal idDeliveryNew As Integer,
                                              ByVal idGoodTypeOld As Integer, ByVal idGoodTypeNew As Integer,
                                              ByVal count As Integer) As Boolean
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
                    exeption.AddTextToList(
                        "Конечная поставка переполнена или произошел сбой. Пожалуйста сообщите об этой ошибке Администратору. ID поставки = " &
                        idDeliveryNew & ".")
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

        Public Function CheckNumbers(ByVal number As String, ByVal type As NumbersCashRegister,
                                     ByVal goodTypeId As String) As Boolean
            Dim exeption As BaseExeption = New BaseExeption()
            Select Case type
                Case NumbersCashRegister.Number
                    If IsEmptyString(number) Then
                        exeption.AddTextToList("Введите заводской номер.")
                    Else
                        If number.Length = 8 Or number.Length = 13 Then
                            If _
                                _sharedDbSql.ExecuteScalar(
                                    "select count(*) from good where num_cashregister='" & number &
                                    "' and good_type_sys_id='" & goodTypeId & "'") > 0 Then
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
                            If _
                                _sharedDbSql.ExecuteScalar(
                                    "select count(*) from good where num_control_reestr='" & number & "'") > 0 Then
                                exeption.AddTextToList("Введенный номер СК реестра уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК реестра.")
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.ROM
                    If _
                        Not _
                        _sharedDbSql.ExecuteScalar(
                            "select name from good_type where good_type_sys_id='" & goodTypeId & "'").
                            ToString.Contains("Касби-03МФ") Then
                        If IsEmptyString(number) Then
                            exeption.AddTextToList("Введите номер ПЗУ реестра.")
                        Else
                            If number.Length = 11 Then
                                If _
                                    _sharedDbSql.ExecuteScalar(
                                        "select count(*) from good where num_control_pzu='" & number & "'") > 0 Then
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
                            If _
                                _sharedDbSql.ExecuteScalar(
                                    "select count(*) from good where num_control_mfp='" & number & "'") >
                                0 Then
                                exeption.AddTextToList("Введенный номер СК МФП уже занесен в базу.")
                            End If
                        Else
                            exeption.AddTextToList("Введите корректный номер СК МФП.")
                        End If
                    End If
                    Exit Select
                Case NumbersCashRegister.CPU
                    If number.Length = 11 Then
                        If _
                            _sharedDbSql.ExecuteScalar("select count(*) from good where num_control_cp='" & number & "'") >
                            0 _
                            Then
                            exeption.AddTextToList("Введенный номер СК ЦП уже занесен в базу.")
                        End If
                    Else
                        exeption.AddTextToList("Введите корректный номер СК ЦП.")
                    End If
                    Exit Select
                Case NumbersCashRegister.SC1
                    If Not IsEmptyString(number) Then
                        If number.Length = 11 Or number.Length = 0 Then
                            If _
                                _sharedDbSql.ExecuteScalar(
                                    "select count(*) from good where num_control_cto='" & number & "'") >
                                0 Then
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
                            If _
                                _sharedDbSql.ExecuteScalar(
                                    "select count(*) from good where num_control_cto2='" & number & "'") >
                                0 Then
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

        Private Function CheckChangeStates(currentStates As Integer, nextStates As Integer) As Boolean
            If _allowedStatesTransitions.ContainsKey(currentStates)
                Return _
                    _allowedStatesTransitions.Item(currentStates).Any(Function(allowedStates) allowedStates = nextStates)
            End If
            Return False
        End Function

        Public Function GetStateRepair(goodId As Integer) As Integer
            Return Convert.ToInt32(
                _sharedDbSql.ExecuteScalar(
                    "SELECT ISNULL(state_repair, 0) state_repair FROM good WHERE good_sys_id = " +
                    goodId.ToString()))
        End Function
        Public Function GetStateRepair(goodId As Object) As Integer
            Return GetStateRepair(Convert.ToInt32(goodId))
        End Function

        Public Sub SetStateRepair(goodId As Integer, stateRepair As Integer,
                                  Optional ignoreValidation As Boolean = False)
            If CheckChangeStates(GetStateRepair(goodId), stateRepair) Or ignoreValidation
                Dim cmd As SqlCommand = New SqlCommand("set_state_repair")
                cmd.Parameters.AddWithValue("@pi_state_repair", stateRepair)
                cmd.Parameters.AddWithValue("@pi_good_sys_id", goodId)
                cmd.CommandType = CommandType.StoredProcedure
                _sharedDbSql.Execute(cmd)
            Else
                Throw New Exception("Not set repair state")
            End If
        End Sub

        Public Sub SetStateRepair(goodId As Object, stateRepair As Integer,
                                  Optional ignoreValidation As Boolean = False)
            SetStateRepair(Convert.ToInt32(goodId), stateRepair, ignoreValidation)
        End Sub
    End Class
End Namespace