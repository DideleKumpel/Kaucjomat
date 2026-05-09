using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaucjomat.Messages
{
    public class BarcodeScannedMessage : ValueChangedMessage<string>
    {
        public BarcodeScannedMessage(string value) : base(value) { }
    }
}
