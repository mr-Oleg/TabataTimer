using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class Exercise
    {
        public int ID { get; private set; }
        public string Title { get; private set; }
        public int TimeBetweenRounds { get; private set; }
        public Image EncryptedImage { get; private set; }

        public Exercise(int id, string title, int timeBetweenRounds, Image encryptedImage)
        {
            ID = id;
            Title = title;
            TimeBetweenRounds = timeBetweenRounds;
            EncryptedImage = encryptedImage;
        }
    }
}
