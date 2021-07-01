Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Misc_DispQuery1
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim department As String = Request("dept")
        Dim Client As String = Request("client")
        Dim LOB As String = Request("lob")
        Dim table As String = Request("Table")
        Dim name As String = Request("Queryname")
        If Client = "" Then
            Client = 0
        End If
        If LOB = "" Then
            LOB = 0
        End If

        Dim cmdget As New SqlCommand("Select idmsreportmaster.ColName,idmsreportmaster.showtotal, convert(varchar,idmsreportmaster.Createdon,103) as CreateDate1 ,idmsreportmaster.TableName as TableName,idmsreportmaster.WhereData as WhereData,idmsreportmaster.GroupBy as GroupBy,idmsreportmaster.OrderBy as OrderBy,idmsreportmaster.HavingCondition as HavingCondition,idmsreportmaster.ColorCondition as ColorCondition,idmsreportmaster.ReportFormat as ReportFormat,idmsreportmaster.ReportType as ReportType,idmsreportmaster.ReportScope as ReportScope,idmsreportmaster.DateConTable as DateconTable,idmsreportmaster.columnFormat as columnFormat,idmsreportheadermaster.HeaderHeight as hheight,idmsreportheadermaster.HeaderFormat as hformat,idmsreportheadermaster.HeaderColumns as hcolumns,idmsreportheadermaster.ColumnFormat as hcolformat,idmsreportheadermaster.ColumnFormula as hcolformula,idmsreportheadermaster.ColorCondition as hcolcon,idmsreportfootermaster.FooterHeight as fheight,idmsreportfootermaster.FooterFormat as fformat,idmsreportfootermaster.FooterColumns as fcolumns,idmsreportfootermaster.ColumnFormat as fcolformat,idmsreportfootermaster.ColumnFormula as fcolformula,idmsreportfootermaster.ColorCondition as fcolcon from idmsreportmaster,idmsreportheadermaster,idmsreportfootermaster where  idmsreportmaster.queryname='" + name + "'  and idmsreportmaster.departmentid='" & department & "' and idmsreportmaster.clientid='" & Client & "' and idmsreportmaster.underlob='" & LOB & "' and idmsreportmaster.recordid=idmsreportheadermaster.repid and  idmsreportmaster.recordid=idmsreportfootermaster.repid", connection)
        Dim drget As SqlDataReader
        connection.Open()
        Dim tabdate
        drget = cmdget.ExecuteReader
        If drget.Read Then
            Try

                Dim bnm = Replace(drget("ColName").ToString(), vbNewLine, "")
                bnm = Replace(bnm, " As ", " AS ")
                bnm = Replace(bnm, " as ", " AS ")
                '''''' for old ill-format reports of phase1
                If bnm <> "" Then
                    If bnm.contains("$As$") = True Or bnm.contains("String.fromCharCode(34)") = True Or bnm.contains("$+$") = True Or bnm.contains("$as$") = True Then
                        bnm = Replace(bnm, "$as$", " AS ")
                        bnm = Replace(bnm, "$As$", " AS ")
                        bnm = Replace(bnm, "$+$", "")
                        bnm = Replace(bnm, "String.fromCharCode(34)", "'")
                        bnm = Replace(bnm, "$", " ")
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''

                hidDpos.Value = bnm
                Me.hidTables.Value = Replace(drget("TableName").ToString(), ",", "~")
                hidWhere.Value = drget("WhereData").ToString()
                hidGroupby.Value = drget("GroupBy").ToString()
                hidOrderby.Value = drget("OrderBy").ToString()
                hidHaving.Value = drget("HavingCondition").ToString()
                hidColorcondition.Value = drget("ColorCondition").ToString()
                hidDetailsformat.Value = drget("ReportFormat").ToString()
                hidReporttype.Value = drget("ReportType").ToString()
                hidReportscope.Value = drget("ReportScope").ToString()
                hidDatetable.Value = drget("DateConTable").ToString()
                hidDformat.Value = drget("columnFormat").ToString()

                hidHeight.Value = drget("hheight").ToString() + "," + drget("fheight").ToString()
                hidSubtotal.Value = drget("showtotal").ToString()
                hidHeaderformat.Value = drget("hformat").ToString()
                hidHpos.Value = drget("hcolumns").ToString()
                hidHformat.Value = drget("hcolformat").ToString()
                hidHformula.Value = drget("hcolformula").ToString()
                hidHcolorcon.Value = drget("hcolcon").ToString()

                hidFooterformat.Value = drget("fformat").ToString()
                hidFpos.Value = drget("fcolumns").ToString()
                hidFformat.Value = drget("fcolformat").ToString()
                hidFformula.Value = drget("fcolformula").ToString()
                hidFcolorcon.Value = drget("fcolcon").ToString()
               Catch ex As Exception
                Response.Write(ex.Message)
                Exit Sub
            End Try
        End If
        drget.Close()
        connection.Close()
        cmdget.Dispose()
        Dim str As String = ""
      If (Request("name") = "") Then
            hidReportname.Value = Request("Queryname")
        Else
            hidReportname.Value = Request("name")
        End If
        txtStartdate.Value = Request("date")
        txtEnddate.Value = Request("date1")
        If (txtFormula.Value.Contains("@Date1@") Or hidHaving.Value.Contains("@Date1@") Or hidWhere.Value.Contains("@Date1@")) And Trim(txtStartdate.Value) = "" Then
            Showmsg("Please Enter Start Date.")
            WinClose()
            Exit Sub
        End If
        If (txtFormula.Value.Contains("@Date2@") Or hidHaving.Value.Contains("@Date2@") Or hidWhere.Value.Contains("@Date2@")) And Trim(txtEnddate.Value) = "" Then
            Showmsg("Please Enter End Date.")
            WinClose()
            Exit Sub
        End If
        infor.Value = txtFormula.Value
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Showmsg", str.ToString)
    End Sub
    Public Sub WinClose()
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("window.close();")
        str.Append("</Script>")
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "WinClose", str.ToString)
    End Sub
End Class
