<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">
         <div id ="Not_Signed_In" runat="server">
            <asp:HyperLink Href="Login.aspx" ID="HyperLink1" runat="server">Login</asp:HyperLink> | 
            <asp:HyperLink Href="Signup.aspx" ID="HyperLink2" runat="server">Sign Up</asp:HyperLink>
        </div>
        <div id="Is_Signed_In" runat="server">           
            <asp:HyperLink Href="Account.aspx" ID="HyperLink3" runat="server">Account</asp:HyperLink> | 
            <asp:HyperLink Href="Logout.aspx" ID="HyperLink4" runat="server">Logout</asp:HyperLink>
        </div>
</asp:Content> 




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id = "Home_Menu">
        <h2 id="Menu_Title">
            Evans Boards
        </h2>

        <asp:Repeater ID="Repeater_Home" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class ="Thread">                 
                    <table id ="Table_Home">
                        <tr>
                            <td id = "Thread_Title"><asp:HyperLink class ="ViewLink" NavigateUrl='<%#Eval("pk_Board_ID", "Board.aspx?pk_Board_ID={0}") %>' runat="server"><%#Eval("Board_Name")%></asp:HyperLink></td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_myDatabase %>" ProviderName="<%$ ConnectionStrings:ConnectionString_myDatabase.ProviderName %>" SelectCommand="SELECT * FROM [Board]"></asp:SqlDataSource>
</asp:Content>
