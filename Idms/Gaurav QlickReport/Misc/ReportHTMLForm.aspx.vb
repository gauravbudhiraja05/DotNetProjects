Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Partial Class Misc_ReportHTMLForm
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        If Me.IsPostBack = False Then
            Dim cmdgetdept As New SqlCommand("select * from IdmsDepartment", connection)
            Dim dsgetdept As New DataSet
            Dim adpgetdept As New SqlDataAdapter
            adpgetdept.SelectCommand = cmdgetdept
            connection.Open()
            adpgetdept.Fill(dsgetdept)
            connection.Close()
            cbodept.DataSource = dsgetdept
            cbodept.DataTextField = "DepartmentName"
            cbodept.DataValueField = "autoid"
            cbodept.DataBind()
            cbodept.Items.Insert(0, "--Select--")

            ''''' to retain span value
            hidlb.Value = Session("lb")
            hidcl.Value = Session("cl")
            hiddpt.Value = Session("dp")
            Session("lb") = ""
            Session("cl") = ""
            Session("dp") = ""
            '''''''''
        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Showmsg", str.ToString)
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        txtclient.Text = ""
        txtlob.Text = ""
        Dim lob
        Dim client
        If Request("cbolob") = "" Or Request("cbolob") = "--Select--" Then
            lob = 0
        Else
            lob = Request("cbolob")
        End If
        If Request("cboclient") = "" Or Request("cboclient") = "--Select--" Then
            client = 0
        Else
            client = Request("cboclient")
        End If
        If cbodept.SelectedIndex = 0 Then
            Showmsg("Please select department")
        Else
            Dim cmdget As New SqlCommand("select distinct SavedFileName,CONVERT(VARCHAR, savedon, 101) as savedon,Path from IDMSSavedHTMLFile where  savedby='" & Session("userid") & "'", connection)
            Dim adpget As New SqlDataAdapter
            Dim dsget As New DataSet
            adpget.SelectCommand = cmdget
            connection.Open()
            adpget.Fill(dsget)
            connection.Close()
            dlshow.DataSource = dsget
            dlshow.DataBind()
        End If
        txtclient.Text = client
        txtlob.Text = lob
        hiddpt.Value = Request("cbodept")
        hidcl.Value = client
        hidlb.Value = lob
    End Sub

    Private Sub cmdno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False
        txtrepname.Text = ""
        hiddpt.Value = Request("cbodept")
        hidcl.Value = Request("cboclient")
        hidlb.Value = Request("cbolob")
    End Sub

    Private Sub dlshow_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlshow.ItemCommand
        If e.CommandName = "delete" Then
            txtrepname.Text = CType(e.Item.FindControl("lblname"), Label).Text
            pandelete.Visible = True
        End If
    End Sub

    Private Sub cmdyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        '''''''''''''''''''''Delete File'''''''''''''''''''''
        hiddpt.Value = Request("cbodept")
        hidcl.Value = Request("cboclient")
        hidlb.Value = Request("cbolob")
        Dim report As String = txtrepname.Text.ToString()
        Dim type As String
        connection.Open()
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select Type from IDMSSavedHTMLFile where SavedFilename='" + report + "'", connection)
        type = cmd.ExecuteScalar().ToString()
        Try
            If (type = "Simple") Then
                If File.Exists(Server.MapPath("/QlickReport/ReportDesigner/UserSpace/" & Session("Userid") & "/" & txtrepname.Text) & ".html") Then
                    File.Delete(Server.MapPath("/QlickReport/ReportDesigner/UserSpace/" & Session("Userid") & "/" & txtrepname.Text) & ".html")
                End If
                Dim cmddel As New SqlCommand("delete IDMSSavedHTMLFile where SavedFileName='" & txtrepname.Text & "'", connection)
                connection.Close()
                connection.Open()
                cmddel.ExecuteNonQuery()
                connection.Close()
                cmddel.Dispose()
                pandelete.Visible = False
                txtrepname.Text = ""
                Showmsg("File has been deleted successfully.")
                If cbodept.SelectedIndex <> 0 Then
                    Dim cmdget As New SqlCommand("select distinct SavedFileName,Path from IDMSSavedHTMLFile where  savedby='" & Session("userid") & "'", connection)
                    Dim adpget As New SqlDataAdapter
                    Dim dsget As New DataSet
                    adpget.SelectCommand = cmdget
                    connection.Open()
                    adpget.Fill(dsget)
                    connection.Close()
                    dlshow.DataSource = dsget
                    dlshow.DataBind()
                End If
            Else
                If File.Exists(Server.MapPath("/QlickReport/QueryBuilder/UsersSpace/" & Session("Userid") & "/" & txtrepname.Text) & ".html") Then
                    File.Delete(Server.MapPath("/QlickReport/QueryBuilder/UsersSpace/" & Session("Userid") & "/" & txtrepname.Text) & ".html")
                End If
                Dim cmddel As New SqlCommand("delete IDMSSavedHTMLFile where SavedFileName='" & txtrepname.Text & "'", connection)
                connection.Close()
                connection.Open()
                cmddel.ExecuteNonQuery()
                connection.Close()
                cmddel.Dispose()
                pandelete.Visible = False
                txtrepname.Text = ""
                Showmsg("File has been deleted successfully.")
                If cbodept.SelectedIndex <> 0 Then
                    Dim cmdget As New SqlCommand("select distinct SavedFileName,CONVERT(VARCHAR, savedon, 101) as savedon,Path from IDMSSavedHTMLFile where  savedby='" & Session("userid") & "'", connection)
                    Dim adpget As New SqlDataAdapter
                    Dim dsget As New DataSet
                    adpget.SelectCommand = cmdget
                    connection.Open()
                    adpget.Fill(dsget)
                    connection.Close()
                    dlshow.DataSource = dsget
                    dlshow.DataBind()
                End If
            End If
        Catch ex As Exception
            pandelete.Visible = False
            txtrepname.Text = ""
            Showmsg("The HTML File Does not Exist Physically.")
        End Try
    End Sub
End Class
