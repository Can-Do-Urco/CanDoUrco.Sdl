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
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Sdl.CustomMarshallers;
using unsafe SDL_EnumerateDirectoryCallback = delegate* unmanaged[Cdecl]<
    void*,
    byte*,
    byte*,
    CanDoUrco.Sdl.Ffi.SDL_EnumerationResult>;

namespace CanDoUrco.Sdl;

public static unsafe partial class Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_StorageInterface
    {
        // SByte is Bool
        public uint version;
        public delegate* unmanaged[Cdecl]<void*, sbyte> close;
        public delegate* unmanaged[Cdecl]<void*, sbyte> ready;
        public delegate* unmanaged[Cdecl]<
            void*,
            byte*,
            SDL_EnumerateDirectoryCallback,
            void*,
            sbyte> enumerate;
        public delegate* unmanaged[Cdecl]<void*, byte*, SDL_PathInfo*, sbyte> info;
        public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> read_file;
        public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> write_file;
        public delegate* unmanaged[Cdecl]<void*, byte*, sbyte> mkdir;
        public delegate* unmanaged[Cdecl]<void*, byte*, sbyte> remove;
        public delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> rename;
        public delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> copy;
        public delegate* unmanaged[Cdecl]<void*, ulong> space_remaining;
    }

    public static void SDL_INIT_INTERFACE(ref SDL_StorageInterface iface)
    {
        // SDL_zerop(iface);
        iface = default;
        // (iface)->version = sizeof(*(iface));
        iface.version = (uint)Marshal.SizeOf<SDL_StorageInterface>();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Storage;

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenTitleStorage(
        string? @override,
        SDL_PropertiesID props
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenUserStorage(
        string org,
        string app,
        SDL_PropertiesID props
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenFileStorage(string? path);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenStorage(
        in SDL_StorageInterface iface,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CloseStorage(SDL_Storage* storage);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_StorageReady(SDL_Storage* storage);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetStorageFileSize(
        SDL_Storage* storage,
        string path,
        out ulong length
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ReadStorageFile(
        SDL_Storage* storage,
        string path,
        void* destination,
        ulong length
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_WriteStorageFile(
        SDL_Storage* storage,
        string path,
        void* source,
        ulong length
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CreateStorageDirectory(SDL_Storage* storage, string path);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_EnumerateStorageDirectory(
        SDL_Storage* storage,
        string path,
        SDL_EnumerateDirectoryCallback callback,
        void* userdata
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_RemoveStoragePath(SDL_Storage* storage, string path);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_RenameStoragePath(
        SDL_Storage* storage,
        string oldpath,
        string newpath
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CopyStoragePath(
        SDL_Storage* storage,
        string oldpath,
        string newpath
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetStoragePathInfo(
        SDL_Storage* storage,
        string path,
        out SDL_PathInfo info
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetStorageSpaceRemaining(SDL_Storage* storage);

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<string, nint>),
        CountElementName = nameof(count)
    )]
    public static partial string[]? SDL_GlobStorageDirectory(
        SDL_Storage* storage,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string path,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? pattern,
        SDL_GlobFlags flags,
        out int count
    );
}
