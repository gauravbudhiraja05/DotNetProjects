Imports System.Drawing
Partial Class ReportDesigner_setData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax)) ' Regiser the AjaxClass to be used to bind the tablefields
        If IsPostBack = False Then
            Me.hidObjname.Value = Request("obj").ToString()
            Me.hidTblname.Value = Request("tbl").ToString()
            Me.hidObjall.Value = Request("objs").ToString()
            Me.hidObjvalue.Value = Request("val").ToString()
            Me.hidSource.Value = Request("src").ToString()
            '''''''''' Fill FontFamily'''''''''''''''''''''''''''''''''''
            Dim temp As System.Drawing.FontFamily
            For Each temp In FontFamily.Families
                Me.ddlFontfamily.Items.Add(temp.Name)
                Me.fontFamilycon.Items.Add(temp.Name)
                Me.ddlFontfamily.Items(Me.ddlFontfamily.Items.Count - 1).Value = temp.Name
                Me.fontFamilycon.Items(Me.fontFamilycon.Items.Count - 1).Value = temp.Name
            Next
            ''''''''''''Fontfamily Ends'''''''''''''''''''''''''''''''''''
            Me.ddlFontfamily.SelectedValue = "Verdana"
            Me.fontFamilycon.SelectedValue = "Verdana"
        End If
    End Sub
   
End Class
