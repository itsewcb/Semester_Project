<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">

</asp:Content> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="login_signup" runat="server">
        <div id = "myContent1" style="height: 287px; width: 210px">
            <h2 id="Login_Signup_Header">SignUp</h2>
            <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="tboxEmail" runat="server" style="margin-left: 9px" Width="188px"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Username: " ></asp:Label>
            <asp:TextBox ID="tboxUsername" runat="server" Width="188px" style="margin-left: 9px"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Password: " ></asp:Label>
            <asp:TextBox ID="tboxPassword" runat="server" Width="188px" TextMode="Password" style="margin-left: 9px"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Confirm Password:"></asp:Label>
            <asp:TextBox ID="tboxConfirmation" runat="server" Width="188px" TextMode="Password" style="margin-left: 9px"></asp:TextBox>
            <asp:Button ID="ButtonSignUp" runat="server" Text="SignUp" OnClick="ButtonSignUp_Click" Width="93px" />
            <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Have an account?" Width="147px" />
            <asp:Label ID="LabelError" runat="server"></asp:Label>
        </div>
    </form>
</asp:Content> 
