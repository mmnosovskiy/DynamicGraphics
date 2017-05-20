<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DynamicGraphics.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Dynamic graphics</title>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />



</head>
<body>
    <div class="text-center">
        <h4>
            <b>
                <em>
                    <asp:Literal ID="TitleLit" runat="server"></asp:Literal>
                </em>
            </b>
        </h4>
    </div>
    <asp:Literal ID="NewScript" runat="server"></asp:Literal>

    <script type="text/javascript">

        var validError0 = false;
        var validError1 = false;
        var validError2 = false;
        var validError3 = false;


        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function createArr() {
            var k = 0;
            var arr = [];
            var step = (xmax - xmin) / 100;
            step = Math.round(step * 1000000) / 1000000;
            for (var i = 0, j = xmin; i < 100; i++) {
                j = Math.round(j * 1000000) / 1000000;
                arr[i] = [j, f(j)];
                j += step;
            }
            arr[i] = [xmax, f(xmax)];
            return arr;
        }

        function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('number', 'X');
            data.addColumn('number', yax);

            data.addRows(createArr());

            var options = {
                height: 450,
                width: 1250,
                tooltip: 'none',
                chartArea:
                {
                    backgroundColor:
                    {
                        strokeWidth: 2,
                        stroke: 'black'
                    },
                    top: 10
                },
                legend: 'none',
                hAxis: {
                    gridlines: {
                        count: -1
                    }
                },
                vAxis: {
                    gridlines: {
                        count: -1
                    }
                }
            };


            var chart = new google.visualization.LineChart(document.getElementById('graph_div'));

            chart.draw(data, options);
        }

    </script>
    <form id="form1" onsubmit="return false;" runat="server">
        <div>
            <div id="graph_div"></div>
            <div id="sliders_div">
                <div class="col-lg-3 col-lg-offset-1">
                    <asp:Label runat="server" ID="Label0" Visible="false" Enabled="false">Param</asp:Label>
                    <asp:TextBox runat="server" onchange="validation0();" autocomplete="off" ID="TextBox0" Visible="false" Enabled="false" Width="150"></asp:TextBox>
                    <div id="slider0" style="width: 175px; margin: 10px"></div>
                </div>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="Label1" Visible="false" Enabled="false">Param</asp:Label>
                    <asp:TextBox runat="server" onchange="validation1();" autocomplete="off" ID="TextBox1" Visible="false" Enabled="false" Width="150"></asp:TextBox>
                    <div id="slider1" style="width: 175px; margin: 10px"></div>
                </div>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="Label2" Visible="false" Enabled="false">Param</asp:Label>
                    <asp:TextBox runat="server" onchange="validation2();" autocomplete="off" ID="TextBox2" Visible="false" Enabled="false" Width="150"></asp:TextBox>
                    <div id="slider2" style="width: 175px; margin: 10px"></div>
                </div>
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="Label3" Visible="false" Enabled="false">Param</asp:Label>
                    <asp:TextBox runat="server" onchange="validation3();" autocomplete="off" ID="TextBox3" Visible="false" Enabled="false" Width="150"></asp:TextBox>
                    <div id="slider3" style="width: 175px; margin: 10px"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
