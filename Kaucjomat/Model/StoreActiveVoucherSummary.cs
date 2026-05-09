using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaucjomat.Model
{
    public partial class StoreActiveVoucherSummary : ObservableObject
    {
        [ObservableProperty]
        private string _storeName;
        [ObservableProperty]
        private int _activeVouchersCount;
    }
}
