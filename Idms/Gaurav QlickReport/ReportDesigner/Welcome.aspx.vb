Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports System.Data
Partial Class ReportDesigner_Welcome
    Inherits System.Web.UI.Page
    Dim con As String = AppSettings("connectionString")
    Dim objsqlcon As New SqlConnection(con)
    Dim connection As New SqlConnection(con)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Dim StrTableName As String
    Dim dept
    Dim client
    Dim lob
    Public pnlHeight As Integer = 40
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("Queryname") = ""
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax)) ' Regiser the AjaxClass to be used to bind the tablefields
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))

        connection.Open()
        cmd = New SqlCommand("select Database1 from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("Database1")
            If (producttype = "Excel" Or producttype = "Excel,Oracle" Or producttype = "Excel,MS-SQL" Or producttype = "Excel,MS-SQL,Oracle") Then
                uploadtable.Visible = True
                uploadtemptab.Visible = True
            Else
                uploadtable.Visible = False
                uploadtemptab.Visible = False
            End If

        End If
        connection.Close()
        ''''''''''' Replicate MasterPage Label to store Page Title'''''''''
        Dim lblThispage As Label = Master.FindControl("lblPage")
        'lblThispage.Text = "Report Designer"
        ''''''''''''' Ends'''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''' Manipulating LeftPane''''''''''''''''''''
        If IsPostBack = False Then
            Session("ex") = ""

            '''''''''' Fill FontFamily'''''''''''''''''''''''''''''''''''
            Dim temp As System.Drawing.FontFamily
            For Each temp In FontFamily.Families
                Me.ddlFontfamily.Items.Add(temp.Name)
                Me.ddlFontfamily.Items(Me.ddlFontfamily.Items.Count - 1).Value = temp.Name
            Next
            Me.ddlFontfamily.SelectedValue = "Verdana"
            ''''''''''''Fontfamily Ends'''''''''''''''''''''''''''''''''''
            Dim cmdget As New SqlCommand
            cmdget = New SqlCommand("select distinct menuid from nlvl_menu_rights where userid='" + Session("userid").ToString + "' and MenuId='2'", connection)
            connection.Open()
            Dim dr As SqlDataReader
            dr = cmdget.ExecuteReader()
            If (dr.HasRows) Then
                Button1.Visible = True
            Else
                Button1.Visible = False
            End If
            dr.Close()
            connection.Close()
            cmdget.Dispose()
        End If
        ''''''''''' Ends'''''''''''''''''''''''''''''''''''

    End Sub
    ''' <summary>
    ''' This function is used to show an alert
    ''' </summary>
    ''' <param name="strPassed"></param>
    ''' <remarks></remarks>
    Sub ShowConfirm(ByVal strPassed As String)
        Dim Script As String = ""
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", Script)

    End Sub

    'Protected Sub btnGraphs_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraphs.ServerClick
    '    If Session("repName") = "" Then
    '        ShowConfirm("Please Save The Report First.")
    '        Exit Sub
    '    End If
    '    Response.Redirect("../Graphicalpresentation/graphdata.aspx?currentreport=" & Session("repName").ToString())
    '    Session("repName") = ""
    'End Sub

    'Protected Sub btnGraphs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraphs.Click
    '    If Session("repName") = "" Then
    '        ShowConfirm("Please Save The Report First.")
    '        Exit Sub
    '    End If
    '    Response.Redirect("../Graphicalpresentation/graphdata.aspx?currentreport=" & Session("repName").ToString())
    '    Session("repName") = ""
    'End Sub

    'Protected Sub btnSetalert_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetalert.ServerClick
    '    If (Session("Queryname").ToString() = "") Then
    '        Response.Redirect("../Graphicalpresentation/graphdata.aspx")
    '    Else
    '        ShowConfirm("Please Process The Report First")
    '    End If
    'End Sub
End Class
