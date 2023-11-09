﻿using System.Drawing;
using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class ImageTaskBody : Body
    {
        public Bitmap Image { get; set; }        
        public override object Clone()
        {
            return new ImageTaskBody
            {
                Text = Text,
                Image = (Bitmap)Image?.Clone()
            };
        }
    }
}
