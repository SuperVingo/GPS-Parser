using System;

#pragma warning disable 414

namespace GPSProtocol
{
    /*  GPGGA
     *  
     *  Field:
     *      Header : Header of GPGGA
     *      TimeToken : Time Token based on UTC Time
     *      Status : Responsibility about GPS(A = Active, V = Void)
     *      Latitude, Longitude : Latitude, Longitude. Basically Set My Home Position.
     *      Latitude_Direction, Longitude_Direction : Lat, Lng Direction. In Korea (N, E)
     *      SpeedInKnot : Speed In Knots Unit
     *      AngleInDegree : Direction of movement. North is 0 Degree
     *      DateToken : Date
     *      Magnetic_Variation, Magnetic_Variation_Direction : Magnetic Variation
     *      CheckSum : CheckSum, XOR between $ and *
     */
    class GPRMC
    {
        // Field
        private readonly String Header = "GPVTG";
        private Time TimeToken;
        private char Status;
        private double Latitude = 37.46663683333333f;
        private double Longitude = 126.85370683333333f;
        private char Latitude_Direction = 'N', Longitude_Direction = 'E';
        private double SpeedInKnot;
        private double AngleInDegree;
        private Date DateToken;
        private double Magnetic_Variation;
        private char Magnetic_Variation_Direction;
        private int CheckSum;

        // Constructor
        public GPRMC(Time TimeToken, char Status, double Latitude, double Longitude, 
            char Latitude_Direction, char Longitude_Direction, 
            double SpeedInKnot, double AngleInDegree, Date DateToken,
            double Magnetic_Variation, char Magnetic_Variation_Direction, int CheckSum)
        {
            this.TimeToken = TimeToken;
            this.Status = Status;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Latitude_Direction = Latitude_Direction;
            this.Longitude_Direction = Longitude_Direction;
            this.SpeedInKnot = SpeedInKnot;
            this.AngleInDegree = AngleInDegree;
            this.DateToken = DateToken;
            this.Magnetic_Variation = Magnetic_Variation;
            this.Magnetic_Variation_Direction = Magnetic_Variation_Direction;
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetTimeToken(Time TimeToken) { this.TimeToken = new Time(TimeToken); }
        public void SetLatitude(double Latitude, char LatitudeDirection = 'N')
        {
            this.Latitude = Latitude;
            this.Latitude_Direction = LatitudeDirection;
        }
        public void SetLongitude(double Longitude, char LongitudeDirection = 'E')
        {
            this.Longitude = Longitude;
            this.Latitude_Direction = LongitudeDirection;
        }
        public void SetLatitudeDirection(char LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
        public void SetLongitudeDirection(char LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }

        // Getter
        public Time GetTimeToken() { return TimeToken; }
        public double GetLatitude() { return Latitude; }
        public double GetLongitude() { return Longitude; }
        public char GetLatitudeDirection() { return Latitude_Direction; }
        public char GetLongitudeDirection() { return Longitude_Direction; }
        public double GetSpeedInKnots() { return SpeedInKnot; }
        public double GetSpeedInMileh() { return SpeedInKnot * 1.151f; }
        public double GetSpeedInKmh() { return SpeedInKnot * 1.852f; }
        public double GeAngleInDegree() { return AngleInDegree; }
        public Date GetDateToken() { return DateToken; }
        public double GetMagneticVariation() { return Magnetic_Variation; }
        public char GetMagneticVariationDirection() { return Magnetic_Variation_Direction; }
    }

    /*  GPVTG
     *  
     *  Field:
     *      Header : Header of GPGGA    
     *      TrueTrack, TrueTrack_Direction : 0Degree ~ 360Degree, North is 0 Degree
     *      MagneticTrack, MagneticTrack_Direction : Magnetic Track, DIrection
     *      SpeedInKnots, SpeedInKmh, SpeedInKnots_Unit, SpeedInKmh_Unit : Speed And Unit(Km/h, Knots)
     *      CheckSum : CheckSum, XOR between $ and *
     */
    class GPVTG
    {
        // Field
        private readonly String Header = "GPVTG";
        private double TrueTrack = 0.0f, MagneticTrack = 0.0f, SpeedInKnots = 0.0f, SpeedInKmh = 0.0f;
        private char TrueTrack_Direction = 'T', MagneticTrack_Direction = 'M', SpeedInKnots_Unit = 'N', SpeedInKmh_Unit = 'K';
        private int CheckSum;

        // Constructor
        public GPVTG(double TrueTrack, double MagneticTrack, double SpeedInKnots, double SpeedInKmh, int CheckSum)
        {
            this.TrueTrack = TrueTrack;
            this.MagneticTrack = MagneticTrack;
            this.SpeedInKnots = SpeedInKnots;
            this.SpeedInKmh = SpeedInKmh;
            this.TrueTrack_Direction = 'T';
            this.MagneticTrack_Direction = 'M';
            this.SpeedInKnots_Unit = 'N';
            this.SpeedInKmh_Unit = 'K';
            this.CheckSum = CheckSum;
        }

        public GPVTG(double TrueTrack, double MagneticTrack, double SpeedInKnots, double SpeedInKmh, 
            char TrueTrack_Direction, char MagneticTrack_Direction, char SpeedInKnots_Unit, char SpeedInKmh_Unit, int CheckSum)
        {
            this.TrueTrack = TrueTrack;
            this.MagneticTrack = MagneticTrack;
            this.SpeedInKnots = SpeedInKnots;
            this.SpeedInKmh = SpeedInKmh;
            this.TrueTrack_Direction = TrueTrack_Direction;
            this.MagneticTrack_Direction = MagneticTrack_Direction;
            this.SpeedInKnots_Unit = SpeedInKnots_Unit;
            this.SpeedInKmh_Unit = SpeedInKmh_Unit;
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetTrueTrack(double TrueTrack, char TrueTrack_Direction) 
        { 
            this.TrueTrack = TrueTrack;
            this.TrueTrack_Direction = TrueTrack_Direction;
        }
        public void SetMagneticTrack(double MagneticTrack, char MagneticTrack_Direction) 
        { 
            this.MagneticTrack = MagneticTrack;
            this.MagneticTrack_Direction = MagneticTrack_Direction;
        }
        public void SetSpeedInKnots(double SpeedInKnots) { this.SpeedInKnots = SpeedInKnots; }
        public void SetSpeedInKmh(double SpeedInKmh) { this.SpeedInKmh = SpeedInKmh; }

        // Getter
        public double GetTrueTrack() { return TrueTrack; }
        public double GetMagneticTrack() { return MagneticTrack; }
        public double GetSpeedInKnots() { return SpeedInKnots; }
        public double GetSpeedInKmh() { return SpeedInKmh; }
        public char GetTrueTrack_Direction() { return TrueTrack_Direction; }
        public char GetMagneticTrack_Direction() { return MagneticTrack_Direction; }
        public char GetSpeedInKnots_Unit() { return SpeedInKnots_Unit; }
        public char GetSpeedInKmh_Unit() { return SpeedInKmh_Unit; }
    }

    /*  GPGGA
     *  
     *  Field:
     *      Header : Header of GPGGA
     *      TimeToken : Time Token based on UTC Time
     *      Latitude, Longitude : Latitude, Longitude. Basically Set My Home Position.
     *      Latitude_Direction, Longitude_Direction :  Lat, Lng Direction. In Korea (N, E)
     *      Quality : Fix Quality.
     *          0 = invalid
     *          1 = GPS Fix, It can use
     *          2 ~ 8 = Unused 
     *      Satellites : Number of satellites being tracked
     *      Horizontal : Horizontal dilution of position
     *      Horizontal_Sea_Level : Altitude above mean sea level
     *      Horizontal_Sea_Level_Unit : Unit of Horizontal_Sea_Level
     *      Horizontal_WGS84 : Height of geoid above WGS84 Ellipsoid
     *      Horizontal_WGS84_Unit : Unit of Horizontal_WGS84
     *      DGPS_Update_Time : Time in seconds since last DGPS Update
     *      DGPS_ID : DGPS Station ID
     *      CheckSum : CheckSum, XOR between $ and *
     */
    class GPGGA
    {
        // Field
        private readonly String Header = "GPGGA";
        private Time TimeToken = null;
        private double Latitude = 37.46663683333333f;
        private double Longitude = 126.85370683333333f;
        private char Latitude_Direction = 'N', Longitude_Direction = 'E';
        private byte Quaility = 0;
        private byte Satellites = 0;
        private double Horizontal = 0.0f, Horizontal_Sea_Level = 0.0f, Horizontal_WGS84 = 0.0f;
        private char Horizontal_Sea_Level_Unit = 'M', Horizontal_WGS84_Unit = 'M';
        private double DGPS_Update_Time = 0.0f;
        private int DGPS_ID = 0;
        private int CheckSum = 0;

        // Constructor
        public GPGGA(Time TimeToken, double Latitude, double Longitude, char Latitude_Direction, char Longitude_Direction, 
            byte FixQuaility, byte Satellites, double Horizontal, double Horizontal_Sea_Level, double Horizontal_WGS84,
            char Horizontal_Sea_Level_Unit, char Horizontal_WGS84_Unit, double DGPS_Update_Time, int DGPS_ID, int CheckSum)
        {
            this.TimeToken = new Time(TimeToken);
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Latitude_Direction = Latitude_Direction;
            this.Longitude_Direction = Longitude_Direction;
            this.Quaility = FixQuaility;
            this.Satellites = Satellites;
            this.Horizontal = Horizontal;
            this.Horizontal_Sea_Level = Horizontal_Sea_Level;
            this.Horizontal_WGS84 = Horizontal_WGS84;
            this.Horizontal_Sea_Level_Unit = Horizontal_Sea_Level_Unit;
            this.Horizontal_WGS84_Unit = Horizontal_WGS84_Unit;
            this.DGPS_Update_Time = DGPS_Update_Time;
            this.DGPS_ID = DGPS_ID;
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetTimeToken(Time TimeToken) { this.TimeToken = new Time(TimeToken); }
        public void SetLatitude(double Latitude, char LatitudeDirection = 'N') 
        { 
            this.Latitude = Latitude;
            this.Latitude_Direction = LatitudeDirection;
        }
        public void SetLongitude(double Longitude, char LongitudeDirection = 'E') 
        { 
            this.Longitude = Longitude;
            this.Latitude_Direction = LongitudeDirection;
        }
        public void SetLatitudeDirection(char LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
        public void SetLongitudeDirection(char LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }

        // Getter
        public Time GetTimeToken() { return TimeToken; }
        public double GetLatitude() { return Latitude; }
        public double GetLongitude() { return Longitude; }
        public char GetLatitudeDirection() { return Latitude_Direction; }
        public char GetLongitudeDirection() { return Longitude_Direction; }
        public byte GetFixQuality() { return Quaility; }
        public byte GetSatellites() { return Satellites; }
        public double GetHorizontal() { return Horizontal; }
        public double GetHorizontalSeaLevel() { return Horizontal_Sea_Level; }
        public double GetHorizontalWGS84() { return Horizontal_WGS84; }
        public char GetHorizontalSeaLevelUnit() { return Horizontal_Sea_Level_Unit; }
        public char GetHorizontalWGS84Unit() { return Horizontal_WGS84_Unit; }
        public double GetDGPSUpdateTime() { return DGPS_Update_Time; }
        public int GetDGPSID() { return DGPS_ID; }
    }

    /*  GPVTG
     *  
     *  Field:
     *      Header : Header of GPGSA   
     *      AutoSelection : 2D / 3D Automatic Conversion
     *      nDFix : 1 no fix
     *              2 2D fix
     *              3 3D fix
     *      usedSatellites : Arrays of Satellites used for fix
     *     PDOP, HDOP, VDOP : Error Range
     */
    class GPGSA
    {
        //Field
        private readonly String Header = "GPGSV";
        private char AutoSelection = 'A';
        private byte nDFix = 1;
        private byte[] usedSatellites = new byte[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private double PDOP = 0.0f, HDOP = 0.0f, VDOP = 0.0f;
        private int CheckSum;

        //Constructor
        public GPGSA(char AutoSelection, byte nDFix, byte[] usedSatellites, double PDOP, double HDOP, double VDOP, int CheckSum)
        {
            this.AutoSelection = AutoSelection;
            this.nDFix = nDFix;
            this.usedSatellites = usedSatellites;
            this.PDOP = PDOP;
            this.HDOP = HDOP;
            this.VDOP = VDOP;
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetAutoSelection(char AutoSelection) { this.AutoSelection = AutoSelection; }
        public void SetnDFix(byte nDFix) { this.nDFix = nDFix; }
        public void SetUsedSatellites(byte[] usedSatellites) { this.usedSatellites = usedSatellites; }
        public void SetPDOP(double PDOP) { this.PDOP = PDOP; }
        public void SetHDOP(double HDOP) { this.HDOP = HDOP; }
        public void SetVDOP(double VDOP) { this.VDOP = VDOP; }

        // Getter 
        public char GetAutoSelection() { return AutoSelection; }
        public byte GetnDFix() { return nDFix; }
        public byte[] GetUsedSatellites() { return usedSatellites; }
        public double GetPDOP() { return PDOP; }
        public double GetHDOP() { return HDOP; }
        public double GetVDOP() { return VDOP; }
    }

    /*  GPGSV
     *  
     *  Field:
     *      Header : Header of GPGSV   
     *      AllSentence = Number Of Sentences for full data
     *      SentenceIndex = Index of all sentence
     *      NumOfSatellites = Total Satellites in log
     *      Satellites : Info about Satellites
     *          Field:
     *              SatelliteID : Satellite PRN Number
     *              Elevation : Elevation, Degree
     *              Azimuth : Azimuth, Degree
     *              SNR : Signal-to-noise ratio
     */
    class GPGSV
    {
        private class Satellite
        {
            // Field
            private byte SatelliteID;
            private byte Elevation;
            private byte Azimuth;
            private byte SNR;

            // Constructor
            public Satellite(byte SatelliteID, byte Elevation, byte Azimuth, byte SNR)
            {
                this.SatelliteID = SatelliteID;
                this.Elevation = Elevation;
                this.Azimuth = Azimuth;
                this.SNR = SNR;
            }

            // Setter
            public void SetSatelliteID(byte SatelliteID) { this.SatelliteID = SatelliteID; }
            public void SetElevation(byte Elevation) { this.Elevation = Elevation; }
            public void SetAzimuth(byte Azimuth) { this.Azimuth = Azimuth; }
            public void SetSNR(byte SNR) { this.SNR = SNR; }

            // Getter
            public byte GetSatelliteID() { return SatelliteID; }
            public byte GetElevation() { return Elevation; }
            public byte GetAzimuth() { return Azimuth; }
            public byte GetSNR() { return SNR; }
            public byte[] GetSatellite() { return new byte[4] { SatelliteID, Elevation, Azimuth, SNR }; }
        }

        // Field
        private readonly String Header = "GPGSV";
        private byte AllSentence = 0;
        private byte SentenceIndex = 0;
        private byte NumOfSatellites = 0;
        private Satellite[] Satellites;
        private int CheckSum = 0;

        // Constructor
        public GPGSV(byte AllSentence, byte SentenceIndex, byte NumOfSatellites, int CheckSum)
        {
            this.AllSentence = AllSentence;
            this.SentenceIndex = SentenceIndex;
            this.NumOfSatellites = NumOfSatellites;
            this.Satellites = null;
            this.CheckSum = CheckSum;
        }

        public GPGSV(byte AllSentence, byte SentenceIndex, byte NumOfSatellites,
            byte SatelliteID_1, byte Elevation_1, byte Azimuth_1, byte SNR_1, int CheckSum)
        {
            this.AllSentence = AllSentence;
            this.SentenceIndex = SentenceIndex;
            this.NumOfSatellites = NumOfSatellites;
            this.Satellites = new Satellite[1];
            this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
            this.CheckSum = CheckSum;
        }

        public GPGSV(byte AllSentence, byte SentenceIndex, byte NumOfSatellites,
            byte SatelliteID_1, byte Elevation_1, byte Azimuth_1, byte SNR_1,
            byte SatelliteID_2, byte Elevation_2, byte Azimuth_2, byte SNR_2, int CheckSum)
        {
            this.AllSentence = AllSentence;
            this.SentenceIndex = SentenceIndex;
            this.NumOfSatellites = NumOfSatellites;
            this.Satellites = new Satellite[2];
            this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
            this.Satellites[1] = new Satellite(SatelliteID_2, Elevation_2, Azimuth_2, SNR_2);
            this.CheckSum = CheckSum;
        }

        public GPGSV(byte AllSentence, byte SentenceIndex, byte NumOfSatellites,
            byte SatelliteID_1, byte Elevation_1, byte Azimuth_1, byte SNR_1,
            byte SatelliteID_2, byte Elevation_2, byte Azimuth_2, byte SNR_2,
            byte SatelliteID_3, byte Elevation_3, byte Azimuth_3, byte SNR_3, int CheckSum)
        {
            this.AllSentence = AllSentence;
            this.SentenceIndex = SentenceIndex;
            this.NumOfSatellites = NumOfSatellites;
            this.Satellites = new Satellite[3];
            this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
            this.Satellites[1] = new Satellite(SatelliteID_2, Elevation_2, Azimuth_2, SNR_2);
            this.Satellites[2] = new Satellite(SatelliteID_3, Elevation_3, Azimuth_3, SNR_3);
            this.CheckSum = CheckSum;
        }

        public GPGSV(byte AllSentence, byte SentenceIndex, byte NumOfSatellites,
            byte SatelliteID_1, byte Elevation_1, byte Azimuth_1, byte SNR_1,
            byte SatelliteID_2, byte Elevation_2, byte Azimuth_2, byte SNR_2,
            byte SatelliteID_3, byte Elevation_3, byte Azimuth_3, byte SNR_3,
            byte SatelliteID_4, byte Elevation_4, byte Azimuth_4, byte SNR_4, int CheckSum)
        {
            this.AllSentence = AllSentence;
            this.SentenceIndex = SentenceIndex;
            this.NumOfSatellites = NumOfSatellites;
            this.Satellites = new Satellite[3];
            this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
            this.Satellites[1] = new Satellite(SatelliteID_2, Elevation_2, Azimuth_2, SNR_2);
            this.Satellites[2] = new Satellite(SatelliteID_3, Elevation_3, Azimuth_3, SNR_3);
            this.Satellites[3] = new Satellite(SatelliteID_4, Elevation_4, Azimuth_4, SNR_4);
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetAllSentence(byte AllSentence) { this.AllSentence = AllSentence; }
        public void SetSentenceIndex(byte SentenceIndex) { this.SentenceIndex = SentenceIndex; }
        public void SetNumOfSatellites(byte NumOfSatellites) { this.NumOfSatellites = NumOfSatellites; }

        // Getter
        public byte GetAllSentence() { return AllSentence; }
        public byte GetSentenceIndex() { return SentenceIndex; }
        public byte GetNumOfSatellites() { return NumOfSatellites; }
        public byte GetLengthOfSatellite() { return (byte)Satellites.Length; }
        public byte[][] GetSatellites()
        {
            if (GetLengthOfSatellite() <= 0)
                return null;

            byte[][] bSatellites = new byte[GetLengthOfSatellite()][];
            for (int i = 0; i < GetLengthOfSatellite(); i++)
                bSatellites[i] = Satellites[i].GetSatellite();
            return bSatellites;
        }
    }

    /*  GPGLL
     * 
     *  Field:
     *      Header : Header of GPGLL
     *      Latitude, Longitude : Latitude, Longitude. Basically Set My Home Position.
     *      Latitude_Direction, Longitude_Direction :  Lat, Lng Direction. In Korea (N, E)
     *      TimeToken : Time Token based on UTC Time
     *      Status : Responsibility about GPS(A = Active, V = Void)
     */
    class GPGLL
    {
        // Field
        private readonly String Header = "GPGLL";
        private double Latitude = 37.46663683333333f;
        private double Longitude = 126.85370683333333f;
        private char Latitude_Direction = 'N', Longitude_Direction = 'E';
        private Time TimeToken;
        private char Status = 'V';
        private int CheckSum;

        // Constructor
        public GPGLL(double Latitude, double Longitude, char Latitude_Direction, char Longitude_Direction, 
            Time TimeToken, char Status, int CheckSum)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Latitude_Direction = Latitude_Direction;
            this.Longitude_Direction = Longitude_Direction;
            this.TimeToken = TimeToken;
            this.Status = Status;
            this.CheckSum = CheckSum;
        }

        // Setter
        public void SetLatitude(double Latitude, char LatitudeDirection = 'N')
        {
            this.Latitude = Latitude;
            this.Latitude_Direction = LatitudeDirection;
        }
        public void SetLongitude(double Longitude, char LongitudeDirection = 'E')
        {
            this.Longitude = Longitude;
            this.Latitude_Direction = LongitudeDirection;
        }
        public void SetLatitudeDirection(char LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
        public void SetLongitudeDirection(char LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }
        public void SetTimeToken(Time TimeToken) { this.TimeToken = new Time(TimeToken); }
        public void SetStatus(char Status) { this.Status = Status; }

        // Getter
        public double GetLatitude() { return Latitude; }
        public double GetLongitude() { return Longitude; }
        public char GetLatitudeDirection() { return Latitude_Direction; }
        public char GetLongitudeDirection() { return Longitude_Direction; }
        public Time GetTimeToken() { return TimeToken; }
        public char GetStatus() { return Status; }
    }

    class Time
    {
        // Field
        private byte Hour = 0;
        private byte Min = 0;
        private byte Sec = 0;

        // Constructor
        public Time()
        {

        }

        public Time(Time other)
        {
            this.Hour = other.GetHour();
            this.Min = other.GetMin();
            this.Sec = other.GetSec();
        }

        public Time(byte Hour, byte Min, byte Sec)
        {
            this.Hour = Hour;
            this.Min = Min;
            this.Sec = Sec;
        }

        // Setter
        public void SetHour(byte Hour) { this.Hour = Hour; }
        public void SetMin(byte Min) { this.Min = Min; }
        public void SetSec(byte Sec) { this.Sec = Sec; }

        // Getter
        public byte GetHour() { return Hour; }
        public byte GetMin() { return Min; }
        public byte GetSec() { return Sec; }

        // Calculate Time
        public static Time operator +(Time a, Time b)
        {
            Time temp = new Time(a);

            temp.AddSec(b.GetSec());
            temp.AddMin(b.GetMin());
            temp.AddHour(b.GetHour());

            return temp;
        }

        public static Time operator -(Time a, Time b)
        {
            Time temp = new Time(a);

            temp.SubSec(b.GetSec());
            temp.SubMin(b.GetMin());
            temp.SubHour(b.GetHour());

            return temp;
        }

        public override string ToString()
        {
            return Hour.ToString("D2") + Min.ToString("D2") + Sec.ToString("D2") + ".00";
        }

        private void AddHour(byte Hour)
        {
            this.Hour = (byte)((this.Hour + Hour) % 24);
        }

        private void AddMin(byte Min)
        {
            byte Temp = (byte)(this.Min + Min);
            this.Hour += (byte)(Temp / 60);
            this.Min = (byte)(Temp % 60);
        }

        private void AddSec(byte Sec)
        {
            byte Temp = (byte)(this.Sec + Sec);
            this.Min += (byte)(Temp / 60);
            this.Sec = (byte)(Temp % 60);
        }

        private void SubHour(byte Hour)
        {
            sbyte temp = (sbyte)((sbyte)this.Hour - (sbyte)Hour);

            if (temp < 0)
                temp += 24;

            this.Hour = (byte)temp;
        }

        private void SubMin(byte Min)
        {
            sbyte temp = (sbyte)((sbyte)this.Min - (sbyte)Min);

            if(temp < 0)
            {
                SubHour(1);
                temp += 60;
            }

            this.Min = (byte)temp;
        }

        private void SubSec(byte Sec)
        {
            sbyte temp = (sbyte)((sbyte)this.Sec - (sbyte)Sec);

            if (temp < 0)
            {
                SubMin(1);
                temp += 60;
            }

            this.Sec = (byte)temp;
        }

        // Return Another Time
        Time GetKoreaTimeFromUTC(Time UTC) { return UTC + new Time(9, 0, 0); }
    }

    class Date
    {
        // Field
        private byte Year = 0;
        private byte Month = 0;
        private byte Day = 0;

        private readonly byte[] DayOfMonth = new byte[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        // Constructor
        public Date()
        {

        }

        public Date(Date other)
        {
            this.Year = other.GetYear();
            this.Month = other.GetMonth();
            this.Day = other.GetDay();
        }

        public Date(byte Year, byte Month, byte Day)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        // Setter
        public void SetYear(byte Year) { this.Year = Year; }
        public void SetMonth(byte Month) { this.Month = Month; }
        public void SetDay(byte Day) { this.Day = Day; }

        // Getter
        public byte GetYear() { return Year; }
        public byte GetMonth() { return Month; }
        public byte GetDay() { return Day; }

        // Calculate Time
        public static Date operator +(Date a, Date b)
        {
            Date temp = new Date(a);

            temp.AddDay(b.GetDay());
            temp.AddMonth(b.GetMonth());
            temp.AddYear(b.GetYear());

            return temp;
        }

        public static Date operator -(Date a, Date b)
        {
            Date temp = new Date(a);

            temp.SubDay(b.GetDay());
            temp.SubMonth(b.GetMonth());
            temp.SubYear(b.GetYear());

            return temp;
        }

        public override string ToString()
        {
            return Day.ToString("D2") + Month.ToString("D2") + Year.ToString("D2");
        }

        private void AddYear(byte Year)
        {
            this.Year = (byte)((this.Year + Year) % 100);
        }

        private void AddMonth(byte Month)
        {
            byte Temp = (byte)(this.Month + Month);
            if (Temp % 12 == 0)
            {
                this.Year += (byte)(Temp / 12 - 1);
                this.Month = (byte)(Temp % 12 + 12);
            }
            else
            {
                this.Year += (byte)(Temp / 12);
                this.Month = (byte)(Temp % 12);
            }
        }

        private void AddDay(byte Day)
        {
            byte Temp = (byte)(this.Day + Day);

            while(true)
            {
                if (DayOfMonth[this.Month - 1] > Temp)
                    break;

                Temp -= DayOfMonth[this.Month - 1];
                AddMonth(1);
            }
        }

        private void SubYear(byte Year)
        {
            sbyte Temp = (sbyte)((sbyte)this.Year - (sbyte)Year);

            if (Temp < 0)
                Temp += 100;

            this.Year = (byte)Temp;
        }

        private void SubMonth(byte Month)
        {
            sbyte temp = (sbyte)((sbyte)this.Month - (sbyte)Month);

            if (temp <= 0)
            {
                SubYear(1);
                temp += 12;
            }

            this.Month = (byte)temp;
        }

        private void SubDay(byte Day)
        {
            sbyte temp = (sbyte)((sbyte)this.Day - (sbyte)Day);

            if (temp <= 0)
            {
                while(true)
                {
                    if (0 < temp)
                        break;

                    SubMonth(1);
                    temp += (sbyte)DayOfMonth[this.Month];
                }
            }

            this.Day = (byte)temp;
        }
    }
}