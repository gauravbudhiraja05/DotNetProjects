
Partial Class ReportDesigner_ControlID
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.hidSrc.value = Trim(Request("val"))
    End Sub
End Class
