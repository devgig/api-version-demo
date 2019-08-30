using System;

namespace Demo.Ui.Core
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
