using EmptyMVXProjCore.Model;
using EmptyMVXProjCore.ViewModels;
using MvvmCross.Wpf.Views;

namespace EmptyMVXProj.Views
{
    public partial class FirstView : MvxWpfView
    {
        private FirstViewModel VM => this.DataContext as FirstViewModel;

        public FirstView()
        {
            this.InitializeComponent();

        }

        private void CollapseAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TreeView.CollapseAll();
        }
    }
}
