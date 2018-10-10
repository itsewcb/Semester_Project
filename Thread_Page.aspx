<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Thread_Page.aspx.cs" Inherits="Thread_Page" %>

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
    <form runat ="server"> 
    <div id="Reply_Box" runat="server" visible="False">
        <textarea id="TextArea1" cols="20" rows="2" runat="server" ></textarea>
        <asp:Button ID="Button1" runat="server" Text="Post" Width="92px" OnClick="Button1_Click"/>
        <asp:Button ID="Button2" runat="server" Text="Cancel" Width="92px" OnClick="Button2_Click" />
        <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>     
    </div>

    <div id="return_button" runat="server">
        [<asp:LinkButton Class="Return" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Return</asp:LinkButton>]
    </div>
    <div id="myButton" runat="server">
        [<asp:LinkButton Class="Delete" ID="DeleteButton" runat="server" OnClick="DeleteButton_Click">Delete</asp:LinkButton>]
    </div>

    <div id = "myContent1">  
        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
            <ItemTemplate>
                <div class ="Thread">                 
                    <table>                    
                        <tr>
                            <td id = "Thread_Title"> <span id="Username"><%#Eval("Username") %></span> | <%#Eval("Thread_Title") %> | <%#Eval("Date_Thread") %>
                              [<asp:LinkButton class ="ViewLink" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1">Reply</asp:LinkButton>] 
                            </td>
                        </tr>    
                        <tr>
                            <td><%#Eval("First_Comment") %></td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class ="Thread">                    
                    <table id="Table1">
                        <tr><td id="Thread_Title"> <span id="Username"><%#Eval("Username") %></span> | <%#Eval("Date_Comment") %></td></tr>
                        <tr><td id="Thread_First_Comment"><%#Eval("Comment") %></td></tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div id="Counters" runat="server">
            <asp:Label ID="LabelUnique" runat="server" ToolTip="Unique Posters"></asp:Label> <asp:Label ID="LabelComments" runat="server" ToolTip="Number of Comments"></asp:Label>
        </div>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_myDatabase %>" ProviderName="<%$ ConnectionStrings:ConnectionString_myDatabase.ProviderName %>" SelectCommand="SELECT Forum_User.Username, Thread.Thread_Title, Thread.First_Comment, Thread.Date_Thread FROM (Thread INNER JOIN Forum_User ON Thread.fk_User_ID = Forum_User.pk_User_ID) WHERE (Thread.pk_Thread_ID = ?)">
            <SelectParameters>
                <asp:QueryStringParameter Name="?" QueryStringField="pk_Thread_ID" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_myDatabase %>" ProviderName="<%$ ConnectionStrings:ConnectionString_myDatabase.ProviderName %>" 
            SelectCommand=  "SELECT Thread_Comment.Comment, Forum_User.Username, Thread_Comment.Date_Comment FROM (Forum_User INNER JOIN Thread_Comment ON Forum_User.pk_User_ID = Thread_Comment.fk_User_ID) WHERE (Thread_Comment.fk_Thread_ID = ?) ORDER BY Thread_Comment.pk_Post_ID">
            <SelectParameters>
                <asp:QueryStringParameter Name="?" QueryStringField="pk_Thread_ID" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</asp:Content> 
