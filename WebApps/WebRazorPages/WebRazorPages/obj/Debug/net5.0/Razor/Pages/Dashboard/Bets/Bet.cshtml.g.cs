#pragma checksum "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "acda3f71a6f3f6540f10b031f7d31eeda418803f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets.Pages_Dashboard_Bets_Bet), @"mvc.1.0.razor-page", @"/Pages/Dashboard/Bets/Bet.cshtml")]
namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "{handler?}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"acda3f71a6f3f6540f10b031f7d31eeda418803f", @"/Pages/Dashboard/Bets/Bet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d09d2985af4fd647a03c83a8b7bd76aae2be1195", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Dashboard_Bets_Bet : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_UserBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CurrentMinuteTickerScript", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_BettingSignalrHubScript", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_DashboardSideMenu", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Dashboard/Bets/Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Dashboard/Bets/Bet", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "CancelBet", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_BetSelectionPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            DefineSection("UserBar", async() => {
                WriteLiteral("\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "acda3f71a6f3f6540f10b031f7d31eeda418803f6663", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "acda3f71a6f3f6540f10b031f7d31eeda418803f7999", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "acda3f71a6f3f6540f10b031f7d31eeda418803f9179", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n<div class=\"dashboard-side-menu-section\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "acda3f71a6f3f6540f10b031f7d31eeda418803f10468", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n<div class=\"bet-section\">\r\n\r\n    <label class=\"bet-details-label\">\r\n        Bet Details\r\n    </label>\r\n\r\n    <label class=\"bet-selections-label\">\r\n        Bet Selections\r\n    </label>\r\n\r\n    <div class=\"bet-details\">\r\n");
#nullable restore
#line 29 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
         if (Model.Bet != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <label>Bet ID: </label>\r\n            <input");
            BeginWriteAttribute("value", " value=\"", 689, "\"", 710, 1);
#nullable restore
#line 32 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 697, Model.Bet.Id, 697, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly />\r\n");
            WriteLiteral("            <label>Creation Date: </label>\r\n            <input");
            BeginWriteAttribute("value", " value=\"", 789, "\"", 846, 1);
#nullable restore
#line 35 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 797, Model.Bet.DateTimeCreated.ToString("dd/MM/yyyy"), 797, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly />\r\n");
            WriteLiteral("            <label>Status: </label>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 918, "\"", 947, 3);
            WriteAttributeValue("", 923, "Bet#", 923, 4, true);
#nullable restore
#line 38 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 927, Model.Bet.Id, 927, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 940, "#Status", 940, 7, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 948, "\"", 977, 1);
#nullable restore
#line 38 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 956, Model.Bet.StatusName, 956, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly />\r\n");
            WriteLiteral("            <label>Outcome: </label>\r\n            <div>\r\n                <input");
            BeginWriteAttribute("id", " id=\"", 1073, "\"", 1102, 3);
            WriteAttributeValue("", 1078, "Bet#", 1078, 4, true);
#nullable restore
#line 42 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1082, Model.Bet.Id, 1082, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1095, "#Result", 1095, 7, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1103, "\"", 1132, 1);
#nullable restore
#line 42 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1111, Model.Bet.ResultName, 1111, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly\r\n                       ");
#nullable restore
#line 43 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                   Write(Model.Bet.StatusName.Equals("Completed") ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral(" />\r\n                <input");
            BeginWriteAttribute("id", " id=\"", 1253, "\"", 1287, 3);
            WriteAttributeValue("", 1258, "Bet#", 1258, 4, true);
#nullable restore
#line 44 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1262, Model.Bet.Id, 1262, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1275, "#DummyResult", 1275, 12, true);
            EndWriteAttribute();
            WriteLiteral(" value=\"-\" readonly\r\n                       ");
#nullable restore
#line 45 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                   Write(Model.Bet.StatusName.Equals("Completed") ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(" />\r\n            </div>\r\n");
            WriteLiteral("            <label>Selections: </label>\r\n            <input");
            BeginWriteAttribute("value", " value=\"", 1477, "\"", 1513, 1);
#nullable restore
#line 49 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1485, Model.Bet.Selections?.Count, 1485, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly />\r\n");
            WriteLiteral("            <label>Total Odd: </label>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 1588, "\"", 1619, 3);
            WriteAttributeValue("", 1593, "Bet#", 1593, 4, true);
#nullable restore
#line 52 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1597, Model.Bet.Id, 1597, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1610, "#TotalOdd", 1610, 9, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1620, "\"", 1647, 1);
#nullable restore
#line 52 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1628, Model.Bet.TotalOdd, 1628, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly\r\n                   ");
#nullable restore
#line 53 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 1758, "\"", 1796, 3);
            WriteAttributeValue("", 1763, "Bet#", 1763, 4, true);
#nullable restore
#line 54 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1767, Model.Bet.Id, 1767, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1780, "#InitialTotalOdd", 1780, 16, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1797, "\"", 1831, 1);
#nullable restore
#line 54 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1805, Model.Bet.InitialTotalOdd, 1805, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" readonly \r\n                   ");
#nullable restore
#line 55 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n");
            WriteLiteral("            <label>Wagered Amount: </label>\r\n            <input");
            BeginWriteAttribute("value", " value=\"", 1990, "\"", 2024, 2);
#nullable restore
#line 58 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 1998, Model.Bet.WageredAmount, 1998, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2022, "€", 2023, 2, true);
            EndWriteAttribute();
            WriteLiteral(" readonly />\r\n");
            WriteLiteral("            <label>Winnings: </label>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 2098, "\"", 2138, 3);
            WriteAttributeValue("", 2103, "Bet#", 2103, 4, true);
#nullable restore
#line 61 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2107, Model.Bet.Id, 2107, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2120, "#PotentialWinnings", 2120, 18, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2139, "\"", 2177, 2);
#nullable restore
#line 61 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2147, Model.Bet.PotentialWinnings, 2147, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2175, "€", 2176, 2, true);
            EndWriteAttribute();
            WriteLiteral(" readonly \r\n                   ");
#nullable restore
#line 62 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 2289, "\"", 2336, 3);
            WriteAttributeValue("", 2294, "Bet#", 2294, 4, true);
#nullable restore
#line 63 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2298, Model.Bet.Id, 2298, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2311, "#InitialPotentialWinnings", 2311, 25, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2337, "\"", 2382, 2);
#nullable restore
#line 63 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2345, Model.Bet.InitialPotentialWinnings, 2345, 35, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2380, "€", 2381, 2, true);
            EndWriteAttribute();
            WriteLiteral(" readonly \r\n                   ");
#nullable restore
#line 64 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n");
            WriteLiteral("            <label>Profit: </label>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 2533, "\"", 2571, 3);
            WriteAttributeValue("", 2538, "Bet#", 2538, 4, true);
#nullable restore
#line 67 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2542, Model.Bet.Id, 2542, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2555, "#PotentialProfit", 2555, 16, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2572, "\"", 2608, 2);
#nullable restore
#line 67 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2580, Model.Bet.PotentialProfit, 2580, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2606, "€", 2607, 2, true);
            EndWriteAttribute();
            WriteLiteral(" readonly \r\n                   ");
#nullable restore
#line 68 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n            <input");
            BeginWriteAttribute("id", " id=\"", 2720, "\"", 2765, 3);
            WriteAttributeValue("", 2725, "Bet#", 2725, 4, true);
#nullable restore
#line 69 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2729, Model.Bet.Id, 2729, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2742, "#InitialPotentialProfit", 2742, 23, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2766, "\"", 2809, 2);
#nullable restore
#line 69 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 2774, Model.Bet.InitialPotentialProfit, 2774, 33, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2807, "€", 2808, 2, true);
            EndWriteAttribute();
            WriteLiteral(" readonly \r\n                   ");
#nullable restore
#line 70 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
               Write(Model.Bet.StatusName.Equals("Canceled") ? "" : "hidden");

#line default
#line hidden
#nullable disable
            WriteLiteral("/>\r\n");
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acda3f71a6f3f6540f10b031f7d31eeda418803f25997", async() => {
                WriteLiteral("\r\n                <button type=\"submit\" class=\"btn-secondary\">Back</button>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-ReturnPage", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 73 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                                                             WriteLiteral(Model.ReturnPage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["ReturnPage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-ReturnPage", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["ReturnPage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <div class=\"cancel-bet-tile\">\r\n\r\n                <div");
            BeginWriteAttribute("id", " id=\"", 3175, "\"", 3216, 3);
            WriteAttributeValue("", 3180, "Bet#", 3180, 4, true);
#nullable restore
#line 78 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 3184, Model.Bet.Id, 3184, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3197, "#NotCancelableLabel", 3197, 19, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"bet-cancelable-status\"\r\n                     ");
#nullable restore
#line 79 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                 Write(Model.Bet.IsCancelable ? "hidden" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                    Not Cancelable\r\n                </div>\r\n\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "acda3f71a6f3f6540f10b031f7d31eeda418803f29873", async() => {
                WriteLiteral("\r\n                    <button");
                BeginWriteAttribute("id", " id=\"", 3594, "\"", 3629, 3);
                WriteAttributeValue("", 3599, "Bet#", 3599, 4, true);
#nullable restore
#line 85 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
WriteAttributeValue("", 3603, Model.Bet.Id, 3603, 13, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 3616, "#CancelButton", 3616, 13, true);
                EndWriteAttribute();
                WriteLiteral(" type=\"submit\" class=\"btn-danger\"\r\n                            ");
#nullable restore
#line 86 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                        Write(!Model.Bet.IsCancelable ? "disabled hidden" : "");

#line default
#line hidden
#nullable disable
                WriteLiteral(">\r\n                        Cancel\r\n                    </button>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.PageHandler = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 83 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                                                                                    WriteLiteral(Model.Bet.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 84 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                                WriteLiteral(Model.ReturnPage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnPage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-returnPage", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnPage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n            </div>\r\n");
#nullable restore
#line 92 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"centered-content-container\">\r\n                <div class=\"centered-content-item\">\r\n                    Bet not found\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 100 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"bet-selections\">\r\n");
#nullable restore
#line 105 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
         if (Model.Bet != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table>\r\n");
#nullable restore
#line 108 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                 foreach (var selection in Model.Bet.Selections)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "acda3f71a6f3f6540f10b031f7d31eeda418803f36113", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
#nullable restore
#line 112 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = selection;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 115 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n");
#nullable restore
#line 117 "C:\Users\Thomas\Dropbox\CEID\Διπλωματική\BettingApp\WebApps\WebRazorPages\WebRazorPages\Pages\Dashboard\Bets\Bet.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets.BetModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets.BetModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets.BetModel>)PageContext?.ViewData;
        public BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets.BetModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591