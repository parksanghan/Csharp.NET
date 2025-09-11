<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="TwoFactorAuthenticationSignIn.aspx.cs" Inherits="DXWebApplication1.TwoFactorAuthenticationSignIn" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Two-Factor Authentication</h2>
</div>
<asp:PlaceHolder runat="server" ID="sendcode">
    <h4>Send verification code</h4>
    <dx:ASPxComboBox Caption="Select Two-Factor Authentication Provider" runat="server" ID="Providers">
        <CaptionSettings Position="Top" />
    </dx:ASPxComboBox><br />
    <dx:ASPxButton runat="server" ID="SelectProvider" Text="Submit" OnClick="ProviderSubmit_Click" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="verifycode" Visible="false">
    <h4>Enter verification code</h4>
    <dx:ASPxHiddenField runat="server" ID="HiddenField"/>
    <dx:ASPxTextBox runat="server" ID="Code" Caption="Code">
        <CaptionSettings Position="Top"/>
        <ValidationSettings>
            <RequiredField IsRequired="true" ErrorText="Code is required"/>
        </ValidationSettings>
    </dx:ASPxTextBox>
    <dx:ASPxCheckBox runat="server" ID="RememberBrowser" Text="Remember Me" /><br />
    <dx:ASPxButton runat="server" ID="CodeSubmit" Text="Submit" OnClick="CodeSubmit_Click" />
</asp:PlaceHolder>
</asp:content>