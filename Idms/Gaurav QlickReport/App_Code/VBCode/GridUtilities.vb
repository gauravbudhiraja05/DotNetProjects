Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO

Public Class GridUtilities

    Public Shared Function OnColSortSelection(ByVal orgGrid As DataGrid, ByVal e As DataGridSortCommandEventArgs) As String
        Dim orgCol As DataGridColumn
        Dim strReturn As String = ""
        For Each orgCol In orgGrid.Columns
            If (e.SortExpression.ToLower().CompareTo(orgCol.SortExpression.ToLower()) = 0) Then
                orgCol.HeaderText = orgCol.HeaderText.Replace(" (ASC)", "").Replace(" (DESC)", "")
                
                If (orgCol.SortExpression.IndexOf(" ASC") > 1) Then
                    orgCol.SortExpression = e.SortExpression.Replace(" ASC", " DESC")
                    orgCol.HeaderText = orgCol.HeaderText + " (DESC)"
                    strReturn = orgCol.SortExpression

                ElseIf (orgCol.SortExpression.IndexOf(" DESC") > 1) Then
                    orgCol.SortExpression = e.SortExpression.Replace(" DESC", " ")
                    strReturn = orgCol.SortExpression
                Else
                    orgCol.SortExpression = e.SortExpression + " ASC"
                    orgCol.HeaderText = orgCol.HeaderText + " (ASC)"
                    strReturn = orgCol.SortExpression
                End If
            Else
                orgCol.SortExpression = orgCol.SortExpression.Replace(" ASC", " ").Replace(" DESC", " ")
                orgCol.HeaderText = orgCol.HeaderText.Replace(" (ASC)", "").Replace(" (DESC)", "")
            End If

        Next
        Return strReturn
    End Function

    Public Shared Function exporttopexcel(ByVal dw As DataView, ByVal sessionvalue As String, ByVal Response As HttpResponse)
        Dim fileName As String

        Dim datesec = Format(System.DateTime.Now, "MM/dd/yy/HH/mm/ss").Replace("/", "-")
        fileName = sessionvalue + "-" + datesec
        'Dim filepath As String = "../Reports/excel/" & Session("userid") & "/" & fileName & " .xls"
        'Dim filepath As String = "C:\Documents and Settings\All Users\Desktop\" + fileName + " .xls"

        'Response.AddHeader("Content-Disposition", "inline; filename=" + fileName & ".xls")
        Response.AddHeader("content-disposition", ("attachment; filename=" + fileName & ".xls"))
        Response.Charset = ""
        Response.Charset = ""
        Response.ContentType = "application/excel"
        'Dim sizvalue As Integer = dw.Tables(0).Rows.Count
        Dim grd As New DataGrid
        grd.DataSource = dw
        grd.DataBind()

        ''Delrec()

        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)

        grd.RenderControl(htw)


        'Dim fp As StreamWriter

        'fp = File.CreateText(fileName)

        ''divgrd.Visible = False
        'fp.WriteLine(sw.ToString())
        'fp.Close()
        Response.Write(sw.ToString())
        Response.End()
        Return fileName
    End Function


End Class
