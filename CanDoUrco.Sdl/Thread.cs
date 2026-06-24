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
using unsafe SDL_FunctionPointer = void*;
using unsafe SDL_ThreadFunction = delegate* unmanaged[Cdecl]<void*, int>;
using unsafe SDL_TLSDestructorCallback = delegate* unmanaged[Cdecl]<void*, void>;

namespace CanDoUrco.Sdl;

public static unsafe partial class Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Thread;

    public readonly record struct SDL_ThreadID(ulong Value)
    {
        public static implicit operator ulong(SDL_ThreadID id) => id.Value;

        public static implicit operator SDL_ThreadID(ulong id) => new(id);
    }

    // Must be identical to SDL_AtomicInt!
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TLSID
    {
        public int value;
    }

    public readonly record struct SDL_ThreadPriority(int Value)
    {
        public static implicit operator SDL_ThreadPriority(int value) => new(value);

        public static implicit operator int(SDL_ThreadPriority priority) => priority.Value;
    }

    public static SDL_ThreadPriority SDL_THREAD_PRIORITY_LOW => new(0);
    public static SDL_ThreadPriority SDL_THREAD_PRIORITY_NORMAL => new(1);
    public static SDL_ThreadPriority SDL_THREAD_PRIORITY_HIGH => new(2);
    public static SDL_ThreadPriority SDL_THREAD_PRIORITY_TIME_CRITICAL => new(3);

    public readonly record struct SDL_ThreadState(int Value)
    {
        public static implicit operator SDL_ThreadState(int value) => new(value);

        public static implicit operator int(SDL_ThreadState state) => state.Value;
    }

    public static SDL_ThreadState SDL_THREAD_UNKNOWN => new(0);
    public static SDL_ThreadState SDL_THREAD_ALIVE => new(1);
    public static SDL_ThreadState SDL_THREAD_DETACHED => new(2);
    public static SDL_ThreadState SDL_THREAD_COMPLETE => new(3);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Thread* SDL_CreateThread(
        SDL_ThreadFunction fn,
        string name,
        void* data
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Thread* SDL_CreateThreadWithProperties(SDL_PropertiesID props);

    public const string SDL_PROP_THREAD_CREATE_ENTRY_FUNCTION_POINTER =
        "SDL.thread.create.entry_function";

    public const string SDL_PROP_THREAD_CREATE_NAME_STRING = "SDL.thread.create.name";
    public const string SDL_PROP_THREAD_CREATE_USERDATA_POINTER = "SDL.thread.create.userdata";
    public const string SDL_PROP_THREAD_CREATE_STACKSIZE_NUMBER = "SDL.thread.create.stacksize";

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Thread* SDL_CreateThreadRuntime(
        SDL_ThreadFunction fn,
        string name,
        void* data,
        SDL_FunctionPointer pfnBeginThread,
        SDL_FunctionPointer pfnEndThread
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Thread* SDL_CreateThreadWithPropertiesRuntime(
        SDL_PropertiesID props,
        SDL_FunctionPointer pfnBeginThread,
        SDL_FunctionPointer pfnEndThread
    );

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetThreadName(SDL_Thread* thread);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_ThreadID SDL_GetCurrentThreadID();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_ThreadID SDL_GetThreadID(SDL_Thread* thread);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetCurrentThreadPriority(SDL_ThreadPriority priority);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_WaitThread(SDL_Thread* thread, out int status);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_ThreadState SDL_GetThreadState(SDL_Thread* thread);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DetachThread(SDL_Thread* thread);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetTLS(ref SDL_TLSID id);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetTLS(
        ref SDL_TLSID id,
        void* value,
        SDL_TLSDestructorCallback destructor
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CleanupTLS();
}
