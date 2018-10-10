<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Account" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">

</asp:Content> 

<asp:Content ID="Content_Account" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <form id="Account_Info" runat="server">
        <h2 id="Account_Header">
            Your Account
        </h2>

        <div id="divCurrent" runat="server" style="width: 219px">
            <p class="account_page">Username:</p>
            <asp:Label ID="LabelUsername" runat="server"></asp:Label>
            <p class="account_page">Email:</p>
            <asp:Label ID="LabelEmail" runat="server"></asp:Label>
            <p class="account_page">Created:</p>
            <asp:Label ID="LabelCreation" runat="server"></asp:Label>
            <asp:Button ID="ButtonModify" runat="server" OnClick="ButtonModify_Click" Text="Modify Account" />
            <asp:Button ID="ButtonLogout" runat="server" OnClick="ButtonLogout_Click" Text="Logout" Width="91px" />
        </div>          

        <div id = "divChange" runat="server" style="width: 234px">
            <p class="account_page">Current Password:*</p>
            <asp:TextBox ID="tboxCurrentPW" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
            <p class="account_page">New Username:*</p>
            <asp:TextBox ID="tboxUsername" runat="server" Width="180px"></asp:TextBox>
            <p class="account_page">New Email:*</p>
            <asp:TextBox ID="tboxEmail" runat="server" Width="180px"></asp:TextBox>
            <p class="account_page">New Password:</p>
            <asp:TextBox ID="tboxPassword" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Change Information" Width="146px" />
            <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" Width="112px" />
            <asp:Button ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Delete Account" Width="145px" />
            <asp:Label ID="LabelError" runat="server"></asp:Label>
        </div>
    </form>
    
</asp:Content> 
