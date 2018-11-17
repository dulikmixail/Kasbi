Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiseHttp
        Inherits ServiceExeption
        Implements IService

        Public Function SendRequest(uri As Uri, dataBytes As Byte(), contentTypeValue As String, method As String) As String
            Dim result As String = String.Empty
            Dim req As WebRequest = WebRequest.Create(uri)
            req.ContentType = contentTypeValue
            req.Method = method
            req.ContentLength = dataBytes.Length
            Try
                Dim stream = req.GetRequestStream()
            stream.Write(dataBytes, 0, dataBytes.Length)
            stream.Close()

            Dim res = req.GetResponse().GetResponseStream()

            Dim reader As New StreamReader(res)
            result = reader.ReadToEnd()
            reader.Close()
            res.Close()
            Catch ex As Exception
                Debug.WriteLine("Ничего не делаем!")
            End Try

            Return result
        End Function

        Public Function SendRequestPostJsonUtf8(uri As Uri, json As String) As String
            Dim data = Encoding.UTF8.GetBytes(json)
            Return SendRequest(uri, data, "application/json", "POST")
        End Function

    End Class

End Namespace