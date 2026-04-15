using System;

public static class GameEvents
{
    public static Action<string> OnShowMessage;

    public static void ShowMessage(string message)
    {
        OnShowMessage?.Invoke(message);
    }
}