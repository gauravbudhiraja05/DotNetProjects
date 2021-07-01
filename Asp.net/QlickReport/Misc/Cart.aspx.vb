Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Misc_Cart
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        connection.Open()
        Dim code As String
        cmd = New SqlCommand("select ProductCode from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            code = rdr("ProductCode").ToString()
            productcode.Text = rdr("ProductCode")
        End If
        cmd.Dispose()
        rdr.Close()

        cmd = New SqlCommand("select Price from ProductMaster where ProductCode='" + code + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            code = rdr("Price")
            productcost.Text = rdr("Price") & "US$"
        End If
        cmd.Dispose()
        rdr.Close()
        connection.Close()
    End Sub

    Protected Sub procedpayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles procedpayment.Click
        Response.Redirect("../Misc/PaymentGateway.aspx")
    End Sub
End Class
