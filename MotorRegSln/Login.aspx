<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MotorRegSln.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container mt-5" style="max-width:400px;">
        <h3 class="text-center mb-4">Login</h3>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

        <div class="form-group mb-3">
            <label>Username</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group mb-3">
            <label>Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />

        <div class="text-center mt-3">
            <a href="Register.aspx">Don't have an account? Register</a>
        </div>
    </div>

</asp:Content>
