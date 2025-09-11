<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Register.aspx.cs" Inherits="DXWebApplication1.Register" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
    <div class="accountHeader">
    <h2>Create a New Account</h2>
    <p>Use the form below to create a new account.</p>
    <p style="color:red">
      <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
</div>
<dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px" Caption="User Name">
  <CaptionSettings Position="Top" />
  <ValidationSettings ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
    <RequiredField ErrorText="User Name is required." IsRequired="true" />
  </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbEmail" runat="server" Width="200px" Caption="E-mail">
  <CaptionSettings Position="Top" />
  <ValidationSettings ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
    <RequiredField ErrorText="E-mail is required." IsRequired="true" />
    <RegularExpression ErrorText="Email validation failed" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
  </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server" Width="200px" Caption="Password">
  <CaptionSettings Position="Top" />
  <ValidationSettings ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
    <RequiredField ErrorText="Password is required." IsRequired="true" />
  </ValidationSettings>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px" Caption="Confirm password">
  <CaptionSettings Position="Top" />
  <ValidationSettings ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text">
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
<dx:ASPxButton ID="btnCreateUser" runat="server" Text="Create User" ValidationGroup="RegisterUserValidationGroup"
    OnClick="btnCreateUser_Click">
</dx:ASPxButton>
</asp:content>