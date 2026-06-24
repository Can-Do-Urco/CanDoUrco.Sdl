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
    public struct SDL_AsyncIO;

    public readonly record struct SDL_AsyncIOTaskType(int Value);

    public static SDL_AsyncIOTaskType SDL_ASYNCIO_TASK_READ => new(0);
    public static SDL_AsyncIOTaskType SDL_ASYNCIO_TASK_WRITE => new(1);
    public static SDL_AsyncIOTaskType SDL_ASYNCIO_TASK_CLOSE => new(2);

    public readonly record struct SDL_AsyncIOResult(int Value);

    public static SDL_AsyncIOResult SDL_ASYNCIO_COMPLETE => new(0);
    public static SDL_AsyncIOResult SDL_ASYNCIO_FAILURE => new(1);
    public static SDL_AsyncIOResult SDL_ASYNCIO_CANCELED => new(2);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AsyncIOOutcome
    {
        public SDL_AsyncIO* asyncio;
        public SDL_AsyncIOTaskType type;
        public SDL_AsyncIOResult result;
        public void* buffer;
        public ulong offset;
        public ulong bytes_requested;
        public ulong bytes_transferred;
        public void* userdata;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AsyncIOQueue;

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AsyncIO* SDL_AsyncIOFromFile(string file, string mode);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetAsyncIOSize(SDL_AsyncIO* asyncio);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadAsyncIO(
        SDL_AsyncIO* asyncio,
        void* ptr,
        ulong offset,
        ulong size,
        SDL_AsyncIOQueue* queue,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteAsyncIO(
        SDL_AsyncIO* asyncio,
        void* ptr,
        ulong offset,
        ulong size,
        SDL_AsyncIOQueue* queue,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CloseAsyncIO(
        SDL_AsyncIO* asyncIo,
        [MarshalAs(UnmanagedType.I1)] bool flush,
        SDL_AsyncIOQueue* queue,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AsyncIOQueue* SDL_CreateAsyncIOQueue();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyAsyncIOQueue(SDL_AsyncIOQueue* queue);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetAsyncIOResult(
        SDL_AsyncIOQueue* queue,
        ref SDL_AsyncIOOutcome outcome
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WaitAsyncIOResult(
        SDL_AsyncIOQueue* queue,
        ref SDL_AsyncIOOutcome outcome
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SignalAsyncIOQueue(SDL_AsyncIOQueue* queue);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_LoadFileAsync(
        string file,
        SDL_AsyncIOQueue* queue,
        void* userdata
    );
}
