<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormList.aspx.cs" Inherits="DynamicGraphics.WebFormList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="Content/bootstrap.min.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Dynamic graphics</title>
</head>
<body>
    <div class="text-center">
        <h4>
            <b>
                <em>
                    <asp:Label runat="server">Список функциональных зависимостей</asp:Label>
                </em>
            </b>
        </h4>
    </div>
    <form id="form1" runat="server">
    <div>
        <asp:BulletedList ID="BulletedList1" DisplayMode="LinkButton" Font-Size="Medium" OnClick="BulletedList1_Click" runat="server"></asp:BulletedList>
    </div>
    </form>
</body>
</html>
