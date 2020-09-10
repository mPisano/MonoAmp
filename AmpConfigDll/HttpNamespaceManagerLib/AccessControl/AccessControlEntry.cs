using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HttpNamespaceManager.Lib.AccessControl
{
    public class AccessControlEntry : ICollection<AceRights>
    {
        private AceType aceType = AceType.AccessAllowed;
        private AceFlags flags = AceFlags.None;
        private AceRights rights = AceRights.None;
        private Guid objectGuid = Guid.Empty;
        private Guid inheritObjectGuid = Guid.Empty;
        private SecurityIdentity accountSID;

        /// <summary>
        /// Gets or Sets the Access Control Entry Type
        /// </summary>
        public AceType AceType
        {
            get { return this.aceType; }
            set { this.aceType = value; }
        }

        /// <summary>
        /// Gets or Sets the Access Control Entry Flags
        /// </summary>
        public AceFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        /// <summary>
        /// Gets or Sets the Access Control Entry Rights
        /// </summary>
        /// <remarks>This is a binary flag value, and can be more easily 
        /// accessed via the Access Control Entry collection methods.</remarks>
        public AceRights Rights
        {
            get { return this.rights; }
            set { this.rights = value; }
        }

        /// <summary>
        /// Gets or Sets the Object Guid
        /// </summary>
        public Guid ObjectGuid
        {
            get { return this.objectGuid; }
            set { this.objectGuid = value; }
        }

        /// <summary>
        /// Gets or Sets the Inherit Object Guid
        /// </summary>
        public Guid InheritObjectGuid
        {
            get { return this.inheritObjectGuid; }
            set { this.inheritObjectGuid = value; }
        }

        /// <summary>
        /// Gets or Sets the Account SID
        /// </summary>
        public SecurityIdentity AccountSID
        {
            get { return this.accountSID; }
            set { this.accountSID = value; }
        }

        private AccessControlEntry()
        {
            // Do Nothing
        }

        public AccessControlEntry(SecurityIdentity account)
        {
            this.accountSID = account;
        }

        public AccessControlEntry(AccessControlEntry original)
        {
            this.accountSID = original.accountSID;
            this.aceType = original.aceType;
            this.flags = original.flags;
            this.inheritObjectGuid = original.inheritObjectGuid;
            this.objectGuid = original.objectGuid;
            this.rights = original.rights;
        }

        /// <summary>
        /// Renders the Access Control Entry as an SDDL ACE string
        /// </summary>
        /// <returns>An SDDL ACE string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0};", AccessControlEntry.aceTypeStrings[(int)this.aceType]);

            for (int flag = 0x01; flag <= (int)AceFlags.AuditFailure; flag = flag << 1)
            {
                if ((flag & (int)this.flags) == flag) sb.Append(AccessControlEntry.aceFlagStrings[(int)Math.Log(flag, 2.0d)]);
            }

            sb.Append(';');

            foreach (AceRights right in this)
            {
                sb.Append(AccessControlEntry.rightsStrings[(int)Math.Log((int)right, 2.0d)]);
            }

            sb.Append(';');

            sb.AppendFormat("{0};", this.objectGuid != Guid.Empty ? this.objectGuid.ToString() : "");

            sb.AppendFormat("{0};", this.inheritObjectGuid != Guid.Empty ? this.inheritObjectGuid.ToString() : "");

            if (this.accountSID != null) sb.Append(this.accountSID.ToString());

            return sb.ToString();
        }

        private static readonly string [] aceTypeStrings = new string [] { "A", "D", "OA", "OD", "AU", "AL", "OU", "OL" };
        private static readonly string [] aceFlagStrings = new string [] { "CI", "OI", "NP", "IO", "ID", "SA", "FA" };
        private static readonly string [] rightsStrings = new string [] {
            "GA",
            "GR",
            "GW",
            "GX",
            "RC",
            "SD",
            "WD",
            "WO",
            "RP",
            "WP",
            "CC",
            "DC",
            "LC",
            "SW",
            "LO",
            "DT",
            "CR",
            "FA",
            "FR",
            "FW",
            "FX",
            "KA",
            "KR",
            "KW",
            "KX"
        };

        private const string aceExpr = @"^(?'ace_type'[A-Z]+)?;(?'ace_flags'([A-Z]{2})+)?;(?'rights'([A-Z]{2})+|0x[0-9A-Fa-f]+)?;(?'object_guid'[0-9A-Fa-f\-]+)?;(?'inherit_object_guid'[0-9A-Fa-f\-]+)?;(?'account_sid'[A-Z]+?|S(-[0-9]+)+)?$";

        public static AccessControlEntry AccessControlEntryFromString(string aceString)
        {
            Regex aceRegex = new Regex(aceExpr, RegexOptions.IgnoreCase);

            Match aceMatch = aceRegex.Match(aceString);

            if (!aceMatch.Success) throw new FormatException("Invalid ACE String Format");

            AccessControlEntry ace = new AccessControlEntry();

            if (aceMatch.Groups["ace_type"] != null && aceMatch.Groups["ace_type"].Success && !String.IsNullOrEmpty(aceMatch.Groups["ace_type"].Value))
            {
                int aceTypeValue = Array.IndexOf<string>(AccessControlEntry.aceTypeStrings, aceMatch.Groups["ace_type"].Value.ToUpper());

                if (aceTypeValue == -1) throw new FormatException("Invalid ACE String Format");

                ace.aceType = (AceType)aceTypeValue;
            }
            else throw new FormatException("Invalid ACE String Format");

            if (aceMatch.Groups["ace_flags"] != null && aceMatch.Groups["ace_flags"].Success && !String.IsNullOrEmpty(aceMatch.Groups["ace_flags"].Value))
            {
                string aceFlagsValue = aceMatch.Groups["ace_flags"].Value.ToUpper();
                for (int i = 0; i < aceFlagsValue.Length - 1; i += 2)
                {
                    int flagValue = Array.IndexOf<string>(AccessControlEntry.aceFlagStrings, aceFlagsValue.Substring(i, 2));

                    if (flagValue == -1) throw new FormatException("Invalid ACE String Format");

                    ace.flags = ace.flags | ((AceFlags)(int)Math.Pow(2.0d, flagValue));
                }
            }

            if (aceMatch.Groups["rights"] != null && aceMatch.Groups["rights"].Success && !String.IsNullOrEmpty(aceMatch.Groups["rights"].Value))
            {
                string rightsValue = aceMatch.Groups["rights"].Value.ToUpper();
                for (int i = 0; i < rightsValue.Length - 1; i += 2)
                {
                    int rightValue = Array.IndexOf<string>(AccessControlEntry.rightsStrings, rightsValue.Substring(i, 2));

                    if (rightValue == -1) throw new FormatException("Invalid ACE String Format");

                    ace.Add((AceRights)(int)Math.Pow(2.0d, rightValue));
                }
            }

            if (aceMatch.Groups["object_guid"] != null && aceMatch.Groups["object_guid"].Success && !String.IsNullOrEmpty(aceMatch.Groups["object_guid"].Value))
            {
                ace.objectGuid = new Guid(aceMatch.Groups["object_guid"].Value);
            }

            if (aceMatch.Groups["inherit_object_guid"] != null && aceMatch.Groups["inherit_object_guid"].Success && !String.IsNullOrEmpty(aceMatch.Groups["inherit_object_guid"].Value))
            {
                ace.inheritObjectGuid = new Guid(aceMatch.Groups["inherit_object_guid"].Value);
            }

            if (aceMatch.Groups["account_sid"] != null && aceMatch.Groups["account_sid"].Success && !String.IsNullOrEmpty(aceMatch.Groups["account_sid"].Value))
            {
                ace.accountSID = SecurityIdentity.SecurityIdentityFromString(aceMatch.Groups["account_sid"].Value.ToUpper());
            }
            else throw new FormatException("Invalid ACE String Format");

            return ace;
        }

        #region Rights Collection Members

        public void Add(AceRights item)
        {
            this.rights = rights | item;
        }

        public void Clear()
        {
            this.rights = AceRights.None;
        }

        public bool Contains(AceRights item)
        {
            return item == (item & this.rights);
        }

        public void CopyTo(AceRights[] array, int arrayIndex)
        {
            foreach (AceRights right in this)
            {
                array[arrayIndex++] = right;
            }
        }

        public int Count
        {
            get
            {
                int count = 0;
                for (int col = (int)this.rights; col != 0; col = col >> 1) count += ((col & 1) == 1) ? 1 : 0;
                return count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AceRights item)
        {
            if (this.Contains(item))
            {
                this.rights = this.rights & ~item;
                return true;
            }
            else return false;
        }

        public IEnumerator<AceRights> GetEnumerator()
        {
            int current = (int)AceRights.GenericAll;
            for (int col = (int)this.rights; col != 0; col = col >> 1, current = current << 1)
            {
                while (col != 0 && (col & 1) != 1)
                {
                    col = col >> 1;
                    current = current << 1;
                }

                if ((col & 1) == 1)
                {
                    yield return (AceRights)current;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)this.GetEnumerator();
        }

        #endregion
    }

    public enum AceType
    {
        AccessAllowed = 0,
        AccessDenied,
        ObjectAccessAllowed,
        ObjectAccessDenied,
        Audit,
        Alarm,
        ObjectAudit,
        ObjectAlarm
    }

    [Flags]
    public enum AceFlags
    {
        None = 0x0000,
        ContainerInherit = 0x0001,
        ObjectInherit = 0x0002,
        NoPropogate = 0x0004,
        InheritOnly = 0x0008,
        Inherited = 0x0010,
        AuditSuccess = 0x0020,
        AuditFailure = 0x0040
    }

    [Flags]
    public enum AceRights
    {
        None = 0x00000000,
        GenericAll = 0x00000001,
        GenericRead = 0x00000002,
        GenericWrite = 0x00000004,
        GenericExecute = 0x00000008,
        StandardReadControl = 0x00000010,
        StandardDelete = 0x00000020,
        StandardWriteDAC = 0x00000040,
        StandardWriteOwner = 0x00000080,
        DirectoryReadProperty = 0x00000100,
        DirectoryWriteProperty = 0x00000200,
        DirectoryCreateChild = 0x00000400,
        DirectoryDeleteChild = 0x00000800,
        DirectoryListChildren = 0x00001000,
        DirectorySelfWrite = 0x00002000,
        DirectoryListObject = 0x00004000,
        DirectoryDeleteTree = 0x00008000,
        DirectoryControlAccess = 0x00010000,
        FileAll = 0x00020000,
        FileRead = 0x00040000,
        FileWrite = 0x00080000,
        FileExecute = 0x00100000,
        KeyAll = 0x00200000,
        KeyRead = 0x00400000,
        KeyWrite = 0x00800000,
        KeyExecute = 0x01000000
    }
}
