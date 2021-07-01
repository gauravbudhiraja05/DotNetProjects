Imports System.Data.SqlClient
Imports System.Data
Imports System.io
Imports System.Configuration.Configurationsettings
Partial Class QueryBuilder_ResultOutput
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim heads As Integer
    Dim fun As New Functions
    Dim selectRep As New ReportDesigner


    Dim ConString As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(ConString)
    Dim reader As SqlDataReader
    Dim dept As String
    Dim client As String
    Dim lob As String
    Public Path1 As String
    Private Sub FormatRow(ByVal RN As Integer, ByVal CN As Integer)
        Dim CurVals(0 To RN) As String
        Dim NextVals(0 To RN) As String
        Dim C As Integer
        Dim idx As Integer
        Dim MatchFlag As Boolean
        Dim CNT As Integer
        Dim StartPos As Integer
        Dim ActualC As Integer

        C = CN
        ActualC = CN


        If RN = 0 Then
            RN = RN * 1
        End If

        While ActualC < DataGridView1.Rows(RN).Cells.Count '- 1
            'Assign New Values to Curvals and nextvals

            For idx = 0 To RN
                If idx = RN Then
                    CurVals(idx) = DataGridView1.Rows(idx).Cells(C).Text
                    NextVals(idx) = DataGridView1.Rows(idx).Cells(C).Text
                Else
                    CurVals(idx) = DataGridView1.Rows(idx).Cells(ActualC).Text
                    NextVals(idx) = DataGridView1.Rows(idx).Cells(ActualC).Text
                End If
            Next


            StartPos = C
            CNT = 0

            While C < DataGridView1.Rows(RN).Cells.Count
                MatchFlag = True

                ''Match Values
                For idx = 0 To RN
                    If UCase(CurVals(idx)) <> UCase(NextVals(idx)) Then
                        MatchFlag = False
                        Exit For
                    End If
                Next


                If MatchFlag = True Then
                    CNT = CNT + 1
                Else
                    Exit While
                End If

                C = C + 1
                ActualC = ActualC + 1

                If C < DataGridView1.Rows(RN).Cells.Count Then
                    For idx = 0 To RN
                        If idx = RN Then
                            NextVals(idx) = DataGridView1.Rows(idx).Cells(C).Text
                        Else
                            NextVals(idx) = DataGridView1.Rows(idx).Cells(ActualC).Text
                        End If
                    Next
                End If

            End While

            'If DataGridView1.Rows(RN).Cells(StartPos).Text = "" Then
            '    CNT = CNT * 1
            'End If


            DataGridView1.Rows(RN).Cells(StartPos).ColumnSpan = CNT
            DataGridView1.Rows(RN).Cells(StartPos).BorderStyle = BorderStyle.Solid
            DataGridView1.Rows(RN).Cells(StartPos).BorderWidth = 2
            DataGridView1.Rows(RN).Cells(StartPos).BorderColor = Drawing.Color.Black
            'ActualC = ActualC + CNT

            While CNT > 1
                DataGridView1.Rows(RN).Cells.RemoveAt(StartPos + 1)
                CNT = CNT - 1
            End While

            'ActualC = ActualC + CNT
            C = StartPos + 1

        End While


    End Sub

    Private Sub FormatGridNew()

        Dim LeftHeads As Integer
        Dim TopHeads As Integer

        Dim rowdataarr = Split(Trim(Request("crdata")), ",")
        Dim columnnamearr = Split(Trim(Request("column")), ",")
        LeftHeads = UBound(rowdataarr)
        TopHeads = UBound(columnnamearr)



        While TopHeads >= 0
            FormatRow(TopHeads, LeftHeads)
            TopHeads = TopHeads - 1
        End While

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cmd1 As SqlCommand = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", conn)
        conn.Open()
        Dim rdr = cmd1.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                dept_td.Visible = True
                ddlDept.Visible = True
                client_td.Visible = True
                ddlClient.Visible = True
                lob_td.Visible = True
                ddlLob.Visible = True
                cmdsave.Visible = True
                Dim boolAVG As Boolean
                Dim cmdgetdept As New SqlCommand("select * from IdmsDepartment where savedBy='" + Session("userid") + "'", conn)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                conn.Close()
                conn.Open()
                adpgetdept.Fill(dsgetdept)
                conn.Close()
                ddlDept.DataSource = dsgetdept
                ddlDept.DataTextField = "DepartmentName"
                ddlDept.DataValueField = "autoid"
                ddlDept.DataBind()
                cmdgetdept.Dispose()
                ddlDept.Items.Insert(0, "--Select--")
            Else
                cmdsave_singleuser.Visible = True
            End If
        End If
        cmd1.Dispose()
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        AjaxPro.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        
        conn.Close()
        If Me.IsPostBack = False Then
            'Dim boolAVG As Boolean
            'Dim cmdgetdept As New SqlCommand("select * from IdmsDepartment where savedBy='" + Session("userid") + "'", conn)
            'Dim dsgetdept As New DataSet
            'Dim adpgetdept As New SqlDataAdapter
            'adpgetdept.SelectCommand = cmdgetdept
            'conn.Open()
            'adpgetdept.Fill(dsgetdept)
            'conn.Close()
            'ddlDept.DataSource = dsgetdept
            'ddlDept.DataTextField = "DepartmentName"
            'ddlDept.DataValueField = "autoid"
            'ddlDept.DataBind()
            'cmdgetdept.Dispose()
            'ddlDept.Items.Insert(0, "--Select--")
            ErrMsg.InnerText = ""
            Dim strQryMod As String = "select name from syscolumns where object_name(id)='" & Request("hidtablename") + "'"
            Dim cmdMod As New SqlCommand(strQryMod, conn)
            conn.Open()
            reader = cmdMod.ExecuteReader
            Dim AllColumn As String = ""
            While reader.Read
                If AllColumn = "" Then
                    AllColumn = reader("name")
                Else
                    AllColumn = AllColumn + "," + reader("name")
                End If
            End While
            conn.Close()
            reader.Close()
            strQryMod = "select isnull(count(*),0) as data from " & Request("hidtablename") + ""
            cmdMod = New SqlCommand(strQryMod, conn)
            conn.Open()
            reader = cmdMod.ExecuteReader

            While reader.Read
                If reader("data").ToString = "0" Then
                    ErrMsg.InnerText = "No record found"
                    Exit Sub
                End If
            End While
            conn.Close()
            reader.Close()
            Dim AllColumnArr As Array = AllColumn.Split(",")

            Dim formulaarr As Array
            Dim formulaarr2 As Array
            Dim formulaarr3 As Array
            Dim showdata As String = ""

            Dim showdataarr As Array



            Dim columnnamearr As Array

            formulaarr = Split(Trim(Request("txtaFormula")), "$")
            formulaarr2 = Split(Trim(Request("txtaFormula")), "$")
            formulaarr3 = Split(Trim(Request("txtaFormula")), "$")
            showdataarr = Split(Trim(Request("Showdata")), ",")
            Dim showdata1 = Split(Trim(Request("Showdata1")), ",")
            If Request("Showdata") = "" Then
                showdataarr = Split(Trim(Request("Showdata1")), ",")
            End If
            If Request("Showdata1") <> "" Then
                showdataarr = Split(Trim(Request("Showdata1")), ",")
            End If
            If Request("Showdata") = "" And Request("Showdata1") = "" Then
                ErrMsg.InnerText = "Data pane must contain atleast one field"
                Exit Sub
            End If
            If Request("column") = "" Then
                ErrMsg.InnerText = "Column pane must contain atleast one field"
                Exit Sub
            End If
            If Request("crdata") = "" Then
                ErrMsg.InnerText = "Row pane must contain atleast one field"
                Exit Sub
            End If
            Dim rowdataarr = Split(Trim(Request("crdata")), ",")
            columnnamearr = Split(Trim(Request("column")), ",")
            Dim cn As New SqlConnection(ConString)

            'Dim constring As String
            Dim CaStr As String = ""
            Dim FStr As String = ""
            Dim Calc As String
            Dim cmd As String
            Dim Chead As String
            Dim i As Integer
            Dim FCount As Integer
            Dim Flag As Integer

            Dim Tempdt As New DataTable("Tempdt")



            For i = 0 To UBound(columnnamearr)
                If CaStr = "" Then
                    CaStr = columnnamearr(i)
                Else
                    CaStr = CaStr & "," & columnnamearr(i)

                End If
            Next

            Dim sssa As Integer = 0
            Dim quertblankcheck As String = ""
            Dim columnvalues As String() = CaStr.Split(",")
            quertblankcheck = " select 0"

            For sssa = 0 To UBound(columnvalues)
                'quertblankcheck += " +sum(case when " + Trim(columnvalues(sssa)) + " is null or " + Trim(columnvalues(sssa)) + "='' then 1 else 0 end ) "
                quertblankcheck += " +sum(case when " + Trim(columnvalues(sssa)) + " is null  then 1 else 0 end ) "
            Next


            heads = UBound(columnnamearr) + 1


            For i = 0 To UBound(rowdataarr)
                If FStr = "" Then
                    FStr = rowdataarr(i)
                Else
                    FStr = FStr & "," & rowdataarr(i)

                End If

            Next

            For sssa = 0 To UBound(rowdataarr)
                'quertblankcheck += " +sum(case when " + Trim(rowdataarr(sssa)) + " is null or " + Trim(rowdataarr(sssa)) + "='' then 1 else 0 end ) "
                quertblankcheck += " +sum(case when " + Trim(rowdataarr(sssa)) + " is null then 1 else 0 end ) "
            Next
            If Trim(Request("wheredata")) <> "" Then
                quertblankcheck += "from " + Trim(Request("hidtablename")) + " where " + Trim(Request("wheredata")) + " "
            Else
                quertblankcheck += "from " + Trim(Request("hidtablename"))
            End If

            Dim sqq As New SqlDataAdapter(quertblankcheck, cn)
            cn.Open()
            Dim dsss As New DataSet
            sqq.Fill(dsss)
            For sssa = 0 To dsss.Tables(0).Rows.Count - 1
                Dim convertdata As Integer = dsss.Tables(0).Rows(sssa)(0)
                If convertdata > 0 Then
                    ErrMsg.InnerText = "Fields under row and column span should not contain blank data"
                    Exit Sub
                End If
            Next
            cn.Close()

            cn.Open()
            If Trim(Request("wheredata")) <> "" Then
                If Request("chkcheck") = "yes" Then
                    cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & " where " + Trim(Request("wheredata")) + " group by " & CaStr & " with rollup" ' order by " & CaStr
                Else
                    cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & " where " + Trim(Request("wheredata")) + " group by " & CaStr + " order by " & CaStr
                End If

            Else
                If Request("chkcheck") = "yes" Then
                    cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & "  group by " & CaStr & " with rollup " ' order by " & CaStr
                Else
                    cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & "  group by " & CaStr + " order by " & CaStr
                End If

            End If
            'cmd = "Select " & CaStr & ",count(*) from Citi_Headcount group by " & CaStr & " with rollup " '" order by " & CaStr
            'cmd = "Select " & CaStr & ",count(*) from DevTrainingRawData group by " & CaStr & " with rollup " '" order by " & CaStr

            'cmd = "Select " & CaStr & ",count(*) from DevTrainingRawData group by " & CaStr '& " with rollup " '" order by " & CaStr

            Dim da As New SqlDataAdapter(cmd, cn)
            da.Fill(ds)
            cmd = ""
            Dim j As Integer
            Calc = ""

            'DataGridView2.DataSource = ds.Tables(0)
            Dim counvalue As Integer = 0
            For i = 0 To ds.Tables(0).Rows.Count - 1

                For FCount = 0 To UBound(showdataarr)

                    'Calc = " avg(case when " 'Add formula here
                    Dim AggFunction As String = ""
                    'Calc = " avg(case when " 'Add formula here

                    Dim lenst As Integer = 0
                    Dim aggdata As String = showdataarr(FCount)
                    lenst = aggdata.IndexOf("(")
                    AggFunction = aggdata.Substring(0, lenst)
                    'If UCase(showdataarr(FCount)).contains("COUNT") Then
                    '    AggFunction = "Count"
                    'ElseIf UCase(showdataarr(FCount)).contains("SUM") Then
                    '    AggFunction = "Sum"
                    'ElseIf UCase(showdataarr(FCount)).contains("AVG") Then
                    '    AggFunction = "Avg"
                    'ElseIf UCase(showdataarr(FCount)).contains("MAX") Then
                    '    AggFunction = "Max"
                    'ElseIf UCase(showdataarr(FCount)).contains("MIN") Then
                    '    AggFunction = "Min"


                    'End If
                    Calc = "convert(decimal(10,2), " & AggFunction & "(case when 1=1 "
                    Chead = ""
                    'Chead = ListBox4.Items.Item(FCount).ToString & " of " & ListBox3.Items.Item(FCount).ToString
                    Flag = 0
                    For j = 0 To ds.Tables(0).Columns.Count - 2
                        If Trim(ds.Tables(0).Rows(i)(j).ToString) <> "" Then
                            Calc = Calc & " and " & ds.Tables(0).Columns(j).Caption & "='" & ds.Tables(0).Rows(i)(j).ToString & "'"
                            If Flag = 0 Then

                                Chead = ds.Tables(0).Rows(i)(j).ToString
                                Flag = 1
                            Else
                                Chead = Chead & "," & ds.Tables(0).Rows(i)(j).ToString
                            End If
                        Else
                            If Flag = 0 Then
                                Chead = ""
                                Flag = 1
                            Else
                                Chead = Chead & ","
                            End If
                        End If
                    Next
                    '  Chead = Chead & "," & ListBox4.Items.Item(FCount).ToString & " of " & ListBox3.Items.Item(FCount).ToString
                    Dim str As String = UCase(showdataarr(FCount))
                    Dim newch As String = ""
                    newch = str.Replace("(", " of ")
                    If Chead = "" Then
                        Chead = "Aggregate"
                    End If
                    If Chead.StartsWith(",") Then
                        Dim maujan As String() = Chead.Split(",")
                        Chead = ""
                        Dim kkk As Integer = 0
                        For kkk = 0 To UBound(maujan)
                            If maujan(kkk) = "" Then
                                If Chead = "" Then
                                    Chead = "Aggregate"
                                Else
                                    Chead = Chead + "," + "Aggregate"
                                End If
                            Else
                                If Chead = "" Then
                                    Chead = maujan(kkk)
                                Else
                                    Chead = Chead + "," + maujan(kkk)
                                End If
                            End If
                        Next

                    End If
                    Chead = Chead & "," & newch.Replace(")", "")
                    Dim ColumVal As Integer = 0
                    'For ColumVal = 0 To UBound(AllColumnArr)
                    '    If UCase(showdataarr(FCount)).contains(UCase(AllColumnArr(ColumVal))) Then
                    '        Calc = Calc & " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                    '        'Calc = " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                    '    End If

                    'Next

                    'cmd = cmd & Calc
                    'For ColumVal = 0 To UBound(AllColumnArr)
                    'If UCase(showdataarr(FCount)).contains(UCase(AllColumnArr(ColumVal))) Then
                    Dim countas As Integer = UBound(showdataarr)
                    Dim staa As Integer = 0
                    Dim manipulate As String = ""
                    If counvalue > countas Then
                        counvalue = 0
                        manipulate = showdataarr(counvalue).ToString()
                        Dim brst As Integer = manipulate.IndexOf("(")
                        Dim brend As Integer = manipulate.Length
                        Dim isfg As Integer = brend - brst


                        manipulate = manipulate.Substring(brst + 1, isfg - 2)
                        Calc = Calc & " then " & manipulate & " else NULL end)) [" & LCase(Chead) & "],"
                    Else
                        manipulate = showdataarr(counvalue).ToString()
                        Dim brst As Integer = manipulate.IndexOf("(")
                        Dim brend As Integer = manipulate.Length
                        Dim isfg As Integer = brend - brst
                        manipulate = manipulate.Substring(brst + 1, isfg - 2)
                        Calc = Calc & " then " & manipulate & " else NULL end)) [" & LCase(Chead) & "],"
                    End If
                    counvalue = counvalue + 1
                    'Calc = Calc & " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                    'Calc = " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                    'End If

                    'Next

                    cmd = cmd & Calc
                Next

            Next

            cmd = cmd.Substring(0, cmd.Length - 1)

            'cmd = "select " & FStr & "," & cmd & " from DevTrainingRawData group by " & FStr & " with rollup" '"  order by " & FStr
            If Trim(Request("wheredata")) <> "" Then
                If Request("chkcheck") = "yes" Then
                    cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  where " + Trim(Request("wheredata")) + " group by " & FStr & " with rollup" '  order by " & FStr
                Else
                    cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  where " + Trim(Request("wheredata")) + " group by " & FStr + "  order by " & FStr
                End If

            Else
                If Request("chkcheck") = "yes" Then
                    cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  group by " & FStr & " with rollup " ' order by " & FStr
                Else
                    cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  group by " & FStr + "  order by " & FStr
                End If

            End If
            Try



                'TextBox1.Text = cmd

                da = New SqlDataAdapter(cmd, cn)

                da.Fill(ds, "Output")
                'changes==================
                Dim gridcolcount As Integer = ds.Tables("Output").Columns.Count - 1
                Dim start As Integer = 0
                Dim columresultant As String = ""

                For start = UBound(rowdataarr) + 1 To gridcolcount
                    If columresultant = "" Then
                        columresultant = ds.Tables("Output").Columns.Item(start).ToString
                    Else
                        columresultant = columresultant + "@" + ds.Tables("Output").Columns.Item(start).ToString
                    End If
                Next
                Dim allresultcol As String() = columresultant.Split("@")
                Dim prevalsu As String = ""
                Dim prevalsu1 As String = ""
                Dim designint As Integer = 0
                Dim l As String = ""
                Dim pw As Integer = 0
                Dim t As Integer = 0
                Dim valuelies As String = ""
                Dim tablemade As String = "<table border='2' bordercolor=black>"
                Dim tostorefordata As String = ""
                tostrorecolumn.Value = ""
                Dim hova As Integer = 0
                Dim boundvalue As String() = FStr.Split(",")
                Dim spaninsubtotal As String = ""
                For designint = 0 To UBound(columnnamearr) + 1
                    tablemade = tablemade + "<tr>"
                    For hova = 0 To UBound(rowdataarr)

                        If designint = UBound(columnnamearr) + 1 Then

                            tablemade = tablemade + "<td align='center' style='background-color:#04699B; color:Gold; font-size:11px; font-weight:bold; font-family:Verdana'>" + boundvalue(hova).ToString + "</td>"
                        Else


                        End If
                    Next

                    If designint < UBound(columnnamearr) + 1 Then
                        Dim fffggg As String = (UBound(rowdataarr) + 1).ToString
                        tablemade = tablemade + "<td align='right' style='background-color:#04699B; color:Gold; font-size:11px; font-weight:bold; font-family:Verdana' colspan='" + fffggg + "'>" + columnnamearr(designint).ToString + "</td>"

                    End If
                    For j = 0 To UBound(allresultcol)
                        Dim valvalval = allresultcol(j).Split(",")
                        Dim ccv As Integer
                        prevalsu1 = ""
                        For ccv = 0 To designint
                            If prevalsu1 = "" Then
                                prevalsu1 = valvalval(ccv)
                            Else
                                prevalsu1 = prevalsu1 + "," + valvalval(ccv)
                            End If

                        Next
                        If prevalsu <> prevalsu1 Or prevalsu = "" Then


                            Dim k = allresultcol(j).Split(",")
                            Dim f As Integer = designint
                            l = ""
                            For pw = 0 To f

                                If l = "" Then
                                    l = k(pw)
                                Else
                                    l = l + "," + k(pw)
                                End If


                            Next
                            Dim gh As Integer = 0

                            'For t = 0 To UBound(allresultcol)
                            '    If (allresultcol(t) + ",").Contains((l + ",")) Then
                            '        gh = gh + 1

                            '    End If
                            'Next
                            'changes made by smitha
                            For t = 0 To UBound(allresultcol)
                                'Changes on 01/08/2010 By Lalit Chauhan
                                Dim splallresultcol As String() = allresultcol(t).Split(",")
                                If (splallresultcol(0).ToString = l) Then
                                    'Changes End
                                    'If (allresultcol(t)).Contains((l + ",")) Then
                                    gh = gh + 1

                                End If
                            Next
                            'changes made by smitha
                            Dim valsplit As String() = l.Split(",")
                            If valuelies = "" Then
                                prevalsu = l

                                'If Request("chkcheck") = "yes" Then
                                '    If designint = 0 Then
                                '        If spaninsubtotal = "" Then
                                '            spaninsubtotal = gh
                                '        Else
                                '            spaninsubtotal = spaninsubtotal + "," + gh
                                '        End If
                                '        gh = gh + UBound(showdataarr)

                                '        tablemade = tablemade + "<td colspan='" + gh.ToString() + "' align='center'>" + valsplit(UBound(valsplit)) + "</td>"
                                '    ElseIf designint = UBound(rowdataarr) + 1 Then
                                '        tablemade = tablemade + "<td colspan='" + gh.ToString() + "' align='center'>" + valsplit(UBound(valsplit)) + "</td>"
                                '        Dim startfrtf As Integer = 0
                                '        For startfrtf = 0 To UBound(showdataarr)
                                '            tablemade = tablemade + "<td align='center'>Aggregate Sum</td>"

                                '        Next
                                '    End If
                                'Else

                                tablemade = tablemade + "<td colspan='" + gh.ToString() + "' align='center' style='background-color:#04699B; color:Gold; font-size:11px; font-weight:bold; font-family:Verdana'>" + valsplit(UBound(valsplit)) + "</td>"


                                'End If

                                valuelies = valsplit(UBound(valsplit)) + "$" + gh.ToString()
                            Else
                                prevalsu = l
                                'If Request("chkcheck") = "yes" Then
                                '    If spaninsubtotal = "" Then
                                '        spaninsubtotal = gh
                                '    Else
                                '        spaninsubtotal = spaninsubtotal + "," + gh
                                '    End If
                                '    gh = gh + UBound(showdataarr)
                                '    tablemade = tablemade + "<td colspan='" + gh.ToString() + "' align='center'>" + valsplit(UBound(valsplit)) + "</td>"
                                'Else
                                tablemade = tablemade + "<td colspan='" + gh.ToString() + "' align='center' style='background-color:#04699B; color:Gold; font-size:11px; font-weight:bold; font-family:Verdana'>" + valsplit(UBound(valsplit)) + "</td>"
                                'End If

                                valuelies = valuelies + "," + valsplit(UBound(valsplit)) + "$" + gh.ToString()

                            End If
                            If designint = UBound(columnnamearr) + 1 Then
                                If tostrorecolumn.Value = "" Then
                                    tostrorecolumn.Value = l
                                Else
                                    tostrorecolumn.Value = tostrorecolumn.Value + "$" + l
                                End If
                            End If
                        End If
                    Next
                    tablemade = tablemade + "</tr>"
                Next
                Dim row As Integer = 0
                Dim rows As Integer = ds.Tables("Output").Rows.Count - 1
                Dim stts As Integer = 0
                tostrorecolumn.Value = FStr.Replace(",", "$") + "$" + tostrorecolumn.Value
                Dim sptcolumn As String() = tostrorecolumn.Value.Split("$")

                For stts = 0 To rows
                    tablemade = tablemade + "<tr>"

                    For start = 0 To UBound(sptcolumn)


                        tablemade = tablemade + "<td align='center' style='color:black; font-size:11px; font-family:Verdana'>" + ds.Tables("Output").Rows(stts)(sptcolumn(start)).ToString() + "</td>"


                    Next
                    tablemade = tablemade + "</tr>"
                Next
                tablemade = tablemade + "<table>"
                'ranjit'''''''''''''
                divGen.InnerHtml = divGen.InnerHtml + tablemade
                Exit Sub
                '''ranjit'''''''''''''''
                'changes==========================
                Dim ooooooooo As String = ""
            Catch ex As Exception
                ErrMsg.InnerText = ex.Message.ToString
                Exit Sub
            End Try
            Dim table As String = ""
            table = "<table border=1 bordercolor=black cellspacing=0 cellpadding=0 >"
            table = table + "<tr >"
            For j = 0 To ds.Tables("Output").Columns.Count - 1
                table = table + "<td align='center' style='background-color:#04699B; color:Gold; font-size:11px; font-weight:bold; font-family:Verdana' ><u>" + ds.Tables("Output").Columns(j).Caption + "<u></td>"
                Tempdt.Columns.Add(ds.Tables("Output").Columns(j).Caption, System.Type.GetType("System.String"))
                table = table + "</td>"
            Next
            table = table + "</tr>"
            Tempdt.TableName = "Tempdt"
            ds.Tables.Add(Tempdt)
            ds.EnforceConstraints = False
            DataGridView1.DataSource = ds.Tables("Output")

            cn.Close()

            Dim MSG As String
            Dim LP As Integer
            Dim HEAD As Integer = heads
            Dim AR(HEAD) As String
            Dim Tstr As String
            Dim P As Integer
            Dim dr(HEAD) As DataRow
            For i = 0 To dr.Length - 1
                dr(i) = ds.Tables("TempDt").NewRow
            Next
            For j = UBound(rowdataarr) To ds.Tables("TempDt").Columns.Count - 1
                'Creat array of headings
                MSG = ds.Tables("Output").Columns(j).Caption
                P = 0
                LP = 0
                i = 0
                Do While True
                    P = MSG.IndexOf(",", LP)
                    If P <> -1 Then
                        Tstr = MSG.Substring(LP, P - LP)
                        dr(i)(j) = Tstr
                    Else
                        Tstr = MSG.Substring(LP)
                        dr(i)(j) = Tstr
                        Exit Do
                    End If
                    i = i + 1
                    LP = P + 1
                Loop
                'Created
            Next

            For i = 0 To HEAD - 1
                ds.Tables("TempDt").Rows.InsertAt(dr(i), i)
            Next

            DataGridView1.DataSource = ds.Tables("Tempdt")

            For Each row As DataRow In ds.Tables("Output").Rows
                ds.Tables("Tempdt").NewRow()
                ds.Tables("Tempdt").ImportRow(row)
            Next


            '---------------- Formatting Grid'''''''''''''''''''''
            DataGridView1.DataBind()
            FormatGridNew()

            Dim item As GridViewRow

            Dim colm As DataColumn
            Dim RrColor As Integer = 0
            For Each item In DataGridView1.Rows
                table = table + "<tr>"
                Dim rows As Integer = item.Cells.Count
                Dim l As Integer = 0

                For l = 0 To rows - 1

                    'If l = UBound(rowdataarr) Then
                    '    table = table + "<td  align='center' style='background-color:Gray; color:white' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'><a href=#>" + item.Cells(l).Text + "</a></td>"
                    'Else
                    If RrColor <= 1 Then
                        table = table + "<td  align='center' style='background-color:#26B6FB; color:white; font-size:11px; font-family:Verdana' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'>" + item.Cells(l).Text + "</td>"

                    Else
                        table = table + "<td  align='center' style='font-size:11px; font-family:Verdana' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'>" + item.Cells(l).Text + "</td>"

                    End If
                    'End If



                Next
                table = table + "</tr>"
                RrColor = RrColor + 1
            Next
            table = table + "</table>"
            divGen.InnerHtml = ""
            divGen.InnerHtml = divGen.InnerHtml + table
        End If
    End Sub

    Protected Sub Button1_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.ServerClick
        Dim showrep
        showrep = Me.txtname.Text
        Dim htmlheaddiv As String = ""
        htmlheaddiv = "<table border=0 cellspacing=0 cellspacing=0 class=grid width=100%>"
        htmlheaddiv = htmlheaddiv & "<tr>"
        htmlheaddiv = htmlheaddiv & "<td align=center ><b>" & showrep & "<b></td>"
        htmlheaddiv = htmlheaddiv & "</tr>"
        htmlheaddiv = htmlheaddiv & "</table>"
        Me.txtdivshow.Value = htmlheaddiv.ToString
        If divGen.InnerHtml <> "" Then
            Dim str As String
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("userid"))) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("userid") & "/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "/IDMS/Menu/UsersSpace/" & Session("userid") & "/" & txtname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))

            fp.WriteLine(txtdivshow.Value)

            fp.WriteLine(divGen.InnerHtml)
            fp.Close()
            Response.Write(htmlheadtxt.Text)
            Dim client
            If Request("cboclient") = "" Or Request("cboclient") = "--Select--" Then
                client = 0
            Else
                client = Request("cboclient")
            End If
            Dim lob
            If Request("cbolob") = "" Or Request("cbolob") = "--Select--" Then
                lob = 0
            Else
                lob = Request("cbolob")
            End If
            '''SavedFilename()
            '''Path()
            '''DepartmentId()
            '''ClientID()
            '''LOBId()
            '''SavedBy()
            '''SavedOn()
            Dim cmdins As New SqlCommand("insert into IDMSSavedHTMLFile values('" & txtname.Text & "','" & Path & "','" & Request("cbodept") & "','" & client & "','" & lob & "','" & Session("userid") & "','" & FormatDateTime(Now, DateFormat.ShortDate) & "','Query','" & Me.queryname.Value & "')", conn)
            conn.Open()
            cmdins.ExecuteNonQuery()
            conn.Close()
            ShowConfirm("File has been created successfully.")

            txtname.Text = ""
        Else
            ShowConfirm("File has not been created. Because it does not have any data.")
        End If
    End Sub
    Public Sub ShowConfirm(ByVal strmsg As String)
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub

    Protected Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Dim showrep

        If ddpt.Value = "" Then
            ShowConfirm("Select department")
            Exit Sub

        End If
        showrep = Me.txtname.Text
        Dim htmlheaddiv As String = ""
        htmlheaddiv = "<table border=0 cellspacing=0 cellspacing=0 class=grid width=100%>"
        htmlheaddiv = htmlheaddiv & "<tr>"
        htmlheaddiv = htmlheaddiv & "<td align=center ><b>" & showrep & "<b></td>"
        htmlheaddiv = htmlheaddiv & "</tr>"
        htmlheaddiv = htmlheaddiv & "</table>"
        Me.txtdivshow.Value = htmlheaddiv.ToString
        If divGen.InnerHtml <> "" Then
            Dim str As String
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("userid"))) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("userid") & "/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "/QlickReport/QueryBuilder/UsersSpace/" & Session("userid") & "/" & txtname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))

            fp.WriteLine(txtdivshow.Value)

            fp.WriteLine(divGen.InnerHtml)
            fp.Close()
            Response.Write(htmlheadtxt.Text)
            Dim client
            If dclt.Value = "" Or dclt.Value = "--Select--" Then
                client = 0
            Else
                client = dclt.Value
            End If
            Dim lob
            If dlob.Value = "" Or dlob.Value = "--Select--" Then
                lob = 0
            Else
                lob = dlob.Value
            End If

            '''SavedFilename()
            '''Path()
            '''DepartmentId()
            '''ClientID()
            '''LOBId()
            '''SavedBy()
            '''SavedOn()
            Dim cmdins As New SqlCommand("insert into IDMSSavedHTMLFile values('" & txtname.Text & "','" & Path & "','" & Session("userid") & "','" & FormatDateTime(Now, DateFormat.ShortDate) & "','Query','" & Me.queryname.Value & "')", conn)
            conn.Open()
            cmdins.ExecuteNonQuery()
            conn.Close()
            ShowConfirm("File has been created successfully.")

            txtname.Text = ""
        Else
            ShowConfirm("File has not been created. Because it does not have any data.")
        End If
    End Sub

    Protected Sub imgexl_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgexl.Click
        'If (hidReportname.Value = "") Then
        '    lblMsg.Text = "Report maynot be processed/saved. Please save the report first"
        'Else
        'savedby = Session("userid")
        'savedon = System.DateTime.Today.Date
        'hidReportname.Value = Trim(Request("hidReportname"))
        If Not Directory.Exists(Server.MapPath("UserSpace/" & Session("userid"))) Then
            '<----------------------Creating Directory for partcular user--------------------------------->
            Directory.CreateDirectory(Server.MapPath("UserSpace/" & Session("userid") & "/" & Session("userid")))
            '<----------------------End of Creating Directory for partcular user------------------------>
        End If
        '<------------------------End of Creating A main Directory--------------------------------------->
        ' finally export to XLS format

        Path1 = "UserSpace/" & Session("userid") & "/" & Session.SessionID & ".xls"
        Dim fp As StreamWriter
        fp = File.CreateText(Server.MapPath(Path1))
        fp.WriteLine(divGen.InnerHtml)
        fp.Close()
        Try
            'Dim sr As String = repDesign.trackXLS(savedby, savedon, hidReportscope.Value, hidReportname.Value, hidDepartment.Value, hidClient.Value, hidLob.Value)
            ' Save the XLS at desired location
            Dim str As String = ""
            str = "<script laungauge=Javascript>"
            str = str + "xls();"
            str = str + "</script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "XLS", str)
        Catch ex As Exception
            ShowConfirm("Error occured.Please try again")
        End Try
        '<--------------------Deleting all the excel files---------------------------------->
        'Dim DelPath As String
        'DelPath = "UserSpace/" & Session("userid")
        'Dim path1 = Server.MapPath(DelPath)
        'Dim dir As New DirectoryInfo(path1)
        'Dim file1() As FileInfo
        'file1 = dir.GetFiles()
        'If file1.Length > 0 Then
        '    For i = 0 To file1.Length - 1
        '        file1(i).Delete()
        '    Next
        'End If
        '<-----------------------End Code-------------------------------------------------------->
        'End If
    End Sub

    Protected Sub cmdsave_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave_singleuser.Click
        Dim showrep

        If txtname.Text = "" Then
            ShowConfirm("Please Enter Report Name")
            Exit Sub

        End If
        showrep = Me.txtname.Text
        Dim htmlheaddiv As String = ""
        htmlheaddiv = "<table border=0 cellspacing=0 cellspacing=0 class=grid width=100%>"
        htmlheaddiv = htmlheaddiv & "<tr>"
        htmlheaddiv = htmlheaddiv & "<td align=center ><b>" & showrep & "<b></td>"
        htmlheaddiv = htmlheaddiv & "</tr>"
        htmlheaddiv = htmlheaddiv & "</table>"
        Me.txtdivshow.Value = htmlheaddiv.ToString
        If divGen.InnerHtml <> "" Then
            Dim str As String
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("userid"))) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("userid") & "/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "/QlickReport/QueryBuilder/UsersSpace/" & Session("userid") & "/" & txtname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))

            fp.WriteLine(txtdivshow.Value)

            fp.WriteLine(divGen.InnerHtml)
            fp.Close()
            Response.Write(htmlheadtxt.Text)
            'Dim client
            'If dclt.Value = "" Or dclt.Value = "--Select--" Then
            '   client = 0
            'Else
            '   client = dclt.Value
            'End If
            'Dim lob
            'If dlob.Value = "" Or dlob.Value = "--Select--" Then
            'lob = 0
            'Else
            '   lob = dlob.Value
            'End If

            '''SavedFilename()
            '''Path()
            '''DepartmentId()
            '''ClientID()
            '''LOBId()
            '''SavedBy()
            '''SavedOn()
            Dim cmdins As New SqlCommand("insert into IDMSSavedHTMLFile values('" & txtname.Text & "','" & Path & ",'" & Session("userid") & "','" & FormatDateTime(Now, DateFormat.ShortDate) & "','Query','" & Me.queryname.Value & "')", conn)
            conn.Open()
            cmdins.ExecuteNonQuery()
            conn.Close()
            ShowConfirm("File has been created successfully.")

            txtname.Text = ""
        Else
            ShowConfirm("File has not been created. Because it does not have any data.")
        End If
    End Sub
End Class
