
Partial Class ReportDesigner_Having
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax)) ' Regiser the AjaxClass to be used to bind the tablefields
        If IsPostBack = False Then
            Me.hidTblname.Value = Request("tbl").ToString()
        End If
    End Sub
End Class
