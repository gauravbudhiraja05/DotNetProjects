Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.HtmlTextWriter
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader
Partial Class QueryBuilder_datadisplay
    Inherits System.Web.UI.Page
    Dim con As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(con)
    Dim connection1 As New SqlConnection(con)
    Dim strsqlshow
    Dim strsqlwhere
    Dim strsqlgroup
    Dim strsql
    Dim doct As IO.Directory
    Dim stringWrite
    Dim htmlWrite
    Dim a As String

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            divdis.Visible = False
            'Session("Username") = "Shalu"
            If Trim(Request("hidtablename")) <> "" Then
                If Trim(Request("blank1")) <> "" Then
                    strsqlshow = Trim(Request("blank1"))
                Else
                    strsqlshow = "*"
                End If
                If Trim(Request("wheredata")) <> "" Then
                    strsqlwhere = " where " & Trim(Request("wheredata"))
                Else
                    strsqlwhere = ""
                End If

                If Trim(Request("gruopdata")) <> "" Then
                    strsqlgroup = " group by " & Trim(Request("gruopdata"))
                Else
                    strsqlgroup = ""
                End If


                strsql = "select " & strsqlshow & " from " & Trim(Request("hidtablename")) & " " & strsqlwhere & strsqlgroup
                txthidden.Text = strsql
                DataGrid()
                Me.lbl1.Visible = True
                Me.txttb.Visible = True
            End If
        End If
    End Sub
    Private Sub datagrid()
        Dim objcmd As New SqlCommand(strsql, connection)
        Dim objadp As New SqlDataAdapter
        Dim objds As New Data.DataSet
        connection.Open()
        objadp.SelectCommand = objcmd
        objadp.Fill(objds)
        connection.Close()
        dgdisplay.DataSource = objds
        dgdisplay.DataBind()
        a = dgdisplay.Items.Count
        If a = 0 Then
            divdis.Visible = False
        Else
            divdis.Visible = True
        End If
    End Sub

    Protected Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Try
            If txttb.Text = "" Then
                ShowConfirm("Fill File Name")
                Exit Sub
            Else
                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=" & Me.txttb.Text & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"
                stringWrite = New System.IO.StringWriter
                htmlWrite = New HtmlTextWriter(stringWrite)
                dgdisplay.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End()
            End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub
End Class
