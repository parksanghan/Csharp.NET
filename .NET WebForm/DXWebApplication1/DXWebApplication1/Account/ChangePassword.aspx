<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ChangePassword.aspx.cs" Inherits="DXWebApplication1.ChangePassword" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
    <h2 runat="server" ID="PageHeader">Change password</h2>
    <p runat="server" ID="PageDescription">Use the form below to change your password.</p>
    <p style="color:red">
      <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
</div>

<dx:ASPxTextBox ID="tbCurrentPassword" runat="server" Caption="Old Password" Password="true" Width="200px">
    <CaptionSettings Position="Top" />
    <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
          <RequiredField ErrorText="Old Password is required." IsRequired="true" />
      </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Caption="Password" Password="true" runat="server"
      Width="200px">
    <CaptionSettings Position="Top" />
      <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
          <RequiredField ErrorText="Password is required." IsRequired="true" />
      </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbConfirmPassword" Password="true" Caption="Confirm password" runat="server" Width="200px">
    <CaptionSettings Position="Top" />
      <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
          <RequiredField ErrorText="Confirm Password is required." IsRequired="true" />
      </ValidationSettings>
      <ClientSideEvents Validation="function(s, e) {
        var originalPasswd = Password.GetText();
        var currentPasswd = s.GetText();
        e.isValid = (originalPasswd  == currentPasswd );
        e.errorText = 'The Password and Confirmation Password must match.';
    }" />
</dx:ASPxTextBox>
<br />
<dx:ASPxButton ID="btnChangePassword" runat="server" Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup"
    OnClick="btnChangePassword_Click">
</dx:ASPxButton>
<dx:ASPxButton ID="btnSetPassword" runat="server" Text="Set Password" Visible="false" ValidationGroup="ChangeUserPasswordValidationGroup"
    OnClick="btnSetPassword_Click">
</dx:ASPxButton>
</asp:Content>