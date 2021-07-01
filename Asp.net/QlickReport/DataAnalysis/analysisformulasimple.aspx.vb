Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.Math
Partial Class DataAnalysis_analysisformulasimple
    Inherits System.Web.UI.Page

    Dim conn As String = AppSettings("ConnectionString")

    Dim noofcols, start As Integer

    Dim cmdvalue, cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet





    Dim datatype As String
    Dim b As Boolean

    Dim strClose As String = ""
    Dim con As New SqlConnection(conn)
    Dim columns
    Dim table


    Dim noofcols211(3) As Array

    Dim noofcolsy

    ''' <summary>
    ''' this is use to show error messsage in any case
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks>created by: Ranjit Singh created on</remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' This Is use to find the value of all the formulas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on</remarks>
    Protected Sub ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ok.Click
        ''''''''for checking the existance of table r566r from database and delete'''''''''''''''
        '''''''''start''''''''''''''
        Session("saveanalysis") = ""
        Session("maxval") = ""
        Session("Checkforreportformilas") = ""
        'Dim tableperform As String = "R566R" + Session("userid").ToString + System.DateTime.Now.Millisecond.ToString
        Dim tableperform As String = "QlickReport"
        columns = Session("colsvalue")
        'table = Session("table")
        noofcols = UBound(columns)
        Dim result
        Dim result1
        cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        dr = cmdvalue.ExecuteReader
        While dr.Read()
            If dr("name") = tableperform Then
                b = False
                Exit While

            Else
                b = True
            End If
        End While
        dr.Close()
        con.Close()

        If b = False Then


            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
        End If
        Dim repname As String = Session("repname")
        Dim repp As String = ""
        Dim repp1 As String = ""
        Dim colspan1 As String = ""
        repp = table.ToString
        If repp.Contains("tab") Then

            repp1 = repp.Remove(0, 3)
        End If
        colspan1 = noofcols + 2.ToString
        result = "<table border=1 style='background-color:GradientActiveCaption;border:#336699 1px solid' > "
        result = result & "<caption>" & repname
        result = result & "</caption>"
        'result = "<table border=2px>"
        'result = result & "<tr><td align=center style=" + "background-color:pink colspan=" + colspan1 + " > " & repname & "</td></tr>"
        result = result & "<tr><td style='background-color:lightgrey; color:black; Font-size:10pt'><b>Formulas</b></td>"

        For start = 0 To noofcols
            result = result & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & columns(start) & "</b></td>"
        Next
        result = result & "</tr>"

        '''''''''''end''''''''''''''''
        ''''''''''find maxmium value''''''''''''''
        '''''''''start''''''''''''''
        If Max.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "max"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "max"
            End If
            Dim maxval As String = ""
            Dim strhtml As String = ""
            result = result & "<tr>"
            Session("saveanalysis") = "yes"

            result = result & "<td style='color:black'>" & Me.maxi.InnerText & "</td>"
            'For start = 0 To noofcols

            'If strhtml = "" Then
            'strhtml = "<table border=2px><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            'Else
            ' strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            'End If

            ' Next
            ' strhtml = strhtml & "</tr>"
            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''
                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)


                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype = "varchar" Then
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select isnull(max(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    ' da.SelectCommand = cmdvalue
                    'da.Fill(ds)
                    dr = cmdvalue.ExecuteReader
                    ' If ds.Tables(0).Rows.Count > 0 Then
                    If dr.Read Then
                        If maxval = "" Then
                            maxval = "<td style='color:black; Font-size:10pt'>" & dr(columns(start)) & "</td>"

                        Else
                            maxval = maxval & " " & "<td style='color:black; Font-size:10pt'>" & dr(columns(start)) & "</td>"


                        End If
                    End If

                    con.Close()
                    dr.Close()
                Else



                    Try



                        cmdvalue = New SqlCommand("select " + (columns(start)) + " as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select isnull(max(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If maxval = "" Then
                            maxval = "<td style='color:black; Font-size:10pt'>" & dr(columns(start)) & "</td>"

                        Else
                            maxval = maxval & " " & "<td style='color:black; Font-size:10pt'>" & dr(columns(start)) & "</td>"


                        End If
                    End If


                    con.Close()
                    dr.Close()



                End If

                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            Next
            maxval = maxval & "</tr>"
            'maxval = strhtml & maxval & "</table>"
            result = result & maxval
            Session("maxval") = result
        Else
            Session("saveanalysis") = "no"
        End If

        ''''''''''''''''end'''''''''''''''
        ''''''''''find minimum value''''''''''''''
        '''''''''start''''''''''''''
        If Min.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "min"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "min"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim minval As String = ""
            Dim strhtml As String = ""
            For start = 0 To noofcols

                If strhtml = "" Then
                    strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
                Else
                    strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
                End If

            Next
            strhtml = strhtml & "</tr>"
            'If result = "" Then
            '    result = Me.mini.InnerText
            'Else
            '    result = result & "$" & Me.mini.InnerText
            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.mini.InnerText & "</td>"

            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''


                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If

                con.Close()

                dr.Close()
                If datatype = "varchar" Then




                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(min(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If minval = "" Then

                            minval = "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"
                        Else

                            minval = minval & "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"
                        End If
                    End If

                    con.Close()
                    dr.Close()
                Else



                    Try



                        cmdvalue = New SqlCommand("select " + (columns(start)) + " as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select isnull(min(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If minval = "" Then
                            minval = "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"

                        Else
                            minval = minval & "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"  '" " & "<b>" & columns(start) & "</b>" & "=" & dr(columns(start)) & "<br>"

                        End If
                    End If


                    con.Close()
                    dr.Close()







                End If
                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            minval = minval & "</tr>"
            ' minval = strhtml & minval & "</table>"
            result = result & minval
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If



        ''''''''''''''''end'''''''''''''''



        ''''''''''find minimum value''''''''''''''
        '''''''''start''''''''''''''

        If average.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "ave"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "ave"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim avgval As String = ""

            'If result = "" Then
            '    result = Me.avg.InnerText
            'Else
            '    result = result & "$" & Me.avg.InnerText


            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.avg.InnerText & "</td>"

            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"


            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''


                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Average Of Date")
                Else


                    Try


                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        'aspnet_msgbox("Select Only Numeric Data")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(avg(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If avgval = "" Then
                            avgval = "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"

                        Else
                            avgval = avgval & "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"
                        End If
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                End If
            Next
            avgval = avgval & "</tr>"
            ' avgval = strhtml & avgval & "</table>"
            result = result & avgval
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If

        ''''''''''''''''end'''''''''''''''

        ''''''''''find count value''''''''''''''
        '''''''''start''''''''''''''

        If count.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "cou"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "cou"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim countval As String = ""
            'If result = "" Then
            '    result = Me.counts.InnerText
            'Else
            '    result = result & "$" & Me.counts.InnerText
            'End If
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"
            result = result & "<tr>"
            result = result & "<td>" & Me.counts.InnerText & "</td>"


            For start = 0 To noofcols

                cmdvalue = New SqlCommand("select count(" + (columns(start)) + ") as " + (columns(start)) + " from " + table + "", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    If countval = "" Then
                        countval = "<td style=" + "color:black;>" & dr(columns(start)) & "</td>" '"<b>" & columns(start) & "</b>" & "=" & dr(columns(start)) & "<br>"

                    Else
                        countval = countval & "<td style=" + "color:black;>" & dr(columns(start)) & "</td>"
                    End If
                End If

                con.Close()
                ds.Clear()

            Next
            countval = countval & "</tr>"
            'countval = strhtml & countval & "</table>"
            result = result & countval
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If

        ''''''''''''''''end'''''''''''''''

        ''''''''''find mean value''''''''''''''
        '''''''''start''''''''''''''

        If mean.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "mea"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "mea"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim means As String = ""
            Dim meansum As String = ""
            Dim meanvalue As String = ""
            'If result = "" Then
            '    result = Me.meanval.InnerText
            'Else
            '    result = result & "$" & Me.meanval.InnerText
            'End If
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"

            result = result & "<tr>"
            result = result & "<td>" & Me.meanval.InnerText & "</td>"

            For start = 0 To noofcols


                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''


                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Mean Of Date")
                    Exit Sub
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()

                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        means = dr(columns(start))
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        meansum = dr(columns(start))
                    End If


                    If meanvalue = "" Then
                        meanvalue = "<td style=" + "color:black;>" & meansum / means & "</td>" ' meansum / means
                        If meanvalue.Contains("NaN") Then
                            meanvalue = meanvalue.Replace("NaN", "0")
                        End If
                    Else
                        meanvalue = meanvalue & "<td style=" + "color:black;>" & meansum / means & "</td>" '"," & " " & meansum / means
                        If meanvalue.Contains("NaN") Then
                            meanvalue = meanvalue.Replace("NaN", "0")
                        End If
                    End If


                    con.Close()

                    dr.Close()
                    cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                End If

            Next
            meanvalue = meanvalue & "</tr>"
            'meanvalue = strhtml & meanvalue & "</table>"
            result = result & meanvalue
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If

        ''''''''''''''''end'''''''''''''''

        ''''''''''find mode value''''''''''''''
        '''''''''start''''''''''''''


        If mode.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "mod"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "mod"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim modenow As String = ""
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"

            Dim modevalue As String = ""
            'If result = "" Then
            '    result = Me.modeval.InnerText
            'Else
            '    result = result & "$" & Me.modeval.InnerText
            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.modeval.InnerText & "</td>"
            For start = 0 To noofcols
                cmdvalue = New SqlCommand("SELECT " + (columns(start)) + ",COUNT(" + (columns(start)) + ") AS NumOccurrences into " + tableperform + " FROM " + table + " GROUP BY " + (columns(start)) + " HAVING ( COUNT(" + (columns(start)) + ")>0)", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                cmdvalue = New SqlCommand("select " + (columns(start)) + " as " + (columns(start)) + " from " + tableperform + " where NumOccurrences=(select max(NumOccurrences) from " + tableperform + ")", con)
                con.Open()

                dr = cmdvalue.ExecuteReader

                While dr.Read
                    If modevalue = "" Then
                        modevalue = dr(columns(start))  ' "<b>" & columns(start) & "</b>" & "=" & dr(columns(start)) & "<br>"


                    Else
                        modevalue = modevalue & "," & dr(columns(start)) '" " & "<b>" & columns(start) & "</b>" & "=" & dr(columns(start)) & "<br>"

                    End If
                End While

                If modenow = "" Then
                    modenow = "<td style=" + "color:black;>" & modevalue & "</td>"
                Else
                    modenow = modenow & "<td style=" + "color:black;>" & modevalue & "</td>"
                End If
                con.Close()
                dr.Close()
                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                modevalue = ""
            Next
            modevalue = modenow
            modevalue = modevalue & "</tr>"
            ' modevalue = strhtml & modevalue & "</table>"
            result = result & modevalue
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If
        ''''''''''''''''end'''''''''''''''

        ''''''''''find range value''''''''''''''
        '''''''''start''''''''''''''

        If range.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "ran"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "ran"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"
            Dim rangemax As String
            Dim rangemin As String
            'Dim rangestr As String = ""
            Dim rangevalue As String = ""
            'If result = "" Then
            '    result = Me.rangeval.InnerText
            'Else
            '    result = result & "$" & Me.rangeval.InnerText


            'End If

            result = result & "<tr>"
            result = result & "<td>" & Me.rangeval.InnerText & "</td>"

            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select isnull(max(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then

                        rangemax = dr(columns(start))

                    End If
                    con.Close()
                    dr.Close()
                    cmdvalue = New SqlCommand("select isnull(min(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then

                        rangemin = dr(columns(start))

                    End If


                    con.Close()
                    dr.Close()
                    Dim i, j As Double
                    i = CType(rangemax, Double)
                    j = CType(rangemin, Double)
                    If rangevalue = "" Then
                        'If rangestr = "" Then
                        rangevalue = "<td style=" + "color:black;>" & (i - j).ToString & "</td>"
                        'Else
                        ' rangevalue = "<td style=" + "color:black;>" & (i - j).ToString & "</td><td></td>"
                        'End If

                    Else
                        rangevalue = rangevalue & "<td style=" + "color:black;>" & (i - j).ToString & "</td>"
                    End If

                Else



                    Try



                        cmdvalue = New SqlCommand("select " + (columns(start)) + " as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select isnull(max(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then

                        rangemax = dr(columns(start))

                    End If
                    con.Close()
                    dr.Close()
                    cmdvalue = New SqlCommand("select isnull(min(" + (columns(start)) + "),0) as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then

                        rangemin = dr(columns(start))

                    End If


                    con.Close()
                    dr.Close()
                    'new'''''''''''
                    Dim maxdate, mindate As Date
                    Dim rangedate As TimeSpan
                    maxdate = CType(rangemax, Date)
                    mindate = CType(rangemin, Date)

                    If rangevalue = "" Then
                        'If rangevalue = "" Then
                        rangedate = maxdate.Subtract(mindate)
                        rangevalue = rangedate.ToString
                        rangevalue = "<td style=" + "color:black;>" & rangevalue.Replace(".00:00:00", " Days") & "</td>"
                        'Else
                        'rangedate = maxdate.Subtract(mindate)
                        'rangestr = rangedate.ToString
                        'rangestr = rangestr & "<td style=" + "color:black;>" & rangestr.Replace(".00:00:00", " Days") & "</td><td></td>"
                        'End If
                    Else
                        rangedate = maxdate.Subtract(mindate)
                        Dim newstr As String
                        newstr = rangedate.ToString
                        newstr = newstr.Replace(".00:00:00", " Days")
                        rangevalue = rangevalue & "<td style=" + "color:black;>" & newstr & "</td>"
                    End If
                    '''''''''''''''''''***********'''''''''''''''

                End If
                'If datatype = "datetime" Then
                'Dim maxdate, mindate As Date
                'Dim rangedate As TimeSpan
                'maxdate = CType(rangemax, Date)
                'mindate = CType(rangemin, Date)

                'If rangestr = "" Then
                '    rangedate = maxdate.Subtract(mindate)
                '    rangestr = rangedate.ToString
                '    rangestr = "<tr><td style=" + "color:black;>" & rangestr.Replace(".00:00:00", " Days") & "</td><td></td>"   '"<b>" & columns(start) & "</b>" & "=" & rangestr.Replace(".00:00:00", " Days") & "<br>"
                'Else
                '    rangedate = maxdate.Subtract(mindate)
                '    Dim newstr As String
                '    newstr = rangedate.ToString
                '    newstr = newstr.Replace(".00:00:00", " Days")
                '    rangestr = "<td style=" + "color:black;>" & newstr & "</td><td></td>" ' "<b>" & columns(start) & "</b>" & "=" & newstr & "<br>"

                'End If
                ' Else

                'Dim i, j As Double
                'i = CType(rangemax, Double)
                'j = CType(rangemin, Double)

                'rangevalue = "<tr><td style=" + "color:black;>" & (i - j).ToString & "</td><td></td>" '"<b>" & columns(start) & "</b>" & "=" & (i - j).ToString & "<br>"

                ' End If
                'Dim nows As String
                'If datatype = "datetime" Then

                '    If nows = "" Then
                '        result = result & "$" & rangestr
                '        Session("maxval") = result
                '        nows = result ' "<b>" & columns(start) & "</b>" & "=" & result & "<br>"
                '    Else
                '        result = result & " " & rangestr
                '        Session("maxval") = result
                '        nows = result ' "<b>" & columns(start) & "</b>" & "=" & result & "<br>"
                '    End If

                'Else
                '    If nows = "" Then
                '        result = result & "$" & rangevalue
                '        Session("maxval") = result
                '        nows = result '"<b>" & columns(start) & "</b>" & "=" & result & "<br>"
                '    Else
                '        result = result & " " & rangevalue
                '        Session("maxval") = result
                '        nows = result ' "<b>" & columns(start) & "</b>" & "=" & result & "<br>"
                '    End If

                'End If
                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            Next
            Dim nows As String = ""
            rangevalue = rangevalue & "</tr>"
            ' rangevalue = strhtml & rangevalue & "</table>"
            result = result & rangevalue
            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If
        ''''''''''''''''end'''''''''''''''


        ''''''''''find median value''''''''''''''
        '''''''''start''''''''''''''

        If median.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "med"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "med"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim midd, cal, ca As Double
            Dim sdiv As String = ""
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"

            Dim midvalue As String
            Dim medeanval As String = ""
            'If result = "" Then
            '    result = Me.mid.InnerText
            'Else
            '    result = result & "$" & Me.mid.InnerText

            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.mid.InnerText & "</td>"
            cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
            con.Open()
            dr = cmdvalue.ExecuteReader
            While dr.Read()
                If dr("name") = tableperform Then
                    b = False
                    Exit While

                Else
                    b = True
                End If
            End While
            dr.Close()
            con.Close()

            If b = False Then


                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            End If
            For start = 0 To noofcols
                cmdvalue = New SqlCommand("select Identity(int, 1,1) as nocount, " + (columns(start)) + "  into " + tableperform + " from  " + table + " order by " + (columns(start)) + " desc", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                cmdvalue = New SqlCommand("select isnull(count(nocount),0) as nocount from " + tableperform + "", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                Dim sa, aa As String
                Dim k As Integer
                If dr.Read Then

                    midvalue = dr("nocount").ToString

                    midd = CType(midvalue, Double)
                    midd = midd + 1

                    cal = midd / 2

                    sa = CType(cal, String)
                    Dim str3 As String = ""
                    Dim int1 As Integer
                    str3 = CType(cal, String)
                    If str3.Contains(".") Then
                        int1 = str3.IndexOf(".")
                        str3 = str3.Substring(0, int1)
                    End If

                    '  k = CType(cal, Integer)
                    aa = str3 ' CType(k, String)
                    sa = sa & ".0"

                    Dim ass = sa.Split(".")
                    If ass(1) <> "" Then
                        ca = CType(ass(1), Double)
                    End If
                End If
                con.Close()
                dr.Close()

                If ca > 0 Then
                    Dim inteloop As Integer
                    'strhtml = ""
                    'For inteloop = 0 To noofcols

                    '    If strhtml = "" Then
                    '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(inteloop)) & "</b></td><td></td><td></td><td></td>"
                    '    Else
                    '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(inteloop)) & "</b></td><td></td><td></td><td></td>"
                    '    End If

                    'Next
                    'strhtml = strhtml & "</tr>"
                    Dim i As Integer


                    For i = 0 To 1
                        cmdvalue = New SqlCommand("select  " + (columns(start)) + "  as " + (columns(start)) + "  from " + tableperform + " where nocount=" + aa + "", con)
                        con.Open()
                        dr = cmdvalue.ExecuteReader

                        If dr.Read Then
                            If medeanval = "" Then
                                medeanval = dr(columns(start)) 'dr(columns(start))
                            Else
                                medeanval = medeanval & "," & dr(columns(start)) '"," & " " & dr(columns(start))
                            End If

                        End If
                        Dim convert As Integer
                        convert = CType(aa, Integer)
                        convert = convert + 1

                        aa = CType(convert, String)
                        con.Close()
                        dr.Close()
                    Next


                    If sdiv = "" Then
                        sdiv = "<td style=" + "color:black;>" & medeanval & "</td>"

                    Else
                        sdiv = sdiv & "<td style=" + "color:black;>" & medeanval & "</td>"
                    End If
                    medeanval = ""

                Else
                    cmdvalue = New SqlCommand("select  " + (columns(start)) + "  as " + (columns(start)) + "  from " + tableperform + " where nocount=" + aa + " ", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If medeanval = "" Then
                            medeanval = "<td style=" + "color:black;>" & dr(columns(start)) & "</td>" 'dr(columns(start))
                        Else
                            medeanval = medeanval & "<td style=" + "color:black;>" & dr(columns(start)) & "</td>" 'medeanval & "," & " " & dr(columns(start))
                        End If

                    End If

                    con.Close()
                    dr.Close()
                End If
                cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader
                While dr.Read()
                    If dr("name") = tableperform Then
                        b = False
                        Exit While

                    Else
                        b = True
                    End If
                End While
                dr.Close()
                con.Close()

                If b = False Then


                    cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                End If
            Next
            'If ca > 0 Then


            '    Dim inteloop As Integer
            '    strhtml = ""
            '    For inteloop = 0 To noofcols

            '        If strhtml = "" Then
            '            strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(inteloop)) & "</b></td><td></td><td></td><td></td>"
            '        Else
            '            strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(inteloop)) & "</b></td><td></td><td></td><td></td>"
            '        End If

            '    Next
            '    strhtml = strhtml & "</tr>"
            'End If 
            If ca > 0 Then
                medeanval = sdiv
            End If

            medeanval = medeanval & "</tr>"
            ' medeanval = strhtml & medeanval & "</table>"
            result = result & medeanval
            Session("maxval") = result

            cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
            con.Open()
            dr = cmdvalue.ExecuteReader
            While dr.Read()
                If dr("name") = tableperform Then
                    b = False
                    Exit While

                Else
                    b = True
                End If
            End While
            dr.Close()
            con.Close()

            If b = False Then


                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            End If
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If


        ''''''''''''''''end'''''''''''''''

        ''''''''''find row sum percentage value''''''''''''''
        '''''''''start''''''''''''''
        If rowsumpercentage.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "rowp"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "rowp"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"



            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"
            Dim sumtotal As String = ""
            Dim allcolumn As String = ""
            Dim singlcolumn1 As String = ""
            Dim singlcolumn As String = ""
            ' Dim rowsumfinal As Double
            '  Dim firstcolumn As Double
            ' Dim totalsum As Double
            Dim sum As String = ""
            Dim obj
            Dim i, j, k, l As Integer
            'If result = "" Then
            '    result = Me.rowsum.InnerText
            'Else
            '    result = result & "$" & Me.rowsum.InnerText


            'End If

            result = result & "<tr>"
            result = result & "<td>" & Me.rowsum.InnerText & "</td>"


            For start = 0 To noofcols
                Dim ss As String
                ss = ""
                Dim fd As String
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then
                    'If allcolumn = "" Then

                    allcolumn = columns(start)
                    ' Else

                    ' allcolumn = allcolumn & "+" & columns(start)
                    'End If
                Else
                    aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                    'aspnet_msgbox("Select Only Numeric Data")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End If



                Try
                    cmdvalue = New SqlCommand("select isnull(sum(Convert(numeric, " + allcolumn + ")),0) as total from " + table + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        If sumtotal = "" Then
                            sumtotal = dr("total").ToString
                        Else
                            sumtotal = sumtotal & "," & dr("total").ToString
                        End If

                    End If
                    con.Close()
                    dr.Close()

                Catch ex As Exception

                    aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End Try

            Next

            obj = sumtotal.Split(",")

            i = UBound(obj)
            For j = 0 To i
                If sum = "" Then
                    sum = obj(j)
                Else
                    Dim db As Double
                    db = CType(sum, Double)

                    sum = db + obj(j)

                End If
            Next
            obj = sumtotal.Split(",")

            l = UBound(obj)

            For k = 0 To i
                Dim sumpercentage, valueof As Double
                sumpercentage = CType(sum, Double)
                valueof = obj(k) * 100 / sumpercentage
                If singlcolumn1 = "" Then
                    singlcolumn1 = valueof.ToString
                    If singlcolumn1.Contains("NaN") Then
                        singlcolumn1 = singlcolumn1.Replace("NaN", "0")
                    End If
                Else
                    singlcolumn1 = singlcolumn1 & "," & valueof.ToString
                    If singlcolumn1.Contains("NaN") Then
                        singlcolumn1 = singlcolumn1.Replace("NaN", "0")
                    End If
                End If
            Next
            Dim vaal = singlcolumn1.Split(",")
            For start = 0 To noofcols


                Dim forcolumn = ""
                If singlcolumn = "" Then
                    singlcolumn = "<td style=" + "color:black;>" & vaal(start).ToString & "</td>"  '"<b>" & columns(start) & "</b>" & "=" & vaal(start).ToString & "<br>"

                Else
                    singlcolumn = singlcolumn & "<td style=" + "color:black;>" & vaal(start).ToString & "</td>"   '"<b>" & columns(start) & "</b>" & "=" & vaal(start).ToString & "<br>"
                End If
            Next


            singlcolumn = singlcolumn & "</tr>"
            'singlcolumn = strhtml & singlcolumn & "</table>"
            result = result & singlcolumn

            Session("maxval") = result

        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"

        End If
        ''''''''''''''''end'''''''''''''''







        If standarderror.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "sta"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "sta"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"

            Dim selectcol, selectcoly As String
            Dim finalcorel As String
            Dim sqtofxmean, sqtofxmeany As String
            Dim sumofxy As String
            Dim multixy As String
            Dim meanxsum, meanysum As String
            Dim comeans, comeansy, comeansum, comeansumy, comeanvalue, comeanvaluey As String
            Dim noofcols2
            Dim xmean, xmeany As String
            'If result = "" Then
            '    result = Me.correl.InnerText
            'Else
            '    result = result & "$" & Me.correl.InnerText

            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.ster.InnerText & "</td>"
            For start = 0 To 0


                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Mean Of Date")
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeans = dr(columns(start)).ToString
                        If comeans = "" Then
                            comeans = 0
                        End If
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansum = dr(columns(start)).ToString
                        If comeansum = "" Then
                            comeansum = 0
                        End If
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvalue = "" Then
                        comeanvalue = comeansum / comeans

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    selectcol = ""
                    While dr.Read
                        If selectcol = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = 0
                            Else
                                selectcol = dr(columns(start)).ToString
                            End If
                            'selectcol = dr(columns(start))
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = selectcol & "," & 0
                            Else
                                selectcol = selectcol & "," & dr(columns(start)).ToString
                            End If
                            'selectcol = selectcol & "," & dr(columns(start))
                        End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcols2 = selectcol.Split(",")

                    Dim i, j As Integer
                    i = UBound(noofcols2)

                    For j = 0 To i
                        If xmean = "" Then
                            xmean = (noofcols2(j) - comeanvalue).ToString
                        Else
                            xmean = xmean & "," & (noofcols2(j) - comeanvalue).ToString
                        End If


                    Next
                    Dim xmeanarra = xmean.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarra)



                    For m = 0 To l
                        If sqtofxmean = "" Then
                            sqtofxmean = (xmeanarra(m) * xmeanarra(m)).ToString
                        Else
                            sqtofxmean = sqtofxmean & "," & (xmeanarra(m) * xmeanarra(m)).ToString
                        End If
                    Next
                    Dim sqtofxmeansum = sqtofxmean.Split(",")
                    Dim p, k As Integer
                    p = UBound(sqtofxmeansum)



                    For k = 0 To p

                        If meanxsum = "" Then
                            meanxsum = sqtofxmeansum(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanxsum, Double)

                            meanxsum = (sqtofxmeansum(k) + g).ToString
                        End If

                    Next
                End If
            Next
            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
            For start = 0 To noofcols
                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Mean Of Date")
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select count(" + (columns(start)) + ") as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeansy = dr(columns(start))
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansumy = dr(columns(start))
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvaluey = "" Then
                        comeanvaluey = comeansumy / comeansy

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    selectcoly = ""
                    While dr.Read

                        If selectcoly = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = 0
                            Else
                                selectcoly = dr(columns(start)).ToString
                            End If
                            'selectcol = dr(columns(start))
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = selectcoly & "," & 0
                            Else
                                selectcoly = selectcoly & "," & dr(columns(start)).ToString
                            End If
                            'selectcol = selectcol & "," & dr(columns(start))
                        End If
                        'If selectcoly = "" Then
                        '    selectcoly = dr(columns(start))
                        'Else
                        '    selectcoly = selectcoly & "," & dr(columns(start))
                        'End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcolsy = selectcoly.Split(",")
                    Dim i, j As Integer
                    i = UBound(noofcolsy)

                    For j = 0 To i

                        If xmeany = "" Then
                            xmeany = (noofcolsy(j) - comeanvaluey).ToString
                        Else
                            xmeany = xmeany & "," & (noofcolsy(j) - comeanvaluey).ToString
                        End If


                    Next
                    Dim xmeanarray = xmeany.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarray)



                    For m = 0 To l
                        If sqtofxmeany = "" Then
                            sqtofxmeany = (xmeanarray(m) * xmeanarray(m)).ToString
                        Else
                            sqtofxmeany = sqtofxmeany & "," & (xmeanarray(m) * xmeanarray(m)).ToString
                        End If
                    Next
                    Dim xmeanarra = sqtofxmeany.Split(",")
                    Dim p, k As Integer
                    p = UBound(xmeanarra)



                    For k = 0 To p
                        ' Dim aj As String
                        If meanysum = "" Then
                            meanysum = xmeanarra(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanysum, Double)

                            meanysum = (xmeanarra(k) + g).ToString
                        End If

                    Next
                    Dim arrayxmean = xmean.Split(",")
                    Dim arrayymean = xmeany.Split(",")
                    Dim strt, strtt As Integer
                    strt = UBound(arrayxmean)


                    For strtt = 0 To strt
                        If multixy = "" Then
                            multixy = (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        Else
                            multixy = multixy & "," & (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        End If
                    Next
                    Dim sumxy = multixy.Split(",")

                    Dim loopx, loopy As Integer
                    loopx = UBound(sumxy)
                    For loopy = 0 To loopx
                        If sumofxy = "" Then
                            sumofxy = sumxy(loopy).ToString
                        Else
                            Dim h As Double
                            h = CType(sumofxy, Double)
                            sumofxy = (h + sumxy(loopy)).ToString
                        End If

                    Next
                    'If sumofxy = 0 Or meanysum = 0 Then
                    '    finalcorel = 0
                    'Else
                    '    finalcorel = sumofxy / meanysum
                    Dim chknow As String = ""
                    chknow = (sumofxy / Math.Sqrt((meanysum * meanxsum))).ToString
                    If chknow.Contains("NaN") Then
                        chknow = chknow.Replace("NaN", "0")

                    End If
                    'End If
                    Dim sterror As Double
                    sterror = (1 - (Math.Pow(chknow, 2))) / Math.Sqrt(comeansy)

                    If finalcorel = "" Then
                        finalcorel = "<td style=" + "color:black;>" & sterror.ToString & "</td>" ' "<b>" & columns(0) & "</b>" & "<b>" & " " & "With" & " " & "</b>" & "<b>" & columns(start) & "</b>" & "=" & chknow & "<br>"
                    Else
                        finalcorel = finalcorel & "<td style=" + "color:black;>" & sterror.ToString & "</td>" ' "<b>" & columns(0) & "</b>" & "<b>" & " " & "With" & " " & "</b>" & "<b>" & columns(start) & "</b>" & "=" & chknow & "<br>"
                    End If

                End If
                selectcoly = ""
                xmeany = ""
                sqtofxmeany = ""
                meanysum = ""
                multixy = ""
                sumofxy = ""
                comeanvaluey = ""

                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            ' Dim stst As String = ""
            ' stst = noofcols.ToString
            'finalcorel = "<td colspan=" + stst + ">" & finalcorel
            finalcorel = finalcorel & "</tr>"
            result = result & finalcorel

            Session("maxval") = result

        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"



        End If
















        ''''''''''find correlatione value''''''''''''''
        '''''''''start''''''''''''''

        If correlation.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "cor"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "cor"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim selectcol, selectcoly As String
            Dim finalcorel As String
            Dim sqtofxmean, sqtofxmeany As String
            Dim sumofxy As String
            Dim multixy As String
            Dim meanxsum, meanysum As String
            Dim comeans, comeansy, comeansum, comeansumy, comeanvalue, comeanvaluey As String
            Dim noofcols2
            Dim xmean, xmeany As String
            'If result = "" Then
            '    result = Me.correl.InnerText
            'Else
            '    result = result & "$" & Me.correl.InnerText

            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.correl.InnerText & "</td>"
            For start = 0 To 0


                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Mean Of Date")
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select count(" + (columns(start)) + ") as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeans = dr(columns(start)).ToString
                        If comeans = "" Then
                            comeans = 0
                        End If
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansum = dr(columns(start)).ToString
                        If comeansum = "" Then
                            comeansum = 0
                        End If
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvalue = "" Then
                        comeanvalue = comeansum / comeans

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    selectcol = ""
                    While dr.Read
                        If selectcol = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = 0
                            Else
                                selectcol = dr(columns(start)).ToString
                            End If
                            'selectcol = dr(columns(start))
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = selectcol & "," & 0
                            Else
                                selectcol = selectcol & "," & dr(columns(start)).ToString
                            End If
                            'selectcol = selectcol & "," & dr(columns(start))
                        End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcols2 = selectcol.Split(",")

                    Dim i, j As Integer
                    i = UBound(noofcols2)

                    For j = 0 To i
                        If xmean = "" Then
                            xmean = (noofcols2(j) - comeanvalue).ToString
                        Else
                            xmean = xmean & "," & (noofcols2(j) - comeanvalue).ToString
                        End If


                    Next
                    Dim xmeanarra = xmean.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarra)



                    For m = 0 To l
                        If sqtofxmean = "" Then
                            sqtofxmean = (xmeanarra(m) * xmeanarra(m)).ToString
                        Else
                            sqtofxmean = sqtofxmean & "," & (xmeanarra(m) * xmeanarra(m)).ToString
                        End If
                    Next
                    Dim sqtofxmeansum = sqtofxmean.Split(",")
                    Dim p, k As Integer
                    p = UBound(sqtofxmeansum)



                    For k = 0 To p

                        If meanxsum = "" Then
                            meanxsum = sqtofxmeansum(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanxsum, Double)

                            meanxsum = (sqtofxmeansum(k) + g).ToString
                        End If

                    Next
                End If
            Next
            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
            For start = 0 To noofcols
                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Mean Of Date")
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeansy = dr(columns(start))
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansumy = dr(columns(start))
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvaluey = "" Then
                        comeanvaluey = comeansumy / comeansy

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    selectcoly = ""
                    While dr.Read

                        If selectcoly = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = 0
                            Else
                                selectcoly = dr(columns(start)).ToString
                            End If
                            'selectcol = dr(columns(start))
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = selectcoly & "," & 0
                            Else
                                selectcoly = selectcoly & "," & dr(columns(start)).ToString
                            End If
                            'selectcol = selectcol & "," & dr(columns(start))
                        End If
                        'If selectcoly = "" Then
                        '    selectcoly = dr(columns(start))
                        'Else
                        '    selectcoly = selectcoly & "," & dr(columns(start))
                        'End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcolsy = selectcoly.Split(",")
                    Dim i, j As Integer
                    i = UBound(noofcolsy)

                    For j = 0 To i

                        If xmeany = "" Then
                            xmeany = (noofcolsy(j) - comeanvaluey).ToString
                        Else
                            xmeany = xmeany & "," & (noofcolsy(j) - comeanvaluey).ToString
                        End If


                    Next
                    Dim xmeanarray = xmeany.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarray)



                    For m = 0 To l
                        If sqtofxmeany = "" Then
                            sqtofxmeany = (xmeanarray(m) * xmeanarray(m)).ToString
                        Else
                            sqtofxmeany = sqtofxmeany & "," & (xmeanarray(m) * xmeanarray(m)).ToString
                        End If
                    Next
                    Dim xmeanarra = sqtofxmeany.Split(",")
                    Dim p, k As Integer
                    p = UBound(xmeanarra)



                    For k = 0 To p
                        ' Dim aj As String
                        If meanysum = "" Then
                            meanysum = xmeanarra(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanysum, Double)

                            meanysum = (xmeanarra(k) + g).ToString
                        End If

                    Next
                    Dim arrayxmean = xmean.Split(",")
                    Dim arrayymean = xmeany.Split(",")
                    Dim strt, strtt As Integer
                    strt = UBound(arrayxmean)


                    For strtt = 0 To strt
                        If multixy = "" Then
                            multixy = (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        Else
                            multixy = multixy & "," & (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        End If
                    Next
                    Dim sumxy = multixy.Split(",")

                    Dim loopx, loopy As Integer
                    loopx = UBound(sumxy)
                    For loopy = 0 To loopx
                        If sumofxy = "" Then
                            sumofxy = sumxy(loopy).ToString
                        Else
                            Dim h As Double
                            h = CType(sumofxy, Double)
                            sumofxy = (h + sumxy(loopy)).ToString
                        End If

                    Next
                    'If sumofxy = 0 Or meanysum = 0 Then
                    '    finalcorel = 0
                    'Else
                    '    finalcorel = sumofxy / meanysum
                    Dim chknow As String = ""
                    chknow = (sumofxy / Math.Sqrt((meanysum * meanxsum))).ToString
                    If chknow.Contains("NaN") Then
                        chknow = chknow.Replace("NaN", "0")

                    End If
                    'End If

                    If finalcorel = "" Then
                        finalcorel = "<b>" & columns(0) & "</b>" & "<b>" & " " & "With" & " " & "</b>" & "<b>" & columns(start) & "</b>" & "=" & chknow & "<br>"
                    Else
                        finalcorel = finalcorel & "<b>" & columns(0) & "</b>" & "<b>" & " " & "With" & " " & "</b>" & "<b>" & columns(start) & "</b>" & "=" & chknow & "<br>"
                    End If

                End If
                selectcoly = ""
                xmeany = ""
                sqtofxmeany = ""
                meanysum = ""
                multixy = ""
                sumofxy = ""
                comeanvaluey = ""

                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            Dim stst As String = ""
            stst = noofcols.ToString
            finalcorel = "<td colspan=" + stst + ">" & finalcorel
            finalcorel = finalcorel & "</tr>"

            result = result & finalcorel

            Session("maxval") = result
            ''''''''''''''''end'''''''''''''''
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If

        ''''''''''find regression value''''''''''''''
        '''''''''start''''''''''''''

        If regression.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "reg"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "reg"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            Dim comeans As String
            Dim comeansum As String
            Dim selectcol As String
            Dim noofcols2
            Dim comeanvalue As String = ""
            Dim xmean As String = ""
            Dim sqtofxmean As String = ""
            Dim meanxsum As String = ""
            Dim selectcoly As String = ""
            Dim xmeany As String = ""
            Dim sqtofxmeany As String = ""
            Dim meanysum As String = ""
            Dim multixy As String = ""
            Dim sumofxy As String = ""
            Dim comeansy As String = ""
            Dim comeansumy As String = ""
            Dim comeanvaluey As String = ""
            Dim finalcorel As String = ""
            Dim valuenow As String
            'If result = "" Then
            '    result = Me.reg.InnerText
            'Else
            '    result = result & "$" & Me.reg.InnerText
            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.reg.InnerText & "</td>"
            For start = 0 To 0


                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Regression Of Date")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeans = dr(columns(start)).ToString
                        If comeans = "" Then
                            comeans = 0
                        End If
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansum = dr(columns(start)).ToString
                        If comeansum = "" Then
                            comeansum = 0
                        End If
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvalue = "" Then
                        comeanvalue = comeansum / comeans

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    selectcol = ""
                    While dr.Read
                        If selectcol = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = 0
                            Else
                                selectcol = dr(columns(start)).ToString
                            End If
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcol = selectcol & "," & 0
                            Else
                                selectcol = selectcol & "," & dr(columns(start)).ToString
                            End If

                        End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcols2 = selectcol.Split(",")

                    Dim i, j As Integer
                    i = UBound(noofcols2)

                    For j = 0 To i
                        If xmean = "" Then
                            xmean = (noofcols2(j) - comeanvalue).ToString
                        Else
                            xmean = xmean & "," & (noofcols2(j) - comeanvalue).ToString
                        End If


                    Next
                    Dim xmeanarra = xmean.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarra)



                    For m = 0 To l
                        If sqtofxmean = "" Then
                            sqtofxmean = (xmeanarra(m) * xmeanarra(m)).ToString
                        Else
                            sqtofxmean = sqtofxmean & "," & (xmeanarra(m) * xmeanarra(m)).ToString
                        End If
                    Next
                    Dim sqtofxmeansum = sqtofxmean.Split(",")
                    Dim p, k As Integer
                    p = UBound(sqtofxmeansum)



                    For k = 0 To p

                        If meanxsum = "" Then
                            meanxsum = sqtofxmeansum(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanxsum, Double)

                            meanxsum = (sqtofxmeansum(k) + g).ToString
                        End If

                    Next
                End If
            Next
            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
            For start = 0 To noofcols
                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Regression Of Date")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeansy = dr(columns(start)).ToString
                        If comeansy = "" Then
                            comeansy = 0
                        End If
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansumy = dr(columns(start)).ToString
                        If comeansumy = "" Then
                            comeansumy = 0
                        End If
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvaluey = "" Then
                        comeanvaluey = comeansumy / comeansy

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    selectcoly = ""
                    While dr.Read

                        If selectcoly = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = 0
                            Else
                                selectcoly = dr(columns(start)).ToString
                            End If
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                selectcoly = selectcoly & "," & 0
                            Else
                                selectcoly = selectcoly & "," & dr(columns(start)).ToString
                            End If

                        End If
                        'If selectcoly = " " Then
                        '    selectcoly = dr(columns(start))
                        'Else
                        '    selectcoly = selectcoly & "," & dr(columns(start))
                        'End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcolsy = selectcoly.Split(",")
                    Dim i, j As Integer
                    i = UBound(noofcolsy)

                    For j = 0 To i

                        If xmeany = "" Then
                            xmeany = (noofcolsy(j) - comeanvaluey).ToString
                        Else
                            xmeany = xmeany & "," & (noofcolsy(j) - comeanvaluey).ToString
                        End If


                    Next
                    Dim xmeanarray = xmeany.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarray)



                    For m = 0 To l
                        If sqtofxmeany = "" Then
                            sqtofxmeany = (xmeanarray(m) * xmeanarray(m)).ToString
                        Else
                            sqtofxmeany = sqtofxmeany & "," & (xmeanarray(m) * xmeanarray(m)).ToString
                        End If
                    Next
                    Dim xmeanarra = sqtofxmeany.Split(",")
                    Dim p, k As Integer
                    p = UBound(xmeanarra)



                    For k = 0 To p

                        If meanysum = "" Then
                            meanysum = xmeanarra(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanysum, Double)

                            meanysum = (xmeanarra(k) + g).ToString
                        End If

                    Next
                    Dim arrayxmean = xmean.Split(",")
                    Dim arrayymean = xmeany.Split(",")
                    Dim strt, strtt As Integer
                    strt = UBound(arrayxmean)


                    For strtt = 0 To strt
                        If multixy = "" Then
                            multixy = (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        Else
                            multixy = multixy & "," & (arrayxmean(strtt) * arrayymean(strtt)).ToString
                        End If
                    Next
                    Dim sumxy = multixy.Split(",")

                    Dim loopx, loopy As Integer
                    loopx = UBound(sumxy)
                    For loopy = 0 To loopx
                        If sumofxy = "" Then
                            sumofxy = sumxy(loopy).ToString
                        Else
                            Dim h As Double
                            h = CType(sumofxy, Double)
                            sumofxy = (h + sumxy(loopy)).ToString
                        End If

                    Next

                    'If sumofxy = 0 Or meanysum = 0 Then
                    '    finalcorel = 0
                    'Else
                    '    finalcorel = sumofxy / meanysum
                    'End If

                    finalcorel = sumofxy / meanysum
                    Dim xony As String
                    xony = ((finalcorel * (-comeanvaluey)) + comeanvalue).ToString
                    If xony.Contains("NaN") Then
                        xony = xony.Replace("NaN", "0")
                    End If
                    Dim ins, klj As Double
                    ins = CType(xony, Integer)
                    If ins > -1 Then
                        xony = "+" & ins.ToString
                    Else
                        xony = ins.ToString
                    End If
                    Dim yonx As String
                    yonx = ((finalcorel * (comeanvalue)) + -comeanvaluey).ToString
                    If yonx.Contains("NaN") Then
                        yonx = yonx.Replace("NaN", "0")
                    End If
                    klj = CType(yonx, Double)
                    If klj > -1 Then
                        yonx = "+" & klj.ToString
                    Else
                        yonx = klj.ToString
                    End If
                    valuenow = valuenow & "<b>" & columns(0) & "</b>" & "=" & "<b>" & columns(start) & "</b>" & xony & "<br>" & "<b>" & columns(start) & "</b>" & "=" & "<b>" & columns(0) & "</b>" & yonx & "<br>"


                End If
                selectcoly = ""
                xmeany = ""
                sqtofxmeany = ""
                meanysum = ""
                multixy = ""
                sumofxy = ""
                comeanvaluey = ""

                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            Dim stst As String = ""
            stst = noofcols.ToString
            valuenow = "<td colspan=" + stst + ">" & valuenow
            valuenow = valuenow & "</tr>"
            result = result & valuenow

            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If
        ''''''''''''''''end'''''''''''''''

        ''''''''''find standard deviation value''''''''''''''
        '''''''''start''''''''''''''
        If standarddeviation.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "stan"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "stan"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            'Dim strhtml As String = ""
            'For start = 0 To noofcols

            '    If strhtml = "" Then
            '        strhtml = "<table><tr><td style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    Else
            '        strhtml = strhtml & "<td  style=" + "color:black><b>" & (columns(start)) & "</b></td><td></td>"
            '    End If

            'Next
            'strhtml = strhtml & "</tr>"
            Dim comeans As String = ""
            Dim comeansum As String = ""
            Dim comeanvalue As String = ""
            Dim selectcol As String = ""
            Dim noofcols2
            Dim xmean As String = ""
            Dim sqtofxmean As String = ""
            Dim meanxsum As String = ""
            Dim deviationvalue As String = ""
            'If result = "" Then
            '    result = Me.deviation.InnerText
            'Else
            '    result = result & "$" & Me.deviation.InnerText
            'End If
            result = result & "<tr>"
            result = result & "<td>" & Me.deviation.InnerText & "</td>"
            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")

                End If

                con.Close()
                dr.Close()

                If datatype = "datetime" Then
                    aspnet_msgbox("You Cannot Find Standard Deviation Of Date")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                Else
                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try
                    cmdvalue = New SqlCommand("select isnull(count(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    If dr.Read Then
                        comeans = dr(columns(start))
                    End If


                    con.Close()
                    dr.Close()


                    cmdvalue = New SqlCommand("select isnull(sum(" + (columns(start)) + "),0) as " + (columns(start)) + " from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        comeansum = dr(columns(start))
                    End If


                    con.Close()

                    dr.Close()
                    If comeanvalue = "" Then
                        comeanvalue = comeansum / comeans

                    End If
                    cmdvalue = New SqlCommand("select " + (columns(start)) + "  from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    selectcol = ""
                    While dr.Read
                        If selectcol = "" Then
                            selectcol = dr(columns(start))
                            'Dim stras As String = ""
                            'stras = dr(columns(start))
                        Else
                            selectcol = selectcol & "," & dr(columns(start))
                            'Dim stras As String = ""
                            'stras = dr(columns(start))
                        End If
                    End While
                    con.Close()
                    dr.Close()
                    noofcols2 = selectcol.Split(",")

                    Dim i, j As Integer
                    i = UBound(noofcols2)

                    For j = 0 To i
                        If xmean = "" Then
                            If noofcols2(j) = "" Then
                                noofcols2(j) = 0
                            End If
                            xmean = (noofcols2(j) - comeanvalue).ToString
                        Else
                            If noofcols2(j) = "" Then
                                noofcols2(j) = 0
                            End If
                            xmean = xmean & "," & (noofcols2(j) - comeanvalue).ToString
                        End If


                    Next
                    Dim xmeanarra = xmean.Split(",")
                    Dim l, m As Integer
                    l = UBound(xmeanarra)



                    For m = 0 To l
                        If sqtofxmean = "" Then
                            sqtofxmean = (xmeanarra(m) * xmeanarra(m)).ToString
                        Else
                            sqtofxmean = sqtofxmean & "," & (xmeanarra(m) * xmeanarra(m)).ToString
                        End If
                    Next
                    Dim sqtofxmeansum = sqtofxmean.Split(",")
                    Dim p, k As Integer
                    p = UBound(sqtofxmeansum)



                    For k = 0 To p

                        If meanxsum = "" Then
                            meanxsum = sqtofxmeansum(k).ToString
                        Else
                            Dim g As Double
                            g = CType(meanxsum, Double)

                            meanxsum = (sqtofxmeansum(k) + g).ToString
                        End If

                    Next
                    Dim deviationvalue1 As String = ""
                    If deviationvalue1 = "" Then
                        deviationvalue1 = Math.Sqrt(meanxsum / comeans).ToString
                        If deviationvalue1.Contains("NaN") Then
                            deviationvalue1 = deviationvalue1.Replace("NaN", 0)

                        End If
                    Else
                        deviationvalue1 = deviationvalue1 & "," & Math.Sqrt(meanxsum / comeans).ToString
                        If deviationvalue1.Contains("NaN") Then
                            deviationvalue1 = deviationvalue1.Replace("NaN", 0)

                        End If
                    End If
                    If deviationvalue = "" Then
                        deviationvalue = "<td style=" + "color:black;>" & deviationvalue1 & "</td>" '"<b>" & columns(start) & "</b>" & "=" & Math.Sqrt(meanxsum / comeans).ToString & "<br>"
                    Else
                        deviationvalue = deviationvalue & "<td style=" + "color:black;>" & deviationvalue1 & "</td>" '"<b>" & columns(start) & "</b>" & "=" & Math.Sqrt(meanxsum / comeans).ToString & "<br>"

                    End If

                End If
                cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                comeans = ""
                comeansum = ""
                comeanvalue = ""
                selectcol = ""

                xmean = ""
                sqtofxmean = ""
                meanxsum = ""

            Next
            deviationvalue = deviationvalue & "</tr>"
            'deviationvalue = strhtml & deviationvalue & "</table>"
            result = result & deviationvalue

            Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If


        result = result & "</table>"
        Session("maxval") = result



        ''''''''''''''''end'''''''''''''''


        ''''''''''find column percentage value''''''''''''''
        '''''''''start''''''''''''''

        If columnpercentage.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "col"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "col"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            result1 = result1 & "yes"
            Session("maxval1") = result1
            check()


            Dim sumtotal As String = ""
            Dim allcolumn As String = ""
            Dim singlcolumn1 As String = ""
            Dim singlcolumn As String = ""
            Dim rowsumfinal As Double
            Dim firstcolumn As Double
            Dim totalsum As Double
            Dim sum As String = ""
            Dim obj
            Dim i, j, k, l As Integer
            ' If result = "" Then
            'result = Me.clmper.InnerText
            ' Else
            'result = result & "$" & Me.clmper.InnerText


            'End If



            For start = 0 To noofcols
                Dim ss As String
                ss = ""
                singlcolumn = ""
                Dim fd As String
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then
                    'If allcolumn = "" Then

                    allcolumn = columns(start)
                    ' Else

                    ' allcolumn = allcolumn & "+" & columns(start)
                    'End If
                Else
                    aspnet_msgbox("Select Only Numeric Data For Column Percentage ")

                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End If



                Try

                    cmdvalue = New SqlCommand("select isnull(sum(Convert(numeric, " + allcolumn + ")),0) as total from " + table + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        'If sumtotal = "" Then
                        sumtotal = dr("total").ToString
                        ' Else
                        '  sumtotal = sumtotal & "," & dr("total").ToString
                        'End If

                    End If
                    con.Close()
                    dr.Close()

                Catch ex As Exception

                    aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End Try

                cmdvalue = New SqlCommand("select Convert(numeric," + (columns(start)) + ") as " + (columns(start)) + "   from  " + table + "", con)
                con.Open()
                dr = cmdvalue.ExecuteReader
                Dim flow, flow1, iii As Int32
                Dim dd As String

                While dr.Read

                    If singlcolumn = "" Then
                        singlcolumn = dr(columns(start)).ToString
                        If singlcolumn = "" Then
                            singlcolumn = "0"
                        End If

                        firstcolumn = CType(singlcolumn, Double)
                        totalsum = CType(sumtotal, Double)
                        rowsumfinal = firstcolumn * 100 / totalsum
                        'flow = rowsumfinal

                        dd = CType(rowsumfinal, String)
                        If dd.Contains("NaN") Then
                            dd = dd.Replace("NaN", "0")
                        End If
                        flow = dd.LastIndexOf(".")
                        flow1 = dd.Length
                        flow = flow + 1
                        iii = flow1 - flow
                        If iii > 4 Then
                            flow = flow + 4
                            dd = dd.Substring(0, flow)

                        End If


                        If ss = "" Then
                            singlcolumn = dd '"<b>" & columns(start) & "</b>" & "=" & 
                            ss = columns(start)
                        Else
                            singlcolumn = dd
                            ss = columns(start)
                        End If


                    Else
                        singlcolumn1 = dr(columns(start)).ToString
                        If singlcolumn1 = "" Then
                            singlcolumn1 = 0
                        End If
                        firstcolumn = CType(singlcolumn1, Double)
                        totalsum = CType(sumtotal, Double)
                        rowsumfinal = firstcolumn * 100 / totalsum


                        dd = CType(rowsumfinal, String)
                        If dd.Contains("NaN") Then
                            dd = dd.Replace("NaN", "0")
                        End If
                        flow = dd.LastIndexOf(".")
                        flow1 = dd.Length
                        flow = flow + 1
                        iii = flow1 - flow
                        If iii > 4 Then
                            flow = flow + 4
                            dd = dd.Substring(0, flow)

                        End If

                        If ss = "" Then
                            singlcolumn = singlcolumn & "," & dd
                            ss = columns(start)
                        Else
                            singlcolumn = singlcolumn & "," & dd
                            ss = columns(start)
                        End If



                    End If
                End While
                con.Close()
                dr.Close()

                cmdvalue = New SqlCommand("alter table " + table + "  add  Column_Percentage_" + columns(start) + " real", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                Dim arrays = singlcolumn.Split(",")
                Dim bond, sond As Integer
                bond = UBound(arrays)
                For sond = 0 To bond
                    Dim nows As String
                    nows = (sond + 1).ToString
                    cmdvalue = New SqlCommand("update " + table + "  set Column_Percentage_" + columns(start) + " =" + arrays(sond) + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()

                Next


            Next
            ' Next
            'result = result & "$" & singlcolumn

            ' Session("maxval") = result
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"

        End If
        ''''''''''''''''''end''''''''''''''''''''''''
        ''''''''''find row percentage value''''''''''''''
        '''''''''start''''''''''''''
        If Me.rowpercentage.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "row"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "row"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            result1 = result1 & "yes"
            Session("maxval1") = result1
            Dim allrows, allrows1 As String
            Dim addrow, curentrow As String
            Dim newint, loops As Integer
            Dim i, j, k As Double
            Dim finalvalue As String
            'If result = "" Then
            '    result = Me.rows.InnerText
            'Else
            '    result = result & "$" & Me.rows.InnerText


            'End If

            cmdvalue = New SqlCommand("select * from  " + table + "", con)
            con.Open()



            Dim datasetaa As New DataSet

            da.SelectCommand = cmdvalue
            da.Fill(datasetaa)

            con.Close()
            Dim culmnaaa As DataColumn
            Dim strclocheck As String
            For Each culmnaaa In datasetaa.Tables(0).Columns


                strclocheck = culmnaaa.ColumnName()
                If strclocheck.StartsWith("Row_Percentage_") Then
                    cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                ElseIf strclocheck.StartsWith("Row_Sum_") Then
                    cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()

                End If




            Next


            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then
                    If allrows = "" Then
                        allrows1 = "sum" & "(" & columns(start) & ")"
                        allrows1 = columns(start)
                        allrows = " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)
                    Else
                        allrows1 = allrows1.Replace("sum", "")
                        allrows1 = allrows1.Replace("(", "")
                        allrows1 = allrows1.Replace(")", "")
                        allrows1 = allrows1 & "+" & columns(start)
                        allrows = allrows & "," & " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)
                    End If
                Else
                    aspnet_msgbox("Select Only Numeric Data For Row  Percentage")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End If

            Next
            Try
                cmdvalue = New SqlCommand("select Identity(int, 1,1) as nocount, " + allrows + "  into " + tableperform + " from " + table + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            Catch ex As Exception


                aspnet_msgbox("column" & " " & "Is Not Numeric")
                strClose = "<Script language='Javascript'>"
                strClose = strClose + "window.close()"
                strClose = strClose + "</Script>"
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                Exit Sub
            End Try
            Dim counts As String
            cmdvalue = New SqlCommand("SELECT isnull(count(*),0) as number from " + tableperform + "", con)
            con.Open()
            dr = cmdvalue.ExecuteReader

            If dr.Read Then

                counts = dr("number")
            End If
            con.Close()
            dr.Close()

            newint = CType(counts, Integer)
            Dim pp As String
            Dim strr, strr1 As String
            Dim lops As String
            For loops = 1 To newint

                lops = CType(loops, String)
                pp = ""

                cmdvalue = New SqlCommand("SELECT " + allrows1 + " as totals FROM " + tableperform + " WHERE nocount = " + lops + "", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    If addrow = "" Then
                        Dim sumvalnow As String = ""
                        sumvalnow = dr("totals").ToString
                        If sumvalnow = "" Then
                            addrow = 0
                        Else
                            addrow = dr("totals").ToString
                        End If
                    Else
                        Dim sumvalnow As String = ""
                        sumvalnow = dr("totals").ToString
                        If sumvalnow = "" Then
                            addrow = addrow & "," & 0
                        Else
                            addrow = addrow & "," & dr("totals").ToString
                        End If

                    End If

                End If
                con.Close()
                dr.Close()
            Next
            Dim obj
            Dim sum, singlcolumn1, singlcolumn As String
            ' Dim i, j, k, l, m As Double
            obj = addrow.Split(",")
            cmdvalue = New SqlCommand("alter table " + table + "  add Row_Sum_ real", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
            For loops = 0 To newint - 1
                Dim nows As String
                nows = (loops + 1).ToString
                cmdvalue = New SqlCommand("update " + table + "  set Row_Sum_=" + obj(loops) + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            For start = 0 To noofcols

                For loops = 1 To newint

                    lops = CType(loops, String)

                    cmdvalue = New SqlCommand("SELECT " + columns(start) + " as curent FROM " + tableperform + " WHERE nocount ='" + lops + "'", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader

                    If dr.Read Then
                        curentrow = dr("curent").ToString
                        If curentrow = "" Then
                            curentrow = 0
                        End If
                    End If

                    k = UBound(obj)
                    ' i = CType(addrow, Double)
                    j = CType(curentrow, Double)
                    If finalvalue = "" Then
                        finalvalue = curentrow * 100 / obj(loops - 1)
                        If finalvalue.Contains("NaN") Then
                            finalvalue = finalvalue.Replace("NaN", "0")

                        End If
                    Else
                        finalvalue = finalvalue & "," & curentrow * 100 / obj(loops - 1)
                        If finalvalue.Contains("NaN") Then
                            finalvalue = finalvalue.Replace("NaN", "0")

                        End If
                    End If


                    con.Close()
                    dr.Close()





                Next
                cmdvalue = New SqlCommand("alter table " + table + "  add Row_Percentage_" + columns(start) + " real", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

                Dim arrays = finalvalue.Split(",")
                Dim bond, sond As Integer
                bond = UBound(arrays)
                For sond = 0 To bond
                    Dim nows As String
                    nows = (sond + 1).ToString
                    cmdvalue = New SqlCommand("update " + table + "  set Row_Percentage_" + columns(start) + "=" + arrays(sond) + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()

                Next

                finalvalue = ""



            Next


            'result = result & "$" & finalvalue

            'Session("maxval") = result
            '' delate table
            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"

        End If




        ''''''''''''''''end'''''''''''''''


        ''''''''''find column sum percentage value''''''''''''''
        '''''''''start''''''''''''''

        If columnsumpercentage.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "colp"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "colp"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            result1 = result1 & "yes"
            Session("maxval1") = result1
            Dim allrows, allrows1 As String
            Dim addrow, curentrow As String
            Dim newint, loops As Integer
            'If result = "" Then
            '    result = Me.columnsum.InnerText
            'Else
            '    result = result & "$" & Me.columnsum.InnerText


            'End If

            cmdvalue = New SqlCommand("select * from  " + table + "", con)
            con.Open()



            Dim datasetaa As New DataSet

            da.SelectCommand = cmdvalue
            da.Fill(datasetaa)

            con.Close()
            Dim culmnaaa As DataColumn
            Dim strclocheck As String
            For Each culmnaaa In datasetaa.Tables(0).Columns


                strclocheck = culmnaaa.ColumnName()
                If strclocheck.StartsWith("Column_Sum_Percentage") Then
                    cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                ElseIf strclocheck.StartsWith("Column_Sum") Then
                    cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()

                End If




            Next


            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''

                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then
                    If allrows = "" Then
                        allrows1 = "sum" & "(" & columns(start) & ")"
                        allrows1 = columns(start)
                        allrows = " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)
                    Else
                        allrows1 = allrows1.Replace("sum", "")
                        allrows1 = allrows1.Replace("(", "")
                        allrows1 = allrows1.Replace(")", "")
                        allrows1 = allrows1 & "+" & columns(start)
                        allrows = allrows & "," & " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)
                    End If
                Else
                    aspnet_msgbox("Select Only Numeric Data For Column Sum Percentage")
                    strClose = "<Script language='Javascript'>"
                    strClose = strClose + "window.close()"
                    strClose = strClose + "</Script>"
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                    Exit Sub
                End If

            Next
            Try
                cmdvalue = New SqlCommand("select Identity(int, 1,1) as nocount, " + allrows + "  into " + tableperform + " from " + table + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            Catch ex As Exception


                aspnet_msgbox("column" & " " & "Is Not Numeric")
                strClose = "<Script language='Javascript'>"
                strClose = strClose + "window.close()"
                strClose = strClose + "</Script>"
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                Exit Sub
            End Try
            Dim counts As String
            cmdvalue = New SqlCommand("SELECT isnull(count(*),0) as number from " + tableperform + "", con)
            con.Open()
            dr = cmdvalue.ExecuteReader

            If dr.Read Then

                counts = dr("number")
            End If
            con.Close()
            dr.Close()

            newint = CType(counts, Integer)
            Dim pp As String
            Dim strr, strr1 As String
            Dim lops As String
            For loops = 1 To newint

                lops = CType(loops, String)
                ' pp = ""
                cmdvalue = New SqlCommand("SELECT " + allrows1 + " as totals FROM " + tableperform + " WHERE nocount = " + lops + "", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    If addrow = "" Then


                        Dim sumvalnow As String = ""
                        sumvalnow = dr("totals").ToString
                        If sumvalnow = "" Then
                            addrow = 0
                        Else
                            addrow = dr("totals").ToString
                        End If

                    Else
                        Dim sumvalnow As String = ""
                        sumvalnow = dr("totals").ToString
                        If sumvalnow = "" Then
                            addrow = addrow & "," & 0
                        Else
                            addrow = addrow & "," & dr("totals").ToString
                        End If


                    End If

                End If
                con.Close()
                dr.Close()
            Next
            Dim obj
            Dim sum, singlcolumn1, singlcolumn As String
            Dim i, j, k, l, m As Double
            obj = addrow.Split(",")

            i = UBound(obj)
            For j = 0 To i
                If sum = "" Then
                    sum = obj(j)
                Else
                    Dim db As Double
                    db = CType(sum, Double)

                    sum = db + obj(j)

                End If
            Next

            For k = 0 To i
                Dim sumpercentage, valueof As Double
                sumpercentage = CType(sum, Double)
                valueof = obj(k) * 100 / sumpercentage
                If singlcolumn1 = "" Then
                    singlcolumn1 = valueof.ToString
                    If singlcolumn1.Contains("NaN") Then
                        singlcolumn1 = singlcolumn1.Replace("NaN", "0")

                    End If
                Else
                    singlcolumn1 = singlcolumn1 & "," & valueof.ToString
                    If singlcolumn1.Contains("NaN") Then
                        singlcolumn1 = singlcolumn1.Replace("NaN", "0")

                    End If

                End If
            Next
            Dim vaal = singlcolumn1.Split(",")
            Dim row As Integer
            Dim int As Integer = 1


            cmdvalue = New SqlCommand("alter table " + table + "  add  Column_Sum real", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()


            cmdvalue = New SqlCommand("alter table " + table + "  add  Column_Sum_Percentage real", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()

            Dim arrays = singlcolumn1.Split(",")
            Dim bond, sond As Integer
            bond = UBound(arrays)
            For sond = 0 To i
                Dim nows As String
                nows = (sond + 1).ToString
                cmdvalue = New SqlCommand("update " + table + "  set Column_Sum=" + obj(sond) + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next
            For sond = 0 To bond
                Dim nows As String
                nows = (sond + 1).ToString
                cmdvalue = New SqlCommand("update " + table + "  set Column_Sum_Percentage=" + arrays(sond) + "", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            Next








            For row = 0 To newint - 1


                Dim forcolumn = ""

                If singlcolumn = "" Then
                    singlcolumn = "<b>" & "Row" & " " & int & "</b>" & "=" & vaal(row).ToString & "<br>"

                Else
                    singlcolumn = singlcolumn & "<b>" & "Row" & " " & int & "</b>" & "=" & vaal(row).ToString & "<br>"
                End If
                int = int + 1
            Next







            'result = result & "$" & singlcolumn

            'Session("maxval") = result

            cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
            con.Open()
            cmdvalue.ExecuteNonQuery()
            con.Close()
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"

        End If



        ''''''''''''''''end'''''''''''''''




        ''''''''''find accumulatedsum value''''''''''''''
        '''''''''start''''''''''''''

        If accumulatedsum.Checked = True Then
            If Session("Checkforreportformilas") = "" Then
                Session("Checkforreportformilas") = "acc"
            Else
                Session("Checkforreportformilas") = Session("Checkforreportformilas") + "," + "acc"
            End If
            Session("saveanalysis") = Session("saveanalysis") & "," & "yes"
            result1 = result1 & "yes"
            Session("maxval1") = result1

            cmdvalue = New SqlCommand("select * from  " + table + "", con)
            con.Open()



            Dim datasetaa As New DataSet

            da.SelectCommand = cmdvalue
            da.Fill(datasetaa)

            con.Close()
            Dim culmnaaa As DataColumn
            Dim strclocheck As String
            For Each culmnaaa In datasetaa.Tables(0).Columns


                strclocheck = culmnaaa.ColumnName()
                If strclocheck.StartsWith("Accu_Sum") Then
                    cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                End If




            Next






            Dim accumlate As String = ""
            Dim accumlate1 As Double
            Dim accustr As String
            'If result = "" Then
            '    result = Me.accumulate.InnerText
            'Else
            '    result = result & "$" & Me.accumulate.InnerText


            'End If



            For start = 0 To noofcols
                '''''''''checking vaues data type we check only for numeric, float, varchar and datetime becouse our autowhiz provide the facility only for these datatypes''''''''
                accumlate = ""
                cmdvalue = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader

                If dr.Read Then
                    datatype = dr("type")
                End If
                con.Close()
                dr.Close()
                If datatype <> "datetime" Then

                    Try
                        cmdvalue = New SqlCommand("select (Convert(numeric," + (columns(start)) + ")) as " + (columns(start)) + " into " + tableperform + "  from " + table + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")
                        strClose = "<Script language='Javascript'>"
                        strClose = strClose + "window.close()"
                        strClose = strClose + "</Script>"
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
                        Exit Sub
                    End Try

                    cmdvalue = New SqlCommand("select " + (columns(start)) + " as " + (columns(start)) + "   from " + tableperform + "", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader


                    While dr.Read
                        If accumlate = "" Then
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)) '.ToString
                            If valuecheck = "" Then
                                accumlate = 0
                            Else
                                accumlate = dr(columns(start))
                            End If
                            accumlate1 = CType(accumlate, Double)
                            accustr = accumlate1.ToString
                        Else
                            Dim valuecheck As String = ""
                            valuecheck = dr(columns(start)).ToString
                            If valuecheck = "" Then
                                accumlate = 0
                            Else
                                accumlate = dr(columns(start))
                            End If
                            ' accumlate = dr(columns(start))
                            Dim nww As Double
                            nww = CType(accumlate, Double)

                            Dim first As String
                            first = (accumlate1 + nww) '.ToString

                            ' accustr = accustr & "," & (accumlate1 + nww).ToString

                            accustr = accustr & "," & first
                            accumlate1 = CType(first, Integer)

                        End If


                    End While

                    con.Close()
                    dr.Close()

                    cmdvalue = New SqlCommand("alter table " + table + "  add  Accu_Sum" + columns(start) + " real ", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()





                    Dim arrays = accustr.Split(",")
                    Dim bond, sond As Integer
                    bond = UBound(arrays)
                    For sond = 0 To bond
                        Dim nows As String
                        nows = (sond + 1).ToString
                        cmdvalue = New SqlCommand("update " + table + "  set Accu_Sum" + columns(start) + " =" + arrays(sond) + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()

                    Next








                    Dim sttp As String
                    If sttp = "" Then
                        result = result & "$" & "<b>" & columns(start) & "</b>" & "=" & accustr & "<br>"
                        sttp = result
                    Else
                        result = result & " " & " " & "<b>" & columns(start) & "</b>" & "=" & accustr & "<br>"
                        sttp = result
                    End If
                    accustr = ""
                    accumlate = ""

                    cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()


                Else
                    cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
                    con.Open()
                    dr = cmdvalue.ExecuteReader
                    While dr.Read()
                        If dr("name") = tableperform Then
                            b = False
                            Exit While

                        Else
                            b = True
                        End If
                    End While
                    dr.Close()
                    con.Close()

                    If b = False Then


                        cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                        con.Open()
                        cmdvalue.ExecuteNonQuery()
                        con.Close()
                    End If
                    aspnet_msgbox(columns(start) & " " & "column" & " " & "Is Not Numeric")


                End If



                cmdvalue = New SqlCommand("select name from sysobjects where xtype='u'", con)
                con.Open()
                dr = cmdvalue.ExecuteReader
                While dr.Read()
                    If dr("name") = tableperform Then
                        b = False
                        Exit While

                    Else
                        b = True
                    End If
                End While
                dr.Close()
                con.Close()

                If b = False Then


                    cmdvalue = New SqlCommand("drop table " + tableperform + "", con)
                    con.Open()
                    cmdvalue.ExecuteNonQuery()
                    con.Close()
                End If





            Next
        Else
            Session("saveanalysis") = Session("saveanalysis") & "," & "no"
        End If
        ' Session("maxval") = result


        ''''''''''''''''end'''''''''''''''


        ''''''''''insert into that table which formulas on which report  has been calculated''''''''''''''
        '''''''''start''''''''''''''
        If Session("analysis") = "" Then


            Dim Regressionx As String = ""
            Dim comparesamplesx As String = ""
            Dim Columnpercentagex As String = ""
            'Dim FilterPercentagex As String = ""
            Dim Meanx As String = ""
            Dim Modex As String = ""
            Dim Standarddeviationx As String = ""
            Dim RowSumPercentagex As String = ""
            Dim AccumulatedSumx As String = ""
            Dim Maxs As String = ""
            Dim Averages As String = ""
            Dim Countsx As String = ""
            Dim Mins As String = ""
            Dim ColumnSumPercentagex As String = ""
            Dim Standarderrorx As String = ""
            Dim Rangex As String = ""
            Dim Medianx As String = ""

            'Dim Indexs As String = ""
            Dim NonWeightedNumberx As String = ""
            Dim RowPercentagex As String = ""
            Dim Correlationx As String = ""
            Dim reportname As String = ""
            reportname = Session("repname")
            Dim repselect As String = ""
            cmdvalue = New SqlCommand("SELECT reportname FROM analysisformula where reportname = '" + reportname + "' and savedby='" + Session("userid") + "'", con)
            con.Open()
            dr = cmdvalue.ExecuteReader

            If dr.Read Then
                repselect = dr("reportname").ToString
            End If
            con.Close()
            dr.Close()





            If Me.columnpercentage.Checked = True Then
                Columnpercentagex = "yes"
            Else
                Columnpercentagex = "no"
            End If
            If Me.standarderror.Checked = True Then
                Standarderrorx = "yes"
            Else
                Standarderrorx = "no"
            End If
            'If Me.filterpercentage.Checked = True Then
            '    FilterPercentagex = "yes"
            'Else
            '    FilterPercentagex = "no"
            'End If
            If Me.rowpercentage.Checked = True Then
                RowPercentagex = "yes"
            Else
                RowPercentagex = "no"
            End If
            If standarddeviation.Checked = True Then
                Standarddeviationx = "yes"
            Else
                Standarddeviationx = "no"
            End If
            If correlation.Checked = True Then
                Correlationx = "yes"
            Else
                Correlationx = "no"
            End If
            If regression.Checked = True Then
                Regressionx = "yes"
            Else
                Regressionx = "no"
            End If
            If Max.Checked = True Then
                Maxs = "yes"
            Else
                Maxs = "no"
            End If
            If Min.Checked = True Then
                Mins = "yes"
            Else
                Mins = "no"
            End If
            If average.Checked = True Then
                Averages = "yes"
            Else
                Averages = "no"
            End If
            If count.Checked = True Then
                Countsx = "yes"
            Else
                Countsx = "no"
            End If
            If mean.Checked = True Then
                Meanx = "yes"
            Else
                Meanx = "no"
            End If
            If mode.Checked = True Then
                Modex = "yes"
            Else
                Modex = "no"
            End If
            If range.Checked = True Then
                Rangex = "yes"
            Else
                Rangex = "no"
            End If
            If accumulatedsum.Checked = True Then
                AccumulatedSumx = "yes"
            Else
                AccumulatedSumx = "no"
            End If
            If median.Checked = True Then
                Medianx = "yes"
            Else
                Medianx = "no"
            End If
            If rowsumpercentage.Checked = True Then
                RowSumPercentagex = "yes"
            Else
                RowSumPercentagex = "no"
            End If
            If columnsumpercentage.Checked = True Then
                ColumnSumPercentagex = "yes"
            Else
                ColumnSumPercentagex = "no"
            End If
            If comparesamples.Checked = True Then
                comparesamplesx = "yes"
            Else
                comparesamplesx = "no"
            End If
            If nonweightednumber.Checked = True Then
                NonWeightedNumberx = "yes"
            Else
                NonWeightedNumberx = "no"
            End If
            'If index.Checked = True Then
            '    Indexs = "yes"
            'Else
            '    Indexs = "no"
            'End If
            If repselect = reportname Then
                cmdvalue = New SqlCommand("update analysisformula set Regression ='" + Regressionx + "', comparesamples = '" + comparesamplesx + "', Columnpercentage = '" + Columnpercentagex + "',Mean = '" + Meanx + "', Mode = '" + Modex + "', standarddeviation = '" + Standarddeviationx + "', RowSumPercentage = '" + RowSumPercentagex + "', AccumulatedSum = '" + AccumulatedSumx + "', Maxs = '" + Maxs + "', Average = '" + Averages + "', Counts = '" + Countsx + "', Mins = '" + Mins + "', ColumnSumPercentage = '" + ColumnSumPercentagex + "', Standarderror = '" + Standarderrorx + "', Range = '" + Rangex + "',Median = '" + Medianx + "',NonWeightedNumber = '" + NonWeightedNumberx + "', RowPercentage = '" + RowPercentagex + "', correlation = '" + Correlationx + "' where reportname = '" + reportname + "' and savedby='" + Session("userid") + "'", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                'cmdvalue = New SqlCommand("update analysisformula set Regression ='" + Regressionx + "', comparesamples = '" + comparesamplesx + "', Columnpercentage = '" + Columnpercentagex + "', FilterPercentage = '" + FilterPercentagex + "', Mean = '" + Meanx + "', Mode = '" + Modex + "', standarddeviation = '" + Standarddeviationx + "', RowSumPercentage = '" + RowSumPercentagex + "', AccumulatedSum = '" + AccumulatedSumx + "', Maxs = '" + Maxs + "', Average = '" + Averages + "', Counts = '" + Countsx + "', Mins = '" + Mins + "', ColumnSumPercentage = '" + ColumnSumPercentagex + "', Standarderror = '" + Standarderrorx + "', Range = '" + Rangex + "',Median = '" + Medianx + "', Indexs = '" + Indexs + "', NonWeightedNumber = '" + NonWeightedNumberx + "', RowPercentage = '" + RowPercentagex + "', correlation = '" + Correlationx + "' where reportname = '" + reportname + "'", con)
                'con.Open()
                'cmdvalue.ExecuteNonQuery()
                'con.Close()

            Else


                cmdvalue = New SqlCommand("insert into analysisformula values('" + Regressionx + "','" + comparesamplesx + "','" + Columnpercentagex + "','" + Meanx + "','" + Modex + "','" + Standarddeviationx + "','" + RowSumPercentagex + "','" + AccumulatedSumx + "','" + Maxs + "','" + Averages + "','" + Countsx + "','" + Mins + "','" + ColumnSumPercentagex + "','" + Standarderrorx + "','" + Rangex + "','" + Medianx + "','" + NonWeightedNumberx + "','" + RowPercentagex + "','" + Correlationx + "','" + reportname + "','" + Session("userid") + "')", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
                'cmdvalue = New SqlCommand("insert into analysisformula values('" + Regressionx + "','" + comparesamplesx + "','" + Columnpercentagex + "','" + FilterPercentagex + "','" + Meanx + "','" + Modex + "','" + Standarddeviationx + "','" + RowSumPercentagex + "','" + AccumulatedSumx + "','" + Maxs + "','" + Averages + "','" + Countsx + "','" + Mins + "','" + ColumnSumPercentagex + "','" + Standarderrorx + "','" + Rangex + "','" + Medianx + "','" + Indexs + "','" + NonWeightedNumberx + "','" + RowPercentagex + "','" + Correlationx + "','" + reportname + "')", con)
                'con.Open()
                'cmdvalue.ExecuteNonQuery()
                'con.Close()
            End If
        End If
        ''''''''''''''''end'''''''''''''''
        '''''''''start''''''''''''''
        '''''to close the window''''''''''


        strClose = "<Script language='Javascript'>"
        strClose = strClose + "window.close();"

        strClose = strClose + "</Script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
        ''''''''end'''''''''
        'Dim strparent As String
        'strparent = "<Script language='Javascript'>"
        'strparent = strparent + "window.opener.closed=false;"

        'strparent = strparent + "</Script>"
        'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strparent)

    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub

    Protected Sub ok_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ok.Command

    End Sub
    ''' <summary>
    ''' this is use to remove the columns from table if they exists
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub check()
        cmdvalue = New SqlCommand("select * from  " + table + "", con)
        con.Open()



        Dim datasetaa As New DataSet

        da.SelectCommand = cmdvalue
        da.Fill(datasetaa)

        con.Close()
        Dim culmnaaa As DataColumn
        Dim strclocheck As String
        For Each culmnaaa In datasetaa.Tables(0).Columns


            strclocheck = culmnaaa.ColumnName()
            If strclocheck.StartsWith("Column_Percentage_") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            End If




        Next

    End Sub
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub
    ''' <summary>
    ''' this is use to show already calculated formulas checked on selected report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim table1 As String
        table1 = Session("table")
        Dim i As Integer
        Dim ss As String = Session("checked")
        If (ss.Equals("Tables")) Then
            cmdvalue = New SqlCommand("select * from  " + table1 + "  ", con)
            table = table1
        Else
            cmd = New SqlCommand("select tablename from IDMSReportMaster where QueryName ='" + table1 + "'", con)
            con.Open()
            Dim sa As String = cmd.ExecuteScalar.ToString()
            cmdvalue = New SqlCommand("select * from  " + sa + "  ", con)
            table = sa
        End If
        Dim datasetaa As New DataSet

        da.SelectCommand = cmdvalue
        da.Fill(datasetaa)

        con.Close()
        Dim culmnaaa As DataColumn
        Dim strclocheck As String
        For Each culmnaaa In datasetaa.Tables(0).Columns


            strclocheck = culmnaaa.ColumnName()
            If strclocheck.StartsWith("Accu_Sum") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            ElseIf strclocheck.StartsWith("Column_Percentage_") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()

            ElseIf strclocheck.StartsWith("Column_Sum_Percentage") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Column_Sum") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Row_Percentage_") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.StartsWith("Row_Sum_") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            ElseIf strclocheck.Contains("Filter") Then
                cmdvalue = New SqlCommand("alter table " + table + " drop column " + strclocheck + " ", con)
                con.Open()
                cmdvalue.ExecuteNonQuery()
                con.Close()
            End If




        Next




        If Page.IsPostBack = False Then
            'Session("saveanalysis") = ""
            'Session("maxval") = ""
            Dim reportname As String = ""
            reportname = Session("repname")
            Dim Regressionx As String = ""
            Dim comparesamplesx As String = ""
            Dim Columnpercentagex As String = ""
            'Dim FilterPercentagex As String = ""
            Dim Meanx As String = ""
            Dim Modex As String = ""
            Dim Standarddeviationx As String = ""
            Dim RowSumPercentagex As String = ""
            Dim AccumulatedSumx As String = ""
            Dim Maxs As String = ""
            Dim Averages As String = ""
            Dim Countsx As String = ""
            Dim Mins As String = ""
            Dim ColumnSumPercentagex As String = ""
            Dim Standarderrorx As String = ""
            Dim Rangex As String = ""
            Dim Medianx As String = ""

            'Dim Indexs As String = ""
            Dim NonWeightedNumberx As String = ""
            Dim RowPercentagex As String = ""
            Dim Correlationx As String = ""
            If Session("analysis") <> "" Then
                cmdvalue = New SqlCommand("SELECT * FROM savedanalysis where analysisname = '" + table + "' ", con)
                con.Open()

                dr = cmdvalue.ExecuteReader

                If dr.Read Then

                    Regressionx = dr("Regression").ToString
                    'comparesamplesx = dr("comparesamples").ToString
                    Columnpercentagex = dr("Columnpercentage").ToString
                    'FilterPercentagex = dr("FilterPercentage").ToString
                    Meanx = dr("Mean").ToString
                    Modex = dr("Mode").ToString
                    Standarddeviationx = dr("standarddeviation").ToString
                    RowSumPercentagex = dr("RowSumPercentage").ToString
                    AccumulatedSumx = dr("AccumulatedSum").ToString
                    Maxs = dr("Maxs").ToString
                    Averages = dr("Average").ToString
                    Countsx = dr("Counts").ToString
                    Mins = dr("Mins").ToString
                    ColumnSumPercentagex = dr("ColumnSumPercentage").ToString
                    Standarderrorx = dr("Standarderror").ToString
                    Rangex = dr("Range").ToString
                    Medianx = dr("Median").ToString
                    'Indexs = dr("Indexs").ToString
                    'NonWeightedNumberx = dr("NonWeightedNumber").ToString
                    RowPercentagex = dr("RowPercentage").ToString

                    Correlationx = dr("correlation").ToString
                End If
                con.Close()
                dr.Close()

                If Columnpercentagex = "yes" Then
                    Me.columnpercentage.Checked = True
                Else
                    Me.columnpercentage.Checked = False
                End If
                If Standarderrorx = "yes" Then
                    Me.standarderror.Checked = True
                Else
                    Me.standarderror.Checked = False
                End If
                'If FilterPercentagex = "yes" Then
                '    Me.filterpercentage.Checked = True
                'Else
                '    Me.filterpercentage.Checked = False
                'End If
                If RowPercentagex = "yes" Then
                    Me.rowpercentage.Checked = True
                Else
                    Me.rowpercentage.Checked = False
                End If
                If Standarddeviationx = "yes" Then
                    standarddeviation.Checked = True
                Else
                    standarddeviation.Checked = False
                End If
                If Correlationx = "yes" Then
                    correlation.Checked = True
                Else
                    correlation.Checked = False
                End If
                If Regressionx = "yes" Then
                    regression.Checked = True
                Else
                    regression.Checked = False
                End If
                If Maxs = "yes" Then
                    Max.Checked = True
                Else
                    Max.Checked = False
                End If
                If Mins = "yes" Then
                    Min.Checked = True
                Else
                    Min.Checked = False
                End If
                If Averages = "yes" Then
                    average.Checked = True
                Else
                    average.Checked = False
                End If
                If Countsx = "yes" Then
                    count.Checked = True
                Else
                    count.Checked = False
                End If
                If Meanx = "yes" Then
                    mean.Checked = True
                Else
                    mean.Checked = False
                End If
                If Modex = "yes" Then
                    mode.Checked = True
                Else
                    mode.Checked = False
                End If
                If Rangex = "yes" Then
                    range.Checked = True
                Else
                    range.Checked = False
                End If
                If AccumulatedSumx = "yes" Then
                    accumulatedsum.Checked = True
                Else
                    accumulatedsum.Checked = False
                End If
                If Medianx = "yes" Then
                    median.Checked = True
                Else
                    median.Checked = False
                End If
                If RowSumPercentagex = "yes" Then
                    rowsumpercentage.Checked = True
                Else
                    rowsumpercentage.Checked = False
                End If
                If ColumnSumPercentagex = "yes" Then
                    columnsumpercentage.Checked = True
                Else
                    columnsumpercentage.Checked = False
                End If
                If comparesamplesx = "yes" Then
                    comparesamples.Checked = True
                Else
                    comparesamples.Checked = False
                End If
                If NonWeightedNumberx = "yes" Then
                    nonweightednumber.Checked = True
                Else
                    nonweightednumber.Checked = False
                End If
                'If Indexs = "yes" Then
                '    index.Checked = True
                'Else
                '    index.Checked = False
                'End If
            Else
                'cmdvalue = New SqlCommand("SELECT * FROM analysisformula where reportname = '" + reportname + "' and savedby='" + Session("userid") + "'", con)
                If Session("Checkforreportformilas") <> "" Then
                    Dim checkallformulachecked As String = Session("Checkforreportformilas").ToString
                    If checkallformulachecked.Contains("col") Then
                        Me.columnpercentage.Checked = True
                    Else
                        Me.columnpercentage.Checked = False
                    End If
                    If checkallformulachecked.Contains("sta") Then
                        Me.standarderror.Checked = True
                    Else
                        Me.standarderror.Checked = False
                    End If
                    'If FilterPercentagex = "yes" Then
                    '    Me.filterpercentage.Checked = True
                    'Else
                    '    Me.filterpercentage.Checked = False
                    'End If
                    If checkallformulachecked.Contains("row") Then
                        Me.rowpercentage.Checked = True
                    Else
                        Me.rowpercentage.Checked = False
                    End If
                    If checkallformulachecked.Contains("stan") Then
                        standarddeviation.Checked = True
                    Else
                        standarddeviation.Checked = False
                    End If
                    If checkallformulachecked.Contains("cor") Then
                        correlation.Checked = True
                    Else
                        correlation.Checked = False
                    End If
                    If checkallformulachecked.Contains("reg") Then
                        regression.Checked = True
                    Else
                        regression.Checked = False
                    End If
                    If checkallformulachecked.Contains("max") Then
                        Max.Checked = True
                    Else
                        Max.Checked = False
                    End If
                    If checkallformulachecked.Contains("min") Then
                        Min.Checked = True
                    Else
                        Min.Checked = False
                    End If
                    If checkallformulachecked.Contains("ave") Then
                        average.Checked = True
                    Else
                        average.Checked = False
                    End If
                    If checkallformulachecked.Contains("cou") Then
                        count.Checked = True
                    Else
                        count.Checked = False
                    End If
                    If checkallformulachecked.Contains("mea") Then
                        mean.Checked = True
                    Else
                        mean.Checked = False
                    End If
                    If checkallformulachecked.Contains("mod") Then
                        mode.Checked = True
                    Else
                        mode.Checked = False
                    End If
                    If checkallformulachecked.Contains("ran") Then
                        range.Checked = True
                    Else
                        range.Checked = False
                    End If
                    If checkallformulachecked.Contains("acc") Then
                        accumulatedsum.Checked = True
                    Else
                        accumulatedsum.Checked = False
                    End If
                    If checkallformulachecked.Contains("med") Then
                        median.Checked = True
                    Else
                        median.Checked = False
                    End If
                    If checkallformulachecked.Contains("rowp") Then
                        rowsumpercentage.Checked = True
                    Else
                        rowsumpercentage.Checked = False
                    End If
                    If checkallformulachecked.Contains("colp") Then
                        columnsumpercentage.Checked = True
                    Else
                        columnsumpercentage.Checked = False
                    End If
                    If checkallformulachecked.Contains("com") Then
                        comparesamples.Checked = True
                    Else
                        comparesamples.Checked = False
                    End If
                    'If NonWeightedNumberx = "yes" Then
                    '    nonweightednumber.Checked = True
                    'Else
                    '    nonweightednumber.Checked = False
                    'End If
                    'If Indexs = "yes" Then
                    '    index.Checked = True
                    'Else
                    '    index.Checked = False
                    'End If
                End If
            End If


        End If
    End Sub

    'Protected Sub btclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btclose.Click

    '    'Dim strClose As String = ""
    '    'strClose = "<Script language='Javascript'>"
    '    'strClose = strClose + "window.close()"
    '    'strClose = strClose + "</Script>"
    '    'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    'End Sub
End Class
