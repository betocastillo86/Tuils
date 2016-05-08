using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Media
{
    public enum EXIFPictureOrientationType : byte
    {
            /// <summary>
            /// Unknown orientation
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Normal orientation
            /// </summary>
            TopLeft = 1,

            /// <summary>
            /// Normal mirrored orientation
            /// </summary>
            TopRight = 2,

            /// <summary>
            /// 180° Rotated orientation
            /// </summary>
            BottomRight = 3,

            /// <summary>
            /// 180° Rotated mirrored orientation
            /// </summary>
            BottomLeft = 4,

            /// <summary>
            /// 90° Rotated mirrored orientation
            /// </summary>
            LeftTop = 5,

            /// <summary>
            /// 270° Rotated orientation
            /// </summary>
            RightTop = 6,

            /// <summary>
            /// 270° Rotated mirrored orientation
            /// </summary>
            RightBottom = 7,

            /// <summary>
            /// 90° Rotated orientation
            /// </summary>
            LeftBottom = 8
    }
}
