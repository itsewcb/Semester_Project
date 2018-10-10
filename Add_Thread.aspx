<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add_Thread.aspx.cs" Inherits="Add_Thread" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">
</asp:Content> 


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="Thread_Start" runat="server" style="width: 241px">
        <asp:Label ID="LabelTitle" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <p id="Seperator"></p>
        <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label>
        <asp:TextBox ID="tboxTitle" runat="server" Width="237px"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="First Comment"></asp:Label>
        <textarea id="TextArea1" rows="2" runat="server" style="width: 238px" ></textarea>
        <asp:Button ID="Button1" runat="server" Text="Start Thread" OnClick="Button1_Click" Width="242px" />
        <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
    </form>
</asp:Content> 
