
Partial Class DataAnalysis_ResultDisplay
    Inherits System.Web.UI.Page
    Dim strsession As String = ""
    ''' <summary>
    ''' display the reports
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            divhead.InnerHtml = Session("Nextdata")
            Report.InnerHtml = Session("Nextdata1")
            formularesults.InnerHtml = Session("Nextdata2")
            strsession = Session("Nextdata2")
            If strsession <> "" Then
                If strsession.Contains("Correlation") Or strsession.Contains("Regression") Then
                    Session("anagraph") = strsession
                Else
                    Session("anagraph") = ""
                End If
            End If
            If strsession <> "" Then
                btnanalysis.Visible = True
            End If
            Session("Nextdata") = ""
            Session("Nextdata1") = ""
            Session("Nextdata2") = ""

        End If
    End Sub
    Protected Sub btnanalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnanalysis.Click
        Dim strClose As String = ""
        strClose = "<Script language='Javascript'>"
        strClose = strClose + "anagraph();"
        strClose = strClose + "</Script>"
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", strClose)
    End Sub
End Class
