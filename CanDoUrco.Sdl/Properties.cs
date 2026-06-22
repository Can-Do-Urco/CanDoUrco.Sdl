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
using unsafe SDL_CleanupPropertyCallback = delegate* unmanaged[Cdecl]<void*, void*, void>;
using unsafe SDL_EnumeratePropertiesCallback = delegate* unmanaged[Cdecl]<
    void*,
    CanDoUrco.Sdl.Ffi.SDL_PropertiesID,
    byte*,
    void>;

namespace CanDoUrco.Sdl;

public static unsafe partial class Ffi
{
    public readonly record struct SDL_PropertiesID(uint Value)
    {
        public static implicit operator uint(SDL_PropertiesID props) => props.Value;

        public static implicit operator SDL_PropertiesID(uint value) => new(value);
    }

    public readonly record struct SDL_PropertyType(int Value)
    {
        public static implicit operator int(SDL_PropertyType type) => type.Value;

        public static implicit operator SDL_PropertyType(int value) => new(value);
    }

    public static SDL_PropertyType SDL_PROPERTY_TYPE_INVALID => new(0);
    public static SDL_PropertyType SDL_PROPERTY_TYPE_POINTER => new(1);
    public static SDL_PropertyType SDL_PROPERTY_TYPE_STRING => new(2);
    public static SDL_PropertyType SDL_PROPERTY_TYPE_NUMBER => new(3);
    public static SDL_PropertyType SDL_PROPERTY_TYPE_FLOAT => new(4);
    public static SDL_PropertyType SDL_PROPERTY_TYPE_BOOLEAN => new(5);

    public const string SDL_PROP_NAME_STRING = "SDL.name";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetGlobalProperties();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_CreateProperties();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_CopyProperties(SDL_PropertiesID src, SDL_PropertiesID dst);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_LockProperties(SDL_PropertiesID props);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockProperties(SDL_PropertiesID props);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetPointerPropertyWithCleanup(
        SDL_PropertiesID props,
        string name,
        void* value,
        SDL_CleanupPropertyCallback cleanup,
        void* userdata
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetPointerProperty(
        SDL_PropertiesID props,
        string name,
        void* value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetStringProperty(
        SDL_PropertiesID props,
        string name,
        string? value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetNumberProperty(
        SDL_PropertiesID props,
        string name,
        long value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetFloatProperty(
        SDL_PropertiesID props,
        string name,
        float value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetBooleanProperty(
        SDL_PropertiesID props,
        string name,
        [MarshalAs(UnmanagedType.I1)] bool value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_HasProperty(SDL_PropertiesID props, string name);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertyType SDL_GetPropertyType(SDL_PropertiesID props, string name);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetPointerProperty(
        SDL_PropertiesID props,
        string name,
        void* default_value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnownedUtf8StringMarshaller))]
    public static partial string? SDL_GetStringProperty(
        SDL_PropertiesID props,
        string name,
        string? default_value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetNumberProperty(
        SDL_PropertiesID props,
        string name,
        long default_value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetFloatProperty(
        SDL_PropertiesID props,
        string name,
        float default_value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetBooleanProperty(
        SDL_PropertiesID props,
        string name,
        [MarshalAs(UnmanagedType.I1)] bool default_value
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ClearProperty(SDL_PropertiesID props, string name);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_EnumerateProperties(
        SDL_PropertiesID props,
        SDL_EnumeratePropertiesCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyProperties(SDL_PropertiesID props);
}
