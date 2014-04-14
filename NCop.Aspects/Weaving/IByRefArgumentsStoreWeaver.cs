﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NCop.Aspects.Weaving
{
    public interface IByRefArgumentsStoreWeaver
    {
        bool Contains(int argPosition);
        bool ContainsByRefParams { get; }
        void StoreArgsIfNeeded(ILGenerator ilGenerator);
        void RestoreArgsIfNeeded(ILGenerator ilGenerator);
        void EmitLoadLocalAddress(ILGenerator ilGenerator, int argPosition);
    }
}
