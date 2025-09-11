<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenAuthProviders.ascx.cs" Inherits="DXWebApplication1.OpenAuthProviders" %>
<h4>Use another service to log in.</h4>
<asp:ListView runat="server" ID="providerDetails" ItemType="System.String" SelectMethod="GetProviderNames" ViewStateMode="Disabled">
    <ItemTemplate>
        <p>
            <button type="submit" name="provider" value="<%#: Item %>" title="Log in using your <%#: Item %> account.">
                <%#: Item %>
            </button>
        </p>
    </ItemTemplate>
    <EmptyDataTemplate>
        <p>There are no external authentication services configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=252803">this article</a> for details on setting up this ASP.NET application to support logging in via external services.</p>
    </EmptyDataTemplate>
</asp:ListView>