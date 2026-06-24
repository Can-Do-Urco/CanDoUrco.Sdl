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

public static unsafe partial class Ffi
{
    public readonly record struct SDL_IOStatus(int Value)
    {
        public static implicit operator int(SDL_IOStatus status) => status.Value;

        public static implicit operator SDL_IOStatus(int status) => new(status);
    }

    public static SDL_IOStatus SDL_IO_STATUS_READY => new(0);
    public static SDL_IOStatus SDL_IO_STATUS_ERROR => new(1);
    public static SDL_IOStatus SDL_IO_STATUS_EOF => new(2);
    public static SDL_IOStatus SDL_IO_STATUS_NOT_READY => new(3);
    public static SDL_IOStatus SDL_IO_STATUS_READONLY => new(4);
    public static SDL_IOStatus SDL_IO_STATUS_WRITEONLY => new(5);

    public readonly record struct SDL_IOWhence(int Value)
    {
        public static implicit operator int(SDL_IOWhence whence) => whence.Value;

        public static implicit operator SDL_IOWhence(int value) => new(value);
    }

    public static SDL_IOWhence SDL_IO_SEEK_SET => new(0);
    public static SDL_IOWhence SDL_IO_SEEK_CUR => new(1);
    public static SDL_IOWhence SDL_IO_SEEK_END => new(2);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_IOStreamInterface
    {
        // SByte is Bool
        public uint version;
        public delegate* unmanaged[Cdecl]<void*, long> size;
        public delegate* unmanaged[Cdecl]<void*, long, SDL_IOWhence, long> seek;
        public delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*> read;
        public delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*> write;
        public delegate* unmanaged[Cdecl]<void*, SDL_IOStatus*, sbyte> flush;
        public delegate* unmanaged[Cdecl]<void*, sbyte> close;
    }

    // From the SDL_INIT_INTERFACE macro!
    public static void SDL_INIT_INTERFACE(ref SDL_IOStreamInterface iface)
    {
        // SDL_zerop(iface);
        iface = default;
        // (iface)->version = sizeof(*(iface));
        iface.version = (uint)Marshal.SizeOf<SDL_IOStreamInterface>();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_IOStream;

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromFile(string file, string mode);

    public const string SDL_PROP_IOSTREAM_WINDOWS_HANDLE_POINTER = "SDL.iostream.windows.handle";
    public const string SDL_PROP_IOSTREAM_STDIO_FILE_POINTER = "SDL.iostream.stdio.file";
    public const string SDL_PROP_IOSTREAM_FILE_DESCRIPTOR_NUMBER = "SDL.iostream.file_descriptor";
    public const string SDL_PROP_IOSTREAM_ANDROID_AASSET_POINTER = "SDL.iostream.android.aasset";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromMem(void* mem, nuint size);

    public const string SDL_PROP_IOSTREAM_MEMORY_POINTER = "SDL.iostream.memory.base";
    public const string SDL_PROP_IOSTREAM_MEMORY_SIZE_NUMBER = "SDL.iostream.memory.size";
    public const string SDL_PROP_IOSTREAM_MEMORY_FREE_FUNC_POINTER = "SDL.iostream.memory.free";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromConstMem(void* mem, nuint size);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromDynamicMem();

    public const string SDL_PROP_IOSTREAM_DYNAMIC_MEMORY_POINTER = "SDL.iostream.dynamic.memory";
    public const string SDL_PROP_IOSTREAM_DYNAMIC_CHUNKSIZE_NUMBER =
        "SDL.iostream.dynamic.chunksize";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_OpenIO(in SDL_IOStreamInterface iface, void* uesrdata);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CloseIO(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetIOProperties(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStatus SDL_GetIOStatus(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetIOSize(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_SeekIO(SDL_IOStream* context, long offset, SDL_IOWhence whence);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_TellIO(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_ReadIO(SDL_IOStream* context, void* ptr, nuint size);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_WriteIO(SDL_IOStream* context, void* ptr, nuint size);

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFormatCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_IOprintf(SDL_IOStream* context, string fmt);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_FlushIO(SDL_IOStream* context);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_LoadFile_IO(
        SDL_IOStream* src,
        out nuint datasize,
        [MarshalAs(UnmanagedType.I1)] bool closeio
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_LoadFile(string file, out nuint datasize);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SaveFile_IO(
        SDL_IOStream* src,
        void* data,
        nuint datasize,
        [MarshalAs(UnmanagedType.I1)] bool closeio
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SaveFile(string file, void* data, nuint datasize);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU8(SDL_IOStream* src, out byte value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SReadU8(SDL_IOStream* src, out sbyte value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU16LE(SDL_IOStream* src, out ushort value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS16LE(SDL_IOStream* src, out short value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU16BE(SDL_IOStream* src, out ushort value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS16BE(SDL_IOStream* src, out short value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU32LE(SDL_IOStream* src, out uint value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS32LE(SDL_IOStream* src, out int value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU32BE(SDL_IOStream* src, out uint value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS32BE(SDL_IOStream* src, out int value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU64LE(SDL_IOStream* src, out ulong value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS64LE(SDL_IOStream* src, out long value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadU64BE(SDL_IOStream* src, out ulong value);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadS64BE(SDL_IOStream* src, out long value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU8(SDL_IOStream* dst, byte value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS8(SDL_IOStream* dst, sbyte value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU16LE(SDL_IOStream* dst, ushort value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS16LE(SDL_IOStream* dst, short value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU16BE(SDL_IOStream* dst, ushort value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS16BE(SDL_IOStream* dst, short value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU32LE(SDL_IOStream* dst, uint value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS32LE(SDL_IOStream* dst, int value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU32BE(SDL_IOStream* dst, uint value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS32BE(SDL_IOStream* dst, int value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU64LE(SDL_IOStream* dst, ulong value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS64LE(SDL_IOStream* dst, long value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteU64BE(SDL_IOStream* dst, ulong value);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteS64BE(SDL_IOStream* dst, long value);
}
