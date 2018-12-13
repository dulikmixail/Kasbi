Imports System.IO
Imports System.IO.Compression
Imports System.Threading
Imports Kasbi
Imports Microsoft.Office.Interop.Word
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Service
    Public Class ServiceDocuments
        Inherits PageBase

        ReadOnly _monthReplacements1 As Hashtable = New Hashtable()
        ReadOnly _monthReplacements2 As Hashtable = New Hashtable()

        Private _wrdApp As Microsoft.Office.Interop.Word.ApplicationClass
        Private _wrdDoc As Microsoft.Office.Interop.Word.DocumentClass

        Public Sub New()
            _monthReplacements1.Add("01", "января")
            _monthReplacements1.Add("02", "февраля")
            _monthReplacements1.Add("03", "марта")
            _monthReplacements1.Add("04", "апреля")
            _monthReplacements1.Add("05", "мая")
            _monthReplacements1.Add("06", "июня")
            _monthReplacements1.Add("07", "июля")
            _monthReplacements1.Add("08", "августа")
            _monthReplacements1.Add("09", "сентября")
            _monthReplacements1.Add("10", "октября")
            _monthReplacements1.Add("11", "ноября")
            _monthReplacements1.Add("12", "декабря")

            _monthReplacements2.Add("01", "январе")
            _monthReplacements2.Add("02", "феврале")
            _monthReplacements2.Add("03", "марте")
            _monthReplacements2.Add("04", "апреле")
            _monthReplacements2.Add("05", "мае")
            _monthReplacements2.Add("06", "июне")
            _monthReplacements2.Add("07", "июле")
            _monthReplacements2.Add("08", "августе")
            _monthReplacements2.Add("09", "сентябре")
            _monthReplacements2.Add("10", "октябре")
            _monthReplacements2.Add("11", "ноябре")
            _monthReplacements2.Add("12", "декабре")
        End Sub


        Public Sub AktForTOandDolg(checkGoods As ListDictionary, httpResponse As HttpResponse, Optional withDate As Boolean = True,
                                        Optional dateAkt As DateTime = Nothing)
            Dim savePath As String = String.Empty
            If checkGoods.Count > 0 Then
                savePath = CreateAllAktForTOandDolg(checkGoods, withDate, dateAkt)
            End If
            ResponseFile(savePath, httpResponse)
        End Sub

        Private Function CreateAllAktForTOandDolg(checkGoods As ListDictionary, Optional withDate As Boolean = True,
                                                  Optional dateAkt As DateTime = Nothing) As String

            Dim filePath, rootPath, folderPath As String
            Const fileName As String = "Akt_Of_TO_And_Dolg"
            Const folderName As String = "Akt_Of_TO_And_Dolg"
            Dim interval As Integer = 50
            Dim currentStep As Integer = interval
            Dim currentIndex As Integer = 0
            Dim subCheckGoods As ListDictionary = New ListDictionary


            rootPath = Server.MapPath("Docs") & "\Akts\" & Session("User").sys_id.ToString
            folderPath = rootPath & "\" & folderName

            If dateAkt = Nothing Then
                dateAkt = DateTime.Today
            End If

            If Directory.Exists(folderPath)
                Directory.Delete(folderPath, True)
            End If

            If checkGoods.Count > 50
                For Each checkGood As DictionaryEntry In checkGoods
                    If currentIndex < currentStep
                        SubCheckGoods.Add(checkGood.Key, checkGood.Value)
                        currentIndex += 1
                    Else
                        CreateAktForTOandDolg(SubCheckGoods, folderPath, fileName & "__" & currentIndex, withDate,
                                              dateAkt)
                        currentStep += interval
                        subCheckGoods = New ListDictionary
                    End If
                Next
                CreateAktForTOandDolg(SubCheckGoods, folderPath, fileName & "__" & checkGoods.Count, withDate, dateAkt)
                filePath = rootPath & "\" & fileName & ".zip"
                If File.Exists(filePath)
                    File.Delete(filePath)
                End If
                Thread.Sleep(2000)
                ZipFile.CreateFromDirectory(folderPath, filePath)
            Else
                filePath = rootPath & "\" &
                           CreateAktForTOandDolg(checkGoods, rootPath, fileName, withDate, dateAkt)
            End If
            Return filePath
        End Function

        Private Function CreateAktForTOandDolg(checkGoods As ListDictionary, rootPath As String, fileName As String,
                                               Optional withDate As Boolean = True,
                                               Optional dateAkt As DateTime = Nothing) As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim drs() As Data.DataRow
            Dim templatePath, savePath, filter, fullFileName As String

            templatePath = Server.MapPath("Templates\") & "Akt_Of_TO_And_Dolg.doc"
            fullFileName = fileName & ".doc"
            savePath = rootPath & "\" & fullFileName

            CopyFile(templatePath, savePath, overwrite := True)
            Try
                _wrdApp = New ApplicationClass()
                _wrdApp.Caption = "Caption1"
                _wrdApp.DisplayAlerts = WdAlertLevel.wdAlertsNone
                _wrdApp.Visible = False

                _wrdDoc = _wrdApp.Documents.Open(savePath)
                With _wrdDoc.Sections(1).Range
                    .Select()
                    .Copy()
                End With

                filter = " "
                If checkGoods.Count <> 0 Then
                    Dim stringKeys As String = ""
                    For Each key In checkGoods.Keys
                        stringKeys &= key.ToString() & ","
                    Next
                    stringKeys = stringKeys.Remove(stringKeys.Length - 1, 1)
                    filter = " WHERE good.good_sys_id IN (" & stringKeys & ") "
                End If

                cmd = New SqlClient.SqlCommand("prc_rpt_AktsTOAndDolg")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_filter", filter)

                cmd.CommandTimeout = 0

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                drs = ds.Tables(0).Select()

                For k = drs.Length - 1 To 0 Step -1

                    'If k Mod 2 = 0 Then
                    paste_Part1(drs, k, withDate, dateAkt, checkGoods)
                    'Else
                    paste_Part2(drs, k, withDate, dateAkt, checkGoods)
                    empty_Part2()
                    If k <> 0 Then
                        With _wrdDoc.Sections(1).Range
                            .Collapse(WdCollapseDirection.wdCollapseEnd)
                            .InsertBreak(WdBreakType.wdPageBreak)
                            .Collapse(WdCollapseDirection.wdCollapseEnd)
                            .Paste()
                        End With
                    End If
                    'End If
                Next
                If drs.Length Mod 2 = 1 Then
                    clear_Part2()
                End If

                _wrdDoc.Close(True)
                _wrdApp.Quit()

            Catch ex As Exception
                _wrdDoc.Close(True)
                _wrdApp.Quit()
            End Try
            Return fullFileName
        End Function

        Private Sub paste_Part1(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime,
                                checkGoods As ListDictionary)
            If withDate Then
                Dim test As String = dateAkt.ToString("dd")
                _wrdDoc.Bookmarks("Day1").Range.Text = test
            End If
            _wrdDoc.Bookmarks("Counter1").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt1").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month1_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("Month1_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("NumDog1").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works1").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash1").Range.Text = drs(k).Item(2).ToString().Trim()
            paste_Summa_TO(drs, k, 1)
            _wrdDoc.Bookmarks("DateDolg1").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg1").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer1").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master1").Range.Text = CurrentUser.Name
        End Sub

        Private Sub paste_Part2(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime,
                                checkGoods As ListDictionary)
            If withDate Then
                _wrdDoc.Bookmarks("Day2").Range.Text = dateAkt.ToString("dd")
            End If
            _wrdDoc.Bookmarks("Counter2").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt2").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month2_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("Month2_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("NumDog2").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works2").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash2").Range.Text = drs(k).Item(2).ToString().Trim()
            paste_Summa_TO(drs, k, 2)
            _wrdDoc.Bookmarks("DateDolg2").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg2").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer2").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master2").Range.Text = CurrentUser.Name
        End Sub

        Private Sub paste_Summa_TO(drs() As Data.DataRow, k As Integer, num As Integer)
            If InStr(drs(k).Item(6).ToString(), "ТО1") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "15.00 (пятнадцать) рублей 00 копеек, в т. ч. НДС (20%) 2.50 (два рубля 50 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО2") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "7.00 (семь) рублей 50 копеек, в т. ч. НДС (20%) 1.25 (один рубль 25 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО3") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "9.00 (девять) рублей 60 копеек, в т. ч. НДС (20%) 1.60 (один рубль 60 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО4") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "24.00 (двадцать четыре) рубля 00 копеек, в т. ч. НДС (20%) 4.00 (четыре рубля 00 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО5") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "6.00 (шесть) рублей 00 копеек, в т. ч. НДС (20%) 1.00 (один рубль 00 копеек)."
            End If
        End Sub

        Private Sub clear_Part2()
            _wrdDoc.Bookmarks("Akt2_body").Range.Text = " "
        End Sub

        Private Sub empty_Part2()
            _wrdDoc.Bookmarks("Akt2_body").Delete()
        End Sub

        Private Sub ResponseFile(savePath As String, httpResponse As HttpResponse)
            Dim fileExtension = savePath.Substring(savePath.Length - 3, 3)
            Dim file As IO.FileInfo
            file = New FileInfo(savePath)
            If file.Exists Then
                httpResponse.Clear()
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                httpResponse.AddHeader("Content-Length", file.Length.ToString())
                httpResponse.ContentType = "application/octet-stream"
                If fileExtension = "zip"
                    httpResponse.ContentType = "application/zip"
                Else
                    httpResponse.ContentType = "application/octet-stream"
                End If
                httpResponse.WriteFile(savePath)
                httpResponse.End()
            Else
                httpResponse.Write("This file does not exist.")
            End If
        End Sub
    End Class
End Namespace