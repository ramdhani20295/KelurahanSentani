﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani
{
    public class EnumCollection
    {
    }

    public enum Kepercayaan
    {
        Islam, Protestan, Khatolik, Hindu, Budha, Konghuchu
    }

    public enum Kelamin
    {
        Pria, Wanita
    }

    public enum Hubungan
    {
        KepalaKeluarga,Istri,Anak,Ibu,Bapak,Famili
    }

    public enum StatusPerkawinan
    {
        Kawin,Belum
    }

    public enum Kewarganegaraan
    {
        WNI, WNA
    }


    public enum Pendidikan
    {
        SD, SMP, SMA,Diploma, S1, S2, S3
    }

    public enum LevelStruktur
    {
        RT, RW, Kelurahan
    }
    public enum JenisJabatan
    {
        Ketua, Sekertaris
    }
    public enum JenisSurat
    {
        Umum,Kematian,Pindah
    }

    public enum StatusPermohonan
    {
        Menunggu,Selesai,Batal
    }

}