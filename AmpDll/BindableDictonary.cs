using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BindableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IBindingList, IRaiseItemChangedEvents
{
    public SortedList<TKey, TValue> _Source = null;

    private ListChangedEventHandler listChanged;

    private bool raiseItemChangedEvents = false;
    private bool raiseListChangedEvents = true;

    [NonSerialized()]
    private PropertyDescriptorCollection itemTypeProperties = null;

    [NonSerialized()]
    private PropertyChangedEventHandler propertyChangedEventHandler = null;

    [NonSerialized()]
    private int lastChangeIndex = -1;

    #region Properties
    #region IBindingList
    //Gets whether you can update items in the list. 
    bool IBindingList.AllowEdit
    {
        get
        {
            return true;
        }
    }

    //Gets whether you can add items to the list using AddNew. 
    bool IBindingList.AllowNew
    {
        get
        {
            return false;
        }
    }

    //Gets whether you can remove items from the list, using Remove or RemoveAt. 
    bool IBindingList.AllowRemove
    {
        get
        {
            return true;
        }
    }


    //Gets a value indicating whether the IList has a fixed size. (Inherited from IList.)
    bool System.Collections.IList.IsFixedSize
    {
        get { return false; }
    }

    //Gets a value indicating whether the IList is read-only. (Inherited from IList.)
    bool System.Collections.IList.IsReadOnly
    {
        get { return true; }
    }

    //Gets whether the items in the list are sorted. 
    bool IBindingList.IsSorted
    {
        get
        {
            return false;
        }
    }

    //Gets a value indicating whether access to the ICollection is synchronized (thread safe). (Inherited from ICollection.)
    bool ICollection.IsSynchronized
    {
        get { return false; }
    }

    //Gets or sets the element at the specified index. (Inherited from IList.)
    object System.Collections.IList.this[int index]
    {
        get { return _Source[_Source.Keys[index]]; }
        set { _Source[_Source.Keys[index]] = (TValue)value; }
    }

    //Gets the direction of the sort. 
    ListSortDirection IBindingList.SortDirection
    {
        get
        {
            return ListSortDirection.Ascending;
        }
    }

    //Gets the PropertyDescriptor that is being used for sorting. 
    PropertyDescriptor IBindingList.SortProperty
    {
        get { return null; }
    }

    //Gets whether a ListChanged event is raised when the list changes or an item in the list changes. 
    bool IBindingList.SupportsChangeNotification
    {
        get { return true; }
    }

    //Gets whether the list supports searching using the Find method. 
    bool IBindingList.SupportsSearching
    {
        get { return false; }
    }

    //Gets whether the list supports sorting. 
    bool IBindingList.SupportsSorting
    {
        get { return false; }
    }

    //Gets an object that can be used to synchronize access to the ICollection. (Inherited from ICollection.)
    object ICollection.SyncRoot
    {
        get { return null; }
    }
    #endregion

    #region IDictionary
    //Gets a value indicating whether the ICollection<(Of <(T>)>) is read-only. (Inherited from ICollection<(Of <(T>)>).)
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
        get { return ((ICollection<KeyValuePair<TKey, TValue>>)_Source).IsReadOnly; }
    }

    //Gets or sets the element with the specified key. 
    public TValue this[TKey key]
    {
        get
        {
            return _Source[key];
        }
        set
        {
            bool bAdded = false;
            if (_Source.ContainsKey(key))
            {
                bAdded = true;
            }
            _Source[key] = value;
            if (bAdded)
            {
                if (this.raiseItemChangedEvents)
                {
                    HookPropertyChanged(value);
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, _Source.IndexOfKey(key)));
                }
            }
        }
    }

    //Gets an ICollection<(Of <(T>)>) containing the keys of the IDictionary<(Of <(TKey, TValue>)>). 
    public ICollection<TKey> Keys
    {
        get { return _Source.Keys; }
    }

    //Gets an ICollection<(Of <(T>)>) containing the values in the IDictionary<(Of <(TKey, TValue>)>). 
    public ICollection<TValue> Values
    {
        get { return _Source.Values; }
    }

    #endregion

    //Gets the number of elements contained in the ICollection. (Inherited from ICollection.)
    //Gets the number of elements contained in the ICollection<(Of <(T>)>). (Inherited from ICollection<(Of <(T>)>).)
    public int Count
    {
        get { return _Source.Count; }
    }

    #endregion

    #region Methods
    #region IBindingList
    //Add function not implemented use other Add function instead
    //Adds an item to the IList. (Inherited from IList.)
    int System.Collections.IList.Add(object value)
    {
        throw new NotImplementedException();
    }

    //Adds the PropertyDescriptor to the indexes used for searching.
    void IBindingList.AddIndex(PropertyDescriptor property)
    {
    }

    //Adds a new item to the list. 
    object IBindingList.AddNew()
    {
        return null;
    }

    //Sorts the list based on a PropertyDescriptor and a ListSortDirection. 
    void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
    {
    }

    //Determines whether the IList contains a specific value. (Inherited from IList.)
    bool System.Collections.IList.Contains(object value)
    {
        if (value is TKey)
        {
            return _Source.ContainsKey((TKey)value);
        }
        else if (value is TValue)
        {
            return _Source.ContainsValue((TValue)value);
        }
        return false;
    }

    //Copies the elements of the ICollection to an Array, starting at a particular Array index. (Inherited from ICollection.)
    void ICollection.CopyTo(Array array, int arrayIndex)
    {
        ((ICollection)_Source).CopyTo(array, arrayIndex);
    }

    //Returns the index of the row that has the given PropertyDescriptor. 
    int IBindingList.Find(PropertyDescriptor property, object key)
    {
        throw new NotImplementedException();
    }

    //Returns an enumerator that iterates through a collection. (Inherited from IEnumerable.)
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //Determines the index of a specific item in the IList. (Inherited from IList.)
    int System.Collections.IList.IndexOf(object value)
    {

        if (value is TKey)
        {
            return _Source.IndexOfKey((TKey)value);
        }
        else if (value is TValue)
        {
            return _Source.IndexOfValue((TValue)value);
        }
        return -1;
    }

    //Inserts an item to the IList at the specified index. (Inherited from IList.)
    void System.Collections.IList.Insert(int index, object value)
    {
        throw new NotImplementedException();
    }

    //Removes the first occurrence of a specific object from the IList. (Inherited from IList.)
    void System.Collections.IList.Remove(object value)
    {
        if (value is TKey)
        {
            Remove((TKey)value);
        }
    }

    //Removes the IList item at the specified index. (Inherited from IList.)
    void System.Collections.IList.RemoveAt(int index)
    {
        _Source.RemoveAt(index);
    }

    //Removes the PropertyDescriptor from the indexes used for searching. 
    void IBindingList.RemoveIndex(PropertyDescriptor property)
    {
    }

    //Removes any sort applied using ApplySort. 
    void IBindingList.RemoveSort()
    {
    }
    #endregion


    #region IDictionary
    //Adds an item to the ICollection<(Of <(T>)>). (Inherited from ICollection<(Of <(T>)>).)
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)_Source).Add(item);

        if (this.raiseItemChangedEvents)
        {
            HookPropertyChanged(item.Value);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, _Source.IndexOfKey(item.Key)));
        }
    }

    //Adds an element with the provided key and value to the IDictionary<(Of <(TKey, TValue>)>). 
    public void Add(TKey key, TValue value)
    {
        _Source.Add(key, value);

        if (this.raiseItemChangedEvents)
        {
            HookPropertyChanged(value);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, _Source.IndexOfKey(key)));
        }
    }

    //Determines whether the ICollection<(Of <(T>)>) contains a specific value. (Inherited from ICollection<(Of <(T>)>).)
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)_Source).Contains(item);
    }

    //Determines whether the IDictionary<(Of <(TKey, TValue>)>) contains an element with the specified key. 
    public bool ContainsKey(TKey key)
    {
        return _Source.ContainsKey(key);
    }

    //Determines whether the IDictionary<(Of <(TKey, TValue>)>) contains an element with the specified key.
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)_Source).CopyTo(array, arrayIndex);
    }

    //Returns an enumerator that iterates through the collection. (Inherited from IEnumerable<(Of <(T>)>).)
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _Source.GetEnumerator();
    }

    //Removes the first occurrence of a specific object from the ICollection<(Of <(T>)>). (Inherited from ICollection<(Of <(T>)>).)
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
        int index = _Source.IndexOfKey(item.Key);
        if (index != -1)
        {
            if (this.raiseItemChangedEvents)
            {
                UnhookPropertyChanged(item.Value);
            }
            ((ICollection<KeyValuePair<TKey, TValue>>)_Source).Remove(item);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            return true;
        }
        return false;
    }

    //Removes the element with the specified key from the IDictionary<(Of <(TKey, TValue>)>). 
    public bool Remove(TKey key)
    {
        int index = _Source.IndexOfKey(key);
        if (index != -1)
        {
            if (this.raiseItemChangedEvents)
            {
                UnhookPropertyChanged(_Source[key]);
            }
            _Source.Remove(key);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            return true;
        } return false;
    }

    //Gets the value associated with the specified key. 
    public bool TryGetValue(TKey key, out TValue value)
    {
        return _Source.TryGetValue(key, out value);
    }
    #endregion

    //Removes all items from the IList. (Inherited from IList.)
    //Removes all items from the ICollection<(Of <(T>)>). (Inherited from ICollection<(Of <(T>)>).)
    public void Clear()
    {

        if (this.raiseItemChangedEvents)
        {
            foreach (TValue item in _Source.Values)
            {
                UnhookPropertyChanged(item);
            }
        }

        _Source.Clear();
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    private void Initialize()
    {

        // Check for INotifyPropertyChanged
        if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(TValue)))
        {
            // Supports INotifyPropertyChanged
            this.raiseItemChangedEvents = true;

            // Loop thru the items already in the collection and hook their change notification.
            foreach (TValue item in _Source.Values)
            {
                HookPropertyChanged(item);
            }
        }
    }

    //Send change notification
    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
        var evt = listChanged; if (evt != null) evt(this, e);
    }

    public void ResetBindings()
    {
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    public bool RaiseListChangedEvents
    {
        get { return this.raiseListChangedEvents; }

        set
        {
            if (this.raiseListChangedEvents != value)
            {
                this.raiseListChangedEvents = value;
            }
        }
    }
    #endregion

    #region Events
    #region IBindingList
    event ListChangedEventHandler IBindingList.ListChanged
    {
        add
        {
            listChanged += value;
        }
        remove { listChanged -= value; }
    }
    #endregion
    #endregion

    #region constructor
    public BindableDictionary()
    {
        _Source = new SortedList<TKey, TValue>();
        Initialize();
    }

    public BindableDictionary(IComparer<TKey> comparer)
    {
        _Source = new SortedList<TKey, TValue>(comparer);
        Initialize();
    }
    #endregion


    #region Property Change Support

    private void HookPropertyChanged(TValue item)
    {
        INotifyPropertyChanged inpc = (item as INotifyPropertyChanged);

        // Note: inpc may be null if item is null, so always check.
        if (null != inpc)
        {
            if (propertyChangedEventHandler == null)
            {
                propertyChangedEventHandler = new PropertyChangedEventHandler(Child_PropertyChanged);
            }
            inpc.PropertyChanged += propertyChangedEventHandler;
        }
    }

    private void UnhookPropertyChanged(TValue item)
    {
        INotifyPropertyChanged inpc = (item as INotifyPropertyChanged);

        // Note: inpc may be null if item is null, so always check.
        if (null != inpc && null != propertyChangedEventHandler)
        {
            inpc.PropertyChanged -= propertyChangedEventHandler;
        }
    }

    void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (this.RaiseListChangedEvents)
        {
            if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
            {
                // Fire reset event (per INotifyPropertyChanged spec)
                ResetBindings();
            }
            else
            {
                TValue item;

                try
                {
                    item = (TValue)sender;
                }
                catch (InvalidCastException)
                {
                    ResetBindings();
                    return;
                }

                // Find the position of the item. This should never be -1. If it is,
                // somehow the item has been removed from our list without our knowledge.
                int pos = lastChangeIndex;

                if (pos < 0 || pos >= Count || !_Source.Values[pos].Equals(item))
                {
                    pos = _Source.IndexOfValue(item);
                    lastChangeIndex = pos;
                }

                if (pos == -1)
                {
                    UnhookPropertyChanged(item);
                    ResetBindings();
                }
                else
                {
                    // Get the property descriptor
                    if (null == this.itemTypeProperties)
                    {
                        // Get Shape
                        itemTypeProperties = TypeDescriptor.GetProperties(typeof(TValue));
                    }

                    PropertyDescriptor pd = itemTypeProperties.Find(e.PropertyName, true);

                    // Create event args. If there was no matching property descriptor,
                    // we raise the list changed anyway.
                    ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.ItemChanged, pos, pd);

                    // Fire the ItemChanged event
                    OnListChanged(args);
                }
            }
        }
    }

    #endregion

    #region IRaiseItemChangedEvents interface

    /// <include file='doc\BindingList.uex' path='docs/doc[@for="BindingList.RaisesItemChangedEvents"]/*' />
    /// <devdoc>
    /// Returns false to indicate that BindingList<T> does NOT raise ListChanged events
    /// of type ItemChanged as a result of property changes on individual list items
    /// unless those items support INotifyPropertyChanged
    /// </devdoc>
    bool IRaiseItemChangedEvents.RaisesItemChangedEvents
    {
        get { return this.raiseItemChangedEvents; }
    }

    #endregion

}
