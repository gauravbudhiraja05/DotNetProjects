Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO
Partial Class DataAnalysis_FilterPercentage
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(conn)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim dataadapter As New SqlDataAdapter
    Dim cmd As New SqlCommand
    Dim commontablename As String = ""
    ''' <summary>
    ''' fill incoming columns in listbox and modify the incoming report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        commontablename = "filtertableforgopaldemand" & Session("userid")


        cmd = New SqlCommand("select * from  " + Session("table") + "", con)
        con.Open()



        Dim datasetaa As New DataSet

        dataadapter.SelectCommand = cmd
        dataadapter.Fill(datasetaa)

        con.Close()
        Dim culmnaaa As DataColumn
        Dim strclocheck As String
        For Each culmnaaa In datasetaa.Tables(0).Columns


            strclocheck = culmnaaa.ColumnName()
            If strclocheck.StartsWith("Accu_Sum") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            ElseIf strclocheck.StartsWith("Column_Percentage_") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            ElseIf strclocheck.StartsWith("Column_Sum_Percentage") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Column_Sum") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Row_Percentage_") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Row_Sum_") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.Contains("Filter") Then
                cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                'ElseIf strclocheck.Contains("RecordId") Then
                '    cmd = New SqlCommand("alter table " + Session("table") + " drop column " + strclocheck + " ", con)
                '    con.Open()
                '    cmd.ExecuteNonQuery()
                '    con.Close()
            End If




        Next
        If Page.IsPostBack = False Then
            Dim lblThispage As Label = Master.FindControl("lblPage")
            lblThispage.Text = "Data Analysis"
            relax.InnerHtml = ""
            listcolumns.DataSource = Session("filtercolumns")
            listcolumns.DataBind()
            Dim arr = Session("filtercolumns")
            Dim column As String = ""
            Dim i, j As Integer
            i = UBound(arr)
            For j = 0 To i
                If column = "" Then
                    column = arr(j)

                Else
                    column = column & "," & arr(j)
                End If

            Next

            'GroupBy.Value = "Group BY" & " " & column & ",RecordId"
            GroupBy.Value = "Group BY" & " " & column
            allcolumn.Value = column
            ddlcolfrfun.DataSource = Session("filtercolumns")
            ddlcolfrfun.DataBind()
            ddlcolfrfun.Items.Insert(0, "---select---")
            allcolumns.DataSource = Session("filtercolumns")
            allcolumns.DataBind()
            allcolumns.Items.Insert(0, "---select---")
        End If
    End Sub
    ''' <summary>
    ''' to show message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' fix it no use(checking the formula is correct or nato according to selected datatype
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnfun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfun.Click

        If ddlfunctions.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("Function Is Not Selected")
            Exit Sub
        End If
        If ddlcolfrfun.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("Column Is Not Selected")
            Exit Sub
        End If

        Dim Table As String = Session("table")
        Dim dr As SqlDataReader
        Try
            cmd = New SqlCommand("select " + ddlfunctions.SelectedItem.Text + " (" + ddlcolfrfun.SelectedItem.Text + ") as functions from " + Table + "", con)
            con.Open()
            dr = cmd.ExecuteReader
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)

            Exit Sub
        End Try
        If dr.Read Then
            finalformula.Text = finalformula.Text & ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
            If hidfun.Value = "" Then
                hidfun.Value = ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
                hidfun.Value = hidfun.Value & "," & dr("functions").ToString

            Else
                hidfun.Value = hidfun.Value & "," & ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
                hidfun.Value = hidfun.Value & "," & dr("functions").ToString
            End If
        End If
        con.Close()
        dr.Close()
        'End If
    End Sub
    ''' <summary>
    ''' to show report with formula if made
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub formulafield_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles formulafield.Click
        nowcolumns.Value = ""
        Dim nameofarray = hidfun.Value.Split(",")
        Dim st2, st1 As Integer
        st2 = UBound(nameofarray)
        st1 = 0
        For st1 = 0 To st2
            If st2 > 0 Then
                If finalformula.Text.Contains(nameofarray(st1)) Then
                    'finalformula.Text = finalformula.Text.Replace(nameofarray(st1), nameofarray(st1 + 1))
                End If

                st1 = st1 + 1
            End If

        Next
        If listcolumns.Items.Count = 0 Then

            aspnet_msgbox("Column Field Is Empty")
            Exit Sub
        End If
        Dim cont As String

        Dim formulaoutput As String = ""
        Dim formulacreated As String = ""
        If alias1.Text = "" Then
            formulacreated = ""
        Else

            formulacreated = "," & "  " & alias1.Text
        End If
        Dim j As Integer = 0

        Dim dr As SqlDataReader
        Dim p As Integer = 0
        Dim st As Integer = 1
        Dim datatype, allrows, allrows1 As String
        Dim strClose As String = ""


        ''''''''''''''comment..........

        Try




            'cmd = New SqlCommand("select count(*) as counts from " + Session("table") + "", con)
            'con.Open()
            'dr = cmd.ExecuteReader
            'If dr.Read Then
            '    cont = dr("counts")
            'End If


            'con.Close()
            'dr.Close()
            ''''''''''''''comment..........
            Dim str As String = ""
            Dim countnew As Integer
            ''''''''''''''comment..........
            'countnew = CType(cont, Integer)
            'Dim chk As String = ""
            'For st = 1 To countnew
            '    Dim nows As String
            '    nows = st.ToString
            '    If chk = "" Then


            '        cmd = New SqlCommand("alter table " + Session("table") + " add Filter numeric ", con)
            '        con.Open()
            '        cmd.ExecuteNonQuery()
            '        con.Close()
            '        chk = "value"
            '    End If
            '    ''''''''count''''''''''
            '    cmd = New SqlCommand("select " + finalformula.Text + " as  '" + finalformula.Text + "'  from " + Session("table") + " where RecordId='" + nows + "'", con)
            '    con.Open()
            '    dr = cmd.ExecuteReader
            '    While dr.Read

            '        Dim valval As String = dr(finalformula.Text)

            '        Dim cmdnew As New SqlCommand("update " + Session("table") + "  set Filter=" + valval + " where RecordId='" + nows + "'", con1)
            '        con1.Open()
            '        cmdnew.ExecuteNonQuery()
            '        con1.Close()

            '    End While





            '    con.Close()
            '    dr.Close()
            'Next
            Dim b As Boolean
            cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
            con.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                If dr("name") = commontablename Then
                    b = False
                    Exit While

                Else
                    b = True
                End If
            End While
            dr.Close()
            con.Close()

            If b = False Then


                cmd = New SqlCommand("drop table " + commontablename + "", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
            ''''''''''''''comment..........
            'Try
            Dim headdi As String = ""
            headdi = headdi & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            headdi = headdi & "<tr>"

            Dim gp As String = "Group by" & " " & allcolumn.Value
            'If gpby.Text = "" Then
            '    gp = ""
            'Else
            '    gp = "Group by" & " " & gpby.Text
            'End If
            ''''''''''''''comment..........
            cmd = New SqlCommand("select " + allcolumn.Value + " " + formulacreated + "  into " + commontablename + " from " + Session("table") + " " + gp + "", con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()


            'dataadapter = New SqlDataAdapter("select * from filtertableforgopaldemand ", con)
            ''''''''''''''comment..........
            dataadapter = New SqlDataAdapter("select * from " + commontablename + " ", con)
            con.Open()

            Dim dsnew As New DataSet
            dataadapter.Fill(dsnew)
            Dim daacol As DataColumn
            str = str & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            str = str & "<tr>"
            Dim colspan As Integer = 0
            For Each daacol In dsnew.Tables(0).Columns
                '
                If daacol.ColumnName <> "RecordId" Then
                    If nowcolumns.Value = "" Then
                        nowcolumns.Value = daacol.ColumnName
                    Else
                        nowcolumns.Value = nowcolumns.Value + "," + daacol.ColumnName
                    End If

                    str = str & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & daacol.ColumnName & "</b>"
                    str = str & "</td>"

                End If
                colspan = colspan + 1
            Next
            allcolumns.Items.Clear()
            allcolumns.DataSource = nowcolumns.Value.Split(",")
            allcolumns.DataBind()
            allcolumns.Items.Insert(0, "---select---")
            Dim notvalid As String = colspan - 1
            headdi = headdi & "<td colspan=" + notvalid + " align=center  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid' > " & "Report"
            'headdi = headdi & "</td></tr></tale>"
            headdi = headdi & "</td></tr>"
            headdi = headdi & "</table>"
            str = str & "</tr>"
            con.Close()
            'str = str & "<tr>"
            Dim row As DataRow
            Dim column As DataColumn

            For Each row In dsnew.Tables(0).Rows
                str = str & "<tr>"
                For Each column In dsnew.Tables(0).Columns
                    Dim inte As Integer = 0

                    'For inte = 1 To colspan
                    If column.ColumnName <> "RecordId" Then

                        str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & row.Item(column.ColumnName).ToString() & "</td>"

                    End If

                    'Next

                Next
                str = str & "</tr>"
            Next
            con.Close()

            str = str & "</table>"
            'cmd = New SqlCommand("alter table  " + Session("table") + " drop column Filter", con)
            'con.Open()
            'cmd.ExecuteNonQuery()
            'con.Close()
            Me.relax.InnerHtml = ""





            Divhead.InnerHtml = ""
            Divhead.InnerHtml = Divhead.InnerHtml & headdi
            Me.relax.InnerHtml = Me.relax.InnerHtml & str
            Session("div") = Divhead.InnerHtml
            Session("div1") = relax.InnerHtml
        Catch ex As Exception

            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)


            Exit Sub
        End Try



        If relax.InnerHtml = "" Then
            relax.Visible = False
        Else
            relax.Visible = True
        End If
        formulacreated = ""
    End Sub
    ''' <summary>
    ''' for select the column from list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub formulafilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles formulafilter.Click
        If listcolumns.Items.Count = 0 Then
            aspnet_msgbox("List Is Empty")
            Exit Sub
        End If
        If listcolumns.SelectedIndex = -1 Then
            aspnet_msgbox("Select Column First")
            Exit Sub
        Else

            finalformula.Text = finalformula.Text + listcolumns.SelectedItem.Text
            
            listcolumns.SelectedIndex = -1
        End If
    End Sub
    ''' <summary>
    ''' for + sign to add the values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click
        addformula(add.Text)
    End Sub
    ''' <summary>
    ''' for - sign to minus the values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub minus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles minus.Click

      addformula(minus.Text)   
    End Sub
    ''' <summary>
    ''' for * sign to multiply the values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub multy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles multy.Click
        addformula(multy.Text) 
    End Sub
    ''' <summary>
    ''' for / sign to divide the values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub divide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles divide.Click
        addformula(divide.Text)
        
    End Sub
    ''' <summary>
    ''' for ( to make the formula
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub leftb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles leftb.Click
        addformula(leftb.Text) 
    End Sub
    ''' <summary>
    ''' to add formula with alias
    ''' </summary>
    ''' <param name="nameofformula"></param>
    ''' <remarks></remarks>
    Public Sub addformula(ByVal nameofformula As String)
        finalformula.Text = finalformula.Text + nameofformula
        'filtercolumn = filtercolumn + finalformula.Text
        listcolumns.SelectedIndex = -1
    End Sub
    ''' <summary>
    ''' for ) to make the formula
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rightb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rightb.Click
        addformula(rightb.Text)
    End Sub
    ''' <summary>
    ''' to clear the formulas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles clear.Click
        If finalformula.Text = "" Then
            aspnet_msgbox("Field Is Already Empty")
        Else
            finalformula.Text = ""
        End If
    End Sub

    'Protected Sub setfrm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles setfrm.Click
    '    If relax.InnerHtml = "" Then
    '        allcolumns.Visible = False
    '        inde.Visible = False
    '        trcols1.Visible = False
    '        trcols.Visible = False
    '        aspnet_msgbox("First Make The Report")
    '        Exit Sub
    '    Else
    '        trcols1.Visible = True
    '        trcols.Visible = True
    '        allcolumns.Visible = True
    '        inde.Visible = True
    '    End If
    'End Sub
    ''' <summary>
    ''' to make invisible or visible some control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub allcolumns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles allcolumns.SelectedIndexChanged
        If selectionformula.SelectedItem.Text = "Between" Then
            valueinput.Visible = False
            tdrang.Visible = True
        Else
            tdrang.Visible = False
            valueinput.Visible = True
        End If
        go.Visible = True
        selectionformula.Visible = True
    End Sub
    ''' <summary>
    ''' to make invisible or visible some control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub selectionformula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectionformula.SelectedIndexChanged
        If selectionformula.SelectedItem.Text = "Between" Then
            valueinput.Visible = False
            tdrang.Visible = True
        Else
            tdrang.Visible = False
            valueinput.Visible = True
        End If
    End Sub
    ''' <summary>
    ''' to perform the selected formula on report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub go_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles go.Click
        If relax.InnerHtml = "" Then
            aspnet_msgbox("There Is No Report")
            Exit Sub
        End If
        If selectionformula.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("No Formula Is Selected")
            Exit Sub
        End If
        If allcolumns.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("No Column Is Selected")
            Exit Sub
        End If
        If Me.relax.InnerHtml = "" Then
            aspnet_msgbox("First Make The Report")
            Exit Sub
        End If
        If selectionformula.SelectedItem.Text = "Between" Then
            If txtfrm.Text = "" Then
                aspnet_msgbox("Fill From Range")
                Exit Sub

            End If

            If Textto.Text = "" Then
                aspnet_msgbox("Fill To Range")
                Exit Sub
            End If

        End If
        If selectionformula.SelectedItem.Text <> "Between" Then
            If valueinput.Text = "" Then
                aspnet_msgbox("Fill Value For Selected Formula")
                Exit Sub

            End If
        End If
        If allcolumns.SelectedItem.Text = "---Select---" Then
            aspnet_msgbox("Select Column First")
            Exit Sub
        Else

            Dim columns

            dataadapter = New SqlDataAdapter("select * from " + commontablename + "", con)
            con.Open()

            Dim ds As New DataSet
            dataadapter.Fill(ds)
            Dim data As DataColumn
            Dim strings As String = ""
            con.Close()
            If ds.Tables(0).Rows.Count > 0 Then
                For Each data In ds.Tables(0).Columns
                    If strings = "" Then

                        strings = data.ColumnName()
                    Else
                        strings = strings & "," & data.ColumnName()

                    End If
                Next
                columns = strings.Split(",")

            End If

            Dim str As String = ""
            Dim headdi As String = ""
            headdi = headdi & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            ' Dim arr = TextBox1.Text.Split(",")
            Dim asd, sdf As Integer
            asd = UBound(columns)


            Dim srrraaa As String = asd

            headdi = headdi & "<tr>"

            If selectionformula.SelectedItem.Text = "Greater Than" Then


                dataadapter = New SqlDataAdapter("select * from " + commontablename + " where " + allcolumns.SelectedItem.Text + " >'" + valueinput.Text + "'", con)
                con.Open()

            ElseIf selectionformula.SelectedItem.Text = "Less Than" Then
                dataadapter = New SqlDataAdapter("select * from " + commontablename + "  where " + allcolumns.SelectedItem.Text + " <'" + valueinput.Text + "'", con)
                con.Open()

            ElseIf selectionformula.SelectedItem.Text = "Equal To" Then
                dataadapter = New SqlDataAdapter("select * from " + commontablename + "  where " + allcolumns.SelectedItem.Text + " ='" + valueinput.Text + "'", con)
                con.Open()

            ElseIf selectionformula.SelectedItem.Text = "Starts With" Then
                dataadapter = New SqlDataAdapter("select * from " + commontablename + "  where " + allcolumns.SelectedItem.Text + " like( '" + valueinput.Text + "%')", con)
                con.Open()

            ElseIf selectionformula.SelectedItem.Text = "Ends With" Then
                dataadapter = New SqlDataAdapter("select * from " + commontablename + "  where " + allcolumns.SelectedItem.Text + "  like( '%" + valueinput.Text + "')", con)
                con.Open()

            ElseIf selectionformula.SelectedItem.Text = "Between" Then
                dataadapter = New SqlDataAdapter("select * from " + commontablename + "  where " + allcolumns.SelectedItem.Text + "  between '" + txtfrm.Text + "' and '" + Textto.Text + "'", con)
                con.Open()

            End If
            Dim dsnew As New DataSet
            dataadapter.Fill(dsnew)
            Dim daacol As DataColumn
            str = str & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            str = str & "<tr>"
            Dim colspan As Integer = 0
            For Each daacol In dsnew.Tables(0).Columns
                If daacol.ColumnName <> "RecordId" Then
                    str = str & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'>  <b>" & daacol.ColumnName & "</b>"
                    str = str & "</td>"
                End If

                colspan = colspan + 1

            Next
            Dim notvalid As String = colspan - 1
            headdi = headdi & "<td colspan=" + notvalid + " align=center  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid' > " & "Filter Percentage Report"
            'headdi = headdi & "</td></tr></tale>"
            headdi = headdi & "</td></tr>"
            headdi = headdi & "</table>"
            str = str & "</tr>"
            con.Close()
            'str = str & "<tr>"
            Dim row As DataRow
            Dim column As DataColumn
            For Each row In dsnew.Tables(0).Rows
                str = str & "<tr>"
                For Each column In dsnew.Tables(0).Columns
                    Dim inte As Integer = 0

                    'For inte = 1 To colspan
                    If column.ColumnName <> "RecordId" Then

                        str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & row.Item(column.ColumnName).ToString() & "</td>"

                    End If

                    'Next

                Next
                str = str & "</tr>"
            Next
            con.Close()

            str = str & "</table>"
            Try


                con.Close()

                Me.relax.InnerHtml = ""

                Divhead.InnerHtml = ""
                Divhead.InnerHtml = Divhead.InnerHtml & headdi
                Me.relax.InnerHtml = Me.relax.InnerHtml & str
                Session("div") = Divhead.InnerHtml
                Session("div1") = relax.InnerHtml
            Catch ex As Exception
                valueinput.Text = ""
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
               
                Exit Sub
            End Try
        End If
    End Sub
    ''' <summary>
    ''' for indexing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub inde_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles inde.Click
        If relax.InnerHtml = "" Then
            aspnet_msgbox("There Is No Report")
            Exit Sub
        End If
        If allcolumns.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("No Column Is Selected")
            Exit Sub
        End If
        If Me.relax.InnerHtml = "" Then
            aspnet_msgbox("First Make The Report")
            Exit Sub
        End If
        If allcolumns.SelectedItem.Text = "---Select---" Then
            aspnet_msgbox("Select Column First")
            Exit Sub
        Else
            dataadapter = New SqlDataAdapter("select * from " + commontablename + "  order by " + allcolumns.SelectedItem.Text + " ", con)
            con.Open()
            'Dim columns

            Dim ds As New DataSet
            dataadapter.Fill(ds)
            Dim data As DataColumn
            Dim strings As String = ""
            con.Close()

            Dim str As String = ""
            Dim headdi As String = ""
            headdi = headdi & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"



            headdi = headdi & "<tr>"






            Dim daacol As DataColumn
            str = str & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            str = str & "<tr>"
            Dim colspan As Integer = 0
            For Each daacol In ds.Tables(0).Columns
                If daacol.ColumnName <> "RecordId" Then


                    str = str & "<td  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'>  <b>" & daacol.ColumnName & "</b>"
                    str = str & "</td>"
                End If
                colspan = colspan + 1
            Next
            Dim notvalid As String = colspan - 1
            headdi = headdi & "<td colspan=" + notvalid + " align=center  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'> " & "Index Report"

            headdi = headdi & "</td></tr>"
            headdi = headdi & "</table>"
            str = str & "</tr>"
            con.Close()
            'str = str & "<tr>"
            Dim row As DataRow
            Dim column As DataColumn
            For Each row In ds.Tables(0).Rows
                str = str & "<tr>"
                For Each column In ds.Tables(0).Columns
                    Dim inte As Integer = 0

                    'For inte = 1 To colspan
                    If column.ColumnName <> "RecordId" Then

                        str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & row.Item(column.ColumnName).ToString() & "</td>"

                    End If

                    'Next

                Next
                str = str & "</tr>"
            Next
            con.Close()

            str = str & "</table>"
            Try
                Me.relax.InnerHtml = ""
                'str = str & "</table>"
                Divhead.InnerHtml = ""
                Divhead.InnerHtml = Divhead.InnerHtml & headdi
                Me.relax.InnerHtml = Me.relax.InnerHtml & str
                Session("div") = Divhead.InnerHtml
                Session("div1") = relax.InnerHtml
            Catch ex As Exception
                valueinput.Text = ""
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)

                Exit Sub

            Catch ex As Exception

            End Try

        End If
    End Sub
    ''' <summary>
    ''' for rating
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Rating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rating.Click
        If relax.InnerHtml = "" Then
            aspnet_msgbox("There Is No Report")
            Exit Sub
        End If
        If allcolumns.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("No Column Is Selected")
            Exit Sub
        End If
        If Me.relax.InnerHtml = "" Then
            aspnet_msgbox("First Make The Report")
            Exit Sub
        End If
        If allcolumns.SelectedItem.Text = "---Select---" Then
            aspnet_msgbox("Select Column First")
            Exit Sub
        Else
            Dim column_name As String = ""
            column_name = allcolumns.SelectedItem.Text
            Dim cmdnew As New SqlCommand("select distinct(" + column_name + ") as " + column_name + " from " + commontablename + " ", con)
            con.Open()
            readquery = cmdnew.ExecuteReader
            Dim columnvalue As String = ""
            Dim incrementstring As String = ""
            Dim increment As Integer
            While readquery.Read
                If columnvalue = "" Then
                    If IsDBNull(readquery(column_name)) Then

                    Else
                        columnvalue = readquery(column_name)

                        incrementstring = increment + 1
                        increment = increment + 1
                    End If
                Else
                    If IsDBNull(readquery(column_name)) Then

                    Else
                        columnvalue = columnvalue & "," & readquery(column_name)
                        increment = increment + 1

                        incrementstring = incrementstring & "," & increment
                    End If
                End If
            End While
            readquery.Close()
            con.Close()

            Dim columnvalueinarray = columnvalue.Split(",")
            Dim incrementcheck = incrementstring.Split(",")

            Dim intforlengt, intforstart As Integer
            intforlengt = UBound(columnvalueinarray)
            'Dim allcolumn = Session("filtercolumns")
            Dim allcolumn = nowcolumns.Value.Split(",")
            Dim i, j As Integer
            Dim querycolumn As String = ""
            i = UBound(allcolumn)
            For j = 0 To i

                If allcolumn(j) <> column_name Then

                    If querycolumn = "" Then
                        querycolumn = allcolumn(j)
                    Else
                        querycolumn = querycolumn & "," & allcolumn(j)

                    End If
                End If
            Next
            Dim nnn As String = finalformula.Text
            querycolumn = querycolumn & "," & column_name
            dataadapter = New SqlDataAdapter("select " + querycolumn + " from " + commontablename + "  order by " + allcolumns.SelectedItem.Text + "", con)
            con.Open()
            'Dim columns

            Dim ds As New DataSet
            dataadapter.Fill(ds)
            Dim data As DataColumn
            Dim strings As String = ""
            con.Close()

            Dim str As String = ""
            Dim headdi As String = ""
            headdi = headdi & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"



            headdi = headdi & "<tr>"






            Dim daacol As DataColumn
            str = str & "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'>"
            str = str & "<tr>"
            Dim colspan As Integer = 0
            For Each daacol In ds.Tables(0).Columns
                If daacol.ColumnName <> "RecordId" Then


                    str = str & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'>  <b>" & daacol.ColumnName & "</b>"
                    str = str & "</td>"
                End If
                colspan = colspan + 1
            Next
            str = str & "<td  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'>  <b>Rating</b>"
            str = str & "</td>"
            Dim notvalid As String = colspan
            headdi = headdi & "<td colspan=" + notvalid + " align=center  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid' > " & "Rating Report"

            headdi = headdi & "</td></tr>"
            headdi = headdi & "</table>"
            str = str & "</tr>"
            con.Close()
            'str = str & "<tr>"
            Dim row As DataRow
            Dim column As DataColumn
            For Each row In ds.Tables(0).Rows
                str = str & "<tr>"
                For Each column In ds.Tables(0).Columns
                    Dim inte As Integer = 0

                    'For inte = 1 To colspan
                    If column.ColumnName <> "RecordId" Then
                        If column.ColumnName = column_name Then
                            For intforstart = 0 To intforlengt


                                If row.Item(column.ColumnName).ToString() = columnvalueinarray(intforstart) Then
                                    str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & row.Item(column.ColumnName).ToString() & "</td>"
                                    str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & incrementcheck(intforstart) & "</td>"
                                End If


                            Next
                        Else
                            str = str & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>" & row.Item(column.ColumnName).ToString() & "</td>"

                        End If
                        
                       
                    End If

                    'Next

                Next
                str = str & "</tr>"
            Next
            con.Close()

            str = str & "</table>"
            Try
                Me.relax.InnerHtml = ""
                'str = str & "</table>"
                Divhead.InnerHtml = ""
                Divhead.InnerHtml = Divhead.InnerHtml & headdi
                Me.relax.InnerHtml = Me.relax.InnerHtml & str
                Session("div") = Divhead.InnerHtml
                Session("div1") = relax.InnerHtml
            Catch ex As Exception
                valueinput.Text = ""
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)

                Exit Sub

            Catch ex As Exception

            End Try

        End If

    End Sub
    ''' <summary>
    ''' to make aggregate function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ok.Click
        If finalformula.Text <> "" Then
            If alias1.Text = "" Then
                alias1.Text = finalformula.Text + " as " + alais.Text
                'allcolumns.Items.Add(alais.Text)
                finalformula.Text = ""
                alais.Text = ""

            Else
                alias1.Text = alias1.Text + "," + finalformula.Text + " as " + alais.Text
                'allcolumns.Items.Add(alais.Text)
                finalformula.Text = ""
                alais.Text = ""

            End If

        End If

    End Sub

    'Protected Sub GrouBy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrouBy.Click
    '    If listcolumns.Items.Count = 0 Then
    '        aspnet_msgbox("List Is Empty")
    '        Exit Sub
    '    End If
    '    If listcolumns.SelectedIndex = -1 Then
    '        aspnet_msgbox("Select Column First")
    '        Exit Sub
    '    Else
    '        If gpby.Text = "" Then
    '            gpby.Text = listcolumns.SelectedItem.Text
    '        Else
    '            gpby.Text = gpby.Text + "," + listcolumns.SelectedItem.Text

    '        End If

    '        listcolumns.SelectedIndex = -1
    '    End If
    'End Sub
End Class
