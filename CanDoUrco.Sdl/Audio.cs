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
using unsafe SDL_AudioPostmixCallback = delegate* unmanaged[Cdecl]<
    void*,
    CanDoUrco.Sdl.Ffi.SDL_AudioSpec*,
    float*,
    int,
    void>;
using unsafe SDL_AudioStreamCallback = delegate* unmanaged[Cdecl]<
    void*,
    CanDoUrco.Sdl.Ffi.SDL_AudioStream*,
    int,
    int,
    void>;
using unsafe SDL_AudioStreamDataCompleteCallback = delegate* unmanaged[Cdecl]<
    void*,
    void*,
    int,
    void>;

namespace CanDoUrco.Sdl;

public static unsafe partial class Ffi
{
    public const byte SDL_AUDIO_MASK_BITSIZE = 0xFF;
    public const ushort SDL_AUDIO_MASK_FLOAT = 1 << 8;
    public const ushort SDL_AUDIO_MASK_BIG_ENDIAN = 1 << 12;
    public const ushort SDL_AUDIO_MASK_SIGNED = 1 << 15;

    // Macro!
    public static ushort SDL_DEFINE_AUDIO_FORMAT(
        bool signed,
        bool bigendian,
        bool flt,
        ushort size
    ) =>
        (ushort)(
            (signed ? 1 : 0)
            | (bigendian ? 1 : 0) << 12
            | (flt ? 1 : 0) << 8
            | (size & SDL_AUDIO_MASK_BITSIZE)
        );

    public readonly record struct SDL_AudioFormat(ushort Value)
    {
        public static implicit operator ushort(SDL_AudioFormat format) => format.Value;

        public static implicit operator SDL_AudioFormat(ushort format) => new(format);
    }

    public static SDL_AudioFormat SDL_AUDIO_UNKNOWN => new(0x0000);
    public static SDL_AudioFormat SDL_AUDIO_U8 => new(0x0008);
    public static SDL_AudioFormat SDL_AUDIO_S8 => new(0x8008);
    public static SDL_AudioFormat SDL_AUDIO_S16LE => new(0x8010);
    public static SDL_AudioFormat SDL_AUDIO_S16BE => new(0x9010);
    public static SDL_AudioFormat SDL_AUDIO_S32LE => new(0x8020);
    public static SDL_AudioFormat SDL_AUDIO_S32BE => new(0x9020);
    public static SDL_AudioFormat SDL_AUDIO_F32LE => new(0x8120);
    public static SDL_AudioFormat SDL_AUDIO_F32BE => new(0x9120);

    public static SDL_AudioFormat SDL_AUDIO_S16 =>
        BitConverter.IsLittleEndian ? SDL_AUDIO_S16LE : SDL_AUDIO_S16BE;
    public static SDL_AudioFormat SDL_AUDIO_S32 =>
        BitConverter.IsLittleEndian ? SDL_AUDIO_S32LE : SDL_AUDIO_S32BE;
    public static SDL_AudioFormat SDL_AUDIO_F32 =>
        BitConverter.IsLittleEndian ? SDL_AUDIO_F32LE : SDL_AUDIO_F32BE;

    // Macro!
    public static int SDL_AUDIO_BITSIZE(SDL_AudioFormat X) => X & SDL_AUDIO_MASK_BITSIZE;

    // Macro!
    public static int SDL_AUDIO_BYTESIZE(SDL_AudioFormat X) => SDL_AUDIO_BITSIZE(X) / 8;

    // Macro!
    public static bool SDL_AUDIO_ISFLOAT(SDL_AudioFormat X) => (X & SDL_AUDIO_MASK_FLOAT) != 0;

    // Macro!
    public static bool SDL_AUDIO_ISBIGENDIAN(SDL_AudioFormat X) =>
        (X & SDL_AUDIO_MASK_BIG_ENDIAN) != 0;

    // Macro!
    public static bool SDL_AUDIO_ISLITTLEENDIAN(SDL_AudioFormat X) => !SDL_AUDIO_ISBIGENDIAN(X);

    // Macro!
    public static bool SDL_AUDIO_ISSIGNED(SDL_AudioFormat X) => (X & SDL_AUDIO_MASK_SIGNED) != 0;

    // Macro!
    public static bool SDL_AUDIO_ISINT(SDL_AudioFormat X) => !SDL_AUDIO_ISFLOAT(X);

    // Macro!
    public static bool SDL_AUDIO_ISUNSIGNED(SDL_AudioFormat X) => !SDL_AUDIO_ISSIGNED(X);

    public readonly record struct SDL_AudioDeviceID(uint Value)
    {
        public static implicit operator SDL_AudioDeviceID(uint value) => new(value);

        public static implicit operator uint(SDL_AudioDeviceID value) => value.Value;
    }

    public static SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK => new(0xFFFF_FFFFU);
    public static SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_RECORDING => new(0xFFFF_FFFEU);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioSpec
    {
        public SDL_AudioFormat format;
        public int channels;
        public int freq;
    }

    // Macro!
    public static int SDL_AUDIO_FRAMESIZE(SDL_AudioSpec X) =>
        SDL_AUDIO_BYTESIZE(X.format) * X.channels;

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioStream;

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumAudioDrivers();

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetAudioDriver(int index);

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetCurrentAudioDriver();

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<SDL_AudioDeviceID, SDL_AudioDeviceID>),
        CountElementName = nameof(count)
    )]
    public static partial SDL_AudioDeviceID[]? SDL_GetAudioPlaybackDevices(out int count);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<SDL_AudioDeviceID, SDL_AudioDeviceID>),
        CountElementName = nameof(count)
    )]
    public static partial SDL_AudioDeviceID[]? SDL_GetAudioRecordingDevices(out int count);

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetAudioDeviceName(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetAudioDeviceFormat(
        SDL_AudioDeviceID devid,
        ref SDL_AudioSpec spec,
        out int sample_frames
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<int, int>),
        CountElementName = nameof(count)
    )]
    public static partial int[]? SDL_GetAudioDeviceChannelMap(
        SDL_AudioDeviceID devid,
        out int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID SDL_OpenAudioDevice(
        SDL_AudioDeviceID devid,
        in SDL_AudioSpec spec
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_IsAudioDevicePhysical(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_IsAudioDevicePlayback(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PauseAudioDevice(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ResumeAudioDevice(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_AudioDevicePaused(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioDeviceGain(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioDeviceGain(SDL_AudioDeviceID devid, float gain);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseAudioDevice(SDL_AudioDeviceID devid);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_BindAudioStreams(
        SDL_AudioDeviceID devid,
        SDL_AudioStream** streams,
        int num_streams
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_BindAudioStream(
        SDL_AudioDeviceID devid,
        SDL_AudioStream* stream
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnbindAudioStreams(SDL_AudioStream** streams, int num_streams);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnbindAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID SDL_GetAudioStreamDevice(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioStream* SDL_CreateAudioStream(
        in SDL_AudioSpec* src_spec,
        in SDL_AudioSpec* dst_spec
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetAudioStreamProperties(SDL_AudioStream* stream);

    public const string SDL_PROP_AUDIOSTREAM_AUTO_CLEANUP_BOOLEAN = "SDL.audiostream.auto_cleanup";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetAudioStreamFormat(
        SDL_AudioStream* stream,
        out SDL_AudioSpec src_spec,
        out SDL_AudioSpec dst_spec
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamFormat(
        SDL_AudioStream* stream,
        in SDL_AudioSpec src_spec,
        in SDL_AudioSpec dst_spec
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioStreamFrequencyRatio(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamFrequencyRatio(
        SDL_AudioStream* stream,
        float ratio
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioStreamGain(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamGain(SDL_AudioStream* stream, float gain);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<int, int>),
        CountElementName = nameof(count)
    )]
    public static partial int[]? SDL_GetAudioStreamInputChannelMap(
        SDL_AudioStream* stream,
        out int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<int, int>),
        CountElementName = nameof(count)
    )]
    public static partial int[]? SDL_GetAudioStreamOutputChannelMap(
        SDL_AudioStream* stream,
        out int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamInputChannelMap(
        SDL_AudioStream* stream,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<int, int>), CountElementName = nameof(count))]
            int[]? chmap,
        int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamOutputChannelMap(
        SDL_AudioStream* stream,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<int, int>), CountElementName = nameof(count))]
            int[]? chmap,
        int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PutAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PutAudioStreamDataNoCopy(
        SDL_AudioStream* stream,
        void* buf,
        int len,
        SDL_AudioStreamDataCompleteCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PutAudioStreamPlanarData(
        SDL_AudioStream* stream,
        void** channel_buffers,
        int num_channels,
        int num_samples
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamAvailable(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamQueued(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_FlushAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ClearAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PauseAudioStreamDevice(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ResumeAudioStreamDevice(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_AudioStreamDevicePaused(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_LockAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_UnlockAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamGetCallback(
        SDL_AudioStream* stream,
        SDL_AudioStreamCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioStreamPutCallback(
        SDL_AudioStream* stream,
        SDL_AudioStreamCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyAudioStream(SDL_AudioStream* stream);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioStream* SDL_OpenAudioDeviceStream(
        SDL_AudioDeviceID devid,
        in SDL_AudioSpec spec,
        SDL_AudioStreamCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAudioPostmixCallback(
        SDL_AudioDeviceID devid,
        SDL_AudioPostmixCallback callback,
        void* userdata
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_LoadWAV_IO(
        SDL_IOStream* src,
        [MarshalAs(UnmanagedType.I1)] bool closeio,
        ref SDL_AudioSpec* spec,
        [Out]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(audio_len))]
            byte[] audio_buf,
        out uint audio_len
    );

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_LoadWAV(
        string path,
        ref SDL_AudioSpec* spec,
        [Out]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(audio_len))]
            byte[] audio_buf,
        out uint audio_len
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_MixAudio(
        [In]
        [Out]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(len))]
            byte[] dst,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(len))]
            byte[] src,
        SDL_AudioFormat format,
        uint len,
        float volume
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ConvertAudioSamples(
        in SDL_AudioSpec src_spec,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(src_len))]
            byte[] src_data,
        int src_len,
        in SDL_AudioSpec* dst_spec,
        [Out]
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(dst_len))]
            byte[] dst_data,
        out int dst_len
    );

    [LibraryImport(
        DllName,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string SDL_GetAudioFormatName(SDL_AudioFormat format);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);
}
