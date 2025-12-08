<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LinkVehicle.aspx.cs" Inherits="MotorRegSln.LinkVehicle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Link a Vehicle</h3>

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

    <div class="form-group mb-3">
        <label>Plate Number or Chassis Number</label>
        <asp:TextBox ID="txtIdentifier" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <asp:Button ID="btnSearch" runat="server" Text="Link Vehicle" CssClass="btn btn-primary" OnClick="btnSearch_Click" />

</asp:Content>
