#pragma checksum "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21539ad75180877975b7f65aef05bc16bd837b1f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BettingApp.WebApps.WebRazorPages.Pages.Shared.Pages_Shared__BetslipSelectionPartial), @"mvc.1.0.view", @"/Pages/Shared/_BetslipSelectionPartial.cshtml")]
namespace BettingApp.WebApps.WebRazorPages.Pages.Shared
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
#line 3 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\_ViewImports.cshtml"
using BettingApp.WebApps.WebRazorPages.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21539ad75180877975b7f65aef05bc16bd837b1f", @"/Pages/Shared/_BetslipSelectionPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d09d2985af4fd647a03c83a8b7bd76aae2be1195", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared__BetslipSelectionPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Selection>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("betslip-selection-remove-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Betslip/Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "RemoveSelection", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
   
    bool displayTicker = !Model.IsCanceled &&
                         !Model.CurrentMinute.Equals("0") &&
                         !Model.CurrentMinute.Equals("HT") &&
                         !Model.CurrentMinute.Equals("FT");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"betslip-selection\">\r\n    <div class=\"betslip-selection-match-id\">\r\n        <strong>");
#nullable restore
#line 11 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
           Write(Model.RelatedMatchId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n    <div class=\"betslip-selection-current-minute\">\r\n        <div");
            BeginWriteAttribute("id", " id=\"", 464, "\"", 502, 3);
            WriteAttributeValue("", 469, "Selection#", 469, 10, true);
#nullable restore
#line 14 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 479, Model.Id, 479, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 488, "#CurrentMinute", 488, 14, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 15 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
             if (!Model.IsCanceled)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
            Write(Model.CurrentMinute.Equals("0") ? Model.KickoffDateTime.ToString("MM/dd HH:mm") : Model.CurrentMinute );

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                                                                                                         
            }
            else
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
            Write("Canceled");

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                             
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>        \r\n        <div class=\"current-minute-ticker\"");
            BeginWriteAttribute("id", " id=\"", 841, "\"", 872, 3);
            WriteAttributeValue("", 846, "Selection#", 846, 10, true);
#nullable restore
#line 24 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 856, Model.Id, 856, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 865, "#Ticker", 865, 7, true);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 24 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                                                       Write(displayTicker ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral(">\'</div>\r\n    </div>\r\n    <div class=\"betslip-selection-gambler-match-result\">\r\n        <strong>Result:</strong> ");
#nullable restore
#line 27 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                            Write(Model.GamblerMatchResultAliasName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"betslip-selection-home-club-name\">\r\n        ");
#nullable restore
#line 31 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
   Write(Model.HomeClubName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"betslip-selection-home-club-score\"");
            BeginWriteAttribute("id", " id=\"", 1212, "\"", 1250, 3);
            WriteAttributeValue("", 1217, "Selection#", 1217, 10, true);
#nullable restore
#line 33 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 1227, Model.Id, 1227, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1236, "#HomeClubScore", 1236, 14, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n        ");
#nullable restore
#line 34 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
    Write(Model.CurrentMinute.Equals("0") || Model.IsCanceled ? "-" : Model.HomeClubScore);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"betslip-selection-odd\">\r\n        <strong>Odd: </strong>\r\n        <div");
            BeginWriteAttribute("id", " id=\"", 1443, "\"", 1471, 3);
            WriteAttributeValue("", 1448, "Selection#", 1448, 10, true);
#nullable restore
#line 38 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 1458, Model.Id, 1458, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1467, "#Odd", 1467, 4, true);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 38 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                      Write(Model.CurrentMinute.Equals("FT") || Model.IsCanceled ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n            ");
#nullable restore
#line 39 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
       Write(Model.Odd);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div");
            BeginWriteAttribute("id", " id=\"", 1599, "\"", 1634, 3);
            WriteAttributeValue("", 1604, "Selection#", 1604, 10, true);
#nullable restore
#line 41 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 1614, Model.Id, 1614, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1623, "#InitialOdd", 1623, 11, true);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 41 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                             Write(Model.CurrentMinute.Equals("FT") || Model.IsCanceled ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n            ");
#nullable restore
#line 42 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
       Write(Model.InitialOdd);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"betslip-selection-away-club-name\">\r\n        ");
#nullable restore
#line 47 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
   Write(Model.AwayClubName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"betslip-selection-away-club-score\"");
            BeginWriteAttribute("id", " id=\"", 1914, "\"", 1952, 3);
            WriteAttributeValue("", 1919, "Selection#", 1919, 10, true);
#nullable restore
#line 49 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 1929, Model.Id, 1929, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1938, "#AwayClubScore", 1938, 14, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n        ");
#nullable restore
#line 50 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
    Write(Model.CurrentMinute.Equals("0") || Model.IsCanceled ? "-" : Model.AwayClubScore);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"betslip-selection-requirement\">\r\n        <small>\r\n");
#nullable restore
#line 54 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
             if (!Model.RequirementTypeName.Equals("norequirement"))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
            Write(Model.RequirementTypeAliasName + ": " + Model.RequiredValue.ToString("G29"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
            Write(Model.RequirementTypeName.Contains("amount") ? " €" : "");

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                                                           
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </small>\r\n    </div>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21539ad75180877975b7f65aef05bc16bd837b1f15704", async() => {
                WriteLiteral("\r\n        <button type=\"submit\" class=\"btn-secondary btn-sm\">Remove</button>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.PageHandler = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-selectionId", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 64 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                     WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["selectionId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-selectionId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["selectionId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 64 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                                                     WriteLiteral(ViewData["ReturnUrl"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-returnUrl", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    <div class=\"betslip-selection-betable-status\"");
            BeginWriteAttribute("id", " id=\"", 2799, "\"", 2837, 3);
            WriteAttributeValue("", 2804, "Selection#", 2804, 10, true);
#nullable restore
#line 68 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
WriteAttributeValue("", 2814, Model.Id, 2814, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2823, "#BetableStatus", 2823, 14, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 69 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
         if (!Model.IsBetable)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
        Write("Not Betable");

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Shared\_BetslipSelectionPartial.cshtml"
                            
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Selection> Html { get; private set; }
    }
}
#pragma warning restore 1591
