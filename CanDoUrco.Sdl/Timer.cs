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
using unsafe SDL_TimerCallback = delegate* unmanaged[Cdecl]<void*, CanDoUrco.Sdl.Ffi.SDL_TimerID, uint, uint>;
using unsafe SDL_NSTimerCallback = delegate* unmanaged[Cdecl]<void*, CanDoUrco.Sdl.Ffi.SDL_TimerID, ulong, ulong>;

namespace CanDoUrco.Sdl;

public static unsafe partial class Ffi
{
    public const uint SDL_MS_PER_SECOND = 1_000;
    public const uint SDL_US_PER_SECOND = 1_000_000;
    public const long SDL_NS_PER_SECOND = 1_000_000_000L;
    public const uint SDL_NS_PER_MS = 1_000_000;
    public const uint SDL_NS_PER_US = 1_000;
    
    // Macro!
    public static ulong SDL_SECONDS_TO_NS(uint S) =>(ulong)S * SDL_NS_PER_SECOND;
    
    // Macro!
    public static ulong SDL_NS_TO_SECONDS(ulong NS) => NS / SDL_NS_PER_SECOND;
    
    // Macro!
    public static ulong SDL_MS_TO_NS(uint MS) => (ulong)MS * SDL_NS_PER_MS;
    
    // Macro!
    public static ulong SDL_NS_TO_MS(ulong NS) => NS / SDL_NS_PER_MS;
    
    // Macro!
    public static ulong SDL_US_TO_NS(ulong US) => US * SDL_NS_PER_US;
    
    // Macro!
    public static ulong SDL_NS_TO_US(ulong NS) => NS / SDL_NS_PER_US;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetTicks();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetTicksNS();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetPerformanceCounter();
    
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetPerformanceFrequency();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_Delay(uint ms);
    
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DelayNS(ulong ns);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DelayPrecise(ulong ns);

    public readonly record struct SDL_TimerID(uint Value)
    {
        public static implicit operator uint(SDL_TimerID id) => id.Value;
        
        public static implicit operator SDL_TimerID(uint value) => new(value);
    }

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TimerID SDL_AddTimer(uint interval, SDL_TimerCallback callback, void* userdata);
    
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TimerID SDL_AddTimerNS(ulong interval, SDL_NSTimerCallback callback, void* userdata);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_RemoveTimer(SDL_TimerID id);
}