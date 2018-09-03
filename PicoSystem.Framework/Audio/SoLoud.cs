﻿using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Audio
{
    public class SoloudObject
    {
        public IntPtr objhandle;
    }

    public class Soloud : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        public const int SDL2 = 2;
        public const int SDL = 1;
        public const int BACKEND_MAX = 13;
        public const int WINMM = 4;
        public const int XAUDIO2 = 5;
        public const int OSS = 8;
        public const int WASAPI = 6;
        public const int CLIP_ROUNDOFF = 1;
        public const int NULLDRIVER = 12;
        public const int LEFT_HANDED_3D = 4;
        public const int ENABLE_VISUALIZATION = 2;
        public const int OPENAL = 9;
        public const int ALSA = 7;
        public const int COREAUDIO = 10;
        public const int AUTO = 0;
        public const int PORTAUDIO = 3;
        public const int OPENSLES = 11;

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_create();
        public Soloud()
        {
            objhandle = Soloud_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_destroy(IntPtr aObjHandle);
        ~Soloud()
        {
            Soloud_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_initEx(IntPtr aObjHandle, uint aFlags, uint aBackend, uint aSamplerate, uint aBufferSize, uint aChannels);
        public int init(uint aFlags = CLIP_ROUNDOFF, uint aBackend = AUTO, uint aSamplerate = AUTO, uint aBufferSize = AUTO, uint aChannels = 2)
        {
            return Soloud_initEx(objhandle, aFlags, aBackend, aSamplerate, aBufferSize, aChannels);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_deinit(IntPtr aObjHandle);
        public void deinit()
        {
            Soloud_deinit(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getVersion(IntPtr aObjHandle);
        public uint getVersion()
        {
            return Soloud_getVersion(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_getErrorString(IntPtr aObjHandle, int aErrorCode);
        public string getErrorString(int aErrorCode)
        {
            IntPtr p = Soloud_getErrorString(objhandle, aErrorCode);
            return Marshal.PtrToStringAnsi(p);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getBackendId(IntPtr aObjHandle);
        public uint getBackendId()
        {
            return Soloud_getBackendId(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_getBackendString(IntPtr aObjHandle);
        public string getBackendString()
        {
            IntPtr p = Soloud_getBackendString(objhandle);
            return Marshal.PtrToStringAnsi(p);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getBackendChannels(IntPtr aObjHandle);
        public uint getBackendChannels()
        {
            return Soloud_getBackendChannels(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getBackendSamplerate(IntPtr aObjHandle);
        public uint getBackendSamplerate()
        {
            return Soloud_getBackendSamplerate(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getBackendBufferSize(IntPtr aObjHandle);
        public uint getBackendBufferSize()
        {
            return Soloud_getBackendBufferSize(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_setSpeakerPosition(IntPtr aObjHandle, uint aChannel, float aX, float aY, float aZ);
        public int setSpeakerPosition(uint aChannel, float aX, float aY, float aZ)
        {
            return Soloud_setSpeakerPosition(objhandle, aChannel, aX, aY, aZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_playEx(IntPtr aObjHandle, IntPtr aSound, float aVolume, float aPan, int aPaused, uint aBus);
        public uint play(SoloudObject aSound, float aVolume = -1.0f, float aPan = 0.0f, int aPaused = 0, uint aBus = 0)
        {
            return Soloud_playEx(objhandle, aSound.objhandle, aVolume, aPan, aPaused, aBus);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_playClockedEx(IntPtr aObjHandle, double aSoundTime, IntPtr aSound, float aVolume, float aPan, uint aBus);
        public uint playClocked(double aSoundTime, SoloudObject aSound, float aVolume = -1.0f, float aPan = 0.0f, uint aBus = 0)
        {
            return Soloud_playClockedEx(objhandle, aSoundTime, aSound.objhandle, aVolume, aPan, aBus);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_play3dEx(IntPtr aObjHandle, IntPtr aSound, float aPosX, float aPosY, float aPosZ, float aVelX, float aVelY, float aVelZ, float aVolume, int aPaused, uint aBus);
        public uint play3d(SoloudObject aSound, float aPosX, float aPosY, float aPosZ, float aVelX = 0.0f, float aVelY = 0.0f, float aVelZ = 0.0f, float aVolume = 1.0f, int aPaused = 0, uint aBus = 0)
        {
            return Soloud_play3dEx(objhandle, aSound.objhandle, aPosX, aPosY, aPosZ, aVelX, aVelY, aVelZ, aVolume, aPaused, aBus);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_play3dClockedEx(IntPtr aObjHandle, double aSoundTime, IntPtr aSound, float aPosX, float aPosY, float aPosZ, float aVelX, float aVelY, float aVelZ, float aVolume, uint aBus);
        public uint play3dClocked(double aSoundTime, SoloudObject aSound, float aPosX, float aPosY, float aPosZ, float aVelX = 0.0f, float aVelY = 0.0f, float aVelZ = 0.0f, float aVolume = 1.0f, uint aBus = 0)
        {
            return Soloud_play3dClockedEx(objhandle, aSoundTime, aSound.objhandle, aPosX, aPosY, aPosZ, aVelX, aVelY, aVelZ, aVolume, aBus);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_seek(IntPtr aObjHandle, uint aVoiceHandle, double aSeconds);
        public void seek(uint aVoiceHandle, double aSeconds)
        {
            Soloud_seek(objhandle, aVoiceHandle, aSeconds);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_stop(IntPtr aObjHandle, uint aVoiceHandle);
        public void stop(uint aVoiceHandle)
        {
            Soloud_stop(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_stopAll(IntPtr aObjHandle);
        public void stopAll()
        {
            Soloud_stopAll(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_stopAudioSource(IntPtr aObjHandle, IntPtr aSound);
        public void stopAudioSource(SoloudObject aSound)
        {
            Soloud_stopAudioSource(objhandle, aSound.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setFilterParameter(IntPtr aObjHandle, uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aValue);
        public void setFilterParameter(uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aValue)
        {
            Soloud_setFilterParameter(objhandle, aVoiceHandle, aFilterId, aAttributeId, aValue);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getFilterParameter(IntPtr aObjHandle, uint aVoiceHandle, uint aFilterId, uint aAttributeId);
        public float getFilterParameter(uint aVoiceHandle, uint aFilterId, uint aAttributeId)
        {
            return Soloud_getFilterParameter(objhandle, aVoiceHandle, aFilterId, aAttributeId);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_fadeFilterParameter(IntPtr aObjHandle, uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aTo, double aTime);
        public void fadeFilterParameter(uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aTo, double aTime)
        {
            Soloud_fadeFilterParameter(objhandle, aVoiceHandle, aFilterId, aAttributeId, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_oscillateFilterParameter(IntPtr aObjHandle, uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aFrom, float aTo, double aTime);
        public void oscillateFilterParameter(uint aVoiceHandle, uint aFilterId, uint aAttributeId, float aFrom, float aTo, double aTime)
        {
            Soloud_oscillateFilterParameter(objhandle, aVoiceHandle, aFilterId, aAttributeId, aFrom, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double Soloud_getStreamTime(IntPtr aObjHandle, uint aVoiceHandle);
        public double getStreamTime(uint aVoiceHandle)
        {
            return Soloud_getStreamTime(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_getPause(IntPtr aObjHandle, uint aVoiceHandle);
        public int getPause(uint aVoiceHandle)
        {
            return Soloud_getPause(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getVolume(IntPtr aObjHandle, uint aVoiceHandle);
        public float getVolume(uint aVoiceHandle)
        {
            return Soloud_getVolume(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getOverallVolume(IntPtr aObjHandle, uint aVoiceHandle);
        public float getOverallVolume(uint aVoiceHandle)
        {
            return Soloud_getOverallVolume(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getPan(IntPtr aObjHandle, uint aVoiceHandle);
        public float getPan(uint aVoiceHandle)
        {
            return Soloud_getPan(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getSamplerate(IntPtr aObjHandle, uint aVoiceHandle);
        public float getSamplerate(uint aVoiceHandle)
        {
            return Soloud_getSamplerate(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_getProtectVoice(IntPtr aObjHandle, uint aVoiceHandle);
        public int getProtectVoice(uint aVoiceHandle)
        {
            return Soloud_getProtectVoice(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getActiveVoiceCount(IntPtr aObjHandle);
        public uint getActiveVoiceCount()
        {
            return Soloud_getActiveVoiceCount(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getVoiceCount(IntPtr aObjHandle);
        public uint getVoiceCount()
        {
            return Soloud_getVoiceCount(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_isValidVoiceHandle(IntPtr aObjHandle, uint aVoiceHandle);
        public int isValidVoiceHandle(uint aVoiceHandle)
        {
            return Soloud_isValidVoiceHandle(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getRelativePlaySpeed(IntPtr aObjHandle, uint aVoiceHandle);
        public float getRelativePlaySpeed(uint aVoiceHandle)
        {
            return Soloud_getRelativePlaySpeed(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getPostClipScaler(IntPtr aObjHandle);
        public float getPostClipScaler()
        {
            return Soloud_getPostClipScaler(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getGlobalVolume(IntPtr aObjHandle);
        public float getGlobalVolume()
        {
            return Soloud_getGlobalVolume(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getMaxActiveVoiceCount(IntPtr aObjHandle);
        public uint getMaxActiveVoiceCount()
        {
            return Soloud_getMaxActiveVoiceCount(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_getLooping(IntPtr aObjHandle, uint aVoiceHandle);
        public int getLooping(uint aVoiceHandle)
        {
            return Soloud_getLooping(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setLooping(IntPtr aObjHandle, uint aVoiceHandle, int aLooping);
        public void setLooping(uint aVoiceHandle, int aLooping)
        {
            Soloud_setLooping(objhandle, aVoiceHandle, aLooping);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_setMaxActiveVoiceCount(IntPtr aObjHandle, uint aVoiceCount);
        public int setMaxActiveVoiceCount(uint aVoiceCount)
        {
            return Soloud_setMaxActiveVoiceCount(objhandle, aVoiceCount);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setInaudibleBehavior(IntPtr aObjHandle, uint aVoiceHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(uint aVoiceHandle, int aMustTick, int aKill)
        {
            Soloud_setInaudibleBehavior(objhandle, aVoiceHandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setGlobalVolume(IntPtr aObjHandle, float aVolume);
        public void setGlobalVolume(float aVolume)
        {
            Soloud_setGlobalVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPostClipScaler(IntPtr aObjHandle, float aScaler);
        public void setPostClipScaler(float aScaler)
        {
            Soloud_setPostClipScaler(objhandle, aScaler);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPause(IntPtr aObjHandle, uint aVoiceHandle, int aPause);
        public void setPause(uint aVoiceHandle, int aPause)
        {
            Soloud_setPause(objhandle, aVoiceHandle, aPause);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPauseAll(IntPtr aObjHandle, int aPause);
        public void setPauseAll(int aPause)
        {
            Soloud_setPauseAll(objhandle, aPause);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_setRelativePlaySpeed(IntPtr aObjHandle, uint aVoiceHandle, float aSpeed);
        public int setRelativePlaySpeed(uint aVoiceHandle, float aSpeed)
        {
            return Soloud_setRelativePlaySpeed(objhandle, aVoiceHandle, aSpeed);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setProtectVoice(IntPtr aObjHandle, uint aVoiceHandle, int aProtect);
        public void setProtectVoice(uint aVoiceHandle, int aProtect)
        {
            Soloud_setProtectVoice(objhandle, aVoiceHandle, aProtect);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setSamplerate(IntPtr aObjHandle, uint aVoiceHandle, float aSamplerate);
        public void setSamplerate(uint aVoiceHandle, float aSamplerate)
        {
            Soloud_setSamplerate(objhandle, aVoiceHandle, aSamplerate);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPan(IntPtr aObjHandle, uint aVoiceHandle, float aPan);
        public void setPan(uint aVoiceHandle, float aPan)
        {
            Soloud_setPan(objhandle, aVoiceHandle, aPan);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPanAbsoluteEx(IntPtr aObjHandle, uint aVoiceHandle, float aLVolume, float aRVolume, float aLBVolume, float aRBVolume, float aCVolume, float aSVolume);
        public void setPanAbsolute(uint aVoiceHandle, float aLVolume, float aRVolume, float aLBVolume = 0, float aRBVolume = 0, float aCVolume = 0, float aSVolume = 0)
        {
            Soloud_setPanAbsoluteEx(objhandle, aVoiceHandle, aLVolume, aRVolume, aLBVolume, aRBVolume, aCVolume, aSVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setVolume(IntPtr aObjHandle, uint aVoiceHandle, float aVolume);
        public void setVolume(uint aVoiceHandle, float aVolume)
        {
            Soloud_setVolume(objhandle, aVoiceHandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setDelaySamples(IntPtr aObjHandle, uint aVoiceHandle, uint aSamples);
        public void setDelaySamples(uint aVoiceHandle, uint aSamples)
        {
            Soloud_setDelaySamples(objhandle, aVoiceHandle, aSamples);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_fadeVolume(IntPtr aObjHandle, uint aVoiceHandle, float aTo, double aTime);
        public void fadeVolume(uint aVoiceHandle, float aTo, double aTime)
        {
            Soloud_fadeVolume(objhandle, aVoiceHandle, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_fadePan(IntPtr aObjHandle, uint aVoiceHandle, float aTo, double aTime);
        public void fadePan(uint aVoiceHandle, float aTo, double aTime)
        {
            Soloud_fadePan(objhandle, aVoiceHandle, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_fadeRelativePlaySpeed(IntPtr aObjHandle, uint aVoiceHandle, float aTo, double aTime);
        public void fadeRelativePlaySpeed(uint aVoiceHandle, float aTo, double aTime)
        {
            Soloud_fadeRelativePlaySpeed(objhandle, aVoiceHandle, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_fadeGlobalVolume(IntPtr aObjHandle, float aTo, double aTime);
        public void fadeGlobalVolume(float aTo, double aTime)
        {
            Soloud_fadeGlobalVolume(objhandle, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_schedulePause(IntPtr aObjHandle, uint aVoiceHandle, double aTime);
        public void schedulePause(uint aVoiceHandle, double aTime)
        {
            Soloud_schedulePause(objhandle, aVoiceHandle, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_scheduleStop(IntPtr aObjHandle, uint aVoiceHandle, double aTime);
        public void scheduleStop(uint aVoiceHandle, double aTime)
        {
            Soloud_scheduleStop(objhandle, aVoiceHandle, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_oscillateVolume(IntPtr aObjHandle, uint aVoiceHandle, float aFrom, float aTo, double aTime);
        public void oscillateVolume(uint aVoiceHandle, float aFrom, float aTo, double aTime)
        {
            Soloud_oscillateVolume(objhandle, aVoiceHandle, aFrom, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_oscillatePan(IntPtr aObjHandle, uint aVoiceHandle, float aFrom, float aTo, double aTime);
        public void oscillatePan(uint aVoiceHandle, float aFrom, float aTo, double aTime)
        {
            Soloud_oscillatePan(objhandle, aVoiceHandle, aFrom, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_oscillateRelativePlaySpeed(IntPtr aObjHandle, uint aVoiceHandle, float aFrom, float aTo, double aTime);
        public void oscillateRelativePlaySpeed(uint aVoiceHandle, float aFrom, float aTo, double aTime)
        {
            Soloud_oscillateRelativePlaySpeed(objhandle, aVoiceHandle, aFrom, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_oscillateGlobalVolume(IntPtr aObjHandle, float aFrom, float aTo, double aTime);
        public void oscillateGlobalVolume(float aFrom, float aTo, double aTime)
        {
            Soloud_oscillateGlobalVolume(objhandle, aFrom, aTo, aTime);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setGlobalFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setGlobalFilter(uint aFilterId, SoloudObject aFilter)
        {
            Soloud_setGlobalFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setVisualizationEnable(IntPtr aObjHandle, int aEnable);
        public void setVisualizationEnable(int aEnable)
        {
            Soloud_setVisualizationEnable(objhandle, aEnable);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_calcFFT(IntPtr aObjHandle);
        public float[] calcFFT()
        {
            float[] ret = new float[256];
            IntPtr p = Soloud_calcFFT(objhandle);

            byte[] buffer = new byte[4];
            for (int i = 0; i < ret.Length; ++i)
            {
                int f_bits = Marshal.ReadInt32(p, i * 4);
                buffer[0] = (byte)((f_bits >> 0) & 0xff);
                buffer[1] = (byte)((f_bits >> 8) & 0xff);
                buffer[2] = (byte)((f_bits >> 16) & 0xff);
                buffer[3] = (byte)((f_bits >> 24) & 0xff);
                ret[i] = BitConverter.ToSingle(buffer, 0);
            }
            return ret;
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Soloud_getWave(IntPtr aObjHandle);
        public float[] getWave()
        {
            float[] ret = new float[256];
            IntPtr p = Soloud_getWave(objhandle);

            byte[] buffer = new byte[4];
            for (int i = 0; i < ret.Length; ++i)
            {
                int f_bits = Marshal.ReadInt32(p, i * 4);
                buffer[0] = (byte)((f_bits >> 0) & 0xff);
                buffer[1] = (byte)((f_bits >> 8) & 0xff);
                buffer[2] = (byte)((f_bits >> 16) & 0xff);
                buffer[3] = (byte)((f_bits >> 24) & 0xff);
                ret[i] = BitConverter.ToSingle(buffer, 0);
            }
            return ret;
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_getLoopCount(IntPtr aObjHandle, uint aVoiceHandle);
        public uint getLoopCount(uint aVoiceHandle)
        {
            return Soloud_getLoopCount(objhandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_getInfo(IntPtr aObjHandle, uint aVoiceHandle, uint aInfoKey);
        public float getInfo(uint aVoiceHandle, uint aInfoKey)
        {
            return Soloud_getInfo(objhandle, aVoiceHandle, aInfoKey);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Soloud_createVoiceGroup(IntPtr aObjHandle);
        public uint createVoiceGroup()
        {
            return Soloud_createVoiceGroup(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_destroyVoiceGroup(IntPtr aObjHandle, uint aVoiceGroupHandle);
        public int destroyVoiceGroup(uint aVoiceGroupHandle)
        {
            return Soloud_destroyVoiceGroup(objhandle, aVoiceGroupHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_addVoiceToGroup(IntPtr aObjHandle, uint aVoiceGroupHandle, uint aVoiceHandle);
        public int addVoiceToGroup(uint aVoiceGroupHandle, uint aVoiceHandle)
        {
            return Soloud_addVoiceToGroup(objhandle, aVoiceGroupHandle, aVoiceHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_isVoiceGroup(IntPtr aObjHandle, uint aVoiceGroupHandle);
        public int isVoiceGroup(uint aVoiceGroupHandle)
        {
            return Soloud_isVoiceGroup(objhandle, aVoiceGroupHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_isVoiceGroupEmpty(IntPtr aObjHandle, uint aVoiceGroupHandle);
        public int isVoiceGroupEmpty(uint aVoiceGroupHandle)
        {
            return Soloud_isVoiceGroupEmpty(objhandle, aVoiceGroupHandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_update3dAudio(IntPtr aObjHandle);
        public void update3dAudio()
        {
            Soloud_update3dAudio(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Soloud_set3dSoundSpeed(IntPtr aObjHandle, float aSpeed);
        public int set3dSoundSpeed(float aSpeed)
        {
            return Soloud_set3dSoundSpeed(objhandle, aSpeed);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Soloud_get3dSoundSpeed(IntPtr aObjHandle);
        public float get3dSoundSpeed()
        {
            return Soloud_get3dSoundSpeed(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dListenerParametersEx(IntPtr aObjHandle, float aPosX, float aPosY, float aPosZ, float aAtX, float aAtY, float aAtZ, float aUpX, float aUpY, float aUpZ, float aVelocityX, float aVelocityY, float aVelocityZ);
        public void set3dListenerParameters(float aPosX, float aPosY, float aPosZ, float aAtX, float aAtY, float aAtZ, float aUpX, float aUpY, float aUpZ, float aVelocityX = 0.0f, float aVelocityY = 0.0f, float aVelocityZ = 0.0f)
        {
            Soloud_set3dListenerParametersEx(objhandle, aPosX, aPosY, aPosZ, aAtX, aAtY, aAtZ, aUpX, aUpY, aUpZ, aVelocityX, aVelocityY, aVelocityZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dListenerPosition(IntPtr aObjHandle, float aPosX, float aPosY, float aPosZ);
        public void set3dListenerPosition(float aPosX, float aPosY, float aPosZ)
        {
            Soloud_set3dListenerPosition(objhandle, aPosX, aPosY, aPosZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dListenerAt(IntPtr aObjHandle, float aAtX, float aAtY, float aAtZ);
        public void set3dListenerAt(float aAtX, float aAtY, float aAtZ)
        {
            Soloud_set3dListenerAt(objhandle, aAtX, aAtY, aAtZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dListenerUp(IntPtr aObjHandle, float aUpX, float aUpY, float aUpZ);
        public void set3dListenerUp(float aUpX, float aUpY, float aUpZ)
        {
            Soloud_set3dListenerUp(objhandle, aUpX, aUpY, aUpZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dListenerVelocity(IntPtr aObjHandle, float aVelocityX, float aVelocityY, float aVelocityZ);
        public void set3dListenerVelocity(float aVelocityX, float aVelocityY, float aVelocityZ)
        {
            Soloud_set3dListenerVelocity(objhandle, aVelocityX, aVelocityY, aVelocityZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourceParametersEx(IntPtr aObjHandle, uint aVoiceHandle, float aPosX, float aPosY, float aPosZ, float aVelocityX, float aVelocityY, float aVelocityZ);
        public void set3dSourceParameters(uint aVoiceHandle, float aPosX, float aPosY, float aPosZ, float aVelocityX = 0.0f, float aVelocityY = 0.0f, float aVelocityZ = 0.0f)
        {
            Soloud_set3dSourceParametersEx(objhandle, aVoiceHandle, aPosX, aPosY, aPosZ, aVelocityX, aVelocityY, aVelocityZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourcePosition(IntPtr aObjHandle, uint aVoiceHandle, float aPosX, float aPosY, float aPosZ);
        public void set3dSourcePosition(uint aVoiceHandle, float aPosX, float aPosY, float aPosZ)
        {
            Soloud_set3dSourcePosition(objhandle, aVoiceHandle, aPosX, aPosY, aPosZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourceVelocity(IntPtr aObjHandle, uint aVoiceHandle, float aVelocityX, float aVelocityY, float aVelocityZ);
        public void set3dSourceVelocity(uint aVoiceHandle, float aVelocityX, float aVelocityY, float aVelocityZ)
        {
            Soloud_set3dSourceVelocity(objhandle, aVoiceHandle, aVelocityX, aVelocityY, aVelocityZ);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourceMinMaxDistance(IntPtr aObjHandle, uint aVoiceHandle, float aMinDistance, float aMaxDistance);
        public void set3dSourceMinMaxDistance(uint aVoiceHandle, float aMinDistance, float aMaxDistance)
        {
            Soloud_set3dSourceMinMaxDistance(objhandle, aVoiceHandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourceAttenuation(IntPtr aObjHandle, uint aVoiceHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dSourceAttenuation(uint aVoiceHandle, uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Soloud_set3dSourceAttenuation(objhandle, aVoiceHandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_set3dSourceDopplerFactor(IntPtr aObjHandle, uint aVoiceHandle, float aDopplerFactor);
        public void set3dSourceDopplerFactor(uint aVoiceHandle, float aDopplerFactor)
        {
            Soloud_set3dSourceDopplerFactor(objhandle, aVoiceHandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_mix(IntPtr aObjHandle, float[] aBuffer, uint aSamples);
        public void mix(float[] aBuffer, uint aSamples)
        {
            Soloud_mix(objhandle, aBuffer, aSamples);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_mixSigned16(IntPtr aObjHandle, IntPtr aBuffer, uint aSamples);
        public void mixSigned16(IntPtr aBuffer, uint aSamples)
        {
            Soloud_mixSigned16(objhandle, aBuffer, aSamples);
        }
    }

    public class AudioAttenuator : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr AudioAttenuator_create();
        public AudioAttenuator()
        {
            objhandle = AudioAttenuator_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr AudioAttenuator_destroy(IntPtr aObjHandle);
        ~AudioAttenuator()
        {
            AudioAttenuator_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float AudioAttenuator_attenuate(IntPtr aObjHandle, float aDistance, float aMinDistance, float aMaxDistance, float aRolloffFactor);
        public float attenuate(float aDistance, float aMinDistance, float aMaxDistance, float aRolloffFactor)
        {
            return AudioAttenuator_attenuate(objhandle, aDistance, aMinDistance, aMaxDistance, aRolloffFactor);
        }
    }

    public class BiquadResonantFilter : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        public const int SAMPLERATE = 1;
        public const int FREQUENCY = 2;
        public const int RESONANCE = 3;
        public const int BANDPASS = 3;
        public const int LOWPASS = 1;
        public const int WET = 0;
        public const int HIGHPASS = 2;
        public const int NONE = 0;

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BiquadResonantFilter_create();
        public BiquadResonantFilter()
        {
            objhandle = BiquadResonantFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BiquadResonantFilter_destroy(IntPtr aObjHandle);
        ~BiquadResonantFilter()
        {
            BiquadResonantFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int BiquadResonantFilter_setParams(IntPtr aObjHandle, int aType, float aSampleRate, float aFrequency, float aResonance);
        public int setParams(int aType, float aSampleRate, float aFrequency, float aResonance)
        {
            return BiquadResonantFilter_setParams(objhandle, aType, aSampleRate, aFrequency, aResonance);
        }
    }

    public class LofiFilter : SoloudObject
    {
        public const int SAMPLERATE = 1;
        public const int BITDEPTH = 2;
        public const int WET = 0;

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LofiFilter_create();
        public LofiFilter()
        {
            objhandle = LofiFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LofiFilter_destroy(IntPtr aObjHandle);
        ~LofiFilter()
        {
            LofiFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LofiFilter_setParams(IntPtr aObjHandle, float aSampleRate, float aBitdepth);
        public int setParams(float aSampleRate, float aBitdepth)
        {
            return LofiFilter_setParams(objhandle, aSampleRate, aBitdepth);
        }
    }

    public class Bus : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Bus_create();
        public Bus()
        {
            objhandle = Bus_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Bus_destroy(IntPtr aObjHandle);
        ~Bus()
        {
            Bus_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Bus_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Bus_playEx(IntPtr aObjHandle, IntPtr aSound, float aVolume, float aPan, int aPaused);
        public uint play(SoloudObject aSound, float aVolume = 1.0f, float aPan = 0.0f, int aPaused = 0)
        {
            return Bus_playEx(objhandle, aSound.objhandle, aVolume, aPan, aPaused);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Bus_playClockedEx(IntPtr aObjHandle, double aSoundTime, IntPtr aSound, float aVolume, float aPan);
        public uint playClocked(double aSoundTime, SoloudObject aSound, float aVolume = 1.0f, float aPan = 0.0f)
        {
            return Bus_playClockedEx(objhandle, aSoundTime, aSound.objhandle, aVolume, aPan);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Bus_play3dEx(IntPtr aObjHandle, IntPtr aSound, float aPosX, float aPosY, float aPosZ, float aVelX, float aVelY, float aVelZ, float aVolume, int aPaused);
        public uint play3d(SoloudObject aSound, float aPosX, float aPosY, float aPosZ, float aVelX = 0.0f, float aVelY = 0.0f, float aVelZ = 0.0f, float aVolume = 1.0f, int aPaused = 0)
        {
            return Bus_play3dEx(objhandle, aSound.objhandle, aPosX, aPosY, aPosZ, aVelX, aVelY, aVelZ, aVolume, aPaused);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Bus_play3dClockedEx(IntPtr aObjHandle, double aSoundTime, IntPtr aSound, float aPosX, float aPosY, float aPosZ, float aVelX, float aVelY, float aVelZ, float aVolume);
        public uint play3dClocked(double aSoundTime, SoloudObject aSound, float aPosX, float aPosY, float aPosZ, float aVelX = 0.0f, float aVelY = 0.0f, float aVelZ = 0.0f, float aVolume = 1.0f)
        {
            return Bus_play3dClockedEx(objhandle, aSoundTime, aSound.objhandle, aPosX, aPosY, aPosZ, aVelX, aVelY, aVelZ, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Bus_setChannels(IntPtr aObjHandle, uint aChannels);
        public int setChannels(uint aChannels)
        {
            return Bus_setChannels(objhandle, aChannels);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_setVisualizationEnable(IntPtr aObjHandle, int aEnable);
        public void setVisualizationEnable(int aEnable)
        {
            Bus_setVisualizationEnable(objhandle, aEnable);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Bus_calcFFT(IntPtr aObjHandle);
        public float[] calcFFT()
        {
            float[] ret = new float[256];
            IntPtr p = Bus_calcFFT(objhandle);

            byte[] buffer = new byte[4];
            for (int i = 0; i < ret.Length; ++i)
            {
                int f_bits = Marshal.ReadInt32(p, i * 4);
                buffer[0] = (byte)((f_bits >> 0) & 0xff);
                buffer[1] = (byte)((f_bits >> 8) & 0xff);
                buffer[2] = (byte)((f_bits >> 16) & 0xff);
                buffer[3] = (byte)((f_bits >> 24) & 0xff);
                ret[i] = BitConverter.ToSingle(buffer, 0);
            }
            return ret;
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Bus_getWave(IntPtr aObjHandle);
        public float[] getWave()
        {
            float[] ret = new float[256];
            IntPtr p = Bus_getWave(objhandle);

            byte[] buffer = new byte[4];
            for (int i = 0; i < ret.Length; ++i)
            {
                int f_bits = Marshal.ReadInt32(p, i * 4);
                buffer[0] = (byte)((f_bits >> 0) & 0xff);
                buffer[1] = (byte)((f_bits >> 8) & 0xff);
                buffer[2] = (byte)((f_bits >> 16) & 0xff);
                buffer[3] = (byte)((f_bits >> 24) & 0xff);
                ret[i] = BitConverter.ToSingle(buffer, 0);
            }
            return ret;
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Bus_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Bus_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Bus_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Bus_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Bus_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Bus_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Bus_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Bus_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Bus_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Bus_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Bus_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Bus_stop(IntPtr aObjHandle);
        public void stop()
        {
            Bus_stop(objhandle);
        }
    }

    public class EchoFilter : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr EchoFilter_create();
        public EchoFilter()
        {
            objhandle = EchoFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr EchoFilter_destroy(IntPtr aObjHandle);
        ~EchoFilter()
        {
            EchoFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int EchoFilter_setParamsEx(IntPtr aObjHandle, float aDelay, float aDecay, float aFilter);
        public int setParams(float aDelay, float aDecay = 0.7f, float aFilter = 0.0f)
        {
            return EchoFilter_setParamsEx(objhandle, aDelay, aDecay, aFilter);
        }
    }

    public class FFTFilter : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FFTFilter_create();
        public FFTFilter()
        {
            objhandle = FFTFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FFTFilter_destroy(IntPtr aObjHandle);
        ~FFTFilter()
        {
            FFTFilter_destroy(objhandle);
        }
    }

    public class BassboostFilter : SoloudObject
    {
        public const int BOOST = 1;
        public const int WET = 0;

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BassboostFilter_create();
        public BassboostFilter()
        {
            objhandle = BassboostFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BassboostFilter_destroy(IntPtr aObjHandle);
        ~BassboostFilter()
        {
            BassboostFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int BassboostFilter_setParams(IntPtr aObjHandle, float aBoost);
        public int setParams(float aBoost)
        {
            return BassboostFilter_setParams(objhandle, aBoost);
        }
    }

    public class Speech : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Speech_create();
        public Speech()
        {
            objhandle = Speech_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Speech_destroy(IntPtr aObjHandle);
        ~Speech()
        {
            Speech_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Speech_setText(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aText);
        public int setText(string aText)
        {
            return Speech_setText(objhandle, aText);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Speech_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Speech_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Speech_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Speech_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Speech_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Speech_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Speech_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Speech_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Speech_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Speech_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Speech_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Speech_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Speech_stop(IntPtr aObjHandle);
        public void stop()
        {
            Speech_stop(objhandle);
        }
    }

    public class Wav : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Wav_create();
        public Wav()
        {
            objhandle = Wav_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Wav_destroy(IntPtr aObjHandle);
        ~Wav()
        {
            Wav_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Wav_load(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int load(string aFilename)
        {
            return Wav_load(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Wav_loadMemEx(IntPtr aObjHandle, IntPtr aMem, uint aLength, int aCopy, int aTakeOwnership);
        public int loadMem(IntPtr aMem, uint aLength, int aCopy = 0, int aTakeOwnership = 1)
        {
            return Wav_loadMemEx(objhandle, aMem, aLength, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Wav_loadFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadFile(SoloudObject aFile)
        {
            return Wav_loadFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double Wav_getLength(IntPtr aObjHandle);
        public double getLength()
        {
            return Wav_getLength(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Wav_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Wav_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Wav_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Wav_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Wav_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Wav_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Wav_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Wav_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Wav_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Wav_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Wav_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Wav_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Wav_stop(IntPtr aObjHandle);
        public void stop()
        {
            Wav_stop(objhandle);
        }
    }

    public class WavStream : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr WavStream_create();
        public WavStream()
        {
            objhandle = WavStream_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr WavStream_destroy(IntPtr aObjHandle);
        ~WavStream()
        {
            WavStream_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WavStream_load(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int load(string aFilename)
        {
            return WavStream_load(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WavStream_loadMemEx(IntPtr aObjHandle, IntPtr aData, uint aDataLen, int aCopy, int aTakeOwnership);
        public int loadMem(IntPtr aData, uint aDataLen, int aCopy = 0, int aTakeOwnership = 1)
        {
            return WavStream_loadMemEx(objhandle, aData, aDataLen, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WavStream_loadToMem(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int loadToMem(string aFilename)
        {
            return WavStream_loadToMem(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WavStream_loadFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadFile(SoloudObject aFile)
        {
            return WavStream_loadFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WavStream_loadFileToMem(IntPtr aObjHandle, IntPtr aFile);
        public int loadFileToMem(SoloudObject aFile)
        {
            return WavStream_loadFileToMem(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double WavStream_getLength(IntPtr aObjHandle);
        public double getLength()
        {
            return WavStream_getLength(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            WavStream_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            WavStream_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            WavStream_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            WavStream_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            WavStream_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            WavStream_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            WavStream_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            WavStream_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            WavStream_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            WavStream_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            WavStream_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            WavStream_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WavStream_stop(IntPtr aObjHandle);
        public void stop()
        {
            WavStream_stop(objhandle);
        }
    }

    public class Prg : SoloudObject
    {

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Prg_create();
        public Prg()
        {
            objhandle = Prg_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Prg_destroy(IntPtr aObjHandle);
        ~Prg()
        {
            Prg_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Prg_rand(IntPtr aObjHandle);
        public uint rand()
        {
            return Prg_rand(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Prg_srand(IntPtr aObjHandle, int aSeed);
        public void srand(int aSeed)
        {
            Prg_srand(objhandle, aSeed);
        }
    }

    public class Sfxr : SoloudObject
    {
        public const int HURT = 4;
        public const int LASER = 1;
        public const int BLIP = 6;
        public const int JUMP = 5;
        public const int POWERUP = 3;
        public const int COIN = 0;
        public const int EXPLOSION = 2;

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sfxr_create();
        public Sfxr()
        {
            objhandle = Sfxr_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sfxr_destroy(IntPtr aObjHandle);
        ~Sfxr()
        {
            Sfxr_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_resetParams(IntPtr aObjHandle);
        public void resetParams()
        {
            Sfxr_resetParams(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Sfxr_loadParams(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int loadParams(string aFilename)
        {
            return Sfxr_loadParams(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Sfxr_loadParamsMemEx(IntPtr aObjHandle, IntPtr aMem, uint aLength, int aCopy, int aTakeOwnership);
        public int loadParamsMem(IntPtr aMem, uint aLength, int aCopy = 0, int aTakeOwnership = 1)
        {
            return Sfxr_loadParamsMemEx(objhandle, aMem, aLength, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Sfxr_loadParamsFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadParamsFile(SoloudObject aFile)
        {
            return Sfxr_loadParamsFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Sfxr_loadPreset(IntPtr aObjHandle, int aPresetNo, int aRandSeed);
        public int loadPreset(int aPresetNo, int aRandSeed)
        {
            return Sfxr_loadPreset(objhandle, aPresetNo, aRandSeed);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Sfxr_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Sfxr_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Sfxr_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Sfxr_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Sfxr_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Sfxr_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Sfxr_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Sfxr_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Sfxr_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Sfxr_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Sfxr_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Sfxr_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sfxr_stop(IntPtr aObjHandle);
        public void stop()
        {
            Sfxr_stop(objhandle);
        }
    }

    public class FlangerFilter : SoloudObject
    {
        public const int FREQ = 2;
        public const int DELAY = 1;
        public const int WET = 0;

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FlangerFilter_create();
        public FlangerFilter()
        {
            objhandle = FlangerFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FlangerFilter_destroy(IntPtr aObjHandle);
        ~FlangerFilter()
        {
            FlangerFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int FlangerFilter_setParams(IntPtr aObjHandle, float aDelay, float aFreq);
        public int setParams(float aDelay, float aFreq)
        {
            return FlangerFilter_setParams(objhandle, aDelay, aFreq);
        }
    }

    public class DCRemovalFilter : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DCRemovalFilter_create();
        public DCRemovalFilter()
        {
            objhandle = DCRemovalFilter_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DCRemovalFilter_destroy(IntPtr aObjHandle);
        ~DCRemovalFilter()
        {
            DCRemovalFilter_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DCRemovalFilter_setParamsEx(IntPtr aObjHandle, float aLength);
        public int setParams(float aLength = 0.1f)
        {
            return DCRemovalFilter_setParamsEx(objhandle, aLength);
        }
    }

    public class Modplug : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Modplug_create();
        public Modplug()
        {
            objhandle = Modplug_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Modplug_destroy(IntPtr aObjHandle);
        ~Modplug()
        {
            Modplug_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Modplug_load(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int load(string aFilename)
        {
            return Modplug_load(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Modplug_loadMemEx(IntPtr aObjHandle, IntPtr aMem, uint aLength, int aCopy, int aTakeOwnership);
        public int loadMem(IntPtr aMem, uint aLength, int aCopy = 0, int aTakeOwnership = 1)
        {
            return Modplug_loadMemEx(objhandle, aMem, aLength, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Modplug_loadFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadFile(SoloudObject aFile)
        {
            return Modplug_loadFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Modplug_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Modplug_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Modplug_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Modplug_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Modplug_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Modplug_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Modplug_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Modplug_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Modplug_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Modplug_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Modplug_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Modplug_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Modplug_stop(IntPtr aObjHandle);
        public void stop()
        {
            Modplug_stop(objhandle);
        }
    }

    public class Monotone : SoloudObject
    {
        public const int SIN = 2;
        public const int SQUARE = 0;
        public const int SAWSIN = 3;
        public const int SAW = 1;

        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Monotone_create();
        public Monotone()
        {
            objhandle = Monotone_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Monotone_destroy(IntPtr aObjHandle);
        ~Monotone()
        {
            Monotone_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Monotone_setParamsEx(IntPtr aObjHandle, int aHardwareChannels, int aWaveform);
        public int setParams(int aHardwareChannels, int aWaveform = SQUARE)
        {
            return Monotone_setParamsEx(objhandle, aHardwareChannels, aWaveform);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Monotone_load(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int load(string aFilename)
        {
            return Monotone_load(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Monotone_loadMemEx(IntPtr aObjHandle, IntPtr aMem, uint aLength, int aCopy, int aTakeOwnership);
        public int loadMem(IntPtr aMem, uint aLength, int aCopy = 0, int aTakeOwnership = 1)
        {
            return Monotone_loadMemEx(objhandle, aMem, aLength, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Monotone_loadFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadFile(SoloudObject aFile)
        {
            return Monotone_loadFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            Monotone_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            Monotone_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            Monotone_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            Monotone_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            Monotone_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            Monotone_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            Monotone_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            Monotone_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            Monotone_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            Monotone_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            Monotone_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            Monotone_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Monotone_stop(IntPtr aObjHandle);
        public void stop()
        {
            Monotone_stop(objhandle);
        }
    }

    public class TedSid : SoloudObject
    {
        private const string LIB = "soloud_x64.dll";

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TedSid_create();
        public TedSid()
        {
            objhandle = TedSid_create();
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TedSid_destroy(IntPtr aObjHandle);
        ~TedSid()
        {
            TedSid_destroy(objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TedSid_load(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int load(string aFilename)
        {
            return TedSid_load(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TedSid_loadToMem(IntPtr aObjHandle, [MarshalAs(UnmanagedType.LPStr)] string aFilename);
        public int loadToMem(string aFilename)
        {
            return TedSid_loadToMem(objhandle, aFilename);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TedSid_loadMemEx(IntPtr aObjHandle, IntPtr aMem, uint aLength, int aCopy, int aTakeOwnership);
        public int loadMem(IntPtr aMem, uint aLength, int aCopy = 0, int aTakeOwnership = 1)
        {
            return TedSid_loadMemEx(objhandle, aMem, aLength, aCopy, aTakeOwnership);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TedSid_loadFileToMem(IntPtr aObjHandle, IntPtr aFile);
        public int loadFileToMem(SoloudObject aFile)
        {
            return TedSid_loadFileToMem(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TedSid_loadFile(IntPtr aObjHandle, IntPtr aFile);
        public int loadFile(SoloudObject aFile)
        {
            return TedSid_loadFile(objhandle, aFile.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_setVolume(IntPtr aObjHandle, float aVolume);
        public void setVolume(float aVolume)
        {
            TedSid_setVolume(objhandle, aVolume);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_setLooping(IntPtr aObjHandle, int aLoop);
        public void setLooping(int aLoop)
        {
            TedSid_setLooping(objhandle, aLoop);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dMinMaxDistance(IntPtr aObjHandle, float aMinDistance, float aMaxDistance);
        public void set3dMinMaxDistance(float aMinDistance, float aMaxDistance)
        {
            TedSid_set3dMinMaxDistance(objhandle, aMinDistance, aMaxDistance);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dAttenuation(IntPtr aObjHandle, uint aAttenuationModel, float aAttenuationRolloffFactor);
        public void set3dAttenuation(uint aAttenuationModel, float aAttenuationRolloffFactor)
        {
            TedSid_set3dAttenuation(objhandle, aAttenuationModel, aAttenuationRolloffFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dDopplerFactor(IntPtr aObjHandle, float aDopplerFactor);
        public void set3dDopplerFactor(float aDopplerFactor)
        {
            TedSid_set3dDopplerFactor(objhandle, aDopplerFactor);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dProcessing(IntPtr aObjHandle, int aDo3dProcessing);
        public void set3dProcessing(int aDo3dProcessing)
        {
            TedSid_set3dProcessing(objhandle, aDo3dProcessing);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dListenerRelative(IntPtr aObjHandle, int aListenerRelative);
        public void set3dListenerRelative(int aListenerRelative)
        {
            TedSid_set3dListenerRelative(objhandle, aListenerRelative);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dDistanceDelay(IntPtr aObjHandle, int aDistanceDelay);
        public void set3dDistanceDelay(int aDistanceDelay)
        {
            TedSid_set3dDistanceDelay(objhandle, aDistanceDelay);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dColliderEx(IntPtr aObjHandle, IntPtr aCollider, int aUserData);
        public void set3dCollider(SoloudObject aCollider, int aUserData = 0)
        {
            TedSid_set3dColliderEx(objhandle, aCollider.objhandle, aUserData);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_set3dAttenuator(IntPtr aObjHandle, IntPtr aAttenuator);
        public void set3dAttenuator(SoloudObject aAttenuator)
        {
            TedSid_set3dAttenuator(objhandle, aAttenuator.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_setInaudibleBehavior(IntPtr aObjHandle, int aMustTick, int aKill);
        public void setInaudibleBehavior(int aMustTick, int aKill)
        {
            TedSid_setInaudibleBehavior(objhandle, aMustTick, aKill);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_setFilter(IntPtr aObjHandle, uint aFilterId, IntPtr aFilter);
        public void setFilter(uint aFilterId, SoloudObject aFilter)
        {
            TedSid_setFilter(objhandle, aFilterId, aFilter.objhandle);
        }

        [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TedSid_stop(IntPtr aObjHandle);
        public void stop()
        {
            TedSid_stop(objhandle);
        }
    }
}
