<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MotorRegSln.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-center align-items-start" style="min-height:80vh;">
    <div class="p-4 shadow rounded"
         style="width:100%; max-width:500px; margin-top:60px; background:white; border-top:3px solid #ff3b00;">

        <h3 class="text-center mb-4">Create Account</h3>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>

        <!-- FULL NAME -->
        <label class="fw-bold">FULL NAME</label>
        <asp:TextBox ID="txtFullName" runat="server"
                     CssClass="form-control mb-3 login-input" />

        <!-- USERNAME -->
        <label class="fw-bold">USERNAME</label>
        <asp:TextBox ID="txtUsername" runat="server"
                     CssClass="form-control mb-3 login-input" />

        <!-- PASSWORD -->
        <label class="fw-bold">PASSWORD</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                     CssClass="form-control mb-4 login-input" />

        <!-- REGISTER BUTTON -->
        <asp:Button ID="btnRegister" runat="server" Text="CREATE ACCOUNT"
            CssClass="login-btn fw-bold text-white"
            OnClick="btnRegister_Click" />

        <div class="text-center mt-3">
            <a href="Login.aspx">Already have an account? Login</a>
        </div>

    </div>
</div>

<style>
    .login-input {
        width: 100% !important;
        height: 45px !important;
        font-size: 16px !important;
        max-width: 370px !important;
        margin-left: auto !important;
        margin-right: auto !important;
        display: block !important;
    }

    .login-btn {
        background: #ff3b00 !important;
        border: none !important;
        padding: 12px !important;
        font-size: 16px !important;
        width: 100% !important;
        max-width: 370px !important;
        margin-left: auto !important;
        margin-right: auto !important;
        display: block !important;
    }
</style>


</asp:Content>
