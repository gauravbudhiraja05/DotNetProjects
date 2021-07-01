Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class OpenGraph
    Inherits System.Web.UI.Page
    Dim fun As New Functions
    Dim selectRep As New ReportDesigner
    Dim dsRep As New DataSet
    Dim dsProcess As New DataSet
    Dim daClient As New SqlDataAdapter
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Public dept As String = "1"
    Public client As String = "0"
    Public lob As String = "0"
    ''' <summary>
    '''  These Global variables are defined to store the data of a report.
    '''  This data is to pass to the parent window.
    ''' The data is passed through a client side function named as assignToparent().
    ''' </summary>
    ''' <remarks></remarks>
    Public repName As String = ""
    'Public repType As String = "Simple"
    Public hidgraphtype As String = ""
    Public hidgraphname As String = ""
    Public hidcolumnname As String = ""
    Public hidcolumnseries As String = ""
    Public hidtodate As String = ""
    Public hidfromdate As String = ""
    Public hidcommanformat As String = ""
    Public hidspecificproperties As String = ""
    Public hidtotalcolumn As String = ""
    Public hidcreatedon As String = ""
    Public hidsavedby As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            rbnReport.Checked = True
            ddlAnalysistable.Visible = False
            labelAnalysis.Visible = False
            Dim classobj As New Functions
            ddlDepartment.DataTextField = "departmentname"
            ddlDepartment.DataValueField = "autoid"
            ddlDepartment.DataSource = classobj.bind_Department()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "---select---")
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        If ddlDepartment.SelectedItem.Text = "---select---" Then
            ddlClient.Items.Clear()
            ddlClient.Items.Insert(0, "---select---")
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")

        Else

            Dim classobj As New Functions
            ddlClient.DataTextField = "clientname"
            ddlClient.DataValueField = "autoid"
            ddlClient.DataSource = classobj.bind_client(ddlDepartment.SelectedValue)
            ddlClient.DataBind()
            ddlClient.Items.Insert("0", "---select---")
            If rbnReport.Checked = True Then
                Dim clientana, lobana As String
                clientAna = ddlClient.SelectedItem.Text
                If clientAna = "" Or clientAna = "---select--" Then
                    clientAna = "0"
                Else
                    clientana = ddlClient.SelectedItem.Text
                End If
                lobana = ddlClient.SelectedItem.Text
                If lobana = "" Or lobana = "---select--" Then
                    lobana = "0"
                Else
                    lobana = ddlClient.SelectedItem.Text
                End If
                ddlReport.Items.Insert(0, "---select---")
                ddlReport.DataTextField = "queryname"
                ddlReport.DataValueField = "queryname"
                ddlReport.DataSource = classobj.bind_openrep(ddlDepartment.SelectedValue, clientana, lobana)
                ddlReport.DataBind()
                ddlReport.Items.Insert(0, "---select---")

            End If
            End If
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        If ddlClient.SelectedItem.Text = "---select---" Then
            ddlLob.Items.Clear()
            ddlLob.Items.Insert(0, "---select---")
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")
        Else
            Dim classobj As New Functions
            ddlLob.DataTextField = "lob"
            ddlLob.DataValueField = "autoid"
            ddlLob.DataSource = classobj.bind_lob(ddlDepartment.SelectedValue, ddlClient.SelectedValue)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "recordid"
            ddlReport.DataSource = classobj.bind_clientrep(ddlDepartment.SelectedValue, ddlClient.SelectedValue)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")
        End If
    End Sub

    Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
        If ddlLob.SelectedItem.Text = "---select---" Then
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")
        Else
            Dim classobj As New Functions
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "recordid"
            ddlReport.DataSource = classobj.bind_lobrep(ddlDepartment.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")
        End If
    End Sub

    Public Sub callParent()
        Dim str As String = ""
        str = "<script launguage=Javascript>"
        str = str + "assignToparent();"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "assignToparent", str)
    End Sub

    Protected Sub btnopen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnopen.Click
        If ddlDepartment.SelectedIndex = 0 Then
            'lblMsg.Text = "Please select department."
        Else
            dept = ddlDepartment.SelectedValue
            If ddlClient.SelectedIndex <> 0 Then
                client = ddlClient.SelectedValue
                If ddlLob.SelectedIndex <> 0 Then
                    lob = ddlLob.SelectedValue
                End If
            End If
            If ddlReport.SelectedIndex = 0 Then
                'lblMsg.Text = "Please select a report"
            Else
                Dim objcmd As New SqlCommand()
                Dim reader As SqlDataReader
                Dim sqlString As String = "Select idmsgraphmaster.Graphtype,idmsgraphmaster.Departmentid,idmsgraphmaster.Clientid,idmsgraphmaster.underlob,idmsgraphmaster.Queryname,idmsgraphmaster.columnname,idmsgraphmaster.columnseries,idmsgraphmaster.todate,idmsgraphmaster.fromdate,idmsgraphmaster.commanformat,idmsgraphmaster.commanformat1,idmsgraphmaster.commanformat2,idmsgraphmaster.legendformat,idmsgraphmaster.specificproperties,idmsgraphmaster.totalcolumn from idmsgraphmaster where  idmsgraphmaster.queryname='" & ddlReport.SelectedItem.ToString() & "' and idmsgraphmaster.departmentid='" & dept & "' and idmsgraphmaster.clientid='" & client & "' and idmsgraphmaster.underlob='" & lob & "'"
                objcmd = New SqlCommand(sqlString, conn)
                conn.Open()
                reader = objcmd.ExecuteReader()
                While reader.Read
                    hidgraphtype = reader("Graphtype").ToString()
                    hidcolumnname = reader("columnname").ToString()
                    hidcolumnseries = reader("columnseries").ToString()
                    hidcommanformat = reader("commanformat").ToString()
                    hidtodate = reader("todate").ToString()
                    hidfromdate = reader("fromdate").ToString()
                    hidtotalcolumn = reader("totalcolumn").ToString()
                End While
                repName = ddlReport.SelectedItem.ToString()
                callParent()
            End If
        End If
    End Sub

    Protected Sub rbnAnalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnAnalysis.CheckedChanged
        If rbnAnalysis.Checked = True And rbnReport.Checked = False Then
            Dim clientAna, lobana As String
            clientAna = ddlClient.SelectedItem.Text
            If clientAna = "" Or clientAna = "---select--" Then
                clientAna = "0"
            Else
                clientAna = ddlClient.SelectedItem.Text
            End If
            lobana = ddlClient.SelectedItem.Text
            If lobana = "" Or lobana = "---select--" Then
                lobana = "0"
            Else
                lobana = ddlClient.SelectedItem.Text
            End If
            Dim classobj As New Functions
            ddlReport.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "queryname"
            ddlReport.DataSource = classobj.bind_openAnarep(ddlDepartment.SelectedValue, clientAna, lobana)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")

        End If
    End Sub

    Protected Sub rbnReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnReport.CheckedChanged
        If rbnReport.Checked = True And rbnAnalysis.Checked = False Then
            Dim clientAna, lobana As String
            clientAna = ddlClient.SelectedItem.Text
            If clientAna = "" Or clientAna = "---select--" Then
                clientAna = "0"
            Else
                clientAna = ddlClient.SelectedItem.Text
            End If
            lobana = ddlClient.SelectedItem.Text
            If lobana = "" Or lobana = "---select--" Then
                lobana = "0"
            Else
                lobana = ddlClient.SelectedItem.Text
            End If
            Dim classobj As New Functions
            ddlReport.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "queryname"
            ddlReport.DataSource = classobj.bind_openrep(ddlDepartment.SelectedValue, clientAna, lobana)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")

        End If
    End Sub
End Class

'Partial Class OpenGraph
'    Inherits System.Web.UI.Page

'End Class
