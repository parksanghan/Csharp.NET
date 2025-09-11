<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ResetPassword.aspx.cs" Inherits="DXWebApplication1.ResetPassword" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Reset password</h2>
</div>
<p><asp:Literal runat="server" ID="ErrorMessage" /></p>
<h4>Enter your new password</h4>
<dx:ASPxTextBox runat="server" ID="Email" Caption="Email">
    <CaptionSettings Position="Top" />
    <ValidationSettings>
        <RequiredField IsRequired="true" ErrorText="Email is required" />
    </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox runat="server" ID="Password" Caption="Password" Password="true" ClientInstanceName="Password">
    <CaptionSettings Position="Top" />
    <ValidationSettings>
        <RequiredField IsRequired="true" ErrorText="Password is required" />
    </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox runat="server" ID="ConfirmPassword" Caption="Confirm Password" Password="true">
    <CaptionSettings Position="Top" />
    <ValidationSettings>
        <RequiredField ErrorText="Confirm Password is required." IsRequired="true" />
    </ValidationSettings>
    <ClientSideEvents Validation="function(s, e) {
        var originalPasswd = Password.GetText();
        var currentPasswd = s.GetText();
        e.isValid = (originalPasswd  == currentPasswd );
        e.errorText = 'The Password and Confirmation Password must match.';
    }" />
</dx:ASPxTextBox><br />
<dx:ASPxButton runat="server" ID="Reset" Text="Reset" OnClick="Reset_Click" />
</asp:content>