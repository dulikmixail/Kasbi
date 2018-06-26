Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace WebForms
    Public Class ProductsController
        Inherits ApiController

        Private products As Product() = New Product() {New Product With {
            .Id = 1,
            .Name = "Tomato Soup",
            .Category = "Groceries",
            .Price = 1
            }, New Product With {
            .Id = 2,
            .Name = "Yo-yo",
            .Category = "Toys",
            .Price = 3.75D
            }, New Product With {
            .Id = 3,
            .Name = "Hammer",
            .Category = "Hardware",
            .Price = 16.99D
            }}

        Public Function GetAllProducts() As IEnumerable(Of Product)
            Return products
        End Function

        Public Function GetProductById(ByVal id As Integer) As Product
            Dim product = products.FirstOrDefault(Function(p) p.Id = id)

            If product Is Nothing Then
                Throw New HttpResponseException(HttpStatusCode.NotFound)
            End If

            Return product
        End Function

        Public Function GetProductsByCategory(ByVal category As String) As IEnumerable(Of Product)
            Return products.Where(Function(p) String.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase))
        End Function
    End Class
End Namespace
