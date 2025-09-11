<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ManageLogins.aspx.cs" Inherits="DXWebApplication1.ManageLogins" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<div class="accountHeader">
    <h2>Manage your external logins</h2>
</div>
<asp:ListView runat="server" ItemType="Microsoft.AspNet.Identity.UserLoginInfo"
    SelectMethod="GetLogins" DeleteMethod="RemoveLogin" DataKeyNames="LoginProvider,ProviderKey">
    <LayoutTemplate>
        <h4>Registered Logins</h4>
        <table>
            <tbody>
                <tr runat="server" id="itemPlaceholder"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td><%#: Item.LoginProvider %></td>
            <td>
                <asp:Button runat="server" Text="Remove" CommandName="Delete" CausesValidation="false"
                    ToolTip='<%# "Remove this " + Item.LoginProvider + " login from your account" %>'
                    Visible="<%# CanRemoveExternalLogins %>" />
            </td>
        </tr>
    </ItemTemplate>
</asp:ListView>
<uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
</asp:content>