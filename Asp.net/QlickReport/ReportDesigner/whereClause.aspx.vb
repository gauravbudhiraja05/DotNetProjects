
Partial Class ReportDesigner_whereClause
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax)) ' Regiser the AjaxClass to be used to bind the tablefields
        Me.hidTables.Value = Trim(Request("tbl"))
    End Sub
End Class
