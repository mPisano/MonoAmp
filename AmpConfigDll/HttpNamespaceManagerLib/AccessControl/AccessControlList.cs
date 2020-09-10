using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HttpNamespaceManager.Lib.AccessControl
{
    /// <summary>
    /// Access Control List
    /// </summary>
    public class AccessControlList : IList<AccessControlEntry>
    {
        private AclFlags flags = AclFlags.None;
        private List<AccessControlEntry> aceList;

        /// <summary>
        /// Gets or Sets the Access Control List flags
        /// </summary>
        public AclFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        /// <summary>
        /// Creates a Blank Access Control List
        /// </summary>
        public AccessControlList()
        {
            this.aceList = new List<AccessControlEntry>();
        }

        /// <summary>
        /// Creates a deep copy of an exusting Access Control List
        /// </summary>
        /// <param name="original"></param>
        public AccessControlList(AccessControlList original)
        {
            this.aceList = new List<AccessControlEntry>();
            this.flags = original.flags;

            foreach (AccessControlEntry ace in original)
            {
                this.Add(new AccessControlEntry(ace));
            }
        }

        /// <summary>
        /// Renders the Access Control List and an SDDL ACL string.
        /// </summary>
        /// <returns>An SDDL ACL string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if ((this.flags & AclFlags.Protected) == AclFlags.Protected) sb.Append('P');
            if ((this.flags & AclFlags.MustInherit) == AclFlags.MustInherit) sb.Append("AR");
            if ((this.flags & AclFlags.Inherited) == AclFlags.Inherited) sb.Append("AI");

            foreach(AccessControlEntry ace in this.aceList)
            {
                sb.AppendFormat("({0})", ace.ToString());
            }

            return sb.ToString();
        }

        private const string aclExpr = @"^(?'flags'[A-Z]+)?(?'ace_list'(\([^\)]+\))+)$";
        private const string aceListExpr = @"\((?'ace'[^\)]+)\)";

        /// <summary>
        /// Creates an Access Control List from the DACL or SACL portion of an SDDL string
        /// </summary>
        /// <param name="aclString">The ACL String</param>
        /// <returns>A populated Access Control List</returns>
        public static AccessControlList AccessControlListFromString(string aclString)
        {
            Regex aclRegex = new Regex(AccessControlList.aclExpr, RegexOptions.IgnoreCase);

            Match aclMatch = aclRegex.Match(aclString);

            if (!aclMatch.Success) throw new FormatException("Invalid ACL String Format");

            AccessControlList acl = new AccessControlList();

            if(aclMatch.Groups["flags"] != null && aclMatch.Groups["flags"].Success && !String.IsNullOrEmpty(aclMatch.Groups["flags"].Value))
            {
                string flagString = aclMatch.Groups["flags"].Value.ToUpper();
                for (int i = 0; i < flagString.Length; i++)
                {
                    if (flagString[i] == 'P')
                    {
                        acl.flags = acl.flags | AclFlags.Protected;
                    }
                    else if(flagString.Length - i >= 2)
                    {
                        switch(flagString.Substring(i, 2))
                        {
                            case "AR":
                                acl.flags = acl.flags | AclFlags.MustInherit;
                                i++;
                                break;
                            case "AI":
                                acl.flags = acl.flags | AclFlags.Inherited;
                                i++;
                                break;
                            default:
                                throw new FormatException("Invalid ACL String Format");
                        }
                    }
                    else throw new FormatException("Invalid ACL String Format");
                }
            }

            if (aclMatch.Groups["ace_list"] != null && aclMatch.Groups["ace_list"].Success && !String.IsNullOrEmpty(aclMatch.Groups["ace_list"].Value))
            {
                Regex aceListRegex = new Regex(AccessControlList.aceListExpr);

                foreach (Match aceMatch in aceListRegex.Matches(aclMatch.Groups["ace_list"].Value))
                {
                    acl.Add(AccessControlEntry.AccessControlEntryFromString(aceMatch.Groups["ace"].Value));
                }
            }

            return acl;
        }

        /// <summary>
        /// Gets the Index of an <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" />
        /// </summary>
        /// <param name="item">The <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /></param>
        /// <returns>The index of the <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" />, or -1 if the Access Control Entry is not found</returns>
        public int IndexOf(AccessControlEntry item)
        {
            return this.aceList.IndexOf(item);
        }

        /// <summary>
        /// Inserts an <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> into the Access Control List
        /// </summary>
        /// <param name="index">The insertion position</param>
        /// <param name="item">The <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> to insert</param>
        public void Insert(int index, AccessControlEntry item)
        {
            this.aceList.Insert(index, item);
        }

        /// <summary>
        /// Removes the <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> at the specified position
        /// </summary>
        /// <param name="index">The position to remove</param>
        public void RemoveAt(int index)
        {
            this.aceList.RemoveAt(index);
        }

        /// <summary>
        /// Gets or Sets an <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AccessControlEntry this[int index]
        {
            get
            {
                return this.aceList[index];
            }
            set
            {
                this.aceList[index] = value;
            }
        }

        /// <summary>
        /// Adds a <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> to the Access Control List
        /// </summary>
        /// <param name="item">The <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> to add</param>
        public void Add(AccessControlEntry item)
        {
            this.aceList.Add(item);
        }

        /// <summary>
        /// Clears all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items from the Access Control List
        /// </summary>
        public void Clear()
        {
            this.aceList.Clear();
        }

        /// <summary>
        /// Checks if an <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> exists in the Access Control List
        /// </summary>
        /// <param name="item">The <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /></param>
        /// <returns>true if the <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> exists, otherwise false</returns>
        public bool Contains(AccessControlEntry item)
        {
            return this.aceList.Contains(item);
        }

        /// <summary>
        /// Copies all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items from the Access Control List to an Array
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="arrayIndex">The index of the array at which to begin copying</param>
        public void CopyTo(AccessControlEntry[] array, int arrayIndex)
        {
            this.aceList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items in the Access Control List
        /// </summary>
        public int Count
        {
            get { return this.aceList.Count; }
        }

        /// <summary>
        /// Returns false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes an <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> from the Access Control List
        /// </summary>
        /// <param name="item">The <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> to remove</param>
        /// <returns>true if the <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> was found and removed, otherwise false</returns>
        public bool Remove(AccessControlEntry item)
        {
            return this.aceList.Remove(item);
        }

        /// <summary>
        /// Gets an Enumerator over all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items in the Access Control List
        /// </summary>
        /// <returns>An Enumerator over all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items</returns>
        public IEnumerator<AccessControlEntry> GetEnumerator()
        {
            return this.aceList.GetEnumerator();
        }

        /// <summary>
        /// Gets an Enumerator over all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items in the Access Control List
        /// </summary>
        /// <returns>An Enumerator over all <see cref="HttpNamespaceManager.Lib.AccessControl.AccessControlEntry" /> items</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.aceList).GetEnumerator();
        }
    }

    /// <summary>
    /// Access Control List Flags
    /// </summary>
    [Flags]
    public enum AclFlags
    {
        None = 0x00,
        Protected = 0x01,
        MustInherit = 0x02,
        Inherited = 0x04
    }
}
