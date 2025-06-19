using System;

namespace question_6._10
{
  class Bottle
  {
    public bool IsPoisoned = false;

    public int Id;
    
    public Bottle(int Id)
    {
      this.Id = Id;
    }

    public int GetId() { return this.Id; }
  }
}