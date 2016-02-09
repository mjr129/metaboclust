using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetaboliteLevels.Utilities;
using MSerialisers;
using MSerialisers.Serialisers;
using MSerialisers.Streams;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// Type of file to read/write data.
    /// </summary>
    enum SerialisationFormat
    {
        /// <summary>
        /// Infer from filename
        /// </summary>
        Infer,

        /// <summary>
        /// DataContractSerializer - don't use - doesn't handle changes well
        /// </summary>
        DataContract,

        /// <summary>
        /// BinaryFormatter - MS-NRBF
        /// https://msdn.microsoft.com/en-us/library/cc236844.aspx
        /// </summary>
        Binary,

        /// <summary>
        /// XmlSerializer - don't use - not supported
        /// </summary>
        Xml,

        /// <summary>
        /// MSerialiser.Serialise (as binary)
        /// </summary>
        MSerialiserBinary,

        /// <summary>
        /// MSerialiser.Serialise (as text)
        /// </summary>
        MSerialiserText,

        /// <summary>
        /// MSerialiser.Serialise (as compact binary)
        /// </summary>
        MSerialiserCompactBinary,

        /// <summary>
        /// MSerialiser.Serialise (as fast binary)
        /// </summary>
        MSerialiserFastBinary,
    }

    /// <summary>
    /// Reads and writes objects to XML.
    /// </summary>
    static class XmlSettings
    {
        public static void Save<T>(string id, SerialisationFormat format, T data, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            string fn = GetFile(id, format);

            SaveToFile<T>(fn, data, format, serialiser, prog);
        }

        public static void SaveToFile<T>(string fn, T data, SerialisationFormat format, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            // Can't cancel halfway through writing a file
            prog.DisableThrowOnCancel();

            try
            {
                // Create backup
                if (MainSettings.Instance.General.AutoBackup)
                {
                    if (File.Exists(fn))
                    {
                        string bakFile = fn + ".bak";

                        if (File.Exists(bakFile))
                        {
                            File.Delete(bakFile);
                        }

                        File.Move(fn, bakFile);
                    }
                }

                if (data == null)
                {
                    // Special case for deleting files
                    File.Delete(fn);
                    return;
                }

                // Save to a temporary file (in case we get an error we don't want to destroy the original by saving over it with a corrupt copy)
                string tempFile = fn + ".tmp";

                try
                {
                    using (Stream sw = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                    {
                        format = Infer(fn, format);
                        _Serialise<T>(data, sw, format, serialiser, prog);
                    }
                }
                catch
                {
                    File.Delete(tempFile);
                    throw;
                }

                // Move the temp file to the new location
                File.Delete(fn);
                File.Move(tempFile, fn);
            }
            finally
            {
                prog.ReenableThrowOnCancel();
            }
        }

        private static SerialisationFormat Infer(string fn, SerialisationFormat format)
        {
            if (format != SerialisationFormat.Infer)
            {
                return format;
            }

            switch (Path.GetExtension(fn).ToUpper())
            {
                case ".MDAT":
                case ".MDAT-BIN":
                case ".MRES-BIN":
                    return SerialisationFormat.Binary;

                case ".MDAT-MBIN":
                case ".MRES-MBIN":
                    return SerialisationFormat.MSerialiserBinary;

                case ".MRES":
                case ".MDAT-CBIN":
                case ".MRES-CBIN":
                    return SerialisationFormat.MSerialiserCompactBinary;

                case ".MDAT-FBIN":
                case ".MRES-FBIN":
                    return SerialisationFormat.MSerialiserFastBinary;

                case ".MDAT-TXT":
                case ".MRES-TXT":
                case ".TXT":
                    return SerialisationFormat.MSerialiserText;

                case ".MDAT-CON":
                case ".MRES-CON":
                    return SerialisationFormat.DataContract;

                case ".MDAT-XML":
                case ".MRES-XML":
                case ".XML":
                    return SerialisationFormat.Xml;

                default:
                    throw new InvalidOperationException("Could not determine file type from extension.");
            }
        }

        private static XmlSerializer CreateXmlSerialiser<T>()
        {
            var xs = new XmlSerializer(typeof(T));

            return xs;
        }

        private static DataContractSerializer CreateDataContactSerialiser<T>()
        {
            var dcs = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null);

            return dcs;
        }

        private static void _Serialise<T>(T data, Stream s, SerialisationFormat format, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            switch (format)
            {
                case SerialisationFormat.MSerialiserBinary:
                    MSerialiser.SerialiseStream(s, data, ETransmission.Binary, new[] { serialiser }, null);
                    return;

                case SerialisationFormat.MSerialiserText:
                    MSerialiser.SerialiseStream(s, data, ETransmission.Text, new[] { serialiser }, null);
                    return;

                case SerialisationFormat.MSerialiserCompactBinary:
                    MSerialiser.SerialiseStream(s, data, ETransmission.CompactBinary, new[] { serialiser }, null);
                    return;

                case SerialisationFormat.MSerialiserFastBinary:
                    MSerialiser.SerialiseStream(s, data, ETransmission.FastBinary, new[] { serialiser }, null);
                    return;

                case SerialisationFormat.Xml:
                    var xs = CreateXmlSerialiser<T>();
                    xs.Serialize(s, data);
                    break;

                case SerialisationFormat.DataContract:
                    var dcs = CreateDataContactSerialiser<T>();
                    dcs.WriteObject(s, (object)data);
                    break;

                case SerialisationFormat.Binary:
                    var bcs = new BinaryFormatter();
                    bcs.Serialize(s, data);
                    break;

                default:
                    throw new InvalidOperationException("Invalid switch: " + format);
            }
        }

        private static T Deserialise<T>(Stream s, SerialisationFormat format, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            switch (format)
            {
                case SerialisationFormat.MSerialiserBinary:
                    return MSerialiser.DeserialiseStream<T>(s, ETransmission.Binary, new[] { serialiser }, null);

                case SerialisationFormat.MSerialiserText:
                    return MSerialiser.DeserialiseStream<T>(s, ETransmission.Text, new[] { serialiser }, null);

                case SerialisationFormat.MSerialiserCompactBinary:
                    return MSerialiser.DeserialiseStream<T>(s, ETransmission.CompactBinary, new[] { serialiser }, null);

                case SerialisationFormat.MSerialiserFastBinary:
                    return MSerialiser.DeserialiseStream<T>(s, ETransmission.FastBinary, new[] { serialiser }, null);

                case SerialisationFormat.Xml:
                    var xs = CreateXmlSerialiser<T>();
                    return (T)xs.Deserialize(s);

                case SerialisationFormat.DataContract:
                    var dcs = CreateDataContactSerialiser<T>();
                    return (T)dcs.ReadObject(s);

                case SerialisationFormat.Binary:
                    var bcs = new BinaryFormatter();
                    return (T)bcs.Deserialize(s);

                default:
                    throw new InvalidOperationException("Invalid switch: " + format);
            }
        }

        public static T Load<T>(string id, SerialisationFormat format, ObjectSerialiser serialiser)
        {
            T result;
            TryLoad(id, format, null, out result, default(T), serialiser);
            return result;
        }

        public static T LoadFromFile<T>(string fn, SerialisationFormat format, ProgressReporter progress, ObjectSerialiser serialiser = null)
        {
            T result;
            TryLoadFromFile(fn, format, progress, out result, default(T), serialiser);
            return result;
        }

        public static T Load<T>(string id, SerialisationFormat format, T @default, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            T result;
            TryLoad(id, format, prog, out result, @default, serialiser);
            return result;
        }

        public static void SaveLoad<T>(bool save, string id, ref T @default, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            if (save)
            {
                Save(id, SerialisationFormat.Binary, @default, serialiser, prog);
            }
            else
            {
                @default = Load(id, SerialisationFormat.Binary, @default, serialiser, prog);
            }
        }

        public static T LoadFromFile<T>(string fn)
        {
            return LoadFromFile<T>(fn, SerialisationFormat.Binary, default(T), null);
        }

        public static T LoadFromFile<T>(string fn, SerialisationFormat format, T @default, ObjectSerialiser serialiser)
        {
            T result;
            TryLoadFromFile(fn, format, null, out result, @default, serialiser);
            return result;
        }

        public static bool TryLoad<T>(string id, SerialisationFormat format, out T result, ObjectSerialiser serialiser)
        {
            return TryLoad(id, format, null, out result, default(T), serialiser);
        }

        public static bool TryLoadFromFile<T>(string fn, SerialisationFormat format, ProgressReporter progress, out T result, ObjectSerialiser serialiser)
        {
            return TryLoadFromFile(fn, format, progress, out result, default(T), serialiser);
        }

        public static bool TryLoad<T>(string id, SerialisationFormat format, ProgressReporter progress, out T result, T @default, ObjectSerialiser serialiser)
        {
            string fn = GetFile(id, format);

            return TryLoadFromFile<T>(fn, format, progress, out result, @default, serialiser);
        }

        public static bool TryLoadFromFile<T>(string fn, SerialisationFormat format, ProgressReporter progress, out T result, T @default, ObjectSerialiser serialiser)
        {
            if (!File.Exists(fn))
            {
                result = @default;
                return false;
            }

            try
            {
                using (FileStream sr = new FileStream(fn, FileMode.Open, FileAccess.Read))
                {
                    format = Infer(fn, format);

                    if (progress != null)
                    {
                        using (ProgressStream ps = new ProgressStream(sr, progress))
                        {
                            result = Deserialise<T>(ps, format, serialiser, progress);
                        }
                    }
                    else
                    {
                        result = Deserialise<T>(sr, format, serialiser, progress);
                    }
                }

                return true;
            }
            catch
            {
                result = @default;
                return false;
            }
        }

        /// <summary>
        /// Gets a filename for internal app settings designated by "id".
        /// </summary>
        public static string GetFile(string id, SerialisationFormat format)
        {
            string dir = Path.Combine(UiControls.StartupPath, "UserData\\Settings");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            switch (format)
            {
                case SerialisationFormat.Binary:
                    return Path.Combine(dir, id + ".bin");

                case SerialisationFormat.DataContract:
                case SerialisationFormat.Xml:
                    return Path.Combine(dir, id + ".xml");

                default:
                    throw new InvalidOperationException("GetFile: Invalid switch: " + format);
            }
        }
    }
}
