Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.HtmlTextWriter
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader
Partial Class ReportDesigner_nSummarizedReport
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
    Dim ex345 As String = ""
    Public Path As String = ""
    Dim savedby As String = ""
    Dim savedon As Date
    Dim repscope As String = ""
    Dim emailalertquery As String = ""
    Dim centercount = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True


        If Me.IsPostBack = False Then

            Dim cmdget As New SqlCommand
            cmdget = New SqlCommand("select distinct menuid from nlvl_menu_rights where userid='" + Session("userid").ToString + "' and MenuId='2'", conn)
            conn.Open()
            Dim dr As SqlDataReader
            dr = cmdget.ExecuteReader()
            If (dr.HasRows) Then
                imgChart.Visible = True
            Else
                imgChart.Visible = False
            End If
            dr.Close()
            conn.Close()
            cmdget.Dispose()

            If (Trim(Request("txtStartdate")) <> "") Then
                Me.hidStart.Value = Trim(Request("txtStartdate"))
            End If
            If (Trim(Request("txtEnddate")) <> "") Then
                Me.hidEnd.Value = Trim(Request("txtEnddate"))
            End If
            If (Trim(Request("hidColorcondition")) <> "") Then
                hidColorcondition.Value = Trim(Request("hidColorcondition"))
            End If

            hidReportscope.Value = Trim(Request("hidReportscope"))
            hidDepartment.Value = Trim(Request("hidDepartment"))
            hidClient.Value = Trim(Request("hidClient"))
            hidLob.Value = Trim(Request("hidLob"))
            hidReportname.Value = Trim(Request("hidReportname"))
            hidReporttype.Value = Trim(Request("hidReporttype"))
            If Trim(Request("hidSubtotal")) <> "" Then
                hidSubtotal.Value = Trim(Request("hidSubtotal"))
            End If
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
                Dim k = Replace(Trim(Request("hidHPos")), vbNewLine, "")
                hpos = Split(k, "~")
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
            If (Trim(Request("hidDetailsformat")) <> "") Then
                Dim hStyle As String() = Split(Trim(Request("hidDetailsformat")), ";")
                For g = 0 To hStyle.Length - 1
                    Dim st As String() = hStyle(g).Split(":")
                    divDetails.Style.Add(st(0), st(1))
                Next
            End If
            If Trim(Request("hidDPos")) <> "" Then
                If (Trim(Request("hidTables")) = "") Then
                    aspnet_msgbox("No Table Found")
                    Exit Sub
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
                Me.hidDpos.Value = Replace(Me.hidDpos.Value, vbNewLine, "")
                hpos = Split(Trim(Me.hidDpos.Value), "~")

                For i = 0 To hpos.Length - 1

                    Dim final As String = ""
                    Dim temp As String = ""
                    temp = hpos(i)
                    If (hpos(i).Contains(" AS ") = False) Then
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
                    Dim jk = Replace(Trim(Request("hidFformat")), vbNewLine, "")
                    hfor = Split(jk, "~")
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
    End Sub
    ''' <summary>
    ''' Generate Header Elements 
    ''' </summary>
    ''' <param name="obj">objectName</param>
    ''' <param name="pos">objectPosition</param>
    ''' <remarks></remarks>
    Public Sub gen_elm(ByVal obj, ByVal pos)
        centercount = 1

        Dim st = Split(pos, "#@#")
        Dim st1 = Split(st(1), ",")
        Dim lbl As New TextBox 'Label
        Dim Assolbl As New Label 'Assosiated control
        Assolbl.ID = "lbl" & obj
        Assolbl.AssociatedControlID = obj
        lbl.ID = obj
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
        '' apply basic header formatting to the element
        If (Trim(Request("hidHeaderformat")) <> "") Then
            Dim g = 0
            Dim hStyle As String() = Split(Trim(Request("hidHeaderformat")), ";")
            For g = 0 To hStyle.Length - 1
                Dim st5 As String() = hStyle(g).Split(":")
                lbl.Style.Add(st5(0), st5(1))
            Next
        End If
        '''''''''''
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
        j = Me.divHeader.Controls.Count
        For i = 0 To j - 1
            If (Me.divHeader.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox
                lb = Me.divHeader.Controls.Item(i)
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
                Dim lb As TextBox
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
                Dim lb As TextBox
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
        Dim str As String = Replace(formula, "@Date1@", Me.hidStart.Value)
        str = Replace(formula, "@Date2@", Me.hidEnd.Value)
        Dim fnObj As New Functions
        Dim val As String = ""
        Try
            val = repDesign.get_Value(str)
        Catch ex As Exception
            aspnet_msgbox(ex.Message)
        End Try

        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divHeader.Controls.Count
        For i = 0 To j - 1
            If (Me.divHeader.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox
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
        'Dim valcc As String()
        j = Me.divHeader.Controls.Count
        Dim val = ""
        For i = 0 To j - 1
            Dim d As String() = Split(arr, "~")
            For h = 0 To d.Length - 1
                Dim f As String() = Split(d(h), "@")
                If (Me.divHeader.Controls.Item(i).ID = f(0)) Then
                    Dim lb As TextBox
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
        lbl.ID = obj
        Assolbl.ID = "lbl" & obj
        Assolbl.AssociatedControlID = obj
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
                Dim lb As TextBox
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
        Dim str As String = Replace(formula, "@Date1@", Me.hidStart.Value)
        str = Replace(formula, "@Date2@", Me.hidEnd.Value)
        Dim fnObj As New Functions
        Dim val As String = ""
        Try
            val = repDesign.get_Value(str)
        Catch ex As Exception
            aspnet_msgbox(ex.Message)
        End Try

        Dim j As Integer
        Dim i As Integer
        i = 0
        j = 0
        j = Me.divFooter.Controls.Count
        For i = 0 To j - 1
            If (Me.divFooter.Controls.Item(i).ID = obj) Then
                Dim lb As TextBox
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
                    Dim lb As TextBox
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
        'Dim strColumn = Replace("agents.age as agents$age~agents$name as agents$name", "$", ",")
        Dim columnnamearr As String() = Split(Me.hidDpos.Value, "~")
        ' Dim columnnamearr = Split("agents$age~agents$name", "~")

        Dim tbl As String = ""
        tbl = Me.hidTables.Value
        'tbl = "agents"
        Dim sqlString As String = ""
        Dim Sstring As String = ""
        Dim whrString As String = ""
        Dim whrString1 As String = ""
        Dim forString As String = ""
        Dim groupby As String = ""
        Dim orderby As String = ""
        Dim having As String = ""
        Sstring = "Select " & strColumn & " from " & tbl & ""
        Sstring = Replace(Sstring, vbNewLine, "")
        'startrana
        emailalertquery = Sstring
        'ranastop

        'If (Trim(Request("hidWhere")) <> "") Then
        '    Dim rep As String = Replace(Trim(Request("hidWhere")), "~", ",")
        '    Dim rep2 As String = Replace(rep, "$", ".")
        '    whrString = " where " + rep2
        '    Sstring = Sstring + whrString
        'Else
        '    If Me.hidStart.Value <> "" And Me.hidEnd.Value <> "" Then
        '        If (hidDatetable.Value <> "") Then
        '            whrString = " where " + hidDatetable.Value + ".Date between '" + Me.hidStart.Value + "' and '" + Me.hidEnd.Value + "'"
        '        Else
        '            Dim tblEx = Split(Me.hidTables.Value, ",")
        '            whrString = " where " + tblEx(0) + ".Date between '" + Me.hidStart.Value + "' and '" + Me.hidEnd.Value + "'"
        '        End If

        '        Sstring = Sstring + whrString
        '    End If
        'End If
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

        'If (Trim(Request("hidGroupby")) <> "") Then
        '    Dim temp As String = Replace(Trim(Request("hidGroupby")), "~", ",")
        '    Dim temp2 As String = Replace(temp, "$", ".")
        '    groupby = " Group By " + temp2
        '    Sstring = Sstring + " Group By " + temp2
        'End If

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
                            'tgp = tgp + " desc"
                            tgp = Request("hidOrderby")
                        End If
                        If LCase(Trim(Request("hidOrderby"))).Contains("asc") Then
                            ' tgp = tgp + " asc"
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
        'If (Trim(Request("hidOrderby")) <> "") Then
        '    Dim temp As String = Replace(Trim(Request("hidOrderby")), "~", ",")
        '    Dim temp2 As String = Replace(temp, "$", ".")
        '    orderby = " Order By " + temp2
        '    Sstring = Sstring + " Order By " + temp2
        'End If




        ' find the column names to be formatted
        Dim arr As String()

        Dim len = 0
        ''
        '' find out volumns on which color condition is to apply
        'Dim arrC As String()
        Dim lenC As Integer = 0
        Dim colCond As String = ""
        Dim valCC As String = ""
        Dim cc = 0
        'If Trim(hidColorcondition.Value) <> "" Then
        '    arrC = Split(hidColorcondition.Value, "~")
        '    lenC = arrC.Length

        '    For cc = 0 To lenC - 1
        '        Dim sp0 As String() = Split(arrC(cc), "@")
        '        Dim q = 0
        '        Dim sp1 As String() = Split(sp0(1), "^")
        '        Dim valC As String = ""
        '        If sp1(0) = "formula" Then
        '            'Dim k = repDesign.get_Value(sp1(2))
        '            valC = sp0(0) + "@formula@" + sp1(1) + "^" + sp1(2)   '' val=tablecolumn@SQL^format"
        '        Else
        '            valC = sp0(0) + "@condition@" + sp1(1) + "^" + sp1(2) + "^" + sp1(3)
        '        End If
        '        If valCC = "" Then
        '            valCC = valC
        '        Else
        '            valCC = valCC + "~" + valC
        '        End If
        '    Next
        'End If
        '' 
        Dim v = 1
        Dim v2 = 1
        If (Sstring.Contains("@Date1@") = True And Me.hidStart.Value = "") Then
            v = 0
        End If

        If (Sstring.Contains("@Date2@") = True And Me.hidEnd.Value = "") Then
            v2 = 0
        End If
        If (v = 0) Then
            aspnet_msgbox("Please supply the start date")
        ElseIf (v2 = 0) Then
            aspnet_msgbox("Please supply the end date")
        Else
            If Trim(hidDformat.Value) <> "" Then
                arr = Split(hidDformat.Value, "~")
            End If
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


            Dim st1 As String = Replace(Sstring, "@Date1@", Me.hidStart.Value)
            Dim st2 As String = Replace(st1, "@Date2@", Me.hidEnd.Value)
            sqlString = st2
            whrString1 = Replace(whrString, "@Date1@", Me.hidStart.Value)
            whrString1 = Replace(whrString1, "@Date2@", Me.hidEnd.Value)

            Dim xtb As String = ""
            Dim style As String
            style = ""
            style = "width:100%;"
            ' Dim tbStyle = "border-top:gray 1px solid;border-left:gray 1px solid;border-right:gray 1px solid;border-bottom:gray 1px solid;"
            Dim tbStyle = "border-top:blue 1px solid;border-left:blue 1px solid;border-right:blue 1px solid;border-bottom:blue 1px solid;"
            Dim color = ""
            If (Trim(Request("hidDetailsformat")) <> "") Then
                tbStyle = Trim(Request("hidDetailsformat"))
                Dim abc = Split(tbStyle, ";")
                color = abc(2)
            End If
            'create table in center when header element found otherwise its in left of details DIV
            If centercount = 1 Then
                divDetails.Style.Add("text-align", "center")
            Else
                divDetails.Style.Add("text-align", "left")

            End If
            ''''''' Add Details Pane Header''''''''
            xtb = "<table  border=1  style='" + tbStyle + "' cellspacing=0 cellpadding=2 ><tr > "
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
                    ' xtb = xtb & "<td  nowrap align=center><b>" & Trim(columnnamearr(i)) & "</b></td>"
                End If

                '' to set column format
                Dim l As Integer = 0
                Dim styl = ""

                If Trim(hidDformat.Value) <> "" Then
                    styl = applyColumnformat(columnnamearr(i), arr)
                    If styl.Contains("background-color:#ffffff;") = True And styl.Contains("color:#000000") = True Then
                        styl = Replace(styl, "background-color:#ffffff;", "background-color:LightGrey;")
                        styl = Replace(styl, "color:#000000;", "")
                    End If
                End If
                If styl = "" Then

                    styl = "background-color:LightGrey;font-size:10pt;"
                End If

                '' column format ends

                xtb = xtb & "<td  wrap   style='" + styl + "'><b>" & val & "</b></td>"


            Next

            ' to draw a horizontal line between the column headings and columns od details pane
            'Dim sp = columnnamearr.Length
            'xtb = xtb & "</tr>"
            'xtb = xtb & "<tr><td colspan="
            'xtb = xtb & sp
            'xtb = xtb & ">"
            'xtb = xtb + "<hr color="
            'xtb = xtb & detailsForecolor
            'xtb = xtb & "></td></tr>"
            Me.divDetails.InnerHtml = Me.divDetails.InnerHtml + xtb
            ''''''''''''' Details Pane Header ends'''''''''''
            ''''''''' Add Rows to the Details Pane''''''''''''''''''''
            xtb = ""
            Dim str2 = tgp
            'Dim str2 = Replace(Trim(Request("hidGroupby")), "~", ",")
            Dim str3() = Split(str2, ",")
            Dim hl = str3.Length
            Dim sumArr(hl) ' declare an array equal to the group items 
            Dim so = 0
            For so = 0 To hl - 1
                sumArr(so) = ""
            Next

            xtb = ""
            If hidSubtotal.Value = "0" Or hidSubtotal.Value = "" Then
                hidSubtotal.Value = hl - 1
            Else
                If CInt(hidSubtotal.Value) > hl - 1 Then
                    hidSubtotal.Value = hl - 1
                End If
            End If
            Dim myDataset As New DataSet()
            Dim myAdapter As New SqlDataAdapter()
            Try
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
                        Dim nxt = 0
                        Dim tmp = ""
                        Dim b = False
                        Dim innerI As Integer = 0
                        For k = 0 To columnnamearr.Length - 1
                            Dim p As Integer = 0
                            Dim q As Integer = 0
                            Dim val = ""
                            If (columnnamearr(k).Contains(" AS ") = True) Then
                                Dim s1 = columnnamearr(k).IndexOf(" AS ")
                                Dim p1 = columnnamearr(k).Substring(s1 + 3)
                                '''' if [formula] encountered
                                If p1.contains("[") Then
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
                            Dim styltd = ""
                            Dim styl As String = ""
                            If Trim(colorCon) <> "" Then
                                styl = applyColorcondition(columnnamearr(k), val, columnnamearr, colorCon, myDataset.Tables(0).Rows(myi))

                            End If
                            '' to set column format
                            If Trim(hidDformat.Value) <> "" And styl = "" Then

                                styl = applyColumnformat(columnnamearr(k), arr)
                            End If
                            'column format ends

                            styltd = styl

                            If styltd = "" Then
                                styltd = color
                            End If
                            If (innerI < hl - 1 And val <> "") Then
                                If (val = sumArr(innerI)) Then
                                    xtb = xtb & "<td  wrap   style='" & styltd & "'>&nbsp;</td>"
                                Else
                                    If (sumArr(innerI) = "") Then
                                        sumArr(innerI) = val
                                    Else
                                        Dim w = innerI 'CInt(hidSubtotal.Value) - 1 'innerI--- current td
                                        Dim w1 = w 'CInt(hidSubtotal.Value) - 1 'innerI
                                        Dim bn = 0
                                        Dim jw = CInt(hidSubtotal.Value) - 1 'hl - 2
                                        Dim styl1 = ""
                                        Dim styltd1 = ""
                                        Dim cou = CInt(hidSubtotal.Value) - 1 'hl - 2
                                        Dim po = 0
                                        If w <= jw Then '' if level is < innerI then don't form subtotal

                                            For jw = CInt(hidSubtotal.Value) - 1 To w Step -1

                                                '' goto  groupby element
                                                For w1 = innerI + po To CInt(hidSubtotal.Value) - 2 'hl - 3
                                                    styl1 = ""
                                                    '' to set column format
                                                    If Trim(hidDformat.Value) <> "" Then
                                                        Dim iop = columnnamearr(w1)
                                                        styl1 = applyColumnformat(iop, arr)
                                                    End If
                                                    'column format ends
                                                    styltd1 = styl1
                                                    xtb = xtb & "<td  wrap   style='" & styltd1 & "'>&nbsp;</td>"
                                                Next
                                                '' summarize  grpby
                                                'If cou <= CInt(hidSubtotal.Value) - 1 Then


                                                styl1 = ""

                                                '' to set column format

                                                If Trim(hidDformat.Value) <> "" Then
                                                    Dim iop = columnnamearr(cou)
                                                    styl1 = applyColumnformat(iop, arr)
                                                End If
                                                'column format ends
                                                styltd1 = styl1
                                                If styltd1 = "" Then
                                                    styltd1 = color
                                                End If
                                                xtb = xtb & "<td  wrap   style='" & styltd1 & "'><b>" + sumArr(cou) + "</b></td>"

                                                '' fill another grpby

                                                For w1 = cou + 1 To hl - 1
                                                    '' to set column format
                                                    styl1 = ""
                                                    If Trim(hidDformat.Value) <> "" Then
                                                        Dim iop = columnnamearr(w1)
                                                        styl1 = applyColumnformat(iop, arr)
                                                    End If
                                                    'column format ends
                                                    styltd1 = styl1
                                                    If styltd1 = "" Then
                                                        styltd1 = color
                                                    End If
                                                    xtb = xtb & "<td  wrap   style='" & styltd1 & "'>&nbsp;</td>"
                                                Next

                                                '' prepare where clause
                                                Dim df = 0
                                                Dim wclause = ""
                                                For df = 0 To jw
                                                    Dim tm1 As String = Replace(columnnamearr(df), "$", ".")

                                                    If tm1.Contains(" AS ") = True Then
                                                        Dim tm67 = Split(tm1, " AS ")
                                                        tm1 = tm67(0)
                                                    End If
                                                    If (wclause = "") Then
                                                        wclause = tm1 + "='" + sumArr(df) + "'"
                                                    Else
                                                        wclause = wclause + " and " + tm1 + "='" + sumArr(df) + "'"
                                                    End If
                                                Next
                                                sumArr(cou) = ""
                                                '' add main where clause
                                                If (Trim(whrString1) <> "") Then
                                                    Dim gyh = Replace(whrString1, " where", "")
                                                    wclause = wclause + " and " + gyh
                                                End If
                                                '''''''

                                                '' fill normal columns

                                                For bn = hl To columnnamearr.Length - 1

                                                    Dim tm1 As String = Replace(columnnamearr(bn), "$", ".")
                                                    '' to set column format
                                                    styl1 = ""
                                                    If Trim(hidDformat.Value) <> "" Then
                                                        Dim iop = columnnamearr(bn)
                                                        styl1 = applyColumnformat(iop, arr)
                                                    End If
                                                    'column format ends
                                                    Dim sql = "Select " + tm1 + " from " + Me.hidTables.Value + " where " + wclause
                                                    Dim sql1 = repDesign.GetFormula(sql)
                                                    styltd1 = styl1
                                                    If styltd1 = "" Then
                                                        styltd1 = color
                                                    End If
                                                    xtb = xtb & "<td  wrap style='" + styltd1 + "'><b>" & sql1 & "</b></td>"
                                                Next
                                                xtb = xtb & "</tr><tr>"
                                                ' End If
                                                '' create nxt row

                                                For bn = 0 To innerI - 1
                                                    '' to set column format
                                                    styl1 = ""
                                                    If Trim(hidDformat.Value) <> "" Then
                                                        Dim iop = columnnamearr(bn)
                                                        styl1 = applyColumnformat(iop, arr)
                                                    End If
                                                    'column format ends
                                                    styltd1 = styl1
                                                    If styltd1 = "" Then
                                                        styltd1 = color
                                                    End If
                                                    xtb = xtb & "<td  wrap   style='" & styltd1 & "'>&nbsp;</td>"

                                                Next

                                                po = po + 1
                                                cou = cou - 1
                                            Next
                                        End If
                                    End If '' if level is < innerI ends
                                    '' '' to set column format  (style value is being retrived from the prior info)
                                    ''Dim styl2 = ""
                                    ''Dim styltd2 = ""
                                    ''If Trim(hidDformat.Value) <> "" Then
                                    ''    Dim iop = columnnamearr(k)
                                    ''    styl2 = applyColumnformat(iop, arr)
                                    ''End If
                                    ' ''column format ends
                                    ''styltd2 = styl2
                                    ''xtb = xtb & "<td  nwrap style='" & styltd2 & "'>" & val & "</td>"
                                    xtb = xtb & "<td  nwrap style='" & styltd & "'>" & val & "</td>"
                                    sumArr(innerI) = val

                                End If
                            ElseIf val = "" Then
                                xtb = xtb & "<td  wrap  style='" & styltd & "'>NULL</td>"
                            Else
                                xtb = xtb & "<td  wrap style='" & styltd & "'>" & val & "</td>"
                            End If
                            innerI = innerI + 1
                        Next
finish:
                        xtb = xtb & "</tr>"
                    Next
                Else
                    divDetails.InnerHtml = ""
                    Me.lblMsg.Text = "No Records Found."
                End If
                If divDetails.InnerHtml <> "" Then
                    '' design last summation
                    Dim var = 0
                    xtb = xtb & "<tr>"
                    Dim po1 = 0
                    Dim cou1 = CInt(hidSubtotal.Value) - 1 ' hl - 2
                    For var = 0 To CInt(hidSubtotal.Value) - 1 'hl - 2
                        Dim w1 = 0
                        Dim styl1 = ""
                        Dim styltd1 = ""
                        For w1 = 0 To CInt(hidSubtotal.Value) - 2 + po1
                            '' to set column format
                            styl1 = ""
                            If Trim(hidDformat.Value) <> "" Then
                                Dim iop = columnnamearr(w1)
                                styl1 = applyColumnformat(iop, arr)
                            End If
                            'column format ends
                            styltd1 = styl1
                            If styltd1 = "" Then
                                styltd1 = color
                            End If
                            xtb = xtb & "<td  wrap style='" & styltd1 & "'>&nbsp;</td>"
                        Next
                        '' summarize  grpby
                        '' to set column format
                        styl1 = ""
                        If Trim(hidDformat.Value) <> "" Then
                            Dim iop = columnnamearr(cou1)
                            styl1 = applyColumnformat(iop, arr)
                        End If
                        'column format ends
                        styltd1 = styl1
                        If styltd1 = "" Then
                            styltd1 = color
                        End If
                        xtb = xtb & "<td  wrap style='" & styltd1 & "'><b>" + sumArr(cou1) + "</b></td>"

                        '' fill another grpby
                        For w1 = cou1 + 1 To hl - 1
                            '' to set column format
                            styl1 = ""
                            If Trim(hidDformat.Value) <> "" Then
                                Dim iop = columnnamearr(w1)
                                styl1 = applyColumnformat(iop, arr)
                            End If
                            'column format ends
                            styltd1 = styl1
                            If styltd1 = "" Then
                                styltd1 = color
                            End If
                            xtb = xtb & "<td  wrap style='" & styltd1 & "'>&nbsp;</td>"
                        Next
                        '' prepare where clause
                        Dim df = 0
                        Dim wclause = ""
                        For df = 0 To hl - 2 + po1
                            Dim tm1 As String = Replace(columnnamearr(df), "$", ".")
                            If tm1.Contains(" AS ") = True Then
                                Dim tm67 = Split(tm1, " AS ")
                                tm1 = tm67(0)
                            End If
                            If (wclause = "") Then
                                wclause = tm1 + "='" + sumArr(df) + "'"
                            Else
                                wclause = wclause + " and " + tm1 + "='" + sumArr(df) + "'"
                            End If
                        Next
                        sumArr(cou1) = ""
                        '' add main where clause
                        If (Trim(whrString1) <> "") Then
                            Dim gyh = Replace(whrString1, " where", "")
                            wclause = wclause + " and " + gyh
                        End If
                        '''''''
                        '' fill normal columns
                        Dim bn = 0
                        For bn = hl To columnnamearr.Length - 1
                            Dim tm1 As String = Replace(columnnamearr(bn), "$", ".")
                            Dim sql = "Select " + tm1 + " from " + Me.hidTables.Value + " where " + wclause
                            Dim sql1 = repDesign.GetFormula(sql)

                            '' to set column format
                            styl1 = ""
                            If Trim(hidDformat.Value) <> "" Then
                                Dim iop = columnnamearr(bn)
                                styl1 = applyColumnformat(iop, arr)
                            End If
                            'column format ends
                            styltd1 = styl1
                            If styltd1 = "" Then
                                styltd1 = color
                            End If
                            xtb = xtb & "<td  wrap style='" & styltd1 & "'><b>" & sql1 & "</b></td>"
                        Next
                        xtb = xtb + "</tr><tr>"
                        '' create nxt row


                        po1 = po1 - 1
                        cou1 = cou1 - 1
                    Next
                    '' design final total
                    Dim styl12 = "background-color:LightGrey;font-size:10pt;"
                    For cou1 = 0 To hl - 2

                        xtb = xtb & "<td  wrap style='" & styl12 & "'>&nbsp;</td>"
                    Next
                    xtb = xtb & "<td  wrap style='" & styl12 & "'><b>Total</b></td>"
                    Dim wcl = ""
                    If (Trim(whrString1) <> "") Then
                        wcl = whrString1

                    End If
                    For cou1 = hl To columnnamearr.Length - 1
                        Dim tm1 As String = Replace(columnnamearr(cou1), "$", ".")
                        Dim sql = "Select " + tm1 + " from " + Me.hidTables.Value + " " + wcl
                        Dim sql1 = repDesign.GetFormula(sql)
                        xtb = xtb & "<td  wrap style='" & styl12 & "'><b>" & sql1 & "</b></td>"
                    Next
                    ''
                    xtb = xtb + "</tr>"
                    xtb = xtb & "</table>"
                    Me.divDetails.InnerHtml = Me.divDetails.InnerHtml + xtb
                    Session("strFinalData") = Me.divDetails.InnerHtml
                End If
            Catch ex As Exception
                divDetails.InnerHtml = ""
                Me.lblMsg.Text = "The Report Cannot Be Displayed: " + ex.Message
            End Try

            ''''''''''''''''''



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
            Session("wheredata") = whrString1
            Session("groupdata") = groupby
            Session("orderdata") = orderby
            Session("having") = having
            '''''''''''''''''''''
            sqlString90 = "Select " + sqlString90 + " from " + tb + whrString1 + groupby + having + orderby
            'ranjit  changed earlier line was : Session("Queryname") = sqlString90 + "#" + Me.hidTables.Valu + "#" + Me.hidReportname.Value + "" also same in showdata.aspx

            Session("Queryname") = sqlString90 + "#" + Me.hidTables.Value
            '+ "#" + Me.hidReportname.Value + ""
            '''''''''''''''''''''''''''
            'Pawan Add this line
            Session("QueryforAlerts") = emailalertquery + "#" + Me.hidTables.Value + "#" + Me.hidReportname.Value + "$" + hidStart.Value + "$" + hidEnd.Value + "!" + ooop
            '''''''''''''''''''''''''''

        End If
        '''''''''''''''''''''''''''
    End Sub
    ''' <summary>
    ''' To apply color condition on details elements
    ''' </summary>
    ''' <remarks></remarks>
    Function applyColorcondition(ByVal colum, ByVal colval, ByVal columnnamearr, ByVal acolorCon, ByVal drow)
        Dim datarw As DataRow
        datarw = drow

        Dim styl = ""
        Try
            Dim d As String() = Split(Trim(acolorCon), "~")
            Dim h As Integer = 0
            For h = 0 To d.Length - 1

                Dim f As String() = Split(d(h), "@#@")
                Dim wifi = Split(colum, " AS ")
                Dim aliasn = Replace(colum, "$", ".")
                If (wifi.length > 1) Then
                    Dim val10 As String = Replace(wifi(1), "[", "")
                    val10 = Replace(val10, "]", "")
                    aliasn = Trim(val10)
                End If
                If (f(0).Contains(aliasn)) Then


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
                                s(1) = Replace(UCase(s(1)), tm, CDbl(datarw.Item(toread)))
                            Catch ex As Exception

                                If IsDBNull(datarw.Item(toread)) Then
                                    s(1) = Replace(UCase(s(1)), tm, "'" + datarw.Item(toread) + "'")
                                Else
                                    s(1) = Replace(UCase(s(1)), tm, "'" + UCase(datarw.Item(toread)) + "'")
                                End If

                            End Try

                        Next

                        '''''''''''''''''''''''''''''''''''''''''''''''
                        Dim a = repDesign.chkCondition(s(1))
                        If a = True Then
                            styl = s(2)
                            GoTo finished
                        End If
                    Else



                        ''''convert to definite datatype
                        Dim tochk
                        Dim tochk1
                        Try
                            tochk = CDbl(colval)
                            tochk1 = CDbl(s(2))
                        Catch ex As Exception

                            tochk = colval.ToString()
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
                            GoTo finished
                        End If
                    End If
                    '  Next
                End If
            Next
            ' Next
        Catch ex As Exception
            lblMsg.Text = "Color Condition Could Not Be Applied Due To Syntax Error In The Condition"
        End Try
finished:
        Return styl
    End Function
    ''' <summary>
    ''' To apply column format on details elements
    ''' </summary>
    ''' <remarks></remarks>
    Function applyColumnformat(ByVal colm, ByVal formt)
        Dim styltd = ""
        Dim l As Integer = 0
        Dim len = formt.Length
        For l = 0 To len - 1
            Dim bvnhj = formt(l)
            bvnhj = Replace(bvnhj, vbNewLine, "")
            Dim s1 = Split(colm, " AS ")
            Dim p1 = colm
            If s1.length > 1 Then
                p1 = s1(1)
            End If

            Dim val0 As String = Replace(Trim(p1), "[", "")
            val0 = Replace(val0, "]", "")
            Dim toread1 = Trim(val0)
            Dim st As String() = Split(bvnhj, ">")
            If (st(0) = Replace(toread1, "$", ".") Or st(0) = Replace(toread1, "$", ".")) Then
                styltd = st(1)
                Exit For
            End If
        Next
        Return styltd
    End Function
    ''' <summary>
    ''' Asp MsgBox
    ''' </summary>
    ''' <param name="message">messageTopass</param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

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

        hidReportname.Value = Trim(Request("hidReportname"))
        hidReporttype.Value = Trim(Request("hidReporttype"))
        If (hidReportname.Value = "") Then
            lblMsg.Text = "Report maynot be processed/saved. Please save the report first"
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
                            ' Dim sr As String = repDesign.trackHTML(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value, hidDepartment.Value, hidClient.Value, hidLob.Value, txtHTMLreport.Text)
                            Dim sr As String = repDesign.trackHTML(Session("userid"), savedon, hidReportscope.Value, hidReportname.Value, dept, client, lob, txtHTMLreport.Text)
                            '''''''''''''''Usertype check for track goes here:- By Suvidha

                        'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where HTMLFileName='" + txtHTMLreport.Text + "' and Action='Save as HTML'", conn)
                        'conn.Open()
                        'cmm.ExecuteNonQuery()
                        'conn.Close()
                            '''''''''''''''Usertype check for track goes here:- By Suvidha
                            If (str1 = "1") Then
                                lblMsg.Text = "The HTML report has been created successfully"
                            Else
                                lblMsg.Text = "Error occured while creating the file. Please try again."
                            End If
                        Else
                            lblMsg.Text = "This HTML Report already exists. Please supply another report name"
                        End If
                    Else
                        lblMsg.Text = "Enter HTML report name"
                    End If
                End If
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

    Protected Sub imgChart_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChart.Click
        If hidReportname.Value = "" Then
            lblMsg.Text = "Please Save The Report First."
            Exit Sub
        End If

        '  Dim path As String
        'Dim rdr As SqlDataReader
        ' set the source of the data for the repeater control and bind it 
        ' Dim i As Integer

        'Dim strQuery As String = "select imageaddress from idmsimagetable where queryname='" + hidReportname.Value + "'"
        'Dim cmd As New SqlCommand(strQuery, conn)
        'conn.Open()
        'rdr = cmd.ExecuteReader()
        'Dim stat As Boolean
        'stat = rdr.HasRows
        'If (stat = True) Then
        Dim str = "<script language='Javascript'>"
        str = str + "window.open('viewGraphs.aspx?repnm=" + hidReportname.Value + "','ViewGraph');"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ViewGraph", str)
        'Else
        'callParent()
        'Response.Redirect("../Graphicalpresentation/graphdata.aspx?currentreport=" & hidReportname.Value)
        'End If
    End Sub
End Class