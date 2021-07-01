
Partial Class analysisresult
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim st As String = Request("repname")
        st = "html/" & st & ".html"
        Response.Redirect(st)
        'Dim obj As New Functions
        'grdhtmlreport.DataSource = obj.bind_htmlreport(st)
        'grdhtmlreport.DataBind()
        Page.SmartNavigation = True
    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        
    End Sub
End Class
