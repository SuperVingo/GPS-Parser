using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGPS
{
    namespace GPSDefinition
    {
        enum GPSParseResult
        {
            GPSPS_OK,
            GPSPS_HeaderNotMatch,
            GPSPS_AttributesConvertError,
            GPSPS_AttributesTooMany,
            GPSPS_AttributesNotEnough
        }

        enum GPSDataState
        {
            GPSDS_OK,
            GPSDS_ERROR,
        }

        static class Definition
        {
            // GPRMC Index
            public readonly static int GPRMC_Header = 0;
            public readonly static int GPRMC_TimeToken = 1;
            public readonly static int GPRMC_Status = 2;
            public readonly static int GPRMC_Latitude = 3;
            public readonly static int GPRMC_Latitude_Direction = 4;
            public readonly static int GPRMC_Longitude = 5;
            public readonly static int GPRMC_Longitude_Direction = 6;
            public readonly static int GPRMC_SpeedInKnot = 7;
            public readonly static int GPRMC_AngleInDegree = 8;
            public readonly static int GPRMC_DateToken = 9;
            public readonly static int GPRMC_Magnetic_Variation = 10;
            public readonly static int GPRMC_Magnetic_Variation_Direction = 11;
            public readonly static int GPRMC_Border = 12;
            public readonly static int GPRMC_CheckSum = 13;

            // GPVTG Index
            public readonly static int GPVTG_Header = 0;
            public readonly static int GPVTG_TrueTrack = 1;
            public readonly static int GPVTG_TrueTrack_Direction = 2;
            public readonly static int GPVTG_MagneticTrack = 3;
            public readonly static int GPVTG_MagneticTrack_Direction = 4;
            public readonly static int GPVTG_SpeedInKnots = 5;
            public readonly static int GPVTG_SpeedInKnots_Unit = 6;
            public readonly static int GPVTG_SpeedInKmh = 7;
            public readonly static int GPVTG_SpeedInKmh_Unit = 8;
            public readonly static int GPVTG_Border = 9;
            public readonly static int GPVTG_CheckSum = 10;

            // GPGGA Index
            public readonly static int GPGGA_Header = 0;
            public readonly static int GPGGA_TimeToken = 1;
            public readonly static int GPGGA_Latitude = 2;
            public readonly static int GPGGA_Latitude_Direction = 3;
            public readonly static int GPGGA_Longitude = 4;
            public readonly static int GPGGA_Longitude_Direction = 5;
            public readonly static int GPGGA_Quaility = 6;
            public readonly static int GPGGA_Satellites = 7;
            public readonly static int GPGGA_HDOP = 8;
            public readonly static int GPGGA_Horizontal_Sea_Level = 9;
            public readonly static int GPGGA_Horizontal_Sea_Level_Unit = 10;
            public readonly static int GPGGA_Horizontal_WGS84 = 11;
            public readonly static int GPGGA_Horizontal_WGS84_Unit = 12;
            public readonly static int GPGGA_DGPS_Update_Time = 13;
            public readonly static int GPGGA_DGPS_ID = 14;
            public readonly static int GPGGA_CheckSum = 15;

            // GPGSA Index
            public readonly static int GPGSA_Header = 0;
            public readonly static int GPGSA_AutoSelection = 1;
            public readonly static int GPGSA_nDFix = 2;
            public readonly static int GPGSA_usedSatellites1 = 3;
            public readonly static int GPGSA_usedSatellites2 = 4;
            public readonly static int GPGSA_usedSatellites3 = 5;
            public readonly static int GPGSA_usedSatellites4 = 6;
            public readonly static int GPGSA_usedSatellites5 = 7;
            public readonly static int GPGSA_usedSatellites6 = 8;
            public readonly static int GPGSA_usedSatellites7 = 9;
            public readonly static int GPGSA_usedSatellites8 = 10;
            public readonly static int GPGSA_usedSatellites9 = 11;
            public readonly static int GPGSA_usedSatellites10 = 12;
            public readonly static int GPGSA_usedSatellites11 = 13;
            public readonly static int GPGSA_usedSatellites12 = 14;
            public readonly static int GPGSA_PDOP = 15;
            public readonly static int GPGSA_HDOP = 16;
            public readonly static int GPGSA_VDOP = 17;
            public readonly static int GPGSA_CheckSum = 18;

            // GPGSV S1 Index
            public readonly static int GPGSV_Header = 0;
            public readonly static int GPGSV_AllSentence = 1;
            public readonly static int GPGSV_SentenceIndex = 2;
            public readonly static int GPGSV_NumOfSatellites = 3;
            public readonly static int GPGSV_Satellite1_SatelliteID = 4;
            public readonly static int GPGSV_Satellite1_Elevation = 5;
            public readonly static int GPGSV_Satellite1_Azimuth = 6;
            public readonly static int GPGSV_Satellite1_SNR = 7;
            public readonly static int GPGSV_Satellite2_SatelliteID = 8;
            public readonly static int GPGSV_Satellite2_Elevation = 9;
            public readonly static int GPGSV_Satellite2_Azimuth = 10;
            public readonly static int GPGSV_Satellite2_SNR = 11;
            public readonly static int GPGSV_Satellite3_SatelliteID = 12;
            public readonly static int GPGSV_Satellite3_Elevation = 13;
            public readonly static int GPGSV_Satellite3_Azimuth = 14;
            public readonly static int GPGSV_Satellite3_SNR = 15;
            public readonly static int GPGSV_Satellite4_SatelliteID = 16;
            public readonly static int GPGSV_Satellite4_Elevation = 17;
            public readonly static int GPGSV_Satellite4_Azimuth = 18;
            public readonly static int GPGSV_Satellite4_SNR = 19;
            public readonly static int GPGSV_S0_CheckSum = 4;
            public readonly static int GPGSV_S1_CheckSum = 8;
            public readonly static int GPGSV_S2_CheckSum = 12;
            public readonly static int GPGSV_S3_CheckSum = 16;
            public readonly static int GPGSV_S4_CheckSum = 20;

            // GPGLL Index
            public readonly static int GPGLL_Header = 0;
            public readonly static int GPGLL_Latitude = 1;
            public readonly static int GPGLL_Latitude_Direction = 2;
            public readonly static int GPGLL_Longitude = 3;
            public readonly static int GPGLL_Longitude_Direction = 4;
            public readonly static int GPGLL_TimeToken = 5;
            public readonly static int GPGLL_Status = 6;
            public readonly static int GPGLL_Border = 7;
            public readonly static int GPGLL_CheckSum = 8;
        }
    }
}