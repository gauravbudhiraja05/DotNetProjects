<%@ Page Language="VB" AutoEventWireup="false" CodeFile="analysisformulasimple.aspx.vb" Inherits="DataAnalysis_analysisformulasimple" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html  xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head id="Head1" runat="server">
        <title>Formulas</title>
        
        <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
    </head>
    <body>
            <form dir="ltr" id="form1" runat="server">
            <div>
       
       <table width="100%"  summary="This Is Showing The Name Of Formulas To Be Applied On Selected Columns" style="width:100%;">
                    <caption class="caption" style ="background-color:#0591D3"> SELECT FORMULA
                    </caption>
                      <tr>
                                 <%--<td title="SELECT FORMULA" scope="row" id="tdshowformulas" class="label" style="background-color: #59afbb; font-weight:bold; text-align:center;" colspan="4">
                                 SELECT FORMULA
                             </td>--%>
                      </tr>

                     <tr style=" background-color:#59afbb;">

                    </tr>
                    <tr>
                              <th scope="col" title="Formula" align="left" class="label" style="width: 147px; height: 17px;" >
                                  <span style="text-decoration: underline">Formula </span>
                             </th>
                             <th scope="col" title="Select" align="left" class="label" style="width: 162px; height: 17px;">
                               <span style="text-decoration: underline">Select</span>
                             </th>
                             <th scope="col" title="Formula" align="left" class="label" style="width: 149px; height: 17px;">
                                <span style="text-decoration: underline">Formula </span>
                             </th>
                             <th scope="col" align="left" title="Select" class="label" style="width: 183px; height: 17px;">
                              <span style="text-decoration: underline">Select</span>
                             </th>
                   </tr>


                   <tr>
                             <td class="label" id="reg" scope="col" title="Regression"  runat="server" style="width: 147px">Regression</td>
                             <td style="width: 162px" scope="col">
                             <label for="regression" ></label>
                                <asp:CheckBox ToolTip="Regression" id="regression" runat="server" AccessKey="r"/>
                             </td>

                             <td id="correl" title="Correlation" class="label" scope="col" runat="server" style="width: 149px">Correlation </td>

                            <td style="width: 183px">
                             <label for="correlation" ></label>
                             <asp:CheckBox ToolTip="Correlation"  id="correlation" runat="server" AccessKey="l"/>
                             </td>

                   </tr>

                      <tr visible="false">
                              <td visible="false" id="cmpsmp" title="Compare Samples" class="label" scope="col" runat="server" style="width: 147px">Compare Samples</td>
                              <td visible="false" style="width: 162px">
                              <label for="comparesamples" ></label>
                                <asp:CheckBox visible="false" ToolTip="Compare Samples"  id="comparesamples" runat="server"/>
                              </td>
                             <td visible="false" id="nweighted" title="Non-Weighted Number" class="label" scope="col" runat="server" style="width: 149px">Non-Weighted Number</td>
                            <td visible="false" style="width: 183px" scope="col">
                             <label for="nonweightednumber" ></label>
                               <asp:CheckBox visible="false" ToolTip="Non-Weighted Number" id="nonweightednumber" runat="server"/>
                            </td>

                           </tr>
                      
                      <tr>
                                    <td id="clmper" title="Column percentage" class="label" scope="col" runat="server" style="width: 147px">Column percentage</td>
                                   <td style="width: 162px" scope="col">
                                    <label for="columnpercentage" ></label>
                                      <asp:CheckBox ToolTip="Column percentage"  id="columnpercentage" runat="server"/>
                                      </td>
                                       <td id="rows" title="Row Percentage" class="label" scope="col" runat="server" style="width: 149px">Row Percentage</td>
                                      <td style="width: 183px">
                                       <label for="rowpercentage" ></label>
                                              <asp:CheckBox ToolTip="Row Percentage" id="rowpercentage" runat="server"/>
                                      </td>
                       

                     </tr>

                          <%--<tr>
                             <td id="fltper" title="Filter Percentage" class="label" scope="col"  runat="server" style="width: 147px">
                                  Filter Percentage
                             </td>
                             <td style="width: 162px">
                             <asp:CheckBox ToolTip="Filter Percentage" id="filterpercentage" runat="server" AccessKey="f"/>
                             </td>

                             <td id="indx" runat="server" title="Index" class="label" scope="col" style="width: 146px">
                               Index
                             </td>
                             <td style="width: 183px">
                                <asp:CheckBox ToolTip="Index" id="index" runat="server"/>
                             </td>

                          </tr>
                        --%>
                      <tr>
                                   <td id="meanval" title="Mean" class="label" scope="col" runat="server" style="width: 147px; height: 22px;">Mean</td>
                                   <td style="width: 162px; height: 22px;">
                                    <label for="mean" ></label>
                                      <asp:CheckBox ToolTip="Mean" id="mean" runat="server" AccessKey="m"/>
                                   </td>
                                   <td id="mid" title="Median" class="label" scope="col"  runat="server" style="width: 149px; height: 22px;">Median</td>
                                    <td style="width: 183px; height: 22px;">
                                      <label for="median" ></label>
                                       <asp:CheckBox ToolTip="Median"  id="median" runat="server" AccessKey="i"/>
                                    </td>

                      </tr>

                      <tr>
                                        <td id="modeval" title="Mode" class="label" scope="col" runat="server" style="width: 147px; height: 22px">Mode</td>
                                        <td style="width: 162px; height: 22px">
                                         <label for="mode" ></label>
                                            <asp:CheckBox ToolTip="Mode" id="mode" runat="server" AccessKey="d"/>
                                        </td>
                                        <td id="rangeval" runat="server"  title="Range" class="label" scope="col" style="width: 149px; height: 22px">Range</td>
                                        <td style="width: 183px; height: 22px;" scope="col">
                                           <label for="range" ></label>
                                          <asp:CheckBox ToolTip="Range" id="range" runat="server" AccessKey="e"/> 
                                        </td>

                          </tr>

                      <tr>
                                         <td id="deviation" runat="server" title="Standard deviation" class="label" scope="col" style="width: 147px">Standard deviation</td>
                                         <td style="width: 162px" scope="col">
                                          <label for="standarddeviation" ></label>
                                             <asp:CheckBox ToolTip="Standard deviation" id="standarddeviation" runat="server" AccessKey="s"/>
                                         </td>
                                         <td id="ster" runat="server" title="Standard error" class="label" scope="col" style="width: 149px">Standard error</td>
                                         <td style="width: 183px" scope="col">
                                          <label for="standarderror" ></label>
                                         <asp:CheckBox ToolTip="Standard error" id="standarderror" runat="server"/></td>

                      </tr>

                      <tr>
                                         <td id="rowsum" runat="server" title="Row Sum Percentage" class="label" scope="col" style="width: 147px; height: 22px">Row Sum Percentage</td>
                                         <td style="width: 162px; height: 22px" scope="col">
                                         <label for="rowsumpercentage" ></label>
                                            <asp:CheckBox ToolTip="Row Sum Percentage" id="rowsumpercentage" runat="server" AccessKey="p"/>
                                         </td>
                                         <td id="columnsum" runat="server" title="Column Sum Percentage" class="label" scope="col"  style="width: 149px; height: 22px">Column Sum Percentage</td>
                                         <td style="width: 183px; height: 22px;" scope="col">
                                          <label for="columnsumpercentage" ></label>
                                            <asp:CheckBox ToolTip="Column Sum Percentage" id="columnsumpercentage" runat="server" AccessKey="u"/>
                                        </td>

                      </tr>
                      <tr>
                                         <td id="accumulate" title="Accumulated Sum" class="label" scope="col" runat="server" style="width: 147px; height: 22px">Accumulated Sum</td>
                                         <td style="width: 162px; height: 22px" scope="col"> 
                                               <label for="accumulatedsum" ></label>
                                               <asp:CheckBox ToolTip="Accumulated Sum" id="accumulatedsum" runat="server" AccessKey="a"/>
                                         </td>
                                         <td id="mini" runat="server" title="Min" class="label" scope="col" style="width: 149px; height: 22px">Min</td>
                                         <td style="width: 183px; height: 22px;" scope="col">
                                          <label for="min" ></label>
                                              <asp:CheckBox ToolTip="Min" id="min" runat="server" AccessKey="z"/>
                                         </td>

                      </tr>
                      <tr> 
                                         <td id="maxi" runat="server" title="Max" class="label" scope="col" style="width: 147px; height: 23px;">Max</td>
                                         <td style="width: 162px; height: 23px;" scope="col">
                                           <label for="max" ></label>
                                             <asp:CheckBox ToolTip="Max" id="max" runat="server" AccessKey="x"/>
                                         </td>
                                         <td id="counts" title="Count" class="label" scope="col"  runat="server" style="width: 149px; height: 23px;">Count</td>
                                         <td style="width: 183px; height: 23px;" scope="col">
                                         <label for="count" ></label>
                                              <asp:CheckBox ToolTip="Count" id="count" runat="server" AccessKey="t"/>
                                         </td>

                      </tr>
                      <tr>
                                         <td id="avg" title="Average" class="label" scope="col" runat="server" style="width: 147px; height: 23px;">Average</td>
                                         <td style="width: 162px; height: 23px;" scope="col">
                                            <label for="average" ></label>
                                            <asp:CheckBox ToolTip="Average" id="average" runat="server" AccessKey="v"/>
                                         </td>
                      </tr>
                      <tr>
                                         <td style="width: 147px; height: 27px;" scope="col">
                                         </td>
                                         <td style="width: 162px; height: 27px;" scope="col">
                                         </td>
                                         <td style="width: 149px; height: 27px;" scope="col">
                                         
                                         </td>
                                         <td style="width: 183px; height: 27px;" scope="col">
                                             <asp:Button ToolTip="OK" CssClass="button" id="ok" text="OK" runat="server" Width="97px" AccessKey="o" /> 
                                        </td>
                      </tr>
          </table>


        </div>
      </form>
   </body>
</html>
