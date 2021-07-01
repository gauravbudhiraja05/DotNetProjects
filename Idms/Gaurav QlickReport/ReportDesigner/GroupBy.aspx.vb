
Partial Class ReportDesigner_GroupBy
    Inherits System.Web.UI.Page
    Public gp = ""
    Public fm = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax)) ' Regiser the AjaxClass to be used to bind the tablefields
        If IsPostBack = False Then

            Me.hidElem.Value = Request("elem")

          
        End If
    End Sub
   
End Class
