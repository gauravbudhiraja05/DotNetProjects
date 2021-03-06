#pragma checksum "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8263c44d6de73c0e53f95f6378b7a8371c62577"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SuperAdmin_AdminUsers), @"mvc.1.0.view", @"/Views/SuperAdmin/AdminUsers.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SuperAdmin/AdminUsers.cshtml", typeof(AspNetCore.Views_SuperAdmin_AdminUsers))]
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
#line 1 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\_ViewImports.cshtml"
using Dream14.WebAdmin;

#line default
#line hidden
#line 2 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\_ViewImports.cshtml"
using Dream14.WebAdmin.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8263c44d6de73c0e53f95f6378b7a8371c62577", @"/Views/SuperAdmin/AdminUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d66911b042d391ea5d66d7f3b135411b6a5ca4e", @"/Views/_ViewImports.cshtml")]
    public class Views_SuperAdmin_AdminUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dream14.ViewModels.SuperAdmin.AdminUser>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/SuperAdmin/superAdmin.adminUsers.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
#line 2 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
  
	ViewData["Title"] = "Admin Users";
	Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(149, 147, true);
            WriteLiteral("\r\n<div class=\"top_filter_bar\">\r\n\t<div class=\"center_wrap\">\r\n\r\n\t\t<div class=\"top_filter_left\">\r\n\r\n\t\t\t<button type=\"button\" class=\"add_new_list_item\"");
            EndContext();
            BeginWriteAttribute("onClick", " onClick=\"", 296, "\"", 369, 3);
            WriteAttributeValue("", 306, "window.location.href=\'", 306, 22, true);
#line 12 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
WriteAttributeValue("", 328, Url.Action("AddAdminUser","SuperAdmin"), 328, 40, false);

#line default
#line hidden
            WriteAttributeValue("", 368, "\'", 368, 1, true);
            EndWriteAttribute();
            BeginContext(370, 1673, true);
            WriteLiteral(@">Add new</button>
			<button id=""btnDeleteAllCheckedAdmin"" type=""button"" class=""delete_list_item"">delete</button>

		</div>

		<div class=""top_filter_right"">


			<div class=""form_group"">
				<fieldset>
					<input type=""text"" id=""txtSearchAdminList"" class=""form-control"" placeholder=""Search"">
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
				<h1>Admin User List </h1>
			</div>
			<!--End page_heading-->

			<div class=""table_wrap"">
				<table id=""tblAdmins"" class=""table"">
					<thead>
						<tr>
							<th style=""width: 70px;"">
								<div class=""table_checkbox"">
									<input type=""checkbox"" id=""checkall"" class=""check_all"">
									<label for=""checkall"">Accept</label>
								</div>
							</th>
							<th style=""width: 300px;"">
								<button type=""button"" cla");
            WriteLiteral(@"ss=""table_title active""><i class=""icon asc""></i>Full Name</button>
							</th>
							<th>
								<button type=""button"" class=""table_title""><i class=""icon""></i>Email Address</button>
							</th>
							<th>
								<button type=""button"" class=""table_title""><i class=""icon""></i>Creation Date</button>
							</th>
							<th>
								<button type=""button"" class=""table_title""><i class=""icon""></i>Is Active</button>
							</th>
							<th class=""editEndUser"" style=""width:75px;text-align: center;"">Edit</th>
							<th class=""deleteEndUser"" style=""width:75px;text-align: center;"">Delete</th>
						</tr>
					</thead>
					<tbody>
");
            EndContext();
#line 70 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                         foreach (var admin in Model)
						{

#line default
#line hidden
            BeginContext(2089, 245, true);
            WriteLiteral("\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t<td style=\"width: 70px;\">\r\n\t\t\t\t\t\t\t\t\t<div class=\"table_checkbox\">\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" id=\"checkitem1\" class=\"check_item\">\r\n\t\t\t\t\t\t\t\t\t\t<label for=\"checkitem1\"></label>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2335, 14, false);
#line 79 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                               Write(admin.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(2349, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2369, 18, false);
#line 80 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                               Write(admin.EmailAddress);

#line default
#line hidden
            EndContext();
            BeginContext(2387, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2407, 24, false);
#line 81 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                               Write(admin.CreatedDateDisplay);

#line default
#line hidden
            EndContext();
            BeginContext(2431, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2451, 14, false);
#line 82 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                               Write(admin.IsActive);

#line default
#line hidden
            EndContext();
            BeginContext(2465, 108, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td style=\"width:75px;text-align: center;\">\r\n\t\t\t\t\t\t\t\t\t<button type=\"button\" class=\"edit_item\"");
            EndContext();
            BeginWriteAttribute("onClick", " onClick=\"", 2573, "\"", 2666, 3);
            WriteAttributeValue("", 2583, "window.location.href=\'", 2583, 22, true);
#line 84 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
WriteAttributeValue("", 2605, Url.Action("EditAdminUser","SuperAdmin",new { Id=admin.Id}), 2605, 60, false);

#line default
#line hidden
            WriteAttributeValue("", 2665, "\'", 2665, 1, true);
            EndWriteAttribute();
            BeginContext(2667, 120, true);
            WriteLiteral(">edit</button>\r\n\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t<td style=\"width:75px;text-align: center;\">\r\n\t\t\t\t\t\t\t\t\t<span style=\"display:none\">");
            EndContext();
            BeginContext(2788, 8, false);
#line 87 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
                                                          Write(admin.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2796, 118, true);
            WriteLiteral("</span>\r\n\t\t\t\t\t\t\t\t\t<button type=\"button\" class=\"delete_item adminItem\">delete</button>\r\n\t\t\t\t\t\t\t\t</td>\r\n\r\n\t\t\t\t\t\t\t</tr>\r\n");
            EndContext();
#line 92 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"

						}

#line default
#line hidden
            BeginContext(2925, 178, true);
            WriteLiteral("\t\t\t\t\t</tbody>\r\n\t\t\t\t</table>\r\n\t\t\t</div>\r\n\r\n\r\n\r\n\r\n\t\t</div>\r\n\t\t<!--End content_wrap-->\r\n\r\n\r\n\r\n\t</div>\r\n\t<!--End center_wrap-->\r\n\r\n</div>\r\n<!--End full_width-->\r\n<!--Modal Popup-->\r\n");
            EndContext();
            BeginContext(3103, 90, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eee2eba2a3c248c69507b9706cd916a4", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 112 "D:\GitHub\DotNetProjects\DotNetCore\Dream14\Dream14.WebAdmin\Views\SuperAdmin\AdminUsers.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3193, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Dream14.ViewModels.SuperAdmin.AdminUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591
