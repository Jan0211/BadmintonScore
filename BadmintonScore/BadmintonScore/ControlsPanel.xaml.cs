using System.Diagnostics;

namespace BadmintonScore;

public partial class ControlsPanel : ContentPage
{
    private MainPage _mainPage = null;
    private int _sets1, _sets2, _points1, _points2;

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