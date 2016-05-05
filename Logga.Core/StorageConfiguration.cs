using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logga
{
    public abstract class StorageConfiguration
    {
         private static readonly object LockObject = new object();
         private static StorageConfiguration _current;

         public static StorageConfiguration Current
         {
             get
             {
                 lock (LockObject)
                 {
                     if (_current == null)
                     {
                         throw new InvalidOperationException("JobStorage.Current property value has not been initialized. You must set it before using Hangfire Client or Server API.");
                     }

                     return _current;
                 }
             }
             set
             {
                 lock (LockObject)
                 {
                     _current = value;
                 }
             }
         }
    }
}