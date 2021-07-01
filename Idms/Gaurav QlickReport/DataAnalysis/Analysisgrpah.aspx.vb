Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.SessionState
Imports System.Collections
Imports Dundas.Charting.Utilities
'Imports Dundas.Charting.Utilities.SixSigma
Imports DundasUtilities.Charting.SixSigma
Imports System.ComponentModel
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Math
Imports System.Web.UI.WebControls.WebParts
Imports System.Drawing.FontFamily
Partial Class DataAnalysis_Analysisgrpah
    Inherits System.Web.UI.Page
    Dim con As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim com As SqlCommand
    Dim com1 As SqlCommand
    Dim data1 As SqlDataReader
    Public Analysistable
    Dim label1 As Label
    Dim label2 As Label
    Dim str As String
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Public Sub aspnet_closemsgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("  window.close();" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim repName, anasource As String
        repName = Request.QueryString("repnm")
        anasource = Request.QueryString("source")
        Dim str As String
        str = Session("anagraph")
        
        If repName = "" Then
            If str = "" Then
                aspnet_closemsgbox("This Analysis Report Does Not Contain Correlation Or Regression Formula ")
                Exit Sub
            End If
            Dim cce
            Dim counteer As Integer = 0
            Dim tablenamepresent, regressiontab As String
            Dim tablenamepresent1 As String
            Dim strddl As String
            Dim classobj As New Functions
            Dim colco, regco As String
            Dim dd As String = str
            Dim cc
            Dim int As Integer = 0
            Dim k As Integer = 0

            For k = 0 To dd.Length
                If dd.Contains("</table>") Then
                    int = dd.IndexOf("</table>")
                    cce = dd.Substring(0, int + 8)
                    Dim ccestr As String
                    ccestr = cce
                    Dim hh
                    ccestr = ccestr.Replace("</tr>", "*")
                    ccestr = ccestr.Replace("</td>", "*")
                    ccestr = ccestr.Replace("<caption>", "*")
                    ccestr = ccestr.Replace("</caption>", "*")
                    ccestr = ccestr.Replace("<b>", "")
                    ccestr = ccestr.Replace("</b>", "")
                    Dim vaslueinsertes As String = ""
                    Dim columnval1 As String = ""
                    Dim tablename As String = ""
                    Dim tablecolname, regressionstr, correlationstr As String
                    Dim regtablename As String = ""
                    Dim regtablecolname As String
                    hh = ccestr.Split("*")
                    Dim j As Integer = UBound(hh)
                    Dim i As Integer = 0
                    For i = 2 To j
                        If hh(i) <> "" Then
                            Dim nm As Integer = hh(i).LastIndexOf(">")
                            Dim nm1 As Integer = hh(i).length
                            Dim nm2 As Integer = nm1 - nm
                            Dim columnval As String = hh(i)

                            If columnval1 = "" Then
                                columnval1 = columnval.Substring(nm + 1, nm2 - 1)
                            Else
                                columnval1 = columnval1 & "*" & columnval.Substring(nm + 1, nm2 - 1)
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    Dim correstr As String
                    For i = 2 To j
                        Dim nm As Integer = hh(i).LastIndexOf(">")
                        Dim nm1 As Integer = hh(i).length
                        Dim nm2 As Integer = nm1 - nm
                        Dim columnval As String = hh(i)
                        If columnval.Contains("Correlation") Then
                            correstr = "Correlation"
                            Dim strcor As String = hh(i + 1)
                            Dim arrcol = strcor.Split("<br>")
                            Dim jc As Integer = UBound(arrcol)
                            Dim ic As Integer = 1
                            For ic = 1 To jc - 1
                                Dim nm1c As Integer = arrcol(ic).LastIndexOf("=")
                                Dim nm2c As Integer = arrcol(ic).length
                                Dim nm3c As Integer = nm2c - nm1c
                                Dim columnval2 As String = arrcol(ic)

                                If colco = "" Then
                                    colco = columnval2.Substring(nm1c + 1, nm3c - 1)
                                Else
                                    colco = colco & "*" & columnval2.Substring(nm1c + 1, nm3c - 1)
                                End If
                            Next
                        End If
                    Next
                    Dim regstr, newreg, strtotreg As String
                    Dim regarr
                    For i = 2 To j
                        Dim nm As Integer = hh(i).LastIndexOf(">")
                        Dim nm1 As Integer = hh(i).length
                        Dim nm2 As Integer = nm1 - nm
                        Dim columnval As String = hh(i)
                        If columnval.Contains("Regression") Then
                            regressionstr = "Regression"
                            Dim strreg As String = hh(i + 1)
                            Dim arrreg = strreg.Split("<br>")
                            Dim jc As Integer = UBound(arrreg)
                            Dim ic As Integer = 1

                            For ic = 1 To jc - 1
                                Dim strreg1 As String = arrreg(ic)
                                Dim nm1c, reg1, reg2, reg3 As Integer
                                reg1 = arrreg(ic).LastIndexOf(">")
                                reg2 = arrreg(ic).length
                                reg3 = reg2 - reg1
                                Dim regres As String = arrreg(ic)

                                regstr = regres.Substring(reg1 + 1, reg3 - 1)
                                If regstr.Contains("+") Then
                                    regarr = regstr.Split("+")
                                Else
                                    regarr = regstr.Split("-")
                                End If
                                If strtotreg = "" Then
                                    strtotreg = regarr(0)
                                Else
                                    strtotreg = strtotreg + "," + regarr(0)
                                End If


                                If strreg1.Contains("+") Then
                                    nm1c = arrreg(ic).LastIndexOf("+")
                                ElseIf strreg1.Contains("-") Then
                                    nm1c = arrreg(ic).LastIndexOf("-")
                                End If
                                Dim nm2c As Integer = arrreg(ic).length
                                Dim nm3c As Integer = nm2c - nm1c
                                Dim regval2 As String = arrreg(ic)
                                If regco = "" Then
                                    regco = regval2.Substring(nm1c, nm3c)
                                Else
                                    regco = regco & "*" & regval2.Substring(nm1c, nm3c)
                                End If
                            Next
                        End If
                    Next
                    Dim nnn As Integer = 0
                    Dim str3, regtable As String
                    Dim str4, str4reg As String
                    Dim regtabentry As String
                    Dim regcomma
                    Dim regdoub
                    Dim regnnn As Integer
                    Dim regarray As Integer
                    Dim strtabentry As String = columnval1 + "**" + colco
                    columnval1 = strtabentry.Replace("**", "$")
                    Dim doublecomma = columnval1.Split("$")
                    str4 = doublecomma(0)
                    Dim doub = str4.Split("*")
                    Dim kyarakhu As Integer = UBound(doublecomma)

                    If strtotreg <> "" Then
                        regtabentry = strtotreg + "**" + regco
                        strtotreg = regtabentry.Replace("**", "$")
                        strtotreg = strtotreg.Replace("=", "@")
                        regcomma = strtotreg.Split("$")
                        str4reg = regcomma(0)
                        regdoub = str4reg.Split(",")
                        regarray = UBound(regcomma)
                        regnnn = 0
                        regtable = "create table "
                        regtable = regtable & "Regression" & "("
                        regressiontab = "Regression"
                    End If



                    str3 = "create table "
                    str3 = str3 & "Correlation" & "("
                    tablenamepresent = "Correlation"
                    'strddl = "Correlation" & Trim(doub(0)) & counteer



                    strddl = tablenamepresent + "," + regressiontab

                    If tablenamepresent1 = "" Then
                        tablenamepresent1 = tablenamepresent
                    Else
                        tablenamepresent1 = tablenamepresent1 & "," & "Correlation"
                    End If

                    If regressionstr = "Regression" Then
                        For regnnn = 0 To regarray
                            If regnnn = 0 Then
                                Dim q As Integer = 1
                                If k = 1 Then
                                    For q = 1 To regdoub.length - 1
                                        If regtablecolname = "" Then
                                            regtablecolname = regdoub(q) & " " & "varchar(1000)"
                                        Else
                                            regtablecolname = regtablecolname & "," & regdoub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                Else
                                    For q = 1 To regdoub.length - 1
                                        If regtablecolname = "" Then
                                            regtablecolname = regdoub(q) & " " & "varchar(1000)"
                                        Else
                                            regtablecolname = regtablecolname & "," & regdoub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                End If

                                regtable = regtable & regtablecolname
                                regtable = regtable & ")"

                                Dim vv As Boolean
                                Dim repName1 As String = ""
                                com1 = New SqlCommand("select name from sysobjects where xtype='u'", conn)
                                conn.Open()
                                data1 = com1.ExecuteReader
                                While data1.Read()
                                    repName1 = regressiontab
                                    If data1("name") = regressiontab Then
                                        vv = False
                                        Exit While
                                    Else
                                        vv = True
                                    End If
                                End While
                                com1.Dispose()
                                conn.Close()
                                Dim blank As String
                                If vv = False Then
                                    blank = "Drop table " + regressiontab + ""
                                    com1 = New SqlCommand(blank, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                    blank = ""
                                    If blank = "" Then
                                        com1 = New SqlCommand(regtable, conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                    End If
                                End If
                                If vv = True Then
                                    com1 = New SqlCommand(regtable, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                End If
                                regtablecolname = ""
                            ElseIf regnnn >= 1 Then
                                Dim skip, q, u As Integer
                                Dim newstringnow As String = regcomma(regnnn)
                                Dim arrayvalues = newstringnow.Split("*")
                                u = UBound(arrayvalues)

                                For q = 1 To u
                                    If arrayvalues(q) <> "" Then
                                        skip = UBound(arrayvalues)
                                        Dim meadian As String = Trim(arrayvalues(q))
                                        If meadian.Contains("Median") Then
                                            If meadian.Contains("Meadian") = True Then
                                                If arrayvalues(q) <> "" Then
                                                    Dim meadianstr As String = ""
                                                    If meadianstr = "" Then
                                                        meadianstr = "'" & arrayvalues(q) & "'"
                                                    Else
                                                        meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                    End If
                                                End If
                                                q = skip
                                            End If
                                        Else
                                            If newstringnow = " FILTER PERCENTAGE" Then
                                                GoTo gg
                                            End If
                                            If meadian.Contains("FILTER PERCENTAGE") = True Then
                                                GoTo gg
                                            End If
                                            If vaslueinsertes = "" Then
                                                vaslueinsertes = "'" & arrayvalues(q) & "'"
                                            Else
                                                vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                            End If
                                        End If
                                    End If
                                Next
                                If vaslueinsertes <> "" Then
                                    If regtable = "" Then
                                        Exit For
                                    Else
                                        com1 = New SqlCommand("insert into " & regressiontab & " values(" & vaslueinsertes & " )", conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                        vaslueinsertes = ""
                                    End If
                                End If
                            End If
                        Next
                    End If
                    If correstr = "Correlation" Then
                        For nnn = 0 To kyarakhu
                            If nnn = 0 Then
                                Dim q As Integer = 1
                                If k = 1 Then
                                    For q = 1 To doub.length - 1
                                        If tablecolname = "" Then
                                            tablecolname = doub(q) & " " & "varchar(1000)"
                                        Else
                                            tablecolname = tablecolname & "," & doub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                Else
                                    For q = 1 To doub.length - 1
                                        If tablecolname = "" Then
                                            tablecolname = doub(q) & " " & "varchar(1000)"
                                        Else
                                            tablecolname = tablecolname & "," & doub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                End If

                                str3 = str3 & tablecolname
                                str3 = str3 & ")"

                                Dim vv As Boolean
                                Dim repName1 As String = ""
                                com1 = New SqlCommand("select name from sysobjects where xtype='u'", conn)
                                conn.Open()
                                data1 = com1.ExecuteReader
                                While data1.Read()
                                    repName1 = tablenamepresent
                                    If data1("name") = tablenamepresent Then
                                        vv = False
                                        Exit While
                                    Else
                                        vv = True
                                    End If
                                End While
                                com1.Dispose()
                                conn.Close()
                                Dim blank As String
                                If vv = False Then
                                    blank = "Drop table " + tablenamepresent + ""
                                    com1 = New SqlCommand(blank, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                    blank = ""
                                    If blank = "" Then
                                        com1 = New SqlCommand(str3, conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                    End If
                                End If
                                If vv = True Then
                                    com1 = New SqlCommand(str3, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                End If
                                tablecolname = ""
                            ElseIf nnn >= 1 Then
                                Dim skip, q, u As Integer
                                Dim newstringnow As String = doublecomma(nnn)
                                Dim arrayvalues = newstringnow.Split("*")
                                u = UBound(arrayvalues)

                                For q = 0 To u
                                    If arrayvalues(q) <> "" Then
                                        skip = UBound(arrayvalues)
                                        Dim meadian As String = Trim(arrayvalues(q))
                                        If meadian.Contains("Median") Then
                                            If meadian.Contains("Meadian") = True Then
                                                If arrayvalues(q) <> "" Then
                                                    Dim meadianstr As String = ""
                                                    If meadianstr = "" Then
                                                        meadianstr = "'" & arrayvalues(q) & "'"
                                                    Else
                                                        meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                    End If
                                                End If
                                                q = skip
                                            End If
                                        Else
                                            If newstringnow = " FILTER PERCENTAGE" Then
                                                GoTo gg1
                                            End If
                                            If meadian.Contains("FILTER PERCENTAGE") = True Then
                                                GoTo gg1
                                            End If
                                            If vaslueinsertes = "" Then
                                                vaslueinsertes = "'" & arrayvalues(q) & "'"
                                            Else
                                                vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                            End If
                                        End If
                                    End If
                                Next
                                If vaslueinsertes <> "" Then
                                    If tablenamepresent = "" Then
                                        Exit For
                                    Else
                                        com1 = New SqlCommand("insert into " & tablenamepresent & " values(" & vaslueinsertes & " )", conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                        vaslueinsertes = ""
                                    End If
                                End If
                            End If
                        Next
                    End If


                    Dim now = columnval1.Split("*")
                    Dim newint As Integer = dd.Length
                    dd = dd.Replace(cce, "")
                    If dd.Length = 0 Then
                        If k = 0 Then
                            dd = ""
                        End If
                        If k = 1 Then
                            dd = "1"
                        End If
                        If k = 2 Then
                            dd = "12"
                        End If
                        If k = 3 Then
                            dd = "123"
                        End If
                    End If
                    cc = dd.Split("</table>")
                End If
                counteer = counteer + 1
            Next
gg1:
            Analysistable = strddl.Split(",")

        End If
        Try

        Catch ex As Exception

        End Try

        If repName <> "" And anasource <> "" Then

            Dim tablenamepresent, regressiontab As String
            Dim tablenamepresent1 As String
            Dim strddl As String
            Dim classobj As New Functions
            Dim colco, regco As String
            Dim path As String
            If Not Directory.Exists(Server.MapPath("AnalysisReport/excel")) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("AnalysisReport/excel"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            Dim tabname As String = repName
            path = "../DataAnalysis/html/" & repName & ".html"
            Dim st As StreamReader
            Dim cce
            Dim counteer As Integer = 0
            st = File.OpenText(Server.MapPath(path))
            Dim dd As String = st.ReadToEnd
            Dim cc
            Dim int As Integer = 0
            Dim k As Integer = 0

            For k = 0 To dd.Length
                If dd.Contains("</table>") Then
                    int = dd.IndexOf("</table>")
                    cce = dd.Substring(0, int + 8)
                    Dim ccestr As String
                    ccestr = cce
                    Dim hh
                    ccestr = ccestr.Replace("</tr>", "*")
                    ccestr = ccestr.Replace("</td>", "*")
                    ccestr = ccestr.Replace("<caption>", "*")
                    ccestr = ccestr.Replace("</caption>", "*")
                    ccestr = ccestr.Replace("<b>", "")
                    ccestr = ccestr.Replace("</b>", "")
                    ' If ccestr.Contains("Correlation") Or ccestr.Contains("Regression") Then


                    Dim vaslueinsertes As String = ""
                    Dim columnval1 As String = ""
                    Dim tablename As String = ""
                    Dim tablecolname, regressionstr, correlationstr As String
                    Dim regtablename As String = ""
                    Dim regtablecolname As String
                    hh = ccestr.Split("*")
                    Dim j As Integer = UBound(hh)
                    Dim i As Integer = 0
                    For i = 2 To j
                        If hh(i) <> "" Then
                            Dim nm As Integer = hh(i).LastIndexOf(">")
                            Dim nm1 As Integer = hh(i).length
                            Dim nm2 As Integer = nm1 - nm
                            Dim columnval As String = hh(i)

                            If columnval1 = "" Then
                                columnval1 = columnval.Substring(nm + 1, nm2 - 1)
                            Else
                                columnval1 = columnval1 & "*" & columnval.Substring(nm + 1, nm2 - 1)
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    Dim correstr As String
                    For i = 2 To j
                        Dim nm As Integer = hh(i).LastIndexOf(">")
                        Dim nm1 As Integer = hh(i).length
                        Dim nm2 As Integer = nm1 - nm
                        Dim columnval As String = hh(i)
                        If columnval.Contains("Correlation") Then
                            correstr = "Correlation"
                            Dim strcor As String = hh(i + 1)
                            Dim arrcol = strcor.Split("<br>")
                            Dim jc As Integer = UBound(arrcol)
                            Dim ic As Integer = 1
                            For ic = 1 To jc - 1
                                Dim nm1c As Integer = arrcol(ic).LastIndexOf("=")
                                Dim nm2c As Integer = arrcol(ic).length
                                Dim nm3c As Integer = nm2c - nm1c
                                Dim columnval2 As String = arrcol(ic)

                                If colco = "" Then
                                    colco = columnval2.Substring(nm1c + 1, nm3c - 1)
                                Else
                                    colco = colco & "*" & columnval2.Substring(nm1c + 1, nm3c - 1)
                                End If
                            Next
                        End If
                    Next
                    Dim regstr, newreg, strtotreg As String
                    Dim regarr
                    For i = 2 To j
                        Dim nm As Integer = hh(i).LastIndexOf(">")
                        Dim nm1 As Integer = hh(i).length
                        Dim nm2 As Integer = nm1 - nm
                        Dim columnval As String = hh(i)
                        If columnval.Contains("Regression") Then
                            regressionstr = "Regression"
                            Dim strreg As String = hh(i + 1)
                            Dim arrreg = strreg.Split("<br>")
                            Dim jc As Integer = UBound(arrreg)
                            Dim ic As Integer = 1

                            For ic = 1 To jc - 1
                                Dim strreg1 As String = arrreg(ic)
                                Dim nm1c, reg1, reg2, reg3 As Integer
                                reg1 = arrreg(ic).LastIndexOf(">")
                                reg2 = arrreg(ic).length
                                reg3 = reg2 - reg1
                                Dim regres As String = arrreg(ic)

                                regstr = regres.Substring(reg1 + 1, reg3 - 1)
                                If regstr.Contains("+") Then
                                    regarr = regstr.Split("+")
                                Else
                                    regarr = regstr.Split("-")
                                End If
                                If strtotreg = "" Then
                                    strtotreg = regarr(0)
                                Else
                                    strtotreg = strtotreg + "," + regarr(0)
                                End If


                                If strreg1.Contains("+") Then
                                    nm1c = arrreg(ic).LastIndexOf("+")
                                ElseIf strreg1.Contains("-") Then
                                    nm1c = arrreg(ic).LastIndexOf("-")
                                End If
                                Dim nm2c As Integer = arrreg(ic).length
                                Dim nm3c As Integer = nm2c - nm1c
                                Dim regval2 As String = arrreg(ic)
                                If regco = "" Then
                                    regco = regval2.Substring(nm1c, nm3c)
                                Else
                                    regco = regco & "*" & regval2.Substring(nm1c, nm3c)
                                End If
                            Next
                        End If
                    Next
                    Dim nnn As Integer = 0
                    Dim str3, regtable As String
                    Dim str4, str4reg As String
                    Dim regtabentry As String
                    Dim regcomma
                    Dim regdoub
                    Dim regnnn As Integer
                    Dim regarray As Integer
                    Dim strtabentry As String = columnval1 + "**" + colco
                    columnval1 = strtabentry.Replace("**", "$")
                    Dim doublecomma = columnval1.Split("$")
                    str4 = doublecomma(0)
                    Dim doub = str4.Split("*")
                    Dim kyarakhu As Integer = UBound(doublecomma)

                    If strtotreg <> "" Then
                        regtabentry = strtotreg + "**" + regco
                        strtotreg = regtabentry.Replace("**", "$")
                        strtotreg = strtotreg.Replace("=", "@")
                        regcomma = strtotreg.Split("$")
                        str4reg = regcomma(0)
                        regdoub = str4reg.Split(",")
                        regarray = UBound(regcomma)
                        regnnn = 0
                        regtable = "create table "
                        regtable = regtable & "Regression" & "("
                        regressiontab = "Regression"
                    End If



                    str3 = "create table "
                    str3 = str3 & "Correlation" & "("
                    tablenamepresent = "Correlation"
                    'strddl = "Correlation" & Trim(doub(0)) & counteer



                    strddl = tablenamepresent + "," + regressiontab

                    If tablenamepresent1 = "" Then
                        tablenamepresent1 = tablenamepresent
                    Else
                        tablenamepresent1 = tablenamepresent1 & "," & "Correlation"
                    End If

                    If regressionstr = "Regression" Then
                        For regnnn = 0 To regarray
                            If regnnn = 0 Then
                                Dim q As Integer = 1
                                If k = 1 Then
                                    For q = 1 To regdoub.length - 1
                                        If regtablecolname = "" Then
                                            regtablecolname = regdoub(q) & " " & "varchar(1000)"
                                        Else
                                            regtablecolname = regtablecolname & "," & regdoub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                Else
                                    For q = 1 To regdoub.length - 1
                                        If regtablecolname = "" Then
                                            regtablecolname = regdoub(q) & " " & "varchar(1000)"
                                        Else
                                            regtablecolname = regtablecolname & "," & regdoub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                End If

                                regtable = regtable & regtablecolname
                                regtable = regtable & ")"

                                Dim vv As Boolean
                                Dim repName1 As String = ""
                                com1 = New SqlCommand("select name from sysobjects where xtype='u'", conn)
                                conn.Open()
                                data1 = com1.ExecuteReader
                                While data1.Read()
                                    repName1 = regressiontab
                                    If data1("name") = regressiontab Then
                                        vv = False
                                        Exit While
                                    Else
                                        vv = True
                                    End If
                                End While
                                com1.Dispose()
                                conn.Close()
                                Dim blank As String
                                If vv = False Then
                                    blank = "Drop table " + regressiontab + ""
                                    com1 = New SqlCommand(blank, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                    blank = ""
                                    If blank = "" Then
                                        com1 = New SqlCommand(regtable, conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                    End If
                                End If
                                If vv = True Then
                                    com1 = New SqlCommand(regtable, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                End If
                                regtablecolname = ""
                            ElseIf regnnn >= 1 Then
                                Dim skip, q, u As Integer
                                Dim newstringnow As String = regcomma(regnnn)
                                Dim arrayvalues = newstringnow.Split("*")
                                u = UBound(arrayvalues)

                                For q = 1 To u
                                    If arrayvalues(q) <> "" Then
                                        skip = UBound(arrayvalues)
                                        Dim meadian As String = Trim(arrayvalues(q))
                                        If meadian.Contains("Median") Then
                                            If meadian.Contains("Meadian") = True Then
                                                If arrayvalues(q) <> "" Then
                                                    Dim meadianstr As String = ""
                                                    If meadianstr = "" Then
                                                        meadianstr = "'" & arrayvalues(q) & "'"
                                                    Else
                                                        meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                    End If
                                                End If
                                                q = skip
                                            End If
                                        Else
                                            If newstringnow = " FILTER PERCENTAGE" Then
                                                GoTo gg
                                            End If
                                            If meadian.Contains("FILTER PERCENTAGE") = True Then
                                                GoTo gg
                                            End If
                                            If vaslueinsertes = "" Then
                                                vaslueinsertes = "'" & arrayvalues(q) & "'"
                                            Else
                                                vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                            End If
                                        End If
                                    End If
                                Next
                                If vaslueinsertes <> "" Then
                                    If regtable = "" Then
                                        Exit For
                                    Else
                                        com1 = New SqlCommand("insert into " & regressiontab & " values(" & vaslueinsertes & " )", conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                        vaslueinsertes = ""
                                    End If
                                End If
                            End If
                        Next
                    End If
                    If correstr = "Correlation" Then
                        For nnn = 0 To kyarakhu
                            If nnn = 0 Then
                                Dim q As Integer = 1
                                If k = 1 Then
                                    For q = 1 To doub.length - 1
                                        If tablecolname = "" Then
                                            tablecolname = doub(q) & " " & "varchar(1000)"
                                        Else
                                            tablecolname = tablecolname & "," & doub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                Else
                                    For q = 1 To doub.length - 1
                                        If tablecolname = "" Then
                                            tablecolname = doub(q) & " " & "varchar(1000)"
                                        Else
                                            tablecolname = tablecolname & "," & doub(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                End If

                                str3 = str3 & tablecolname
                                str3 = str3 & ")"

                                Dim vv As Boolean
                                Dim repName1 As String = ""
                                com1 = New SqlCommand("select name from sysobjects where xtype='u'", conn)
                                conn.Open()
                                data1 = com1.ExecuteReader
                                While data1.Read()
                                    repName1 = tablenamepresent
                                    If data1("name") = tablenamepresent Then
                                        vv = False
                                        Exit While
                                    Else
                                        vv = True
                                    End If
                                End While
                                com1.Dispose()
                                conn.Close()
                                Dim blank As String
                                If vv = False Then
                                    blank = "Drop table " + tablenamepresent + ""
                                    com1 = New SqlCommand(blank, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                    blank = ""
                                    If blank = "" Then
                                        com1 = New SqlCommand(str3, conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                    End If
                                End If
                                If vv = True Then
                                    com1 = New SqlCommand(str3, conn)
                                    conn.Open()
                                    com1.ExecuteNonQuery()
                                    conn.Close()
                                End If
                                tablecolname = ""
                            ElseIf nnn >= 1 Then
                                Dim skip, q, u As Integer
                                Dim newstringnow As String = doublecomma(nnn)
                                Dim arrayvalues = newstringnow.Split("*")
                                u = UBound(arrayvalues)

                                For q = 0 To u
                                    If arrayvalues(q) <> "" Then
                                        skip = UBound(arrayvalues)
                                        Dim meadian As String = Trim(arrayvalues(q))
                                        If meadian.Contains("Median") Then
                                            If meadian.Contains("Meadian") = True Then
                                                If arrayvalues(q) <> "" Then
                                                    Dim meadianstr As String = ""
                                                    If meadianstr = "" Then
                                                        meadianstr = "'" & arrayvalues(q) & "'"
                                                    Else
                                                        meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                    End If
                                                End If
                                                q = skip
                                            End If
                                        Else
                                            If newstringnow = " FILTER PERCENTAGE" Then
                                                GoTo gg
                                            End If
                                            If meadian.Contains("FILTER PERCENTAGE") = True Then
                                                GoTo gg
                                            End If
                                            If vaslueinsertes = "" Then
                                                vaslueinsertes = "'" & arrayvalues(q) & "'"
                                            Else
                                                vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                            End If
                                        End If
                                    End If
                                Next
                                If vaslueinsertes <> "" Then
                                    If tablenamepresent = "" Then
                                        Exit For
                                    Else
                                        com1 = New SqlCommand("insert into " & tablenamepresent & " values(" & vaslueinsertes & " )", conn)
                                        conn.Open()
                                        com1.ExecuteNonQuery()
                                        conn.Close()
                                        vaslueinsertes = ""
                                    End If
                                End If
                            End If
                        Next
                    End If


                    Dim now = columnval1.Split("*")
                    Dim newint As Integer = dd.Length
                    dd = dd.Replace(cce, "")
                    If dd.Length = 0 Then
                        If k = 0 Then
                            dd = ""
                        End If
                        If k = 1 Then
                            dd = "1"
                        End If
                        If k = 2 Then
                            dd = "12"
                        End If
                        If k = 3 Then
                            dd = "123"
                        End If
                    End If
                    cc = dd.Split("</table>")
                End If
                counteer = counteer + 1
                'Else
                'aspnet_closemsgbox("This Analysis Report Does Not Contain Correlation Or Regression Formula ")
                'Exit Sub
                ' End If



            Next
gg:
            If strddl = "" Then
                aspnet_closemsgbox("This Analysis Report Does Not Contain Correlation Or Regression Formula ")
                Exit Sub
            End If
            
            Analysistable = strddl.Split(",")

        End If
        If Page.IsPostBack = False Then
            Dim selectquery As String
            selectquery = "select * from " & " " & Analysistable(0)

            Dim myCommand As New SqlCommand(selectquery, conn)
            ' Open the connection	
            myCommand.Connection.Open()
            ' Initializes a new instance of the OleDbDataAdapter class
            Dim myDataAdapter As New SqlDataAdapter
            myDataAdapter.SelectCommand = myCommand
            ' Initializes a new instance of the DataSet class
            Dim myDataSet As New DataSet()
            ' Adds rows in the DataSet
            myDataAdapter.Fill(myDataSet, "Query")
            myCommand.Connection.Close()

            Dim xval, Duplicate As String
            Dim count As Integer
            xval = ""
            Dim row As DataRow
            count = 1
            Duplicate = ""
            Dim ii, t As Integer
            Dim rowname As String
            Dim rval As Integer
            Dim sval As String
            Dim dval, dateval As Date
            Dim arr(myDataSet.Tables(0).Rows.Count - 1) As String
            Dim fb As Boolean

            For Each row In myDataSet.Tables("Query").Rows
                ' for each Row, add a new series
                For ii = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                    rowname = myDataSet.Tables("Query").Columns(ii).ColumnName
                    Try
                        If IsDBNull(row(rowname)) Then
                        Else
                            rval = CInt(row(rowname))
                            arr(ii) = rval
                        End If
                    Catch ex As Exception
                    End Try
                Next ii
                Dim jk As Integer
                str = ""

                For jk = 0 To arr.Length - 1
                    If (arr(jk) <> "") Then
                        If (str = "") Then
                            str = arr(jk)
                        Else
                            str = str + " " + arr(jk)
                        End If
                    Else
                        Exit For
                    End If
                Next
                Dim series0 As String
                series0 = str
                If str = Duplicate Then
                    str = str + count.ToString
                    count = count + 1
                End If
                Chart1.Series.Add(str)
                Chart1.Series(str).Type = SeriesChartType.Point
                Chart1.Series(str).BorderWidth = 2
                Dim st As String
                st = str.Replace(count - 1, "")
                st = series0
                Duplicate = st
                Dim colIndex As Integer
                Dim columnName As String
                Dim bb As String = ""
                Dim YVal As Double

                Try
                    For colIndex = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        ' for each column (column 1 and onward), add the value as a point
                        columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                        Try
                            If IsDBNull(row(columnName)) Then
                            Else
                                YVal = CDbl(row(columnName))
                            End If

                            Chart1.Series(str).Points.AddXY(columnName, YVal)
                        Catch ex As Exception
                            Try
                                If IsDBNull(row(columnName)) Then
                                Else
                                    dateval = CDate(row(columnName))
                                End If

                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                    Chart1.Series(str).Points.AddXY(columnName, dateval)
                                End If

                            Catch ex1 As Exception

                            End Try
                            bb = row(columnName)
                        End Try
                        Chart1.Titles(0).Text = Analysistable(0) + " " + "Chart"
                        Chart1.Series(str).ShowLabelAsValue = True
                        Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                    Next colIndex
                    Dim blankravl As Integer = 0
                    rval = blankravl
                Catch ex As Exception
                End Try
                YVal = 0.0
                dateval = "#12:00:00 AM#"
                fb = False
            Next row
            Dim point As DataPoint
            Chart1.Series.Add("TrendLine")
            Chart1.Series("TrendLine").Type = SeriesChartType.Line
            For Each point In Chart1.Series(str).Points
                Chart1.Series("TrendLine").Points.AddXY(point.XValue, point.XValue)
            Next
            Chart1.Series("TrendLine").Points.Clear()

            '// Calculate Correlation
            Try
                Chart1.DataManipulator.FormulaFinancial(FinancialFormula.Forecasting, "2,0", str, "TrendLine:Y,TrendLine:Y2,TrendLine:Y3")
            Catch ex As Exception
                aspnet_msgbox("There Must Be Atleast Two Values For Ploting TrendLine Graph")
                Exit Sub
            End Try

label1:

        End If
    End Sub

    Protected Sub ddlanalysistable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlanalysistable.SelectedIndexChanged
        Try

       
            Dim selectquery As String
            Chart1.Series.Clear()
            If ddlanalysistable.SelectedItem.Text = "Correlation" Then
                selectquery = "select * from " & " " & Analysistable(0)
                Chart1.Titles(0).Text = Analysistable(0) + " " + "Chart"
            Else
                selectquery = "select * from " & " " & Analysistable(1)
                Chart1.Titles(0).Text = Analysistable(1) + " " + "Chart"
            End If
            Dim myCommand As New SqlCommand(selectquery, conn)
            ' Open the connection	
            myCommand.Connection.Open()
            ' Initializes a new instance of the OleDbDataAdapter class
            Dim myDataAdapter As New SqlDataAdapter
            myDataAdapter.SelectCommand = myCommand
            ' Initializes a new instance of the DataSet class
            Dim myDataSet As New DataSet()
            ' Adds rows in the DataSet
            myDataAdapter.Fill(myDataSet, "Query")
            myCommand.Connection.Close()

            Dim xval, Duplicate As String
            Dim count As Integer
            xval = ""
            Dim row As DataRow
            count = 1
            Duplicate = ""
            Dim ii As Integer
            Dim rowname As String
            Dim rval As Double
            Dim str1 As String
            Dim dateval As Date
            Dim arr(myDataSet.Tables(0).Rows.Count - 1) As String
            Dim fb As Boolean

            For Each row In myDataSet.Tables("Query").Rows
                ' for each Row, add a new series
                For ii = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                    rowname = myDataSet.Tables("Query").Columns(ii).ColumnName
                    Try
                        If IsDBNull(row(rowname)) Then
                        Else
                            rval = CInt(row(rowname))
                            arr(ii) = rval
                        End If
                    Catch ex As Exception
                    End Try
                Next ii
                Dim jk As Integer
                For jk = 0 To arr.Length - 1
                    If (arr(jk) <> "") Then
                        If (str1 = "") Then
                            str1 = arr(jk)
                        Else
                            str1 = str1 + " " + arr(jk)
                        End If
                    Else
                        Exit For
                    End If
                Next
                Dim series0 As String
                series0 = str1
                If str1 = Duplicate Then
                    str1 = str1 + count.ToString
                    count = count + 1
                End If
                Chart1.Series.Add(str1)
                Chart1.Series(str1).Type = SeriesChartType.Point
                Chart1.Series(str1).BorderWidth = 2
                Dim st As String
                st = str1.Replace(count - 1, "")
                st = series0
                Duplicate = st
                Dim colIndex As Integer
                Dim columnName As String
                Dim bb As String = ""
                Dim YVal As Double

                Try
                    For colIndex = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        ' for each column (column 1 and onward), add the value as a point
                        columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                        Try
                            If IsDBNull(row(columnName)) Then
                            Else
                                YVal = CDbl(row(columnName))
                            End If

                            Chart1.Series(str1).Points.AddXY(columnName, YVal)
                        Catch ex As Exception
                            Try
                                If IsDBNull(row(columnName)) Then
                                Else
                                    dateval = CDate(row(columnName))
                                End If

                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                    Chart1.Series(str1).Points.AddXY(columnName, dateval)
                                End If

                            Catch ex1 As Exception

                            End Try
                            bb = row(columnName)
                        End Try
                        Dim temp As System.Drawing.FontFamily

                        Chart1.Series(str1).ShowLabelAsValue = True
                        Chart1.Series(str1).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                    Next colIndex
                    Dim blankravl As Integer = 0
                    rval = blankravl
                Catch ex As Exception
                End Try
                YVal = 0.0
                dateval = "#12:00:00 AM#"
                fb = False
            Next row
            Dim point As DataPoint
            Chart1.Series.Add("TrendLine")
            Chart1.Series("TrendLine").Type = SeriesChartType.Line
            For Each point In Chart1.Series(str1).Points
                Chart1.Series("TrendLine").Points.AddXY(point.XValue, point.XValue)
            Next
            Chart1.Series("TrendLine").Points.Clear()
            Try
                Chart1.DataManipulator.FormulaFinancial(FinancialFormula.Forecasting, "2,0", str1, "TrendLine:Y,TrendLine:Y2,TrendLine:Y3")
            Catch ex As Exception
                aspnet_msgbox("There Must Be Atleast Two Values For Ploting TrendLine Graph")
                Exit Sub
            End Try
            '// Calculate Correlation

label1:
        Catch ex As Exception
            aspnet_msgbox("Selected analysis has not been performed")
            Exit Sub
        End Try

    End Sub
End Class
