using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;
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
        /// DataContractSerializer (REQUIRES SUPPORTED CLASSES. Nb. don't use - doesn't handle changes well)
        /// </summary>
        DataContract,

        /// <summary>
        /// BinaryFormatter - MS-NRBF
        /// https://msdn.microsoft.com/en-us/library/cc236844.aspx
        /// </summary>
        MsnrbfBinary,

        /// <summary>
        /// XmlSerializer (REQUIRES SUPPORTED CLASSES)
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

        /// <summary>
        /// IniSerialiser.Serialise (REQUIRES SUPPORTED CLASSES)
        /// </summary>
        Ini,
    }

    enum FileId
    {
        [Name( "FileLoadInfo.ini" )] FileLoadInfo,
        [Name( "RecentSessions.dat" )] RecentSessions,
        [Name( "RecentWorkspaces.dat" )] RecentWorkspaces,
        [Name( "General.dat" )] General,
        [Name( "DoNotShowAgain.dat" )] DoNotShowAgain
    }

    internal class FileDescriptor
    {
        public readonly string FileName;
        public readonly SerialisationFormat Format;

        public FileDescriptor( string fileName )
            : this(fileName, SerialisationFormat.Infer )
        {                                          
            // NA
        }

        public FileDescriptor( string fileName, SerialisationFormat format )
        {
            this.FileName = fileName;
            this.Format = _GetFormat(fileName, format );
        }

        public FileDescriptor( FileId fileId )
            : this( _GetFile( fileId ))
        {                                          
            // NA
        }

        public override string ToString()
        {
            return $"\"{FileName}\" ({Format})";
        }

        public static implicit operator string( FileDescriptor descriptor )
        {
            return descriptor.FileName;
        }

        public static implicit operator FileDescriptor( string fileName )
        {
            return new FileDescriptor( fileName );
        }

        public static implicit operator FileDescriptor( FileId fileId )
        {
            return new FileDescriptor( fileId );
        }

        /// <summary>
        /// Gets a filename for internal app settings designated by "id".
        /// 
        /// </summary>
        public static string _GetFile( FileId id )
        {
            string dir = Path.Combine( UiControls.StartupPath, "UserData\\Settings" );

            if (!Directory.Exists( dir ))
            {
                Directory.CreateDirectory( dir );
            }

            return Path.Combine( dir, id.ToUiString() );
        }

        private static SerialisationFormat _GetFormat( string fn, SerialisationFormat format )
        {
            if (format != SerialisationFormat.Infer)
            {
                return format;
            }

            switch (Path.GetExtension( fn ).ToUpper())
            {
                case ".DAT":
                case ".MDAT":
                case ".MDAT-BIN":
                case ".MRES-BIN":
                    return SerialisationFormat.MsnrbfBinary;

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

                case ".INI":
                    return SerialisationFormat.Ini;

                default:
                    throw new InvalidOperationException( $"A request was made to determine the filetype of a file based on its extension, but that extension could not be found in the lookup table. The filename in question is \"{fn}\"." );
            }
        }
    }

    /// <summary>
    /// Reads and writes objects to XML.
    /// </summary>
    static class XmlSettings
    {                          
        public static void Save<T>( FileDescriptor fileName, T data,  ObjectSerialiser serialiser, ProgressReporter prog)
        {
            // Because we move files we can write over readonly files, so prevent this here
            if (File.Exists( fileName ) && new FileInfo( fileName ).IsReadOnly)
            {
                throw new InvalidOperationException($"Will not overwrite file because it is marked as readonly. Filename: {fileName}" );
            }

            // Can't cancel halfway through writing a file
            prog.DisableThrowOnCancel();

            try
            {
                // Create backup
                if (MainSettings.Instance.General.AutoBackup)
                {
                    if (File.Exists( fileName ))
                    {
                        string bakFile = fileName + ".bak";

                        if (File.Exists(bakFile))
                        {
                            File.Delete(bakFile);
                        }

                        File.Move( fileName, bakFile);
                    }
                }

                if (data == null)
                {
                    // Special case for deleting files
                    File.Delete( fileName );
                    return;
                }

                // Save to a temporary file (in case we get an error we don't want to destroy the original by saving over it with a corrupt copy)
                string tempFile = fileName + ".tmp";

                try
                {
                    using (Stream sw = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                    {                                         
                        _Serialise<T>(data, sw, fileName.Format, serialiser, prog);
                    }
                }
                catch
                {
                    File.Delete(tempFile);
                    throw;
                }

                // Move the temp file to the new location
                File.Delete( fileName );
                File.Move(tempFile, fileName );
            }
            finally
            {
                prog.ReenableThrowOnCancel();
            }
        }                                 

        private static XmlSerializer _CreateXmlSerialiser<T>()
        {
            var xs = new XmlSerializer(typeof(T));

            return xs;
        }

        private static DataContractSerializer _CreateDataContactSerialiser<T>()
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
                    var xs = _CreateXmlSerialiser<T>();
                    xs.Serialize(s, data);
                    break;

                case SerialisationFormat.DataContract:
                    var dcs = _CreateDataContactSerialiser<T>();
                    dcs.WriteObject(s, (object)data);
                    break;

                case SerialisationFormat.MsnrbfBinary:
                    var bcs = new BinaryFormatter();
                    bcs.Serialize(s, data);
                    break;

                case SerialisationFormat.Ini:
                    IniSerialiser.Serialise( s, data );                                                    
                    break;

                default:
                    throw new InvalidOperationException("Invalid switch: " + format);
            }
        }

        private static T _Deserialise<T>(Stream s, SerialisationFormat format, ObjectSerialiser serialiser, ProgressReporter prog)
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
                    var xs = _CreateXmlSerialiser<T>();
                    return (T)xs.Deserialize(s);

                case SerialisationFormat.DataContract:
                    var dcs = _CreateDataContactSerialiser<T>();
                    return (T)dcs.ReadObject(s);

                case SerialisationFormat.MsnrbfBinary:
                    var bcs = new BinaryFormatter();
                    bcs.Binder = new TypeNameConverter();
                    return (T)bcs.Deserialize(s);

                case SerialisationFormat.Ini:
                    return IniSerialiser.Deserialise<T>( s );                                                    

                default:
                    throw new InvalidOperationException("Invalid switch: " + format);
            }
        }

        /// <summary>
        /// This little beast handles the conversion of old data types into new data to ensure files
        /// saved with an earlier version still work.  
        /// </summary>
        private class TypeNameConverter : SerializationBinder
        {
            public override Type BindToType( string assemblyName, string typeName )
            {
                // Changed library
                if (typeName == "MetaboliteLevels.Utilities.ParseElementCollection")
                {
                    return typeof( ParseElementCollection );
                }

                if (typeName == "MetaboliteLevels.Utilities.ParseElementCollection+ParseElement")
                {
                    return typeof( ParseElement );
                }

                typeName = typeName.Replace( "MetaboliteLevels.Utilities.ParseElementCollection+ParseElement, MetaboliteLevels", typeof( ParseElement ).Namespace + "." + typeof( ParseElement ).Name + ", MGui" );
                //typeName = typeName.Replace( "MetaboliteLevels.Utilities.ParseElementCollection+ParseElement", typeof( ParseElement ).Namespace + "." + typeof( ParseElement ).Name );

                Type result =  Type.GetType( string.Format( "{0}, {1}", typeName, assemblyName ) );

                if (result == null)
                {
                    Debug.WriteLine( "Could not get type: "+ assemblyName+" " + typeName );
                    Debugger.Break();
                }

                return result;
            }
        }


        public static T LoadAndResave<T>( FileDescriptor fileName,  ProgressReporter prog, ObjectSerialiser serialiser  )
            where T:new()
        {
            T result;

            if (!TryLoad<T>( fileName, prog, out result, default( T ), serialiser ))
            {
                result = new T();
            }

            Save<T>( fileName, result, null, prog );

            return result;
        }

        public static T LoadOrDefault<T>( FileDescriptor fileName,  T @default, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            T result;
            TryLoad( fileName,  prog, out result, @default, serialiser);
            return result;
        }

        public static void SaveLoad<T>( bool save, FileDescriptor fileName, ref T @default, ObjectSerialiser serialiser, ProgressReporter prog)
        {
            if (save)
            {
                Save( fileName,  @default, serialiser, prog);
            }
            else
            {
                @default = LoadOrDefault( fileName,  @default, serialiser, prog);
            }
        }          

        public static bool TryLoad<T>( FileDescriptor fileName, ProgressReporter progress, out T result, T @default, ObjectSerialiser serialiser)
        {
            if (!File.Exists( fileName ))
            {
                result = @default;
                return false;
            }

            try
            {
                using (FileStream sr = new FileStream( fileName, FileMode.Open, FileAccess.Read))
                {        
                    if (progress != null)
                    {
                        using (ProgressStream ps = new ProgressStream(sr, progress))
                        {
                            result = _Deserialise<T>(ps, fileName.Format, serialiser, progress);
                        }
                    }
                    else
                    {
                        result = _Deserialise<T>(sr, fileName.Format, serialiser, progress);
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
    }
}
