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
using static CanDoUrco.Sdl.Ffi;

namespace CanDoUrco.Sdl.CustomMarshallers;

[CustomMarshaller(
    typeof(CustomMarshallerAttribute.GenericPlaceholder[]),
    MarshalMode.Default,
    typeof(SdlAllocatedArrayMarshaller<,>)
)]
[CustomMarshaller(
    typeof(CustomMarshallerAttribute.GenericPlaceholder[]),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(SdlAllocatedArrayMarshaller<,>.ManagedToUnmanagedIn)
)]
[ContiguousCollectionMarshaller]
internal static unsafe class SdlAllocatedArrayMarshaller<T, TUnmanagedElement>
    where TUnmanagedElement : unmanaged
{
    public static TUnmanagedElement* AllocateContainerForUnmanagedElements(
        T[]? managed,
        out int numElements
    )
    {
        if (managed is null)
        {
            numElements = 0;
            return null;
        }

        numElements = managed.Length;
        return (TUnmanagedElement*)SDL_malloc(
            nuint.Max((nuint)checked(sizeof(TUnmanagedElement) * numElements), 1)
        );
    }

    public static ReadOnlySpan<T> GetManagedValuesSource(T[]? managed) =>
        managed is null ? default : (ReadOnlySpan<T>)managed;

    public static Span<TUnmanagedElement> GetUnmanagedValuesDestination(
        TUnmanagedElement* unmanaged,
        int numElements
    ) => unmanaged is null ? default : new Span<TUnmanagedElement>(unmanaged, numElements);

    public static T[]? AllocateContainerForManagedElements(
        TUnmanagedElement* unmanaged,
        int numElements
    ) => unmanaged is null ? null : new T[numElements];

    public static Span<T> GetManagedValuesDestination(T[]? managed) =>
        managed is null ? default : (Span<T>)managed;

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(
        TUnmanagedElement* unmanagedValue,
        int numElements
    ) =>
        unmanagedValue is null
            ? default
            : new ReadOnlySpan<TUnmanagedElement>(unmanagedValue, numElements);

    public static void Free(TUnmanagedElement* unmanaged)
    {
        if (unmanaged is null)
        {
            return;
        }

        SDL_free(unmanaged);
    }

    public ref struct ManagedToUnmanagedIn
    {
        private T[]? _managedArray;
        private TUnmanagedElement* _allocatedMemory;
        private Span<TUnmanagedElement> _span;

        public static int BufferSize => 0x0200 / sizeof(TUnmanagedElement);

        public void FromManaged(T[]? array, Span<TUnmanagedElement> buffer)
        {
            _allocatedMemory = null;
            if (array is null)
            {
                _managedArray = null;
                _span = default;
                return;
            }

            _managedArray = array;
            if (array.Length <= buffer.Length)
            {
                _span = buffer[..array.Length];
            }
            else
            {
                _allocatedMemory = (TUnmanagedElement*)SDL_malloc(
                    nuint.Max((nuint)checked(array.Length * sizeof(TUnmanagedElement)), 1)
                );

                _span = new Span<TUnmanagedElement>(_allocatedMemory, array.Length);
            }
        }

        public ReadOnlySpan<T> GetManagedValuesSource() =>
            _managedArray is null ? default : (ReadOnlySpan<T>)_managedArray;

        public Span<TUnmanagedElement> GetUnmanagedValuesDestination() => _span;

        public ref TUnmanagedElement GetPinnableReference() =>
            ref MemoryMarshal.GetReference(_span);

        public TUnmanagedElement* ToUnmanaged() =>
            (TUnmanagedElement*)Unsafe.AsPointer(ref GetPinnableReference());

        public void Free()
        {
            if (_allocatedMemory is null)
            {
                return;
            }

            SDL_free(_allocatedMemory);
        }

        public static ref T GetPinnableReference(T[]? array) =>
            ref (
                array is null
                    ? ref Unsafe.NullRef<T>()
                    : ref MemoryMarshal.GetArrayDataReference(array)
            );
    }
}
