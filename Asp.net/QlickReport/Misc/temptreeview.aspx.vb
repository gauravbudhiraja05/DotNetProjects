Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Partial Class Misc_temptreeview
    Inherits System.Web.UI.Page
    Dim nodeWRM, nodeWRM1, nodeWRM2, nodeWRM3 As TreeNode

               

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nodeWRM = New TreeNode
        nodeWRM.Text = "WRM"
        menu.Nodes.Add(nodeWRM)
        nodeWRM1 = New TreeNode
        nodeWRM1.Text = "Add Report"
        nodeWRM.ChildNodes.Add(nodeWRM1)
        'nodeWRM.Nodes.Add(nodeWRM1)
        nodeWRM1.Target = "frmFormTarget"
        nodeWRM1.NavigateUrl = "/IDMS/Menu/ReportDesigner/WFMEntry.aspx"
        nodeWRM2 = New TreeNode
        nodeWRM2.Text = "Edit/View Report"
        nodeWRM.ChildNodes.Add(nodeWRM2)
        nodeWRM2.Target = "frmFormTarget"
        nodeWRM2.NavigateUrl = "/IDMS/Menu/ReportDesigner/WRMlist.aspx"
    End Sub
End Class
