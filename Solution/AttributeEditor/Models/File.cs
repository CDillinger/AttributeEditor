using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AttributeEditor.Models
{
	public class File
	{
		public File(string path, bool useUTC = false)
		{
			Path = path;
			Security = System.IO.File.GetAccessControl(Path);
			Attributes = AttributeBreakdown(System.IO.File.GetAttributes(Path));
			if (Attributes.ReadOnly)
				OriginallyReadOnly = true;

			if (!useUTC)
			{
				CreationTime = System.IO.File.GetCreationTime(Path);
				LastAccessTime = System.IO.File.GetLastAccessTime(Path);
				LastWriteTime = System.IO.File.GetLastWriteTime(Path);
			}

			else
			{
				CreationTime = System.IO.File.GetCreationTimeUtc(Path);
				LastAccessTime = System.IO.File.GetLastAccessTimeUtc(Path);
				LastWriteTime = System.IO.File.GetLastWriteTimeUtc(Path);
			}
		}

		public string Path { get; set; }

		public bool OriginallyReadOnly { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime LastAccessTime { get; set; }

		public DateTime LastWriteTime { get; set; }

		public FileSecurity Security { get; set; }

		public AttributeFlags Attributes { get; set; }

		public class AttributeFlags
		{
			public bool Normal { get; set; }

			public bool Archive { get; set; }

			public bool Compressed { get; set; }

			public bool Device { get; set; }

			public bool Directory { get; set; }

			public bool Encrypted { get; set; }

			public bool Hidden { get; set; }

			public bool IntegrityStream { get; set; }

			public bool NoScrubData { get; set; }

			public bool NotContentIndexed { get; set; }

			public bool Offline { get; set; }

			public bool ReadOnly { get; set; }

			public bool ReparsePoint { get; set; }

			public bool SparseFile { get; set; }

			public bool System { get; set; }

			public bool Temporary { get; set; }
		}

		public static FileAttributes AttributesFromBools(AttributeFlags attributes)
		{
			FileAttributes flags = new FileAttributes();

			if (attributes.Normal)
				flags |= FileAttributes.Normal;

			if (attributes.Archive)
				flags |= FileAttributes.Archive;

			if (attributes.Compressed)
				flags |= FileAttributes.Compressed;

			if (attributes.Device)
				flags |= FileAttributes.Device;

			if (attributes.Directory)
				flags |= FileAttributes.Directory;

			if (attributes.Encrypted)
				flags |= FileAttributes.Encrypted;

			if (attributes.Hidden)
				flags |= FileAttributes.Hidden;

			if (attributes.IntegrityStream)
				flags |= FileAttributes.IntegrityStream;

			if (attributes.NoScrubData)
				flags |= FileAttributes.NoScrubData;

			if (attributes.NotContentIndexed)
				flags |= FileAttributes.NotContentIndexed;

			if (attributes.Offline)
				flags |= FileAttributes.Offline;

			if (attributes.ReadOnly)
				flags |= FileAttributes.ReadOnly;

			if (attributes.ReparsePoint)
				flags |= FileAttributes.ReparsePoint;

			if (attributes.SparseFile)
				flags |= FileAttributes.SparseFile;

			if (attributes.System)
				flags |= FileAttributes.System;

			if (attributes.Temporary)
				flags |= FileAttributes.Temporary;

			return flags;
		}

		public static AttributeFlags AttributeBreakdown(FileAttributes flags)
		{
			AttributeFlags attributes = new AttributeFlags();

			if (flags.HasFlag(FileAttributes.Normal))
				attributes.Normal = true;

			if (flags.HasFlag(FileAttributes.Archive))
				attributes.Archive = true;

			if (flags.HasFlag(FileAttributes.Compressed))
				attributes.Compressed = true;

			if (flags.HasFlag(FileAttributes.Device))
				attributes.Device = true;

			if (flags.HasFlag(FileAttributes.Directory))
				attributes.Directory = true;

			if (flags.HasFlag(FileAttributes.Encrypted))
				attributes.Encrypted = true;

			if (flags.HasFlag(FileAttributes.Hidden))
				attributes.Hidden = true;

			if (flags.HasFlag(FileAttributes.IntegrityStream))
				attributes.IntegrityStream = true;

			if (flags.HasFlag(FileAttributes.NoScrubData))
				attributes.NoScrubData = true;

			if (flags.HasFlag(FileAttributes.NotContentIndexed))
				attributes.NotContentIndexed = true;

			if (flags.HasFlag(FileAttributes.Offline))
				attributes.Offline = true;

			if (flags.HasFlag(FileAttributes.ReadOnly))
				attributes.ReadOnly = true;

			if (flags.HasFlag(FileAttributes.ReparsePoint))
				attributes.ReparsePoint = true;

			if (flags.HasFlag(FileAttributes.SparseFile))
				attributes.SparseFile = true;

			if (flags.HasFlag(FileAttributes.System))
				attributes.System = true;

			if (flags.HasFlag(FileAttributes.Temporary))
				attributes.Temporary = true;

			return attributes;
		}

		public static void Save(File file, bool ignoreReadOnly = false)
		{
			bool readOnly = false;
			if (ignoreReadOnly && file.Attributes.ReadOnly)
			{
				readOnly = true;
				file.Attributes.ReadOnly = false;
			}

			System.IO.File.SetAttributes(file.Path, AttributesFromBools(file.Attributes));
			System.IO.File.SetAccessControl(file.Path, file.Security);
			System.IO.File.SetCreationTime(file.Path, file.CreationTime);
			System.IO.File.SetLastAccessTime(file.Path, file.LastAccessTime);
			System.IO.File.SetLastWriteTime(file.Path, file.LastWriteTime);

			if (readOnly)
			{
				file.Attributes.ReadOnly = true;
				System.IO.File.SetAttributes(file.Path, AttributesFromBools(file.Attributes));
			}
		}
	}
}
