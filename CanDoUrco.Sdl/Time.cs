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

public static partial class Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DateTime
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int nanosecond;
        public int day_of_week;
        public int utc_offset;
    }

    public readonly record struct SDL_DateFormat(int Value)
    {
        public static implicit operator int(SDL_DateFormat dateFormat) => dateFormat.Value;

        public static implicit operator SDL_DateFormat(int value) => new(value);
    }

    public static SDL_DateFormat SDL_DATE_FORMAT_YYYYMMDD => new(0);
    public static SDL_DateFormat SDL_DATE_FORMAT_DDMMYYYY => new(1);
    public static SDL_DateFormat SDL_DATE_FORMAT_MMDDYYYY => new(2);

    public readonly record struct SDL_TimeFormat(int Value)
    {
        public static implicit operator int(SDL_TimeFormat timeFormat) => timeFormat.Value;

        public static implicit operator SDL_TimeFormat(int value) => new(value);
    }

    public static SDL_TimeFormat SDL_TIME_FORMAT_24HR => new(0);
    public static SDL_TimeFormat SDL_TIME_FORMAT_12HR => new(1);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetDateTimeLocalePreferences(
        out SDL_DateFormat dateFormat,
        out SDL_TimeFormat timeFormat
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_GetCurrentTime(out SDL_Time ticks);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_TimeToDateTime(
        SDL_Time ticks,
        out SDL_DateTime dt,
        [MarshalAs(UnmanagedType.I1)] bool localTime
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_DateTimeToTime(in SDL_DateTime dt, out SDL_Time ticks);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_TimeToWindows(
        SDL_Time ticks,
        out uint dwLowDatTime,
        out uint dwHighDateTime
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Time SDL_TimeFromWindows(uint dwLowDatTime, uint dwHighDateTime);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetDaysInMonth(int year, int month);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetDayOfYear(int year, int month, int day);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetDayOfWeek(int year, int month, int day);
}
