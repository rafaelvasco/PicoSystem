using System;
using System.Collections.Generic;
using System.ComponentModel;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Graphics
{
    public enum RendererStretchMode
    {
        NoStretch,
        Stretch,
        LetterBox,
        PixelPerfect,
        
    }

    public sealed class PicoGfx : IDisposable
    {
        private IntPtr _ctx;

        private PicoSurface _baseSurface;

        private SDL.SDL_Rect _renderRect;

        private Size _resolution;

        private Font _defaultFont;

        public static PicoGfx Instance { get; private set; }

        public RendererStretchMode StretchMode { get; set; } = RendererStretchMode.PixelPerfect;

        private float _stretchFactorX = 1.0f;
        private float _stretchFactorY = 1.0f;

        public Size Size
        {
            get
            {
                int w, h;
                SDL.SDL_RenderGetLogicalSize(_ctx, out w, out h);

                return new Size(w, h);
            }
        }

        internal IntPtr Context => _ctx;
       
        internal int MaxTextureWidth { get; private set; }

        internal int MaxTextureHeight { get; private set; }

        internal PicoGfx()
        {
            Instance = this;
        }

        internal void Initialize(IntPtr window, int width, int height)
        {
            _ctx = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            SDL.SDL_RendererInfo rendererInfo;

            SDL.SDL_GetRendererInfo(_ctx, out rendererInfo);

            MaxTextureWidth = rendererInfo.max_texture_width;
            MaxTextureHeight = rendererInfo.max_texture_height;
            
            SDL.SDL_RenderSetIntegerScale(_ctx, SDL.SDL_bool.SDL_TRUE);
            SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "0");
            SDL.SDL_SetRenderDrawBlendMode(_ctx, SDL.SDL_BlendMode.SDL_BLENDMODE_NONE);

            SDL.SDL_SetRenderDrawColor(_ctx, 0, 0, 0, 255);
            SDL.SDL_RenderClear(_ctx);

            _resolution = new Size(width, height);

            _baseSurface = new PicoSurface(this, _resolution.Width, _resolution.Height, PicoSurface.AccessType.Target);

            _renderRect = new SDL.SDL_Rect() {x = 0, y = 0, w = _resolution.Width, h = _resolution.Height};
        }

        internal void OnDisplayResize(int displayWidth, int displayHeight)
        {
            switch (StretchMode)
            {
                case RendererStretchMode.NoStretch: return;
                case RendererStretchMode.Stretch:
                {
                    float scaleW = (float)displayWidth / _resolution.Width;
                    float scaleH = (float)displayHeight / _resolution.Height;

                    _stretchFactorX = scaleW;
                    _stretchFactorY = scaleH;
                    _renderRect.x = 0;
                    _renderRect.y = 0;
                    _renderRect.w = displayWidth;
                    _renderRect.h = displayHeight;

                    break;
                }
                case RendererStretchMode.LetterBox:
                {
                    int origW = _resolution.Width;
                    int origH = _resolution.Height;

                    if (displayWidth > origW && displayHeight > origH)
                    {
                        float arOrigin = (float)origW / origH;
                        float arNew = (float)displayWidth / displayHeight;

                        float scaleW = ((float)displayWidth / origW);
                        float scaleH = ((float)displayHeight / origH);


                        if (arNew > arOrigin)
                        {
                            scaleW = scaleH;
                        }
                        else
                        {
                            scaleH = scaleW;
                        }

                        Console.WriteLine($"Scale: {scaleW}, {scaleH}");

                        int marginX = (int) ((displayWidth - origW * scaleW) / 2);
                        int marginY = (int) ((displayHeight - origH * scaleH) / 2);

                        _stretchFactorX = scaleW;
                        _stretchFactorY = scaleH;
                        _renderRect.x = marginX;
                        _renderRect.y = marginY;
                        _renderRect.w = (int) (origW * scaleW);
                        _renderRect.h = (int) (origH * scaleH);
                    }
                    else
                    {
                        _stretchFactorX = 1.0f;
                        _stretchFactorY = 1.0f;
                        _renderRect.x = 0;
                        _renderRect.y = 0;
                        _renderRect.w = displayWidth;
                        _renderRect.h = displayHeight;

                    }

                    break;
                }
                case RendererStretchMode.PixelPerfect:
                {

                    int origW = _resolution.Width;
                    int origH = _resolution.Height;

                    if (displayWidth > origW && displayHeight > origH)
                    {
                        float arOrigin = (float)origW / origH;
                        float arNew = (float)displayWidth / displayHeight;

                        int scaleW = (int) Calculate.Floor((float)displayWidth / origW);
                        int scaleH = (int) Calculate.Floor((float)displayHeight / origH);


                        if (arNew > arOrigin)
                        {
                            scaleW = scaleH;
                        }
                        else
                        {
                            scaleH = scaleW;
                        }

                        Console.WriteLine($"Scale: {scaleW}, {scaleH}");

                        int marginX = (displayWidth - origW * scaleW) / 2;
                        int marginY = (displayHeight - origH * scaleH) / 2;

                        _stretchFactorX = scaleW;
                        _stretchFactorY = scaleH;
                        _renderRect.x = marginX;
                        _renderRect.y = marginY;
                        _renderRect.w = origW * scaleW;
                        _renderRect.h = origH * scaleH;
                    }
                    else
                    {
                        _stretchFactorX = 1.0f;
                        _stretchFactorY = 1.0f;
                        _renderRect.x = 0;
                        _renderRect.y = 0;
                        _renderRect.w = displayWidth;
                        _renderRect.h = displayHeight;

                    }

                    break;
                }
            }
        }

        internal Point TransformMousePosition(int x, int y)
        {
            return new Point((int) ((x - _renderRect.x)/_stretchFactorX),(int) ((y - _renderRect.y)/_stretchFactorY));

        }

        internal void LoadEmbeddedAssets()
        {
            
        }

        public void SetResolution(int width, int height)
        {
            _resolution = new Size(width, height);
        }

        public void Begin()
        {
            SDL.SDL_SetRenderTarget(_ctx, _baseSurface.Texture);
            SDL.SDL_SetRenderDrawColor(_ctx, 0, 0, 0, 255);
            SDL.SDL_RenderClear(_ctx);
        }

        public void End()
        {
            SDL.SDL_SetRenderTarget(_ctx, IntPtr.Zero);

            SDL.SDL_RenderCopy(_ctx, _baseSurface.Texture, IntPtr.Zero, ref _renderRect);

            SDL.SDL_RenderPresent(_ctx);
        }

        public void BeginClip(int x, int y, int w, int h)
        {
            SDL.SDL_Rect clipRect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };

            SDL.SDL_RenderSetClipRect(_ctx, ref clipRect);
        }

        public void EndClip()
        {
            SDL.SDL_RenderSetClipRect(_ctx, IntPtr.Zero);
        }

        public void BeginSurface(PicoSurface surface)
        {
            SDL.SDL_SetRenderTarget(_ctx, surface.Texture);
            SDL.SDL_SetRenderDrawColor(_ctx, 0, 0, 0, 255);
            SDL.SDL_RenderClear(_ctx);
        }

        public void EndSurface()
        {
            SDL.SDL_SetRenderTarget(_ctx, _baseSurface.Texture);
        }

        public void SetColor(ref Color color)
        {
            SDL.SDL_SetRenderDrawColor(_ctx, color.R, color.G, color.B, 255);
        }

        public void FillRect(int x, int y, int w, int h)
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };

            SDL.SDL_RenderFillRect(_ctx, ref rect);
        }

        public void DrawRect(int x, int y, int w, int h)
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };

            SDL.SDL_RenderDrawRect(_ctx, ref rect);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            SDL.SDL_RenderDrawLine(_ctx, x1, y1, x2, y2);
        }

        public void DrawHLine(int x1, int x2, int y)
        {
            SDL.SDL_RenderDrawLine(_ctx, x1, y, x2, y);
        }

        public void DrawEllipse(int x, int y, int rx, int ry)
        {

            if (rx <= 0 || ry <= 0)
            {
                return;
            }

            int h, i, j, k;

            var ok = 0xFFFF;
            var oj = 0xFFFF;
            var oh = 0xFFFF;
            var oi = 0xFFFF;

            int ix, iy, xmi, xpi, xmj, xpj, ymi, ypi, xmk, xpk, ymh, yph;

            int ymj, ypj, xmh, xph, ymk, ypk;

            SDL.SDL_Point[] points = new SDL.SDL_Point[4];
            

            if (rx > ry)
            {
                ix = 0;
                iy = rx * 64;

                h = (ix + 32) >> 6;
                i = (iy + 32) >> 6;

                while (i > h)
                {
                    h = (ix + 32) >> 6;
                    i = (iy + 32) >> 6;
                    j = (h * ry) / rx;
                    k = (i * ry) / rx;

                    if (((ok != k) && (oj != k)) || ((oj != j) && (ok != j)) || (k != j))
                    {
                        xph = x + h;
                        xmh = x - h;

                        if (k > 0)
                        {
                            ypk = y + k;
                            ymk = y - k;

                            points[0].x = xmh;
                            points[0].y = ypk;

                            points[1].x = xph;
                            points[1].y = ypk;

                            points[2].x = xmh;
                            points[2].y = ymk;

                            points[3].x = xph;
                            points[3].y = ymk;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 4);

                          

                        }
                        else
                        {
                            points[0].x = xmh;
                            points[0].y = y;

                            points[1].x = xph;
                            points[1].y = y;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 2);

                        }

                        ok = k;
                        xpi = x + i;
                        xmi = x - i;

                        if (j > 0)
                        {
                            ypj = y + j;
                            ymj = y - j;

                            points[0].x = xmi;
                            points[0].y = ypj;

                            points[1].x = xpi;
                            points[1].y = ypj;

                            points[2].x = xmi;
                            points[2].y = ymj;

                            points[3].x = xpi;
                            points[3].y = ymj;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 4);
                        }
                        else
                        {
                            points[0].x = xmi;
                            points[0].y = y;

                            points[1].x = xpi;
                            points[1].y = y;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 2);

                        }

                        oj = j;
                    }

                    ix += iy / rx;
                    iy -= ix / rx;
                }
            }
            else
            {
                ix = 0;
                iy = ry * 64;

                h = (ix + 32) >> 6;
                i = (iy + 32) >> 6;

                while (i > h)
                {
                    h = (ix + 32) >> 6;
                    i = (iy + 32) >> 6;
                    j = (h * rx) / ry;
                    k = (i * rx) / ry;

                    if (((oi != i) && (oh != i)) || ((oh != h) && (oi != h) && (i != h)))
                    {
                        xmj = x - j;
                        xpj = x + j;

                        if (i > 0)
                        {
                            ypi = y + i;
                            ymi = y - i;

                            points[0].x = xmj;
                            points[0].y = ypi;

                            points[1].x = xpj;
                            points[1].y = ypi;

                            points[2].x = xmj;
                            points[2].y = ymi;

                            points[3].x = xpj;
                            points[3].y = ymi;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 4);

                        }
                        else
                        {

                            points[0].x = xmj;
                            points[0].y = y;

                            points[1].x = xpj;
                            points[1].y = y;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 2);
                        }

                        oi = i;
                        xmk = x - k;
                        xpk = x + k;

                        if (h > 0)
                        {
                            yph = y + h;
                            ymh = y - h;

                            points[0].x = xmk;
                            points[0].y = yph;

                            points[1].x = xpk;
                            points[1].y = yph;

                            points[2].x = xmk;
                            points[2].y = ymh;

                            points[3].x = xpk;
                            points[3].y = ymh;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 4);
                        
                        }
                        else
                        {
                            points[0].x = xmk;
                            points[0].y = y;

                            points[1].x = xpk;
                            points[1].y = y;

                            SDL.SDL_RenderDrawPoints(_ctx, points, 2);
                          

                        }

                        oh = h;
                    }

                    ix += iy / ry;
                    iy -= ix / ry;
                }
            }
        }

        public void FillEllipse(int x, int y, int rx, int ry)
        {

            if (rx <= 0 || ry <= 0)
            {
                return;
            }


            int h, i, j, k;

            var ok = 0xFFFF;
            var oj = 0xFFFF;
            var oh = 0xFFFF;
            var oi = 0xFFFF;

            SDL.SDL_Point[] points = new SDL.SDL_Point[2];

            int ix, iy, xmi, xpi, xmj, xpj, xmh, xph, xmk, xpk;

            if (rx > ry)
            {
                ix = 0;
                iy = rx * 64;

                h = (ix + 32) >> 6;
                i = (iy + 32) >> 6;

                while (i > h)
                {
                    h = (ix + 32) >> 6;
                    i = (iy + 32) >> 6;
                    j = (h * ry) / rx;
                    k = (i * ry) / rx;

                    if ((ok != k) && (oj != k))
                    {
                        xph = x + h;
                        xmh = x - h;

                        if (k > 0)
                        {
                            int xmin = Calculate.Min(xmh, xph);
                            int xmax = Calculate.Max(xmh, xph);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y + k), xmax, (y + k));
                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y - k), xmax, (y - k));
                        }
                        else
                        {
                            int xmin = Calculate.Min(xmh, xph);
                            int xmax = Calculate.Max(xmh, xph);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, y, xmax, y);

                        }
                        ok = k;
                    }
                    if (oj != j && (ok != j) && (k != j))
                    {
                        xmi = x - i;
                        xpi = x + i;

                        if (j > 0)
                        {
                            int xmin = Calculate.Min(xmi, xpi);
                            int xmax = Calculate.Max(xmi, xpi);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y + j), xmax, (y + j));
                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y - j), xmax, (y - j));
                        }
                        else
                        {
                            int xmin = Calculate.Min(xmi, xpi);
                            int xmax = Calculate.Max(xmi, xpi);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, y, xmax, (y - j));

                        }
                        oj = j;
                    }

                    ix += iy / rx;
                    iy -= ix / rx;
                }
            }
            else
            {
                ix = 0;
                iy = ry * 64;

                h = (ix + 32) >> 6;
                i = (iy + 32) >> 6;

                while (i > h)
                {
                    h = (ix + 32) >> 6;
                    i = (iy + 32) >> 6;
                    j = (h * rx) / ry;
                    k = (i * rx) / ry;

                    if ((oi != i) && (oh != i))
                    {
                        xmj = x - j;
                        xpj = x + j;

                        if (i > 0)
                        {
                            int xmin = Calculate.Min(xmj, xpj);
                            int xmax = Calculate.Max(xmj, xpj);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y + i), xmax, (y + i));
                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y - i), xmax, (y - i));

                        }
                        else
                        {
                            int xmin = Calculate.Min(xmj, xpj);
                            int xmax = Calculate.Max(xmj, xpj);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, y, xmax, (y - i));

                        }
                        oi = i;
                    }
                    if ((oh != h) && (oi != h) && (i != h))
                    {
                        xmk = x - k;
                        xpk = x + k;

                        if (h > 0)
                        {
                            int xmin = Calculate.Min(xmk, xpk);
                            int xmax = Calculate.Max(xmk, xpk);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y + h), xmax, (y + h));
                            SDL.SDL_RenderDrawLine(_ctx, xmin, (y - h), xmax, (y - h));
                        }
                        else
                        {
                            int xmin = Calculate.Min(xmk, xpk);
                            int xmax = Calculate.Max(xmk, xpk);

                            SDL.SDL_RenderDrawLine(_ctx, xmin, y, xmax, (y - h));

                        }
                        oh = h;
                    }

                    ix += iy / ry;
                    iy -= ix / ry;
                }
            }

        }

        public void DrawText(int x, int y, Font font, string text)
        {
            PicoSurface surface = font.FontSurface;

            SDL.SDL_Rect destRect = new SDL.SDL_Rect();

            int glyphSize = font.GlyphSize;

            for (int i = 0; i < text.Length; i++)
            {
                SDL.SDL_Rect glyphRect = font[text[i]];

                destRect.x = x + (i * glyphSize);
                destRect.y = y;
                destRect.w = glyphSize;
                destRect.h = glyphSize;

                SDL.SDL_RenderCopy(_ctx, surface.Texture, ref glyphRect, ref destRect);
            }
        }

        /* ========================================================= */

        public void Dispose()
        {
            SDL.SDL_DestroyRenderer(_ctx);
        }
    }
}
