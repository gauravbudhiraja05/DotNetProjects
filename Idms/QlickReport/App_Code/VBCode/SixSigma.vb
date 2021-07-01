' ------------------------------------------------------------------------ 
' Six Sigma for Dundas Chart 
' 
' File:         SixSigma.cs 
' 
' Namespace:    DundasUtilities.Charting.SixSigma 
' 
' Classes:      SixSigmaBase, 
'               C_Chart, 
'               P_Chart, 
'               NP_Chart, 
'               U_Chart, 
'               S_Chart, 
'               R_Chart, 
'               Run_Chart, 
'               XBAR_Chart, 
'               Individuals_Chart, 
'               ControlLineStyle
' 
' Purpose:      To create Six Sigma charts. 
' 
' How to use:   SixSigmaBase class serves as container to save styles for ControlLines and internal variables that can be used by classes 
'               that derived from SixSigmaBase. 
'               All specific charts are derived from SixSigmaBase class and have different implementation of CreateSeries method. 
'               Create instance of specific SixSigma class. Use CreateSeries method with parameters. 
'               All of the chart creation functions expect data to be contained in arrays. 
' 
'               Imports DundasUtilities.Charting.SixSigma 
'               Dim c_Chart As C_Chart = new C_Chart()
' 
'               ' Create a C-Chart of the data and plot it on the chart. 
'               Dim tmpSeries As Series = c_Chart.CreateSeries(aSubGroup, aData, chart1) 
' 
'               ' Optionally before calling any chart creation function, you can setup styles for 
'               ' Control Lines. 
'               c_Chart.UCLstyle.LineStyle = ChartDashStyle.Solid
'               c_Chart.UCLstyle.LineColor = Color.Red
'               c_Chart.UCLstyle.LineWidth = 2
' 
'               ' Also you can set style for text 
'               c_Chart.ShowText = true
'               c_Chart.LCLstyle.TextColor = Color.Blue
'               c_Chart.LCLstyle.TextFont = new Font("Arial", 10)
' 
' 
' You must define DUNDAS_CHART_WEB or DUNDAS_CHART_WIN as 
' appropriate before compiling this class. 
' 
' This class must be used and distributed solely in conjunction with and 
' in accordance with the terms of a valid license for Dundas Chart for 
' .NET, Dundas Chart for ASP.NET, Dundas Chart for Reporting Services, 
' or Dundas Chart for SharePoint. 
' 
' Copyright (c) 2007 Dundas Data Visualization, Inc. and others. 
' All Rights Reserved. 
' ------------------------------------------------------------------------ 
Imports System
Imports System.Drawing
Imports Dundas.Charting.Utilities
Imports Dundas.Charting.Utilities.SixSigma
Imports System.ComponentModel
Imports Dundas.Charting.WebControl
Imports System.Web

#If DUNDAS_CHART_WEB Then
Imports Dundas.Charting.WebControl 
Imports System.Web 
#End If

'#If DUNDAS_CHART_WIN Then
'Imports Dundas.Charting.WinControl 
'Imports System.Windows.Forms 
'#Else
''#Error You must define DUNDAS_CHART_WEB or DUNDAS_CHART_WIN. 
'#End If

Namespace DundasUtilities.Charting.SixSigma
#Region "SixSigma C-chart class"
    ''' <summary> 
    ''' C_Chart, based on SixSigmaBase class, provides functionality to create C type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class C_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates a C-Chart with lines indicating cBar, UCL and LCL. A C-Chart is a measure of the number of 
        ''' non-conformities per unit, where unit is a fixed rate. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the nonconform array.</param> 
        ''' <param name="nonconform">An array holding the non-conformity data associated with the subgroups. Must align with the subgroups.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal nonconform As Single(), ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> nonconform.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, nonconform")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize cBar, LCL and UCL. 
            Dim cbar As Single = 0
            Dim LCL As Single = 0
            Dim UCL As Single = 0
            For i As Integer = 0 To c - 1

                'Calculate cbar. 
                cbar += CSng((nonconform(i)))
            Next
            cbar /= c

            'Calculate UCL and LCL. 
            UCL = CSng((cbar + 3 * System.Math.Sqrt(cbar)))
            LCL = CSng((cbar - 3 * System.Math.Sqrt(cbar)))

            'If LCL is less than zero, then it should be zero. 
            If LCL < 0 Then
                LCL = 0
            End If

            'Create the series for the cchart data. 
            Dim cseries As Series = CreateSeries(output, "cseries")
            For i As Integer = 0 To c - 1

                'Graph subgroups vs. non-conformities. 
                cseries.Points.AddXY(subgroup(i), nonconform(i))
            Next

            'Set the series type to a line. 
            cseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(cseries)

            'Add the lines to the graph as annotations. 
            'cBar line. 
            addLineAnnotation(output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, cbar, [Double].NaN, 0, _
            output, BCLstyle)

            'UCL line. 
            addLineAnnotation(output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, UCL, [Double].NaN, 0, _
            output, UCLstyle)

            'LCL line. 
            addLineAnnotation(output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, LCL, [Double].NaN, 0, _
            output, LCLstyle)

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("CBAR", output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, cbar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, UCL, output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(cseries.ChartArea).AxisX, output.ChartAreas(cseries.ChartArea).AxisY, cseries.Points(0).XValue, LCL, output, _
            LCLstyle)

            'Scale the chart if the user has requested it. 
            FitChart(UCL, cseries.ChartArea, output)

            'Return the series. 
            Return cseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma P-chart class"
    ''' <summary> 
    ''' P_Chart, based on SixSigmaBase class, provides functionality to create P type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class P_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates a P-Chart with lines indicating pBar, UCL and LCL. A P-Chart is the same as an NP-Chart except 
        ''' with a variable number of items in each subgroup. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the nonconform array and the number tested.</param> 
        ''' <param name="nonconform">An array holding the non-conformity data associated with the subgroups. Must align with the subgroups and the number tested.</param> 
        ''' <param name="numbertested">An array holding the number of items in each subgroup. Must align with the subgroups and the nonconform array.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal nonconform As Single(), ByVal numbertested As Single(), ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If (subgroup.Length <> nonconform.Length) OrElse (subgroup.Length <> numbertested.Length) OrElse (nonconform.Length <> numbertested.Length) Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, nonconform, numbertested")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize phat, UCL, LCL and pBar. 
            Dim phat As Single() = New Single(c - 1) {}
            Dim UCL As Single() = New Single(c - 1) {}
            Dim LCL As Single() = New Single(c - 1) {}
            Dim pbar As Single = 0
            For i As Integer = 0 To c - 1

                'Calculate phat(i). 
                phat(i) = nonconform(i) / numbertested(i)
            Next
            For i As Integer = 0 To c - 1

                'Calculate pbar. 
                pbar += phat(i)
            Next
            pbar /= c
            For i As Integer = 0 To c - 1

                'Calculate UCL and LCL. 
                UCL(i) = CSng((pbar + 3 * System.Math.Sqrt((pbar * (1 - pbar)) / numbertested(i))))
                LCL(i) = CSng((pbar - 3 * System.Math.Sqrt((pbar * (1 - pbar)) / numbertested(i))))
            Next

            'Create the series for the pchart data. 
            Dim pseries As Series = CreateSeries(output, "pseries")
            For i As Integer = 0 To c - 1
                'Graph subgroup vs. proportion. 
                pseries.Points.AddXY(subgroup(i), phat(i))
            Next

            'Set the series type to a line. 
            pseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(pseries)

            'Add the lines to the graph as annotations. 
            'PBAR line. 
            addLineAnnotation(output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, pseries.Points(0).XValue, pbar, pseries.Points(c - 1).XValue, 0, _
            output, BCLstyle)
            For i As Integer = 0 To c - 2

                'Add the UCL and LCL line segments as individual line annotations. 
                'UCL line. 
                addLineAnnotation(output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, subgroup(i), UCL(i), subgroup(i + 1) - subgroup(i), UCL(i + 1) - UCL(i), _
                output, UCLstyle)

                'LCL line. 
                addLineAnnotation(output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, subgroup(i), LCL(i), subgroup(i + 1) - subgroup(i), LCL(i + 1) - LCL(i), _
                output, LCLstyle)
            Next

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("PBAR", output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, pseries.Points(0).XValue, pbar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, pseries.Points(0).XValue, UCL(0), output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(pseries.ChartArea).AxisX, output.ChartAreas(pseries.ChartArea).AxisY, pseries.Points(0).XValue, LCL(0), output, _
            LCLstyle)

            'Find the maximum UCL value and store the index of it. 
            Dim maxUCL As Integer = 0
            For i As Integer = 1 To c - 1
                If UCL(maxUCL) < UCL(i) Then
                    maxUCL = i
                End If
            Next

            'Scale the chart if the user has requested it. 
            FitChart(UCL(maxUCL), pseries.ChartArea, output)

            'Return the series. 
            Return pseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma NP-chart class"
    ''' <summary> 
    ''' NP_Chart, based on SixSigmaBase class, provides functionality to create NP type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class NP_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates an NP-Chart with lines indicating CL, UCL and LCL. An NP chart is the same as a P-chart except 
        ''' that the number of items in each subgroup is the same. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the nonconform array.</param> 
        ''' <param name="nonconform">An array holding the non-conformity data associated with the subgroups. Must align with the subgroups.</param> 
        ''' <param name="numbertested">An array holding the number of items in each subgroup. Must align with the subgroups and the nonconform array.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal nonconform As Single(), ByVal numbertested As Integer, ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> nonconform.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, nonconform, numbertested")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize UCL, LCL and pBar. 
            Dim UCL As Single = 0
            Dim LCL As Single = 0
            Dim pbar As Single = 0
            For i As Integer = 0 To c - 1

                'Calculate pbar. 
                pbar += nonconform(i)
            Next
            pbar /= (c * numbertested)

            'Calculate UCL and LCL. 
            UCL = CSng((numbertested * pbar + 3 * System.Math.Sqrt(numbertested * pbar * (1 - pbar))))
            LCL = CSng((numbertested * pbar - 3 * System.Math.Sqrt(numbertested * pbar * (1 - pbar))))

            'If LCL is less than zero, then it should be zero. 
            If LCL < 0 Then
                LCL = 0
            End If

            'Create the npseries 
            Dim npseries As Series = CreateSeries(output, "npseries")
            For i As Integer = 0 To c - 1

                'Graph subgroup vs. non-conforming values. 
                npseries.Points.AddXY(subgroup(i), nonconform(i))
            Next

            'Set the type to line series. 
            npseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(npseries)

            'Add the lines to the graph as annotations. 
            'CL line. 
            addLineAnnotation(output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, pbar * 100, npseries.Points(c - 1).XValue, 0, _
            output, BCLstyle)

            'UCL line. 
            addLineAnnotation(output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, UCL, npseries.Points(c - 1).XValue, 0, _
            output, UCLstyle)

            'LCL line. 
            addLineAnnotation(output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, LCL, npseries.Points(c - 1).XValue, 0, _
            output, LCLstyle)

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("PBAR", output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, pbar * 100, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, UCL, output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(npseries.ChartArea).AxisX, output.ChartAreas(npseries.ChartArea).AxisY, npseries.Points(0).XValue, LCL, output, _
            LCLstyle)

            'Scale the chart if the user has requested it. 
            FitChart(UCL, npseries.ChartArea, output)

            'Return the series. 
            Return npseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma U-chart class"
    ''' <summary> 
    ''' U_Chart, based on SixSigmaBase class, provides functionality to create U type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class U_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates a U-Chart with UBar, UCL and LCL lines. A U-Chart is used when the desired chart 
        ''' is that of the number of non-conformities per inspection unit, where the inspection unit is variable size. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the nonconform array.</param> 
        ''' <param name="nonconform">An array holding the non-conformity data associated with the subgroups. Must align with the subgroups.</param> 
        ''' <param name="numbertested">The number of items in each subgroup.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal nonconform As Single(), ByVal numbertested As Single(), ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If (subgroup.Length <> nonconform.Length) OrElse (subgroup.Length <> numbertested.Length) OrElse (nonconform.Length <> numbertested.Length) Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, nonconform, numbertested")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize UCL, LCL and uBar. 
            Dim UCL As Single() = New Single(c - 1) {}
            Dim LCL As Single() = New Single(c - 1) {}
            Dim ubar As Single = 0

            'Initialize temporary variables to hold temp sums for the ubar calculation 
            Dim sumNumerator As Single = 0
            Dim sumDenominator As Single = 0
            For i As Integer = 0 To c - 1

                'Calculate ubar. 
                sumNumerator += nonconform(i)
                sumDenominator += numbertested(i)
            Next
            ubar = sumNumerator / sumDenominator

            'Calculate the Yvalues 
            Dim yvalues As Single() = New Single(c - 1) {}
            For i As Integer = 0 To c - 1
                yvalues(i) = nonconform(i) / numbertested(i)
            Next
            For i As Integer = 0 To c - 1

                'Calculate UCL and LCL. 
                UCL(i) = CSng((ubar + 3 * System.Math.Sqrt(ubar / numbertested(i))))
                LCL(i) = CSng((ubar - 3 * System.Math.Sqrt(ubar / numbertested(i))))
            Next

            'Create the series for the pchart data. 
            Dim useries As Series = CreateSeries(output, "useries")
            For i As Integer = 0 To c - 1
                'Graph subgroup vs. proportion. 
                useries.Points.AddXY(subgroup(i), yvalues(i))
            Next

            'Set the series type to a line. 
            useries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(useries)

            'Add the lines to the graph as annotations. 
            'UBAR line. 
            addLineAnnotation(output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, useries.Points(0).XValue, ubar, useries.Points(c - 1).XValue, 0, _
            output, BCLstyle)
            For i As Integer = 0 To c - 2

                'Add the UCL and LCL line segments as individual line annotations. 
                'UCL line. 
                addLineAnnotation(output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, subgroup(i), UCL(i), subgroup(i + 1) - subgroup(i), UCL(i + 1) - UCL(i), _
                output, UCLstyle)

                'LCL line. 
                addLineAnnotation(output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, subgroup(i), LCL(i), subgroup(i + 1) - subgroup(i), LCL(i + 1) - LCL(i), _
                output, LCLstyle)
            Next

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("UBAR", output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, useries.Points(0).XValue, ubar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, useries.Points(0).XValue, UCL(0), output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(useries.ChartArea).AxisX, output.ChartAreas(useries.ChartArea).AxisY, useries.Points(0).XValue, LCL(0), output, _
            LCLstyle)

            'Find the maximum UCL value and store the index of it. 
            Dim maxUCL As Integer = 0
            For i As Integer = 1 To c - 1
                If UCL(maxUCL) < UCL(i) Then
                    maxUCL = i
                End If
            Next

            'Scale the chart if the user has requested it. 
            FitChart(UCL(maxUCL), useries.ChartArea, output)

            'Return the series. 
            Return useries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma S-chart class"
    ''' <summary> 
    ''' S_Chart, based on SixSigmaBase class, provides functionality to create S type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class S_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates an S-Chart of prepared data that can be evaluated. If the data is deemed to be in statistical control 
        ''' an XBAR chart can be created via XBARChart function. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' 
        ''' <exception cref="System.ArgumentOutOfRangeException">Thrown when a number of subgroups is less than 20 or 
        ''' n parameter is not between 2 and 9. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array. There must be at least 20 subgroups.</param> 
        ''' <param name="data">Array holding the Standard Deviation of each subgroup.</param> 
        ''' <param name="n">Number of measurements per subgroup. Must be between 2 and 9.</param> 
        ''' <param name="processStdDev">Variable returning the process standard deviation estimation.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal data As Single(), ByVal n As Integer, ByRef processStdDev As Single, ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> data.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, data")
            End If

            'Assure there are at least 20 subgroups 
            If subgroup.Length < 20 Then
                Throw New ArgumentOutOfRangeException("There must be at least 20 subgroups.", "subgroup")
            End If

            'Assure n is between 2 and 9 
            If n < 2 OrElse n > 9 Then
                Throw New ArgumentOutOfRangeException("n must be between 2 and 9.", "n")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize a variable to hold sbar, UCL and LCL. 
            Dim sbar As Single = 0
            Dim UCL As Single = 0
            Dim LCL As Single = 0

            'Declare the constants needed for calculations. 
            Dim B3 As Single() = {0, 0, 0, 0, 0.03F, 0.118F, _
            0.185F, 0.239F}
            Dim B4 As Single() = {3.267F, 2.568F, 2.266F, 2.089F, 1.97F, 1.882F, _
            1.815F, 1.761F}
            Dim C4 As Single() = {0.7979F, 0.8862F, 0.9213F, 0.94F, 0.9515F, 0.9594F, _
            0.965F, 0.9693F}
            For i As Integer = 0 To c - 1

                'Calculate sbar which is the center line of the standard deviation of each subgroup. 
                sbar += data(i)
            Next
            sbar /= c

            'Calculate UCL and LCL. 
            'It is n-2 as the number is between 2 and 9, but the array starts at 0. 
            UCL = B4(n - 2) * sbar
            LCL = B3(n - 2) * sbar

            'If LCL is less than zero, then it should be zero. 
            If LCL < 0 Then
                LCL = 0
            End If

            'Create the series for the sChart data. 
            Dim sseries As Series = CreateSeries(output, "sseries")
            For i As Integer = 0 To c - 1

                'Graph subgroups vs. data. 
                sseries.Points.AddXY(subgroup(i), data(i))
            Next

            'Set the series type to a line. 
            sseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(sseries)

            'Add the lines to the graph as annotations. 
            'sbar line. 
            addLineAnnotation(output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, sbar, [Double].NaN, 0, _
            output, BCLstyle)

            'UCL line. 
            addLineAnnotation(output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, UCL, [Double].NaN, 0, _
            output, UCLstyle)

            'LCL line. 
            addLineAnnotation(output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, LCL, [Double].NaN, 0, _
            output, LCLstyle)

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("SBAR", output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, sbar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, UCL, output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(sseries.ChartArea).AxisX, output.ChartAreas(sseries.ChartArea).AxisY, sseries.Points(0).XValue, LCL, output, _
            LCLstyle)

            'Scale the chart if the user has requested it. 
            FitChart(UCL, sseries.ChartArea, output)

            'Calculate process standard deviation. 
            processStdDev = CSng(((sbar / C4(n - 2)) * System.Math.Sqrt(1 - System.Math.Pow(C4(n - 2), 2))))

            'Set the sBar class variable and clear rBar. 
            Me.mysBar = sbar
            Me.myrBar = 0

            'Store the n value for use by XBAR. 
            Me.mynValue = n

            'Return the series. 
            Return sseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma R-chart class"
    ''' <summary> 
    ''' R_Chart, based on SixSigmaBase class, provides functionality to create R type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class R_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates an R-Chart of prepared data that can be evaluated. If the data is deemed to be in statistical control 
        ''' an XBAR chart can be created via XBARChart function. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' 
        ''' <exception cref="System.ArgumentOutOfRangeException">Thrown when a number of subgroups is less than 20 or 
        ''' n parameter is not between 2 and 9. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array. There must be at least 20 subgroups.</param> 
        ''' <param name="data">Array holding the Range (biggest value - smallest value) of each subgroup.</param> 
        ''' <param name="n">Number of measurements per subgroup. Must be between 2 and 9.</param> 
        ''' <param name="processStdDev">Variable returning the process standard deviation estimation.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal data As Single(), ByVal n As Integer, ByRef processStdDev As Single, ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> data.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, data")
            End If

            'Assure there are at least 20 subgroups 
            If subgroup.Length < 20 Then
                Throw New ArgumentOutOfRangeException("There must be at least 20 subgroups.", "subgroup")
            End If

            'Assure n is between 2 and 9 
            If n < 2 OrElse n > 9 Then
                Throw New ArgumentOutOfRangeException("n must be between 2 and 9.", "n")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize a variable to hold sbar, UCL and LCL. 
            Dim rbar As Single = 0
            Dim UCL As Single = 0
            Dim LCL As Single = 0

            'Declare the constants needed for calculations. 
            Dim D3 As Single() = {0, 0, 0, 0, 0, 0.076F, _
            0.136F, 0.184F}
            Dim D4 As Single() = {3.267F, 2.574F, 2.282F, 2.114F, 2.004F, 1.924F, _
            1.864F, 1.816F}
            Dim D2 As Single() = {1.128F, 1.693F, 2.059F, 2.326F, 2.534F, 2.704F, _
            2.847F, 2.97F}
            For i As Integer = 0 To c - 1

                'Calculate rbar which is the center line of the range of each subgroup. 
                rbar += data(i)
            Next
            rbar /= c

            'Calculate UCL and LCL. 
            'It is n-2 as the number is between 2 and 9, but the array starts at 0. 
            UCL = D4(n - 2) * rbar
            LCL = D3(n - 2) * rbar

            'If LCL is less than zero, then it should be zero. 
            If LCL < 0 Then
                LCL = 0
            End If

            'Create the series for the sChart data. 
            Dim rseries As Series = CreateSeries(output, "rseries")
            For i As Integer = 0 To c - 1

                'Graph subgroups vs. data. 
                rseries.Points.AddXY(subgroup(i), data(i))
            Next

            'Set the series type to a line. 
            rseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(rseries)

            'Add the lines to the graph as annotations. 
            'sbar line. 
            addLineAnnotation(output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, rbar, [Double].NaN, 0, _
            output, BCLstyle)

            'UCL line. 
            addLineAnnotation(output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, UCL, [Double].NaN, 0, _
            output, UCLstyle)

            'LCL line. 
            addLineAnnotation(output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, LCL, [Double].NaN, 0, _
            output, LCLstyle)

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("RBAR", output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, rbar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, UCL, output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(rseries.ChartArea).AxisX, output.ChartAreas(rseries.ChartArea).AxisY, rseries.Points(0).XValue, LCL, output, _
            LCLstyle)

            'Scale the chart if the user has requested it. 
            FitChart(UCL, rseries.ChartArea, output)

            'Calculate process standard deviation. 
            processStdDev = CSng((rbar / D2(n - 2)))

            'Set the rBar class variable and clear sBar. 
            Me.myrBar = rbar
            Me.mysBar = 0

            'Store the n value for use by XBAR. 
            Me.mynValue = n

            'Return the series. 
            Return rseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma Run-chart class"
    ''' <summary> 
    ''' Run_Chart, based on SixSigmaBase class, provides functionality to create Run type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class Run_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates a Run Chart of the data and adds an average line. A run chart is a plot of the data without 
        ''' manipulation along with a line indicating where the average of the points is. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array.</param> 
        ''' <param name="data">An array holding the data. Must align with the subgroup array.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup() As Single, ByVal data As Single(), ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            'If subgroup.Length <> data.Length Then
            '    Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, data")
            'End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer
            c = subgroup.Length

            'Declare and initialize a variable to hold the average. 
            Dim average As Single = 0

            'Create the series for the runchart data. 
            Dim runseries As Series = CreateSeries(output, "runseries")
            For i As Integer = 0 To c - 1

                'Graph subgroups vs. data. 
                runseries.Points.AddXY(subgroup(i), data(i))
            Next

            'Set the series type to a line. 
            runseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(runseries)

            'Calculate the average. 
            'average = (float)output.DataManipulator.Statistics.Mean("runseries"); 
            average = Mean(data)

            'Add the lines to the graph as annotations. 
            'average line. 
            addLineAnnotation(output.ChartAreas(runseries.ChartArea).AxisX, output.ChartAreas(runseries.ChartArea).AxisY, runseries.Points(0).XValue, average, runseries.Points(c - 1).XValue, 0, _
            output, BCLstyle)

            'Return the series. 
            Return runseries
        End Function
#End Region
    End Class
#End Region


#Region "SixSigma XBAR-chart class"
    ''' <summary> 
    ''' XBAR_Chart, based on SixSigmaBase class, provides functionality to create XBAR type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class XBAR_Chart
        Inherits SixSigmaBase
#Region "Public Methods"
        ''' <summary> 
        ''' Creates an XBAR chart of the prepared data. sChart or rChart must have already been called to create a XBAR chart. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null or 
        ''' input arrays have different length. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array.</param> 
        ''' <param name="data">Array holding the Mean of each subgroup. Must align with the subgroup array.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal data As Single(), ByVal output As Chart) As Series
            'Call the private function with no MRBAR to graph a separate XBAR chart. 
            Return XBARChartImplementation(subgroup, data, output, 0)
        End Function

        ''' <summary> 
        ''' ExecuteS_Chart or ExecuteR_Chart methods should be called before CreateSeries for XBAR chart. 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array. There must be at least 20 subgroups.</param> 
        ''' <param name="data">Array holding the Standard Deviation of each subgroup.</param> 
        ''' <param name="n">Number of measurements per subgroup. Must be between 2 and 9.</param> 
        ''' <param name="processStdDev">Variable returning the process standard deviation estimation.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        Public Sub ExecuteS_Chart(ByVal subgroup As Single(), ByVal data As Single(), ByVal n As Integer, ByRef processStdDev As Single, ByVal output As Chart)
            Dim sChart As New S_Chart()

            ' no text and lines 
            Dim lineStyle As New ControlLineStyle()
            lineStyle.LineStyle = ChartDashStyle.NotSet
            lineStyle.ShowText = False
            lineStyle.CopyTo(sChart.BCLstyle)
            lineStyle.CopyTo(sChart.UCLstyle)
            lineStyle.CopyTo(sChart.LCLstyle)

            Dim series As Series = sChart.CreateSeries(subgroup, data, n, processStdDev, output)
            output.Series.Remove(series)
            Me.mysBar = sChart.mysBar
            Me.mynValue = sChart.mynValue
        End Sub

        ''' <summary> 
        ''' ExecuteS_Chart or ExecuteR_Chart methods should be called before CreateSeries for XBAR chart. 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array. There must be at least 20 subgroups.</param> 
        ''' <param name="data">Array holding the Range (biggest value - smallest value) of each subgroup.</param> 
        ''' <param name="n">Number of measurements per subgroup. Must be between 2 and 9.</param> 
        ''' <param name="processStdDev">Variable returning the process standard deviation estimation.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        Public Sub ExecuteR_Chart(ByVal subgroup As Single(), ByVal data As Single(), ByVal n As Integer, ByRef processStdDev As Single, ByVal output As Chart)
            Dim rChart As New R_Chart()

            ' no text and lines 
            Dim lineStyle As New ControlLineStyle()
            lineStyle.LineStyle = ChartDashStyle.NotSet
            lineStyle.ShowText = False
            lineStyle.CopyTo(rChart.BCLstyle)
            lineStyle.CopyTo(rChart.UCLstyle)
            lineStyle.CopyTo(rChart.LCLstyle)

            Dim series As Series = rChart.CreateSeries(subgroup, data, n, processStdDev, output)
            output.Series.Remove(series)
            Me.myrBar = rChart.myrBar
            Me.mynValue = rChart.mynValue
        End Sub

#End Region

#Region "Protected Methods"
        ''' <summary> 
        ''' Creates an XBAR chart of the prepared data. This function contains the implementation for the public function with one extra 
        ''' parameter for MRBAR. If MRBAR is specified, then only one chart is created with all the values. 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array.</param> 
        ''' <param name="data">Array holding the Mean of each subgroup. Must align with the subgroup array.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <param name="mrbar">The value of MRBAR if it has been calculated by Individuals Chart.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Protected Function XBARChartImplementation(ByVal subgroup As Single(), ByVal data As Single(), ByVal output As Chart, ByVal mrbar As Single) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> data.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, nonconform, numbertested")
            End If

            'Assure that either rBar or sBar have a value, and n has a value. If MRBAR has been specified, do not check. 
            If mrbar = 0 Then
                If (Me.myrBar = 0 AndAlso Me.mysBar = 0) OrElse (Me.mynValue < 2 OrElse Me.mynValue > 9) Then
                    Throw New ArgumentException("sChart or rChart must be executed before XBAR.")
                End If
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize a variable to hold sbar, UCL and LCL. 
            Dim xbar As Single = 0
            Dim UCL As Single = 0
            Dim LCL As Single = 0

            'Declare the constants needed for calculations. 
            Dim A2 As Single() = {1.88F, 1.023F, 0.729F, 0.577F, 0.483F, 0.419F, _
            0.373F, 0.337F}
            Dim A3 As Single() = {2.659F, 1.954F, 1.628F, 1.427F, 1.287F, 1.182F, _
            1.099F, 1.032F}
            For i As Integer = 0 To c - 1

                'Calculate xbar, which is the mean of all the subgroup means. 
                xbar += data(i)
            Next
            xbar /= c

            'Calculate UCL and LCL. 
            'Switch based on if we're calculating from a rChart or a sChart, or if a MRBAR was given. 
            'It is n-2 as the number is between 2 and 9, but the array starts at 0 
            If mrbar <> 0 Then
                UCL = CSng((xbar + 2.66 * mrbar))
                LCL = CSng((xbar - 2.66 * mrbar))
            ElseIf Me.mysBar <> 0 Then
                UCL = xbar + A3(Me.mynValue - 2) * Me.mysBar
                LCL = xbar - A3(Me.mynValue - 2) * Me.mysBar
            ElseIf Me.myrBar <> 0 Then
                UCL = xbar + A2(Me.mynValue - 2) * Me.myrBar
                LCL = xbar - A2(Me.mynValue - 2) * Me.myrBar
            End If

            'If LCL is less than zero, then it should be zero. 
            If LCL < 0 Then
                LCL = 0
            End If

            'Create the series for the xbar data. 
            Dim xbarseries As Series = CreateSeries(output, "xbarseries")
            For i As Integer = 0 To c - 1

                'Graph subgroups vs. data. 
                xbarseries.Points.AddXY(subgroup(i), data(i))
            Next

            'Set the series type to a line. 
            xbarseries.Type = SeriesChartType.Line

            'Add the series to the chart. 
            output.Series.Add(xbarseries)

            'Add the lines to the graph as annotations. 
            'xbar line. 
            addLineAnnotation(output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, xbar, [Double].NaN, 0, _
            output, BCLstyle)

            'UCL line. 
            addLineAnnotation(output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, UCL, [Double].NaN, 0, _
            output, UCLstyle)

            'LCL line. 
            addLineAnnotation(output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, LCL, [Double].NaN, 0, _
            output, LCLstyle)

            'MBAR line if it has been specified 
            If mrbar <> 0 Then
                addLineAnnotation(output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, mrbar, xbarseries.Points(c - 1).XValue, 0, _
                output, BCLstyle)
            End If

            'Add Text Annotations (line labels) if the user desired. 
            addTextAnnotation("XBAR", output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, xbar, output, _
            BCLstyle)

            addTextAnnotation("UCL", output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, UCL, output, _
            UCLstyle)

            addTextAnnotation("LCL", output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, LCL, output, _
            LCLstyle)

            If mrbar <> 0 Then
                addTextAnnotation("MRBAR", output.ChartAreas(xbarseries.ChartArea).AxisX, output.ChartAreas(xbarseries.ChartArea).AxisY, xbarseries.Points(0).XValue, mrbar, output, _
                BCLstyle)
            End If

            'Scale the chart if the user has requested it. 
            FitChart(UCL, xbarseries.ChartArea, output)

            'Return the series. 
            Return xbarseries
        End Function
#End Region
    End Class
#End Region


#Region "IndividualsChart class"
    ''' <summary> 
    ''' Individuals_Chart, based on SixSigmaBase class, provides functionality to create Individuals type chart related to the Six Sigma strategy. 
    ''' </summary> 
    Public Class IndividualsChart
        Inherits XBAR_Chart
#Region "Public Methods"
        ''' <summary> 
        ''' Creates an Individuals Chart of the data and places the MRBar, XBAR, UCL and LCL upon it. 
        ''' 
        ''' <exception cref="System.ArgumentException">Thrown when chart parameter is null, or 
        ''' input arrays have different length, or sChart or rChart are not called before XBAR. 
        ''' </exception> 
        ''' </summary> 
        ''' <param name="subgroup">An array holding the subgroups. Must align with the data array.</param> 
        ''' <param name="data">An array holding the data. Must align with the subgroup array.</param> 
        ''' <param name="processStdDev">Variable returning the process standard deviation estimation.</param> 
        ''' <param name="output">The chart which will have the created series added to it.</param> 
        ''' <returns>Series which has been created and added to the output chart.</returns> 
        Public Overloads Function CreateSeries(ByVal subgroup As Single(), ByVal data As Single(), ByRef processStdDev As Single, ByVal output As Chart) As Series
            ' check input parameters 
            If output Is Nothing Then
                Throw New ArgumentException("chart input parameter is null")
            End If

            'Assure the input all have the same number of items. 
            If subgroup.Length <> data.Length Then
                Throw New ArgumentException("Input arrays must all be the same length.", "subgroup, data")
            End If

            'c holds the total number of items (subgroups). 
            Dim c As Integer = subgroup.Length

            'Declare and initialize MRBAR. 
            Dim mrbar As Single = 0
            For i As Integer = 0 To c - 2

                'Calculate cbar. 
                mrbar += CSng(System.Math.Abs(data(i + 1) - data(i)))
            Next
            mrbar /= c - 1

            'Calculate the process standard deviation. 
            processStdDev = CSng((mrbar / 1.128))

            'Return the series 
            Return XBARChartImplementation(subgroup, data, output, mrbar)
        End Function
#End Region
    End Class
#End Region

#Region "SixSigmaBase class"
    ''' <summary> 
    ''' SixSigmaBase is a base class on we built several SixSigma classes. The class provides public math methods and ControlLineStyles properties for 
    ''' UCL, BCL, LCL control lines. Private methods let create these control lines. Real implementation of every chart type for SixSigma see 
    ''' classes that are derived from SixSigmaBase class. 
    ''' </summary> 
    Public Class SixSigmaBase
#Region "Private Members"
        ''' <summary> 
        ''' sBar holds a value if an schart has been created. sBar is used for a XBAR chart. 
        ''' </summary> 
        Friend mysBar As Single = 0

        ''' <summary> 
        ''' rBar holds a value if an rchart has been created. rBar is used for a XBAR chart. 
        ''' </summary> 
        Friend myrBar As Single = 0

        ''' <summary> 
        ''' Holds the n value passed into sChart or rChart for use by XBAR. 
        ''' </summary> 
        Friend mynValue As Integer = 0

        ''' <summary> 
        ''' Holds control line style for Upper Control Line 
        ''' </summary> 
        Private myUCLstyle As New ControlLineStyle()

        ''' <summary> 
        ''' Holds control line style for Bar Control Line 
        ''' </summary> 
        Private myBCLstyle As New ControlLineStyle()

        ''' <summary> 
        ''' Holds control line style for Lower Control Line 
        ''' </summary> 
        Private myLCLstyle As New ControlLineStyle()

        ''' <summary> 
        ''' Holds flag to fit lines to ChartArea's GridLines 
        ''' </summary> 
        Private myAutoFitLines As Boolean = False

#End Region

#Region "Public Members"
        ''' <summary> 
        ''' Controls whether the AxisY will have an automatic Maximum set so that the chart will always 
        ''' contain the UCL annotation. If left to false, it is up to the user to set a maximum AxisY value 
        ''' such that all annotations appear on the graph. 
        ''' </summary> 
        Public Property AutoFitLines() As Boolean
            Get
                Return myAutoFitLines
            End Get
            Set(ByVal value As Boolean)
                myAutoFitLines = value
            End Set
        End Property

        ''' <summary> 
        ''' Style of Upper Control Line. This style will be applyed to UCL lines. 
        ''' </summary> 
        Public ReadOnly Property UCLstyle() As ControlLineStyle
            Get
                Return myUCLstyle
            End Get
        End Property

        ''' <summary> 
        ''' Style of Bar Control Line. This style will be applyed to Bar lines. 
        ''' </summary> 
        Public ReadOnly Property BCLstyle() As ControlLineStyle
            Get
                Return myBCLstyle
            End Get
        End Property

        ''' <summary> 
        ''' Style of Lower Control Line. This style will be applyed to LCL lines. 
        ''' </summary> 
        Public ReadOnly Property LCLstyle() As ControlLineStyle
            Get
                Return myLCLstyle
            End Get
        End Property

#End Region

#Region "Math Methods"
        ''' <summary> 
        ''' Calculates the mean of an array of numbers. 
        ''' </summary> 
        ''' <param name="data">The input array of items of which the mean will be calculated.</param> 
        ''' <returns>The mean of the data.</returns> 
        Public Function Mean(ByVal data As Single()) As Single
            'Create a temporary variable to hold the sum. 
            Dim sum As Single = 0
            For i As Integer = 0 To data.Length - 1

                'Calculate the sum of the data items. 
                sum += data(i)
            Next

            'Divide them by the total number of items. 
            sum /= data.Length

            'Return the mean. 
            Return sum
        End Function

        ''' <summary> 
        ''' Calculates the range of an array of numbers. 
        ''' </summary> 
        ''' <param name="data">The input array of items of which the range will be calculated.</param> 
        ''' <returns>The range of the data.</returns> 
        Public Function Range(ByVal data As Single()) As Single
            'Create two variables: one to hold the index of the largest value and one to hold the index of the smallest value. 
            Dim maxIndex As Integer = 0
            Dim minIndex As Integer = 0
            For i As Integer = 1 To data.Length - 1

                'Find the largest and smallest values in the array. 
                If data(maxIndex) < data(i) Then
                    maxIndex = i
                End If

                If data(minIndex) > data(i) Then
                    minIndex = i
                End If
            Next

            'Return the Range. 
            Return (data(maxIndex) - data(minIndex))
        End Function

        ''' <summary> 
        ''' Calculates the standard deviation of an array of numbers. 
        ''' </summary> 
        ''' <param name="data">The input array of items of which the standard deviation will be calculated.</param> 
        ''' <returns>The standard deviation of the data.</returns> 
        Public Function StandardDeviation(ByVal data As Single()) As Single
            'Create two variables: one to hold the sum of the deviations, and one to hold the mean of the data. 
            Dim sumdeviation As Single = 0
            Dim mean As Single = 0

            'Calculate the mean. 
            mean = Me.Mean(data)
            For i As Integer = 0 To data.Length - 1

                'Calculate the sum of the squared deviations. 
                sumdeviation += CSng(System.Math.Pow(data(i) - mean, 2))
            Next

            'Return the standard deviation. 
            Return CSng(System.Math.Sqrt(sumdeviation / (data.Length - 1)))
        End Function
#End Region

#Region "Protected Methods"
        ''' <summary> 
        ''' Scales the chart so that the UCL line will always be within the chart. 
        ''' </summary> 
        ''' <param name="UCL">The maximum UCL value.</param> 
        ''' <param name="ChartArea">The name of the chart area we are plotting to.</param> 
        ''' <param name="output">The chart which contains the chart area.</param> 
        Protected Sub FitChart(ByVal UCL As Single, ByVal ChartArea As String, ByVal output As Chart)
            'Check to see if scaling is enabled. 
            If myAutoFitLines Then
                'Force the chartarea to recalculate the axis values so that we can find the Y-axis maximum. 
                output.ChartAreas(ChartArea).ReCalc()

                'Check if UCL is outside the maximum Y value. 
                If UCL > CSng(output.ChartAreas(ChartArea).AxisY.Maximum) Then
                    'If so set the Y-axis maximum to be the UCL value. 
                    output.ChartAreas(ChartArea).AxisY.Maximum = UCL
                End If
            Else
                'Set the Y-axis maximum to be auto-scaled. 
                output.ChartAreas(ChartArea).AxisY.Maximum = [Double].NaN
            End If
        End Sub

        ''' <summary> 
        ''' Function to add a line annotation to the desired chart in the format which is followed by 
        ''' all the line annotations in this add-on. 
        ''' </summary> 
        ''' <param name="AxisX">X-Axis that the line annotation is to use for co-ordinates.</param> 
        ''' <param name="AxisY">Y-Axis that the line annotation is to use for co-ordinates.</param> 
        ''' <param name="X">X value for the start of the line.</param> 
        ''' <param name="Y">Y value for the start of the line.</param> 
        ''' <param name="Width">Width of the line.</param> 
        ''' <param name="Height">Height of the line.</param> 
        ''' <param name="output">The chart that the line annotation is being added to.</param> 
        ''' <param name="lineStyle">The controlLineStyle that has LineStyle, LineColor and LineWidth values for annotation.</param> 
        Protected Sub addLineAnnotation(ByVal AxisX As Axis, ByVal AxisY As Axis, ByVal X As Double, ByVal Y As Double, ByVal Width As Double, ByVal Height As Double, _
        ByVal output As Chart, ByVal lineStyle As ControlLineStyle)
            If lineStyle.LineStyle <> ChartDashStyle.NotSet Then
                'Create a new line annotation. 
                Dim lineAnnotation As New LineAnnotation()

                'Set each property to the parameters passed in. 
                lineAnnotation.AxisX = AxisX
                lineAnnotation.AxisY = AxisY
                lineAnnotation.Y = Y
                lineAnnotation.X = X
                lineAnnotation.Height = Height

                ' if Width parameter is Double.NaN, LineAnnotation is straight line from X to the Maximum of AxisX. 
                If [Double].IsNaN(Width) Then
                    If [Double].IsNaN(AxisX.Maximum) Then
                        output.ChartAreas(0).ReCalc()
                    End If
                    lineAnnotation.Width = AxisX.Maximum - X
                Else
                    lineAnnotation.Width = Width
                End If

                ' line style 
                lineAnnotation.LineStyle = lineStyle.LineStyle
                lineAnnotation.LineColor = lineStyle.LineColor
                lineAnnotation.LineWidth = lineStyle.LineWidth

                'Turn off relative size to get graph-oriented co-ordinates and set the line color. 
                lineAnnotation.SizeAlwaysRelative = False

                'Add the annotation to the chart. 
                output.Annotations.Add(lineAnnotation)
            End If
        End Sub

        ''' <summary> 
        ''' Function to add text as an annotation to the chart. 
        ''' </summary> 
        ''' <param name="text">The text that we wish to display.</param> 
        ''' <param name="AxisX">X-Axis that the text annotation is to use for co-ordinates.</param> 
        ''' <param name="AxisY">Y-Axis that the text annotation is to use for co-ordinates.</param> 
        ''' <param name="X">X value for the start of the text.</param> 
        ''' <param name="Y">Y value for the start of the text.</param> 
        ''' <param name="output">The chart that the line annotation is being added to.</param> 
        ''' <param name="controlLineStyle">The control line style that has TextColor, TextFont values for annotation.</param> 
        Protected Sub addTextAnnotation(ByVal text As String, ByVal AxisX As Axis, ByVal AxisY As Axis, ByVal X As Double, ByVal Y As Double, ByVal output As Chart, _
        ByVal controlLineStyle As ControlLineStyle)
            If controlLineStyle.ShowText Then
                'Create a new line annotation. 
                Dim textAnnotation As New TextAnnotation()

                'Set each property to the parameters passed in. 
                textAnnotation.AxisX = AxisX
                textAnnotation.AxisY = AxisY
                textAnnotation.Y = Y
                textAnnotation.X = X
                textAnnotation.Text = text
                textAnnotation.TextColor = controlLineStyle.TextColor
                textAnnotation.TextFont = controlLineStyle.TextFont

                'Turn off relative size to get graph-oriented co-ordinates and set the aesthetic properties. 
                textAnnotation.SizeAlwaysRelative = False

                'Add the annotation to the chart. 
                output.Annotations.Add(textAnnotation)
            End If
        End Sub

        Protected Overloads Function CreateSeries(ByVal chart As Chart, ByVal seriesName As String) As Series
            If chart.Series.GetIndex(seriesName) < 0 Then
                Return New Series(seriesName)
            End If

            ' generate unique series name 
            Dim i As Integer = 0
            While True
                If chart.Series.GetIndex(seriesName + i.ToString()) < 0 Then
                    Return New Series(seriesName + i.ToString())
                End If
                i += 1
            End While

            ' You won't get here, this prevents a compiler warning.
            Return New Series()
        End Function
#End Region
    End Class

#End Region

#Region "ControlLineStyle class"
    ''' <summary> 
    ''' ControlLineStyle class lets save some properties of Line and Text 
    ''' </summary> 
    Public Class ControlLineStyle
#Region "Public Members"
        ''' <summary> 
        ''' Gets and sets LineStyle 
        ''' </summary> 
        Public LineStyle As ChartDashStyle = ChartDashStyle.Solid

        ''' <summary> 
        ''' Gets and sets LineColor 
        ''' </summary> 
        Public LineColor As Color = Color.Red

        ''' <summary> 
        ''' Gets and sets LineWidth 
        ''' </summary> 
        Public LineWidth As Integer = 1

        ''' <summary> 
        ''' Controls whether text is added to the lines to label what they are. 
        ''' </summary> 
        Public ShowText As Boolean = True

        ''' <summary> 
        ''' Specifies the color of the text that will label the lines. 
        ''' </summary> 
        Public TextColor As Color = Color.Red

        ''' <summary> 
        ''' Specifies the font of the text that will label the lines. 
        ''' </summary> 
        Public TextFont As New Font("Arial", 8)

#End Region

#Region "Constructors"
        ''' <summary> 
        ''' Default constructor. Create instance of ControlLineStyle class with default values of properties: 
        ''' LineStyle as Solid, LineColr as Red, LineWidth as 1, ShowText as true, TextColor as Red, TextFont as Arial,8 
        ''' </summary> 
        Public Sub New()
            ' do nothing 
        End Sub

        ''' <summary> 
        ''' Create instance of ControlLineStyle with passed parameters 
        ''' </summary> 
        ''' <param name="lineStyle">LineStyle</param> 
        ''' <param name="lineColor">LineColor</param> 
        ''' <param name="lineWidth">LineWidth</param> 
        ''' <param name="showText">Flag to show text with the line</param> 
        ''' <param name="textColor">TextColor</param> 
        ''' <param name="textFont">TextFont</param> 
        Public Sub New(ByVal lineStyle As ChartDashStyle, ByVal lineColor As Color, ByVal lineWidth As Integer, ByVal showText As Boolean, ByVal textColor As Color, ByVal textFont As Font)
            Me.LineStyle = lineStyle
            Me.LineColor = lineColor
            Me.LineWidth = lineWidth

            Me.ShowText = showText
            Me.TextColor = textColor
            Me.TextFont = textFont
        End Sub

        ''' <summary> 
        ''' Create instance of ControlLineStyle and get values from another ControlLineStyle instance 
        ''' </summary> 
        ''' <param name="otherStyle">Source of properties for style.</param> 
        Public Sub New(ByVal otherStyle As ControlLineStyle)
            otherStyle.CopyTo(Me)
        End Sub
#End Region

#Region "Public Methods"
        ''' <summary> 
        ''' Copies properties of current instance to another instance of ControlLineStyle. 
        ''' </summary> 
        ''' <param name="otherStyle"></param> 
        Public Sub CopyTo(ByVal otherStyle As ControlLineStyle)
            otherStyle.LineStyle = Me.LineStyle
            otherStyle.LineColor = Me.LineColor
            otherStyle.LineWidth = Me.LineWidth

            otherStyle.ShowText = Me.ShowText
            otherStyle.TextColor = Me.TextColor
            otherStyle.TextFont = Me.TextFont
        End Sub
#End Region
    End Class
#End Region
End Namespace