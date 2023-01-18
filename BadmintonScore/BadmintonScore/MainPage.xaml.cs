namespace BadmintonScore;

public partial class MainPage : ContentPage
{

    public MainPage(string name1, string name2)
	{
        InitializeComponent();
        SetName1(name1);
        SetName2(name2);
    }

    private void SetName1(string name) => LabelName1.Text = name;
    private void SetName2(string name) => LabelName2.Text = name;
    public void SetSetCount1(string count) => SetCount1.Text = count;
    public void SetSetCount2(string count) => SetCount2.Text = count;
    public void SetPointCount1(string count) => PointCount1.Text = count;
    public void SetPointCount2(string count) => PointCount2.Text = count;
}

