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
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetBasePath();

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlAllocatedUtf8StringMarshaller))]
    public static partial string? SDL_GetPrefPath(string? org, string app);

    public readonly record struct SDL_Folder(int Value);

    public static SDL_Folder SDL_FOLDER_HOME => new(0);
    public static SDL_Folder SDL_FOLDER_DESKTOP => new(1);
    public static SDL_Folder SDL_FOLDER_DOCUMENTS => new(2);
    public static SDL_Folder SDL_FOLDER_DOWNLOADS => new(3);
    public static SDL_Folder SDL_FOLDER_MUSIC => new(4);
    public static SDL_Folder SDL_FOLDER_PICTURES => new(5);
    public static SDL_Folder SDL_FOLDER_PUBLICSHARE => new(6);
    public static SDL_Folder SDL_FOLDER_SAVEDGAMES => new(7);
    public static SDL_Folder SDL_FOLDER_SCREENSHOTS => new(8);
    public static SDL_Folder SDL_FOLDER_TEMPLATES => new(9);
    public static SDL_Folder SDL_FOLDER_VIDEOS => new(10);
    public static SDL_Folder SDL_FOLDER_COUNT => new(11);

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetUserFolder(SDL_Folder folder);

    public readonly record struct SDL_PathType(int Value);

    public static SDL_PathType SDL_PATHTYPE_NONE => new(0);
    public static SDL_PathType SDL_PATHTYPE_FILE => new(1);
    public static SDL_PathType SDL_PATHTYPE_DIRECTORY => new(2);
    public static SDL_PathType SDL_PATHTYPE_OTHER => new(3);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PathInfo
    {
        public SDL_PathType type;
        public ulong size;
        public SDL_Time create_time;
        public SDL_Time modify_time;
        public SDL_Time access_time;
    }

    public readonly record struct SDL_GlobFlags(uint Value)
    {
        public static implicit operator uint(SDL_GlobFlags flags) => flags.Value;

        public static implicit operator SDL_GlobFlags(uint value) => new(value);
    }

    public static SDL_GlobFlags SDL_GLOB_CASEINSENSITIVE => new(1U << 0);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CreateDirectory(string path);

    public readonly record struct SDL_EnumerationResult(int Value)
    {
        public static implicit operator int(SDL_EnumerationResult result) => result.Value;

        public static implicit operator SDL_EnumerationResult(int value) => new(value);
    }

    public static SDL_EnumerationResult SDL_ENUM_CONTINUE => new(0);
    public static SDL_EnumerationResult SDL_ENUM_SUCCESS => new(1);
    public static SDL_EnumerationResult SDL_ENUM_FAILURE => new(2);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_EnumerateDirectory(
        string path,
        SDL_EnumerateDirectoryCallback callback,
        void* userdata
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_RemovePath(string path);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_RenamePath(string oldpath, string newpath);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CopyFile(string oldpath, string newpath);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetPathInfo(string path, out SDL_PathInfo info);

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
    public static partial string[]? SDL_GlobDirectory(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string path,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? pattern,
        SDL_GlobFlags flags,
        out int count
    );

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetCurrentDirectory();
}
