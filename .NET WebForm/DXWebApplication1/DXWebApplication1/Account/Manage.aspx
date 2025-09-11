<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Manage.aspx.cs" Inherits="DXWebApplication1.Manage" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<p><%: SuccessMessage %></p>
<div class="accountHeader">
    <h2>Change your account settings</h2>
</div>
<ul>
    <li>
        Password:
 
        <dx:ASPxHyperLink NavigateUrl="/Account/ChangePassword.aspx" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
        <dx:ASPxHyperLink NavigateUrl="/Account/ChangePassword.aspx" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
 
    </li>
    <li>
        External Logins: <%: LoginsCount %> 
 
        <dx:ASPxHyperLink NavigateUrl="/Account/ManageLogins.aspx" Text="[Manage]" runat="server" />
 
    </li>
    <%-- 
        Phone Numbers can used as a second factor of verification in a two-factor authentication system.
        See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
        for details on setting up this ASP.NET application to support two-factor authentication using SMS.
        Uncomment the following blocks after you have set up two-factor authentication
    --%> 
    <%-- 
    <li>
        Phone Number:
 
    <% if (HasPhoneNumber) { %> 
        <dx:ASPxHyperLink NavigateUrl="/Account/AddPhoneNumber.aspx" runat="server" Text="[Add]" />
    <% } else { %> 
        <dx:ASPxLabel Text="" ID="PhoneNumber" runat="server" />
        <dx:ASPxHyperLink NavigateUrl="/Account/AddPhoneNumber.aspx" runat="server" Text="[Change]" /> &nbsp;|&nbsp;
        <dx:ASPxButton runat="server" ID="RemovePhone" RenderMode="Link" Text="[Remove]" OnClick="RemovePhone_Click" />
    <% } %> 
 
    </li> 
    --%> 
    <li>
        Two-Factor Authentication:
        There are no two-factor authentication providers configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
        for details on setting up this ASP.NET application to support two-factor authentication.
 
        <% if (TwoFactorEnabled) { %> 
        <%--
        Enabled
        <dx:ASPxButton runat="server" id="TwoFactorDisable" RenderMode="Link" Text="[Disable]" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
        --%>
        <% } else { %>          
        <%--
        Disabled
        <dx:ASPxButton runat="server" id="TwoFactorEnable" RenderMode="Link" Text="[Enable]" CommandArgument="true" OnClick="TwoFactorEnable_Click" />
        --%>
        <% } %> 
    </li>
</ul>
</asp:content>