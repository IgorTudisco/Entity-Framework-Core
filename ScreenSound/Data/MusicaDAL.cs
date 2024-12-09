using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data;

internal class MusicaDAL : DAL<Musica>
{
    public MusicaDAL(ScreenSoundContext context) : base(context)
    {
    }
}
