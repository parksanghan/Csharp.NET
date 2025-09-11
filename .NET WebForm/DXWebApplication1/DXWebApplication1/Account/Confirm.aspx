<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Confirm.aspx.cs" Inherits="DXWebApplication1.Confirm" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
    <div class="accountHeader">
        <h2>Account Confirmation</h2>
    </div>
    <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
        <p>
            Thank you for confirming your account. Click <dx:ASPxHyperLink ID="login" runat="server" NavigateUrl="~/Account/Login.aspx" Text="here" /> to login
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
        <p>An error has occurred.</p>
    </asp:PlaceHolder>
</asp:content>