<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">

</asp:Content> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="login_signup" runat="server">
        <div id = "myContent1">
            <h2 id ="Login_Signup_Header">Login</h2>
            <asp:Label ID="Label1" runat="server" Text="Username: "></asp:Label>
            <asp:TextBox ID="tboxUsername" runat="server" Width="188px" style="margin-left: 9px"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Password: " Font-Strikeout="False"></asp:Label>
            <asp:TextBox ID="tboxPassword" runat="server" Width="188px" TextMode="Password" style="margin-left: 9px"></asp:TextBox>
            <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" Width="75px" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Dont have account?" Width="164px" />
            <asp:Label ID="LabelError" runat="server"></asp:Label>
        </div>
    </form>
</asp:Content> 
