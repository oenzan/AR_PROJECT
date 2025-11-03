// Assets/Scripts/Core/SelectedContext.cs
using System;

public static class SelectedContext
{
    static IActions _current;
    public static IActions Current
    {
        get => _current;
        set
        {
            if (_current == value) return;
            _current = value;
            OnChanged?.Invoke(_current);
        }
    }
    public static void Clear()
    {
        if (_current == null) return;
        _current = null;
        OnChanged?.Invoke(null);
    }
    public static event Action<IActions> OnChanged;
}
