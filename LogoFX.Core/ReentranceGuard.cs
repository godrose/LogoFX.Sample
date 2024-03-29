﻿// Partial Copyright (c) LogoUI Software Solutions LTD
// Autor: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

namespace System
{
  /// <summary>
  /// Represents a semaphore for locking user interface updating.
  /// </summary>
  public class ReentranceGuard
  {
    /// <summary>
    /// Represents an automatic reference counter for <see cref="ReentranceGuard"/> class.
    /// </summary>
    class Raiser : IDisposable
    {
      ///////////////////////////////////////////////////////////////////
      // Private members
      private readonly ReentranceGuard _reentranceGuard;
      /// <summary>
      /// Initializes a new instance of the <see cref="Raiser"/> class.
      /// </summary>
      /// <param name="owner">The owner of the instance.</param>
      public Raiser(ReentranceGuard owner)
      {
        _reentranceGuard = owner;
        _reentranceGuard._counter++;
      }
      /// <summary>
      /// Disposes locked application resources.
      /// </summary>
      public void Dispose()
      {
        _reentranceGuard._counter--;
      }
    }

    ///////////////////////////////////////////////////////////////////
    // Private members
    private int _counter = -1;

    ///////////////////////////////////////////////////////////////////
    // Private members
    /// <summary>
    /// Gets automatic reference counter.
    /// </summary>
    public int Counter
    {
      get { return _counter; }
    }
    /// <summary>
    /// Gets user interface lock flag based on the <see cref="Counter"/> value.
    /// </summary>
    public bool IsLocked
    {
      get { return this.Counter > 0; }
    }
    ///////////////////////////////////////////////////////////////////
    // Private members
    /// <summary>
    /// Increments the counter of the references.
    /// </summary>
    /// <returns>The object, which decrements the reference on its disposure.</returns>
    public IDisposable Raise()
    {
      return new Raiser(this);
    }
  }
}
