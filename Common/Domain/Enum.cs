﻿namespace Domain
{
    public enum LogUserLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical,
        None
    }

    public enum Operation
    {
        Login = 1,
        Register,
        None
    }
    public enum State
    {
        Pasif = 0,
        Aktif,
        None
    }
}
