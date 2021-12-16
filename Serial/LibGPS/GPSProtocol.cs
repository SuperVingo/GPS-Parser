using System;

#pragma warning disable 414

namespace LibGPS
{
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
            public readonly String Header = "GPRMC";
            private Time TimeToken;
            private char? Status = null;
            private double? Latitude = null;
            private double? Longitude = null;
            private char? Latitude_Direction = null, Longitude_Direction = null;
            private double? SpeedInKnot = null;
            private double? AngleInDegree = null;
            private Date DateToken = null;
            private double? Magnetic_Variation = null;
            private char? Magnetic_Variation_Direction = null;
            private int? CheckSum = null;

            // Constructor
            public GPRMC(Time TimeToken, char? Status, double? Latitude, double? Longitude,
                char? Latitude_Direction, char? Longitude_Direction,
                double? SpeedInKnot, double? AngleInDegree, Date DateToken,
                double? Magnetic_Variation, char? Magnetic_Variation_Direction, int? CheckSum)
            {
                if (TimeToken != null)
                    this.TimeToken = new Time(TimeToken);
                else
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
            public void SetLatitude(double? Latitude, char? LatitudeDirection = 'N')
            {
                this.Latitude = Latitude;
                this.Latitude_Direction = LatitudeDirection;
            }
            public void SetLongitude(double? Longitude, char? LongitudeDirection = 'E')
            {
                this.Longitude = Longitude;
                this.Latitude_Direction = LongitudeDirection;
            }
            public void SetLatitudeDirection(char? LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
            public void SetLongitudeDirection(char? LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }

            // Getter
            public Time GetTimeToken() { return TimeToken; }
            public char? GetStatus() { return Status; }
            public double? GetLatitude() { return Latitude; }
            public double? GetLongitude() { return Longitude; }
            public double? GetLatitudeCalculator() 
            { 
                if(Latitude == null)
                    return null;

                String[] lat = (Latitude / 100.0f).ToString().Split('.');
                try
                {
                    string[] temp = (Convert.ToDouble(lat[1]) / 60.0f).ToString().Split('.');
                    string real = temp[0] + temp[1];
                    return Convert.ToDouble(lat[0] + "." + real);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            public double? GetLongitudeCalculator() 
            {
                if (Longitude == null)
                    return null;

                String[] lat =  (Longitude / 100.0f).ToString().Split('.');
                try
                {
                    string[] temp = (Convert.ToDouble(lat[1]) / 60.0f).ToString().Split('.');
                    string real = temp[0] + temp[1];
                    return Convert.ToDouble(lat[0] + "." + real);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            public char? GetLatitudeDirection() { return Latitude_Direction; }
            public char? GetLongitudeDirection() { return Longitude_Direction; }
            public double? GetSpeedInKnots() { return SpeedInKnot; }
            public double? GetSpeedInMileh() { return SpeedInKnot * 1.151f; }
            public double? GetSpeedInKmh() { return SpeedInKnot * 1.852f; }
            public double? GetAngleInDegree() { return AngleInDegree; }
            public Date GetDateToken() { return DateToken; }
            public double? GetMagneticVariation() { return Magnetic_Variation; }
            public char? GetMagneticVariationDirection() { return Magnetic_Variation_Direction; }
            public int? GetCheckSum() { return CheckSum; }
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
            public readonly String Header = "GPVTG";
            private double? TrueTrack = null, MagneticTrack = null, SpeedInKnots = null, SpeedInKmh = null;
            private char? TrueTrack_Direction = null, MagneticTrack_Direction = null, SpeedInKnots_Unit = null, SpeedInKmh_Unit = null;
            private int? CheckSum = null;

            // Constructor
            public GPVTG(double? TrueTrack, double? MagneticTrack, double? SpeedInKnots, double? SpeedInKmh, int? CheckSum)
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

            public GPVTG(double? TrueTrack, double? MagneticTrack, double? SpeedInKnots, double? SpeedInKmh,
                char? TrueTrack_Direction, char? MagneticTrack_Direction, char? SpeedInKnots_Unit, char? SpeedInKmh_Unit, int? CheckSum)
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
            public void SetTrueTrack(double? TrueTrack, char? TrueTrack_Direction)
            {
                this.TrueTrack = TrueTrack;
                this.TrueTrack_Direction = TrueTrack_Direction;
            }
            public void SetMagneticTrack(double? MagneticTrack, char? MagneticTrack_Direction)
            {
                this.MagneticTrack = MagneticTrack;
                this.MagneticTrack_Direction = MagneticTrack_Direction;
            }
            public void SetSpeedInKnots(double? SpeedInKnots) { this.SpeedInKnots = SpeedInKnots; }
            public void SetSpeedInKmh(double? SpeedInKmh) { this.SpeedInKmh = SpeedInKmh; }

            // Getter
            public double? GetTrueTrack() { return TrueTrack; }
            public double? GetMagneticTrack() { return MagneticTrack; }
            public double? GetSpeedInKnots() { return SpeedInKnots; }
            public double? GetSpeedInKmh() { return SpeedInKmh; }
            public char? GetTrueTrack_Direction() { return TrueTrack_Direction; }
            public char? GetMagneticTrack_Direction() { return MagneticTrack_Direction; }
            public char? GetSpeedInKnots_Unit() { return SpeedInKnots_Unit; }
            public char? GetSpeedInKmh_Unit() { return SpeedInKmh_Unit; }
            public int? GetCheckSum() { return CheckSum; }
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
            public readonly String Header = "GPGGA";
            private Time TimeToken = null;
            private double? Latitude = null;
            private double? Longitude = null;
            private char? Latitude_Direction = null, Longitude_Direction = null;
            private byte? Quality = null;
            private byte? Satellites = null;
            private double? HDOP = null, Horizontal_Sea_Level = null, Horizontal_WGS84 = null;
            private char? Horizontal_Sea_Level_Unit = null, Horizontal_WGS84_Unit = null;
            private double? DGPS_Update_Time = null;
            private int? DGPS_ID = null;
            private int? CheckSum = null;

            // Constructor
            public GPGGA(Time TimeToken, double? Latitude, double? Longitude, char? Latitude_Direction, char? Longitude_Direction,
                byte? FixQuality, byte? Satellites, double? HDOP, double? Horizontal_Sea_Level, double? Horizontal_WGS84,
                char? Horizontal_Sea_Level_Unit, char? Horizontal_WGS84_Unit, double? DGPS_Update_Time, int? DGPS_ID, int? CheckSum)
            {
                if (TimeToken != null)
                    this.TimeToken = new Time(TimeToken);
                else
                    this.TimeToken = TimeToken;
                this.Latitude = Latitude;
                this.Longitude = Longitude;
                this.Latitude_Direction = Latitude_Direction;
                this.Longitude_Direction = Longitude_Direction;
                this.Quality = FixQuality;
                this.Satellites = Satellites;
                this.HDOP = HDOP;
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
            public void SetLatitude(double? Latitude, char? LatitudeDirection = 'N')
            {
                this.Latitude = Latitude;
                this.Latitude_Direction = LatitudeDirection;
            }
            public void SetLongitude(double? Longitude, char? LongitudeDirection = 'E')
            {
                this.Longitude = Longitude;
                this.Latitude_Direction = LongitudeDirection;
            }
            public void SetLatitudeDirection(char? LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
            public void SetLongitudeDirection(char? LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }

            // Getter
            public Time GetTimeToken() { return TimeToken; }
            public double? GetLatitude() { return Latitude; }
            public double? GetLongitude() { return Longitude; }
            public char? GetLatitudeDirection() { return Latitude_Direction; }
            public char? GetLongitudeDirection() { return Longitude_Direction; }
            public byte? GetFixQuality() { return Quality; }
            public byte? GetSatellites() { return Satellites; }
            public double? GetHDOP() { return HDOP; }
            public double? GetHorizontalSeaLevel() { return Horizontal_Sea_Level; }
            public double? GetHorizontalWGS84() { return Horizontal_WGS84; }
            public char? GetHorizontalSeaLevelUnit() { return Horizontal_Sea_Level_Unit; }
            public char? GetHorizontalWGS84Unit() { return Horizontal_WGS84_Unit; }
            public double? GetDGPSUpdateTime() { return DGPS_Update_Time; }
            public int? GetDGPSID() { return DGPS_ID; }
            public int? GetCheckSum() { return CheckSum; }
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
            public readonly String Header = "GPGSA";
            private char? AutoSelection = null;
            private byte? nDFix = null;
            private byte?[] usedSatellites = new byte?[12] { null, null, null, null, null, null, null, null, null, null, null, null};
            private double? PDOP = null, HDOP = null, VDOP = null;
            private int? CheckSum = null;

            //Constructor
            public GPGSA(char? AutoSelection, byte? nDFix, byte?[] usedSatellites, double? PDOP, double? HDOP, double? VDOP, int? CheckSum)
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
            public void SetAutoSelection(char? AutoSelection) { this.AutoSelection = AutoSelection; }
            public void SetnDFix(byte? nDFix) { this.nDFix = nDFix; }
            public void SetUsedSatellites(byte?[] usedSatellites) { this.usedSatellites = usedSatellites; }
            public void SetPDOP(double? PDOP) { this.PDOP = PDOP; }
            public void SetHDOP(double? HDOP) { this.HDOP = HDOP; }
            public void SetVDOP(double? VDOP) { this.VDOP = VDOP; }

            // Getter 
            public char? GetAutoSelection() { return AutoSelection; }
            public byte? GetnDFix() { return nDFix; }
            public byte?[] GetUsedSatellites() { return usedSatellites; }
            public double? GetPDOP() { return PDOP; }
            public double? GetHDOP() { return HDOP; }
            public double? GetVDOP() { return VDOP; }
            public int? GetCheckSum() { return CheckSum; }
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
                private int? SatelliteID = null;
                private int? Elevation = null;
                private int? Azimuth = null;
                private int? SNR = null;

                // Constructor
                public Satellite(int? SatelliteID, int? Elevation, int? Azimuth, int? SNR)
                {
                    this.SatelliteID = SatelliteID;
                    this.Elevation = Elevation;
                    this.Azimuth = Azimuth;
                    this.SNR = SNR;
                }

                // Setter
                public void SetSatelliteID(int? SatelliteID) { this.SatelliteID = SatelliteID; }
                public void SetElevation(int? Elevation) { this.Elevation = Elevation; }
                public void SetAzimuth(int? Azimuth) { this.Azimuth = Azimuth; }
                public void SetSNR(int? SNR) { this.SNR = SNR; }

                // Getter
                public int? GetSatelliteID() { return SatelliteID; }
                public int? GetElevation() { return Elevation; }
                public int? GetAzimuth() { return Azimuth; }
                public int? GetSNR() { return SNR; }
                public int?[] GetSatellite() { return new int?[4] { SatelliteID, Elevation, Azimuth, SNR }; }
            }

            // Field
            public readonly String Header = "GPGSV";
            private byte? AllSentence = null;
            private byte? SentenceIndex = null;
            private byte? NumOfSatellites = null;
            private Satellite[] Satellites;
            private int? CheckSum = null;

            // Constructor
            public GPGSV(byte? AllSentence, byte? SentenceIndex, byte? NumOfSatellites, int? CheckSum)
            {
                this.AllSentence = AllSentence;
                this.SentenceIndex = SentenceIndex;
                this.NumOfSatellites = NumOfSatellites;
                this.Satellites = null;
                this.CheckSum = CheckSum;
            }

            public GPGSV(byte? AllSentence, byte? SentenceIndex, byte? NumOfSatellites,
                int? SatelliteID_1, int? Elevation_1, int? Azimuth_1, int? SNR_1, int? CheckSum)
            {
                this.AllSentence = AllSentence;
                this.SentenceIndex = SentenceIndex;
                this.NumOfSatellites = NumOfSatellites;
                this.Satellites = new Satellite[1];
                this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
                this.CheckSum = CheckSum;
            }

            public GPGSV(byte? AllSentence, byte? SentenceIndex, byte? NumOfSatellites,
                int? SatelliteID_1, int? Elevation_1, int? Azimuth_1, int? SNR_1,
                int? SatelliteID_2, int? Elevation_2, int? Azimuth_2, int? SNR_2, int? CheckSum)
            {
                this.AllSentence = AllSentence;
                this.SentenceIndex = SentenceIndex;
                this.NumOfSatellites = NumOfSatellites;
                this.Satellites = new Satellite[2];
                this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
                this.Satellites[1] = new Satellite(SatelliteID_2, Elevation_2, Azimuth_2, SNR_2);
                this.CheckSum = CheckSum;
            }

            public GPGSV(byte? AllSentence, byte? SentenceIndex, byte? NumOfSatellites,
                int? SatelliteID_1, int? Elevation_1, int? Azimuth_1, int? SNR_1,
                int? SatelliteID_2, int? Elevation_2, int? Azimuth_2, int? SNR_2,
                int? SatelliteID_3, int? Elevation_3, int? Azimuth_3, int? SNR_3, int? CheckSum)
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

            public GPGSV(byte? AllSentence, byte? SentenceIndex, byte? NumOfSatellites,
                int? SatelliteID_1, int? Elevation_1, int? Azimuth_1, int? SNR_1,
                int? SatelliteID_2, int? Elevation_2, int? Azimuth_2, int? SNR_2,
                int? SatelliteID_3, int? Elevation_3, int? Azimuth_3, int? SNR_3,
                int? SatelliteID_4, int? Elevation_4, int? Azimuth_4, int? SNR_4, int? CheckSum)
            {
                this.AllSentence = AllSentence;
                this.SentenceIndex = SentenceIndex;
                this.NumOfSatellites = NumOfSatellites;
                this.Satellites = new Satellite[4];
                this.Satellites[0] = new Satellite(SatelliteID_1, Elevation_1, Azimuth_1, SNR_1);
                this.Satellites[1] = new Satellite(SatelliteID_2, Elevation_2, Azimuth_2, SNR_2);
                this.Satellites[2] = new Satellite(SatelliteID_3, Elevation_3, Azimuth_3, SNR_3);
                this.Satellites[3] = new Satellite(SatelliteID_4, Elevation_4, Azimuth_4, SNR_4);
                this.CheckSum = CheckSum;
            }

            // Setter
            public void SetAllSentence(byte? AllSentence) { this.AllSentence = AllSentence; }
            public void SetSentenceIndex(byte? SentenceIndex) { this.SentenceIndex = SentenceIndex; }
            public void SetNumOfSatellites(byte? NumOfSatellites) { this.NumOfSatellites = NumOfSatellites; }

            // Getter
            public byte? GetAllSentence() { return AllSentence; }
            public byte? GetSentenceIndex() { return SentenceIndex; }
            public byte? GetNumOfSatellites() { return NumOfSatellites; }
            public byte GetLengthOfSatellite() 
            {
                if (Satellites != null)
                    return (byte)Satellites.Length;
                else
                    return 0;
            }
            public int?[][] GetSatellites()
            {
                if (GetLengthOfSatellite() <= 0)
                    return null;

                int?[][] bSatellites = new int?[GetLengthOfSatellite()][];
                for (int i = 0; i < GetLengthOfSatellite(); i++)
                    bSatellites[i] = Satellites[i].GetSatellite();
                return bSatellites;
            }
            public int? GetCheckSum() { return CheckSum; }
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
            public readonly String Header = "GPGLL";
            private double? Latitude = null;
            private double? Longitude = null;
            private char? Latitude_Direction = null, Longitude_Direction = null;
            private Time TimeToken = null;
            private char? Status = null;
            private int? CheckSum = null;

            // Constructor
            public GPGLL(double? Latitude, double? Longitude, char? Latitude_Direction, char? Longitude_Direction,
                Time TimeToken, char? Status, int? CheckSum)
            {
                this.Latitude = Latitude;
                this.Longitude = Longitude;
                this.Latitude_Direction = Latitude_Direction;
                this.Longitude_Direction = Longitude_Direction;
                if (this.TimeToken != null)
                    this.TimeToken = new Time(TimeToken);
                else
                    this.TimeToken = TimeToken;
                this.Status = Status;
                this.CheckSum = CheckSum;
            }

            // Setter
            public void SetLatitude(double? Latitude, char? LatitudeDirection = 'N')
            {
                this.Latitude = Latitude;
                this.Latitude_Direction = LatitudeDirection;
            }
            public void SetLongitude(double? Longitude, char? LongitudeDirection = 'E')
            {
                this.Longitude = Longitude;
                this.Latitude_Direction = LongitudeDirection;
            }
            public void SetLatitudeDirection(char? LatitudeDirection) { this.Latitude_Direction = LatitudeDirection; }
            public void SetLongitudeDirection(char? LongitudeDirection) { this.Longitude_Direction = LongitudeDirection; }
            public void SetTimeToken(Time TimeToken) { this.TimeToken = new Time(TimeToken); }
            public void SetStatus(char? Status) { this.Status = Status; }

            // Getter
            public double? GetLatitude() { return Latitude; }
            public double? GetLongitude() { return Longitude; }
            public char? GetLatitudeDirection() { return Latitude_Direction; }
            public char? GetLongitudeDirection() { return Longitude_Direction; }
            public Time GetTimeToken() { return TimeToken; }
            public char? GetStatus() { return Status; }
            public int? GetCheckSum() { return CheckSum; }
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
            public String GetAttributes() { return Hour.ToString() + "h " + Min.ToString() + "m " + Sec.ToString() + "s"; }

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

                if (temp < 0)
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

            private readonly byte[] DayOfMonth = new byte[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

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
            public String GetAttributes() { return "20" + Year.ToString() + "." + Month.ToString() + "." + Day.ToString(); }

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

                while (true)
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
                    while (true)
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
}
#pragma warning restore 414