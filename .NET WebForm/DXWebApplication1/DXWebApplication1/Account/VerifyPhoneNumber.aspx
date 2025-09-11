<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="VerifyPhoneNumber.aspx.cs" Inherits="DXWebApplication1.VerifyPhoneNumber" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Enter verification code</h2>
</div>
<p><asp:Literal runat="server" ID="ErrorMessage" /></p>
<dx:ASPxHiddenField runat="server" ID="HiddenField" />
<dx:ASPxTextBox runat="server" ID="Code" TextMode="Phone" Caption="Code">
    <CaptionSettings Position="Top" />
    <ValidationSettings>
        <RequiredField IsRequired="true" ErrorText="The Verification Code is required."/>
    </ValidationSettings>
</dx:ASPxTextBox><br />
<dx:ASPxButton runat="server" ID="btnSubmit" OnClick="Code_Click" Text="Submit" />
 
</asp:content>