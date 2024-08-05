﻿namespace Domain
{
    public enum LogLevel
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
        Aktif = 1,
        Pasif,
        None
    }
}
