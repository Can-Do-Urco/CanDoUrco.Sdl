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
using CanDoUrco.Sdl.CustomMarshallers;

namespace CanDoUrco.Sdl;

public static partial class Ffi
{
    // Keep the project's version and these constants up to date!
    public const int SDL_MAJOR_VERSION = 3;
    public const int SDL_MINOR_VERSION = 4;
    public const int SDL_MICRO_VERSION = 10;

    // Macro!
    public static int SDL_VERSIONNUM(int major, int minor, int patch) =>
        major * 1_000_000 + minor * 1_000 + patch;

    // Macro!
    public static int SDL_VERSIONNUM_MAJOR(int version) => version / 1_000_000;

    // Macro!
    public static int SDL_VERSIONNUM_MINOR(int version) => (version % 1_000) % 1_000;

    // Macro!
    public static int SDL_VERSIONNUM_MICRO(int version) => version % 1_000;

    // Macro!
    public static int SDL_VERSION =>
        SDL_VERSIONNUM(SDL_MAJOR_VERSION, SDL_MICRO_VERSION, SDL_MICRO_VERSION);

    // Macro!
    public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z) =>
        SDL_VERSION >= SDL_VERSIONNUM(X, Y, Z);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetVersion();

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string SDL_GetRevision();
}
