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

public static unsafe partial class Ffi
{
    public readonly record struct SDL_SpinLock(int Value)
    {
        public static implicit operator int(SDL_SpinLock spinLock) => spinLock.Value;

        public static implicit operator SDL_SpinLock(int value) => new SDL_SpinLock(value);
    }

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TryLockSpinlock(out SDL_SpinLock @lock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LockSpinlock(ref SDL_SpinLock @lock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockSpinlock(ref SDL_SpinLock @lock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_MemoryBarrierReleaseFunction();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_MemoryBarrierAcquireFunction();

    // Macro!
    public static void SDL_MemoryBarrierRelease() => SDL_MemoryBarrierReleaseFunction();

    // Macro!
    public static void SDL_MemoryBarrierAcquire() => SDL_MemoryBarrierAcquireFunction();

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AtomicInt
    {
        public int value;
    }

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CompareAndSwapAtomicIn(
        ref SDL_AtomicInt a,
        int oldval,
        int newval
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_SetAtomicInt(ref SDL_AtomicInt a, int v);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAtomicInt(ref SDL_AtomicInt a);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddAtomicInt(ref SDL_AtomicInt a, int v);

    // Macro!
    public static int SDL_AtomicIncRef(ref SDL_AtomicInt a) => SDL_AddAtomicInt(ref a, 1);

    // Macro!
    public static bool SDL_AtomicDecRef(ref SDL_AtomicInt a) => SDL_AddAtomicInt(ref a, -1) == 1;

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AtomicU32
    {
        public uint value;
    }

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CompareAndSwapAtomicU32(
        ref SDL_AtomicU32 a,
        uint oldval,
        uint newval
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_SetAtomicU32(ref SDL_AtomicU32 a, uint v);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GetAtomicU32(ref SDL_AtomicU32 a);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_AddAtomicU32(ref SDL_AtomicU32 a, uint v);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CompareAndSwapAtomicPointer(
        ref void* a,
        void* oldval,
        void* newval
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_SetAtomicPointer(ref void* a, void* v);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetAtomicPointer(ref void* a);
}
