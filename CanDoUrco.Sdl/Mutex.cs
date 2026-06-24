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
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Mutex;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Mutex* SDL_CreateMutex();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LockMutex(SDL_Mutex* mutex);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TryLockMutex(SDL_Mutex* mutex);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockMutex(SDL_Mutex* mutex);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyMutex(SDL_Mutex* mutex);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_RWLock;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_RWLock* SDL_CreateRWLock();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LockRWLockForReading(SDL_RWLock* rwlock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LockRWLockForWriting(SDL_RWLock* rwlock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TryLockRWLockForReading(SDL_RWLock* rwlock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TryLockRWLockForWriting(SDL_RWLock* rwlock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockRWLock(SDL_RWLock* rwlock);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyRWLock(SDL_RWLock* rwlock);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Semaphore;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Semaphore* SDL_CreateSemaphore(uint initial_value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroySemaphore(SDL_Semaphore* sem);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_WaitSemaphore(SDL_Semaphore* sem);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TryWaitSemaphore(SDL_Semaphore* sem);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SemaphoreWaitTimeout(SDL_Semaphore* sem, int timeoutMS);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SignalSemaphore(SDL_Semaphore* sem);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GetSemaphoreValue(SDL_Semaphore* sem);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Condition;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Condition* SDL_CreateCondition();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyCondition(SDL_Condition* cond);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SignalCondition(SDL_Condition* cond);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BroadcastCondition(SDL_Condition* cond);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_WaitCondition(SDL_Condition* cond, SDL_Mutex* mutex);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ConditionWaitTimeout(
        SDL_Condition* cond,
        SDL_Mutex* mutex,
        int timeoutMS
    );

    public readonly record struct SDL_InitStatus(int Value)
    {
        public static implicit operator SDL_InitStatus(int value) => new(value);

        public static implicit operator int(SDL_InitStatus value) => value.Value;
    }

    public static SDL_InitStatus SDL_INIT_STATUS_UNINITIALIZED => new(0);
    public static SDL_InitStatus SDL_INIT_STATUS_INITIALIZING => new(1);
    public static SDL_InitStatus SDL_INIT_STATUS_INITIALIZED => new(2);
    public static SDL_InitStatus SDL_INIT_STATUS_UNINITIALIZING => new(3);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_InitState
    {
        public SDL_AtomicInt status;
        public SDL_ThreadID thread;
        public void* reserved;
    }

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ShouldInit(ref SDL_InitState state);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ShouldQuit(ref SDL_InitState state);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetInitialized(
        ref SDL_InitState state,
        [MarshalAs(UnmanagedType.I1)] bool initialized
    );
}
