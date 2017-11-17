using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace Bandcamp2OneDriveLogic
{
    

    public class Bandcamp2OneDriveConverter
    {
        private Configs _configs;
        private string _genre;
        private string _inputFile;
        private string _artist;
        private string _album;

        public Bandcamp2OneDriveConverter(Configs configs, string genre, string inputfile, string artist, string album)
        {
            _configs = configs;
            _genre = genre;
            _inputFile = inputfile;
            _artist = artist;
            _album = album;
        }

        public string GetFullOutputPath()
        {
            return Path.Combine(_configs.OutputPathRoot, MapGenre2Path(_genre), _artist, _album);
        }


        public async Task Convert4OneDrive(string outputPath)
        {
            string newPath= Path.Combine(Path.GetDirectoryName(_inputFile), _album);
            string newFileName = newPath + ".zip";
            //rename
            File.Move(_inputFile, newFileName);
            //extract
            ZipFile.ExtractToDirectory(newFileName, Path.Combine(Path.GetDirectoryName(_inputFile), _album));

            foreach (string mp3 in Directory.EnumerateFiles(newPath,"*.mp3"))
            {
                TagLib.File file = TagLib.File.Create(mp3);
                file.Tag.Genres = new string[] { _genre };
                file.Save();
                file.Dispose();
            }

            foreach (string mp3 in Directory.EnumerateFiles(newPath, "*.mp3"))
            {
                string newMp3Name = Path.GetFileName(mp3).Split('-')[2].TrimStart();
                File.Move(mp3, Path.Combine(Path.GetDirectoryName(mp3), newMp3Name));
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            Directory.Move(newPath, outputPath);

        }

        private string MapGenre2Path(string genre)
        {
            switch (genre)
            {
                case ("Electronica"):
                    return "Electronics";
                case ("NeoFolk"):
                    return "Neo Folk";
                case ("Alternative"):
                    return "Rock";
                case ("Fun"):
                    return "fun";
                case ("Neue Musik"):
                    return "Avant Garde&Film Musik& Neue Musik";
                default:
                    return genre;
            }
        }


    }
}

