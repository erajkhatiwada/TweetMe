using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class Picture
    {
        public int PictureId { get; set; }
        public int UserId { get; set; }
        public byte[] PictureBinary { get; set; }

        public User User { get; set; }
    }
}
