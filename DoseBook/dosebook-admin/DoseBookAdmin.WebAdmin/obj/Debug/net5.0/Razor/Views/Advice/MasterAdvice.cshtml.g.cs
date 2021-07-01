#pragma checksum "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5892ad2d160ac15cd239ace852939f1e52446ef7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Advice_MasterAdvice), @"mvc.1.0.view", @"/Views/Advice/MasterAdvice.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\_ViewImports.cshtml"
using DoseBookAdmin.WebAdmin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\_ViewImports.cshtml"
using DoseBookAdmin.WebAdmin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5892ad2d160ac15cd239ace852939f1e52446ef7", @"/Views/Advice/MasterAdvice.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0102b2e221a485a09b4bdd4e288c17a18a893f1", @"/Views/_ViewImports.cshtml")]
    public class Views_Advice_MasterAdvice : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DoseBookAdmin.Dto.Advice.AdviceDtoList>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/Advice/advice.masterAdvice.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
  
    ViewData["Title"] = "Master Advice List";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""top_filter_bar"">
    <div class=""center_wrap"">

        <div class=""top_filter_left"">
            <button type=""button"" id=""AddRow"" class=""add_new_list_item"">Add new</button>
            <button id=""btnDeleteAllCheckeMasterAdvice"" type=""button"" class=""delete_list_item"">Delete</button>
        </div>
        <button type=""button"" id=""Save_MasterAdvice_Btn"" class=""btn_field_new""");
            BeginWriteAttribute("style", " style=\"", 548, "\"", 556, 0);
            EndWriteAttribute();
            WriteLiteral(@">Save</button>
        <div class=""top_filter_right"">
            <div class=""form_group"">
                <fieldset>
                    <input type=""text"" id=""txtSearchMasterAdvice"" class=""form-control"" placeholder=""Search"">
                    <input type=""button"" class=""search_btn"" value=""go"" />
                </fieldset>
            </div>
        </div>

    </div>
</div>
<!--End top_filter_bar-->


<div class=""full_width"">
    <div class=""center_wrap"">

        <div class=""content_wrap"">

            <div class=""page_heading"">
                <h1>Master Advices List</h1>
            </div>
            <div class=""table_wrap"">
                <table id=""tblMasterAdvices"" class=""table"">
                    <thead>
                        <tr>
                            <th style=""width: 70px;"">
                                <div class=""table_checkbox"">
                                    <input type=""checkbox"" id=""checkall"" class=""check_all"">
                            ");
            WriteLiteral(@"        <label for=""checkall"">Accept</label>
                                </div>
                            </th>
                            <th style=""width: 500px;"">
                                <button type=""button"" class=""table_title active""><i class=""icon asc""></i>Description</button>
                            </th>
                            <th class=""editEndUser"" style=""width:75px;text-align: center;"">Edit</th>
                            <th class=""deleteEndUser"" style=""width:75px;text-align: center;"">Delete</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 55 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
                          
                            int index = 0;
                            foreach (var adviceDto in Model)
                            {
                                index = index + 1;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <tr>
                                    <td style=""width: 70px;"">
                                        <div class=""table_checkbox"">
                                            <input type=""checkbox"" id=""checkitem1"" class=""check_item"">
                                            <label for=""checkitem1""></label>
                                        </div>
                                    </td>
                                    <td style=""width: 500px;"">");
#nullable restore
#line 67 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
                                                         Write(adviceDto.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td style=\"width:75px;text-align: center;\">\r\n                                        <button type=\"button\" class=\"edit_item\"");
            BeginWriteAttribute("onClick", " onClick=\"", 3130, "\"", 3175, 5);
            WriteAttributeValue("", 3140, "EditRedirect(", 3140, 13, true);
#nullable restore
#line 69 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
WriteAttributeValue("", 3153, adviceDto.Id, 3153, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3166, ",", 3166, 1, true);
#nullable restore
#line 69 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
WriteAttributeValue(" ", 3167, index, 3168, 6, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3174, ")", 3174, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Edit</button>\r\n                                    </td>\r\n                                    <td style=\"width:75px;text-align: center;\">\r\n                                        <span style=\"display:none\">");
#nullable restore
#line 72 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
                                                              Write(adviceDto.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                        <button type=\"button\" class=\"delete_item masterAdviceItem\">delete</button>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 76 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--End full_width-->\r\n<!--Modal Popup-->\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5892ad2d160ac15cd239ace852939f1e52446ef79664", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 86 "D:\Projects\Git Projects\dosebook-admin\DoseBookAdmin.WebAdmin\Views\Advice\MasterAdvice.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            WriteLiteral("<link href=\"https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css\" rel=\"Stylesheet\" type=\"text/css\" />\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DoseBookAdmin.Dto.Advice.AdviceDtoList> Html { get; private set; }
    }
}
#pragma warning restore 1591
