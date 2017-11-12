using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class SkyboxTexture
    {
        public Texture2D Front { get; set; }
        public Texture2D Back { get; set; }
        public Texture2D Left { get; set; }
        public Texture2D Right { get; set; }
        public Texture2D Up { get; set; }
        public Texture2D Down { get; set; }
    }
}
