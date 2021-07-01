Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.HtmlTextWriter
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader

Partial Class ReportDesigner_nShowData
    Inherits System.Web.UI.Page
    Dim con As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim objadp As New SqlDataAdapter
    Dim reader As SqlDataReader
    Dim objcmd As New SqlCommand
    Dim fun As New Functions
    Dim repDesign As New ReportDesigner
    Dim dept As Integer = 0
    Dim client As Integer = 0
    Dim lob As Integer = 0
    Dim i As Integer = 0
    Public Path As String = ""
    Dim savedby As String = ""
    Dim savedon As Date
    Dim ex345 As String = ""
    Dim repscope As String = ""
    Dim emailalertquery As String = ""
    Dim centercount = 0
    Dim cmdget As New SqlCommand
    Dim dr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Page.SmartNavigation = True
        Page.MaintainScrollPositionOnPostBack = True
        divmsgboxdeltable.Visible = False
        If Me.IsPostBack = False Then
            cmdget = New SqlCommand("select distinct menuid from nlvl_menu_rights where userid='" + Session("userid").ToString + "' and MenuId='2'", conn)
            conn.Open()

            dr = cmdget.ExecuteReader()
            If (dr.HasRows) Then
                imgChart.Visible = True
            Else
                imgChart.Visible = False
            End If
            dr.Close()
            conn.Close()
            cmdget.Dispose()

            'Changes by Mohit Tyagi on 3-August-2012
            'cmdget = New SqlCommand("select COUNT(Tableid) from WARSLOBTableMaster where CreatedBy='" + Session("userid").ToString + "' and localTable='temp'", conn)
            'conn.Open()
            'Dim count As Integer
            'count = Convert.ToInt32(cmdget.ExecuteScalar())
            'If (count > 0) Then
            '    imgAnalysis.Visible = True
            'Else
            '    imgAnalysis.Visible = False
            'End If
            'conn.Close()
            'cmdget.Dispose()


            If (Trim(Request("txtStartdate")) <> "") Then
                Me.hidStart.Value = Trim(Request("txtStartdate"))
            End If
            If (Trim(Request("txtEnddate")) <> "") Then
                Me.hidEnd.Value = Trim(Request("txtEnddate"))
            End If
            hidReportscope.Value = Trim(Request("hidReportscope"))
            hidDepartment.Value = Trim(Request("hidDepartment"))
            hidClient.Value = Trim(Request("hidClient"))
            hidLob.Value = Trim(Request("hidLob"))
            hidReportname.Value = Trim(Request("hidReportname"))
            hidReporttype.Value = Trim(Request("hidReporttype"))
            If (Trim(Request("hidDatetable")) <> "") Then
                hidDatetable.Value = Trim(Request("hidDatetable"))
            End If

            If Trim(Request("hidHeight")) <> "" Then
                Dim hgt As String()
                hgt = Split(Trim(Request("hidHeight")), ",")
                Me.divHeader.Style.Add("height", hgt(0))
                Me.divFooter.Style.Add("height", hgt(1))
            End If
            '''''''' Basic Formatting of Header'''''''''

            Dim g = 0
            If (Trim(Request("hidHeaderformat")) <> "") Then
                Dim hStyle As String() = Split(Trim(Request("hidHeaderformat")), ";")
                For g = 0 To hStyle.Length - 1
                    Dim st As String() = hStyle(g).Split(":")
                    divHeader.Style.Add(st(0), st(1))
                Next
            End If
            ''''''''''''''''''''''''''''''''''''''''''
            'Design Header 
            'Creating the header elements for the first time
            If Trim(Request("hidHPos")) <> "" Then
                divHeader.Style.Add("border-top", "gray 1px solid")
                divHeader.Style.Add("border-left", "gray 1px solid")
                divHeader.Style.Add("border-right", "gray 1px solid")
                Dim i As Integer = 0
                Dim hpos As String()
                hpos = Split(Trim(Request("hidHPos")), "~")
                Dim vishiddencount As Integer = 0
                For i = 0 To hpos.Length - 1
                    Dim elm As String()
                    elm = Split(hpos(i), "@#@")
                    gen_elm(elm(0), elm(1)) ' actually creates the elements
                Next
                For i = 0 To hpos.Length - 1
                    Dim elm As String()
                    elm = Split(hpos(i), "@#@")
                    Dim st = Split(elm(1), "#@#")
                    Dim st1 = Split(st(1), ",")
                    ' If (st1(2).ToString() <> "") Then
                    If (st1(2) <> "") Then
                        vishiddencount += 1
                    Else
                    End If

                Next
                If vishiddencount = hpos.Length Then
                    divHeader.Style.Add("border-top", "white 0px none")
                    divHeader.Style.Add("border-left", "white 0px none")
                    divHeader.Style.Add("border-right", "white 0px none")

                    Me.divHeader.Style.Add("height", "0")
                    centercount = 0
                End If
                ''''''' ends'''''''
                'formatting the header elements so generated
                If Trim(Request("hidHformat")) <> "" Then
                    Dim hfor As String()
                    hfor = Split(Trim(Request("hidHformat")), "~")
                    For i = 0 To hfor.Length - 1
                        Dim elmf As String()
                        elmf = Split(hfor(i), ">")
                        ' aspnet_msgbox(elmf(1))
                        set_elmf(elmf(0), elmf(1)) ' format the elements
                    Next
                End If
                '''''''''' ends''''''''''''
                'apply Formula on header elements so generated
                If Trim(Request("hidHformula")) <> "" Then
                    Dim hfor As String()
                    ' Dim tmp = Replace(Trim(Request("hidHformula")), "$", ".")
                    Dim asdf As String = Replace(Trim(Request("hidHformula")), vbNewLine, "")
                    If hidStart.Value <> "" Then
                        asdf = Replace(asdf, "@Date1@", hidStart.Value)
                    End If
                    If hidEnd.Value <> "" Then
                        asdf = Replace(asdf, "@Date2@", hidEnd.Value)
                    End If
                    hfor = Split(asdf, "~")
                    For i = 0 To hfor.Length - 1
                        Dim elmf As String()
                        elmf = Split(hfor(i), "@#@")
                        Dim tmp = Replace(elmf(1), "$", ".")
                        ' aspnet_msgbox(elmf(1))
                        set_Hformula(elmf(0), tmp) ' apply formula
                    Next
                End If
                '''''''''' ends''''''''''''
                ' apply color condition on header elements
                If Trim(Request("hidHcolorcon")) <> "" Then
                    Dim hcol As String()
                    '' store color condition
                    Dim colorCon = ""

                    Dim d As String() = Split(Trim(Request("hidHcolorcon")), "~")
                    Dim h As Integer = 0
                    For h = 0 To d.Length - 1
                        Dim f As String() = Split(d(h), "@#@") '' f(0)= object name, f(1)= all conditions
                        Dim tmop1 = Split(f(1), "##")
                        Dim s1 = 0
                        For s1 = 0 To tmop1.Length - 1
                            Dim tmop2 = Split(tmop1(s1), "#@#")

                            If colorCon = "" Then
                                colorCon = f(0) + "@#@" + tmop2(1)
                            Else
                                colorCon = colorCon + "~" + f(0) + "@#@" + tmop2(1)
                            End If
                        Next
                    Next


                    ''''
                    hcol = Split(Trim(colorCon), "~")
                    For i = 0 To hcol.Length - 1
                        Dim col As String()
                        Dim col1 As String()
                        Dim val = ""
                        col1 = Split(hcol(i), "@#@")
                        col = Split(col1(1), "^")
                        If col(0) = "fixed" Then
                            val = col1(0) + "@" + col(1) + "^" + col(2) + "^" + col(3)
                        ElseIf col(0) = "formula" Then
                            ' Dim fmula = repDesign.get_Value(col(2))
                            val = col1(0) + "@" + col(1) + "^" + col(3)
                        Else ' condition based on some object value
                            Dim res = get_Headervalue(col(2))
                            val = col1(0) + "@" + col(1) + "^" + res + "^" + col(3)
                        End If

                        'Temp Code Added by Lalit
                        hidHcolorcon.Value = ""
                        'End

                        If Trim(hidHcolorcon.Value) = "" Then
                            hidHcolorcon.Value = val
                        Else
                            hidHcolorcon.Value = Trim(hidHcolorcon.Value) + "~" + val
                        End If
                    Next
                    apply_Hcolorcon() ' apply the color condition
                End If
                ''''''''' ends ''''''''''
            Else
                Me.divHeader.Style.Add("height", "0px")
            End If
            ''''''' Header columns ends'''''''''
            ''''''' Generating Details columns

            If Trim(Request("hidDPos")) <> "" Then
                If (Trim(Request("hidTables")) = "") Then
                    Me.lblMsg.Text = "No Table Found"
                    Exit Sub
                End If

                If (Trim(Request("hidColorcondition")) <> "") Then
                    hidColorcondition.Value = Trim(Request("hidColorcondition"))
                End If
                Me.hidDpos.Value = Trim(Request("hidDPos"))
                '''''''' replace formula name with formula definition
                Dim formul = Trim(Request("hidDformula"))
                If formul <> "" Then
                    Dim for1 = Split(formul, "~")
                    Dim nm = 0
                    For nm = 0 To for1.Length - 1
                        Dim bnm = Split(for1(nm), " AS ")
                        hidDpos.Value = Replace(hidDpos.Value, bnm(1), for1(nm))
                    Next
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Me.hidTables.Value = Replace(Trim(Request("hidTables")), "~", ",")
                Me.hidDformat.Value = Trim(Request("hidDformat"))
                Dim i As Integer
                i = 0
                Dim hpos As String()

                '' Replace newline character\
                Me.hidDpos.Value = Replace(hidDpos.Value, vbNewLine, "")
                '''''''''''''
                hpos = Split(Trim(Me.hidDpos.Value), "~")


                For i = 0 To hpos.Length - 1

                    Dim final As String = ""
                    Dim temp As String = ""
                    temp = hpos(i)
                    If (hpos(i).Contains(" AS ") = False) Then
                        temp = Replace(temp, ".", "$")
                        final = Replace(hpos(i), "$", ".") + " AS " + Replace(temp, ".", "$")
                    Else
                        final = Replace(hpos(i), "$", ".")
                    End If
                    If (Trim(Me.hidColumns.Value) <> "") Then
                        Me.hidColumns.Value = Trim(Me.hidColumns.Value) + "," + Trim(final)
                    Else
                        Me.hidColumns.Value = Trim(final)
                    End If
                Next
                gen_delm()

                '''''''''' ends''''''''''''
            Else
                Me.divDetails.Style.Add("display", "none")
            End If
            '''''' Details columns ends
            '''''''' Basic Formatting of footer'''''''''
            If (Trim(Request("hidFooterformat")) <> "") Then
                Dim hStyle As String() = Split(Trim(Request("hidFooterformat")), ";")
                For g = 0 To hStyle.Length - 1
                    Dim st As String() = hStyle(g).Split(":")
                    divFooter.Style.Add(st(0), st(1))
                Next
            End If
            ''''''''''''''''''''''''''''''''''''''''''
            ''''''' Generating Footer columns
            If Trim(Request("hidFPos")) <> "" Then
                divFooter.Style.Add("border-bottom", "gray 1px solid")
                divFooter.Style.Add("border-left", "gray 1px solid")
                divFooter.Style.Add("border-right", "gray 1px solid")
                Dim i As Integer = 0
                Dim fvishiddencount As Integer = 0

                Dim hpos As String()
                hpos = Split(Trim(Request("hidFPos")), "~")

                For i = 0 To hpos.Length - 1
                    Dim elm As String()
                    elm = Split(hpos(i), "@#@")
                    gen_felm(elm(0), elm(1)) ' actually creates the elements
                Next
                For i = 0 To hpos.Length - 1
                    Dim elmf As String()
                    elmf = Split(hpos(i), "@#@")
                    Dim st = Split(elmf(1), "#@#")
                    Dim st1 = Split(st(1), ",")
                    ' If (st1(2).ToString() <> "") Then
                    If (st1(2) <> "") Then
                        fvishiddencount += 1
                    Else
                    End If
                Next
                Me.divDetails.Style.Add("text-align", "center")
                'TEXT-ALIGN: center
                If fvishiddencount = hpos.Length Then
                    divFooter.Style.Add("border-bottom", "white 0px none")
                    divFooter.Style.Add("border-left", "white 0px none")
                    divFooter.Style.Add("border-right", "white 0px none")

                    Me.divFooter.Style.Add("height", "0")
                    If (centercount <> 1) Then
                        Me.divDetails.Style.Add("text-align", "left")
                    End If
                End If
                ''''''' ends'''''''
                'formatting the footer elements so generated
                If Trim(Request("hidFformat")) <> "" Then
                    Dim hfor As String()
                    hfor = Split(Trim(Request("hidFformat")), "~")
                    For i = 0 To hfor.Length - 1
                        Dim elmf As String()
                        elmf = Split(hfor(i), ">")
                        set_fformat(elmf(0), elmf(1)) ' format the elements
                    Next

                End If
                '''''''''' ends''''''''''''
                'apply Formula on footer elements so generated
                If Trim(Request("hidFformula")) <> "" Then
                    Dim hfor As String()

                    ' Dim tmp = Replace(Trim(Request("hidFformula")), "$", ".")
                    Dim aswe As String = Replace(Trim(Request("hidFformula")), vbNewLine, "")
                    If hidStart.Value <> "" Then
                        aswe = Replace(aswe, "@Date1@", hidStart.Value)
                    End If
                    If hidEnd.Value <> "" Then
                        aswe = Replace(aswe, "@Date2@", hidEnd.Value)
                    End If
                    hfor = Split(aswe, "~")
                    For i = 0 To hfor.Length - 1
                        Dim elmf As String()
                        elmf = Split(hfor(i), "@#@")
                        Dim tmp = Replace(elmf(1), "$", ".")
                        ' aspnet_msgbox(elmf(1))
                        set_Fformula(elmf(0), tmp) ' apply formula
                    Next

                End If
                '''''''''' ends''''''''''''
                ' apply color condition on footer elements
                If Trim(Request("hidFcolorcon")) <> "" Then
                    Dim hcol As String()
                    ' Dim tmp = Replace(Trim(Request("hidHformula")), "$", ".")
                    '' store color condition
                    Dim colorCon = ""

                    Dim d As String() = Split(Trim(Request("hidFcolorcon")), "~")
                    Dim h As Integer = 0
                    For h = 0 To d.Length - 1
                        Dim f As String() = Split(d(h), "@#@") '' f(0)= object name, f(1)= all conditions
                        Dim tmop1 = Split(f(1), "##")
                        Dim s1 = 0
                        For s1 = 0 To tmop1.Length - 1
                            Dim tmop2 = Split(tmop1(s1), "#@#")

                            If colorCon = "" Then
                                colorCon = f(0) + "@#@" + tmop2(1)
                            Else
                                colorCon = colorCon + "~" + f(0) + "@#@" + tmop2(1)
                            End If
                        Next
                    Next


                    ''''
                    hcol = Split(Trim(colorCon), "~")
                    For i = 0 To hcol.Length - 1
                        Dim col As String()
                        Dim col1 As String()
                        Dim val = ""
                        col1 = Split(hcol(i), "@#@")
                        col = Split(col1(1), "^")
                        If col(0) = "fixed" Then
                            val = col1(0) + "@" + col(1) + "^" + col(2) + "^" + col(3)
                        ElseIf col(0) = "formula" Then
                            'Dim fmula = repDesign.get_Value(col(2))
                            val = col1(0) + "@" + col(1) + "^" + col(3)
                        Else ' condition based on some object value
                            Dim res = get_Footervalue(col(2))
                            val = col1(0) + "@" + col(1) + "^" + res + "^" + col(3)
                        End If
                        If Trim(hidFcolorcon.Value) = "" Then
                            hidFcolorcon.Value = val
                        Else
                            hidFcolorcon.Value = Trim(hidFcolorcon.Value) + "~" + val
                        End If
                    Next
                    apply_Fcolorcon() ' apply the color condition
                End If
                ''''''''' ends ''''''''''
            Else
                Me.divFooter.Style.Add("height", "0px")
            End If


            '''''' footer columns ends

            If hidReportname.Value <> "" Then
                lblCaption.Text = " Report Name : " + hidReportname.Value
            End If
        End If
        Dim removespace As String = Me.divDetails.InnerHtml
        removespace = removespace.Replace(vbNewLine, "")
        removespace = Trim(removespace)
        Me.divDetails.InnerHtml = removespace
        If (Trim(Request("hidDetailsformat")) <> "") Then

            Dim g As Integer = 0
            Dim hStyle As String() = Split(Trim(Request("hidDetailsformat")), ";")
            For g = 0 To hStyle.Length - 1
                Dim st As String() = hStyle(g).Split(":")
                divDetails.Style.Add(st(0), st(1))
            Next

        End If
    End Sub
    ''' <summary>
    ''' Generate Header Elements 
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="pos">objectPosition</param>
    ''' <remarks></remarks>
    Public Sub gen_elm(ByVal obj, ByVal pos)
        Dim st = Split(pos, "#@#")
        Dim st1 = Split(st(1), ",")
        Dim lbl As New TextBox 'TextBox
        Dim Assolbl As New Label 'Assosiated control
        centercount = 1
        lbl.ID = obj
        Assolbl.AssociatedControlID = obj
        Assolbl.ID = "lbl" & obj

        lbl.Style.Add("position", "relative")
        lbl.Width = 180 '184
        lbl.Height = 16 '20       
        lbl.Font.Name = "verdana"
        lbl.BackColor = Drawing.Color.White
        lbl.BorderStyle = BorderStyle.Solid
        lbl.BorderWidth = 1
        lbl.BorderColor = lbl.BackColor  'lbl.BackColor
        lbl.ReadOnly = True
        lbl.Font.Size = 8
        lbl.Style.Add("top", st1(0) + "px")
        lbl.Style.Add("left", st1(1) + "px")
        If (st1(2).ToString() <> "") Then
            lbl.Style.Add("visibility", "hidden")
        Else
            lbl.Style.Add("visibility", "visible")
        End If
        '' apply basic footer formatting to the element
        If (Trim(Request("hidHeaderformat")) <> "") Then
            Dim g = 0
            Dim hStyle As String() = Split(Trim(Request("hidHeaderformat")), ";")
            For g = 0 To hStyle.Length - 1
                Dim st5 As String() = hStyle(g).Split(":")
                lbl.Style.Add(st5(0), st5(1))
            Next
        End If
        '''''''''''
        lbl.Text = st(0)
        Assolbl.Text = ""

        Me.divHeader.Controls.Add(Assolbl)
        Me.divHeader.Controls.Add(lbl)
    End Sub
    ''' <summary>
    ''' Format theHeader elements
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="fval">formatValue</param>
    ''' <remarks></remarks>
    Public Sub set_elmf(ByVal obj, ByVal fval)
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divheader.Controls.Count
        For i = 0 To j - 1
            If (Me.divheader.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox 'Label
                lb = Me.divheader.Controls.Item(i)
                Dim fval1 As String()
                fval1 = Split(fval, ";")
                Dim f As Integer
                f = 0
                For f = 0 To fval1.Length - 1
                    Dim afval As String()
                    afval = Split(fval1(f), ":")
                    lb.Style.Add(afval(0), afval(1))
                Next

            End If
        Next
    End Sub
    ''' <summary>
    ''' fetch value of a header object
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <remarks></remarks>
    Public Function get_Headervalue(ByVal obj)
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divHeader.Controls.Count
        Dim val = ""
        For i = 0 To j - 1
            If (Me.divHeader.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox ' Label
                lb = Me.divHeader.Controls.Item(i)
                val = lb.Text
                Exit For
            End If
        Next
        Return val
    End Function
    ''' <summary>
    ''' fetch value of a footer object
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <remarks></remarks>
    Public Function get_Footervalue(ByVal obj)
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divFooter.Controls.Count
        Dim val = ""
        For i = 0 To j - 1
            If (Me.divFooter.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox 'Label
                lb = Me.divFooter.Controls.Item(i)
                val = lb.Text
                Exit For
            End If
        Next
        Return val
    End Function

    ''' <summary>
    ''' Apply formula on header elements
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="formula">formulaToapply</param>
    ''' <remarks></remarks>
    Public Sub set_Hformula(ByVal obj, ByVal formula)
        '        Dim str As String = "select " + formula
        Dim str As String = Replace(formula, "@Date1@", Me.hidStart.Value)
        str = Replace(formula, "@Date2@", Me.hidEnd.Value)
        Dim fnObj As New Functions
        Dim val As String = ""
        Try
            val = repDesign.get_Value(str)
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message
        End Try

        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divHeader.Controls.Count
        For i = 0 To j - 1
            If (Me.divHeader.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox 'Label
                lb = Me.divHeader.Controls.Item(i)
                lb.Text = val
            End If
        Next
    End Sub
    ''' <summary>
    ''' to apply color condition on header elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub apply_Hcolorcon()
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        Dim h = 0
        Dim arr As String = Trim(hidHcolorcon.Value)
        Dim cc = 0
        ' Dim valcc As String()
        j = Me.divHeader.Controls.Count
        Dim val = ""
        For i = 0 To j - 1
            Dim d As String() = Split(arr, "~")
            For h = 0 To d.Length - 1
                Dim f As String() = Split(d(h), "@")
                If (Me.divHeader.Controls.Item(i).ID = f(0)) Then
                    Dim lb As TextBox 'Label
                    lb = Me.divHeader.Controls.Item(i)
                    Dim s8 = Split(f(1), "^")
                    Dim vC1 As Boolean = False

                    Dim tochk = lb.Text
                    Dim tochk1 = s8(1)
                    'Changes Done At IBM By Lalit
                    If IsNumeric(lb.Text) And IsNumeric(s8(1)) Then
                        tochk = CType(lb.Text, Double)
                        tochk1 = CType(s8(1), Double)
                        'Lalit
                    End If

                    If s8(0) = "=" Then
                        If tochk = tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<>" Then
                        If tochk <> tochk1 Then
                            vC1 = True
                        End If

                    ElseIf s8(0) = ">" Then
                        If tochk > tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<" Then
                        If tochk < tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = ">=" Then
                        If tochk >= tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<=" Then
                        If tochk <= tochk1 Then
                            vC1 = True
                        End If
                        'ElseIf s8(0) = "between" Then
                        '    Dim qc = Split(s8(1), ",")
                        '    If lb.Text >= qc(0) And lb.Text <= qc(1) Then
                        '        vC1 = True
                        '    End If
                        'ElseIf s8(0) = "not between" Then
                        '    Dim qc = Split(s8(1), ",")
                        '    If lb.Text <= qc(0) And lb.Text >= qc(1) Then
                        '        vC1 = True
                        '    End If
                    End If
                    If vC1 = True Then
                        Dim fmat = Split(s8(2), ";")
                        Dim k = 0
                        For k = 0 To fmat.length - 1
                            Dim fm = Split(fmat(k), ":")
                            lb.Style.Add(fm(0), fm(1))
                        Next
                    End If
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' Generate Footer elements
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="pos">objectPosition</param>
    ''' <remarks></remarks>
    Public Sub gen_felm(ByVal obj, ByVal pos)
        Dim st = Split(pos, "#@#")
        Dim st1 = Split(st(1), ",")
        Dim lbl As New TextBox 'Label
        Dim Assolbl As New Label 'Assosiated control
        'Dim centercount = 1
        lbl.ID = obj
        Assolbl.AssociatedControlID = obj
        Assolbl.ID = "lbl" & obj
        lbl.Style.Add("position", "relative")
        lbl.Width = 180 '184
        lbl.Height = 16 '20       
        lbl.Font.Name = "verdana"
        lbl.BackColor = Drawing.Color.White
        lbl.BorderStyle = BorderStyle.Solid
        lbl.BorderWidth = 1
        lbl.BorderColor = lbl.BackColor  'lbl.BackColor
        lbl.ReadOnly = True
        lbl.Font.Size = 8
        lbl.Style.Add("top", st1(0) + "px")
        lbl.Style.Add("left", st1(1) + "px")
        If (st1(2).ToString() <> "") Then
            lbl.Style.Add("visibility", "hidden")
        Else
            lbl.Style.Add("visibility", "visible")
        End If
        lbl.Text = st(0)
        '' apply basic footer formatting to the element
        If (Trim(Request("hidFooterformat")) <> "") Then
            Dim g = 0
            Dim hStyle As String() = Split(Trim(Request("hidFooterformat")), ";")
            For g = 0 To hStyle.Length - 1
                Dim st5 As String() = hStyle(g).Split(":")
                lbl.Style.Add(st5(0), st5(1))
            Next
        End If
        '''''''''''
        Assolbl.Text = ""
        Me.divFooter.Controls.Add(Assolbl)
        Me.divFooter.Controls.Add(lbl)
    End Sub
    ''' <summary>
    ''' Format the header elements
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="fval">formatValue</param>
    ''' <remarks></remarks>
    Public Sub set_fformat(ByVal obj, ByVal fval)
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divFooter.Controls.Count
        For i = 0 To j - 1
            If (Me.divFooter.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox ' Label
                lb = Me.divFooter.Controls.Item(i)
                Dim fval1 As String()
                fval1 = Split(fval, ";")
                Dim f As Integer
                f = 0
                For f = 0 To fval1.Length - 1
                    Dim afval As String()
                    afval = Split(fval1(f), ":")
                    lb.Style.Add(afval(0), afval(1))
                Next
            End If
        Next
    End Sub
    ''' <summary>
    ''' Apply formula on footer elements
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="formula">formulaToapply</param>
    ''' <remarks></remarks>
    Public Sub set_Fformula(ByVal obj, ByVal formula)
        ' Dim str As String = "select " + formula
        Dim str As String = Replace(formula, "@Date1@", Me.hidStart.Value)
        str = Replace(formula, "@Date2@", Me.hidEnd.Value)
        Dim fnObj As New Functions
        Dim val As String = ""
        Try
            val = repDesign.get_Value(str)
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divFooter.Controls.Count
        For i = 0 To j - 1
            If (Me.divFooter.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox 'Label
                lb = Me.divFooter.Controls.Item(i)
                lb.Text = val
            End If
        Next
    End Sub
    ''' <summary>
    ''' to apply color condition on footer elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub apply_Fcolorcon()
        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        Dim h = 0
        Dim arr As String = Trim(hidFcolorcon.Value)
        Dim cc = 0
        'Dim valcc As String()
        j = Me.divFooter.Controls.Count
        Dim val = ""
        For i = 0 To j - 1
            Dim d As String() = Split(arr, "~")
            For h = 0 To d.Length - 1
                Dim f As String() = Split(d(h), "@")
                If (Me.divFooter.Controls.Item(i).ID = f(0)) Then
                    Dim lb As TextBox 'Label
                    lb = Me.divFooter.Controls.Item(i)
                    Dim s8 = Split(f(1), "^")
                    Dim vC1 As Boolean = False

                    Dim tochk = lb.Text
                    Dim tochk1 = s8(1)

                    If s8(0) = "=" Then
                        If tochk = tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<>" Then
                        If tochk <> tochk1 Then
                            vC1 = True
                        End If

                    ElseIf s8(0) = ">" Then
                        If tochk > tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<" Then
                        If tochk < tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = ">=" Then
                        If tochk >= tochk1 Then
                            vC1 = True
                        End If
                    ElseIf s8(0) = "<=" Then
                        If tochk <= tochk1 Then
                            vC1 = True
                        End If
                        'ElseIf s8(0) = "between" Then
                        '    Dim qc = Split(s8(1), ",")
                        '    If lb.Text >= qc(0) And lb.Text <= qc(1) Then
                        '        vC1 = True
                        '    End If
                        'ElseIf s8(0) = "not between" Then
                        '    Dim qc = Split(s8(1), ",")
                        '    If lb.Text <= qc(0) And lb.Text >= qc(1) Then
                        '        vC1 = True
                        '    End If
                    End If
                    If vC1 = True Then
                        Dim fmat = Split(s8(2), ";")
                        Dim k = 0
                        For k = 0 To fmat.length - 1
                            Dim fm = Split(fmat(k), ":")
                            lb.Style.Add(fm(0), fm(1))
                        Next
                    End If
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' Generate Details elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gen_delm()

        Dim strColumn = Me.hidColumns.Value
        Dim columnnamearr As String() = Split(Me.hidDpos.Value, "~")
        Dim tbl As String = ""
        tbl = Me.hidTables.Value
        'tbl = "agents"
        Dim sqlString As String = ""
        Dim Sstring As String = ""
        Dim whrString As String = ""
        Dim forString As String = ""
        Dim groupby As String = ""
        Dim orderby As String = ""
        Dim having As String = ""
        Dim Sstring2 = "Select " & strColumn & " from " & tbl & ""
        Sstring = Replace(Sstring2, vbNewLine, "")
        'startrana
        emailalertquery = Sstring
        'ranastop

        If (Trim(Request("hidWhere")) <> "") Then
            Dim rep As String = Replace(Trim(Request("hidWhere")), "~", ",")
            Dim rep2 As String = Replace(rep, "$", ".")
            whrString = " where " + rep2
            Sstring = Sstring + whrString
            'startrana
            emailalertquery = Sstring
            'ranaend
        End If
        ''''' Attach automatic Dates
        If Sstring.Contains("@Date1@") = False And Sstring.Contains("@Date2@") = False Then
            Dim twr = ""
            If Me.hidStart.Value <> "" And Me.hidEnd.Value <> "" Then
                If (hidDatetable.Value <> "") Then
                    twr = hidDatetable.Value + ".Date between '" + Me.hidStart.Value + "' and '" + Me.hidEnd.Value + "'"
                Else
                    Dim tblEx = Split(Me.hidTables.Value, ",")
                    twr = tblEx(0) + ".Date between '" + Me.hidStart.Value + "' and '" + Me.hidEnd.Value + "'"
                End If
                If Trim(whrString) = "" Then
                    'startrana
                    Dim tblEx = Split(Me.hidTables.Value, ",")
                    emailalertquery = Sstring + " where " + tblEx(0) + ".Date between '@Date1@' and '@Date2@'"
                    'ranastop
                    Sstring = Sstring + " where " + twr
                    whrString = " where " + twr
                Else
                    'startrana
                    Dim tblEx = Split(Me.hidTables.Value, ",")
                    emailalertquery = Sstring + " and " + tblEx(0) + ".Date between '@Date1@' and '@Date2@'"
                    'ranastop
                    whrString = whrString + " and " + twr
                    Sstring = Sstring + " and " + twr

                End If

            End If
        End If
        '''''''''''''''''''''''''''''''''''''
        ''**************Changed by ranjit*******Befor this only commented code was here****************Start


        'If (Trim(Request("hidGroupby")) = "") Then


        '    Dim ranjitchanged As String() = hidDpos.Value.Split("~")
        '    Dim startr As Integer = 0
        '    Dim grouranjitmade As String = ""
        '    For startr = 0 To UBound(ranjitchanged)
        '        Dim vbn = LCase(ranjitchanged(startr))
        '        If vbn.Contains("sum(") Or vbn.Contains("max(") Or vbn.Contains("min(") Or vbn.Contains("avg(") Or vbn.Contains("count(") Then
        '        Else
        '            If grouranjitmade = "" Then
        '                grouranjitmade = ranjitchanged(startr).Replace("$", ".")
        '            Else
        '                grouranjitmade = grouranjitmade + "," + ranjitchanged(startr).Replace("$", ".")
        '            End If

        '        End If
        '    Next
        '    Dim tgp As String = ""
        '    If (Trim(grouranjitmade) <> "") Then
        '        tgp = repDesign.updateGroupby(hidDpos.Value, Trim(grouranjitmade))
        '        If tgp <> "" Then
        '            groupby = " Group By " + tgp
        '            Sstring = Sstring + " Group By " + tgp
        '        End If
        '    End If
        '    If grouranjitmade <> "" Then


        '        If (Trim(Request("hidOrderby")) = "") Then
        '            'Dim temp As String = Replace(Trim(Request("hidOrderby")), "~", ",")
        '            Dim temp2 As String = grouranjitmade + " Asc"
        '            orderby = " Order By " + temp2
        '            Sstring = Sstring + " Order By " + temp2
        '        End If
        '    End If
        'End If
        '''''''before this code was working------- Start
        Dim tgp As String = ""
        If (Trim(Request("hidGroupby")) <> "") Then
            tgp = repDesign.updateGroupby(hidDpos.Value, Trim(Request("hidGroupby")))
            If tgp <> "" Then
                groupby = " Group By " + tgp
                Sstring = Sstring + " Group By " + tgp
                'ranaadd
                emailalertquery = emailalertquery + " Group By " + tgp
            End If
        End If

        ''''''before this code was working------- Stope
        ''**************Changed by ranjit*******Befor this only commented code was here****************Stope

        If (Trim(Request("hidHaving")) <> "") Then
            Dim temp As String = Replace(Trim(Request("hidHaving")), "~", ",")
            Dim temp2 As String = Replace(temp, "$", ".")
            having = " having " + temp2
            Sstring = Sstring + " Having " + temp2
            'ranaadd
            emailalertquery = emailalertquery + " Having " + temp2
        End If
        'If groupby <> "" Then                '''''''' Place same elements of group by
        '    orderby = " Order by " + tgp
        '    Sstring = Sstring + " Order By " + tgp

        'End If

        If tgp Is Nothing Then


        Else
            If (Trim(Request("hidOrderby")) <> "") Then
                If LCase(Trim(Request("hidOrderby"))) = (LCase(tgp)) Then
                    Dim temp As String = Replace(Trim(Request("hidOrderby")), "~", ",")
                    Dim temp2 As String = Replace(temp, "$", ".")
                    orderby = " Order By " + temp2
                    Sstring = Sstring + " Order By " + temp2
                    'ranaadd
                    emailalertquery = emailalertquery + " Order By " + temp2
                Else
                    If tgp.Length = 0 Then
                        If (Trim(Request("hidOrderby")) <> "") Then
                            Dim temp As String = Replace(Trim(Request("hidOrderby")), "~", ",")
                            Dim temp2 As String = Replace(temp, "$", ".")
                            orderby = " Order By " + temp2
                            Sstring = Sstring + " Order By " + temp2
                            'ranaadd
                            emailalertquery = emailalertquery + " Order By " + temp2
                        End If
                    Else
                        If LCase(Trim(Request("hidOrderby"))).Contains("desc") Then
                            ' tgp = tgp + " desc"
                            tgp = Request("hidOrderby")
                        End If
                        If LCase(Trim(Request("hidOrderby"))).Contains("asc") Then
                            'tgp = tgp + " asc"
                            tgp = Request("hidOrderby")
                        End If
                        Dim temp As String = Replace(Trim(tgp), "~", ",")
                        Dim temp2 As String = Replace(temp, "$", ".")
                        orderby = " Order By " + temp2
                        Sstring = Sstring + " Order By " + temp2
                        'ranaadd
                        emailalertquery = emailalertquery + " Order By " + temp2
                    End If


                End If

            End If
        End If
        ' find the column names to be formatted
        Dim arr As String()
        Dim len As Integer = 0
        If hidDformat.Value <> "" Then
            arr = Split(hidDformat.Value, "~")
            len = arr.Length
        End If
        ''
        '' find out columns on which color condition is to apply

        Dim lenC As Integer = 0
        Dim colCond As String = ""
        Dim valCC As String = ""
        Dim cc = 0
        Dim v = 1
        Dim v2 = 1
        If (Sstring.Contains("@Date1@") = True And Me.hidStart.Value = "") Then
            v = 0
        End If

        If (Sstring.Contains("@Date2@") = True And Me.hidEnd.Value = "") Then
            v2 = 0
        End If
        If (v = 0) Then
            lblMsg.Text = "Please supply the start date"
        ElseIf (v2 = 0) Then
            lblMsg.Text = "Please supply the end date"
        Else

            Dim st1 As String = Replace(Sstring, "@Date1@", Me.hidStart.Value)
            Dim st2 As String = Replace(st1, "@Date2@", Me.hidEnd.Value)
            sqlString = st2
            whrString = Replace(whrString, "@Date1@", Me.hidStart.Value)
            whrString = Replace(whrString, "@Date2@", Me.hidEnd.Value)
            '' store color condition
            Dim colorCon = ""
            If Trim(hidColorcondition.Value) <> "" Then
                Dim d As String() = Split(Trim(hidColorcondition.Value), "~")
                Dim h As Integer = 0
                For h = 0 To d.Length - 1
                    Dim f As String() = Split(d(h), "@#@") '' f(0)= object name, f(1)= all conditions
                    Dim tmop1 = Split(f(1), "##")
                    Dim s1 = 0
                    For s1 = 0 To tmop1.length - 1
                        Dim tmop2 = Split(tmop1(s1), "#@#")

                        If colorCon = "" Then
                            colorCon = f(0) + "@#@" + tmop2(1)
                        Else
                            colorCon = colorCon + "~" + f(0) + "@#@" + tmop2(1)
                        End If
                    Next
                Next

            End If
            ''''

            Dim xtb As String = ""
            Dim style As String
            style = ""

            Dim tbStyle = "border-top:#336699 1px solid;border-left:#336699 1px solid;border-right:#336699 1px solid;border-bottom:#336699 1px solid;"

            Dim color = ""
            If (Trim(Request("hidDetailsformat")) <> "") Then
                tbStyle = Trim(Request("hidDetailsformat"))
                Dim abc = Split(tbStyle, ";")
                ''code added on 17/6/09
                'Dim g As Integer = 0
                'Dim tbStyle As String() = Split(Trim(Request("hidDetailsformat")), ";")
                'For g = 0 To hStyle.Length - 1
                '    Dim st As String() = hStyle(g).Split(":")
                '    divDetails.Style.Add(st(0), st(1))
                'Next
                color = abc(2)

            End If
            ''''''' Add Details Pane Header
            If centercount = 1 Then
                divDetails.Style.Add("text-align", "center")
            Else
                divDetails.Style.Add("text-align", "left")

            End If
            xtb = "<table border=1  style='" + tbStyle + "' cellspacing=0 cellpadding=2 NAME=GS id=GS class=sortable><tr > "

            xtb = "<table border=1 style='" + tbStyle + "' cellspacing=0 cellpadding=2 NAME=GS id=GS class=sortable><tr > "


            For i = 0 To columnnamearr.Length - 1
                Dim val As String = ""
                If (columnnamearr(i).Contains(" AS ") = True) Then
                    Dim s = columnnamearr(i).IndexOf(" AS ")
                    val = columnnamearr(i).Substring(s + 3)
                    '''' if [formula] encountered
                    If val.Contains("[") Then
                        Dim val0 As String = Replace(Trim(val), "[", "")
                        val0 = val0.Replace("]", "")
                        val = Trim(val0)
                    End If
                    ''''''''''''''''''''''''

                Else
                    If (Trim(columnnamearr(i)).Contains("$")) Then
                        Dim tmp As String() = Split(Trim(columnnamearr(i)), "$")
                        val = tmp(1)
                    Else
                        val = Trim(columnnamearr(i))
                    End If
                End If

                '' to set column format
                Dim l As Integer = 0
                Dim styl As String = ""
                Dim b = False
                For l = 0 To len - 1
                    If (arr(l).Contains(Replace(columnnamearr(i), "$", ".")) Or arr(l).Contains(columnnamearr(i))) Then
                        Dim st As String() = Split(arr(l), ">")
                        styl = st(1)
                        b = True
                    End If
                Next
                '' column format ends
                If (b = True) Then
                    If styl.Contains("background-color:#ffffff;") = True And styl.Contains("color:#000000") = True Then
                        styl = Replace(styl, "background-color:#ffffff;", "background-color:LightGrey;")
                        styl = Replace(styl, "color:#000000;", "")
                    End If
                    xtb = xtb & "<td  wrap style='" + styl + "'><b>" & val & "</b></td>"
                Else
                    xtb = xtb & "<td  wrap style='background-color:LightGrey;color:#000000;font-size:10pt;'><b>" & val & "</b></td>"
                End If

            Next

            Me.divDetails.InnerHtml = Me.divDetails.InnerHtml + xtb

            ''''''''''''' Details Pane Header ends'''''''''''
            ''''''''' Add Rows to the Details Pane''''''''''''''''''''
            xtb = ""
            Dim myDataset As New DataSet()
            Dim myAdapter As New SqlDataAdapter()
            Try

                '''Test Gopal
                'If sqlString.Contains("@USERID@") Then
                '    sqlString = Replace(sqlString, "@USERID@", Session("userid"))
                'End If

                ''' 
                objcmd = New SqlCommand(sqlString, conn)
                conn.Open()
                myDataset.Clear()
                myAdapter.SelectCommand = objcmd
                myAdapter.Fill(myDataset)
                conn.Close()
                myAdapter.Dispose()
                objcmd.Dispose()
                If myDataset.Tables(0).Rows.Count <> 0 Then '' If dataset is not null
                    Dim myi = 0
                    For myi = 0 To myDataset.Tables(0).Rows.Count - 1
                        xtb = xtb + "<tr>"
                        Dim k As Integer = 0
                        Dim b = False
                        For k = 0 To columnnamearr.Length - 1
                            Dim p As Integer = 0
                            Dim q As Integer = 0
                            Dim val = ""
                            If (columnnamearr(k).Contains(" AS ") = True) Then
                                Dim s1 = columnnamearr(k).IndexOf(" AS ")
                                Dim p1 As String = columnnamearr(k).Substring(s1 + 3)
                                '''' if [formula] encountered
                                If p1.Contains("[") Then
                                    Dim val0 As String = Replace(Trim(p1), "[", "")
                                    val0 = Replace(val0, "]", "")
                                    val = myDataset.Tables(0).Rows(myi).Item(Trim(val0)).ToString()

                                Else
                                    val = myDataset.Tables(0).Rows(myi).Item(Trim(p1)).ToString()
                                End If
                                ''''''''''''''''''''''''

                            Else
                                Dim hjk = Replace(columnnamearr(k), ".", "$")
                                val = myDataset.Tables(0).Rows(myi).Item(Trim(hjk)).ToString()
                            End If
                            '' to set color condition
                            cc = 0
                            Dim styl As String = ""
                            If Trim(colorCon) <> "" Then
                                Try
                                    Dim d As String() = Split(Trim(colorCon), "~")
                                    Dim h As Integer = 0
                                    For h = 0 To d.Length - 1
                                        Dim f As String() = Split(d(h), "@#@")
                                        Dim wifi = Split(columnnamearr(k), " AS ")
                                        Dim aliasn = Replace(columnnamearr(k), "$", ".")
                                        If (wifi.length > 1) Then
                                            Dim val10 As String = Replace(wifi(1), "[", "")
                                            val10 = Replace(val10, "]", "")
                                            aliasn = Trim(val10)
                                        End If
                                        'Ranjit changed becoz it was showing the color with previous same formula name
                                        ' If (f(0) = (aliasn)) Then
                                        If (f(0) = (aliasn)) Then
                                            Dim s = Split(f(1), "^")
                                            Dim vC As Boolean = False
                                            If (s(0) = "formula") Then
                                                '' replace color condition columns with values

                                                Dim o = 0
                                                For o = 0 To columnnamearr.Length - 1
                                                    Dim toread = columnnamearr(o)
                                                    If (toread.Contains(" AS ") = True) Then
                                                        Dim s1 = columnnamearr(o).IndexOf(" AS ")
                                                        Dim p1 = columnnamearr(o).Substring(s1 + 3)
                                                        Dim val0 As String = Replace(Trim(p1), "[", "")
                                                        val0 = Replace(val0, "]", "")
                                                        toread = Trim(val0)
                                                    End If

                                                    Dim tm = UCase(toread)
                                                    If (toread.Contains("$")) Then
                                                        tm = Replace(tm, "$", ".")
                                                    End If

                                                    Try
                                                        s(1) = Replace(UCase(s(1)), tm, CDbl(myDataset.Tables(0).Rows(myi).Item(toread)))
                                                    Catch ex As Exception

                                                        If IsDBNull(myDataset.Tables(0).Rows(myi).Item(toread)) Then
                                                            s(1) = Replace(UCase(s(1)), tm, "'" + myDataset.Tables(0).Rows(myi).Item(toread) + "'")
                                                        Else
                                                            s(1) = Replace(UCase(s(1)), tm, "'" + UCase(myDataset.Tables(0).Rows(myi).Item(toread)) + "'")
                                                        End If

                                                    End Try

                                                Next

                                                '''''''''''''''''''''''''''''''''''''''''''''''
                                                Dim a = repDesign.chkCondition(s(1))
                                                If a = True Then
                                                    styl = s(2)
                                                    GoTo sty
                                                End If
                                            Else

                                                ''''convert to definite datatype
                                                Dim tochk
                                                Dim tochk1
                                                Try
                                                    tochk = CDbl(val)
                                                    tochk1 = CDbl(s(2))
                                                Catch ex As Exception

                                                    tochk = val.ToString()
                                                    tochk1 = s(2).ToString()

                                                End Try
                                                ''''''''''''''''''''''''''''''''

                                                If s(1) = "=" Then
                                                    If tochk = tochk1 Then
                                                        vC = True
                                                    End If
                                                ElseIf s(1) = "<>" Then
                                                    If tochk <> tochk1 Then
                                                        vC = True
                                                    End If

                                                ElseIf s(1) = ">" Then
                                                    If tochk > tochk1 Then
                                                        vC = True
                                                    End If
                                                ElseIf s(1) = "<" Then
                                                    If tochk < tochk1 Then
                                                        vC = True
                                                    End If
                                                ElseIf s(1) = ">=" Then
                                                    If tochk >= tochk1 Then
                                                        vC = True
                                                    End If
                                                ElseIf s(1) = "<=" Then
                                                    If tochk <= tochk1 Then
                                                        vC = True
                                                    End If
                                                    'ElseIf s(1) = "between" Then
                                                    '    Dim qc = Split(s(2), ",")
                                                    '    If val >= qc(0) And val <= qc(1) Then
                                                    '        vC = True
                                                    '    End If
                                                    'ElseIf s(1) = "not between" Then
                                                    '    Dim qc = Split(s(2), ",")
                                                    '    If val <= qc(0) And val >= qc(1) Then
                                                    '        vC = True
                                                    '    End If
                                                End If
                                                If vC = True Then
                                                    styl = s(3)
                                                    GoTo sty
                                                End If
                                            End If
                                            '  Next
                                        End If
                                    Next
                                    ' Next
                                Catch ex As Exception
                                    lblMsg.Text = "Color Condition Could Not Be Applied Due To Syntax Error In The Condition"
                                End Try
                            End If
                            '' to set column format
                            Dim l As Integer = 0
                            Dim styltd = ""
                            For l = 0 To len - 1
                                Dim bvnhj = arr(l)
                                bvnhj = Replace(bvnhj, vbNewLine, "")
                                Dim s1 = Split(columnnamearr(k), " AS ")
                                Dim p1 = columnnamearr(k)
                                If s1.length > 1 Then
                                    p1 = s1(1)
                                End If

                                Dim val0 As String = Replace(Trim(p1), "[", "")
                                val0 = Replace(val0, "]", "")
                                Dim toread1 = Trim(val0)
                                Dim st As String() = Split(bvnhj, ">")
                                If (st(0) = Replace(toread1, "$", ".") Or st(0) = Replace(toread1, "$", ".")) Then
                                    styltd = st(1)
                                    GoTo stytd
                                End If
                            Next
stytd:
                            '''''' to print empty td when value is null
                            If val = "" Then
                                val = "&nbsp;"
                            End If
                            ''''''''''''''''''''''''''''''''''''''''''''''
                            If (styltd <> "") Then

                                xtb = xtb & "<td  wrap  style='" + styltd + "'>" & val & "</td>"
                            Else
                                xtb = xtb & "<td  wrap style='" + color + "' >" & val & "</td>"
                            End If

                            GoTo columnFormat
sty:
                            If (styl <> "") Then
                                xtb = xtb & "<td  wrap  style='" + styl + "'>" & val & "</td>"
                            Else
                                xtb = xtb & "<td  wrap style='" + color + "' >" & val & "</td>"
                            End If
columnFormat:
                            '' column format ends

                        Next
                        xtb = xtb & "</tr>"
                    Next
                    xtb = xtb & "</table>"
                    Dim strdiv = Me.divDetails.InnerHtml + xtb
                    'Me.divDetails.InnerHtml = Me.divDetails.InnerHtml + xtb
                    Me.divDetails.InnerHtml = ""
                    Me.divDetails.InnerHtml = Me.divDetails.InnerHtml + strdiv




                    Session("strFinalData") = Me.divDetails.InnerHtml

                Else
                    Me.divDetails.InnerHtml = ""
                    Me.lblMsg.Text = "No Records Found."
                End If
            Catch ex As Exception
                Me.divDetails.InnerHtml = ""
                Me.lblMsg.Text = "The Report Cannot Be Displayed: " + ex.Message
            End Try


        End If
        ''To Mails & Alert
        Dim sqlString90 = Replace(Me.hidDpos.Value, "~", ",")
        sqlString90 = Replace(sqlString90, "$", ".")
        Dim tb = Replace(hidTables.Value, "$", ",")
        tb = Replace(tb, "~", ",")
        sqlString90 = Replace(sqlString90, "@Date1@", Me.hidStart.Value)
        sqlString90 = Replace(sqlString90, "@Date2@", Me.hidEnd.Value)

        Dim ooop = Replace(hidDpos.Value, "@Date1@", Me.hidStart.Value)
        ooop = Replace(ooop, "@Date2@", Me.hidEnd.Value)
        ''''' specific values
        Session("colnames") = ooop
        Session("tables") = tb
        Session("wheredata") = whrString
        Session("groupdata") = groupby
        Session("orderdata") = orderby
        Session("having") = having
        '''''''''''''''''''''
        sqlString90 = "Select " + sqlString90 + " from " + tb + whrString + groupby + having + orderby
        'ranjit changed this line
        Session("Queryname") = sqlString90 + "#" + Me.hidTables.Value
        '''''''''''''''''''''''''''
        'Pawan Add this line
        Session("QueryforAlerts") = emailalertquery + "#" + Me.hidTables.Value + "#" + Me.hidReportname.Value + "$" + hidStart.Value + "$" + hidEnd.Value + "!" + ooop

        '''''''''''''''''''''''''''
    End Sub
  
    ''' <summary>
    ''' to generate and save a report as HTML file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSavehtml_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavehtml.Click
        'Dim savedby As String = Session("userid")
        savedby = Session("userid")
        savedon = System.DateTime.Today.ToShortDateString
        conn.Open()
        hidReportname.Value = Trim(Request("hidReportname"))
        hidReporttype.Value = Trim(Request("hidReporttype"))
        Dim tablename As String = Trim(Request("hidTables"))
        Session("tablename") = tablename
        If (tablename.Contains(",")) Then
            If (Session("reportname") = "") Then
                lblMsg.Text = "Report Maynot Be Processed/Saved. Please Save The Report First"
            Else
                If (txtHTMLreport.Text <> "") Then
                    ' code to generate and save HTML file
                    Dim b As Boolean = False
                    b = repDesign.CheckExistingHTMLReport(txtHTMLreport.Text)
                    If (b = False) Then
                        ' Generating the HTML Report
                        If divDetails.InnerHtml <> "" Then
                            Dim str As String = ""
                            Dim fp As StreamWriter
                            If Not Directory.Exists(Server.MapPath("UserSpace/" & Session("userid"))) Then
                                '<----------------------Creating Directory for partcular user--------------------------------->
                                Directory.CreateDirectory(Server.MapPath("UserSpace/" & Session("userid") & "/"))
                                '<----------------------End of Creating Directory for partcular user------------------------>
                            End If
                            '<------------------------End of Creating A main Directory--------------------------------------->
                            Path = "~/ReportDesigner/UserSpace/" & Session("userid") & "/" & txtHTMLreport.Text & ".html"
                            '<--------------------Creating a new text file---------------------------------->
                            fp = File.CreateText(Server.MapPath(Path))
                            If divHeader.InnerHtml <> "" Then
                                fp.WriteLine(divHeader.InnerHtml)
                            End If

                            fp.WriteLine(divDetails.InnerHtml)
                            If divFooter.InnerHtml <> "" Then
                                fp.WriteLine(divFooter.InnerHtml)
                            End If
                            fp.Close()
                            'Save the record
                            Dim str1 As String = repDesign.insertHTMLReport(txtHTMLreport.Text, Path, savedby, savedon, hidReporttype.Value, hidReportname.Value)
                            Dim sr As String = repDesign.trackHTML(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value, dept, client, lob, txtHTMLreport.Text)
                            '''''''''''''''Usertype check for track goes here:- By Suvidha

                            Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where HTMLFileName='" + txtHTMLreport.Text + "' and Action='Save as HTML'", conn)
                            'conn.Open()
                            cmm.ExecuteNonQuery()
                            'conn.Close()
                            '''''''''''''''Usertype check for track goes here:- By Suvidha
                            If (str1 = "1") Then
                                lblMsg.Text = "The HTML report Has Been Created Successfully."
                            Else
                                lblMsg.Text = "Error Occured While Creating The File. Please Try Again."
                            End If
                        Else
                            lblMsg.Text = "This HTML Report Already Exists. Please Supply Another Report Name"
                        End If
                    Else
                        lblMsg.Text = "Enter HTML Report Name"
                    End If
                End If
            End If
        Else
                Dim cmdnew = New SqlCommand("select localTable from WARSLOBTableMaster where TableName='" + tablename + "'", conn)
                Dim res As String
                res = cmdnew.ExecuteScalar().ToString()
            If (res.Equals("temp")) Then
                'If (Session("reportname") = "") Then
                '    lblMsg.Text = "Report Maynot Be Processed/Saved. Please Save The Report First"
                'Else
                If (txtHTMLreport.Text <> "") Then
                    ' code to generate and save HTML file
                    Dim b As Boolean = False
                    b = repDesign.CheckExistingHTMLReport(txtHTMLreport.Text)
                    If (b = False) Then
                        ' Generating the HTML Report
                        If divDetails.InnerHtml <> "" Then
                            Dim str As String = ""
                            Dim fp As StreamWriter
                            If Not Directory.Exists(Server.MapPath("UserSpace/" & Session("userid"))) Then
                                '<----------------------Creating Directory for partcular user--------------------------------->
                                Directory.CreateDirectory(Server.MapPath("UserSpace/" & Session("userid") & "/"))
                                '<----------------------End of Creating Directory for partcular user------------------------>
                            End If
                            '<------------------------End of Creating A main Directory--------------------------------------->
                            Path = "~/ReportDesigner/UserSpace/" & Session("userid") & "/" & txtHTMLreport.Text & ".html"
                            '<--------------------Creating a new text file---------------------------------->
                            fp = File.CreateText(Server.MapPath(Path))
                            If divHeader.InnerHtml <> "" Then
                                fp.WriteLine(divHeader.InnerHtml)
                            End If

                            fp.WriteLine(divDetails.InnerHtml)
                            If divFooter.InnerHtml <> "" Then
                                fp.WriteLine(divFooter.InnerHtml)
                            End If
                            fp.Close()
                            'Save the record
                            Dim str1 As String = repDesign.insertHTMLReport(txtHTMLreport.Text, Path, savedby, savedon, hidReporttype.Value, hidReportname.Value)
                            Dim sr As String = repDesign.trackHTML(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value, dept, client, lob, txtHTMLreport.Text)
                            '''''''''''''''Usertype check for track goes here:- By Suvidha

                            'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where HTMLFileName='" + txtHTMLreport.Text + "' and Action='Save as HTML'", conn)
                            'conn.Open()
                            'cmm.ExecuteNonQuery()
                            'conn.Close()
                            '''''''''''''''Usertype check for track goes here:- By Suvidha
                            If (str1 = "1") Then
                                lblMsg.Text = "The HTML report Has Been Created Successfully."
                                divmsgboxdeltable.Visible = True
                            Else
                                lblMsg.Text = "Error Occured While Creating The File. Please Try Again."
                            End If
                        Else
                            lblMsg.Text = "This HTML Report Already Exists. Please Supply Another Report Name"
                        End If
                    Else
                        lblMsg.Text = "Enter HTML Report Name"
                    End If
                End If
                'End If
            Else
            If (Session("reportname") = "") Then
                lblMsg.Text = "Report Maynot Be Processed/Saved. Please Save The Report First"
            Else
                If (txtHTMLreport.Text <> "") Then
                    ' code to generate and save HTML file
                    Dim b As Boolean = False
                    b = repDesign.CheckExistingHTMLReport(txtHTMLreport.Text)
                    If (b = False) Then
                        ' Generating the HTML Report
                        If divDetails.InnerHtml <> "" Then
                            Dim str As String = ""
                            Dim fp As StreamWriter
                            If Not Directory.Exists(Server.MapPath("UserSpace/" & Session("userid"))) Then
                                '<----------------------Creating Directory for partcular user--------------------------------->
                                Directory.CreateDirectory(Server.MapPath("UserSpace/" & Session("userid") & "/"))
                                '<----------------------End of Creating Directory for partcular user------------------------>
                            End If
                            '<------------------------End of Creating A main Directory--------------------------------------->
                            Path = "~/ReportDesigner/UserSpace/" & Session("userid") & "/" & txtHTMLreport.Text & ".html"
                            '<--------------------Creating a new text file---------------------------------->
                            fp = File.CreateText(Server.MapPath(Path))
                            If divHeader.InnerHtml <> "" Then
                                fp.WriteLine(divHeader.InnerHtml)
                            End If

                            fp.WriteLine(divDetails.InnerHtml)
                            If divFooter.InnerHtml <> "" Then
                                fp.WriteLine(divFooter.InnerHtml)
                            End If
                            fp.Close()
                            'Save the record
                            Dim str1 As String = repDesign.insertHTMLReport(txtHTMLreport.Text, Path, savedby, savedon, hidReporttype.Value, hidReportname.Value)
                            Dim sr As String = repDesign.trackHTML(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value, dept, client, lob, txtHTMLreport.Text)
                            '''''''''''''''Usertype check for track goes here:- By Suvidha

                            'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where HTMLFileName='" + txtHTMLreport.Text + "' and Action='Save as HTML'", conn)
                            'conn.Open()
                            'cmm.ExecuteNonQuery()
                            'conn.Close()
                            '''''''''''''''Usertype check for track goes here:- By Suvidha
                            If (str1 = "1") Then
                                lblMsg.Text = "The HTML report Has Been Created Successfully."
                            Else
                                lblMsg.Text = "Error Occured While Creating The File. Please Try Again."
                            End If
                        Else
                            lblMsg.Text = "This HTML Report Already Exists. Please Supply Another Report Name"
                        End If
                    Else
                        lblMsg.Text = "Enter HTML Report Name"
                    End If
                End If
            End If
        End If
            conn.Close()
            cmdnew.Dispose()
        End If
    End Sub
    ''' <summary>
    ''' Before exporting the report to Excel, record the action
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgXls.Click
        'If (hidReportname.Value = "") Then
        '    lblMsg.Text = "Report maynot be processed/saved. Please save the report first"
        'Else
        'savedby = Session("userid")
        'savedon = System.DateTime.Today.Date
        hidReportname.Value = Trim(Request("hidReportname"))
        If Not Directory.Exists(Server.MapPath("UserSpace/" & Session("userid"))) Then
            '<----------------------Creating Directory for partcular user--------------------------------->
            Directory.CreateDirectory(Server.MapPath("UserSpace/" & Session("userid") & "/" & Session("userid")))
            '<----------------------End of Creating Directory for partcular user------------------------>
        End If
        '<------------------------End of Creating A main Directory--------------------------------------->
        ' finally export to XLS format
        Path = "UserSpace/" & Session("userid") & "/" & Session.SessionID & ".xls"
        Dim fp As StreamWriter
        fp = File.CreateText(Server.MapPath(Path))
        fp.WriteLine(divDetails.InnerHtml)
        fp.Close()
        Try
            Dim sr As String = repDesign.trackXLS(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value)

            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + hidReportname.Value + "' and Action='Export to XLS'", conn)
            'conn.Open()
            'cmm.ExecuteNonQuery()
            'conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            ' Save the XLS at desired location
            Dim str As String = ""
            str = "<script laungauge=Javascript>"
            str = str + "xls();"
            str = str + "</script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "XLS", str)
            lblMsg.Text = "Export in Excel has been successfully completed"
        Catch ex As Exception
            lblMsg.Text = "Error occured.Please try again"
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
    ''' <summary>
    ''' to redirect to Graphical Presentation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgChart_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChart.Click
        If hidReportname.Value = "" Then
            lblMsg.Text = "Please Save The Report First."
            Exit Sub
        End If
        Dim str = "<script language='Javascript'>"
        str = str + "window.open('viewGraphs.aspx?repnm=" + hidReportname.Value + "','ViewGraph');"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ViewGraph", str)
    End Sub
    Public Sub callParent()
        Dim str As String = ""
        str = "<script launguage=Javascript>"
        str = str + "document.forms[0].target='_top';"
        str = str + "document.forms[0].action='../Graphicalpresentation/graphdata.aspx?currentreport=" + hidReportname.Value + "';"
        str = str + "document.forms[0].submit();"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "assignToparent", str)
    End Sub
    Protected Sub imgAnalysis_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnalysis.Click
        Dim str = "<script language='Javascript'>"
        str = str + "window.open('../DataAnalysis/AnalsisOnTempTable.aspx','DataAnalysis');"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "DataAnalysis", str)
    End Sub
    Private Sub cmdyesdt_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles cmdyesdt.Command
        Dim str = "<script language='Javascript'>"
        str = str + "window.open('../DataAnalysis/AnalsisOnTempTable.aspx','DataAnalysis');"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "DataAnalysis", str)
        divmsgboxdeltable.Visible = False
    End Sub
    Private Sub cmdnodt_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdnodt.Click
        Dim tablename As String = Trim(Request("hidTables"))
        Dim cmdnew = New SqlCommand("delete WARSLOBTableMaster where TableName='" + tablename + "'", conn)
        conn.Open()
        cmdnew.ExecuteNonQuery()
        cmdnew.Dispose()
        Dim cmdnew2 = New SqlCommand("drop table " + tablename + "", conn)
        'conn.Open()
        cmdnew2.ExecuteNonQuery()
        cmdnew2.Dispose()
        conn.Close()
    End Sub
   End Class
