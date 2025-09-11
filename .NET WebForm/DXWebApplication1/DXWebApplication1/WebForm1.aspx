<%@ Page Title="Products" Language="C#" MasterPageFile="~/Main.master"
    AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="DXWebApplication1.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <dx:ASPxGridView ID="gvProducts" runat="server" ClientInstanceName="ASPxGridView1"
      KeyFieldName="ProductId" Width="100%" AutoGenerateColumns="False"
      OnRowInserting="gvProducts_RowInserting"
      OnRowUpdating="gvProducts_RowUpdating"
      OnRowDeleting="gvProducts_RowDeleting"
      OnInitNewRow="gvProducts_InitNewRow">

    <!-- 편집 UX -->
    <SettingsEditing Mode="PopupEditForm" PopupEditForm-Modal="true" />
    <SettingsPager PageSize="20" />
    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="500" />
    <Paddings Padding="0px" />
    <Border BorderWidth="0px" />

    <!-- 편집/삭제/추가 버튼 -->
    <Columns>
      <dx:GridViewCommandColumn ShowNewButtonInHeader="true"
                                ShowEditButton="true"
                                ShowDeleteButton="true" />

      <dx:GridViewDataTextColumn FieldName="ProductId" Caption="ID" ReadOnly="true" />
      <dx:GridViewDataTextColumn FieldName="ProductName" Caption="제품명" />
      <dx:GridViewDataSpinEditColumn FieldName="ProductPrice" Caption="가격">
        <PropertiesSpinEdit NumberType="Integer" MinValue="0" />
      </dx:GridViewDataSpinEditColumn>

      <!-- FK 콤보박스 컬럼 -->
      <dx:GridViewDataComboBoxColumn Name="colCategory" FieldName="ProductCategoryId" Caption="분류">
        <PropertiesComboBox ValueType="System.Int32" ValueField="CategoryId" TextField="CategoryName" />
      </dx:GridViewDataComboBoxColumn>

      <dx:GridViewDataComboBoxColumn Name="colMaker" FieldName="ProductManufacturerId" Caption="제조사">
        <PropertiesComboBox ValueType="System.Int32" ValueField="ManufacturerId" TextField="ManufacturerName" />
      </dx:GridViewDataComboBoxColumn>
    </Columns>
  </dx:ASPxGridView>
</asp:Content>
