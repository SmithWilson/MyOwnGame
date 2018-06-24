using System;

namespace MyOwnGame.Core.Services.Navigation
{
    public interface INavigatedToEventArgs
    {
        Uri Uri { get; }

        object Parameter { get; }
    }
}
