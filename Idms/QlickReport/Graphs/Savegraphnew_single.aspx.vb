Imports System
Imports System.Data
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Collections
Imports Dundas.Charting.Utilities
Imports Dundas.Charting.Utilities.SixSigma
Imports System.ComponentModel
Imports System.Web
Partial Class Graphs_Savegraphnew_single
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim cmd As SqlCommand
    Dim con As New SqlConnection(conn)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim colname, formula, groupby, orderby, seriesName, sortedseries As String
    Dim fontname As FontFamily
    Public graph As Chart
    Public stt As String
    Dim p
    Dim j As Integer = 0
    Dim bool As Boolean
    Dim Duplicate As String
    Dim counter As Double = 0.0
    Dim count As Integer
    Dim classobj As New Functions
    Dim graphobj As New GraphicalPresentation
    Dim showlabel As Label
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("  window.close();" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtReportname.Text = Session("queryname")
        txtGraphtype.Text = Session("gptype")
    End Sub

    Protected Sub btnSavenew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavenew.Click
        If txtGraphname.Text = "" Or txtReportname.Text = "" And txtGraphtype.Text = "" Then
            savemesssage.Text = "Please Select Reportname,GraphType"
            Exit Sub
        End If
        Dim gpname
        Dim CreatedOn As String = ""
        CreatedOn = System.DateTime.Today.ToString()
        Dim strgraphname, exitgraphname As String
        strgraphname = txtGraphname.Text
        Session("Savegp") = "Saved Graph Name: " + txtGraphname.Text
        Dim Reportname As String
        Reportname = Session("queryname")
        'cmd = New SqlCommand("select  graphname from idmsgraphmaster where queryname='" + Reportname + "' ", con)
        'con.Open()
        'readquery = cmd.ExecuteReader()

        'While readquery.Read()
        '    exitgraphname = readquery("graphname")
        '    If strgraphname = readquery("graphname") Then
        '        gpname = False
        '        txtReportname.Text = Session("repname")
        '        txtGraphtype.Text = Session("gtype")
        '        savemesssage.ForeColor = Color.Red
        '        savemesssage.Text = "This Graph Name Is Already Exist"
        '        Exit Sub
        '    Else
        '        gpname = True
        '    End If

        'End While
        Dim gtype, queryname, columnname, repcols, selectedcols As String
        Dim deptid, clientid, lobid As Integer
        deptid = 60
        clientid = 0
        lobid = 0
        gtype = Session("gptype")
        queryname = Session("queryname")
        repcols = Session("repcols1")
        selectedcols = Session("selectcols1")
        con.Close()

        If gpname = True Then
            graphobj.insertGraph(gtype, deptid, clientid, lobid, queryname, txtGraphname.Text, selectedcols, "Column", "", "", "", "", "", "", "Linetype", CreatedOn, Session("userid"), repcols, "Normal")
            aspnet_msgbox("Graph Saved Sucessfully")
        ElseIf exitgraphname = "" Then
            graphobj.insertGraph(gtype, deptid, clientid, lobid, queryname, txtGraphname.Text, selectedcols, "Column", "", "", "", "", "", "", "Linetype", CreatedOn, Session("userid"), repcols, "Normal")
            aspnet_msgbox("Graph Saved Sucessfully")
        End If

        con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        Dim cmm As New SqlCommand("insert into Graph_utype select MAX(newrecordid)," + Session("usertype") + " from loggraphdesigner where GraphName='" + strgraphname + "' and Action='Save' and reportname='" + txtReportname.Text + "'", con)
        con.Open()
        cmm.ExecuteNonQuery()
        con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha
        'Dim str As String
        'Str = "<script laungauge=Javascript>"
        'str = str + "loadGraphname();"
        'str = str + "</script>"
        'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "XLS", Str)
    End Sub
End Class
