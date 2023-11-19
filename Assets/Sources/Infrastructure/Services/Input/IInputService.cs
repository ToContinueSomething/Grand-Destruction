using System;
using Sources.Infrastructure.Services;

namespace Sources.Services.Input
{
    public interface IInputService : IService
    {
        event Action Clicked;
    }
}