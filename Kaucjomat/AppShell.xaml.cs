using Kaucjomat.View;
using Kaucjomat.ViewModel;

namespace Kaucjomat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddNewView), typeof(AddNewView));
        }
    }
}
