Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Graphicalpresentation_showgraph
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(conn)
    Dim ds As New DataSet
    Dim cmd As SqlCommand
    Dim adp As New SqlDataAdapter
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblReportname.Text = Session("repname") + "" + "Report"
        Dim value As String
        Try
            value = Session("reportquery")
            cmd = New SqlCommand(value, con)
            con.Open()
            adp.SelectCommand = cmd
            adp.Fill(ds)
        Catch ex As Exception
            aspnet_msgbox(".")
        End Try
        gdGraphreport.DataSource = ds
        gdGraphreport.DataBind()
        con.Close()
    End Sub

    Protected Sub gdGraphreport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdGraphreport.PageIndexChanging
        If gdGraphreport.PageIndex < gdGraphreport.PageCount And gdGraphreport.PageIndex >= 0 Then
            gdGraphreport.PageIndex = e.NewPageIndex
            gdGraphreport.DataSource = ds
            gdGraphreport.DataBind()
        End If
    End Sub
End Class
