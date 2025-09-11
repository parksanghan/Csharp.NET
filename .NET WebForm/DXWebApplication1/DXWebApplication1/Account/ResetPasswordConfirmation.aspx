<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="DXWebApplication1.ResetPasswordConfirmation" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Password Changed</h2>
</div>
<p>Your password has been changed. Click <dx:ASPxHyperLink ID="login" runat="server" NavigateUrl="~/Account/Login.aspx" Text="here" /> to login </p>
</asp:content>