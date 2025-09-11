<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Login.aspx.cs" Inherits="DXWebApplication1.Login" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
 <%--   <%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
 --%>
<div class="accountHeader">
    <h2>Log In</h2>
    <p>
        Please enter your username and password. <a href="Register.aspx">Register</a> if you don't have an account.
    </p>
</div>
<dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px" Caption="User Name">
    <CaptionSettings Position="Top" />
    <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorTextPosition="Bottom" Display="Dynamic" ErrorDisplayMode="Text">
        <RequiredField ErrorText="User Name is required." IsRequired="true" />
    </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" Width="200px" Caption="Password">
    <CaptionSettings Position="Top" />
    <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorTextPosition="Bottom" Display="Dynamic" ErrorDisplayMode="Text">
        <RequiredField ErrorText="Password is required." IsRequired="true" />
    </ValidationSettings>
</dx:ASPxTextBox>
<br />
<dx:ASPxButton ID="btnLogin" runat="server" Text="Log In" ValidationGroup="LoginUserValidationGroup"
    OnClick="btnLogin_Click">
</dx:ASPxButton>
 
<%-- Enable this once you have account confirmation enabled for password reset functionality
<br />
<dx:ASPxHyperLink runat="server" NavigateUrl="~/Account/ForgotPassword.aspx" Text="Forgot your password?" />
--%>
<br />
<%--<uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />--%>
 
</asp:Content>