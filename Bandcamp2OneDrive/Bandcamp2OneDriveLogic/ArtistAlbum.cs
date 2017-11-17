using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bandcamp2OneDriveLogic
{
    public class ArtistAlbum
    {
        public string Artist { get; set; }
        public string Album { get; set; }

        public static ArtistAlbum GetArtistAlbum(string fileName)
        {

            string mp3File = Path.GetFileNameWithoutExtension(fileName);

            string artist = mp3File.Split('-')[0].TrimEnd();
            string album = mp3File.Split('-')[1].TrimStart();

            return new ArtistAlbum { Artist = artist, Album = album };
        }
    }
}
