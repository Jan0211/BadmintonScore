using Microsoft.AspNetCore.Builder;
using System.Net;
using System.Net.Sockets;

namespace BadmintonScore;

public partial class ControlsPanel : ContentPage
{
    private MainPage _mainPage = null;
    private int _sets1, _sets2, _points1, _points2;
    private WebApplication server;
    private bool _isServerRunning;

    public ControlsPanel()
    {
        InitializeComponent();
    }

    public void MainPanelDestroyed()
    {
        _mainPage = null;
        Debug("Destroyed");
    }

    private void Debug(string text)
    {
        debug.Text = text;
    }

    private void StartHttpServer()
    {
        if (_isServerRunning) return;
        var builder = WebApplication.CreateBuilder();
        server = builder.Build();
        server.MapGet("/ChangeDisplay", (string points, string player1, string increase) => ChangeDisplay(Convert.ToBoolean(points), Convert.ToBoolean(player1), Convert.ToBoolean(increase)));
        server.MapGet("/ResetPoints", (string player1) => ResetPoints(Convert.ToBoolean(player1)));
        var host = Dns.GetHostEntry(Dns.GetHostName());
        wirelessDebug.Text = "IP-Adresses:";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                server.Urls.Add("http://" + ip.ToString() + ":3000");
                wirelessDebug.Text = wirelessDebug.Text + "\n" + ip.ToString();
            }
        }
        Task.Run(() =>
        {
            server.RunAsync();
        });
        _isServerRunning = true;
    }

    private void StopHttpServer()
    {
        if (server == null) return;
        if (!_isServerRunning) return;
        server.StopAsync();
        wirelessDebug.Text = "";
        _isServerRunning = false;
    }

    private string ChangeDisplay(bool usePoints, bool isPlayer1, bool increase)
    {
        if(usePoints)
        {
            if(isPlayer1)
            {
                if(increase)
                {
                    _points1++;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount1(_points1.ToString()));
                    return "Increased points from player 1";
                } else
                {
                    _points1--;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount1(_points1.ToString()));
                    return "Decreased points from player 1";
                }
            }else
            {
                if (increase)
                {
                    _points2++;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount2(_points2.ToString()));
                    return "Increased points from player 2";
                }
                else
                {
                    _points2--;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount2(_points2.ToString()));
                    return "Decreased points from player 2";
                }
            }
        } else
        {
            if(isPlayer1)
            {
                if (increase)
                {
                    _sets1++;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetSetCount1(_sets1.ToString()));
                    return "Increased set from player 1";
                }
                else
                {
                    _sets1--;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetSetCount1(_sets1.ToString()));
                    return "Decreased set from player 1";
                }
            } else
            {
                if (increase)
                {
                    _sets2++;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetSetCount2(_sets2.ToString()));
                    return "Increased set from player 2";
                }
                else
                {
                    _sets2--;
                    Application.Current.Dispatcher.Dispatch(() => _mainPage.SetSetCount2(_sets2.ToString()));
                    return "Decreased set from player 2";
                }
            }
        }
    }

    private string ResetPoints(bool player1)
    {
        if (player1)
        {
            _points1 = 0;
            Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount1(_points1.ToString()));
            return "Reset points from player 1";
        }
        _points2 = 0;
        Application.Current.Dispatcher.Dispatch(() => _mainPage.SetPointCount2(_points2.ToString()));
        return "Reset points from player 2";
    }

    private void Button_OnClicked_CreateDisplay(object sender, EventArgs e)
    {
        if (_mainPage != null) return;
        _mainPage = new MainPage(EntryName1.Text, EntryName2.Text);
        var window = new Window(_mainPage);
        window.Destroying += (_, _) => { MainPanelDestroyed(); };
        if (Application.Current != null) Application.Current.OpenWindow(window);
        _sets1 = _sets2 = _points1 = _points2 = 0;
    }

    private void Button_Clicked_SetPlus1(object sender, EventArgs e)
    {
        if (_mainPage == null) return;

        _sets1++;
        _mainPage.SetSetCount1(_sets1.ToString());
    }
    private void Button_Clicked_SetPlus2(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _sets2++;
        _mainPage.SetSetCount2(_sets2.ToString());
    }
    private void Button_Clicked_SetMinus1(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _sets1--;
        _mainPage.SetSetCount1(_sets1.ToString());
    }
    private void Button_Clicked_SetMinus2(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _sets2--;
        _mainPage.SetSetCount2(_sets2.ToString());
    }
    private void Button_Clicked_PointPlus1(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points1++;
        _mainPage.SetPointCount1(_points1.ToString());
    }
    private void Button_Clicked_PointPlus2(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points2++;
        _mainPage.SetPointCount2(_points2.ToString());
    }
    private void Button_Clicked_PointMinus1(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points1--;
        _mainPage.SetPointCount1(_points1.ToString());
    }
    private void Button_Clicked_PointMinus2(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points2--;
        _mainPage.SetPointCount2(_points2.ToString());
    }

    private void Button_Clicked_WirelessStart(object sender, EventArgs e)
    {
        StartHttpServer();
    }

    private void Button_Clicked_WirelessStop(object sender, EventArgs e)
    {
        StopHttpServer();
    }

    private void Button_Clicked_ResetPoints1(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points1 = 0;
        _mainPage.SetPointCount1(_points1.ToString());
    }

    private void Button_Clicked_ResetPoints2(object sender, EventArgs e)
    {
        if (_mainPage == null) return;
        _points2 = 0;
        _mainPage.SetPointCount2(_points2.ToString());
    }

}