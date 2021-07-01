Imports System.Data.SqlClient
Imports System.Data
Imports System.io
Imports System.Configuration.Configurationsettings
Partial Class QueryBuilder_Drildown
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
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        AjaxPro.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        If Me.IsPostBack = False Then


            Dim boolAVG As Boolean
            Dim cmdgetdept As New SqlCommand("select * from IdmsDepartment", conn)
            Dim dsgetdept As New DataSet
            Dim adpgetdept As New SqlDataAdapter
            adpgetdept.SelectCommand = cmdgetdept
            conn.Open()
            adpgetdept.Fill(dsgetdept)
            conn.Close()
            ddlDept.DataSource = dsgetdept
            ddlDept.DataTextField = "DepartmentName"
            ddlDept.DataValueField = "autoid"
            ddlDept.DataBind()
            cmdgetdept.Dispose()
            ddlDept.Items.Insert(0, "--Select--")
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
                ErrMsg.InnerText = "Data must contain one field"
                Exit Sub
            End If
            If Request("column") = "" Then
                ErrMsg.InnerText = "Column must contain one field"
                Exit Sub
            End If
            If Request("crdata") = "" Then
                ErrMsg.InnerText = "Row must contain one field"
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

            heads = UBound(columnnamearr) + 1


            For i = 0 To UBound(rowdataarr)
                If FStr = "" Then
                    FStr = rowdataarr(i)
                Else
                    FStr = FStr & "," & rowdataarr(i)

                End If

            Next

            cn.Open()
            If Trim(Request("wheredata")) <> "" Then
                cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & " where " + Trim(Request("wheredata")) + " group by " & CaStr & " with rollup " '" order by " & CaStr
            Else
                cmd = "Select " & CaStr & ",count(*) from " & Trim(Request("hidtablename")) & "  group by " & CaStr & " with rollup " '" order by " & CaStr
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

            For i = 0 To ds.Tables(0).Rows.Count - 1

                For FCount = 0 To UBound(showdataarr)

                    'Calc = " avg(case when " 'Add formula here
                    Dim AggFunction As String = ""
                    If UCase(showdataarr(FCount)).contains("COUNT") Then
                        AggFunction = "Count"
                    ElseIf UCase(showdataarr(FCount)).contains("SUM") Then
                        AggFunction = "Sum"
                    ElseIf UCase(showdataarr(FCount)).contains("AVG") Then
                        AggFunction = "Avg"
                    ElseIf UCase(showdataarr(FCount)).contains("MAX") Then
                        AggFunction = "Max"
                    ElseIf UCase(showdataarr(FCount)).contains("MIN") Then
                        AggFunction = "Min"


                    End If
                    Calc = AggFunction & "(case when 1=1 "
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
                    Chead = Chead & "," & newch.Replace(")", "")
                    Dim ColumVal As Integer = 0
                    For ColumVal = 0 To UBound(AllColumnArr)
                        If UCase(showdataarr(FCount)).contains(UCase(AllColumnArr(ColumVal))) Then
                            Calc = Calc & " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                            'Calc = " then " & AllColumnArr(ColumVal).ToString & " else NULL end) [" & LCase(Chead) & "],"
                        End If

                    Next

                    cmd = cmd & Calc
                Next

            Next

            cmd = cmd.Substring(0, cmd.Length - 1)

            'cmd = "select " & FStr & "," & cmd & " from DevTrainingRawData group by " & FStr & " with rollup" '"  order by " & FStr
            If Trim(Request("wheredata")) <> "" Then
                cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  where " + Trim(Request("wheredata")) + " group by " & FStr & " with rollup" '"  order by " & FStr
            Else
                cmd = "select " & FStr & "," & cmd & " from " & Trim(Request("hidtablename")) & "  group by " & FStr & " with rollup" '"  order by " & FStr
            End If
            Try



                'TextBox1.Text = cmd

                da = New SqlDataAdapter(cmd, cn)

                da.Fill(ds, "Output")
            Catch ex As Exception
                ErrMsg.InnerText = ex.Message.ToString
            End Try
            Dim table As String = ""
            table = "<table border=1 style='border-color:Black' >"
            table = table + "<tr >"
            For j = 0 To ds.Tables("Output").Columns.Count - 1
                table = table + "<td align='center' style='background-color:blue; color:white' ><u>" + ds.Tables("Output").Columns(j).Caption + "<u></td>"
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

                    If l = UBound(rowdataarr) Then
                        table = table + "<td  align='center' style='background-color:Gray; color:white' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'><a href=#>" + item.Cells(l).Text + "</a></td>"
                    Else
                        If RrColor <= 1 Then
                            table = table + "<td  align='center' style='background-color:Gray; color:white' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'>" + item.Cells(l).Text + "</td>"

                        Else
                            table = table + "<td  align='center' colspan='" + item.Cells(l).ColumnSpan.ToString() + "'>" + item.Cells(l).Text + "</td>"

                        End If
                    End If

                Next
                table = table + "</tr>"
                RrColor = RrColor + 1
            Next
            table = table + "</table>"
            divGen.InnerHtml = ""
            divGen.InnerHtml = divGen.InnerHtml + table
        End If
    End Sub

End Class
