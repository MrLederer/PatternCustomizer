using System;
using Microsoft.VisualStudio.TextManager.Interop;
using RegexCustomize.State;

namespace RegexCustomize
{
    internal class CustomizeCommand2
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("b0dff607-6b0c-488a-a4fe-398201c955de");

        private IVsTextManager textManager;
        private IState m_settingState;

        public CustomizeCommand2(IVsTextManager textManager, IState settingState)
        {
            this.textManager = textManager;
            this.m_settingState = settingState;
        }
    }
}