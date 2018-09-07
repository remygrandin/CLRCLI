﻿using System;

namespace CLIForms.Interfaces
{
    interface IAcceptGlobalInput
    {
        bool FireGlobalKeypress(ConsoleKeyInfo key);

        event FocusEventHandler GlobalKeypress;
    }
}
