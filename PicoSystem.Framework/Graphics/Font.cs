using System.Collections.Generic;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Graphics
{
    public class Font : Resource
    {
        private const int CELL_W = 8;
        private const int CELL_H = 8;
        private const int COUNT_CELL_HORZ = 16;

        public Dictionary<int, SDL.SDL_Rect> GlyphQuadMap { get; }
        public PicoSurface FontSurface { get; }
        public int GlyphSize => CELL_W;

        public SDL.SDL_Rect this[char ch]
        {
            get
            {
                SDL.SDL_Rect rect;
                return GlyphQuadMap.TryGetValue(ch, out rect) ? rect : GlyphQuadMap[' '];
            }
        }
       
        internal Font(PicoGfx gfx, Pixmap pixmap)
        {
            GlyphQuadMap = new Dictionary<int, SDL.SDL_Rect>();

            FontSurface = new PicoSurface(gfx, pixmap, PicoSurface.AccessType.Static);

            LoadGlyphMap();
        }

        private void LoadGlyphMap()
        {
            // 128x128 Image
            // Assume chars:  !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~
            // With int values from 32 to 126
            // In an image with 96 cells for each char, with 16 horizontal and 6 vertical 8x8 cells. 

            for (int c = 32; c <= 126; c++)
            {
                int glyphX = (c % COUNT_CELL_HORZ) * CELL_W;
                int glyphY = (c / COUNT_CELL_HORZ) * CELL_W;

                SDL.SDL_Rect glyphRect = new SDL.SDL_Rect()
                {
                    x = glyphX,
                    y = glyphY,
                    w = CELL_W,
                    h = CELL_H
                };

                GlyphQuadMap[c] = glyphRect;
            }

        }

        public override void Dispose()
        {
            FontSurface.Dispose();
        }
    }
}
