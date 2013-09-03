<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preventivocosta.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="preventivo.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="preventivo.js"></script>
</head>
<body >
    <form id="form1" runat="server">   


       <asp:GridView ID="GridPrezzi" runat="server" AutoGenerateColumns="false"  Visible="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                           <asp:Label ID="Code"   runat="server" Text='<%#databinder.eval(container.dataitem,"Code")%>'></asp:Label>
                                                           <asp:Label ID="Description"   runat="server" Text='<%#databinder.eval(container.dataitem,"Description")%>'></asp:Label>
                                                           <asp:Label ID="Amount"   runat="server" Text='<%#databinder.eval(container.dataitem,"Amount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            </asp:GridView>   
                                            <asp:Label ID="prezzo" Visible="false" runat="server" CssClass="ClPPP" Font-Bold="True"></asp:Label>
    </form>           
</body>
</html>
