<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="DXWebApplication1.RegisterExternalLogin" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Associate your <%: ProviderName %> account</h2>
</div>
<asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
<p>
    You've authenticated with <strong><%: ProviderName %></strong>. Please enter an email below for the current site
    and click the Log in button.
</p>
    <dx:ASPxTextBox runat="server" ID="email" Caption="Email">
        <CaptionSettings Position="Top" />
        <ValidationSettings>
            <RequiredField IsRequired="true" ErrorText="Email is required" />
        </ValidationSettings>
    </dx:ASPxTextBox><br />
    <dx:ASPxButton runat="server" ID="LogIn" Text="Log in" OnClick="LogIn_Click" />
</asp:content>