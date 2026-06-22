// The MIT License (MIT)
//
// Copyright (C) 2026 Victor Matia (vitimiti)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
// the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl;

public static partial class Ffi
{
    public readonly record struct SDL_BlendMode(uint Value)
    {
        public static implicit operator uint(SDL_BlendMode blendMode) => blendMode.Value;

        public static implicit operator SDL_BlendMode(uint value) => new(value);
    }

    public static SDL_BlendMode SDL_BLENDMODE_NONE => new(0x0000_0000U);
    public static SDL_BlendMode SDL_BLENDMODE_BLEND => new(0x0000_0001U);
    public static SDL_BlendMode SDL_BLENDMODE_BLEND_PREMULTIPLIED => new(0x0000_0010U);
    public static SDL_BlendMode SDL_BLENDMODE_ADD => new(0x0000_0002U);
    public static SDL_BlendMode SDL_BLENDMODE_ADD_PREMULTIPLIED => new(0x0000_0020U);
    public static SDL_BlendMode SDL_BLENDMODE_MOD => new(0x0000_0004U);
    public static SDL_BlendMode SDL_BLENDMODE_MUL => new(0x0000_0008U);
    public static SDL_BlendMode SDL_BLENDMODE_INVALID => new(0x7FFF_FFFFU);

    public readonly record struct SDL_BlendOperation(int Value)
    {
        public static implicit operator int(SDL_BlendOperation blendOperation) =>
            blendOperation.Value;

        public static implicit operator SDL_BlendOperation(int value) => new(value);
    }

    public static SDL_BlendOperation SDL_BLENDOPERATION_ADD => new(0x0000_0001);
    public static SDL_BlendOperation SDL_BLENDOPERATION_SUBTRACT => new(0x0000_0002);
    public static SDL_BlendOperation SDL_BLENDOPERATION_REV_SUBTRACT => new(0x0000_0003);
    public static SDL_BlendOperation SDL_BLENDOPERATION_MINIMUM => new(0x0000_0004);
    public static SDL_BlendOperation SDL_BLENDOPERATION_MAXIMUM => new(0x0000_0005);

    public readonly record struct SDL_BlendFactor(int Value)
    {
        public static implicit operator int(SDL_BlendFactor blendFactor) => blendFactor.Value;

        public static implicit operator SDL_BlendFactor(int value) => new(value);
    }

    public static SDL_BlendFactor SDL_BLENDFACTOR_ZERO => new(0x0000_0001);
    public static SDL_BlendFactor SDL_BLENDFACTOR_ONE => new(0x0000_0002);
    public static SDL_BlendFactor SDL_BLENDFACTOR_SRC_COLOR => new(0x0000_0003);
    public static SDL_BlendFactor SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR => new(0x0000_0004);
    public static SDL_BlendFactor SDL_BLENDFACTOR_SRC_ALPHA => new(0x0000_0005);
    public static SDL_BlendFactor SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA => new(0x0000_0006);
    public static SDL_BlendFactor SDL_BLENDFACTOR_DST_COLOR => new(0x0000_0007);
    public static SDL_BlendFactor SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR => new(0x0000_0008);
    public static SDL_BlendFactor SDL_BLENDFACTOR_DST_ALPHA => new(0x0000_0009);
    public static SDL_BlendFactor SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA => new(0x0000_000A);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_BlendMode SDL_ComposeCustomBlendMode(
        SDL_BlendFactor srcColorFactor,
        SDL_BlendFactor dstColorFactor,
        SDL_BlendOperation colorOperation,
        SDL_BlendFactor srcAlphaFactor,
        SDL_BlendFactor dstAlphaFactor,
        SDL_BlendOperation alphaOperation
    );
}
