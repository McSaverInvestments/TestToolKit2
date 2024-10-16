

namespace TestToolKit2
{
    internal class TestWindow : Window
    {
        #region Static -  Methods
        public static void OpenNewWindow<TWindow>(TWindow aWindow)
             where TWindow : Window
        {
            Application.Current!.OpenWindow(aWindow);
        } 
        #endregion

        public TestWindow()
        {
            Page = new TestPage();
        }
    }
}

