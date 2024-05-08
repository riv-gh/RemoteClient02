// Decompiled with JetBrains decompiler
// Type: RemoteClient02.Properties.Resources
// Assembly: RemoteClient02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75BB76F9-4EAC-4D1C-BAB5-A4CE19B3DAD1
// Assembly location: C:\Users\User\Desktop\RemoteClient02.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RemoteClient02.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (RemoteClient02.Properties.Resources.resourceMan == null)
          RemoteClient02.Properties.Resources.resourceMan = new ResourceManager("RemoteClient02.Properties.Resources", typeof (RemoteClient02.Properties.Resources).Assembly);
        return RemoteClient02.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => RemoteClient02.Properties.Resources.resourceCulture;
      set => RemoteClient02.Properties.Resources.resourceCulture = value;
    }
  }
}
