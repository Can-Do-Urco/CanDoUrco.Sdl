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

using System.Runtime.InteropServices.Marshalling;
using System.Text;
using static CanDoUrco.Sdl.Ffi;

namespace CanDoUrco.Sdl.CustomMarshallers;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(SdlAllocatedUtf8StringMarshaller))]
internal static unsafe class SdlAllocatedUtf8StringMarshaller
{
    public static byte* ConvertToUnmanaged(string? managed)
    {
        if (managed is null)
        {
            return null;
        }

        int num = checked(Encoding.UTF8.GetByteCount(managed) + 1);
        var pointer = (byte*)SDL_malloc((nuint)num);
        Span<byte> bytes1 = new(pointer, num);
        int bytes2 = Encoding.UTF8.GetBytes(managed.AsSpan(), bytes1);
        bytes1[bytes2] = 0;
        return pointer;
    }

    public static string? ConvertToManaged(byte* unmanaged) =>
        Utf8StringMarshaller.ConvertToManaged(unmanaged);

    public static void Free(byte* unmanaged)
    {
        if (unmanaged is null)
        {
            return;
        }

        SDL_free(unmanaged);
    }
}
